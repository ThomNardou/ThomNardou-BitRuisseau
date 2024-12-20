using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit_Ruisseau.Classes
{
    /// <summary>
    /// Class qui représente les métadonnées d'un fichier
    /// </summary>
    public class MediaData
    {
        private string _title;
        private string _artist;
        private string _type;
        private long _size;
        private string _duration;

        public string Title { get => _title; set => _title = value; }
        public string Artist { get => _artist; set => _artist = value; }
        public string Type { get => _type; set => _type = value; }
        public long Size { get => _size; set => _size = value; }
        public string Duration { get => _duration; set => _duration = value; }

    }
}
