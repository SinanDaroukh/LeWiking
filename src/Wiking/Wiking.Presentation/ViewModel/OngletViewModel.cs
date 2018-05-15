using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiking.Presentation.Views;
using Wiking.Presentation.Commands;
using System.Diagnostics;
using System.Globalization;
using static Wiking.VikingDBEntities;
using Microsoft.Win32;
using System.IO;

namespace Wiking.Presentation.ViewModel
{
    class OngletViewModel : ViewModelBase
    {
        ObservableCollection<List<Viking>> lEntites;
        #region Commandes
        public SimpleCommand Suppression { get; private set; }
        public SimpleCommand Ajout { get; private set; }
        public SimpleCommand BackButton { get; private set; }
        public SimpleCommand Modification { get; private set; }
        public SimpleCommand ConfirmerAjout { get; private set; }
        public SimpleCommand ConfirmerModification { get; private set; }
        public SimpleCommand RecuperationImagePourAjout { get; private set; }
        public SimpleCommand RecuperationImagePourModification { get; private set; }
        public SimpleCommand Connexion { get; private set; }
        public SimpleCommand ConfirmerConnection { get; private set; }
        public SimpleCommand Deconnexion { get; private set; }
        public SimpleCommand CreerCompte { get; private set; }
        #endregion

        #region Booleens de visibilité
        // Booléen qui correspond à l'affichage de la description du viking
        private bool _isUCVikingVisible;
        // Booléen qui correspond à l'affichage du bouton de retour
        private bool _isBackButtonVisible;
        // Booléen qui correspond à l'affichage du bouton d'ajout
        private bool _isAddButtonVisible;
        // Booléen qui correspond à l'affichage du bouton de suppression
        private bool _isDeleteButtonVisible;
        // Booléen qui correspond à l'affichage du bouton de modification
        private bool _isUpdateButtonVisible;
        // Booléen qui correspond à l'affichage du bouton de confirmation d'ajout
        private bool _isAddConfirmationButtonVisible;
        // Booléen qui correspond à l'affichage du formulaire de modification
        private bool _isUCModificationVikingVisible;
        // Booléen qui correspond à l'affichage du formulaire de connexion
        private bool _isUCConnexionVisible;
        // Booléen qui correspond à l'affichage du bouton de confirmation de modification
        private bool _isUpdateConfirmationButtonVisible;
        // Booléen qui correspond à l'affichage du descriptif bref, du descriptif détaillé, de l'image, et de la fiche d'identité
        private bool _isDetailVisible;
        // Booléen qui correspond à l'affichage de la bannière
        private bool _isBannerVisible;
        // Booléen qui correspond à l'affichage de la liste de Vikings
        private bool _isMasterVisible;
        // Booléen qui correspond à l'affichage du message qui indique que la création d'un viking n'a pas pu être effectuée
        private bool _isTextBoxCreationImpossibleVisible;
        // Booléen qui correspond à l'affichage du message qui indique que la création d'un viking a pu être effectuée
        private bool _isTextBoxCreationReussieVisible;
        // Booléen qui correspond à l'affichage du message qui indique que la modification d'un viking a pu être effectuée
        private bool _isTextBoxModificationReussieVisible;
        // Booléen qui correspond à l'affichage du message qui indique que la modification d'un viking n'a pas pu être effectuée
        private bool _isTextBoxModificationImpossibleVisible;
        // Booléen qui correspond à l'affichage du bouton de connection
        private bool _isConnectionButtonVisible;
        // Booléen qui correspond à l'affichage du bouton de confirmation de la connexion
        private bool _isConnectionConfirmationButtonVisible;
        // Booléen qui permet de savoir si un utilisateur est connecté ou non
        private bool _isUserConnected;
        // Booléen qui correspond à l'affichage du message de connexion réussie
        private bool _isConnexionReussieVisible;
        // Booléen qui correspond à l'affichage du message de connexion échouée
        private bool _isConnexionImpossibleVisible;
        private bool _isImageVikingVisible;
        // Booléen qui correspond à l'affichage du bouton déconnexion
        private bool _isDisconnectionButtonVisible;
        // Booléen qui correspond à l'affichage du message de connexion échouée
        private bool _isConnectionFailedVisible;
        // Booléen qui correspond à l'affichage du bouton de création de compte
        private bool _isAccountCreationButtonVisible;
        // Booléen qui correspond à l'affichage du message de création de compte échouée à cause de mots de passes différents
        private bool _isDifferentPasswordVisible;
        // Booléen qui correspond à l'affichage du message de création de compte réussie
        private bool _isAccountCreationSucceededVisible;
        #endregion

