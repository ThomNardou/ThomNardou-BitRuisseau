using Bit_Ruisseau.Classes;
using MQTTnet;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bit_Ruisseau.Utils
{
    public static class Utils
    {
        private static string guid = Guid.NewGuid().ToString();

        public static string GetGuid()
        {
            return guid;
        }

        public static async void SendMessage(IMqttClient _client, string _message, string _topic)
        {
            Enveloppe enveloppe = new Enveloppe();
            enveloppe.Guid = GetGuid();
            enveloppe.Content = _message;

            await _client.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(_topic)
                .WithNoLocal(true)
                .Build()
                );

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(_topic)
                .WithPayload(JsonSerializer.Serialize(enveloppe))
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag()
                .Build();
            await _client.PublishAsync(message);
        }
    }
}
