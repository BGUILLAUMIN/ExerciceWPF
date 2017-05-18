using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Trombinoscope
{
    class VMEmployes : ViewModelBase
    {
        private ObservableCollection<Personne> _employes;
        public ObservableCollection<Personne> Employes
        {
            get { return _employes; }
            set { SetProperty(ref _employes, value); }
        }

        private Personne _nouvelEmploye;

        public Personne NouvelEmploye
        {
            get { return _nouvelEmploye; }
            set { _nouvelEmploye = value; }
        }

        public VMEmployes()
        {
            _employes = DAL.GetEmployesInformations();
        }

        private ICommand _cmdAjouter;
        public ICommand CmdAjouter
        {
            get
            {
                if (_cmdAjouter == null)
                    _cmdAjouter = new RelayCommand(AjouterEmploye);

                return _cmdAjouter;
            }
        }

        private ICommand _cmdSupprimer;

        public ICommand CmdSupprimer
        {
            get
            {
                if (_cmdSupprimer == null)
                    _cmdSupprimer = new RelayCommand(SupprimerEmploye);

                return _cmdSupprimer;
            }
        }


        private void AjouterEmploye(object employe)
        {
            WndAjoutEmploye dlg = new WndAjoutEmploye(NouvelEmploye);
            bool? res = dlg.ShowDialog();
            if (res.Value)
            {
                DAL.AjouterEnBDDEmploye(NouvelEmploye);
                Employes = DAL.GetEmployesInformations();
                NouvelEmploye = new Personne();
            }
        }

        private void SupprimerEmploye(object employe)
        {
            try
            {
                var e = (Personne)CollectionViewSource.GetDefaultView(Employes).CurrentItem;
                DAL.SupprimerEnBDDEmploye(e);
                Employes = DAL.GetEmployesInformations();
            }
            catch (Exception)
            {
                MessageBox.Show("Vous ne pouvez pas supprimer cet employé car il est relié dans la base de donnée à une clé étrangère.");
            }
        }
    }
}
