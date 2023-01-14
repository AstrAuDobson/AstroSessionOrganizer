using System.Collections.Generic;
using System.Linq;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Objet céleste
    /// </summary>
    internal class ObjObjetCeleste : IObjObjetCeleste
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
        public string Description { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdTypeObjet { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjTypeObjet TypeObjet
        {
            get
            {
                if (!string.IsNullOrEmpty(IdTypeObjet))
                {
                    return listeTypeObjet.Where(t => t.Id == IdTypeObjet).FirstOrDefault();
                }
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdConstellation { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjConstellation Constellation
        {
            get
            {
                if (!string.IsNullOrEmpty(IdConstellation))
                {
                    return listeConstellation.Where(c => c.Id == IdConstellation).FirstOrDefault();
                }
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string CompleteDenominations { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<string> Denominations
        {
            get
            {
                if (denominations == null && !string.IsNullOrEmpty(CompleteDenominations))
                {
                    denominations = CompleteDenominations.Split(';').ToList();
                }
                if (denominations == null)
                    denominations = new List<string>();
                return denominations;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string DenominationsFormated
        { 
            get
            {
                string retour = string.Empty;
                if (Denominations != null)
                {
                    foreach (string denominationEnCours in Denominations)
                    {
                        retour += denominationEnCours;
                        if (denominationEnCours != Denominations.Last())
                            retour += " / ";
                    }
                }
                return retour;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string CompleteCatalogues { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjCatalogue> Catalogues
        {
            get
            {
                if (catalogues == null && !string.IsNullOrEmpty(CompleteCatalogues))
                {
                    catalogues = new List<IObjCatalogue>();
                    foreach(string codeCatalogueEnCours in CompleteCatalogues.Split(';').ToList())
                    {
                        catalogues.Add(listeCatalogue.Where(c => c.Code == codeCatalogueEnCours).FirstOrDefault());
                    }
                }
                if (catalogues == null)
                    catalogues = new List<IObjCatalogue>();
                return catalogues;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string CataloguesFormated
        {
            get
            {
                string retour = string.Empty;
                if (Catalogues != null)
                {
                    foreach (IObjCatalogue catalogueEnCours in Catalogues)
                    {
                        if (catalogueEnCours.Code == "M")
                            retour += catalogueEnCours.Nom;
                        else
                            retour += catalogueEnCours.Code;
                        if (catalogueEnCours != Catalogues.Last())
                            retour += " / ";
                    }
                }
                return retour;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate RA { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Coordinate DEC { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? SIZE_MAX { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string GrandeurMaxFormated
        {
            get
            {
                string retour = string.Empty;
                if (SIZE_MAX.HasValue)
                {
                    retour = appToolFactory.GetCoordinate(SIZE_MAX.Value / 60, CoordinatesType.Degree).FormatedString;
                }
                return retour;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? SIZE_MIN { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string GrandeurMinFormated
        {
            get
            {
                string retour = string.Empty;
                if (SIZE_MIN.HasValue)
                {
                    retour = appToolFactory.GetCoordinate(SIZE_MIN.Value / 60, CoordinatesType.Degree).FormatedString;
                }
                return retour;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? MAG_VISUAL { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? MAG_PHOTO { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? REDSHIFT { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? DISTANCE_RS { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? DISTANCE_M { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ThumbnailPosition { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string URLWiki { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Icone
        {
            get
            {
                return TypeObjet.Icone;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int IconeIndex
        {
            get
            {
                switch(TypeObjet.Icone)
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
        public string Origin { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool CatalogueInitial
        {
            get
            {
                return Origin == "1";
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Comment { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjObjetCeleste(IAppToolFactory appToolFactory, List<IObjTypeObjet> listeTypeObjet, List<IObjConstellation> listeConstellation, List<IObjCatalogue> listeCatalogue)
        {
            this.appToolFactory = appToolFactory;
            this.listeTypeObjet = listeTypeObjet;
            this.listeConstellation = listeConstellation;
            this.listeCatalogue = listeCatalogue;

            // Positionnement des valeurs par défaut
        }

        #endregion

        #region Méthodes

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Instance de la liste globale des types d'objets
        /// </summary>
        private List<IObjTypeObjet> listeTypeObjet = null;

        /// <summary>
        /// Instance de la liste globale des constellations
        /// </summary>
        private List<IObjConstellation> listeConstellation = null;

        /// <summary>
        /// Instance de la liste globale des Catalogues
        /// </summary>
        private List<IObjCatalogue> listeCatalogue = null;

        /// <summary>
        /// Liste des dénominations
        /// </summary>
        private List<string> denominations = null;

        /// <summary>
        /// Liste des catalogues
        /// </summary>
        private List<IObjCatalogue> catalogues = null;

        #endregion
    }
}
