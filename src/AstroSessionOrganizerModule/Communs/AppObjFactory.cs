using System;
using System.Collections.Generic;
using System.IO;
using ApplicationTools;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Fabrique d'objets Business (métier) et Logic (applicatif)
    /// </summary>
    public class AppObjFactory : AppToolFactory, IAppObjFactory
    {
        #region Propriétés
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public AppObjFactory()
        {
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal ISQLiteDatabase GetISQLiteDatabase()
        {
            if (sqliteDatabase == null)
                sqliteDatabase = new SQLiteDatabase(this);
            return sqliteDatabase;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjCatalogue> GetListeCatalogues()
        {
            // Si la liste n'existe pas, on la créer
            if (listeCatalogues == null)
            {
                listeCatalogues = GetISQLiteDatabase().GetListeCatalogues();
            }
            return listeCatalogues;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjTypeObjet> GetListeTypeObjets()
        {
            // Si la liste n'existe pas, on la créer
            if (listeTypeObjets == null)
            {
                listeTypeObjets = GetISQLiteDatabase().GetListeTypeObjets();
            }
            return listeTypeObjets;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjConstellation> GetListeConstellations()
        {
            // Si la liste n'existe pas, on la créer
            if (listeConstellations == null)
            {
                listeConstellations = GetISQLiteDatabase().GetListeConstellations();
            }
            return listeConstellations;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjObjetCelesteListe GetListeObjetCeleste()
        {
            // Si la liste n'existe pas, on la créer
            if (objObjetCelesteListe == null)
            {
                objObjetCelesteListe = new ObjObjetCelesteListe(this, GetISQLiteDatabase().GetListeObjetCeleste(GetListeTypeObjets(), GetListeConstellations(), GetListeCatalogues()));
            }
            return objObjetCelesteListe;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjObjetCeleste GetNewObjetCeleste()
        {
            return new ObjObjetCeleste(this, GetListeTypeObjets(), GetListeConstellations(), GetListeCatalogues());
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateObjetCeleste(IObjObjetCeleste objetCeleste)
        {
            // Update de l'équipement en BDD
            GetISQLiteDatabase().UpdateObjetCeleste(objetCeleste);
            FlushInternalListes();
        }


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateObjetCeleste(string nom, string description, string denominations, string idType, string idConstellation,
                                        Coordinate ra, Coordinate dec, double? size_max, double? size_min, double? mag_visual, double? mag_photo,
                                        double? redshift, double? distance_rs, double? distance_m, string catalogue, string thumbnail,
                                        string thumbnailPosition, string urlWiki, string origin, string comment)
        {
            // Création de l'équipement dans la BDD
            GetISQLiteDatabase().CreateObjetCeleste(nom, description, denominations, idType, idConstellation,
                                                    ra, dec, size_max, size_min, mag_visual, mag_photo,
                                                    redshift, distance_rs, distance_m, catalogue, thumbnail,
                                                    thumbnailPosition, urlWiki, origin, comment);
            // Clear de la liste
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteObjetCeleste(IObjObjetCeleste objetCeleste)
        {
            // Suppression de l'équipement dans la BDD
            GetISQLiteDatabase().DeleteObjetCeleste(objetCeleste);

            // Clear de la liste
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Dictionary<string, string> GetListeFiltreMagnitude()
        {
            // Si la liste n'existe pas, on la créer
            if (listeFiltreMagnitude == null)
            {
                listeFiltreMagnitude = new Dictionary<string, string>();
                listeFiltreMagnitude.Add(Resources.Tous, Resources.Tous);
                listeFiltreMagnitude.Add("7", "7");
                listeFiltreMagnitude.Add("8", "8");
                listeFiltreMagnitude.Add("9", "9");
                listeFiltreMagnitude.Add("10", "10");
                listeFiltreMagnitude.Add("11", "11");
                listeFiltreMagnitude.Add("12", "12");
                listeFiltreMagnitude.Add("13", "13");
                listeFiltreMagnitude.Add("14", "14");
            }
            return listeFiltreMagnitude;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjTypeEquipement> GetListeTypeEquipements()
        {
            // Si la liste n'existe pas, on la créer
            if (listeTypeEquipement == null)
            {
                listeTypeEquipement = GetISQLiteDatabase().GetListeTypeEquipements();
            }
            return listeTypeEquipement;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipement> GetListeEquipements()
        {
            // Si la liste n'existe pas, on la créer
            if (listeEquipement == null)
            {
                listeEquipement = GetISQLiteDatabase().GetListeEquipements(GetListeTypeEquipements());
            }
            return listeEquipement;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateEquipement(IObjEquipement equipement)
        {
            // Update de l'équipement en BDD
            GetISQLiteDatabase().UpdateEquipement(equipement);
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateEquipement(string nom, string idTypeEquipement, string thumbnail, string comment)
        {
            // Création de l'équipement dans la BDD
            GetISQLiteDatabase().CreateEquipement(nom, idTypeEquipement, thumbnail, comment);

            // Clear de la liste des équipements pour forcer le rechargement au prochain appel
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteEquipement(IObjEquipement equipement)
        {
            // Suppression de l'équipement dans la BDD
            GetISQLiteDatabase().DeleteEquipement(equipement);

            // Clear de la liste des équipements pour forcer le rechargement au prochain appel
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjSetup> GetListeSetup()
        {
            // Si la liste n'existe pas, on la créer
            if (listeSetup == null)
            {
                listeSetup = GetISQLiteDatabase().GetListeSetup(GetListeEquipements(), GetListeAllEquipementsSetup());
            }
            return listeSetup;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjEquipementSetup GetNewEquipementSetup()
        {
            return new ObjEquipementSetup(this);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjSetup CreateSetup(string nom, string thumbnail, string comment)
        {
            // Création du Setup dans la BDD
            IObjSetup setup = GetISQLiteDatabase().CreateSetup(nom, thumbnail, comment, GetListeEquipements(), GetListeAllEquipementsSetup());
            FlushInternalListes();
            return setup;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteSetup(IObjSetup setup)
        {
            // Suppression de l'équipement dans la BDD
            GetISQLiteDatabase().DeleteSetup(setup);

            // Clear de la liste des équipements pour forcer le rechargement au prochain appel
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateSetup(IObjSetup setup)
        {
            // Update de l'équipement en BDD
            GetISQLiteDatabase().UpdateSetup(setup);

            // Clear de la liste des équipements pour forcer le rechargement au prochain appel
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateEquipementSetup(string nom, string idEquipement, string idSetup)
        {
            GetISQLiteDatabase().CreateEquipementSetup(nom, idEquipement, idSetup);
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjSite> GetListeSites()
        {
            // Si la liste n'existe pas, on la créer
            if (listeSites == null)
            {
                listeSites = GetISQLiteDatabase().GetListeSites();
            }
            return listeSites;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjSite CreateSite(string nom, Coordinates coordonnee, string thumbnail, string thumbnailPosition, string thumbnailBortle, string comment, double? indiceBortle)
        {
            // Création du Setup dans la BDD
            IObjSite site = GetISQLiteDatabase().CreateSite(nom, coordonnee, thumbnail, thumbnailPosition, thumbnailBortle, comment, indiceBortle);
            FlushInternalListes();
            return site;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateSite(IObjSite site)
        {
            // Update de l'équipement en BDD
            GetISQLiteDatabase().UpdateSite(site);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteSite(IObjSite site)
        {
            // Suppression de l'équipement dans la BDD
            GetISQLiteDatabase().DeleteSite(site);

            // Clear des listes spour forcer le rechargement au prochain appel
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjTypeObservation> GetListeTypeObservation()
        {
            // Si la liste n'existe pas, on la créer
            if (listeTypeObservation == null)
            {
                listeTypeObservation = GetISQLiteDatabase().GetListeTypeObservation();
            }
            return listeTypeObservation;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjSessionListe GetListeSession()
        {
            // Si la liste n'existe pas, on la créer
            if (listeSession == null)
            {
                listeSession = new ObjSessionListe(this,
                    GetISQLiteDatabase().GetListeSession(GetISQLiteDatabase().GetListeObjetCeleste(GetListeTypeObjets(), GetListeConstellations(), GetListeCatalogues()),
                                                        GetListeSetup(),
                                                        GetListeTypeObservation(),
                                                        GetListeEquipements(),
                                                        GetListeSites(),
                                                        GetListeAllObservationsSession(),
                                                        GetListeAllEquipementsSession(),
                                                        GetListeAllLogicielsSession(),
                                                        GetListeLogiciels()));
            }
            return listeSession;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjEquipementSession GetNewEquipementSession()
        {
            return new ObjEquipementSession(this);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjObservation GetNewObservation()
        {
            return new ObjObservation(this, GetListeTypeObservation(), GetListeEquipements());
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjSession CreateSession(string idObjetCeleste, string idSetup, string idStite, string dateLtnv, string description,
                                        string comment, bool commentDansExif, string thumbnail, string imagesPath, int? rank)
        {
            IObjSession session = GetNewObjSession();
            session.IdObjetCeleste = idObjetCeleste;
            session.IdSetup = idSetup;
            session.IdSite = idStite;
            session.DateLtnv = dateLtnv;
            session.Description = description;
            session.Comment = comment;
            session.CommentDansExif = commentDansExif;
            session.Thumbnail = thumbnail;
            session.ImagesPath = imagesPath;
            session.Rank = rank;
            // Création du Setup dans la BDD
            GetISQLiteDatabase().CreateSession(ref session);
            FlushInternalListes();
            return session;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateSession(IObjSession session)
        {
            // Update de la session en BDD
            GetISQLiteDatabase().UpdateSession(session);
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteSession(IObjSession session)
        {
            // Suppression de l'équipement dans la BDD
            GetISQLiteDatabase().DeleteSession(session);

            // Clear des listes
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjTypeLogiciel> GetListeTypeLogiciels()
        {
            // Si la liste n'existe pas, on la créer
            if (listeTypeLogiciels == null)
            {
                listeTypeLogiciels = GetISQLiteDatabase().GetListeTypeLogiciels();
            }
            return listeTypeLogiciels;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjLogiciel> GetListeLogiciels()
        {
            // Si la liste n'existe pas, on la créer
            if (listeLogiciels == null)
            {
                listeLogiciels = GetISQLiteDatabase().GetListeLogiciels(GetListeTypeLogiciels());
            }
            return listeLogiciels;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateLogiciel(string nom, string idTypeLogiciel, string thumbnail, string comment)
        {
            // Création de l'équipement dans la BDD
            GetISQLiteDatabase().CreateLogiciel(nom, idTypeLogiciel, thumbnail, comment);

            // Clear de la liste des équipements pour forcer le rechargement au prochain appel
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateLogiciel(IObjLogiciel logiciel)
        {
            // Update de l'équipement en BDD
            GetISQLiteDatabase().UpdateLogiciel(logiciel);
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteLogiciel(IObjLogiciel logiciel)
        {
            // Suppression en BDD
            GetISQLiteDatabase().DeleteLogiciel(logiciel);

            // Clear de la liste pour forcer le rechargement au prochain appel
            FlushInternalListes();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjLogicielSession GetNewLogicielSession()
        {
            return new ObjLogicielSession(this);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjObservation> GetListeAllObservationsSession()
        {
            // Si la liste n'existe pas, on la créer
            if (listeObservationSession == null)
            {
                listeObservationSession = GetISQLiteDatabase().GetListeAllObservationsSession(GetListeTypeObservation(), GetListeEquipements());
            }
            return listeObservationSession;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjLogicielSession> GetListeAllLogicielsSession()
        {
            // Si la liste n'existe pas, on la créer
            if (listeLogicielSession == null)
            {
                listeLogicielSession = GetISQLiteDatabase().GetListeAllLogicielsSession();
            }
            return listeLogicielSession;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipementSession> GetListeAllEquipementsSession()
        {
            // Si la liste n'existe pas, on la créer
            if (listeEquipementSession == null)
            {
                listeEquipementSession = GetISQLiteDatabase().GetListeAllEquipementsSession(GetListeEquipements());
            }
            return listeEquipementSession;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipementSetup> GetListeAllEquipementsSetup()
        {
            // Si la liste n'existe pas, on la créer
            if (listeEquipementSetup == null)
            {
                listeEquipementSetup = GetISQLiteDatabase().GetListeAllEquipementsSetup(GetListeEquipements());
            }
            return listeEquipementSetup;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateEquipementsSession(List<IObjEquipementSession> listeNouveauEquipementSession)
        {
            GetISQLiteDatabase().CreateEquipementsSession(listeNouveauEquipementSession);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateObservations(List<IObjObservation> listeObservations)
        {
            GetISQLiteDatabase().CreateObservations(listeObservations);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateLogicielsSession(List<IObjLogicielSession> listeLogicielsSesssion)
        {
            GetISQLiteDatabase().CreateLogicielsSession(listeLogicielsSesssion);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return Resources.Janvier;
                case 2:
                    return Resources.Fevrier;
                case 3:
                    return Resources.Mars;
                case 4:
                    return Resources.Avril;
                case 5:
                    return Resources.Mai;
                case 6:
                    return Resources.Juin;
                case 7:
                    return Resources.Juillet;
                case 8:
                    return Resources.Aout;
                case 9:
                    return Resources.Septembre;
                case 10:
                    return Resources.Octobre;
                case 11:
                    return Resources.Novembre;
                case 12:
                    return Resources.Decembre;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string GetBDDFileName()
        {
            return GetISQLiteDatabase().DatabaseName;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Version GetBDDVersion()
        {
            return GetISQLiteDatabase().GetBDDVersion();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string GetApplicationDataPath()
        {
            return Path.Combine(GetAppContext().UserProfilePath, "AstrAuDobson", "AstroSessionOrganizer");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string GetApplicationSauvegardeBDDPath()
        {
            // On vérifie que le répertoire existe, sinoin on le créer
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Save AstroSessionOrganizer")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Save AstroSessionOrganizer"));
            }
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Save AstroSessionOrganizer");
        }

        #endregion

        #region Méthodes Private

        /// <summary>
        /// Flush les listes internes
        /// </summary>
        private void FlushInternalListes()
        {
            // Liste des objets célestes
            if (objObjetCelesteListe != null && objObjetCelesteListe.Liste != null)
                objObjetCelesteListe.Liste.Clear();
            objObjetCelesteListe = null;
            // Liste des équipements
            if (listeEquipement != null)
            {
                listeEquipement.Clear();
                listeEquipement = null;
            }
            // Liste des logiciels
            if (listeLogiciels != null)
            {
                listeLogiciels.Clear();
                listeLogiciels = null;
            }
            // Liste des Setup
            if (listeSetup != null)
            {
                listeSetup.Clear();
                listeSetup = null;
            }
            // Liste des Sites
            if (listeSites != null)
            {
                listeSites.Clear();
                listeSites = null;
            }
            // Liste des Session
            listeSession = null;
            if (listeLogicielSession != null)
            {
                listeLogicielSession.Clear();
                listeLogicielSession = null;
            }
            if (listeObservationSession != null)
            {
                listeObservationSession.Clear();
                listeObservationSession = null;
            }
            if (listeEquipementSession != null)
            {
                listeEquipementSession.Clear();
                listeEquipementSession = null;
            }
            if (listeEquipementSetup != null)
            {
                listeEquipementSetup.Clear();
                listeEquipementSetup = null;
            }
        }

        /// <summary>
        /// Renvoi un nouvel objet de type <see cref="IObjSession"/>
        /// </summary>
        /// <returns></returns>
        private IObjSession GetNewObjSession()
        {
            return new ObjSession(this,
                                    GetISQLiteDatabase(),
                                    GetListeObjetCeleste().Liste,
                                    GetListeSetup(),
                                    GetListeTypeObservation(),
                                    GetListeEquipements(),
                                    GetListeSites(),
                                    GetListeAllObservationsSession(),
                                    GetListeAllEquipementsSession(),
                                    GetListeAllLogicielsSession(),
                                    GetListeLogiciels());
        }

        #endregion

        #region Champs

        /// <summary>
        /// BDD SQLite
        /// </summary>
        private ISQLiteDatabase sqliteDatabase = null;

        /// <summary>
        /// Liste des catalogues d'objets célestes
        /// </summary>
        private List<IObjCatalogue> listeCatalogues = null;

        /// <summary>
        /// Liste des Types d'objets célestes
        /// </summary>
        private List<IObjTypeObjet> listeTypeObjets = null;

        /// <summary>
        /// Liste des Constellations
        /// </summary>
        private List<IObjConstellation> listeConstellations = null;

        /// <summary>
        /// Liste des objets célestes
        /// </summary>
        private IObjObjetCelesteListe objObjetCelesteListe = null;

        /// <summary>
        /// Liste des Filtres de Magnitude pour l'affichage dans la liste
        /// </summary>
        private Dictionary<string, string> listeFiltreMagnitude = null;

        /// <summary>
        /// Liste des Types d'équipement
        /// </summary>
        private List<IObjTypeEquipement> listeTypeEquipement = null;

        /// <summary>
        /// Liste des équipements
        /// </summary>
        private List<IObjEquipement> listeEquipement = null;

        /// <summary>
        /// Liste des Setup
        /// </summary>
        private List<IObjSetup> listeSetup = null;

        /// <summary>
        /// Liste des Sites d'observations
        /// </summary>
        private List<IObjSite> listeSites = null;

        /// <summary>
        /// Liste des Types d'observation
        /// </summary>
        private List<IObjTypeObservation> listeTypeObservation = null;

        /// <summary>
        /// Liste des sessions d'observations
        /// </summary>
        private IObjSessionListe listeSession = null;

        /// <summary>
        /// Liste des Types de logiciels
        /// </summary>
        private List<IObjTypeLogiciel> listeTypeLogiciels = null;

        /// <summary>
        /// Liste complète des logiciels
        /// </summary>
        private List<IObjLogiciel> listeLogiciels = null;

        /// <summary>
        /// Liste complète des observations session
        /// </summary>
        private List<IObjObservation> listeObservationSession = null;

        /// <summary>
        /// Liste complète des EquipementSession
        /// </summary>
        private List<IObjEquipementSession> listeEquipementSession = null;

        /// <summary>
        /// Liste complète des LogicielsSessions
        /// </summary>
        private List<IObjLogicielSession> listeLogicielSession = null;

        /// <summary>
        /// Liste complète des <see cref="ObjEquipementSetup"/>
        /// </summary>
        private List<IObjEquipementSetup> listeEquipementSetup = null;

        #endregion
    }
}
