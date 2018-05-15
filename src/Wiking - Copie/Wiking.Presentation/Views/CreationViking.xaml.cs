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
using Wiking.Presentation.ViewModel;

namespace Wiking.Presentation.Views
{
    /// <summary>
    /// Logique d'interaction pour CreationViking.xaml
    /// </summary>
    public partial class CreationViking : UserControl
    {
        internal OngletViewModel OngletVM
        {
            get
            {
                return (App.Current as App).OngletVM;
            }
        }
        public CreationViking()
        {
            InitializeComponent();
            DataContext = OngletVM;
        }
    }
}
