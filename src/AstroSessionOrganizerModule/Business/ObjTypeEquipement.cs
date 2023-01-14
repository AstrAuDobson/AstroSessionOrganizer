using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Type d'équipement
    /// </summary>
    internal class ObjTypeEquipement : IObjTypeEquipement
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Icone { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Comment { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTypeEquipement(IAppToolFactory appToolFactory)
        {
            this.appToolFactory = appToolFactory;

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

        #endregion
    }
}
