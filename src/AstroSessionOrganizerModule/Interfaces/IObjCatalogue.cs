namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Catalogue d'objet céleste
    /// </summary>
    public interface IObjCatalogue
    {
        /// <summary>
        /// Identifiant du catalogue
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Code du catalogue
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// Nom du catalogue
        /// </summary>
        string Nom { get; set; }
    }
}