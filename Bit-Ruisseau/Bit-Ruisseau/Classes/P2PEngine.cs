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
    public static IMqttClient MqttClient { get; set; }
    public async void Connect(string hostBoxText, string userBoxText, string passwordBoxText, Form _form)
    {
        // Liste et enregistre les fichier locaux
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

            media.Title = fi.Name.Replace(fi.Extension, "");
            media.Type = Path.GetExtension(path);
            media.Artist = tfile.Tag.FirstPerformer;
            TimeSpan duration = tfile.Properties.Duration;
            media.Duration = $"{duration.Minutes:D2}:{duration.Seconds:D2}";
            Utils.Utils.LocalMusicList.Add(media);
        });

        MqttClientFactory factory = new MqttClientFactory();

        var mqttClient = factory.CreateMqttClient();

        // Configuration du client MQTT
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(hostBoxText, 1883)
            .WithCredentials(userBoxText, passwordBoxText)
            .WithClientId(Utils.Utils.GetGuid())
            .WithCleanSession()
            .Build();


        var res = await mqttClient.ConnectAsync(options);

        if (res.ResultCode == MqttClientConnectResultCode.Success)
        {
            Console.WriteLine("Connected to MQTT broker successfully.");
            
            GenericEnvelope sender =
                Utils.Utils.CreateGenericEnvelop(Utils.Utils.LocalMusicList, MessageType.DEMANDE_CATALOGUE);
            
            
            // Abonnement aux topic global et personnel
            var subBuilder = new MqttTopicFilterBuilder()
                .WithNoLocal(true);
            var globalSub = await mqttClient.SubscribeAsync(
                subBuilder.WithTopic(Utils.Utils.GetGeneralTopic()).Build()
            );
            
            var personalSub = await mqttClient.SubscribeAsync(
                subBuilder.WithTopic(Utils.Utils.GetGuid()).Build()
            );
            
            
            // Envoie du message pour demander le catalogue
            Utils.Utils.SendMessage(mqttClient, sender, Utils.Utils.GetGeneralTopic());
            
            MqttClient = mqttClient;

            /////////////////////////// RECEIVE MESSAGES EVENT ///////////////////////////
            mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                string receivedMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                GenericEnvelope envelope = JsonSerializer.Deserialize<GenericEnvelope>(receivedMessage);

                if (envelope.SenderId != Utils.Utils.GetGuid())
                {
                    
                    if (mqttClient == null || !mqttClient.IsConnected)
                    {
                        MessageBox.Show("Client not connected. Reconnecting...");
                        await mqttClient.ConnectAsync(options);
                    }
                    
                    Utils.MessageUtilis.OnMessageReceived(envelope, mqttClient);
                    
                }
                else
                {
                    Console.WriteLine("Message from self.");
                }
            };

            
    

            if (globalSub.Items.Count!=1 || personalSub.Items.Count!=1)
            {
                throw new Exception("Cannot subscribe to topics.");
            }


            // Changement de page vers le lobby
            LobbyPage lobby = new LobbyPage(mqttClient, Utils.Utils.LocalMusicList);
            lobby.Location = _form.Location;
            lobby.StartPosition = FormStartPosition.Manual;
            lobby.FormClosing += delegate { _form.Close(); };
            _form.Hide();
            lobby.Show();
        }
    }
}