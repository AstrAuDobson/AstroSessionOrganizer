using System;
using System.IO;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Constellation
    /// </summary>
    internal class ObjConstellation : IObjConstellation
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
        public string NomLatin { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Abr { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ThumbnailPosition { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string DisplayThumbnailPosition
        {
            get
            {
                try
                {
                    string urlImage = string.Empty;
                    if (!string.IsNullOrEmpty(ThumbnailPosition))
                        urlImage = Path.Combine(appToolFactory.GetAppContext().UserProfilePath, "AstrAuDobson", "AstroSessionOrganizer", "data", ThumbnailPosition);
                    return urlImage;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string URLWiki { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate RA { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate DEC { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? Etendue_Deg2 { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? Etendue_Pct { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjConstellation(IAppToolFactory appToolFactory)
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
