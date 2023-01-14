namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Equipement d'un Setup
    /// </summary>
    public interface IObjEquipementSetup
    {
        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Nom custom de l'équipement dans le setup
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// Identidiant de l'équipement
        /// </summary>
        string IdEquipement { get; set; }

        /// <summary>
        /// Identidiant du Setup
        /// </summary>
        string IdSetup { get; set; }
    }
}