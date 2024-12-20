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
            
            case MessageType.ENVOIE_FICHIER:
                SendMusic enveloppeSendMusic = JsonSerializer.Deserialize<SendMusic>(_envelope.EnveloppeJson);
                MediaData music = enveloppeSendMusic.FileInfo;
                
                byte[] file = Convert.FromBase64String(enveloppeSendMusic.Content);
                string path = $"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics\\{music.Title}{music.Type}";
                File.WriteAllBytes(path, file);
                break;
        }
    }

    public static void AskFile(MediaData media)
    {
        string userTopic = Utils.SendersCatalogs.First(sender => sender.Value.Contains(media)).Key.ToString();
        GenericEnvelope sender = Utils.CreateGenericEnvelop(new List<MediaData>() {media}, MessageType.DEMANDE_FICHIER);
        Utils.SendMessage(P2PEngine.MqttClient, sender, userTopic);
    }
    
}