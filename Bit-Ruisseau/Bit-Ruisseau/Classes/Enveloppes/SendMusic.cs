using Bit_Ruisseau.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bit_Ruisseau.Classes.Enveloppes
{
    /// <summary>
    /// Class qui représente le fichier d'un autre client
    /// </summary>
    public class SendMusic : IMessage
    {
        /*
            type 3
        */
        private string _content;

        public string Content
        {
            get => _content;
            set => _content = value;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}