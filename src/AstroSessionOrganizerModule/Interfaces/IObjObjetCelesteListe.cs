using System.Collections.Generic;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Liste des Objets céleste
    /// </summary>
    public interface IObjObjetCelesteListe
    {
        /// <summary>
        /// Liste des objets célestes
        /// </summary>
        List<IObjObjetCeleste> Liste { get; }

        /// <summary>
        /// Constellation sélectionnée dans la TreeView
        /// </summary>
        string SelectedConstellation { get; set; }

        /// <summary>
        /// TypeObjet sélectionné dans la TreeView
        /// </summary>
        string SelectedTypeObjet { get; set; }

        /// <summary>
        /// TypeObjet sélectionné dans la TreeView
        /// </summary>
        string FiltreNomDescription { get; set; }

        /// <summary>
        /// Filtre sur Type
        /// </summary>
        string FiltreIdType { get; set; }

        /// <summary>
        /// Filtre sur Magnitude
        /// </summary>
        string FiltreMagnitude { get; set; }

        /// <summary>
        /// Filtre sur HasSession
        /// </summary>
        bool FiltreHasSession { get; set; }

        /// <summary>
        /// Nombre d'objets célestes répertoriés au total
        /// </summary>
        int NombreObjetsRepertories { get; }

        /// <summary>
        /// Filtre sur l'identifiant du Catalogue
        /// </summary>
        string FiltreIdCatalogue { get; set; }

        /// <summary>
        /// Liste complète des objets célestes
        /// </summary>
        List<IObjObjetCeleste> ListeComplete { get; }

        /// <summary>
        /// Renvoi l'objet correspondant au nom passé en paramètre
        /// </summary>
        /// <param name="nomObjet"></param>
        /// <returns></returns>
        IObjObjetCeleste GetObjetCeleste(string nomObjet);
    }
}