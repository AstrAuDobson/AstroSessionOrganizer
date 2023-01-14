namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Logiciel d'une Session
    /// </summary>
    public interface IObjLogicielSession
    {
        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Identifiant du logiciel
        /// </summary>
        string IdLogiciel { get; set; }

        /// <summary>
        /// Identifiant de la session
        /// </summary>
        string IdSession { get; set; }

        /// <summary>
        /// Nom du logiciel dans la session
        /// </summary>
        string Nom { get; set; }
    }
}