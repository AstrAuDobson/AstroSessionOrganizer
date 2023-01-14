using System.Collections.Generic;
using System.Linq;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Liste des Sessions d'observations
    /// </summary>
    internal class ObjSessionListe : IObjSessionListe
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjSession> Liste
        {
            get
            {
                // Liste complète
                List<IObjSession> listeRetour = liste;

                // Sélection TreeView
                if (!string.IsNullOrEmpty(SelectedConstellation))
                    listeRetour = liste.Where(o => o.ObjetCeleste.IdConstellation == SelectedConstellation).ToList();
                if (!string.IsNullOrEmpty(SelectedTypeObjet))
                    listeRetour = listeRetour.Where(o => o.ObjetCeleste.IdTypeObjet == SelectedTypeObjet).ToList();
                // Date
                if (!string.IsNullOrEmpty (SelectedDate) && SelectedDate.Length == 4)
                {
                    int annee;
                    if (int.TryParse(SelectedDate, out annee))
                        listeRetour = listeRetour.Where(o => o.DateHeure.Year == annee).ToList();
                }
                if (!string.IsNullOrEmpty(SelectedDate) && SelectedDate.Length == 6)
                {
                    int annee;
                    int mois;
                    if (int.TryParse(SelectedDate.Substring(0, 4), out annee) && int.TryParse(SelectedDate.Substring(4,2), out mois))
                        listeRetour = listeRetour.Where(o => o.DateHeure.Year == annee && o.DateHeure.Month == mois).ToList();
                }
                return listeRetour;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IObjSession> ListeComplete
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
        public string SelectedDate { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int NombreTotalSessions
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
        internal ObjSessionListe(IAppToolFactory appToolFactory, List<IObjSession> liste)
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
        public IObjSession GetSession(string idSession)
        {
            if (!string.IsNullOrEmpty(idSession))
            {
                return liste.Where(o => o.Id == idSession).FirstOrDefault();
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
        /// Liste des sessions d'observations
        /// </summary>
        private List<IObjSession> liste = null;

        #endregion
    }
}
