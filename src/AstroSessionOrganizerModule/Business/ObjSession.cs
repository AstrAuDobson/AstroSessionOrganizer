using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApplicationTools;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Session d'observations
    /// </summary>
    internal class ObjSession : IObjSession
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdObjetCeleste { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjObjetCeleste ObjetCeleste
        {
            get
            {
                if (!string.IsNullOrEmpty(IdObjetCeleste))
                {
                    return listeObjetCeleste.Where(o => o.Id == IdObjetCeleste).FirstOrDefault();
                }
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdSetup { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjSetup Setup
        {
            get
            {
                if (!string.IsNullOrEmpty(IdSetup))
                {
                    return listeSetup.Where(o => o.Id == IdSetup).FirstOrDefault();
                }
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string NomSetup
        {
            get
            {
                return Setup != null ? Setup.Nom : string.Empty;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdSite { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjSite Site
        {
            get
            {
                if (!string.IsNullOrEmpty(IdSite))
                {
                    return listeSite.Where(o => o.Id == IdSite).FirstOrDefault();
                }
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string NomSite
        {
            get
            {
                return Site != null ? Site.Nom : string.Empty;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string DateLtnv { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime DateHeure
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(DateLtnv))
                    {
                        int year = int.Parse(DateLtnv.Substring(0, 4));
                        int month = int.Parse(DateLtnv.Substring(4, 2));
                        int day = int.Parse(DateLtnv.Substring(6, 2));
                        int hour = int.Parse(DateLtnv.Substring(8, 2));
                        int minute = int.Parse(DateLtnv.Substring(10, 2));
                        int second = int.Parse(DateLtnv.Substring(12, 2));
                        return new DateTime(year, month, day, hour, minute, second);
                    }
                    return DateTime.Now;
                }
                catch
                {
                    return DateTime.Now;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool CommentDansExif { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string DisplayThumbnail
        {
            get
            {
                try
                {
                    // On vérifie que le répertoire contenant les images existe
                    if (string.IsNullOrEmpty(ImagesPath) || !Directory.Exists(ImagesPath))
                        return string.Empty;

                    // Une image est spécifié pour cette session
                    if (!string.IsNullOrEmpty(Thumbnail))
                    {
                        // On vérifie que l'image existe toujours
                        string url = Path.Combine(ImagesPath, Thumbnail);
                        if (File.Exists(url))
                            return url;
                    }
                    // Pas d'image de session spécifié, on prend la première image du répertoire ImagesPath
                    else
                    {
                        foreach(string nomFichier in Directory.GetFiles(ImagesPath))
                        {
                            // On récupère l'extension de fichier
                            if (nomFichier.Length > 4 && 
                                (nomFichier.Substring(nomFichier.Length - 3).ToLower() == "jpg"
                                || nomFichier.Substring(nomFichier.Length - 4).ToLower() == "jpeg"
                                || nomFichier.Substring(nomFichier.Length - 3).ToLower() == "tif"
                                || nomFichier.Substring(nomFichier.Length - 3).ToLower() == "bmp"))
                            {
                                //appToolFactory.GetLog().Log($"Fichier : {nomFichier}");
                                return nomFichier;
                            }
                        }
                    }
                    return string.Empty;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ResizedDisplayThumbnail
        {
            get
            {
                try
                {
                    // Sécurité sur l'image a afficher
                    if (string.IsNullOrEmpty(DisplayThumbnail))
                        return string.Empty;

                    string thumbPath = Path.ChangeExtension(DisplayThumbnail, "thumb");
                    if (!File.Exists(thumbPath))
                    {
                        Image image = Image.FromFile(DisplayThumbnail);
                        if (image.Width == 0 || image.Height == 0)
                            return string.Empty;
                        double factor = 0;
                        double new_width = image.Width;
                        double new_height = image.Height;
                        if (image.Width >= image.Height)
                            factor = 120.0 / image.Width;
                        else
                            factor = 120.0 / image.Height;
                        new_width = image.Width * factor;
                        new_height = image.Height * factor;

                        Image thumb = image.GetThumbnailImage((int)new_width, (int)new_height, () => false, IntPtr.Zero);
                        thumb.Save(thumbPath);
                    }

                    return thumbPath;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ImagesPath { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int? Rank { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int IconeIndex
        {
            get
            {
                switch (ObjetCeleste.TypeObjet.Icone)
                {
                    case "Constellation":
                        return 0;
                    case "NonDefini":
                        return 1;
                    case "Star":
                        return 2;
                    case "MultipleStars":
                        return 3;
                    case "Galaxie":
                        return 4;
                    case "Nebuleuse":
                        return 5;
                    case "Cluster":
                        return 6;
                    case "Planete":
                        return 7;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjObservation> ListeObservationsSession
        {
            get
            {
                if (!string.IsNullOrEmpty(Id) && listeObservationsSession == null)
                {
                    ChargementListeObservationsSession();
                }
                return listeObservationsSession;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipementSession> ListeEquipementsSession
        {
            get
            {
                if (!string.IsNullOrEmpty(Id) && listeEquipementsSession == null)
                {
                    ChargementListeEquipementsSession();
                }
                return listeEquipementsSession;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjLogicielSession> ListeLogicielsSession
        {
            get
            {
                if (!string.IsNullOrEmpty(Id) && listeLogicielsSession == null)
                {
                    ChargementListeLogicielsSession();
                }
                return listeLogicielsSession;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FormatedTempsTotalObservations
        {
            get
            {
                try
                {
                    if (ListeObservationsSession != null)
                    {
                        TimeSpan tempsTotal = TimeSpan.Zero;
                        foreach(IObjObservation observation in ListeObservationsSession.Where(o => o.IdTypeObservation == "1").ToList())
                        {
                            tempsTotal += observation.TempsTotalExposition;
                        }
                        if (tempsTotal != TimeSpan.Zero)
                            return tempsTotal.ToStandardFormatString();
                    }
                    return string.Empty;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FullLogicielsListe
        {
            get
            {
                try
                {
                    if (ListeLogicielsSession != null)
                    {
                        string retourListeLogiciel = string.Empty;
                        foreach (IObjLogicielSession logicielSession in ListeLogicielsSession)
                        {
                            IObjLogiciel logiciel = listeLogiciels.Where(l => l.Id == logicielSession.IdLogiciel).FirstOrDefault();
                            if (logiciel != null)
                            {
                                if (!string.IsNullOrEmpty(retourListeLogiciel))
                                    retourListeLogiciel += " / ";
                                retourListeLogiciel += logiciel.Nom;
                            }
                        }
                        return retourListeLogiciel;
                    }
                    return string.Empty;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                    return string.Empty;
                }
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjSession(IAppToolFactory appToolFactory,
                            ISQLiteDatabase sQLiteDatabase,
                            List<IObjObjetCeleste> listeObjetCeleste,
                            List<IObjSetup> listeSetup,
                            List<IObjTypeObservation> listeTypeObservation,
                            List<IObjEquipement> listeEquipements,
                            List<IObjSite> listeSite,
                            List<IObjObservation> listeCompleteObservationsSession,
                            List<IObjEquipementSession> listeCompleteEquipementsSession,
                            List<IObjLogicielSession> listeCompleteLogicielSession,
                            List<IObjLogiciel> listeLogiciels)
        {
            this.appToolFactory = appToolFactory;
            this.sQLiteDatabase = sQLiteDatabase;
            this.listeObjetCeleste = listeObjetCeleste;
            this.listeSetup = listeSetup;
            this.listeTypeObservation = listeTypeObservation;
            this.listeEquipements = listeEquipements;
            this.listeSite = listeSite;
            this.listeCompleteObservationsSession = listeCompleteObservationsSession;
            this.listeCompleteEquipementsSession = listeCompleteEquipementsSession;
            this.listeCompleteLogicielSession = listeCompleteLogicielSession;
            this.listeLogiciels = listeLogiciels;

            // Positionnement des valeurs par défaut
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateEquipementSession(string nom, string idEquipement)
        {
            if (string.IsNullOrEmpty(idEquipement))
                throw new Exception(Resources.ChampsObligatoiresManquants);
            // Ajout de l'équipement dans la BDD
            sQLiteDatabase.CreateEquipementSession(nom, idEquipement, Id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteEquipements()
        {
            sQLiteDatabase.DeleteEquipementsSession(this);
            if (listeEquipementsSession != null)
                listeEquipementsSession.Clear();
            listeEquipementsSession = null;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateObservationSession(IObjObservation observation)
        {
            if (observation == null)
                throw new Exception(Resources.ChampsObligatoiresManquants);
            // Ajout de l'observation dans la BDD
            sQLiteDatabase.CreateObservation(observation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteObservations()
        {
            sQLiteDatabase.DeleteObservationsSession(this);
            if (listeObservationsSession != null)
                listeObservationsSession.Clear();
            listeObservationsSession = null;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateLogicielSession(string nom, string idLogiciel)
        {
            if (string.IsNullOrEmpty(idLogiciel))
                throw new Exception(Resources.ChampsObligatoiresManquants);
            // Ajout de l'équipement dans la BDD
            sQLiteDatabase.CreateLogicielSession(nom, idLogiciel, Id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteLogiciels()
        {
            sQLiteDatabase.DeleteLogicielsSession(this);
            if (listeLogicielsSession != null)
                listeLogicielsSession.Clear();
            listeLogicielsSession = null;
        }

        /// <summary>
        /// Charegement de la liste des observations de la session
        /// </summary>
        private void ChargementListeObservationsSession()
        {
            try
            {
                listeObservationsSession = listeCompleteObservationsSession.Where(os => os.IdSession == Id).ToList();
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// Charegement de la liste des équipements de la session
        /// </summary>
        private void ChargementListeEquipementsSession()
        {
            try
            {
                listeEquipementsSession = listeCompleteEquipementsSession.Where(es => es.IdSession == Id).ToList();
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// Charegement de la liste des logiciels de la session
        /// </summary>
        private void ChargementListeLogicielsSession()
        {
            try
            {
                listeLogicielsSession = listeCompleteLogicielSession.Where(ls => ls.IdSession == Id).ToList();
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Référence sur l'objet BDD
        /// </summary>
        private readonly ISQLiteDatabase sQLiteDatabase = null;

        /// <summary>
        /// Liste complète des objets célestes
        /// </summary>
        private List<IObjObjetCeleste> listeObjetCeleste = null;

        /// <summary>
        /// Liste complète des setup
        /// </summary>
        private List<IObjSetup> listeSetup = null;

        /// <summary>
        /// Liste complète des types d'observations
        /// </summary>
        private List<IObjTypeObservation> listeTypeObservation = null;

        /// <summary>
        /// Liste complète des équipements
        /// </summary>
        private List<IObjEquipement> listeEquipements = null;

        /// <summary>
        /// Liste complète des sites
        /// </summary>
        private List<IObjSite> listeSite = null;

        /// <summary>
        /// Liste des observations de la session (Singleton)
        /// </summary>
        private List<IObjObservation> listeObservationsSession = null;

        /// <summary>
        /// Liste complète des observations session
        /// </summary>
        private List<IObjObservation> listeCompleteObservationsSession = null;

        /// <summary>
        /// Liste des équipements additionnels de la session (Singleton)
        /// </summary>
        private List<IObjEquipementSession> listeEquipementsSession = null;

        /// <summary>
        /// Liste complète des EquipementSession
        /// </summary>
        private List<IObjEquipementSession> listeCompleteEquipementsSession = null;

        /// <summary>
        /// Liste des logiciels de la session
        /// </summary>
        private List<IObjLogicielSession> listeLogicielsSession = null;

        /// <summary>
        /// Liste complète des logiciels session
        /// </summary>
        private List<IObjLogicielSession> listeCompleteLogicielSession = null;

        /// <summary>
        /// Liste complète des logiciels
        /// </summary>
        private List<IObjLogiciel> listeLogiciels = null;

        #endregion
    }
}
