using System.Collections.Generic;
using System.Linq;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Liste des Objets céleste
    /// </summary>
    internal class ObjObjetCelesteListe : IObjObjetCelesteListe
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjObjetCeleste> Liste
        {
            get
            {
                // Liste complète
                List<IObjObjetCeleste> listeRetour = liste;

                // Sélection TreeView
                if (!string.IsNullOrEmpty(SelectedConstellation))
                    listeRetour = liste.Where(o => o.IdConstellation == SelectedConstellation).ToList();
                if (!string.IsNullOrEmpty(SelectedTypeObjet))
                    listeRetour = listeRetour.Where(o => o.IdTypeObjet == SelectedTypeObjet).ToList();

                // Filtre description
                if (!string.IsNullOrEmpty(FiltreNomDescription))
                    listeRetour = listeRetour.Where(o => o.Nom.Replace(" ", "").ToUpper().Contains(FiltreNomDescription.Replace(" ", "").ToUpper())
                                                        || (!string.IsNullOrEmpty(o.CompleteDenominations) && o.CompleteDenominations.ToUpper().Contains(FiltreNomDescription.ToUpper()))
                                                        || o.Constellation.Nom.Replace(" ", "").ToUpper().Contains(FiltreNomDescription.Replace(" ", "").ToUpper())).ToList();
                
                // Filtre Type
                if (!string.IsNullOrEmpty(FiltreIdType) && FiltreIdType != "-1")
                    listeRetour = listeRetour.Where(o => o.IdTypeObjet == FiltreIdType).ToList();
                //listeRetour = listeRetour.Where(o => o.TypeObjet.Nom.Replace(" ", "").ToUpper().Contains(FiltreIdType.Replace(" ", "").ToUpper())).ToList();

                // Filtre Catalogue
                if (!string.IsNullOrEmpty(FiltreIdCatalogue) && FiltreIdCatalogue != "-1")
                    listeRetour = listeRetour.Where(o => o.Catalogues.Where(c => c.Id == FiltreIdCatalogue).ToList().Count > 0).ToList();

                // Filtre Magnitude
                if (!string.IsNullOrEmpty(FiltreMagnitude) && FiltreMagnitude != "Tous")
                {
                    double magnitudeMax;
                    if (double.TryParse(FiltreMagnitude, out magnitudeMax))
                    {
                        listeRetour = listeRetour.Where(o => !o.MAG_VISUAL.HasValue || o.MAG_VISUAL < magnitudeMax).ToList();
                    }
                }
                return listeRetour;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjObjetCeleste> ListeComplete
        {
            get
            {
                return liste;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string SelectedConstellation { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string SelectedTypeObjet { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FiltreNomDescription { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FiltreIdType { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FiltreIdCatalogue { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FiltreMagnitude { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool FiltreHasSession { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int NombreObjetsRepertories
        {
            get
            {
                return liste.Count;
            }
        }


        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjObjetCelesteListe(IAppToolFactory appToolFactory, List<IObjObjetCeleste> liste)
        {
            this.appToolFactory = appToolFactory;
            this.liste = liste;

            // Positionnement des valeurs par défaut
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjObjetCeleste GetObjetCeleste(string nomObjet)
        {
            if (!string.IsNullOrEmpty(nomObjet))
            {
                return liste.Where(o => o.Nom == nomObjet).FirstOrDefault();
            }
            return null;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Liste des objets célestes
        /// </summary>
        private List<IObjObjetCeleste> liste = null;

        #endregion
    }
}
