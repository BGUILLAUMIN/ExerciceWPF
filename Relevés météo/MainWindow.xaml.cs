using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Relevés_météo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DALMeteo _DALmétéo;
        public MainWindow()
        {
            InitializeComponent();
            Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            _DALmétéo = new DALMeteo();
            btnChemin.Click += BtnChemin_Click;
            cbxVue.SelectionChanged += Cbx_SelectionChanged;
            cbxTri.SelectionChanged += Cbx_SelectionChanged;
            cbxCroissant.SelectionChanged += Cbx_SelectionChanged;
        }


        private void Cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            lbxRelevésMétéo.ItemTemplate = (DataTemplate)Resources[cbxVue.SelectedValue];
            ICollectionView view = CollectionViewSource.GetDefaultView(_DALmétéo.Data);

            if (cbxVue.SelectedValue.ToString() == "RelevésParAnnée")
            {
                view.SortDescriptions.Clear();
                view.GroupDescriptions.Clear();

                var sens = cbxCroissant.SelectedValue.ToString() == "0" ? ListSortDirection.Ascending : ListSortDirection.Descending;
                var tri = new SortDescription(cbxTri.SelectedValue.ToString(), sens);

                view.SortDescriptions.Add(new SortDescription("Année", sens));
                view.GroupDescriptions.Add(new PropertyGroupDescription("Année"));
                
                view.SortDescriptions.Add(tri);
            }
            else
            {
                view.GroupDescriptions.Clear();
                view.SortDescriptions.Clear();
            }
        }

        private void BtnChemin_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
            file.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            file.ShowDialog();

            try
            {
                _DALmétéo.ChargerDonnées(file.FileName);
                DataContext = _DALmétéo;

            }
            catch (Exception)
            {
                MessageBox.Show("Le chemin spécifié n'est pas correct.");
            }

        }
    }
}
