using System.IO;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Site d'observation
    /// </summary>
    internal class ObjSite : IObjSite
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
        public double? Longitude { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinates Coordonnee
        {
            get
            {
                if (Longitude.HasValue && Latitude.HasValue)
                {
                    return appToolFactory.GetCoordinates(Latitude.Value, Longitude.Value);
                }
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FormatedThumbnailPathName
        {
            get
            {
                return Path.Combine(appToolFactory.GetAppContext().UserProfilePath, "AstrAuDobson", "AstroSessionOrganizer", "data", $"site_{Id}.jpg");
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ThumbnailPosition { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ThumbnailBortle { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? IndiceBortle { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjSite(IAppToolFactory appToolFactory)
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
