namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Type de logiciel
    /// </summary>
    public interface IObjTypeLogiciel
    {
        /// <summary>
        /// Commentaires
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Icone
        /// </summary>
        string Icone { get; set; }

        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        string Nom { get; set; }
    }
}