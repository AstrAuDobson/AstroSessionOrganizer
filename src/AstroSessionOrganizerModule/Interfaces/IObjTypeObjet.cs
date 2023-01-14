namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Type d'objet céleste
    /// </summary>
    public interface IObjTypeObjet
    {
        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// Icone
        /// </summary>
        string Icone { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// Thumbnail
        /// </summary>
        string Thumbnail { get; set; }
    }
}