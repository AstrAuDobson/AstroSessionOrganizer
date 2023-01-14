using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Type d'observation
    /// </summary>
    internal class ObjTypeObservation : IObjTypeObservation
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

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjTypeObservation(IAppToolFactory appToolFactory)
        {
            this.appToolFactory = appToolFactory;
            // Positionnement des valeurs par défaut
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        #endregion
    }
}
