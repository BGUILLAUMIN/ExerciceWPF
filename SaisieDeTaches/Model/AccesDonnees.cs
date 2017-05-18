using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SaisieDeTaches
{
    public class AccesDonnees
    {
        /// <summary>
        /// Désérialiser la liste des Taches
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Tache> ChargerTaches()
        {
            ObservableCollection<Tache> Taches = null;
            XmlSerializer deserialiser = new XmlSerializer(typeof(ObservableCollection<Tache>), new XmlRootAttribute("Taches"));

            using (var sr = new StreamReader(@"..\..\Tache.xml"))
            {
                Taches = (ObservableCollection<Tache>)deserialiser.Deserialize(sr);
            }
            return Taches;
        }

        /// <summary>
        /// Sérialiser la liste des Taches
        /// </summary>
        /// <param name="listeTache"></param>
        public static void EnregistrerTaches(ObservableCollection<Tache> listeTache)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Tache>), new XmlRootAttribute("Taches"));

            using (var sw = new StreamWriter(@"..\..\Tache.xml"))
            {
                serializer.Serialize(sw, listeTache);
            }
        }
    }
}
