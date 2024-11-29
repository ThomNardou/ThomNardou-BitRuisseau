using Bit_Ruisseau.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit_Ruisseau.Classes.Enveloppes
{
    public class EnveloppeEnvoieFichier :IMessage
    {
        /* 
            type 3
        */
        private string _content;
        public string Content { get => _content; set => _content = value; }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
    }
}
