using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Logiciel d'une Session
    /// </summary>
    internal class ObjLogicielSession : IObjLogicielSession
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
        public string IdLogiciel { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdSession { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjLogicielSession(IAppToolFactory appToolFactory)
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
