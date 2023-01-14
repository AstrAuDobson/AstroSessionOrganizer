using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Setup
    /// </summary>
    internal class ObjSetup : IObjSetup
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjEquipementSetup> ListeEquipement
        {
            get
            {
                if (!string.IsNullOrEmpty(Id) && listeEquipementSetup == null)
                {
                    ChargementListeEquipement();
                }
                return listeEquipementSetup;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FormatedThumbnailPathName
        {
            get
            {
                return Path.Combine(appToolFactory.GetAppContext().UserProfilePath, "AstrAuDobson", "AstroSessionOrganizer", "data", $"setup_{Id}.jpg");
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjSetup(IAppToolFactory appToolFactory,
                            ISQLiteDatabase sQLiteDatabase,
                            List<IObjEquipement> listeEquipement,
                            List<IObjEquipementSetup> listeCompleteEquipementSetup)
        {
            this.appToolFactory = appToolFactory;
            this.sQLiteDatabase = sQLiteDatabase;
            this.listeEquipement = listeEquipement;
            this.listeCompleteEquipementSetup = listeCompleteEquipementSetup;

            // Positionnement des valeurs par défaut
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Charegement de la liste des équipements
        /// </summary>
        private void ChargementListeEquipement()
        {
            try
            {
                listeEquipementSetup = listeCompleteEquipementSetup.Where(es => es.IdSetup == Id).ToList();
                //listeEquipementSetup = sQLiteDatabase.GetListeEquipementsSetup(Id, listeEquipement);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                appToolFactory.GetLog().LogException(err, GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DeleteEquipements()
        {
            sQLiteDatabase.DeleteEquipementsSetup(this);
            listeEquipementSetup = null;
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
        /// Liste des équipements
        /// </summary>
        private readonly List<IObjEquipement> listeEquipement = null;

        /// <summary>
        /// Liste des Equipements du Setup
        /// </summary>
        private List<IObjEquipementSetup> listeEquipementSetup = null;

        /// <summary>
        /// Liste complète des <see cref="ObjEquipementSetup"/>
        /// </summary>
        private List<IObjEquipementSetup> listeCompleteEquipementSetup = null;

        #endregion
    }
}
