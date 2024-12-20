using System.Text.Json;
using Bit_Ruisseau.Interface;

namespace Bit_Ruisseau.Classes.Enveloppes
{
    /// <summary>
    /// Class qui représente une demande de musique (Le fichier va être envoyé)
    /// </summary>
    public class AskMusic : IMessage
    {
        /*
            type 4
        */
        private string _file_name;

        public string FileName
        {
            get => _file_name;
            set => _file_name = value;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

