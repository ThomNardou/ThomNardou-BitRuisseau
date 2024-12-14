using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bit_Ruisseau.Interface;

namespace Bit_Ruisseau.Classes.Enveloppes
{
    public class AskCatalog : IMessage
    {
        /*
            type 2
        */
        private int _type;
        private string _guid;
        private string _content;

        public int Type
        {
            get => _type;
            set => _type = value;
        }

        public string Guid
        {
            get => _guid;
            set => _guid = value;
        }

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