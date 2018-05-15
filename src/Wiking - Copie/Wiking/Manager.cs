using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static Wiking.VikingDBEntities;

namespace Wiking
{
    public class Manager : IDataManager
    {
        #region Attributs
        /// <summary>
        /// Initialisation des données du master detail
        /// </summary>
        private IEnumerable<Viking> _lesVikings = GetAllVikings();
        #endregion

        #region Propriétés
        /// <summary>
        /// Liste de viking
        /// </summary>
        public IEnumerable<Viking> LesVikings
        {
            get
            {
                return _lesVikings;
            }
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Ajout d'un viking au stub
        /// </summary>
        /// <param name="viking"></param>
        public Viking AddV(Viking viking)
        {
            Debug.WriteLine($"Le viking {viking.Name} a été ajouté");
            viking.Identite = $"Nom : {viking.Name} \nSexe : {viking.Sexe} \nMere : {viking.Mere} \nPere : {viking.Pere} \nDate de Naissance : {viking.Dtn.ToShortDateString()} \nDate de décès : {viking.Dtd.ToShortDateString()}";
            viking.Path = $"pack://application:,,,/Wiking.Presentation;component/Images/{viking.Image}";
            AddDB(viking);
            return viking;
        }

        /// <summary>
        /// Suppression d'un viking du stub
        /// </summary>
        /// <param name="viking"></param>
        public Viking RemoveV(Viking viking)
        {
            Debug.WriteLine($"Le viking {viking.Name} a été enlevé");
            RemoveDB(viking);
            return viking;
        }

        /// <summary>
        /// Modification d"un viking du stub
        /// </summary>
        /// <param name="viking"></param>
        public void UpdateV(Viking viking1, Viking viking2, ObservableCollection<Viking> LesVikings)
        {
            Debug.WriteLine($"Le viking {viking1.Name} a été mis à jour");
            LesVikings.Remove(RemoveV(viking1));
            viking2.Path = viking2.Image;
            viking2.Identite = $"Nom : {viking2.Name} \nSexe : {viking2.Sexe} \nMere : {viking2.Mere} \nPere : {viking2.Pere} \nDate de Naissance : {viking2.Dtn.ToShortDateString()} \nDate de décès : {viking2.Dtd.ToShortDateString()}";
            LesVikings.Add(AddV(viking2));
        }
        #endregion
    }
}



