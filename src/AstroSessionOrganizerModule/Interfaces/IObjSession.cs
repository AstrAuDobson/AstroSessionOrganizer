using System;
using System.Collections.Generic;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface Session d'observations
    /// </summary>
    public interface IObjSession
    {
        /// <summary>
        /// Commentaires
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Les commentaires apparaissent dans les EXIF
        /// </summary>
        bool CommentDansExif { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        DateTime DateHeure { get; }

        /// <summary>
        /// Date / heure au format LTNV
        /// </summary>
        string DateLtnv { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Identifiant
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Identifiant de l'objet Celeste
        /// </summary>
        string IdObjetCeleste { get; set; }

        /// <summary>
        /// Identifiant du Setup
        /// </summary>
        string IdSetup { get; set; }

        /// <summary>
        /// Path contenant les images
        /// </summary>
        string ImagesPath { get; set; }

        /// <summary>
        /// Liste des <see cref="IObjObservation"/> de la Session
        /// </summary>
        List<IObjObservation> ListeObservationsSession { get; }

        /// <summary>
        /// Objet Objet Céleste
        /// </summary>
        IObjObjetCeleste ObjetCeleste { get; }

        /// <summary>
        /// Rank de la Session
        /// </summary>
        int? Rank { get; set; }

        /// <summary>
        /// Objet Setup
        /// </summary>
        IObjSetup Setup { get; }

        /// <summary>
        /// Thumbnail surchargée
        /// </summary>
        string Thumbnail { get; set; }

        /// <summary>
        /// Index de l'icone dans la liste
        /// </summary>
        int IconeIndex { get; }

        /// <summary>
        /// Identifiant du site d'observations
        /// </summary>
        string IdSite { get; set; }

        /// <summary>
        /// Objet Site
        /// </summary>
        IObjSite Site { get; }

        /// <summary>
        /// Liste des équipements de la Session
        /// </summary>
        List<IObjEquipementSession> ListeEquipementsSession { get; }

        /// <summary>
        /// Image de la session
        /// <para>Si Thumbnail n'est pas spécifié, on renvoi la première image se trouvant dans le répertoire ImagesPath</para>
        /// </summary>
        string DisplayThumbnail { get; }

        /// <summary>
        /// Renvoi le temps total d'expositions calculé pour la session
        /// </summary>
        string FormatedTempsTotalObservations { get; }

        /// <summary>
        /// Liste des logiciels de la session
        /// </summary>
        List<IObjLogicielSession> ListeLogicielsSession { get; }

        /// <summary>
        /// Renvoi la liste des logiciels de la session sous la forrme d'une chaîne formatée
        /// </summary>
        string FullLogicielsListe { get; }

        /// <summary>
        /// Renvoi le nom du site d'observations
        /// <para>string.empty si Site == null</para>
        /// </summary>
        string NomSite { get; }

        /// <summary>
        /// Renvoi le nom du Setup
        /// <para>string.empty si Setup == null</para>
        /// </summary>
        string NomSetup { get; }

        /// <summary>
        /// Thumbnail redimensionnée
        /// <para>Si la thumbnail n'existe pas, elle est crée</para>
        /// </summary>
        string ResizedDisplayThumbnail { get; }

        /// <summary>
        /// Création d'un équipement en BDD pour la session
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="idEquipement"></param>
        void CreateEquipementSession(string nom, string idEquipement);

        /// <summary>
        /// Création d'un LogicielSession pour la Session
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="idLogiciel"></param>
        void CreateLogicielSession(string nom, string idLogiciel);

        /// <summary>
        /// Création d'une observation en BDD pour la session
        /// </summary>
        /// <param name="observation"></param>
        void CreateObservationSession(IObjObservation observation);

        /// <summary>
        /// Suppression d'un équipement en BDD
        /// </summary>
        void DeleteEquipements();

        /// <summary>
        /// Suppresion des logiciels de la session
        /// </summary>
        void DeleteLogiciels();

        /// <summary>
        /// Suppression des observations d'une session
        /// </summary>
        void DeleteObservations();
    }
}