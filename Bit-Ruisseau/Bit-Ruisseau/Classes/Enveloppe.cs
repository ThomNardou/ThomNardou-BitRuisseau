using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit_Ruisseau.Classes
{
    public class Enveloppe
    {
        private string _guid;
        private string _content;
        private MediaData? _mediaDatas;

        public string Guid { get => _guid; set => _guid = value; }
        public string Content { get => _content; set => _content = value; }
        public MediaData? MediaDatas { get => _mediaDatas; set => _mediaDatas = value; }
    }
}
