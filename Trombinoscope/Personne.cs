using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trombinoscope
{
    public class Personne
    {
        public int Id { get; set; }
        public string Prénom { get; set; }
        public string Nom { get; set; }
        public string NomComplet { get; set; }
        public List<Territoire> ListeTerritoire { get; set; }
    }
}
