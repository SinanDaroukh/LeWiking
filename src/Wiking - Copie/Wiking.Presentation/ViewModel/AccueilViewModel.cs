using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiking.Presentation.Commands;
using Wiking.Presentation.ViewModel;

namespace Wiking.Presentation.ViewModel
{
    /// <summary>
    /// ViewModel de la page d'accueil de l'appli 
    /// </summary>
    class AccueilViewModel : ViewModelBase
    {
        #region Initialisation
        public SimpleCommand Button { get; private set; }
        public SimpleCommand Button2 { get; private set; }
        public SimpleCommand Button3 { get; private set; }
        public SimpleCommand Button4 { get; private set; }
        public SimpleCommand ConnexionButton { get; private set; }
        public SimpleCommand HostButton { get; private set; }
        public SimpleCommand CreationButton { get; private set; }
        public PortfolioViewModel PortfolioVM;
        public ConnectionViewModel Connection;
        private bool _tabDisplay;

        public bool TabDisplay
        {
            get
            {
                return _tabDisplay;
            }
            set
            {
                _tabDisplay = value;
                OnPropertyChanged(nameof(TabDisplay));
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur 
        /// </summary>
        public AccueilViewModel()
        {
            Button = new SimpleCommand(OnClick);
            Button2 = new SimpleCommand(OnClick2);
            Button3 = new SimpleCommand(OnClick3);
            Button4 = new SimpleCommand(OnClick4);
            ConnexionButton = new SimpleCommand(OnClick5);
            HostButton = new SimpleCommand(OnClick6);
            CreationButton = new SimpleCommand(OnClick7);
            PortfolioVM = new PortfolioViewModel();
            Connection = new ConnectionViewModel();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Action lorsque l'on clique sur le bouton de l'onglet "viking"
        /// </summary>
        private void OnClick(object o)
        {
            PortfolioVM.ChangeImage("viking");
        }

        /// <summary>
        /// Action lorsque l'on clique sur le bouton "arme"
        /// </summary>
        private void OnClick2(object o)
        {
            PortfolioVM.ChangeImage("arme");
        }

        /// <summary>
        /// Action lorsque l'on clique sur le bouton "divinité"
        /// </summary>
        private void OnClick3(object o)
        {
            PortfolioVM.ChangeImage("divinité");
        }

        /// <summary>
        /// Action lorsque l'on clique sur le bouton "divinité"
        /// </summary>
        private void OnClick4(object o)
        {
            PortfolioVM.ChangeImage("navire");
        }

        /// <summary>
        /// Action lorsque l'on clique sur le bouton "Connection"
        /// </summary>
        private void OnClick5(object o)
        {
            Connection.ConnexionDisplay = true;
            PortfolioVM.PortfolioDisplay = false;
            TabDisplay = true;
        }

        /// <summary>
        /// Action lorsque l'on clique sur le bouton "Accueil"
        /// </summary>
        private void OnClick6(object o)
        {
            Connection.ConnexionDisplay = false;
            PortfolioVM.PortfolioDisplay = true;
            TabDisplay = false;
        }

        /// <summary>
        /// Action lorsque l'on clique sur le bouton "Creation"
        /// </summary>
        private void OnClick7(object o)
        {
            Connection.ConnexionDisplay = false;
            PortfolioVM.PortfolioDisplay = false;
        }
        #endregion 
    }
}
