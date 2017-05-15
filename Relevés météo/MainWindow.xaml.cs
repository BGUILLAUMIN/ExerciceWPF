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
            _DALmétéo = new DALMeteo();
            btnChemin.Click += BtnChemin_Click;
            cbxVue.SelectionChanged += CbxVue_SelectionChanged;            
        }

        private void CbxVue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbxVue.SelectedIndex)
            {
                case 0:
                    lbxRelevésMétéo.ItemTemplate = (DataTemplate)Resources["RelevésMétéo"];
                    ControleContent.Visibility = Visibility.Hidden;
                    stack1.Visibility = Visibility.Visible;
                    stack2.Visibility = Visibility.Visible;
                    stack3.Visibility = Visibility.Visible;
                    stack4.Visibility = Visibility.Visible;

                    break;
                case 1:
                    lbxRelevésMétéo.ItemTemplate = (DataTemplate)Resources["RelevésParAnnée"];
                    ControleContent.Visibility = Visibility.Visible;
                    stack1.Visibility = Visibility.Hidden;
                    stack2.Visibility = Visibility.Hidden;
                    stack3.Visibility = Visibility.Hidden;
                    stack4.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
        }

        private void BtnChemin_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
            file.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            bool? result = file.ShowDialog();

            try
            {
                if (result == true)
                    _DALmétéo.ChargerDonnées(file.FileName);
                lbxRelevésMétéo.DataContext = _DALmétéo.Data;
                DataContext = _DALmétéo.Stats;
                ControleContent.DataContext = _DALmétéo.Data;
            }
            catch (Exception)
            {
                MessageBox.Show("Le chemin spécifié n'est pas correct.");
            }
            
        }
    }
}
