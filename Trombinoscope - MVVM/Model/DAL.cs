using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Trombinoscope
{
    class DAL
    {
        /// <summary>
        /// Récupère les photos des employés depuis la BDD
        /// </summary>
        /// <returns></returns>
        public static List<ImageSource> GetEmployeesPhotos()
        {
            List<ImageSource> listePhotos = new List<ImageSource>();
            string connectString = Properties.Settings.Default.ConnectionString;
            var queryString = @"select Photo from Employees";

            using (var connect = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connect);
                connect.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listePhotos.Add(ConvertBytesToImageSource((Byte[])reader[0]));
                    }
                }
            }
            return listePhotos;
        }

        /// <summary>
        /// Converti les photos des employés en type ImageSource
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        private static ImageSource ConvertBytesToImageSource(Byte[] tab)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Les images stockées dans la base Northwind ont un en-tête de 78 octets 
                // qu'il faut enlever pour pouvoir les charger correctement
                ms.Write(tab, 78, tab.Length - 78);
                ImageSource image = BitmapFrame.Create(ms, BitmapCreateOptions.None,
                                      BitmapCacheOption.OnLoad);
                return image;
            }
        }

        /// <summary>
        /// Récupère toutes les informations des employés dans une liste de Personne
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Personne> GetEmployesInformations()
        {
            int IdCourant;
            Personne p = null;
            ObservableCollection<Personne> listeEmployes = new ObservableCollection<Personne>();
            string queryString = @"select E.EmployeeID, E.LastName, E.FirstName, E.Photo, M.FirstName, M.LastName, T.TerritoryID, T.TerritoryDescription from Employees E
                                   left outer join EmployeeTerritories ET on ET.EmployeeID = E.EmployeeID
                                   left outer join Territories T on ET.TerritoryID = T.TerritoryID
                                   left outer join Employees M on M.EmployeeID = E.ReportsTo";
            string connectString = Properties.Settings.Default.ConnectionString;

            using (var connect = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connect);
                connect.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IdCourant = (int)reader[0];
                        if (listeEmployes.Count == 0 || listeEmployes.Last().Id != IdCourant)
                        {
                            p = new Personne();
                            p.Territoires = new List<Territoire>();
                            p.Id = (int)reader[0];
                            p.Nom = (string)reader[1];
                            p.Prénom = (string)reader[2];
                            p.NomComplet = (string)reader[2] + " " + (string)reader[1];
                            if(reader[3] != DBNull.Value)
                                p.Photo = ConvertBytesToImageSource((Byte[])reader[3]);
                            if (reader[4] != DBNull.Value)
                                p.PrénomManager = (string)reader[4];
                            if (reader[5] != DBNull.Value)
                                p.NomManager = (string)reader[5];
                            listeEmployes.Add(p);
                        }
                        else p = listeEmployes[listeEmployes.Count - 1];

                        Territoire ter = new Territoire();
                        if (reader[6] != DBNull.Value)
                            ter.IdTerritoire = (string)reader[6];
                        if (reader[7] != DBNull.Value)
                            ter.DscrpTerritoire = (string)reader[7];
                        p.Territoires.Add(ter);
                    }
                }
            }
            return listeEmployes;
        }

        public static void AjouterEnBDDEmploye(Personne p)
        {
            string connectString = Properties.Settings.Default.ConnectionString;
            string queryString = @"INSERT Employees(FirstName, LastName) VALUES (@Prénom, @Nom)";
            var paramNom = new SqlParameter("@Nom", DbType.String);
            var paramPrénom = new SqlParameter("@Prénom", DbType.String);
            paramNom.Value = p.Nom;
            paramPrénom.Value = p.Prénom;

            using (var connect = new SqlConnection(connectString))
            {
                connect.Open();
                var command = new SqlCommand(queryString, connect);
                command.Parameters.Add(paramNom);
                command.Parameters.Add(paramPrénom);
                command.ExecuteNonQuery();
            }
        }

        public static void SupprimerEnBDDEmploye(Personne p)
        {
            string connectString = Properties.Settings.Default.ConnectionString;
            string queryString = @"DELETE FROM Employees WHERE EmployeeId = @ID";
            var param = new SqlParameter("@ID", DbType.Int32);
            param.Value = p.Id;

            using (var connect = new SqlConnection(connectString))
            {
                connect.Open();
                SqlTransaction tran = connect.BeginTransaction();

                try
                {
                    var command = new SqlCommand(queryString, connect, tran);
                    command.Parameters.Add(param);
                    command.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (Exception )
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
