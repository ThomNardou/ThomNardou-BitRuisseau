using MQTTnet;
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
        public LoginPages()
        {
            InitializeComponent();
            this.hostBox.Text = "mqtt.blue.section-inf.ch";
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
                .WithClientId(this.userBox.Text + Guid.NewGuid().ToString())
                .WithCleanSession()
                .Build();


            var res = await mqttClient.ConnectAsync(options);

            if (res.ResultCode == MqttClientConnectResultCode.Success)
            {
                Debug.WriteLine("Connected to MQTT broker successfully.");


                mqttClient.ApplicationMessageReceivedAsync += message =>
                {

                    var payload = Encoding.UTF8.GetString(message.ApplicationMessage.Payload);

                    Debug.WriteLine($"{payload}");
                    try
                    {
                        return Task.CompletedTask;
                    }
                    catch (Exception e)
                    {
                        return Task.FromException(e);
                    }
                };

           

                LobbyPage lobby = new LobbyPage(mqttClient);
                lobby.Location = this.Location;
                lobby.StartPosition = FormStartPosition.Manual;
                lobby.FormClosing += delegate {this.Close(); };
                this.Hide();
                lobby.Show();
            }
        }
    }
}
