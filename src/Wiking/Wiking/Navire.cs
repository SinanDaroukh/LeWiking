using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiking
{
    public class Navire : Freya
    {
        #region Attributs
        private int _dimLong;
        private int _dimHaut;
        private int _dimLarg;
        #endregion

        #region Propriétés
        public int DimLong
        {
            get
            {
                return _dimLong;
            }

            private set
            {
                _dimLong = value;
            }
        }
        public int DimHaut
        {
            get
            {
                return _dimHaut;
            }

            private set
            {
                _dimHaut = value;
            }
        }
        public int DimLarg
        {
            get
            {
                return _dimLarg;
            }

            private set
            {
                _dimLarg = value;
            }
        }
        #endregion

        #region Constructeurs
        public Navire() { }
        public Navire(string name, string image, string text1, string text2, int dimLong, int dimHaut, int dimLarg ) : base(name, image, text1, text2)
        {
            DimLong = dimLong;
            DimHaut = dimHaut;
            DimLarg = dimLarg;
        }
        #endregion

        #region Méthodes à redéfinir
        public override string ToString()
        {
            return base.ToString() + $"Dimension Longueur: {DimLong}\nDimension Largeur: {DimLarg}\nDimension Hauteur: {DimHaut}\n";
        }
        #endregion
    }
}
