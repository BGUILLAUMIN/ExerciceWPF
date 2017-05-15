using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Trombinoscope
{
    public class Personne
    {
        public int Id { get; set; }
        public string Prénom { get; set; }
        public string Nom { get; set; }
        public string NomComplet { get; set; }
        public ImageSource Photo { get; set; }
        public string NomManager { get; set; }
        public string PrénomManager { get; set; }
        public List<Territoire> ListeTerritoire { get; set; }
    }
}
