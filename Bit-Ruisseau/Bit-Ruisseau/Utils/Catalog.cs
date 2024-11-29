using Bit_Ruisseau.Classes.Enveloppes;
using Bit_Ruisseau.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bit_Ruisseau.Enums;
using Bit_Ruisseau.Interface;

namespace Bit_Ruisseau.Message
{
    internal class Catalog : IMessage
    {
        private List<MediaData> _content;
        public List<MediaData>? Content { get => _content; set => _content = value; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
