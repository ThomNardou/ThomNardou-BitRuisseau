using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Utils;
using MQTTnet;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bit_Ruisseau.Enums;

namespace Bit_Ruisseau.Pages
{
    public partial class LoginPages : Form
    {
        private List<MediaData> LocalMusicList;
        public LoginPages()
        {
            InitializeComponent();
            LocalMusicList = new List<MediaData>();
            this.hostBox.Text = "blue.section-inf.ch";
            this.userBox.Text = "ict";
            this.passwordBox.Text = "321";
        }

        private async void connectButton_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics"))
            {
                Directory.CreateDirectory($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics");
            }
            
            string[] path = Directory.GetFiles($"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics");
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

                Utils.Utils.SendMessage(mqttClient, "HELLO, qui a des musiques ?", Utils.Utils.GetTopic(), MessageType.DEMANDE_CATALOGUE);

                
                
                mqttClient.ApplicationMessageReceivedAsync += async e =>
                {
                    string receivedMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    
                    GenericEnvelope envelope = JsonSerializer.Deserialize<GenericEnvelope>(receivedMessage);    

                    if (envelope.MessageType == MessageType.DEMANDE_CATALOGUE)
                    {
                        EnveloppeEnvoieCatalogue enveloppeCatalogue = new EnveloppeEnvoieCatalogue();
                        enveloppeCatalogue.Type = 1;
                        enveloppeCatalogue.Guid = Utils.Utils.GetGuid();
                        enveloppeCatalogue.Content = LocalMusicList;
                        
                        GenericEnvelope response = new GenericEnvelope();
                        response.MessageType = MessageType.ENVOIE_CATALOGUE;
                        response.SenderId = Utils.Utils.GetGuid();
                        response.EnveloppeJson = enveloppeCatalogue.ToJson();
        
                        if (mqttClient == null || !mqttClient.IsConnected)
                        {
                            MessageBox.Show("Client not connected. Reconnecting...");
                            await mqttClient.ConnectAsync(options);
                        }

                        // Créez le message à envoyer
                        var message = new MqttApplicationMessageBuilder()
                            .WithTopic(Utils.Utils.GetTopic())
                            .WithPayload(JsonSerializer.Serialize(response))
                            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                            .WithRetainFlag(false)
                            .Build();

                        // Envoyez le message
                        mqttClient.PublishAsync(message);
                        Console.WriteLine("Message sent successfully!");
                    }

                    return;
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
}
