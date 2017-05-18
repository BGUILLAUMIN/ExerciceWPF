using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace BandesDessinées
{
	public static class BD_DAL
	{
		// Charge la liste de collections à partir du fichier xml
		public static List<CollectionBD> ChargerCollectionsBD()
		{
            List<CollectionBD> listCol = null;

			XmlSerializer deserializer = new XmlSerializer(typeof(List<CollectionBD>),
				new XmlRootAttribute("CollectionsBD"));
			using (var sr = new StreamReader(@"..\..\CollectionsBD.xml"))
			{
				// Deserialize renvoie un type object, qu'il faut transtyper 
				listCol = (List<CollectionBD>)deserializer.Deserialize(sr);
			}

			return listCol;
		}
	}

	public class CollectionBD
	{
		// Par défaut les propriétés sont sérialisées en éléments xml
		// Pour les sérialiser en attributs, il faut les décorer de [XmlAttribute]
		[XmlAttribute]
		public string Nom { get; set; }
		public List<Auteur> Auteurs { get; set; }
		public List<Genre> Genres { get; set; }
		public List<Album> Albums { get; set; }
	}

	// Classe nécessaire pour que l'élément xml soit bien nommé <Auteur>
	// Si on utilisait une List<string>, l'élément xml serait nommé <string>
	public class Auteur
	{
		// XMLText désigne le texte à l'intérieur d'un élément
		[XmlText]
		public string Nom { get; set; }
	}

	// Classe nécessaire pour que l'élément xml soit bien nommé <Auteur>
	// Si on utilisait une List<string>, l'élément xml serait nommé <string>
	public class Genre
	{
		[XmlText]
		public string Libelle { get; set; }
	}

	// Par défaut les attributs xml sont nommés comme les propriétés de la classe
	// Mais on peut spécifier un autre nom via [XmlAttribute("xxx")]
	public class Album
	{
		[XmlAttribute]
		public int Id { get; set; }
		[XmlAttribute]
		public string Titre { get; set; }
		[XmlAttribute]
		public int Année { get; set; }
	}
}
