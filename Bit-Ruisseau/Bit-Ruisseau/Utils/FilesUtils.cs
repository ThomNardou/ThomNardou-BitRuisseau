using System.Text.Json;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Enums;

namespace Bit_Ruisseau.Utils;

public class FilesUtils
{
    public static GenericEnvelope OnFileRequest(GenericEnvelope _enveloppe)
    {
        AskMusic enveloppeAskMusic = JsonSerializer.Deserialize<AskMusic>(_enveloppe.EnvelopJson);
        
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
                EnvelopJson = enveloppeSendMusic.ToJson()
            };

            return response;
        }
        
        return null;
    }
    
    public static void DownloadFile(GenericEnvelope _envelope)
    {
        SendMusic enveloppeSendMusic = JsonSerializer.Deserialize<SendMusic>(_envelope.EnvelopJson);
        MediaData music = enveloppeSendMusic.FileInfo;
                
        byte[] file = Convert.FromBase64String(enveloppeSendMusic.Content);
        string path = $"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics\\{music.Title}{music.Type}";
        File.WriteAllBytes(path, file);
        
        MessageBox.Show("Fichier téléchargé avec succès !");
    }
}