        #region Attributs pour la connexion
        // Pseudo de la connexion
        private string _pseudo;
        // Mot de passe de la connexion
        private string _motDePasse;
        // Message de bienvenue
        private string _messageDeBienvenue;
        // Pseudo de la création de compte
        private string _login;
        // Mot de passe de la création de compte
        private string _mdp1;
        // Confirmation de mot de passe de la création de compte
        private string _mdp2;

        public String Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public String Mdp1
        {
            get
            {
                return _mdp1;
            }
            set
            {
                _mdp1 = value;
                OnPropertyChanged(nameof(Mdp1));
            }
        }

        public String Mdp2
        {
            get
            {
                return _mdp2;
            }
            set
            {
                _mdp2 = value;
                OnPropertyChanged(nameof(Mdp2));
            }
        }

        public String MessageDeBienvenue
        {
            get
            {
                return _messageDeBienvenue;
            }
            set
            {
                _messageDeBienvenue = value;
                OnPropertyChanged(nameof(MessageDeBienvenue));
            }
        }

        public String Pseudo
        {
            get
            {
                return _pseudo;
            }
            set
            {
                _pseudo = value;
                OnPropertyChanged(nameof(Pseudo));
            }
        }

        public String MotDePasse
        {
            get
            {
                return _motDePasse;
            }
            set
            {
                _motDePasse = value;
                OnPropertyChanged(nameof(MotDePasse));
            }
        }
        #endregion

        #region Instances de Vikings
        // Viking utilisé pour la création
        private Viking _vikingCreated;
        // Viking utilisé pour la modification
        private Viking _vikingUpdated;
        // Index dans la ListView qui correspond au viking sélectionné 
        private int _selectedIndex;
        #endregion

