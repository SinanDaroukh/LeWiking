using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiking.Presentation.ViewModel
{
    /// <summary>
    /// ViewModel du portfolio de l'appli  
    /// </summary>
    class PortfolioViewModel : ViewModelBase
    {
        #region Attributs
        private string _image;
        private bool _portfolioDisplay;
        #endregion

        #region Constructeur
        public PortfolioViewModel()
        {
            Image = "pack://application:,,,/Wiking.Presentation;component/Images/viking.jpg";
            PortfolioDisplay = true;
        }
        #endregion

        #region Propriétés
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public bool PortfolioDisplay
        {
            get
            {
                return _portfolioDisplay;
            }
            set
            {
                _portfolioDisplay = value;
                OnPropertyChanged(nameof(PortfolioDisplay));
            }
        }
        #endregion

        #region Méthodes
        public void ChangeImage (string image)
        {
            if (image == "viking")
            {
                Image = "pack://application:,,,/Wiking.Presentation;component/Images/viking.png";
            }
            else if (image == "arme")
            {
                Image = "pack://application:,,,/Wiking.Presentation;component/Images/swords.png";
            }
            else if (image == "divinité")
            {
                Image = "pack://application:,,,/Wiking.Presentation;component/Images/thor.png";
            }
            else if (image == "navire")
            {
                Image = "pack://application:,,,/Wiking.Presentation;component/Images/ship.jpg";
            }
        }
        #endregion
    }
}
