using System;
using Wiking;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wiking.Presentation.Views;
using Wiking.Presentation.ViewModel;

namespace Wiking.Presentation
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal OngletViewModel OngletVM { get; } = new OngletViewModel(new Manager());

    }
}  
