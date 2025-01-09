using System.Text.Json;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Enums;
using MQTTnet;

namespace Bit_Ruisseau.Utils;

public class MessageUtilis
{
    
    /// <summary>
    /// Fonction qui est appelée lorsqu'un message est reçu
    /// </summary>
    /// <param name="_envelope"> Enveloppe générique </param>
    /// <param name="_mqttClient"> Client MQTT </param>
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
            
            case MessageType.ENVOIE_FICHIER:
                FilesUtils.DownloadFile(_envelope);
                Utils.DownloadStarted = false;
                break;
        }
    }

    /// <summary>
    /// Fonction qui permet de demander un fichier
    /// </summary>
    /// <param name="media"> Métadonnées du fichier </param>
    public static void AskFile(MediaData media)
    {
        string userTopic = Utils.SendersCatalogs.First(sender => sender.Value.Contains(media)).Key.ToString();
        GenericEnvelope sender = Utils.CreateGenericEnvelop(new List<MediaData>() {media}, MessageType.DEMANDE_FICHIER);
        Utils.SendMessage(P2PEngine.MqttClient, sender, userTopic);
    }
    
    public static void AskCatalog()
    {
        GenericEnvelope sender = Utils.CreateGenericEnvelop(new List<MediaData>(), MessageType.DEMANDE_CATALOGUE);
        Utils.SendMessage(P2PEngine.MqttClient, sender, Utils.GetGeneralTopic());
    }
    
}