using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Enums;
using Bit_Ruisseau.Interface;
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

        public static async void SendMessage(IMqttClient _client, string _message, string _topic, MessageType type)
        {
            GenericEnvelope enveloppe = new GenericEnvelope();
            string classJson = "";
            
            enveloppe.SenderId = GetGuid();

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

        public static GenericEnvelope Create(MessageType type, IMessage message)
        {
            GenericEnvelope envelope = new GenericEnvelope();
            

            envelope.EnveloppeJson = message.ToJson();

            //string json = JsonSerializer.Serialize(enveloppeEnvoieFichier);

            envelope.SenderId = GetGuid();
            envelope.MessageType = type;
            envelope.EnveloppeJson = message.ToJson();

            return envelope;
        }

        public static GenericEnvelope CreateEnveloppeFileSender(string _content, MessageType type)
        {
            GenericEnvelope envelope = new GenericEnvelope();
            EnveloppeEnvoieFichier enveloppeEnvoieFichier = new EnveloppeEnvoieFichier();

            enveloppeEnvoieFichier.Content = _content;

            string json = JsonSerializer.Serialize(enveloppeEnvoieFichier);

            envelope.SenderId = GetGuid();
            envelope.MessageType = type;
            envelope.EnveloppeJson = json;

            return envelope;
        }

        public static GenericEnvelope CreateEnveloppeCatalogSender(List<MediaData> _list, MessageType _type)
        {
            GenericEnvelope envelope = new GenericEnvelope();
            EnveloppeEnvoieCatalogue enveloppeEnvoieFichier = new EnveloppeEnvoieCatalogue();

            enveloppeEnvoieFichier.Content = _list;

            string json = JsonSerializer.Serialize(enveloppeEnvoieFichier);

            envelope.SenderId = GetGuid();
            envelope.MessageType = _type;
            envelope.EnveloppeJson = json;

            return envelope;
        }

        //public static List<MediaData> GetFiles()
        //{

        //}
    }
}
