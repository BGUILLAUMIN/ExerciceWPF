using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SaisieDeTaches
{
    public class Tache
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Creation")]
        public DateTime Creation { get; set; }

        [XmlAttribute("Term")]
        public DateTime Term { get; set; }

        [XmlAttribute("Prio")]
        public int Prio { get; set; }

        [XmlAttribute("Fait")]
        public bool Fait { get; set; }

        [XmlText]
        public string Description { get; set; } 
    }
}
