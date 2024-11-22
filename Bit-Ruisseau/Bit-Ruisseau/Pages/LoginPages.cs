using Bit_Ruisseau.Classes;
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

namespace Bit_Ruisseau.Pages
{
    public partial class LoginPages : Form
    {
        public LoginPages()
        {
            InitializeComponent();
            this.hostBox.Text = "inf-n510-p301";
            this.userBox.Text = "ict";
            this.passwordBox.Text = "321";
        }

        private async void connectButton_Click(object sender, EventArgs e)
        {
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

                Utils.Utils.SendMessage(mqttClient, "HELLO, qui a des musiques ?", "testTiago");

                mqttClient.ApplicationMessageReceivedAsync += message =>
                {

                    var payload = Encoding.UTF8.GetString(message.ApplicationMessage.Payload);
                    Enveloppe receivedMessage = JsonSerializer.Deserialize<Enveloppe>(payload);
                    
                    if (receivedMessage.Guid != Utils.Utils.GetGuid())
                    {

                        if (receivedMessage.Content == "HELLO, qui a des musiques ?")
                        {
                            Utils.Utils.SendMessage(mqttClient, "J'en ai pas", "testTiago");
                        }

                        Debug.WriteLine($"EVENT : {payload}");
                        try
                        {
                            return Task.CompletedTask;
                        }
                        catch (Exception e)
                        {
                            return Task.FromException(e);
                        }
                    }

                    return Task.CompletedTask;

                };



                LobbyPage lobby = new LobbyPage(mqttClient);
                lobby.Location = this.Location;
                lobby.StartPosition = FormStartPosition.Manual;
                lobby.FormClosing += delegate { this.Close(); };
                this.Hide();
                lobby.Show();
            }
        }
    }
}
