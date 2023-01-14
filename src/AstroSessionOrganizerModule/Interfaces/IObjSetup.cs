using System.Collections.Generic;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Setup
    /// </summary>
    public interface IObjSetup
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
        /// Thumbnail
        /// </summary>
        string Thumbnail { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Liste des équipements du setup
        /// </summary>
        List<IObjEquipementSetup> ListeEquipement { get; }

        /// <summary>
        /// URL standard de l'image du site
        /// </summary>
        string FormatedThumbnailPathName { get; }

        /// <summary>
        /// Suppression des équipements du Setup
        /// </summary>
        void DeleteEquipements();
    }
}