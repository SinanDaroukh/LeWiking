using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiking
{
    public class Divinite : Freya
    {
        #region Attributs
        private string _fonctionPrincipale;

        #endregion

        #region Propriétés
        public string FonctionPrincipale
        {
            get
            {
                return _fonctionPrincipale;
            }

            set
            {
                _fonctionPrincipale = value;
            }
        }
        #endregion

        #region Constructueur
        public Divinite() { }

        public Divinite(string name, string image, string text1, string text2, string fonctionPrincipale) : base(name, image, text1, text2)
        {
            FonctionPrincipale = fonctionPrincipale;
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return base.ToString() + $"Fonction Principale : {FonctionPrincipale}\n";
        }
        #endregion
    }
}
