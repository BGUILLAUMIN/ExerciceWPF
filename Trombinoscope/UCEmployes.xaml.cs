using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trombinoscope
{
    /// <summary>
    /// Interaction logic for UCEmployes.xaml
    /// </summary>
    public partial class UCEmployes : UserControl
    {
        public List<Personne> ListeEmployes { get; set; }
        public UCEmployes()
        {
            InitializeComponent();
            lbxNomEmployes.SelectionChanged += LbxNomEmployes_SelectionChanged;
            ListeEmployes = DAL.GetEmployesInformations();
            tbkId.Visibility = Visibility.Hidden;
            tbkNom.Visibility = Visibility.Hidden;
            tbkPrenom.Visibility = Visibility.Hidden;
            foreach (var p in ListeEmployes)
            {
                lbxNomEmployes.DisplayMemberPath = "NomComplet";
                lbxNomEmployes.SelectedValuePath = "Id";
                lbxNomEmployes.Items.Add(p);
            }
        }

        private void LbxNomEmployes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbkId.Visibility = Visibility.Visible;
            tbkNom.Visibility = Visibility.Visible;
            tbkPrenom.Visibility = Visibility.Visible;
            var p = (Personne)lbxNomEmployes.SelectedItem;
                tbkId.Text = "ID : " + (p.Id).ToString();
                tbkNom.Text = "Nom : " + p.Nom;
                tbkPrenom.Text = "Prénom : " + p.Prénom;
        }
    }
}
