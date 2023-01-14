using System.Collections.Generic;
using System.Linq;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Equipement
    /// </summary>
    internal class ObjEquipement : IObjEquipement
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdTypeEquipement { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjTypeEquipement TypeEquipement
        {
            get
            {
                return listeTypeEquipements.Where(t => t.Id == IdTypeEquipement).FirstOrDefault();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Comment { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjEquipement(IAppToolFactory appToolFactory, List<IObjTypeEquipement> listeTypeEquipements)
        {
            this.appToolFactory = appToolFactory;
            this.listeTypeEquipements = listeTypeEquipements;

            // Positionnement des valeurs par défaut
        }

        #endregion

        #region Méthodes
        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Liste des Type Equipement
        /// </summary>
        private List<IObjTypeEquipement> listeTypeEquipements = null;

        #endregion
    }
}
