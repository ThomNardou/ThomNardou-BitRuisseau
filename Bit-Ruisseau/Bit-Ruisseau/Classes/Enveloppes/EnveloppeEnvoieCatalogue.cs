using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit_Ruisseau.Classes.Enveloppes
{
    public class EnveloppeEnvoieCatalogue
    {
        /* 
            type 1
        */
        private List<MediaData> _content;

        public List<MediaData>? Content { get => _content; set => _content = value; }
    }
}
