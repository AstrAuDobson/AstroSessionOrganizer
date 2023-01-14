namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Type d'équipement
    /// </summary>
    public interface IObjTypeEquipement
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
        /// Icone
        /// </summary>
        string Icone { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        string Comment { get; set; }
    }
}