        #region Propriété des vikings
        /// <summary>
        /// Propriété qui correspond à l'index du viking sélectionné dans la ListView
        /// On notifie à la vue les changements d'index et de viking sélectionnés 
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                OnPropertyChanged(nameof(SelectedViking));
            }
        }

        /// <summary>
        /// Propriété qui associe un viking à l'index du sélectionné dans la ListView
        /// Si il n'y plus de viking dans la ListView on renvoie null (n'affiche rien)
        /// </summary>
        public Viking SelectedViking
        {
            get
            {
                if (LesVikings.Count > 0)
                {
                    return SelectedIndex >= 0 ? _lesVikings[SelectedIndex] : null;

                }
                return null;
            }
        }

        public Viking VikingCreated
        {
            get
            {
                return _vikingCreated;
            }
            set
            {
                _vikingCreated = value;
                OnPropertyChanged(nameof(VikingCreated));
            }
        }

        public Viking VikingUpdated
        {
            get
            {
                return _vikingUpdated;
            }
            set
            {
                _vikingUpdated = value;
                OnPropertyChanged(nameof(VikingUpdated));
            }
        }
        #endregion

        #region Propriétés de booléens de visibilité
        public bool IsAccountCreationSucceededVisible
        {
            get
            {
                return _isAccountCreationSucceededVisible;
            }
            set
            {
                _isAccountCreationSucceededVisible = value;
                OnPropertyChanged(nameof(IsAccountCreationSucceededVisible));
            }
        }

        public bool IsDifferentPasswordVisible
        {
            get
            {
                return _isDifferentPasswordVisible;
            }
            set
            {
                _isDifferentPasswordVisible = value;
                OnPropertyChanged(nameof(IsDifferentPasswordVisible));
            }
        }

        public bool IsAccountCreationButtonVisible
        {
            get
            {
                return _isAccountCreationButtonVisible;
            }
            set
            {
                _isAccountCreationButtonVisible = value;
                OnPropertyChanged(nameof(IsAccountCreationButtonVisible));
            }
        }

        public bool IsConnectionFailedVisible
        {
            get
            {
                return _isConnectionFailedVisible;
            }
            set
            {
                _isConnectionFailedVisible = value;
                OnPropertyChanged(nameof(IsConnectionFailedVisible));
            }
        }

        public bool IsDisconnectionButtonVisible
        {
            get
            {
                return _isDisconnectionButtonVisible;
            }
            set
            {
                _isDisconnectionButtonVisible = value;
                OnPropertyChanged(nameof(IsDisconnectionButtonVisible));
            }
        }
        public bool IsImageVikingVisible
        {
            get
            {
                return _isImageVikingVisible;
            }
            set
            {
                _isImageVikingVisible = value;
                OnPropertyChanged(nameof(IsImageVikingVisible));
            }
        }

        public bool IsConnexionReussieVisible
        {
            get
            {
                return _isConnexionReussieVisible;
            }
            set
            {
                _isConnexionReussieVisible = value;
                OnPropertyChanged(nameof(IsConnexionReussieVisible));
            }
        }

        public bool IsConnexionImpossibleVisible
        {
            get
            {
                return _isConnexionImpossibleVisible;
            }
            set
            {
                _isConnexionImpossibleVisible = value;
                OnPropertyChanged(nameof(IsConnexionImpossibleVisible));
            }
        }

        public bool IsUserConnected
        {
            get
            {
                return _isUserConnected;
            }
            set
            {
                _isUserConnected = value;
                OnPropertyChanged(nameof(IsUserConnected));
            }
        }

        public bool IsConnectionConfirmationButtonVisible
        {
            get
            {
                return _isConnectionConfirmationButtonVisible;
            }
            set
            {
                _isConnectionConfirmationButtonVisible = value;
                OnPropertyChanged(nameof(IsConnectionConfirmationButtonVisible));
            }
        }

        public bool IsUCConnexionVisible
        {
            get
            {
                return _isUCConnexionVisible;
            }
            set
            {
                _isUCConnexionVisible = value;
                OnPropertyChanged(nameof(IsUCConnexionVisible));
            }
        }

        public bool IsTextBoxModificationReussieVisible
        {
            get
            {
                return _isTextBoxModificationReussieVisible;
            }
            set
            {
                _isTextBoxModificationReussieVisible = value;
                OnPropertyChanged(nameof(IsTextBoxModificationReussieVisible));
            }
        }
       
        public bool IsTextBoxModificationImpossibleVisible
        {
            get
            {
                return _isTextBoxModificationImpossibleVisible;
            }
            set
            {
                _isTextBoxModificationImpossibleVisible = value;
                OnPropertyChanged(nameof(IsTextBoxModificationImpossibleVisible));
            }
        }

        public bool IsTextBoxCreationImpossibleVisible
        {
            get
            {
                return _isTextBoxCreationImpossibleVisible;
            }
            set
            {
                _isTextBoxCreationImpossibleVisible = value;
                OnPropertyChanged(nameof(IsTextBoxCreationImpossibleVisible));
            }
        }

        public bool IsTextBoxCreationReussieVisible
        {
            get
            {
                return _isTextBoxCreationReussieVisible;
            }
            set
            {
                _isTextBoxCreationReussieVisible = value;
                OnPropertyChanged(nameof(IsTextBoxCreationReussieVisible));
            }
        }

        public bool IsMasterVisible
        {
            get
            {
                return _isMasterVisible;
            }
            set
            {
                _isMasterVisible = value;
                OnPropertyChanged(nameof(IsMasterVisible));
            }
        }

        public bool IsBannerVisible
        {
            get
            {
                return _isBannerVisible;
            }
            set
            {
                _isBannerVisible = value;
                OnPropertyChanged(nameof(IsBannerVisible));
            }
        }

        public bool IsUCVikingVisible
        {
            get
            {
                return _isUCVikingVisible;
            }
            set
            {
                _isUCVikingVisible = value;
                OnPropertyChanged(nameof(IsUCVikingVisible));
            }
        }

        public bool IsBackButtonVisible
        {
            get
            {
                return _isBackButtonVisible;
            }
            set
            {
                _isBackButtonVisible = value;
                OnPropertyChanged(nameof(IsBackButtonVisible));
            }
        }

        public bool IsAddButtonVisible
        {
            get
            {
                return _isAddButtonVisible;
            }
            set
            {
                _isAddButtonVisible = value;
                OnPropertyChanged(nameof(IsAddButtonVisible));
            }
        }

        public bool IsDeleteButtonVisible
        {
            get
            {
                return _isDeleteButtonVisible;
            }
            set
            {
                _isDeleteButtonVisible = value;
                OnPropertyChanged(nameof(IsDeleteButtonVisible));
            }
        }

        public bool IsUpdateButtonVisible
        {
            get
            {
                return _isUpdateButtonVisible;
            }
            set
            {
                _isUpdateButtonVisible = value;
                OnPropertyChanged(nameof(IsUpdateButtonVisible));
            }
        }

        public bool IsAddConfirmationButtonVisible
        {
            get
            {
                return _isAddConfirmationButtonVisible;
            }
            set
            {
                _isAddConfirmationButtonVisible = value;
                OnPropertyChanged(nameof(IsAddConfirmationButtonVisible));
            }
        }

        public bool IsUCModificationVikingVisible
        {
            get
            {
                return _isUCModificationVikingVisible;
            }
            set
            {
                _isUCModificationVikingVisible = value;
                OnPropertyChanged(nameof(IsUCModificationVikingVisible));
            }
        }

        public bool IsUpdateConfirmationButtonVisible
        {
            get
            {
                return _isUpdateConfirmationButtonVisible;
            }
            set
            {
                _isUpdateConfirmationButtonVisible = value;
                OnPropertyChanged(nameof(IsUpdateConfirmationButtonVisible));
            }
        }

        public bool IsDetailVisible
        {
            get
            {
                return _isDetailVisible;
            }
            set
            {
                _isDetailVisible = value;
                OnPropertyChanged(nameof(IsDetailVisible));
            }
        }

        public bool IsConnectionButtonVisible
        {
            get
            {
                return _isConnectionButtonVisible;
            }
            set
            {
                _isConnectionButtonVisible = value;
                OnPropertyChanged(nameof(IsConnectionButtonVisible));
            }
        }
        #endregion

        #region Listes
        // Liste d'enum Sexe à afficher dans le ComboBox du formulaire de modification
        public Array ListEnum { get; set; } = Enum.GetValues(typeof(Sexe));

        // Liste de Vikings
        private ObservableCollection<Viking> _lesVikings = new ObservableCollection<Viking>();
        public ObservableCollection<Viking> LesVikings
        {
            get
            {
                return _lesVikings;
            }
            set
            {
                _lesVikings = value;
                OnPropertyChanged(nameof(LesVikings));
            }
        }
        #endregion

        #region Constructeur
        IDataManager DataManager { get; set; }

        /// <summary>
        /// Constructeur : affichage en fonction de si l'utilisateur est connecté ou non. Initialisation des commandes
        /// </summary>
        /// <param name="dataManager"></param>
        public OngletViewModel(IDataManager dataManager)
        {
            DataManager = dataManager;
            foreach (Viking v in dataManager.LesVikings)
            {
                _lesVikings.Add(v);
            }
            if (IsUserConnected)
            {
                IsAddButtonVisible = true;
                IsDeleteButtonVisible = true;
                IsUpdateButtonVisible = true;
            }
            else
            {
                IsImageVikingVisible = true;
            }
            IsDetailVisible = true;
            IsMasterVisible = true;
            IsConnectionButtonVisible = true;
            BackButton = new SimpleCommand(ClickBackButton);
            Suppression = new SimpleCommand(ClickSuppr);
            Ajout = new SimpleCommand(ClickAj);
            Modification = new SimpleCommand(ClickModif);
            ConfirmerAjout = new SimpleCommand(ClickConfAj);
            ConfirmerModification = new SimpleCommand(ClickConfModif);
            RecuperationImagePourAjout = new SimpleCommand(ClickRecupImgAj);
            RecuperationImagePourModification = new SimpleCommand(ClickRecupImgModif);
            Connexion = new SimpleCommand(ClickConnection);
            ConfirmerConnection = new SimpleCommand(ClickConfConnexion);
            Deconnexion = new SimpleCommand(ClickDeconnexion);
            CreerCompte = new SimpleCommand(ClickCreationCompte);
        }
        #endregion

        #region Méthodes de clic sur boutons
        /// <summary>
        /// Fonction pour créer un compte
        /// </summary>
        /// <param name="o"></param>
        private void ClickCreationCompte(object o)
        {
            if (Mdp1 != Mdp2)
            {
                IsDifferentPasswordVisible = true;
                IsAccountCreationSucceededVisible = false;
            }
            else
            {
                CreateUser(Login, Mdp1, Mdp2);
                IsDifferentPasswordVisible = false;
                IsAccountCreationSucceededVisible = true;
            }
        }

        /// <summary>
        /// Fonction pour se déconnecter
        /// </summary>
        /// <param name="o"></param>
        private void ClickDeconnexion(object o)
        {
            Debug.Write("Vous êtes déconnectés");
            IsConnexionReussieVisible = false;
            IsUserConnected = false;
            IsDisconnectionButtonVisible = false;
            IsAddButtonVisible = false;
            IsDeleteButtonVisible = false;
            IsUpdateButtonVisible = false;
            IsConnectionButtonVisible = true;
            Pseudo = "";
            MotDePasse = "";
        }

        /// <summary>
        /// Fonction pour confirmer la connexion
        /// </summary>
        /// <param name="o"></param>
        private void ClickConfConnexion(object o)
        {
            if(_IsAValidUser(Pseudo, MotDePasse)==null)
            {
                if (!IsUserConnected)
                {
                    Debug.Write("Impossible de se connecter");
                    IsUserConnected = false;
                    IsConnectionFailedVisible = true;
                }
            }
            else
            {
                Debug.Write("Vous etes connecté" + Pseudo + "avec le mot de passe : " + MotDePasse);
                MessageDeBienvenue = $"Bonjour {Pseudo}, vous êtes connecté, bienvenue sur Wiking! Vous pouvez désormais ajouter, supprimer ou modifier un viking!";
                IsConnexionReussieVisible = true;
                IsConnectionFailedVisible = false;
                IsUserConnected = true;
            }
        }

        /// <summary>
        /// Fonction pour afficher le formulaire de connexion
        /// </summary>
        /// <param name="o"></param>
        private void ClickConnection(object o)
        {
            IsUCVikingVisible = false;
            IsUCConnexionVisible = true;
            IsUpdateButtonVisible = false;
            IsAddButtonVisible = false;
            IsDeleteButtonVisible = false;
            IsUpdateButtonVisible = false;
            IsDetailVisible = false;
            IsMasterVisible = false;
            IsBannerVisible = true;
            IsBackButtonVisible = true;
            IsConnectionButtonVisible = false;
            IsConnectionConfirmationButtonVisible = true;
            IsAccountCreationButtonVisible = true;
        }

        /// <summary>
        /// Fonction pour récupérer l'image via une boite de dialogue lors de la création d'un viking
        /// </summary>
        /// <param name="o"></param>
        private void ClickRecupImgAj(object o)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            //File.Copy(openFileDialog1.FileName, "D:/git/wiking/src/Wiking/Wiking.Presentation/Images/" + openFileDialog1.SafeFileName);
            VikingCreated.Image = openFileDialog1.SafeFileName;
            VikingCreated.Path = VikingCreated.Image;
        }

        /// <summary>
        /// Fonction pour récupérer l'image via une boite de dialogue lors de la modification d'un viking
        /// </summary>
        /// <param name="o"></param>
        private void ClickRecupImgModif(object o)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            //File.Copy(openFileDialog1.FileName, "D:/git/wiking/src/Wiking/Wiking.Presentation/Images/" + openFileDialog1.SafeFileName);
            VikingUpdated.Image = openFileDialog1.SafeFileName;
            VikingUpdated.Path = VikingUpdated.Image;
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'on clique sur le bouton de suppression : 
        /// On supprime le viking sélectionné de la liste de vikings
        /// Le premier viking est sélectionné (retour au début de la ListView)
        /// </summary>
        /// <param name="o"></param>
        private void ClickSuppr(object o)
        {
            if (LesVikings.Count > 0)
            {
                IDataManager Manager;
                Manager = new Manager();
                LesVikings.Remove(Manager.RemoveV(SelectedViking));
                SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'on clique sur le bouton d'ajout :
        /// On affiche un formulaire d'ajout à la place de l'affichage du viking
        /// On affiche les boutons "Retour" et "Confirmer Ajout" à la place des boutons "Ajouter", "Supprimer" et "Modifier"
        /// On affiche une bannière à la place de la ListView
        /// On créé une instance vide d'un Viking
        /// </summary>
        /// <param name="o"></param>
        private void ClickAj(object o)
        {
            IsUCVikingVisible = true;
            IsBackButtonVisible = true;
            IsAddButtonVisible = false;
            IsDetailVisible = false;
            IsDeleteButtonVisible = false;
            IsUpdateButtonVisible = false;
            IsAddConfirmationButtonVisible = true;
            IsBannerVisible = true;
            IsMasterVisible = false;
            IsUCConnexionVisible = false;
            IsDisconnectionButtonVisible = false;
            VikingCreated = new Viking();
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'on clique sur le bouton de confirmation d'ajout :
        /// Sachant que les TextBox du formulaire sont bindées sur les propriétés de VikingCreated, on peut récupérer les valeurs de ces TextBox
        /// On modifie les propriétés Path et Identité du viking
        /// On ajoute le viking à la liste
        /// </summary>
        /// <param name="o"></param>
        private void ClickConfAj(object o)
        {
            if (VikingCreated.Name == null || VikingCreated.Mere==null || VikingCreated.Pere==null || VikingCreated.Text1==null || VikingCreated.Text2==null || VikingCreated.Dtn==null || VikingCreated.Dtd==null || VikingCreated.Name == "" || VikingCreated.Mere == "" || VikingCreated.Pere == "" || VikingCreated.Text1 == "" || VikingCreated.Text2 == "")
            {
                Debug.WriteLine("Tous les champs ne sont pas remplis");
                IsTextBoxCreationImpossibleVisible = true;
                IsTextBoxCreationReussieVisible = false;
            }
            else
            {
                IDataManager Manager;
                Manager = new Manager();
                LesVikings.Add(Manager.AddV(VikingCreated));
                SelectedIndex = LesVikings.Count-1;
                IsTextBoxCreationImpossibleVisible = false;
                IsTextBoxCreationReussieVisible = true;
            }
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'on clique sur le bouton de modification :
        /// On affiche un formulaire d'ajout à la place de l'affichage du viking
        /// On affiche les boutons "Retour" et "Confirmer Modification" à la place des boutons "Ajouter", "Supprimer" et "Modifier"
        /// On affiche une bannière à la place de la ListView
        /// On créé une instance de Viking qui récupère les valeurs de propriétés du viking sélectionné dans la ListView
        /// (Ainsi, on binde ce viking plutot que SelectedViking pour que le nom dans la ListView ne soit mis à jour que lorsque
        /// la modification est confirmée)
        /// </summary>
        /// <param name="o"></param>
        private void ClickModif(object o)
        {
            if (LesVikings.Count > 0)
            {
                IsAddButtonVisible = false;
                IsDeleteButtonVisible = false;
                IsUpdateButtonVisible = false;
                IsDetailVisible = false;
                IsUCModificationVikingVisible = true;
                IsBackButtonVisible = true;
                IsUpdateConfirmationButtonVisible = true;
                IsBannerVisible = true;
                IsMasterVisible = false;
                IsUCConnexionVisible = false;
                IsDisconnectionButtonVisible = false;
                VikingUpdated = new Viking(SelectedViking.Name, SelectedViking.Image, SelectedViking.Text1, SelectedViking.Text2, SelectedViking.Sexe, SelectedViking.Mere, SelectedViking.Pere, SelectedViking.Dtn, SelectedViking.Dtd);
            }
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'on clique sur le bouton de confirmation de modification :
        /// On supprime le viking sélectionné de la liste
        /// On met à jour les propriétés Path et Identite de notre nouveau viking
        /// On ajoute le nouveau viking à notre liste
        /// Le premier viking est sélectionné (retour au début de la ListView)
        /// </summary>
        /// <param name="o"></param>
        private void ClickConfModif(object o)
        {
            if (VikingUpdated.Name == null || VikingUpdated.Mere == null || VikingUpdated.Pere == null || VikingUpdated.Text1 == null || VikingUpdated.Text2 == null || VikingUpdated.Dtn == null || VikingUpdated.Dtd == null || VikingUpdated.Image == null || VikingUpdated.Name == "" || VikingUpdated.Mere == "" || VikingUpdated.Pere == "" || VikingUpdated.Text1 == "" || VikingUpdated.Text2 == "" || VikingUpdated.Image == "")
            {
                IsTextBoxModificationImpossibleVisible = true;
                IsTextBoxModificationReussieVisible = false;
            }
            else
            {
                IDataManager Manager;
                Manager = new Manager();
                Manager.UpdateV(SelectedViking, VikingUpdated, LesVikings);
                SelectedIndex = LesVikings.Count - 1;
                IsTextBoxModificationImpossibleVisible = false;
                IsTextBoxModificationReussieVisible = true;
            }
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'on clique sur le bouton de retour :
        /// On affiche la ListView à la place de la bannière
        /// On affiche les boutons "Ajouter", "Supprimer" et "Modifier" à la place des boutons "Retour" et les deux boutons de confirmations
        /// </summary>
        /// <param name="o"></param>
        private void ClickBackButton(object o)
        {
            if (IsUserConnected)
            {
                IsAddButtonVisible = true;
                IsDeleteButtonVisible = true;
                IsUpdateButtonVisible = true;
                IsDisconnectionButtonVisible = true;
            }
            else if (!IsUserConnected)
            {
                IsAddButtonVisible = false;
                IsDeleteButtonVisible = false;
                IsUpdateButtonVisible = false;
                IsConnectionButtonVisible = true;
            }
            IsBackButtonVisible = false;
            IsUCVikingVisible = false;
            IsDetailVisible = true;
            IsAddConfirmationButtonVisible = false;
            IsUpdateConfirmationButtonVisible = false;
            IsUCModificationVikingVisible = false;
            IsMasterVisible = true;
            IsBannerVisible = false;
            IsTextBoxCreationImpossibleVisible = false;
            IsTextBoxCreationReussieVisible = false;
            IsTextBoxModificationImpossibleVisible = false;
            IsTextBoxModificationReussieVisible = false;
            IsUCConnexionVisible = false;
            IsConnectionConfirmationButtonVisible = false;
            IsDifferentPasswordVisible = false;
            IsAccountCreationButtonVisible = false;
        }
        #endregion

    }
}