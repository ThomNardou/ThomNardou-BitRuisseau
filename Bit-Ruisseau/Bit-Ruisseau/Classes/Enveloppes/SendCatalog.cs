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
    /// Class qui représente le catalogue d'un autre client
    /// </summary>
    public class SendCatalog : IMessage
    {
        /*
            type 1
        */
        private List<MediaData> _content;

        public List<MediaData>? Content
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