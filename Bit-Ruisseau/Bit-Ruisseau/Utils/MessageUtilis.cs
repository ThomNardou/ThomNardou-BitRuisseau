using System.Text.Json;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Enums;
using MQTTnet;

namespace Bit_Ruisseau.Utils;

public class MessageUtilis
{
    public static void OnMessageReceived(GenericEnvelope _envelope, IMqttClient _mqttClient)
    {
        Console.WriteLine("Message received: " + _envelope.MessageType);
        switch (_envelope.MessageType)
        {
            case MessageType.DEMANDE_CATALOGUE:
                GenericEnvelope res = Utils.CreateGenericEnvelop(Utils.LocalMusicList, MessageType.ENVOIE_CATALOGUE);
                Utils.SendMessage(_mqttClient, res, Utils.GetGeneralTopic());
                break;

            case MessageType.ENVOIE_CATALOGUE:
                CatalogUtils.OnCatalogReceived(_envelope);
                break;

            case MessageType.DEMANDE_FICHIER:
                
                
                GenericEnvelope response = FilesUtils.OnFileRequest(_envelope);

                if (response != null)
                {
                    Utils.SendMessage(_mqttClient, FilesUtils.OnFileRequest(_envelope), _envelope.SenderId);
                }

                break;
        }
    }

    
}