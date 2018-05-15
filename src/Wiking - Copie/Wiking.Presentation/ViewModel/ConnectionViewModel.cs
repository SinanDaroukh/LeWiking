using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiking.Presentation.ViewModel
{
    class ConnectionViewModel : ViewModelBase
    {
        public ConnectionViewModel()
        {
            ConnexionDisplay = false;
        }
        private bool _connexionDisplay = false;
        public bool ConnexionDisplay
        {
            get
            {
                return _connexionDisplay;
            }
            set
            {
                _connexionDisplay = value;
                OnPropertyChanged(nameof(ConnexionDisplay));
            }
        }
    }
}
