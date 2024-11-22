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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bit_Ruisseau.Pages
{
    public partial class LoginPages : Form
    {
        string guid = Guid.NewGuid().ToString();
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
                .WithClientId(guid)
                .WithCleanSession()
                .Build();


            var res = await mqttClient.ConnectAsync(options);

            if (res.ResultCode == MqttClientConnectResultCode.Success)
            {
                Debug.WriteLine("Connected to MQTT broker successfully.");

                sendMSG(mqttClient, "HELLO, qui a des musiques ?");

                mqttClient.ApplicationMessageReceivedAsync += message =>
                {
                    Debug.WriteLine(message.ClientId + " VS " + guid);
                    if (message.ClientId != guid)
                    {

                        var payload = Encoding.UTF8.GetString(message.ApplicationMessage.Payload);

                        if (payload.ToString() == "HELLO, qui a des musiques ?")
                        {
                            sendMSG(mqttClient, "J'en ai pas");
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

        public async void sendMSG(IMqttClient mqttClient, string _msg)
        {
            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                                .WithTopic("test")
                                .WithNoLocal(true)
                                .Build()
                                );

            var msg = new MqttApplicationMessageBuilder()
                .WithTopic("test")
                .WithPayload(_msg)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag()
                .Build();
            await mqttClient.PublishAsync(msg);
        }
    }
}
