using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trombinoscope
{
    class VMTrombi : ViewModelBase
    {
        private ObservableCollection<Personne> _employes;
        public ObservableCollection<Personne> Employes
        {
            get { return _employes; }
            set { _employes = value; }
        }

        public VMTrombi()
        {
            _employes = DAL.GetEmployesInformations();
        }
    }
}
