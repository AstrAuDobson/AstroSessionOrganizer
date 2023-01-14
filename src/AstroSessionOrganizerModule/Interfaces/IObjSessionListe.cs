using System.Collections.Generic;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Liste des Sessions d'observations
    /// </summary>
    public interface IObjSessionListe
    {
        /// <summary>
        /// Liste des Sessions
        /// </summary>
        List<IObjSession> Liste { get; }

        /// <summary>
        /// Nombre total de sessions
        /// </summary>
        int NombreTotalSessions { get; }

        /// <summary>
        /// Constellation sélectionnée dans la TreeView
        /// </summary>
        string SelectedConstellation { get; set; }

        /// <summary>
        /// Objet Céleste sélectionné dans la TreeView
        /// </summary>
        string SelectedTypeObjet { get; set; }

        /// <summary>
        /// Date sélectionnée dans la TreeView
        /// </summary>
        string SelectedDate { get; set; }

        /// <summary>
        /// Liste des Sessions complète non filtrée
        /// </summary>
        List<IObjSession> ListeComplete { get; }

        /// <summary>
        /// Renvoi <see cref="IObjSession"/> correspondant à l'idSession
        /// </summary>
        /// <param name="idSession"></param>
        /// <returns></returns>
        IObjSession GetSession(string idSession);
    }
}