using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Equipement d'une Session
    /// </summary>
    internal class ObjEquipementSession : IObjEquipementSession
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
        public string IdEquipement { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdSession { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjEquipementSession(IAppToolFactory appToolFactory)
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
