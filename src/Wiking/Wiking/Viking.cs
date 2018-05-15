using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System.ComponentModel;

namespace Wiking
{
    public class Viking : Freya
    {
        #region Attributs
        private Sexe _sex;
        private string _pere;
        private string _mere;
        private string _identite;
        public DateTime Dtn { get; set; }
        public DateTime Dtd { get; set; }
        #endregion

        #region Propriétés
        /// <summary>
        /// Sexe (M : Masculin ou F : Féminin)
        /// </summary>
        public Sexe Sexe
        {
            get
            {
                return _sex;
            }

            set
            {
                _sex = value;
                OnPropertyChanged(nameof(Sexe));
                OnPropertyChanged(nameof(Identite));
            }
        }

        /// <summary>
        /// Pere
        /// </summary>
        public string Pere
        {
            get
            {
                return _pere;
            }

            set
            {
                _pere = value;
                OnPropertyChanged(nameof(Pere));
                OnPropertyChanged(nameof(Identite));
            }
        }

        /// <summary>
        /// Mere
        /// </summary>
        public String Mere
        {
            get
            {
                return _mere;
            }

            set
            {
                _mere = value;
                OnPropertyChanged(nameof(Mere));
                OnPropertyChanged(nameof(Identite));
            }
        }

        /// <summary>
        /// Fiche d'identité qui permet d'afficher dans le détail certains attributs (initialisé dans le constructeur)
        /// </summary>
        public string Identite
        {
            get
            {
                return _identite;
            }
            set
            {
                _identite = $"Nom : {Name} \nSexe : {Sexe} \nMere : {Mere} \nPere : {Pere} \nDate de Naissance : {Dtn.ToShortDateString()} \nDate de décès : {Dtd.ToShortDateString()}";
                OnPropertyChanged(nameof(Identite));
            }
        }


        #endregion

        #region Constructeurs
        //Constructeur Vide Utile pr echapper à l'exception chelou
        public Viking() { }

        /// <summary>
        /// Constructeur d'un viking qui permet également d'initialiser les propriétés Path et Identité
        /// </summary>
        /// <param name="name"></param>
        /// <param name="image"></param>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <param name="sex"></param>
        /// <param name="mere"></param>
        /// <param name="pere"></param>
        /// <param name="dtn"></param>
        /// <param name="dtd"></param>
        public Viking(string name, string image, string text1, string text2, Sexe sex, String mere, String pere, DateTime dtn, DateTime dtd) : base(name, image, text1, text2)
        {
            Sexe = sex;
            Pere = pere;
            Mere = mere;
            Dtn = dtn;
            Dtd = dtd;
            Identite = _identite;
        }
        #endregion

        #region Méthodes à redéfinir
        /// <summary>
        /// Equals : pour pouvoir comparer un objet et un viking
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public override bool Equals(object right)
        {
            //check null
            if (object.ReferenceEquals(right, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, right))
            {
                return true;
            }

            if (this.GetType() != right.GetType())
            {
                return false;
            }

            return this.Equals(right as Viking);
        }

        /// <summary>
        /// Equals : Compare un viking avec un autre viking en comparant chaque attribut
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Viking other)
        {
            return (this.Name.Equals(other.Name) && this.Pere == other.Pere && this.Mere == other.Mere && this.Dtn == other.Dtn && this.Dtd == other.Dtd && this.Sexe == other.Sexe);
        }

        /// <summary>
        /// Renvoie un HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() + 17 * Sexe.GetHashCode() + 17 * Pere.GetHashCode() + 17 + Mere.GetHashCode() + 17 * Dtn.GetHashCode() + 17 * Dtd.GetHashCode();
        }

        /// <summary>
        /// Renvoie une chaine de caractères qui décrit le viking
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"Sexe : {Sexe} \nPere : {Pere} \nMere : {Mere} \nDate de Naissance : {Dtn} \nDate de Decès : {Dtd}";
        }
        #endregion
    }
}
