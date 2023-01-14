using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Site d'observation
    /// </summary>
    public interface IObjSite
    {
        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        double? Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        double? Longitude { get; set; }

        /// <summary>
        /// Coordonnées
        /// </summary>
        Coordinates Coordonnee { get; }

        /// <summary>
        /// Miniature
        /// </summary>
        string Thumbnail { get; set; }

        /// <summary>
        /// Miniature de l'image de Bortle
        /// </summary>
        string ThumbnailBortle { get; set; }

        /// <summary>
        /// Miniature de l'image de position
        /// </summary>
        string ThumbnailPosition { get; set; }

        /// <summary>
        /// Commentaires
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// URL standard de l'image du site
        /// </summary>
        string FormatedThumbnailPathName { get; }

        /// <summary>
        /// Indice de Bortle du Site
        /// </summary>
        double? IndiceBortle { get; set; }
    }
}