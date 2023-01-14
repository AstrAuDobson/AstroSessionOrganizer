using System;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Observation d'une Session d'observations
    /// </summary>
    public interface IObjObservation
    {
        /// <summary>
        /// BINNING
        /// </summary>
        double? BINNING { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        DateTime DateHeure { get; }

        /// <summary>
        /// Date au format LTNV
        /// </summary>
        string DateLtnv { get; set; }

        /// <summary>
        /// Commentaires
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Les commentaires apparaissent dans les EXIF
        /// </summary>
        bool CommentDansExif { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Objet Equipement
        /// </summary>
        IObjEquipement Equipement { get; }

        /// <summary>
        /// Gain
        /// </summary>
        double? GAIN { get; set; }

        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Identifiant de l'équipement (Filtre) associé
        /// </summary>
        string IdEquipement { get; set; }

        /// <summary>
        /// Identifiant de la Session parent
        /// </summary>
        string IdSession { get; set; }

        /// <summary>
        /// Identifiant du type d'observation
        /// </summary>
        string IdTypeObservation { get; set; }

        /// <summary>
        /// Libellé formaté de l'équipement
        /// </summary>
        string LibelleEquipement { get; }

        /// <summary>
        /// Etat Lune. Format custom (% - Text)
        /// </summary>
        string Lune { get; set; }

        /// <summary>
        /// Nombre d'expositions de l'observation
        /// </summary>
        double? NBR_EXPO { get; set; }

        /// <summary>
        /// Etat seeing
        /// </summary>
        string Seeing { get; set; }

        /// <summary>
        /// Température de la caméra
        /// </summary>
        double? TEMP { get; set; }

        /// <summary>
        /// Temps d'exposition (s)
        /// </summary>
        double? TPS_EXPO { get; set; }

        /// <summary>
        /// Objet Type d'observation
        /// </summary>
        IObjTypeObservation TypeObservation { get; }

        /// <summary>
        /// Renvoi le temps total d'expositions calculé pour l'observation
        /// </summary>
        TimeSpan TempsTotalExposition { get; }
    }
}