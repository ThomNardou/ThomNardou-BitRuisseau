using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bit_Ruisseau.Interface;

namespace Bit_Ruisseau.Classes.Enveloppes
{
    /// <summary>
    /// Class qui représente une demande de catalogue (Le catalogue va être envoyé)
    /// </summary>
    public class AskCatalog : IMessage
    {
        /*
            type 2
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