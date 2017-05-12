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
    /// Interaction logic for UCTrombi.xaml
    /// </summary>
    public partial class UCTrombi : UserControl
    {
        public UCTrombi()
        {
            InitializeComponent();
            var listePhotos = DAL.GetEmployeesPhotos();
            foreach (var p in listePhotos)
            {
                var controlImage = new Image();
                controlImage.Source = p;
                controlImage.Width = 200;
                lbxPhotos.Items.Add(controlImage);
            }
        }
    }
}
