using System;
using System.Collections.Generic;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface de l'Objet représentant une base de données SQLite
    /// </summary>
    public interface ISQLiteDatabase
    {
        /// <summary>
        /// Chemin complet de la BDD
        /// </summary>
        string DatabaseName { get; }

        /// <summary>
        /// Version de la BDD
        /// </summary>
        string DatabaseVersion { get; set; }

        /// <summary>
        /// Renvoi la liste des catalogue d'objets célestes
        /// </summary>
        /// <returns></returns>
        List<IObjCatalogue> GetListeCatalogues();

        /// <summary>
        /// Renvoi la liste des objets célestes
        /// </summary>
        /// <param name="listeTypeObjet"></param>
        /// <param name="listeConstellation"></param>
        /// <param name="listeCatalogue"></param>
        /// <returns></returns>
        List<IObjObjetCeleste> GetListeObjetCeleste(List<IObjTypeObjet> listeTypeObjet, List<IObjConstellation> listeConstellation, List<IObjCatalogue> listeCatalogue);

        /// <summary>
        /// Renvoi la liste des type d'objets célestes
        /// </summary>
        List<IObjTypeObjet> GetListeTypeObjets();

        /// <summary>
        /// Renvoi la liste des Constellations
        /// </summary>
        List<IObjConstellation> GetListeConstellations();

        /// <summary>
        /// Renvoi la liste des type d'équipements
        /// </summary>
        List<IObjTypeEquipement> GetListeTypeEquipements();

        /// <summary>
        /// Renvoi la liste des équipements
        /// </summary>
        /// <param name="listeTypeEquipements"></param>
        /// <returns></returns>
        List<IObjEquipement> GetListeEquipements(List<IObjTypeEquipement> listeTypeEquipements);

        /// <summary>
        /// Renvoi la liste des Setup
        /// </summary>
        /// <param name="listeEquipements"></param>
        /// <param name="listeCompleteEquipementSetup"></param>
        /// <returns></returns>
        List<IObjSetup> GetListeSetup(List<IObjEquipement> listeEquipements, List<IObjEquipementSetup> listeCompleteEquipementSetup);

        /// <summary>
        /// Update d'un équipement
        /// </summary>
        void UpdateEquipement(IObjEquipement equipement);
        
        /// <summary>
        /// Création d'un équipement
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="idEquipement"></param>
        /// <param name="thumbnail"></param>
        /// <param name="comment"></param>
        void CreateEquipement(string nom, string idEquipement, string thumbnail, string comment);

        /// <summary>
        /// Suppression d'un équipement
        /// </summary>
        /// <param name="equipement"></param>
        void DeleteEquipement(IObjEquipement equipement);

        /// <summary>
        /// Renvoi la liste des équipements d'un Setup
        /// </summary>
        /// <param name="idSetup"></param>
        /// <param name="listeEquipements"></param>
        /// <returns></returns>
        List<IObjEquipementSetup> GetListeEquipementsSetup(string idSetup, List<IObjEquipement> listeEquipements);

        /// <summary>
        /// Création d'un Setup
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="thumbnail"></param>
        /// <param name="comment"></param>
        /// <param name="listeEquipements"></param>
        /// <param name="listeCompleteEquipementSetup"></param>
        IObjSetup CreateSetup(string nom, string thumbnail, string comment, List<IObjEquipement> listeEquipements, List<IObjEquipementSetup> listeCompleteEquipementSetup);

        /// <summary>
        /// Update d'un Setup
        /// </summary>
        void UpdateSetup(IObjSetup setup);

        /// <summary>
        /// Suppression des équipements d'un setup
        /// </summary>
        /// <param name="setup"></param>
        void DeleteEquipementsSetup(IObjSetup setup);

        /// <summary>
        /// Création d'un Equipement d'un Setup
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="idEquipement"></param>
        /// <param name="idSetup"></param>
        void CreateEquipementSetup(string nom, string idEquipement, string idSetup);

        /// <summary>
        /// Suppression d'un setup
        /// </summary>
        /// <param name="setup"></param>
        void DeleteSetup(IObjSetup setup);

        /// <summary>
        /// Renvoi la liste des Sites d'observation
        /// </summary>
        List<IObjSite> GetListeSites();

        /// <summary>
        /// Création d'un site d'observation
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="coordonnee"></param>
        /// <param name="thumbnail"></param>
        /// <param name="thumbnailPosition"></param>
        /// <param name="thumbnailBortle"></param>
        /// <param name="comment"></param>
        /// <param name="indiceBortle"></param>
        /// <returns></returns>
        IObjSite CreateSite(string nom, Coordinates coordonnee, string thumbnail, string thumbnailPosition, string thumbnailBortle, string comment, double? indiceBortle);

        /// <summary>
        /// Update d'un site d'observation
        /// </summary>
        /// <param name="site"></param>
        void UpdateSite(IObjSite site);

        /// <summary>
        /// Suppression d'un site d'observations
        /// </summary>
        /// <param name="site"></param>
        void DeleteSite(IObjSite site);

        /// <summary>
        /// Update d'un objet céleste
        /// </summary>
        /// <param name="objetCeleste"></param>
        void UpdateObjetCeleste(IObjObjetCeleste objetCeleste);

        /// <summary>
        /// Création d'un nouvel Objet céleste
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="description"></param>
        /// <param name="denominations"></param>
        /// <param name="idType"></param>
        /// <param name="idConstellation"></param>
        /// <param name="ra"></param>
        /// <param name="dec"></param>
        /// <param name="size_max"></param>
        /// <param name="size_min"></param>
        /// <param name="mag_visual"></param>
        /// <param name="mag_photo"></param>
        /// <param name="redshift"></param>
        /// <param name="distance_rs"></param>
        /// <param name="distance_m"></param>
        /// <param name="catalogue"></param>
        /// <param name="thumbnail"></param>
        /// <param name="thumbnailPosition"></param>
        /// <param name="urlWiki"></param>
        /// <param name="origin"></param>
        /// <param name="comment"></param>
        void CreateObjetCeleste(string nom, string description, string denominations, string idType, string idConstellation, Coordinate ra, Coordinate dec, double? size_max, double? size_min, double? mag_visual, double? mag_photo, double? redshift, double? distance_rs, double? distance_m, string catalogue, string thumbnail, string thumbnailPosition, string urlWiki, string origin, string comment);
        
        /// <summary>
        /// Suppression d'un Objet céleste
        /// </summary>
        /// <param name="objetCeleste"></param>
        void DeleteObjetCeleste(IObjObjetCeleste objetCeleste);

        /// <summary>
        /// Renvoi la liste des types d'observation
        /// </summary>
        /// <returns></returns>
        List<IObjTypeObservation> GetListeTypeObservation();

        /// <summary>
        /// Chargement de la liste complète des sessions d'observations
        /// </summary>
        /// <param name="listeObjetCeleste"></param>
        /// <param name="listeSetup"></param>
        /// <param name="listeTypeObservation"></param>
        /// <param name="listeEquipements"></param>
        /// <param name="listeSite"></param>
        /// <param name="listeCompleteObservationsSession"></param>
        /// <param name="listeCompleteEquipementsSession"></param>
        /// <param name="listeCompleteLogicielSession"></param>
        /// <param name="listeLogiciels"></param>
        /// <returns></returns>
        List<IObjSession> GetListeSession(List<IObjObjetCeleste> listeObjetCeleste,
                                            List<IObjSetup> listeSetup,
                                            List<IObjTypeObservation> listeTypeObservation,
                                            List<IObjEquipement> listeEquipements,
                                            List<IObjSite> listeSite,
                                            List<IObjObservation> listeCompleteObservationsSession,
                                            List<IObjEquipementSession> listeCompleteEquipementsSession,
                                            List<IObjLogicielSession> listeCompleteLogicielSession,
                                            List<IObjLogiciel> listeLogiciels);
        
        /// <summary>
        /// Création d'une session en BDD
        /// </summary>
        /// <param name="session"></param>
        void CreateSession(ref IObjSession session);

        /// <summary>
        /// Update d'une session en BDD
        /// </summary>
        /// <param name="session"></param>
        void UpdateSession(IObjSession session);

        /// <summary>
        /// Renvoi la liste des équipements de la session correspondante
        /// </summary>
        /// <param name="idSession"></param>
        /// <param name="listeEquipements"></param>
        /// <returns></returns>
        List<IObjEquipementSession> GetListeEquipementsSession(string idSession, List<IObjEquipement> listeEquipements);

        /// <summary>
        /// Suppression en BDD des équipements de la session correspondante
        /// </summary>
        /// <param name="session"></param>
        void DeleteEquipementsSession(IObjSession session);

        /// <summary>
        /// Création d'un équipement pour la session
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="idEquipement"></param>
        /// <param name="idSession"></param>
        void CreateEquipementSession(string nom, string idEquipement, string idSession);

        /// <summary>
        /// Supprime en BDD toutes les observations d'une sessions
        /// </summary>
        /// <param name="session"></param>
        void DeleteObservationsSession(IObjSession session);

        /// <summary>
        /// Lecture en BDD de la liste des observations d'une session
        /// </summary>
        /// <param name="idSession"></param>
        /// <param name="listeTypeObservation"></param>
        /// <param name="listeEquipements"></param>
        /// <returns></returns>
        List<IObjObservation> GetListeObservationsSession(string idSession, List<IObjTypeObservation> listeTypeObservation, List<IObjEquipement> listeEquipements);
        
        /// <summary>
        /// Création d'une observation en BDD
        /// </summary>
        /// <param name="observation"></param>
        void CreateObservation(IObjObservation observation);

        /// <summary>
        /// Charge depuis la BDD la liste des types de logiciels
        /// </summary>
        /// <returns></returns>
        List<IObjTypeLogiciel> GetListeTypeLogiciels();

        /// <summary>
        /// Charge depuis la BDD la liste complète des logiciels
        /// </summary>
        /// <param name="listeTypeLogiciels"></param>
        /// <returns></returns>
        List<IObjLogiciel> GetListeLogiciels(List<IObjTypeLogiciel> listeTypeLogiciels);

        /// <summary>
        /// Création d'un nouveau logiciel en BDD
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="idTypeLogiciel"></param>
        /// <param name="thumbnail"></param>
        /// <param name="comment"></param>
        void CreateLogiciel(string nom, string idTypeLogiciel, string thumbnail, string comment);

        /// <summary>
        /// Update d'un logiciel en BDD
        /// </summary>
        /// <param name="logiciel"></param>
        void UpdateLogiciel(IObjLogiciel logiciel);

        /// <summary>
        /// Suppression d'un logiciel en BDD
        /// </summary>
        /// <param name="logiciel"></param>
        void DeleteLogiciel(IObjLogiciel logiciel);

        /// <summary>
        /// Charge depuis la BDD la liste des logiciels d'une session
        /// </summary>
        /// <param name="idSession"></param>
        /// <returns></returns>
        List<IObjLogicielSession> GetListeLogicielsSession(string idSession);

        /// <summary>
        /// Suppression en BDD des logicielsSession d'une Session
        /// </summary>
        /// <param name="session"></param>
        void DeleteLogicielsSession(IObjSession session);

        /// <summary>
        /// Création d'un LogicielSession pour la Session
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="idLogiciel"></param>
        /// <param name="idSession"></param>
        void CreateLogicielSession(string nom, string idLogiciel, string idSession);

        /// <summary>
        /// Charge la liste complète de tous les logiciels session
        /// </summary>
        /// <returns></returns>
        List<IObjLogicielSession> GetListeAllLogicielsSession();

        /// <summary>
        /// Charge la liste complète des observations
        /// </summary>
        /// <param name="listeTypeObservation"></param>
        /// <param name="listeEquipements"></param>
        /// <returns></returns>
        List<IObjObservation> GetListeAllObservationsSession(List<IObjTypeObservation> listeTypeObservation, List<IObjEquipement> listeEquipements);

        /// <summary>
        /// Charge la liste complète des <see cref="ObjEquipementSession"/>
        /// </summary>
        /// <param name="listeEquipements"></param>
        /// <returns></returns>
        List<IObjEquipementSession> GetListeAllEquipementsSession(List<IObjEquipement> listeEquipements);

        /// <summary>
        /// Charge la liste complète des <see cref="ObjEquipementSetup"/>
        /// </summary>
        /// <param name="listeEquipements"></param>
        /// <returns></returns>
        List<IObjEquipementSetup> GetListeAllEquipementsSetup(List<IObjEquipement> listeEquipements);

        /// <summary>
        /// Suppression d'une session en BDD
        /// </summary>
        /// <param name="session"></param>
        void DeleteSession(IObjSession session);

        /// <summary>
        /// Création IObjEquipementSession par lot
        /// </summary>
        /// <param name="listeNouveauEquipementSession"></param>
        void CreateEquipementsSession(List<IObjEquipementSession> listeNouveauEquipementSession);

        /// <summary>
        /// Création IObjObservation par lot
        /// </summary>
        /// <param name="listeObservations"></param>
        void CreateObservations(List<IObjObservation> listeObservations);

        /// <summary>
        /// Création IObjLogicielSession par lot
        /// </summary>
        /// <param name="listeLogicielsSesssion"></param>
        void CreateLogicielsSession(List<IObjLogicielSession> listeLogicielsSesssion);

        /// <summary>
        /// Lecture de la version de la BDD
        /// </summary>
        /// <returns></returns>
        Version GetBDDVersion();

        /// <summary>
        /// Update de la versionde la BDD
        /// </summary>
        /// <param name="version"></param>
        /// <param name="nom"></param>
        /// <param name="comment"></param>
        void UpdateBDDVersion(string version, string nom, string comment);
    }
}