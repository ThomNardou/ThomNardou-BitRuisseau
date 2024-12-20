using System.Text.Json;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Enums;

namespace Bit_Ruisseau.Utils;

public class FilesUtils
{
    
    /// <summary>
    /// Fonction qui permet de gérer une demande de fichier
    /// </summary>
    /// <param name="_enveloppe"></param>
    /// <returns> Une enveloppe générique </returns>
    public static GenericEnvelope OnFileRequest(GenericEnvelope _enveloppe)
    {
        AskMusic enveloppeAskMusic = JsonSerializer.Deserialize<AskMusic>(_enveloppe.EnvelopeJson);
        
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
                EnvelopeJson = enveloppeSendMusic.ToJson()
            };

            return response;
        }
        
        return null;
    }
    
    /// <summary>
    /// Fonction qui permet de télécharger un fichier
    /// </summary>
    /// <param name="_envelope"> Générique enveloppe reçu </param>
    public static void DownloadFile(GenericEnvelope _envelope)
    {
        SendMusic enveloppeSendMusic = JsonSerializer.Deserialize<SendMusic>(_envelope.EnvelopeJson);
        MediaData music = enveloppeSendMusic.FileInfo;
                
        byte[] file = Convert.FromBase64String(enveloppeSendMusic.Content);
        string path = $"C:\\Users\\{Environment.UserName}\\Bit-Ruisseau\\Musics\\{music.Title}{music.Type}";
        File.WriteAllBytes(path, file);
        
        MessageBox.Show("Fichier téléchargé avec succès !");
    }
}