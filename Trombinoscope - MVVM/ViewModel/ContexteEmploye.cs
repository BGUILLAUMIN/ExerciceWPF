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

namespace Trombinoscope
{
    public class ContexteEmploye : ViewModelBase
    {
        private ViewModelBase _VMCourante;

        public ViewModelBase VMCourante
        {
            get { return _VMCourante; }
            private set
            {
                SetProperty(ref _VMCourante, value);
            }
        }

        private ICommand _cmdTrombi;
        public ICommand CmdTrombi
        {
            get
            {
                // On définit une instance de VMTrombi comme vue-modèle courante
                if (_cmdTrombi == null)
                    _cmdTrombi = new RelayCommand((object o) => VMCourante = new VMTrombi());
                return _cmdTrombi;
            }
        }

        private ICommand _cmdEmployes;
        public ICommand CmdEmployes
        {
            get
            {
                // On définit une instance de VMEployes comme vue-modèle courante
                if (_cmdEmployes == null)
                    _cmdEmployes = new RelayCommand((object o) => VMCourante = new VMEmployes());
                return _cmdEmployes;
            }
        }

    }
}
