using System.Collections.Generic;
using System.Linq;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Logiciel
    /// </summary>
    internal class ObjLogiciel : IObjLogiciel
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdTypeLogiciel { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjTypeLogiciel TypeLogiciel
        {
            get
            {
                return listeTypeLogiciels.Where(t => t.Id == IdTypeLogiciel).FirstOrDefault();
            }
        }

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

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjLogiciel(IAppToolFactory appToolFactory, List<IObjTypeLogiciel> listeTypeLogiciels)
        {
            this.appToolFactory = appToolFactory;
            this.listeTypeLogiciels = listeTypeLogiciels;

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
        /// Liste des Type de Logiciels
        /// </summary>
        private List<IObjTypeLogiciel> listeTypeLogiciels = null;

        #endregion
    }
}
