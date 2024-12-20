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
        
        
        // Liste de musique en Local
        public static List<MediaData> LocalMusicList;
        
        // Liste de catalogue des autres utilisateurs
        public static Dictionary<string, List<MediaData>> SendersCatalogs;
        
        // Liste global des fichiers disponibles
        public static List<MediaData> CatalogList = new List<MediaData>();
        

        public static string GetGuid()
        {
            return "Thomas-asda";
        }

        public static string GetGeneralTopic()
        {
            return "global";
        }
        
        public static string GetPersonalTopic()
        {
            return "thomas";
        }

        public static async void SendMessage(IMqttClient _client, GenericEnvelope _envelope, string _topic)
        {
            
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(_topic)
                .WithPayload(JsonSerializer.Serialize(_envelope))
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag()
                .Build();
            await _client.PublishAsync(message);
        }

        public static GenericEnvelope CreateGenericEnvelop(List<MediaData> _list, MessageType _type)
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
                        Content = _list
                    };

                    response.EnvelopJson = enveloppeCatalogue.ToJson();
                    break;
                case MessageType.DEMANDE_CATALOGUE:
                    AskCatalog askCatalog = new AskCatalog
                    {
                        Content = "Demande de catalogue"
                    };

                    response.EnvelopJson = askCatalog.ToJson();
                    break;
                case MessageType.DEMANDE_FICHIER:
                    AskMusic askMusic = new AskMusic
                    {
                        FileName = _list.First().Title + _list.First().Type
                    };

                    response.EnvelopJson = askMusic.ToJson();
                    break;
            }
            
            
            

            return response;
        }

        //public static List<MediaData> GetFiles()
        //{

        //}
    }
}
