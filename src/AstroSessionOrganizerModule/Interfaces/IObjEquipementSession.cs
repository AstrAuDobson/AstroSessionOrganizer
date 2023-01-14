namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Equipement d'une Session
    /// </summary>
    public interface IObjEquipementSession
    {
        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Identifiant de l'équipement
        /// </summary>
        string IdEquipement { get; set; }

        /// <summary>
        /// Identifiant de la Session
        /// </summary>
        string IdSession { get; set; }

        /// <summary>
        /// Nom surchargé
        /// </summary>
        string Nom { get; set; }
    }
}