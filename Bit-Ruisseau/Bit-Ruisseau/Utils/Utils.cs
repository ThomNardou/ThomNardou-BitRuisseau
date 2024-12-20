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
        

        /// <summary>
        /// Fonction qui retourne un GUID
        /// </summary>
        /// <returns>
        /// Un GUID en string
        /// </returns>
        public static string GetGuid()
        {
            return "Thomas-asda";
        }

        /// <summary>
        /// Fonction qui retourne le topic global du broker
        /// </summary>
        /// <returns>
        /// Un string qui représente le topic global
        /// </returns>
        public static string GetGeneralTopic()
        {
            return "global";
        }

        /// <summary>
        /// Fonction qui permet d'envoyer un message sur un topic donné
        /// </summary>
        /// <param name="_client"> Client mqtt </param>
        /// <param name="_envelope"> Enveloppe du message </param>
        /// <param name="_topic"> Topic sur lequel le message va être envoyé </param>
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

        /// <summary>
        /// Fonction qui permet de créer une enveloppe générique
        /// </summary>
        /// <param name="_list"> Paramètre qui contient des information des fichiers </param>
        /// <param name="_type"> Type du message </param>
        /// <returns> Une enveloppe générique </returns>
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

                    response.EnvelopeJson = enveloppeCatalogue.ToJson();
                    break;
                case MessageType.DEMANDE_CATALOGUE:
                    AskCatalog askCatalog = new AskCatalog
                    {
                        Content = "Demande de catalogue"
                    };

                    response.EnvelopeJson = askCatalog.ToJson();
                    break;
                case MessageType.DEMANDE_FICHIER:
                    AskMusic askMusic = new AskMusic
                    {
                        FileName = _list.First().Title + _list.First().Type
                    };

                    response.EnvelopeJson = askMusic.ToJson();
                    break;
            }
            return response;
        }
    }
}
