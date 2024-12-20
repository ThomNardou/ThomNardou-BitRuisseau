using System.Text.Json;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Enums;

namespace Bit_Ruisseau.Utils;

public class FilesUtils
{
    public static GenericEnvelope OnFileRequest(GenericEnvelope _enveloppe)
    {
        AskMusic enveloppeAskMusic = JsonSerializer.Deserialize<AskMusic>(_enveloppe.EnveloppeJson);
        
        MediaData music = Utils.LocalMusicList.First(media => $"{media.Title}{media.Type}" == enveloppeAskMusic.FileName);


        if (music != null)
        {
            string path = $"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics\\{music.Title}{music.Type}";
            byte[] file = File.ReadAllBytes(path);
            
            

            string base64 = Convert.ToBase64String(file);
            SendMusic enveloppeSendMusic = new SendMusic
            {
                FileInfo = music,
                Content = base64
            };

            GenericEnvelope response = new GenericEnvelope
            {
                MessageType = MessageType.ENVOIE_FICHIER,
                SenderId = Utils.GetGuid(),
                EnveloppeJson = enveloppeSendMusic.ToJson()
            };

            return response;
        }
        
        return null;
    }
}