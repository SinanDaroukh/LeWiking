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
using System.Windows.Shapes;
using Wiking.Presentation.ViewModel;

namespace Wiking.Presentation.Views
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        private AccueilViewModel _vm;

        public Accueil()
        {
            InitializeComponent();

            _vm = new AccueilViewModel();

            DataContext = _vm;
            PortFolioUC.DataContext = _vm.PortfolioVM;
            ConnectionUC.DataContext = _vm.Connection;
        }
    }
}
