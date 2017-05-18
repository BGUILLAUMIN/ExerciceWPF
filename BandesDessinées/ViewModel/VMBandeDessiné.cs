using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace BandesDessinées
{
    class VMBandeDessiné : ViewModelBase
    {
        private List<CollectionBD> _collectionsBD;
        public List<CollectionBD> CollectionsBD
        {
            get { return _collectionsBD; }
            set { SetProperty(ref _collectionsBD, value); }
        }

        public VMBandeDessiné()
        {
            CollectionsBD = BD_DAL.ChargerCollectionsBD();
        }

        private ICommand _cmdNavigation;

        public ICommand CmdNavigation
        {
            get
            {
                if (_cmdNavigation == null)
                    _cmdNavigation = new RelayCommand(Navigation);

                return _cmdNavigation;
            }
        }

        private void Navigation(object bouton)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(CollectionsBD);

            // Navigue dans la collection selon la direction souhaitée
            switch (bouton.ToString())
            {
                case "F":
                    view.MoveCurrentToFirst(); // premier élément
                    break;
                case "P":
                    if (view.CurrentPosition != 0)
                        view.MoveCurrentToPrevious(); // élément précédent
                    break;
                case "N":
                    view.MoveCurrentToNext();
                    if (view.IsCurrentAfterLast)
                        view.MoveCurrentToPrevious(); // élément suivant
                    break;
                case "L":
                    view.MoveCurrentToLast(); // dernier élément
                    break;
            }
        }
    }
}
