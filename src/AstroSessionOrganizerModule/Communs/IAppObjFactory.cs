using System;
using System.Collections.Generic;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Interface de la Fabrique d'objets Business (métier) et Logic (applicatif)
    /// </summary>
    public interface IAppObjFactory : IAppToolFactory
    {
        ///// <summary>
        ///// Renvoi l'objet <see cref="ISQLiteDatabase"/>
        ///// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        ///// </summary>
        //ISQLiteDatabase GetISQLiteDatabase();

        /// <summary>
        /// Renvoi la liste des catalogues d'objets célestes
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        List<IObjCatalogue> GetListeCatalogues();

        /// <summary>
        /// Renvoi la liste des Types d'objets célestes
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        List<IObjTypeObjet> GetListeTypeObjets();

        /// <summary>
        /// Renvoi la liste des Constellations
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        List<IObjConstellation> GetListeConstellations();

        /// <summary>
        /// Renvoi une nouvelle instance de <see cref="IObjObjetCeleste"/>
        /// </summary>
        /// <returns></returns>
        IObjObjetCeleste GetNewObjetCeleste();

        /// <summary>
        /// Renvoi la liste des Objets céleste
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        IObjObjetCelesteListe GetListeObjetCeleste();

        /// <summary>
        /// Renvoi la liste des Filtres de Magnitude pour l'affichage dans la liste
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        Dictionary<string, string> GetListeFiltreMagnitude();

        /// <summary>
        /// Renvoi la liste des Types d'équipements
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        List<IObjTypeEquipement> GetListeTypeEquipements();

        /// <summary>
        /// Renvoi la liste des équipements
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        List<IObjEquipement> GetListeEquipements();

        /// <summary>
        /// Update d'un équipement
        /// </summary>
        void UpdateEquipement(IObjEquipement equipement);

        /// <summary>
        /// Renvoi la liste des Setup
        /// <para>L'objet est renvoyé sous la forme d'un singleton. S'il n'existe pas il est créé</para>
        /// </summary>
        List<IObjSetup> GetListeSetup();

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
        /// Renvoi un nouvel objet équipement d'un Setup
        /// </summary>
        /// <returns></returns>
        IObjEquipementSetup GetNewEquipementSetup();

        /// <summary>
        /// Création d'un Setup
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="thumbnail"></param>
        /// <param name="comment"></param>
        IObjSetup CreateSetup(string nom, string thumbnail, string comment);

        /// <summary>
        /// Update d'un setup
        /// </summary>
        void UpdateSetup(IObjSetup setup);

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
        /// Création d'un objet céleste
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
        /// Renvoi la liste complète des Sessions
        /// </summary>
        /// <returns></returns>
        IObjSessionListe GetListeSession();

        /// <summary>
        /// Renvoi un nouvel objet Equipement d'une Session
        /// </summary>
        /// <returns></returns>
        IObjEquipementSession GetNewEquipementSession();

        /// <summary>
        /// Renvoi un nouvel objet Observation
        /// </summary>
        /// <returns></returns>
        IObjObservation GetNewObservation();


        /// <summary>
        /// Création d'une session en BDD
        /// </summary>
        /// <param name="idObjetCeleste"></param>
        /// <param name="idSetup"></param>
        /// <param name="idStite"></param>
        /// <param name="dateLtnv"></param>
        /// <param name="description"></param>
        /// <param name="comment"></param>
        /// <param name="commentDansExif"></param>
        /// <param name="thumbnail"></param>
        /// <param name="imagesPath"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        IObjSession CreateSession(string idObjetCeleste, string idSetup, string idStite, string dateLtnv, string description,
                                        string comment, bool commentDansExif, string thumbnail, string imagesPath, int? rank);
        
        /// <summary>
        /// Update d'une session en BDD
        /// </summary>
        /// <param name="session"></param>
        void UpdateSession(IObjSession session);

        /// <summary>
        /// Renvoi la liste des types de logiciel
        /// </summary>
        /// <returns></returns>
        List<IObjTypeLogiciel> GetListeTypeLogiciels();

        /// <summary>
        /// Renvoi la liste complète des logiciels
        /// </summary>
        /// <returns></returns>
        List<IObjLogiciel> GetListeLogiciels();

        /// <summary>
        /// Création d'un nouveau logiciel
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="idTypeLogiciel"></param>
        /// <param name="thumbnail"></param>
        /// <param name="comment"></param>
        void CreateLogiciel(string nom, string idTypeLogiciel, string thumbnail, string comment);

        /// <summary>
        /// Update d'un logiciel
        /// </summary>
        /// <param name="logiciel"></param>
        void UpdateLogiciel(IObjLogiciel logiciel);

        /// <summary>
        /// Suppression d'un logiciel
        /// </summary>
        /// <param name="logiciel"></param>
        void DeleteLogiciel(IObjLogiciel logiciel);

        /// <summary>
        /// Renvoi un nouvel objet de type <see cref="IObjLogicielSession"/>
        /// </summary>
        /// <returns></returns>
        IObjLogicielSession GetNewLogicielSession();

        /// <summary>
        /// Renvoi le nom du mois passé en paramètre
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        string GetMonthName(int month);

        /// <summary>
        /// Renvoi la liste complète des logiciels session
        /// </summary>
        /// <returns></returns>
        List<IObjLogicielSession> GetListeAllLogicielsSession();

        /// <summary>
        /// Renvoi la liste complète des observations session
        /// </summary>
        /// <returns></returns>
        List<IObjObservation> GetListeAllObservationsSession();

        /// <summary>
        /// Renvoi la liste complète des <see cref="ObjEquipementSession"/>
        /// </summary>
        /// <returns></returns>
        List<IObjEquipementSession> GetListeAllEquipementsSession();

        /// <summary>
        /// Renvoi la liste complète des <see cref="ObjEquipementSetup"/>
        /// </summary>
        /// <returns></returns>
        List<IObjEquipementSetup> GetListeAllEquipementsSetup();

        /// <summary>
        /// Suppression d'une session
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
        /// Renvoi le path des data de l'application
        /// </summary>
        /// <returns></returns>
        string GetApplicationDataPath();

        /// <summary>
        /// Renvoi le path du répertoire de sauvegarde de la BDD de l'application
        /// </summary>
        /// <returns></returns>
        string GetApplicationSauvegardeBDDPath();

        /// <summary>
        /// FileName de la base de données en cours
        /// </summary>
        /// <returns></returns>
        string GetBDDFileName();
    }
}