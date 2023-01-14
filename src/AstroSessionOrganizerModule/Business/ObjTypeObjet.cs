using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Type d'objet céleste
    /// </summary>
    internal class ObjTypeObjet : IObjTypeObjet
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
        public string Code { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Icone { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTypeObjet(IAppToolFactory appToolFactory)
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
