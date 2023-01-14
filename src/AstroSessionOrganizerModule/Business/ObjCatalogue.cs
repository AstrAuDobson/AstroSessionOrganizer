using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Catalogue d'objet céleste
    /// </summary>
    internal class ObjCatalogue : IObjCatalogue
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

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjCatalogue(IAppToolFactory appToolFactory)
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
