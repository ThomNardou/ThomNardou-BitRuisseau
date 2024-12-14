using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Enums;
using Bit_Ruisseau.Pages;
using MQTTnet;

namespace Bit_Ruisseau.Classes;

public class P2PEngine
{
    public async void Connect(string hostBoxText, string userBoxText, string passwordBoxText, Form _form)
    {
        //todo Environment.SpecialFolder.LocalApplicationData
        if (!Directory.Exists($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics"))
        {
            Directory.CreateDirectory($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics");
        }

        List<string> paths = Directory.GetFiles($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics").ToList();


        paths.ForEach(path =>
        {
            MediaData media = new MediaData();
            var tfile = TagLib.File.Create(path);

            FileInfo fi = new FileInfo(path);
            media.Size = fi.Length;

            media.Title = tfile.Tag.Title;
            media.Type = Path.GetExtension(path);
            media.Artist = tfile.Tag.FirstPerformer;
            TimeSpan duration = tfile.Properties.Duration;
            media.Duration = $"{duration.Minutes:D2}:{duration.Seconds:D2}";
            Utils.Utils.LocalMusicList.Add(media);
        });

        MqttClientFactory factory = new MqttClientFactory();

        var mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(hostBoxText, 1883)
            .WithCredentials(userBoxText, passwordBoxText)
            .WithClientId(Utils.Utils.GetGuid())
            .WithCleanSession()
            .Build();


        var res = await mqttClient.ConnectAsync(options);

        if (res.ResultCode == MqttClientConnectResultCode.Success)
        {
            Debug.WriteLine("Connected to MQTT broker successfully.");
            Debug.WriteLine("GUID: " + Utils.Utils.GetGuid());
            GenericEnvelope sender =
                Utils.Utils.CreateGenericEnvelop(Utils.Utils.LocalMusicList, MessageType.DEMANDE_CATALOGUE);

            Utils.Utils.SendMessage(mqttClient, sender, Utils.Utils.GetGeneralTopic());


            /////////////////////////// RECEIVE MESSAGES EVENT ///////////////////////////
            mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                string receivedMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                GenericEnvelope envelope = JsonSerializer.Deserialize<GenericEnvelope>(receivedMessage);
                
                Console.WriteLine("Message received: " + envelope.MessageType);

                if (envelope.SenderId != Utils.Utils.GetGuid())
                {
                    
                    if (mqttClient == null || !mqttClient.IsConnected)
                    {
                        MessageBox.Show("Client not connected. Reconnecting...");
                        await mqttClient.ConnectAsync(options);
                    }
                    
                    switch (envelope.MessageType)
                    {
                        case MessageType.DEMANDE_CATALOGUE:
                            GenericEnvelope res = Utils.Utils.CreateGenericEnvelop(Utils.Utils.LocalMusicList, MessageType.ENVOIE_CATALOGUE);
                            Utils.Utils.SendMessage(mqttClient, res, Utils.Utils.GetGeneralTopic());

                            Console.WriteLine("Message sent successfully!");
                            break;

                        case MessageType.ENVOIE_CATALOGUE:
                            SendCatalog enveloppeSendCatalog = JsonSerializer.Deserialize<SendCatalog>(envelope.EnveloppeJson);
                            Utils.Utils.SendersCatalogs.Add(envelope.SenderId, enveloppeSendCatalog.Content);

                            enveloppeSendCatalog.Content.ForEach(media => { Utils.Utils.CatalogList.Add(media); });
                            break;
                        
                        case MessageType.DEMANDE_FICHIER:
                            AskMusic enveloppeAskMusic = JsonSerializer.Deserialize<AskMusic>(envelope.EnveloppeJson);
                            MediaData music = Utils.Utils.CatalogList.Find(media => media.Title == enveloppeAskMusic.FileName);

                            Console.WriteLine("Music found: " + music.Title);
                            if (music != null)
                            {
                                string path = $"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics\\{music.Title}{music.Type}";
                                byte[] file = File.ReadAllBytes(path);
                                
                                string base64 = Convert.ToBase64String(file);
                                SendMusic enveloppeSendMusic = new SendMusic
                                {
                                    Type = 3,
                                    Guid = Utils.Utils.GetGuid(),
                                    Content = base64
                                };
                                
                                GenericEnvelope response = new GenericEnvelope
                                {
                                    MessageType = MessageType.ENVOIE_FICHIER,
                                    SenderId = Utils.Utils.GetGuid(),
                                    EnveloppeJson = enveloppeSendMusic.ToJson()
                                };
                                
                                Utils.Utils.SendMessage(mqttClient, response, enveloppeAskMusic.PersonnalTopic);
                            }
                            break;
                    }
                }
            };

            var subBuilder = new MqttTopicFilterBuilder()
                .WithNoLocal(true);
            
            var globalSub = await mqttClient.SubscribeAsync(
                subBuilder.WithTopic(Utils.Utils.GetGeneralTopic()).Build()
            );
            
            var personalSub = await mqttClient.SubscribeAsync(
                subBuilder.WithTopic(Utils.Utils.GetPersonalTopic()).Build()
            );
            
    

            if (globalSub.Items.Count!=1 || personalSub.Items.Count!=1)
            {
                throw new Exception("Cannot subscribe to topics.");
            }


            LobbyPage lobby = new LobbyPage(mqttClient, Utils.Utils.LocalMusicList);
            lobby.Location = _form.Location;
            lobby.StartPosition = FormStartPosition.Manual;
            lobby.FormClosing += delegate { _form.Close(); };
            _form.Hide();
            lobby.Show();
        }
    }
}