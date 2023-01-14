namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Logiciel
    /// </summary>
    public interface IObjLogiciel
    {
        /// <summary>
        /// Commentaires
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Identifiant du type de logiciel
        /// </summary>
        string IdTypeLogiciel { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// Thumbnail
        /// </summary>
        string Thumbnail { get; set; }

        /// <summary>
        /// Objet Type Logiciel
        /// </summary>
        IObjTypeLogiciel TypeLogiciel { get; }
    }
}