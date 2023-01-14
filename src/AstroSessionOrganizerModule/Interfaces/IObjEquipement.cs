namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Equipement
    /// </summary>
    public interface IObjEquipement
    {
        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Identifiant du Type d'equipement
        /// </summary>
        string IdTypeEquipement { get; set; }

        /// <summary>
        /// Objet Type d'equipement
        /// </summary>
        IObjTypeEquipement TypeEquipement { get; }

        /// <summary>
        /// Nom
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// Thumbnail
        /// </summary>
        string Thumbnail { get; set; }

        /// <summary>
        /// Commentaires
        /// </summary>
        string Comment { get; set; }
    }
}