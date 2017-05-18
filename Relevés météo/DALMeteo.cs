using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Relevés_météo
{
	/// <summary>
	/// Classe permettant de charger les données météo issues d'un fichier csv
	/// et d'en extraire des statistiques
	/// </summary>
	public class DALMeteo
	{
		#region Propriétés

		private List<DonnéesMois> _data;
		public List<DonnéesMois> Data
		{
			get { return _data; }
		}

		private Statistiques _stats;
		public Statistiques Stats
		{
			get { return _stats; }
		}
		#endregion

		// Constructeur
		public DALMeteo()
		{
			_data = new List<DonnéesMois>();
		}

		// Méthode de chargement des données météo depuis un fichier au format csv
		public void ChargerDonnées(string chemin)
		{
			_data.Clear();

			int cpt = 0;
			using (StreamReader str = new StreamReader(chemin))
			{
				string ligne;

				while ((ligne = str.ReadLine()) != null)
				{
					cpt++;
					if (cpt == 1) continue; // On n'analyse pas la première ligne car elle contient les en-têtes

					var tab = ligne.Split('\t');
					var donnéesMois = new DonnéesMois(
						DateTime.Parse(tab[0]),
						double.Parse(tab[1]),
						double.Parse(tab[2]),
						double.Parse(tab[3]),
						double.Parse(tab[4]));

					// Ajout des données du mois à la liste
					_data.Add(donnéesMois);
				}
			}

			// Initialise les statistiques
			_stats = new Statistiques(_data);
		}
	}

	/// <summary>
	///  Classe fournissant des statistiques sur les données météorologiques
	/// </summary>
	public class Statistiques
	{
		#region Propriétés
		public DateTime MoisPlusFroid { get; }
		public double TMin { get; }

		public DateTime MoisPlusChaud { get; }
		public double TMax { get; }

		public int AnnéePlusSèche { get; }
		public double PrécipitationsMin { get; }

		public int AnnéePlusHumide { get; }
		public double PrécipitationsMax { get; }
		#endregion

		private enum Op { Min, Max }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="data">Données météo dont on souhaite extraire des statistiques</param>
		public Statistiques(List<DonnéesMois> data)
		{
			DonnéesMois dm;

			dm = data.Where(D => D.TMin == data.Min(d => d.TMin)).First();
			MoisPlusFroid = dm.Date;
			TMin = dm.TMin;

			dm = data.Where(D => D.TMax == data.Max(d => d.TMax)).First();
			MoisPlusChaud = dm.Date;
			TMax = dm.TMax;

			dm = PrécipitationsAnnée(data, Op.Min);
			AnnéePlusSèche = dm.Année;
			PrécipitationsMin = dm.Précipitations;

			dm = PrécipitationsAnnée(data, Op.Max);
			AnnéePlusHumide = dm.Année;
			PrécipitationsMax = dm.Précipitations;
		}

		// Calcule et renvoie le min ou le max des précipitations annuelles
		// à partir des données passées en paramètre
		private DonnéesMois PrécipitationsAnnée(List<DonnéesMois> data, Op op)
		{
			double precip;
			double P = (op == Op.Min ? 10000 : 0);	// Précipitations min ou max
			int A = 0;	// Année des précipitations min ou max


			foreach (var an in data.Select(d => d.Année).Distinct())
			{
				precip = data.Where(a => a.Année == an).Sum(d => d.Précipitations);
				if (op == Op.Min && precip < P || op == Op.Max && precip > P)
				{
					A = an;
					P = precip;
				}
			}
			return new DonnéesMois(new DateTime(A, 1, 1), 0, 0, P, 0);
		}
	}

	/// <summary>
	/// Classe contenant les données d'un mois de relevé météo
	/// </summary>
	public class DonnéesMois
	{
		public DateTime Date { get; }
		public int Année
		{
			get { return Date.Year; }
		}
		public int Mois
		{
			get { return Date.Month; }
		}
		public double TMin { get; }
		public double TMax { get; }
		public double Précipitations { get; }
		public double Ensoleillement { get; }

        public DonnéesMois(DateTime date, double tmin, double tmax, double précip, double ensol)
		{
			Date = date;
			TMin = tmin;
			TMax = tmax;
			Précipitations = précip;
			Ensoleillement = ensol;
		}
	}
}
