using MQTTnet;

namespace Bit_Ruisseau.Classes;

public class P2PEngine
{
    public void Connect(string hostBoxText, string userBoxText, string passwordBoxText)
    {
        //todo Environment.SpecialFolder.LocalApplicationData
            if (!Directory.Exists($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics"))
            {
                Directory.CreateDirectory($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics");
            }
            
            string[] path = Directory.GetFiles($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics");
            
            //todo fonctionnel
            foreach (string file in path)
            {
                MediaData media = new MediaData();
                var tfile = TagLib.File.Create(file);
                
                FileInfo fi = new FileInfo(file);
                media.File_size = fi.Length;
                
                media.File_name = tfile.Tag.Title;
                media.File_type = Path.GetExtension(file);
                media.File_artist = tfile.Tag.FirstPerformer;
                TimeSpan duration = tfile.Properties.Duration;
                media.File_duration = $"{duration.Minutes:D2}:{duration.Seconds:D2}";
                LocalMusicList.Add(media);
            }
            
            MqttClientFactory factory = new MqttClientFactory();

            var mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(this.hostBox.Text, 1883)
                .WithCredentials(this.userBox.Text, this.passwordBox.Text)
                .WithClientId(Utils.Utils.GetGuid())
                .WithCleanSession()
                .Build();


            var res = await mqttClient.ConnectAsync(options);

            if (res.ResultCode == MqttClientConnectResultCode.Success)
            {
                Debug.WriteLine("Connected to MQTT broker successfully.");
                GenericEnvelope demande = Utils.Utils.CreateEnveloppeCatalogSender(LocalMusicList, MessageType.DEMANDE_CATALOGUE);

                Utils.Utils.SendMessage(mqttClient, demande, Utils.Utils.GetTopic());

                
                /////////////////////////// RECEIVE MESSAGES EVENT ///////////////////////////
                mqttClient.ApplicationMessageReceivedAsync += async e =>
                {
                    string receivedMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    
                    GenericEnvelope envelope = JsonSerializer.Deserialize<GenericEnvelope>(receivedMessage);

                    if (envelope.SenderId != Utils.Utils.GetGuid())
                    {
                        switch (envelope.MessageType)
                        {
                            case MessageType.DEMANDE_CATALOGUE:
                                GenericEnvelope res = Utils.Utils.CreateEnveloppeCatalogSender(LocalMusicList, MessageType.ENVOIE_CATALOGUE);
        
                                if (mqttClient == null || !mqttClient.IsConnected)
                                {
                                    MessageBox.Show("Client not connected. Reconnecting...");
                                    await mqttClient.ConnectAsync(options);
                                }

                                Utils.Utils.SendMessage(mqttClient, res, Utils.Utils.GetTopic());
                        
                                Console.WriteLine("Message sent successfully!");
                                break;
                            
                            case MessageType.ENVOIE_CATALOGUE:
                                EnveloppeEnvoieCatalogue enveloppe = JsonSerializer.Deserialize<EnveloppeEnvoieCatalogue>(envelope.EnveloppeJson);
                                SendersCatalogs.Add(envelope.SenderId, enveloppe.Content);
                                break;
                        }
                    }
                };



                LobbyPage lobby = new LobbyPage(mqttClient, LocalMusicList);
                lobby.Location = this.Location;
                lobby.StartPosition = FormStartPosition.Manual;
                lobby.FormClosing += delegate { this.Close(); };
                this.Hide();
                lobby.Show();
            }
        }



}