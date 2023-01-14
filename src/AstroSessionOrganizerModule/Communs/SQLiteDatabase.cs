using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Objet représentant une base de données SQLite
    /// </summary>
    internal class SQLiteDatabase : ISQLiteDatabase
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string DatabaseName
        {
            get
            {
                return Path.Combine(appToolFactory.GetAppContext().UserProfilePath, "AstrAuDobson", "AstroSessionOrganizer", "ASO.db");
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string DatabaseVersion { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal SQLiteDatabase(IAppToolFactory appToolFactory)
        {
            this.appToolFactory = appToolFactory;

            // Positionnement des valeurs par défaut
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Ouverture de la BDD
        /// </summary>
        private SQLiteConnection CreateConnection()
        {
            // Instanciation de la BDD
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + DatabaseName + ";foreign keys=true;");

            try
            {
                // Ouverture de la BDD
                connection.Open();
            }
            catch (Exception ex)
            {
                // Trace de l'erreur et information à l'utilisateur
                appToolFactory.GetLog().LogException(ex, GetType().Name);
                throw;
            }
            return connection;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjObjetCeleste> GetListeObjetCeleste(List<IObjTypeObjet> listeTypeObjet, List<IObjConstellation> listeConstellation, List<IObjCatalogue> listeCatalogue)
        {
            List<IObjObjetCeleste> listTarget = new List<IObjObjetCeleste>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM ObjetsCelestes";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                double ra;
                                double? raValue = null;
                                if (!string.IsNullOrEmpty(reader["RA"].ToString()) && double.TryParse(reader["RA"].ToString(), out ra))
                                    raValue = ra;
                                double dec;
                                double? decValue = null;
                                if (!string.IsNullOrEmpty(reader["DEC"].ToString()) && double.TryParse(reader["DEC"].ToString(), out dec))
                                    decValue = dec;
                                double size_max;
                                double? size_maxValue = null;
                                if (!string.IsNullOrEmpty(reader["SIZE_MAX"].ToString()) && double.TryParse(reader["SIZE_MAX"].ToString(), out size_max))
                                    size_maxValue = size_max;
                                double size_min;
                                double? size_minValue = null;
                                if (!string.IsNullOrEmpty(reader["SIZE_MIN"].ToString()) && double.TryParse(reader["SIZE_MIN"].ToString(), out size_min))
                                    size_minValue = size_min;
                                double mag_visual;
                                double? mag_visualValue = null;
                                if (!string.IsNullOrEmpty(reader["MAG_VISUAL"].ToString()) && double.TryParse(reader["MAG_VISUAL"].ToString(), out mag_visual))
                                    mag_visualValue = mag_visual;
                                double mag_photo;
                                double? mag_photoValue = null;
                                if (!string.IsNullOrEmpty(reader["MAG_PHOTO"].ToString()) && double.TryParse(reader["MAG_PHOTO"].ToString(), out mag_photo))
                                    mag_photoValue = mag_photo;
                                double redshift;
                                double? redshiftValue = null;
                                if (!string.IsNullOrEmpty(reader["REDSHIFT"].ToString()) && double.TryParse(reader["REDSHIFT"].ToString(), out redshift))
                                    redshiftValue = redshift;
                                double distance_rs;
                                double? distance_rsValue = null;
                                if (!string.IsNullOrEmpty(reader["DISTANCE_RS"].ToString()) && double.TryParse(reader["DISTANCE_RS"].ToString(), out distance_rs))
                                    distance_rsValue = distance_rs;
                                double distance_m;
                                double? distance_mValue = null;
                                if (!string.IsNullOrEmpty(reader["DISTANCE_M"].ToString()) && double.TryParse(reader["DISTANCE_M"].ToString(), out distance_m))
                                    distance_mValue = distance_m;
                                listTarget.Add(new ObjObjetCeleste(appToolFactory, listeTypeObjet, listeConstellation, listeCatalogue)
                                {
                                    Id = reader["Id"].ToString(),
                                    Nom = reader["Nom"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    IdTypeObjet = reader["IdType"].ToString(),
                                    IdConstellation = reader["IdConstellation"].ToString(),
                                    CompleteDenominations = reader["Denominations"].ToString(),
                                    CompleteCatalogues = reader["Catalogue"].ToString(),
                                    RA = raValue.HasValue ? appToolFactory.GetCoordinate(raValue.Value, CoordinatesType.RA) : appToolFactory.GetCoordinate(0, CoordinatesType.RA),
                                    DEC = decValue.HasValue ? appToolFactory.GetCoordinate(decValue.Value, CoordinatesType.DEC) : appToolFactory.GetCoordinate(0, CoordinatesType.DEC),
                                    SIZE_MAX = size_maxValue,
                                    SIZE_MIN = size_minValue,
                                    MAG_VISUAL = mag_visualValue,
                                    MAG_PHOTO = mag_photoValue,
                                    REDSHIFT = redshiftValue,
                                    DISTANCE_RS = distance_rsValue,
                                    DISTANCE_M = distance_mValue,
                                    Thumbnail = reader["Thumbnail"].ToString(),
                                    ThumbnailPosition = reader["ThumbnailPosition"].ToString(),
                                    URLWiki = reader["URLWiki"].ToString(),
                                    Origin = reader["Origin"].ToString(),
                                    Comment = reader["Comment"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetTargetListe effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listTarget;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateObjetCeleste(IObjObjetCeleste objetCeleste)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (objetCeleste == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string updateCommande = @"update ObjetsCelestes set Nom = @Nom," +
                                                                        @" Description = @Description," +
                                                                        @" Denominations = @Denominations," +
                                                                        @" IdType = @IdType," +
                                                                        @" IdConstellation = @IdConstellation," +
                                                                        @" RA = @RA," +
                                                                        @" DEC = @DEC," +
                                                                        @" SIZE_MAX = @SIZE_MAX," +
                                                                        @" SIZE_MIN = @SIZE_MIN," +
                                                                        @" MAG_VISUAL = @MAG_VISUAL," +
                                                                        @" MAG_PHOTO = @MAG_PHOTO," +
                                                                        @" REDSHIFT = @REDSHIFT," +
                                                                        @" DISTANCE_RS = @DISTANCE_RS," +
                                                                        @" DISTANCE_M = @DISTANCE_M," +
                                                                        @" Catalogue = @Catalogue," +
                                                                        @" Thumbnail = @Thumbnail," +
                                                                        @" ThumbnailPosition = @ThumbnailPosition," +
                                                                        @" URLWiki = @URLWiki," +
                                                                        @" Origin = @Origin," +
                                                                        @" Comment = @Comment" +
                                                                        @" where Id = @Id";
                        sqlCommande.CommandText = updateCommande;
                        sqlCommande.Parameters.AddWithValue("Id", objetCeleste.Id);
                        sqlCommande.Parameters.AddWithValue("Nom", objetCeleste.Nom);
                        sqlCommande.Parameters.AddWithValue("Description", objetCeleste.Description);
                        sqlCommande.Parameters.AddWithValue("Denominations", objetCeleste.CompleteDenominations);
                        sqlCommande.Parameters.AddWithValue("IdType", objetCeleste.IdTypeObjet);
                        sqlCommande.Parameters.AddWithValue("IdConstellation", objetCeleste.IdConstellation);
                        sqlCommande.Parameters.AddWithValue("RA", objetCeleste.RA.Coordonnee.ToString(CultureInfo.InvariantCulture));
                        sqlCommande.Parameters.AddWithValue("DEC", objetCeleste.DEC.Coordonnee.ToString(CultureInfo.InvariantCulture));
                        sqlCommande.Parameters.AddWithValue("SIZE_MAX", objetCeleste.SIZE_MAX.HasValue ? objetCeleste.SIZE_MAX.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("SIZE_MIN", objetCeleste.SIZE_MIN.HasValue ? objetCeleste.SIZE_MIN.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("MAG_VISUAL", objetCeleste.MAG_VISUAL.HasValue ? objetCeleste.MAG_VISUAL.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("MAG_PHOTO", objetCeleste.MAG_PHOTO.HasValue ? objetCeleste.MAG_PHOTO.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("REDSHIFT", objetCeleste.REDSHIFT.HasValue ? objetCeleste.REDSHIFT.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("DISTANCE_RS", objetCeleste.DISTANCE_RS.HasValue ? objetCeleste.DISTANCE_RS.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("DISTANCE_M", objetCeleste.DISTANCE_M.HasValue ? objetCeleste.DISTANCE_M.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("Catalogue", objetCeleste.CompleteCatalogues);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", objetCeleste.Thumbnail);
                        sqlCommande.Parameters.AddWithValue("ThumbnailPosition", objetCeleste.ThumbnailPosition);
                        sqlCommande.Parameters.AddWithValue("URLWiki", objetCeleste.URLWiki);
                        sqlCommande.Parameters.AddWithValue("Origin", objetCeleste.Origin);
                        sqlCommande.Parameters.AddWithValue("Comment", objetCeleste.Comment);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête UpdateObjetCeleste [{objetCeleste.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateObjetCeleste(string nom, string description, string denominations, string idType, string idConstellation,
                                        Coordinate ra, Coordinate dec, double? size_max, double? size_min, double? mag_visual, double? mag_photo,
                                        double? redshift, double? distance_rs, double? distance_m, string catalogue, string thumbnail,
                                        string thumbnailPosition, string urlWiki, string origin, string comment)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(idType) || string.IsNullOrEmpty(idConstellation))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertCommande = @"insert into ObjetsCelestes(Nom, Description, Denominations, IdType, IdConstellation,"
                                                                    + @"RA, DEC, SIZE_MAX, SIZE_MIN, MAG_VISUAL, MAG_PHOTO, REDSHIFT,"
                                                                    + @"DISTANCE_RS, DISTANCE_M, Catalogue, Thumbnail, ThumbnailPosition, URLWiki, Origin, Comment)"
                                                                    + @" values(@Nom, @Description, @Denominations, @IdType, @IdConstellation,"
                                                                    + @"@RA, @DEC, @SIZE_MAX, @SIZE_MIN, @MAG_VISUAL, @MAG_PHOTO, @REDSHIFT,"
                                                                    + @"@DISTANCE_RS, @DISTANCE_M, @Catalogue, @Thumbnail, @ThumbnailPosition, @URLWiki, @Origin, @Comment)";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("Nom", nom);
                        sqlCommande.Parameters.AddWithValue("Description", description);
                        sqlCommande.Parameters.AddWithValue("Denominations", denominations);
                        sqlCommande.Parameters.AddWithValue("IdType", idType);
                        sqlCommande.Parameters.AddWithValue("IdConstellation", idConstellation);
                        sqlCommande.Parameters.AddWithValue("RA", ra.Coordonnee.ToString(CultureInfo.InvariantCulture));
                        sqlCommande.Parameters.AddWithValue("DEC", dec.Coordonnee.ToString(CultureInfo.InvariantCulture));
                        sqlCommande.Parameters.AddWithValue("SIZE_MAX", size_max.HasValue ? size_max.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("SIZE_MIN", size_min.HasValue ? size_min.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("MAG_VISUAL", mag_visual.HasValue ? mag_visual.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("MAG_PHOTO", mag_photo.HasValue ? mag_photo.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("REDSHIFT", redshift.HasValue ? redshift.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("DISTANCE_RS", distance_rs.HasValue ? distance_rs.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("DISTANCE_M", distance_m.HasValue ? distance_m.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("Catalogue", catalogue);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", thumbnail);
                        sqlCommande.Parameters.AddWithValue("ThumbnailPosition", thumbnailPosition);
                        sqlCommande.Parameters.AddWithValue("URLWiki", urlWiki);
                        sqlCommande.Parameters.AddWithValue("Origin", origin);
                        sqlCommande.Parameters.AddWithValue("Comment", comment);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête CreateObjetCeleste [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteObjetCeleste(IObjObjetCeleste objetCeleste)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (objetCeleste == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM ObjetsCelestes WHERE Id=@Id";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("Id", objetCeleste.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête DeleteObjetCeleste [{objetCeleste.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjCatalogue> GetListeCatalogues()
        {
            List<IObjCatalogue> listCatalogue = new List<IObjCatalogue>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM ObjetsCatalogues";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listCatalogue.Add(new ObjCatalogue(appToolFactory)
                                {
                                    Id = reader["Id"].ToString(),
                                    Code = reader["Code"].ToString(),
                                    Nom = reader["Nom"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetCatalogueListe effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listCatalogue;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjTypeObjet> GetListeTypeObjets()
        {
            List<IObjTypeObjet> listType = new List<IObjTypeObjet>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM TypeObjets";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listType.Add(new ObjTypeObjet(appToolFactory)
                                {
                                    Id = reader["Id"].ToString(),
                                    Code = reader["Code"].ToString(),
                                    Nom = reader["Nom"].ToString(),
                                    Thumbnail = reader["Thumbnail"].ToString(),
                                    Icone = reader["Icone"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetTypeObjetsListe effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listType;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjConstellation> GetListeConstellations()
        {
            List<IObjConstellation> listConstellations = new List<IObjConstellation>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM Constellations";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                double ra;
                                double? raValue = null;
                                if (double.TryParse(reader["RA"].ToString(), out ra))
                                    raValue = ra;
                                double dec;
                                double? decValue = null;
                                if (double.TryParse(reader["DEC"].ToString(), out dec))
                                    decValue = dec;
                                double etendue_Deg2;
                                double? etendue_Deg2Value = null;
                                if (double.TryParse(reader["Etendue_Deg2"].ToString(), out etendue_Deg2))
                                    etendue_Deg2Value = etendue_Deg2;
                                double etendue_Pct;
                                double? etendue_PctValue = null;
                                if (double.TryParse(reader["Etendue_Deg2"].ToString(), out etendue_Pct))
                                    etendue_PctValue = etendue_Pct;
                                listConstellations.Add(new ObjConstellation(appToolFactory)
                                {
                                    Id = reader["Id"].ToString(),
                                    Nom = reader["Nom"].ToString(),
                                    NomLatin = reader["NomLatin"].ToString(),
                                    Abr = reader["Abr"].ToString(),
                                    RA = raValue.HasValue ? appToolFactory.GetCoordinate(raValue.Value, CoordinatesType.RA) : appToolFactory.GetCoordinate(0, CoordinatesType.RA),
                                    DEC = decValue.HasValue ? appToolFactory.GetCoordinate(decValue.Value, CoordinatesType.DEC) : appToolFactory.GetCoordinate(0, CoordinatesType.DEC),
                                    Etendue_Deg2 = etendue_Deg2Value,
                                    Etendue_Pct = etendue_PctValue,
                                    Description = reader["Description"].ToString(),
                                    URLWiki = reader["URLWiki"].ToString(),
                                    ThumbnailPosition = reader["ThumbnailPosition"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetConstellationsListe effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listConstellations;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjTypeEquipement> GetListeTypeEquipements()
        {
            List<IObjTypeEquipement> listType = new List<IObjTypeEquipement>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM TypeEquipements";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listType.Add(new ObjTypeEquipement(appToolFactory)
                                {
                                    Id = reader["Id"].ToString(),
                                    Nom = reader["Nom"].ToString(),
                                    Icone = reader["Icone"].ToString(),
                                    Comment = reader["Comment"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetTypeEquipementsListe effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listType;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipement> GetListeEquipements(List<IObjTypeEquipement> listeTypeEquipements)
        {
            List<IObjEquipement> listType = new List<IObjEquipement>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM Equipements";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listType.Add(new ObjEquipement(appToolFactory, listeTypeEquipements)
                                {
                                    Id = reader["Id"].ToString(),
                                    IdTypeEquipement = reader["IdTypeEquipement"].ToString(),
                                    Nom = reader["Nom"].ToString(),
                                    Thumbnail = reader["Thumbnail"].ToString(),
                                    Comment = reader["Comment"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetEquipementsListe effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listType;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipementSetup> GetListeEquipementsSetup(string idSetup, List<IObjEquipement> listeEquipements)
        {
            List<IObjEquipementSetup> listEquipement = new List<IObjEquipementSetup>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (listeEquipements == null || string.IsNullOrEmpty(idSetup))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM EquipementSetup WHERE IdSetup=@IdSetup";
                        sqlCommande.Parameters.AddWithValue("IdSetup", idSetup);
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IObjEquipement equipement = listeEquipements.Where(e => e.Id == reader["IdEquipement"].ToString()).FirstOrDefault();
                                if (equipement != null)
                                {
                                    string nomEquipementSetup = reader["Nom"].ToString();
                                    //if (string.IsNullOrEmpty(nomEquipementSetup))
                                    //    nomEquipementSetup = equipement.Nom;
                                    listEquipement.Add(new ObjEquipementSetup(appToolFactory)
                                    {
                                        IdEquipement = equipement.Id,
                                        IdSetup = idSetup,
                                        Nom = nomEquipementSetup
                                    });
                                }
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"GetListeEquipementsSetup effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listEquipement;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipementSetup> GetListeAllEquipementsSetup(List<IObjEquipement> listeEquipements)
        {
            List<IObjEquipementSetup> listEquipement = new List<IObjEquipementSetup>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (listeEquipements == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM EquipementSetup";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IObjEquipement equipement = listeEquipements.Where(e => e.Id == reader["IdEquipement"].ToString()).FirstOrDefault();
                                if (equipement != null)
                                {
                                    string nomEquipementSetup = reader["Nom"].ToString();
                                    //if (string.IsNullOrEmpty(nomEquipementSetup))
                                    //    nomEquipementSetup = equipement.Nom;
                                    listEquipement.Add(new ObjEquipementSetup(appToolFactory)
                                    {
                                        IdEquipement = equipement.Id,
                                        IdSetup = reader["IdSetup"].ToString(),
                                        Nom = nomEquipementSetup
                                    });
                                }
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"GetListeAllEquipementsSetup effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listEquipement;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjSetup> GetListeSetup(List<IObjEquipement> listeEquipements, List<IObjEquipementSetup> listeCompleteEquipementSetup)
        {
            List<IObjSetup> listSetup = new List<IObjSetup>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM Setup";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listSetup.Add(new ObjSetup(appToolFactory, this, listeEquipements, listeCompleteEquipementSetup)
                                {
                                    Id = reader["Id"].ToString(),
                                    Nom = reader["Nom"].ToString(),
                                    Thumbnail = reader["Thumbnail"].ToString(),
                                    Comment = reader["Comment"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetSetupListe effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listSetup;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateEquipement(IObjEquipement equipement)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (equipement == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string updateCommande = @"update Equipements set Nom = @Nom," +
                                                                        @" IdTypeEquipement = @IdTypeEquipement," +
                                                                        @" Thumbnail = @Thumbnail," +
                                                                        @" Comment = @Comment" +
                                                                        @" where Id = @Id";
                        sqlCommande.CommandText = updateCommande;
                        sqlCommande.Parameters.AddWithValue("Id", equipement.Id);
                        sqlCommande.Parameters.AddWithValue("Nom", equipement.Nom);
                        sqlCommande.Parameters.AddWithValue("IdTypeEquipement", equipement.IdTypeEquipement);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", equipement.Thumbnail);
                        sqlCommande.Parameters.AddWithValue("Comment", equipement.Comment);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête UpdateEquipement [{equipement.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateEquipement(string nom, string idTypeEquipement, string thumbnail, string comment)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(idTypeEquipement))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertCommande = @"insert into Equipements(Nom, IdTypeEquipement, Thumbnail, Comment)" +
                                                                        @" values(@Nom, @IdTypeEquipement, @Thumbnail, @Comment)";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("Nom", nom);
                        sqlCommande.Parameters.AddWithValue("IdTypeEquipement", idTypeEquipement);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", thumbnail);
                        sqlCommande.Parameters.AddWithValue("Comment", comment);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête CreateEquipement [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteEquipement(IObjEquipement equipement)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (equipement == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM Equipements WHERE Id=@Id";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("Id", equipement.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête DeleteEquipement [{equipement.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjSetup CreateSetup(string nom, string thumbnail, string comment, List<IObjEquipement> listeEquipements, List<IObjEquipementSetup> listeCompleteEquipementSetup)
        {
            IObjSetup setup = null;
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(nom))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertCommande = @"insert into Setup(Nom, Thumbnail, Comment) values(@Nom, @Thumbnail, @Comment); SELECT last_insert_rowid();";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("Nom", nom);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", thumbnail);
                        sqlCommande.Parameters.AddWithValue("Comment", comment);
                        long rowId = (long)sqlCommande.ExecuteScalar();

                        setup = new ObjSetup(appToolFactory, this, listeEquipements, listeCompleteEquipementSetup)
                        {
                            Id = rowId.ToString(),
                            Nom = nom,
                            Thumbnail = thumbnail,
                            Comment = comment
                        };

                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête CreateSetup [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw new Exception("Erreur lors du traitement de la requête");
            }
            return setup;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateSetup(IObjSetup setup)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (setup == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string updateCommande = @"update Setup set Nom = @Nom," +
                                                                        @" Thumbnail = @Thumbnail," +
                                                                        @" Comment = @Comment" +
                                                                        @" where Id = @Id";
                        sqlCommande.CommandText = updateCommande;
                        sqlCommande.Parameters.AddWithValue("Id", setup.Id);
                        sqlCommande.Parameters.AddWithValue("Nom", setup.Nom);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", setup.Thumbnail);
                        sqlCommande.Parameters.AddWithValue("Comment", setup.Comment);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête UpdateSetup [{setup.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteSetup(IObjSetup setup)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (setup == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM Setup WHERE Id=@Id";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("Id", setup.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête DeleteSetup [{setup.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteEquipementsSetup(IObjSetup setup)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (setup == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM EquipementSetup WHERE IdSetup=@IdSetup";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("IdSetup", setup.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête DeleteEquipementSetup [{setup.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateEquipementSetup(string nom, string idEquipement, string idSetup)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(idEquipement) || string.IsNullOrEmpty(idSetup))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertCommande = @"insert into EquipementSetup(Nom, IdEquipement, IdSetup) values(@Nom, @IdEquipement, @IdSetup)";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("Nom", nom);
                        sqlCommande.Parameters.AddWithValue("IdEquipement", idEquipement);
                        sqlCommande.Parameters.AddWithValue("IdSetup", idSetup);
                        sqlCommande.ExecuteNonQuery();

                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête CreateEquipementSetup [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjSite> GetListeSites()
        {
            List<IObjSite> listSites = new List<IObjSite>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM Sites";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                double longitude;
                                double? longitudeValue = null;
                                if (double.TryParse(reader["Longitude"].ToString(), out longitude))
                                    longitudeValue = longitude;
                                double latitude;
                                double? latitudeValue = null;
                                if (double.TryParse(reader["Latitude"].ToString(), out latitude))
                                    latitudeValue = latitude;
                                double indiceBortle;
                                double? indiceBortleValue = null;
                                if (double.TryParse(reader["IndiceBortle"].ToString(), out indiceBortle))
                                    indiceBortleValue = indiceBortle;
                                listSites.Add(new ObjSite(appToolFactory)
                                {
                                    Id = reader["Id"].ToString(),
                                    Nom = reader["Nom"].ToString(),
                                    Longitude = longitudeValue,
                                    Latitude = latitudeValue,
                                    Thumbnail = reader["Thumbnail"].ToString(),
                                    ThumbnailPosition = reader["ThumbnailPosition"].ToString(),
                                    ThumbnailBortle = reader["ThumbnailBortle"].ToString(),
                                    Comment = reader["Comment"].ToString(),
                                    IndiceBortle = indiceBortleValue
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetListeSites effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listSites;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjSite CreateSite(string nom, Coordinates coordonnee, string thumbnail, string thumbnailPosition, string thumbnailBortle, string comment, double? indiceBortle)
        {
            IObjSite site = null;
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(nom) || coordonnee == null || coordonnee.CoordonneeLongitude == null || coordonnee.CoordonneeLatitude == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertCommande = @"insert into Sites(Nom, Longitude, Latitude, Thumbnail, ThumbnailPosition, ThumbnailBortle, Comment, IndiceBortle)"
                                                + @" values(@Nom, @Longitude, @Latitude, @Thumbnail, @ThumbnailPosition, @ThumbnailBortle, @Comment, @IndiceBortle); SELECT last_insert_rowid();";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("Nom", nom);
                        sqlCommande.Parameters.AddWithValue("Longitude", coordonnee.CoordonneeLongitude.Coordonnee);
                        sqlCommande.Parameters.AddWithValue("Latitude", coordonnee.CoordonneeLatitude.Coordonnee);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", thumbnail);
                        sqlCommande.Parameters.AddWithValue("ThumbnailPosition", thumbnailPosition);
                        sqlCommande.Parameters.AddWithValue("ThumbnailBortle", thumbnailBortle);
                        sqlCommande.Parameters.AddWithValue("Comment", comment);
                        sqlCommande.Parameters.AddWithValue("IndiceBortle", indiceBortle.HasValue ? indiceBortle.Value.ToString(CultureInfo.InvariantCulture) : null);
                        long rowId = (long)sqlCommande.ExecuteScalar();

                        site = new ObjSite(appToolFactory)
                        {
                            Id = rowId.ToString(),
                            Nom = nom,
                            Longitude = coordonnee.CoordonneeLongitude.Coordonnee,
                            Latitude = coordonnee.CoordonneeLatitude.Coordonnee,
                            Thumbnail = thumbnail,
                            ThumbnailPosition = thumbnailPosition,
                            ThumbnailBortle = thumbnailBortle,
                            Comment = comment
                        };

                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête CreateSite [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return site;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateSite(IObjSite site)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (site == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string longitude = site.Longitude.HasValue ? site.Longitude.Value.ToString(CultureInfo.InvariantCulture) : "";
                        string latitude = site.Latitude.HasValue ? site.Latitude.Value.ToString(CultureInfo.InvariantCulture) : "";
                        string updateCommande = @"update Sites set Nom = @Nom," +
                                                                        @" Longitude = @Longitude," +
                                                                        @" Latitude = @Latitude," +
                                                                        @" Thumbnail = @Thumbnail," +
                                                                        @" ThumbnailPosition = @ThumbnailPosition," +
                                                                        @" ThumbnailBortle = @ThumbnailBortle," +
                                                                        @" Comment = @Comment," +
                                                                        @" IndiceBortle = @IndiceBortle" +
                                                                        @" where Id = @Id";
                        sqlCommande.CommandText = updateCommande;
                        sqlCommande.Parameters.AddWithValue("Id", site.Id);
                        sqlCommande.Parameters.AddWithValue("Nom", site.Nom);
                        sqlCommande.Parameters.AddWithValue("Longitude", longitude);
                        sqlCommande.Parameters.AddWithValue("Latitude", latitude);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", site.Thumbnail);
                        sqlCommande.Parameters.AddWithValue("ThumbnailPosition", site.ThumbnailPosition);
                        sqlCommande.Parameters.AddWithValue("ThumbnailBortle", site.ThumbnailBortle);
                        sqlCommande.Parameters.AddWithValue("Comment", site.Comment);
                        sqlCommande.Parameters.AddWithValue("IndiceBortle", site.IndiceBortle.HasValue ? site.IndiceBortle.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête UpdateSite [{site.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteSite(IObjSite site)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (site == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM Sites WHERE Id=@Id";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("Id", site.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête DeleteSite [{site.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjTypeObservation> GetListeTypeObservation()
        {
            List<IObjTypeObservation> listType = new List<IObjTypeObservation>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM TypeObservations";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listType.Add(new ObjTypeObservation(appToolFactory)
                                {
                                    Id = reader["Id"].ToString(),
                                    Nom = reader["Nom"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetListeTypeObservation effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listType;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjSession> GetListeSession(List<IObjObjetCeleste> listeObjetCeleste,
                                                    List<IObjSetup> listeSetup,
                                                    List<IObjTypeObservation> listeTypeObservation,
                                                    List<IObjEquipement> listeEquipements,
                                                    List<IObjSite> listeSite,
                                                    List<IObjObservation> listeCompleteObservationsSession,
                                                    List<IObjEquipementSession> listeCompleteEquipementsSession,
                                                    List<IObjLogicielSession> listeCompleteLogicielSession,
                                                    List<IObjLogiciel> listeLogiciels)
        {
            List<IObjSession> listSession = new List<IObjSession>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM Sessions";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (string.IsNullOrEmpty(reader["IdObjetCeleste"].ToString()))
                                    throw new Exception("IdObjetCeleste nul lors de la lecture d'une session");
                                if (listeObjetCeleste == null || listeObjetCeleste.Count == 0)
                                    throw new Exception("Liste des objets céleste nul ou vide lors de la lecture d'une session");
                                int rank;
                                int? rankValue = null;
                                if (!string.IsNullOrEmpty(reader["Rank"].ToString()) && int.TryParse(reader["Rank"].ToString(), out rank))
                                    rankValue = rank;
                                listSession.Add(new ObjSession(appToolFactory, this,
                                                                listeObjetCeleste,
                                                                listeSetup,
                                                                listeTypeObservation,
                                                                listeEquipements,
                                                                listeSite,
                                                                listeCompleteObservationsSession,
                                                                listeCompleteEquipementsSession,
                                                                listeCompleteLogicielSession,
                                                                listeLogiciels)
                                {
                                    Id = reader["Id"].ToString(),
                                    IdObjetCeleste = reader["IdObjetCeleste"].ToString(),
                                    IdSite = reader["IdSite"].ToString(),
                                    IdSetup = reader["IdSetup"].ToString(),
                                    DateLtnv = reader["Date"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Comment = reader["Comment"].ToString(),
                                    CommentDansExif = !string.IsNullOrEmpty(reader["CommentDansExif"].ToString()) && reader["CommentDansExif"].ToString() == "1",
                                    Thumbnail = reader["Thumbnail"].ToString(),
                                    ImagesPath = reader["ImagesPath"].ToString(),
                                    Rank = rankValue
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetListeSession effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listSession;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateSession(ref IObjSession session)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (session == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        if (string.IsNullOrEmpty(session.IdObjetCeleste))
                            throw new Exception("IdObjetCeleste nul lors de la création d'une session");
                        
                        string insertCommande = @"insert into Sessions(IdObjetCeleste, IdSetup, IdSite, Date, Description, Comment,"
                                                    + @" CommentDansExif, Thumbnail, ImagesPath, Rank)"
                                                    + @" values(@IdObjetCeleste, @IdSetup, @IdSite, @Date, @Description, @Comment,"
                                                    + @" @CommentDansExif, @Thumbnail, @ImagesPath, @Rank); SELECT last_insert_rowid();";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("IdObjetCeleste", session.IdObjetCeleste);
                        sqlCommande.Parameters.AddWithValue("IdSetup", !string.IsNullOrEmpty(session.IdSetup) ? session.IdSetup : null);
                        sqlCommande.Parameters.AddWithValue("IdSite", !string.IsNullOrEmpty(session.IdSite) ? session.IdSite : null);
                        sqlCommande.Parameters.AddWithValue("Date", session.DateLtnv);
                        sqlCommande.Parameters.AddWithValue("Description", session.Description);
                        sqlCommande.Parameters.AddWithValue("Comment", session.Comment);
                        sqlCommande.Parameters.AddWithValue("CommentDansExif", session.CommentDansExif ? "1" : "0");
                        sqlCommande.Parameters.AddWithValue("Thumbnail", session.Thumbnail);
                        sqlCommande.Parameters.AddWithValue("ImagesPath", session.ImagesPath);
                        sqlCommande.Parameters.AddWithValue("Rank", session.Rank.HasValue ? session.Rank.Value.ToString() : null);
                        long rowId = (long)sqlCommande.ExecuteScalar();

                        session.Id = rowId.ToString();

                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête CreateSession [{session.Id}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw new Exception("Erreur lors du traitement de la requête");
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateSession(IObjSession session)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (session == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string updateCommande = @"update Sessions set IdObjetCeleste = @IdObjetCeleste," +
                                                @" IdSetup = @IdSetup," +
                                                @" IdSite = @IdSite," +
                                                @" Date = @Date," +
                                                @" Description = @Description," +
                                                @" Comment = @Comment," +
                                                @" CommentDansExif = @CommentDansExif," +
                                                @" Thumbnail = @Thumbnail," +
                                                @" ImagesPath = @ImagesPath," +
                                                @" Rank = @Rank" +
                                                @" where Id = @Id";
                        sqlCommande.CommandText = updateCommande;
                        sqlCommande.Parameters.AddWithValue("Id", session.Id);
                        sqlCommande.Parameters.AddWithValue("IdObjetCeleste", session.IdObjetCeleste);
                        sqlCommande.Parameters.AddWithValue("IdSetup", !string.IsNullOrEmpty(session.IdSetup) ? session.IdSetup : null);
                        sqlCommande.Parameters.AddWithValue("IdSite", !string.IsNullOrEmpty(session.IdSite) ? session.IdSite : null);
                        sqlCommande.Parameters.AddWithValue("Date", session.DateLtnv);
                        sqlCommande.Parameters.AddWithValue("Description", session.Description);
                        sqlCommande.Parameters.AddWithValue("Comment", session.Comment);
                        sqlCommande.Parameters.AddWithValue("CommentDansExif", session.CommentDansExif ? "1" : "0");
                        sqlCommande.Parameters.AddWithValue("Thumbnail", session.Thumbnail);
                        sqlCommande.Parameters.AddWithValue("ImagesPath", session.ImagesPath);
                        sqlCommande.Parameters.AddWithValue("Rank", session.Rank.HasValue ? session.Rank.Value.ToString() : null);
                        sqlCommande.ExecuteNonQuery();

                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête CreateSession [{session.Id}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw new Exception("Erreur lors du traitement de la requête");
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteSession(IObjSession session)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (session == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM Sessions WHERE Id=@Id";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("Id", session.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête DeleteSession [{session.DateLtnv}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipementSession> GetListeEquipementsSession(string idSession, List<IObjEquipement> listeEquipements)
        {
            List<IObjEquipementSession> listEquipement = new List<IObjEquipementSession>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (listeEquipements == null || string.IsNullOrEmpty(idSession))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM EquipementSession WHERE IdSession=@IdSession";
                        sqlCommande.Parameters.AddWithValue("IdSession", idSession);
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IObjEquipement equipement = listeEquipements.Where(e => e.Id == reader["IdEquipement"].ToString()).FirstOrDefault();
                                if (equipement != null)
                                {
                                    string nomEquipementSession = reader["Nom"].ToString();
                                    //if (string.IsNullOrEmpty(nomEquipementSetup))
                                    //    nomEquipementSetup = equipement.Nom;
                                    listEquipement.Add(new ObjEquipementSession(appToolFactory)
                                    {
                                        IdEquipement = equipement.Id,
                                        IdSession = idSession,
                                        Nom = nomEquipementSession
                                    });
                                }
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"GetListeEquipementsSession effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listEquipement;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipementSession> GetListeAllEquipementsSession(List<IObjEquipement> listeEquipements)
        {
            List<IObjEquipementSession> listEquipement = new List<IObjEquipementSession>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (listeEquipements == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM EquipementSession";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IObjEquipement equipement = listeEquipements.Where(e => e.Id == reader["IdEquipement"].ToString()).FirstOrDefault();
                                if (equipement != null)
                                {
                                    string nomEquipementSession = reader["Nom"].ToString();
                                    listEquipement.Add(new ObjEquipementSession(appToolFactory)
                                    {
                                        IdEquipement = equipement.Id,
                                        IdSession = reader["IdSession"].ToString(),
                                        Nom = nomEquipementSession
                                    });
                                }
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"GetListeAllLogicielsSession effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listEquipement;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteEquipementsSession(IObjSession session)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (session == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM EquipementSession WHERE IdSession=@IdSession";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("IdSession", session.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                //appToolFactory.GetLog().Log($"Requête DeleteEquipementsSession [{session.Id}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateEquipementSession(string nom, string idEquipement, string idSession)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(idEquipement) || string.IsNullOrEmpty(idSession))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertCommande = @"insert into EquipementSession(Nom, IdEquipement, IdSession) values(@Nom, @IdEquipement, @IdSession)";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("Nom", nom);
                        sqlCommande.Parameters.AddWithValue("IdEquipement", idEquipement);
                        sqlCommande.Parameters.AddWithValue("IdSession", idSession);
                        sqlCommande.ExecuteNonQuery();

                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                //appToolFactory.GetLog().Log($"Requête CreateEquipementSession [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateEquipementsSession(List<IObjEquipementSession> listeNouveauEquipementSession)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (listeNouveauEquipementSession == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach(IObjEquipementSession nouveauEquipementSession in listeNouveauEquipementSession)
                        {
                            using (SQLiteCommand sqlCommande = connection.CreateCommand())
                            {
                                string insertCommande = @"insert into EquipementSession(Nom, IdEquipement, IdSession) values(@Nom, @IdEquipement, @IdSession)";
                                sqlCommande.CommandText = insertCommande;
                                sqlCommande.Parameters.AddWithValue("Nom", nouveauEquipementSession.Nom);
                                sqlCommande.Parameters.AddWithValue("IdEquipement", nouveauEquipementSession.IdEquipement);
                                sqlCommande.Parameters.AddWithValue("IdSession", nouveauEquipementSession.IdSession);
                                sqlCommande.ExecuteNonQuery();
                                sqlCommande.Dispose();
                            }
                        }
                        // On commit la transaction
                        transaction.Commit();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                //appToolFactory.GetLog().Log($"Requête CreateEquipementSession [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjObservation> GetListeObservationsSession(string idSession, List<IObjTypeObservation> listeTypeObservation, List<IObjEquipement> listeEquipements)
        {
            List<IObjObservation> listObservations = new List<IObjObservation>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (listeEquipements == null || listeTypeObservation == null || string.IsNullOrEmpty(idSession))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM Observations WHERE IdSession=@IdSession";
                        sqlCommande.Parameters.AddWithValue("IdSession", idSession);
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                double? nbr_expoValue = null;
                                double nbr_expo;
                                if (!string.IsNullOrEmpty(reader["NBR_EXPO"].ToString()) && double.TryParse(reader["NBR_EXPO"].ToString(), out nbr_expo))
                                    nbr_expoValue = nbr_expo;
                                double? tps_expoValue = null;
                                double tps_expo;
                                if (!string.IsNullOrEmpty(reader["TPS_EXPO"].ToString()) && double.TryParse(reader["TPS_EXPO"].ToString(), out tps_expo))
                                    tps_expoValue = tps_expo;
                                double? gainValue = null;
                                double gain;
                                if (!string.IsNullOrEmpty(reader["GAIN"].ToString()) && double.TryParse(reader["GAIN"].ToString(), out gain))
                                    gainValue = gain;
                                double? temperatureValue = null;
                                double temperature;
                                if (!string.IsNullOrEmpty(reader["TEMP"].ToString()) && double.TryParse(reader["TEMP"].ToString(), out temperature))
                                    temperatureValue = temperature;
                                double? binningValue = null;
                                double binning;
                                if (!string.IsNullOrEmpty(reader["BINNING"].ToString()) && double.TryParse(reader["BINNING"].ToString(), out binning))
                                    binningValue = binning;
                                listObservations.Add(new ObjObservation(appToolFactory, listeTypeObservation, listeEquipements)
                                {
                                    Id = reader["Id"].ToString(),
                                    IdSession = reader["IdSession"].ToString(),
                                    IdTypeObservation = reader["IdTypeObservation"].ToString(),
                                    IdEquipement = reader["IdEquipement"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    DateLtnv = reader["Date"].ToString(),
                                    NBR_EXPO = nbr_expoValue,
                                    TPS_EXPO = tps_expoValue,
                                    GAIN = gainValue,
                                    TEMP = temperatureValue,
                                    BINNING = binningValue,
                                    Seeing = reader["Seeing"].ToString(),
                                    Lune = reader["Lune"].ToString(),
                                    Comment = reader["Comment"].ToString(),
                                    CommentDansExif = !string.IsNullOrEmpty(reader["CommentDansExif"].ToString()) && reader["CommentDansExif"].ToString() == "1"
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"GetListeObservationsSession effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listObservations;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjObservation> GetListeAllObservationsSession(List<IObjTypeObservation> listeTypeObservation, List<IObjEquipement> listeEquipements)
        {
            List<IObjObservation> listObservations = new List<IObjObservation>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (listeEquipements == null || listeTypeObservation == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM Observations";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                double? nbr_expoValue = null;
                                double nbr_expo;
                                if (!string.IsNullOrEmpty(reader["NBR_EXPO"].ToString()) && double.TryParse(reader["NBR_EXPO"].ToString(), out nbr_expo))
                                    nbr_expoValue = nbr_expo;
                                double? tps_expoValue = null;
                                double tps_expo;
                                if (!string.IsNullOrEmpty(reader["TPS_EXPO"].ToString()) && double.TryParse(reader["TPS_EXPO"].ToString(), out tps_expo))
                                    tps_expoValue = tps_expo;
                                double? gainValue = null;
                                double gain;
                                if (!string.IsNullOrEmpty(reader["GAIN"].ToString()) && double.TryParse(reader["GAIN"].ToString(), out gain))
                                    gainValue = gain;
                                double? temperatureValue = null;
                                double temperature;
                                if (!string.IsNullOrEmpty(reader["TEMP"].ToString()) && double.TryParse(reader["TEMP"].ToString(), out temperature))
                                    temperatureValue = temperature;
                                double? binningValue = null;
                                double binning;
                                if (!string.IsNullOrEmpty(reader["BINNING"].ToString()) && double.TryParse(reader["BINNING"].ToString(), out binning))
                                    binningValue = binning;
                                listObservations.Add(new ObjObservation(appToolFactory, listeTypeObservation, listeEquipements)
                                {
                                    Id = reader["Id"].ToString(),
                                    IdSession = reader["IdSession"].ToString(),
                                    IdTypeObservation = reader["IdTypeObservation"].ToString(),
                                    IdEquipement = reader["IdEquipement"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    DateLtnv = reader["Date"].ToString(),
                                    NBR_EXPO = nbr_expoValue,
                                    TPS_EXPO = tps_expoValue,
                                    GAIN = gainValue,
                                    TEMP = temperatureValue,
                                    BINNING = binningValue,
                                    Seeing = reader["Seeing"].ToString(),
                                    Lune = reader["Lune"].ToString(),
                                    Comment = reader["Comment"].ToString(),
                                    CommentDansExif = !string.IsNullOrEmpty(reader["CommentDansExif"].ToString()) && reader["CommentDansExif"].ToString() == "1"
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"GetListeAllObservationsSession effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listObservations;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteObservationsSession(IObjSession session)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (session == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM Observations WHERE IdSession=@IdSession";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("IdSession", session.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                //appToolFactory.GetLog().Log($"Requête DeleteObservationsSession [{session.Id}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateObservation(IObjObservation observation)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (observation == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertCommande = @"insert into Observations(IdSession, IdTypeObservation, IdEquipement, Description, Date,"
                                                    + @" TPS_EXPO, NBR_EXPO, GAIN, TEMP, BINNING, Seeing, Lune, Comment, CommentDansExif)"
                                                    + @" values(@IdSession, @IdTypeObservation, @IdEquipement, @Description, @Date,"
                                                    + @" @TPS_EXPO, @NBR_EXPO, @GAIN, @TEMP, @BINNING, @Seeing, @Lune, @Comment, @CommentDansExif); SELECT last_insert_rowid();";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("IdSession", observation.IdSession);
                        sqlCommande.Parameters.AddWithValue("IdTypeObservation", observation.IdTypeObservation);
                        sqlCommande.Parameters.AddWithValue("IdEquipement", !string.IsNullOrEmpty(observation.IdEquipement) ? observation.IdEquipement : null);
                        sqlCommande.Parameters.AddWithValue("Description", observation.Description);
                        sqlCommande.Parameters.AddWithValue("Date", observation.DateLtnv);
                        sqlCommande.Parameters.AddWithValue("TPS_EXPO", observation.TPS_EXPO.HasValue ? observation.TPS_EXPO.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("NBR_EXPO", observation.NBR_EXPO.HasValue ? observation.NBR_EXPO.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("GAIN", observation.GAIN.HasValue ? observation.GAIN.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("TEMP", observation.TEMP.HasValue ? observation.TEMP.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("BINNING", observation.BINNING.HasValue ? observation.BINNING.Value.ToString(CultureInfo.InvariantCulture) : null);
                        sqlCommande.Parameters.AddWithValue("Seeing", observation.Seeing);
                        sqlCommande.Parameters.AddWithValue("Lune", observation.Lune);
                        sqlCommande.Parameters.AddWithValue("Comment", observation.Comment);
                        sqlCommande.Parameters.AddWithValue("CommentDansExif", observation.CommentDansExif ? "1" : "0");
                        long rowId = (long)sqlCommande.ExecuteScalar();

                        observation.Id = rowId.ToString();

                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                //appToolFactory.GetLog().Log($"Requête CreateObservation [{observation.Id}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw new Exception("Erreur lors du traitement de la requête");
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateObservations(List<IObjObservation> listeObservations)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (listeObservations == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach(IObjObservation observation in listeObservations)
                        {
                            using (SQLiteCommand sqlCommande = connection.CreateCommand())
                            {
                                string insertCommande = @"insert into Observations(IdSession, IdTypeObservation, IdEquipement, Description, Date,"
                                                            + @" TPS_EXPO, NBR_EXPO, GAIN, TEMP, BINNING, Seeing, Lune, Comment, CommentDansExif)"
                                                            + @" values(@IdSession, @IdTypeObservation, @IdEquipement, @Description, @Date,"
                                                            + @" @TPS_EXPO, @NBR_EXPO, @GAIN, @TEMP, @BINNING, @Seeing, @Lune, @Comment, @CommentDansExif); SELECT last_insert_rowid();";
                                sqlCommande.CommandText = insertCommande;
                                sqlCommande.Parameters.AddWithValue("IdSession", observation.IdSession);
                                sqlCommande.Parameters.AddWithValue("IdTypeObservation", observation.IdTypeObservation);
                                sqlCommande.Parameters.AddWithValue("IdEquipement", !string.IsNullOrEmpty(observation.IdEquipement) ? observation.IdEquipement : null);
                                sqlCommande.Parameters.AddWithValue("Description", observation.Description);
                                sqlCommande.Parameters.AddWithValue("Date", observation.DateLtnv);
                                sqlCommande.Parameters.AddWithValue("TPS_EXPO", observation.TPS_EXPO.HasValue ? observation.TPS_EXPO.Value.ToString(CultureInfo.InvariantCulture) : null);
                                sqlCommande.Parameters.AddWithValue("NBR_EXPO", observation.NBR_EXPO.HasValue ? observation.NBR_EXPO.Value.ToString(CultureInfo.InvariantCulture) : null);
                                sqlCommande.Parameters.AddWithValue("GAIN", observation.GAIN.HasValue ? observation.GAIN.Value.ToString(CultureInfo.InvariantCulture) : null);
                                sqlCommande.Parameters.AddWithValue("TEMP", observation.TEMP.HasValue ? observation.TEMP.Value.ToString(CultureInfo.InvariantCulture) : null);
                                sqlCommande.Parameters.AddWithValue("BINNING", observation.BINNING.HasValue ? observation.BINNING.Value.ToString(CultureInfo.InvariantCulture) : null);
                                sqlCommande.Parameters.AddWithValue("Seeing", observation.Seeing);
                                sqlCommande.Parameters.AddWithValue("Lune", observation.Lune);
                                sqlCommande.Parameters.AddWithValue("Comment", observation.Comment);
                                sqlCommande.Parameters.AddWithValue("CommentDansExif", observation.CommentDansExif ? "1" : "0");
                                long rowId = (long)sqlCommande.ExecuteScalar();
                                observation.Id = rowId.ToString();
                                sqlCommande.Dispose();
                            }
                        }

                        // On commit la transaction
                        transaction.Commit();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                //appToolFactory.GetLog().Log($"Requête CreateObservation [{observation.Id}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw new Exception("Erreur lors du traitement de la requête");
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjTypeLogiciel> GetListeTypeLogiciels()
        {
            List<IObjTypeLogiciel> listType = new List<IObjTypeLogiciel>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM TypeLogiciels";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listType.Add(new ObjTypeLogiciel(appToolFactory)
                                {
                                    Id = reader["Id"].ToString(),
                                    Nom = reader["Nom"].ToString(),
                                    Icone = reader["Icone"].ToString(),
                                    Comment = reader["Comment"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetListeTypeLogiciels effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listType;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjLogiciel> GetListeLogiciels(List<IObjTypeLogiciel> listeTypeLogiciels)
        {
            List<IObjLogiciel> listType = new List<IObjLogiciel>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM Logiciels";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listType.Add(new ObjLogiciel(appToolFactory, listeTypeLogiciels)
                                {
                                    Id = reader["Id"].ToString(),
                                    IdTypeLogiciel = reader["IdType"].ToString(),
                                    Nom = reader["Nom"].ToString(),
                                    Thumbnail = reader["Thumbnail"].ToString(),
                                    Comment = reader["Comment"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetListeLogiciels effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listType;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateLogiciel(string nom, string idTypeLogiciel, string thumbnail, string comment)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(idTypeLogiciel))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertCommande = @"insert into Logiciels(Nom, IdType, Thumbnail, Comment)" +
                                                                        @" values(@Nom, @IdType, @Thumbnail, @Comment)";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("Nom", nom);
                        sqlCommande.Parameters.AddWithValue("IdType", idTypeLogiciel);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", thumbnail);
                        sqlCommande.Parameters.AddWithValue("Comment", comment);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête CreateLogiciel [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateLogiciel(IObjLogiciel logiciel)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (logiciel == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string updateCommande = @"update Logiciels set Nom = @Nom," +
                                                                        @" IdType = @IdType," +
                                                                        @" Thumbnail = @Thumbnail," +
                                                                        @" Comment = @Comment" +
                                                                        @" where Id = @Id";
                        sqlCommande.CommandText = updateCommande;
                        sqlCommande.Parameters.AddWithValue("Id", logiciel.Id);
                        sqlCommande.Parameters.AddWithValue("Nom", logiciel.Nom);
                        sqlCommande.Parameters.AddWithValue("IdType", logiciel.IdTypeLogiciel);
                        sqlCommande.Parameters.AddWithValue("Thumbnail", logiciel.Thumbnail);
                        sqlCommande.Parameters.AddWithValue("Comment", logiciel.Comment);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête UpdateLogiciel [{logiciel.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteLogiciel(IObjLogiciel logiciel)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (logiciel == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM Logiciels WHERE Id=@Id";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("Id", logiciel.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête DeleteLogiciel [{logiciel.Nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjLogicielSession> GetListeLogicielsSession(string idSession)
        {
            List<IObjLogicielSession> listLogicielsSession = new List<IObjLogicielSession>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(idSession))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM LogicielSession WHERE IdSession=@IdSession";
                        sqlCommande.Parameters.AddWithValue("IdSession", idSession);
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listLogicielsSession.Add(new ObjLogicielSession(appToolFactory)
                                {
                                    Id = reader["Id"].ToString(),
                                    IdLogiciel = reader["IdLogiciel"].ToString(),
                                    IdSession = idSession,
                                    Nom = reader["Nom"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"GetListeLogicielsSession effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listLogicielsSession;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjLogicielSession> GetListeAllLogicielsSession()
        {
            List<IObjLogicielSession> listLogicielsSession = new List<IObjLogicielSession>();
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM LogicielSession";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listLogicielsSession.Add(new ObjLogicielSession(appToolFactory)
                                {
                                    Id = reader["Id"].ToString(),
                                    IdLogiciel = reader["IdLogiciel"].ToString(),
                                    IdSession = reader["IdSession"].ToString(),
                                    Nom = reader["Nom"].ToString()
                                });
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"GetListeAllLogicielsSession effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return listLogicielsSession;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteLogicielsSession(IObjSession session)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (session == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string deleteCommande = @"DELETE FROM LogicielSession WHERE IdSession=@IdSession";
                        sqlCommande.CommandText = deleteCommande;
                        sqlCommande.Parameters.AddWithValue("IdSession", session.Id);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                //appToolFactory.GetLog().Log($"Requête DeleteLogicielsSession [{session.Id}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateLogicielSession(string nom, string idLogiciel, string idSession)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(idLogiciel) || string.IsNullOrEmpty(idSession))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertCommande = @"insert into LogicielSession(Nom, IdLogiciel, IdSession) values(@Nom, @IdLogiciel, @IdSession)";
                        sqlCommande.CommandText = insertCommande;
                        sqlCommande.Parameters.AddWithValue("Nom", nom);
                        sqlCommande.Parameters.AddWithValue("IdLogiciel", idLogiciel);
                        sqlCommande.Parameters.AddWithValue("IdSession", idSession);
                        sqlCommande.ExecuteNonQuery();

                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                //appToolFactory.GetLog().Log($"Requête CreateLogicielSession [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CreateLogicielsSession(List<IObjLogicielSession> listeLogicielsSesssion)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (listeLogicielsSesssion == null)
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach(IObjLogicielSession logicielSession in listeLogicielsSesssion)
                        {
                            using (SQLiteCommand sqlCommande = connection.CreateCommand())
                            {
                                string insertCommande = @"insert into LogicielSession(Nom, IdLogiciel, IdSession) values(@Nom, @IdLogiciel, @IdSession)";
                                sqlCommande.CommandText = insertCommande;
                                sqlCommande.Parameters.AddWithValue("Nom", logicielSession.Nom);
                                sqlCommande.Parameters.AddWithValue("IdLogiciel", logicielSession.IdLogiciel);
                                sqlCommande.Parameters.AddWithValue("IdSession", logicielSession.IdSession);
                                sqlCommande.ExecuteNonQuery();
                                sqlCommande.Dispose();
                            }
                        }

                        // On commit la transaction
                        transaction.Commit();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                //appToolFactory.GetLog().Log($"Requête CreateLogicielSession [{nom}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Version GetBDDVersion()
        {
            Version version = new Version("1.0.0.0");
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = (SQLiteCommand)connection.CreateCommand())
                    {
                        sqlCommande.CommandText = "SELECT * FROM Version";
                        using (SQLiteDataReader reader = sqlCommande.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Version versionLue;
                                string versionEnBdd = reader["Version"].ToString();
                                if (!string.IsNullOrEmpty(versionEnBdd) && Version.TryParse(versionEnBdd, out versionLue))
                                {
                                    version = versionLue;
                                }
                            }
                        }
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête GetBDDVersion effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
            return version;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void UpdateBDDVersion(string version, string nom, string comment)
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Verif des paramètres
                if (string.IsNullOrEmpty(version))
                    throw new Exception("Champs obligatoires manquants");

                using (SQLiteConnection connection = CreateConnection())
                {
                    using (SQLiteCommand sqlCommande = connection.CreateCommand())
                    using (var transaction = connection.BeginTransaction())
                    {
                        string updateCommande = @"update Version set Version = @Version," +
                                                                        @" Nom = @Nom," +
                                                                        @" Comment = @Comment" +
                                                                        @" where Id = @Id";
                        sqlCommande.CommandText = updateCommande;
                        sqlCommande.Parameters.AddWithValue("Id", "1");
                        sqlCommande.Parameters.AddWithValue("Version", version);
                        sqlCommande.Parameters.AddWithValue("Nom", nom);
                        sqlCommande.Parameters.AddWithValue("Comment", comment);
                        sqlCommande.ExecuteNonQuery();
                        // On commit la transaction
                        transaction.Commit();
                        sqlCommande.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }

                // Trace et retour
                appToolFactory.GetLog().Log($"Requête UpdateBDDVersion [{version}] effectué en {debutInitialisation.ElapsedMilliseconds} ms", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
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

        #endregion
    }
}
