using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bit_Ruisseau.Classes.Enveloppes
{
    public class EnveloppeEnvoieCatalogue
    {
        /*
            type 1
        */
        private int _type;
        private string _guid;
        private List<MediaData> _content;

        public List<MediaData>? Content
        {
            get => _content;
            set => _content = value;
        }

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

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}