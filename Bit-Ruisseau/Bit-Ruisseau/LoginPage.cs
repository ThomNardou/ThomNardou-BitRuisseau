using System.Net.Sockets;
using System.Net;
using MQTTnet;
using System.Text;
using System.Diagnostics;
using MQTTnet.Protocol;

namespace Bit_Ruisseau
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

                await mqttClient.SubscribeAsync("thomasTest");

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


                for (int i = 0; i < 10; i++)
                {
                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic("thomasTest")
                        .WithPayload($"Hello, MQTT! Message number {i}")
                        .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                        .WithRetainFlag()
                        .Build();

                    await mqttClient.PublishAsync(message);
                    await Task.Delay(1000); // Wait for 1 second
                }
            }
        }
    }
}
