using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Constellation
    /// </summary>
    public interface IObjConstellation
    {
        /// <summary>
        /// Id
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// NomLatin
        /// </summary>
        string NomLatin { get; set; }

        /// <summary>
        /// Abr
        /// </summary>
        string Abr { get; set; }

        /// <summary>
        /// RA
        /// </summary>
        Coordinate RA { get; set; }

        /// <summary>
        /// DEC
        /// </summary>
        Coordinate DEC { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Etendue_Deg2
        /// </summary>
        double? Etendue_Deg2 { get; set; }

        /// <summary>
        /// Etendue_Pct
        /// </summary>
        double? Etendue_Pct { get; set; }

        /// <summary>
        /// ThumbnailPosition
        /// </summary>
        string ThumbnailPosition { get; set; }

        /// <summary>
        /// URLWiki
        /// </summary>
        string URLWiki { get; set; }

        /// <summary>
        /// URL complète de l'image de la position de la constellation
        /// </summary>
        string DisplayThumbnailPosition { get; }
    }
}