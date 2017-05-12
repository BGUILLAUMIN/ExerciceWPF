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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UCTrombi Trombi { get; set; }
        public UCEmployes  Employes { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            menuTrombinoscope.Click += MenuTrombinoscope_Click;
            menuEmployes.Click += MenuEmployes_Click;
        }

        private void MenuTrombinoscope_Click(object sender, RoutedEventArgs e)
        {
            if (Trombi == null)
            {
                Trombi = new UCTrombi();
            }
            ContentControl.Content = Trombi;
        }
         
        private void MenuEmployes_Click(object sender, RoutedEventArgs e)
        {
            if (Employes == null)
            {
                Employes = new UCEmployes();
            }
            ContentControl.Content = Employes;
        }
    }
}
