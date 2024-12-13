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

        public static P2PEngine Engine = new P2PEngine();
        
        public static List<MediaData> LocalMusicList;
        public static Dictionary<string, List<MediaData>> SendersCatalogs;
        public static List<MediaData> CatalogList = new List<MediaData>();
        

        public static string GetGuid()
        {
            return "Thomas-" + guid;
        }

        public static string GetTopic()
        {
            return "thomasTest";
        }

        public static async void SendMessage(IMqttClient _client, GenericEnvelope _envelope, string _topic)
        {
            

            await _client.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(_topic)
                .WithNoLocal(true)
                .Build()
                );

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(_topic)
                .WithPayload(JsonSerializer.Serialize(_envelope))
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag()
                .Build();
            await _client.PublishAsync(message);
        }

        public static GenericEnvelope CreateEnveloppeCatalogSender(List<MediaData> _list, MessageType _type)
        {
            GenericEnvelope response = new GenericEnvelope
            {
                MessageType = _type,
                SenderId = GetGuid()
            };

            switch (_type)
            {
                case MessageType.ENVOIE_CATALOGUE:
                    SendCatalog enveloppeCatalogue = new SendCatalog
                    {
                        Type = 1,
                        Guid = GetGuid(),
                        Content = _list
                    };

                    response.EnveloppeJson = enveloppeCatalogue.ToJson();
                    break;
                case MessageType.DEMANDE_CATALOGUE:
                    AskCatalog askCatalog = new AskCatalog
                    {
                        Type = 2,
                        Guid = GetGuid(),
                        Content = "Demande de catalogue"
                    };

                    response.EnveloppeJson = askCatalog.ToJson();
                    break;
            }
            
            
            

            return response;
        }

        //public static List<MediaData> GetFiles()
        //{

        //}
    }
}
