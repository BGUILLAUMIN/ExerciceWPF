using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SaisieDeTaches
{
    #region Enum
    public enum ModesEdition
    {
        Consultation,
        Edition
    } 
    #endregion
    public class ContexteTache : ViewModelBase
    {
        public ContexteTache()
        {
            _taches = AccesDonnees.ChargerTaches();
            ModeEdit = ModesEdition.Consultation;
        }

        #region Propriété
        private ModesEdition _modeEdit;
        public ModesEdition ModeEdit
        {
            get { return _modeEdit; }
            private set
            {
                SetProperty(ref _modeEdit, value);
            }
        }

        private ObservableCollection<Tache> _taches;
        public ObservableCollection<Tache> Taches
        {
            get { return _taches; }
            private set
            {
                SetProperty(ref _taches, value);
            }
        }

        private Tache _nouvelTache;

        public Tache NouvelTache
        {
            get { return _nouvelTache; }
            set { _nouvelTache = value; }
        }

        #endregion

        #region Commande en propriétés privées
        private ICommand _cmdAjouter;
        public ICommand CmdAjouter
        {
            get
            {
                if (_cmdAjouter == null)
                    _cmdAjouter = new RelayCommand(AjouterTache, o => ModeEdit == ModesEdition.Consultation);

                return _cmdAjouter;
            }
        }

        private ICommand _cmdSupprimer;
        public ICommand CmdSupprimer
        {
            get
            {
                if (_cmdSupprimer == null)
                    _cmdSupprimer = new RelayCommand(SupprimerTache, ActiverEdition);

                return _cmdSupprimer;
            }
        }

        private ICommand _cmdEnregistrer;
        public ICommand CmdEnregistrer
        {
            get
            {
                if (_cmdEnregistrer == null)
                    _cmdEnregistrer = new RelayCommand(EnregistrerTache, ActiverConsultation);

                return _cmdEnregistrer;
            }
        }

        private ICommand _cmdAnnuler;
        public ICommand CmdAnnuler
        {
            get
            {
                if (_cmdAnnuler == null)
                    _cmdAnnuler = new RelayCommand(AnnulerTache, ActiverConsultation);

                return _cmdAnnuler;
            }
        }
        #endregion

        #region Méthodes privées
        private void AjouterTache(object tache)
        {
            ModeEdit = ModesEdition.Edition;
            NouvelTache = new Tache();
            NouvelTache.Creation = DateTime.Today;
            NouvelTache.Term = DateTime.Today;
            NouvelTache.Id = Taches.Select(a => a.Id).DefaultIfEmpty(1).Max()+1;
            NouvelTache.Prio = 1;
            NouvelTache.Fait = false;
            Taches.Add(NouvelTache);

            ICollectionView view = CollectionViewSource.GetDefaultView(Taches);
            view.MoveCurrentToLast();
        }

        private void SupprimerTache(object o)
        {
            var t = (Tache)CollectionViewSource.GetDefaultView(Taches).CurrentItem;
            Taches.Remove(t);
            AccesDonnees.EnregistrerTaches(Taches);
        }

        private void EnregistrerTache(object o)
        {
            ModeEdit = ModesEdition.Consultation;
            AccesDonnees.EnregistrerTaches(Taches);
        }

        private void AnnulerTache(object o)
        {
            ModeEdit = ModesEdition.Consultation;
            Taches.Remove(Taches.Last());
            
        }

        private bool ActiverEdition(object o)
        {
            return (ModeEdit == ModesEdition.Consultation);
        }
        private bool ActiverConsultation(object tache)
        {
            return (ModeEdit == ModesEdition.Edition);
        }
        #endregion
    }
}
