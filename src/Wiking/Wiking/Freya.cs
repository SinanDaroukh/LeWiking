using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wiking
{
    public abstract class Freya : EntityBase
    {
        #region Attributs
        private string _name;
        private string _image;
        private string _text1;
        private string _text2;
        private string _path;
        #endregion

        #region Propriétés
        // Clé primaire de la table qui sera hérité par les Types Concretes
        [Key]
        // Ligne permettant la génération de la clé primaire lors de l'insertion en base de données
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        /// <summary>
        /// Identifiant unique
        /// </summary>
        public Guid UniqueId
        {
            get; set;
        }

        [Column("Nom")]
        /// <summary>
        /// Nom de l'objet
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        /// <summary>
        /// Nom de l'image suivi de son extension
        /// </summary>
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

        [Column("Court descriptif")]
        /// <summary>
        /// Court descriptif
        /// </summary>
        public string Text1
        {
            get
            {
                return _text1;
            }

            set
            {
                _text1 = value;
                OnPropertyChanged(nameof(Text1));
            }
        }

        [Column("Descriptif exhaustif")]
        /// <summary>
        /// Descriptif exhaustif
        /// </summary>
        public string Text2
        {
            get
            {
                return _text2;
            }

            set
            {
                _text2 = value;
                OnPropertyChanged(nameof(Text2));
            }
        }

        /// <summary>
        /// Chemin vers l'image composé du chemin jusqu'au dossier image suivi du nom de l'image (qui comprend l'extension)
        /// </summary>
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                OnPropertyChanged(nameof(Path));
            }
        }
        #endregion

        #region Constructeurs

        // Constructeur vide néccésaire pour éviter des erreurs lors de l'initialisation de la base de données
        public Freya() { }

        // Constructeur de la classe abstraite Freya
        public Freya(string name, string image, string text1, string text2)
        {
            Name = name;
            Image = image;
            Text1 = text1;
            Text2 = text2;
            Path = $"pack://application:,,,/Wiking.Presentation;component/Images/{image}";

        }
        #endregion

        #region Méthodes à redéfinir
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

            return this.Equals(right as Freya);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + 17 * + Text1.GetHashCode() + 17 * Text2.GetHashCode();
        }

        public bool Equals(Freya other)
        {
            return (this.Name.Equals(other.Name));
        }

        public override string ToString()
        {
            return $"\nIdentifiant : {UniqueId} \nPath : {Path} \nNom : {Name} \nImage :{Image} \nTexte Bref : {Text1} \nTexte Complet : {Text2}\n";
        }   
        #endregion
    }
}