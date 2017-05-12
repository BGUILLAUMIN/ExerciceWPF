using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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

        public static List<Personne> GetEmployesInformations()
        {
            List<Personne> listeEmployes = new List<Personne>();
            string queryString = @"select EmployeeID, LastName, FirstName from Employees";
            string connectString = Properties.Settings.Default.ConnectionString;

            using (var connect = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connect);
                connect.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Personne p = new Personne();
                        p.Id = (int)reader[0];
                        p.Nom = (string)reader[1];
                        p.Prénom = (string)reader[2];
                        p.NomComplet = (string)reader[2] + " " + (string)reader[1];
                        listeEmployes.Add(p);
                    }
                }
            }
            return listeEmployes;
        }

    }
}
