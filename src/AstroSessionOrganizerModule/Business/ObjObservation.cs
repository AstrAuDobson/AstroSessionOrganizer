using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationTools;

namespace AstroSessionOrganizerModule
{
    /// <summary>
    /// Observation d'une Session d'observations
    /// </summary>
    internal class ObjObservation : IObjObservation
    {
        #region Propriétés

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdSession { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdTypeObservation { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjTypeObservation TypeObservation
        {
            get
            {
                if (!string.IsNullOrEmpty(IdTypeObservation))
                {
                    return listeTypeObservation.Where(o => o.Id == IdTypeObservation).FirstOrDefault();
                }
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string IdEquipement { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IObjEquipement Equipement
        {
            get
            {
                if (!string.IsNullOrEmpty(IdEquipement))
                {
                    return listeEquipements.Where(o => o.Id == IdEquipement).FirstOrDefault();
                }
                return null;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string LibelleEquipement
        {
            get
            {
                if (Equipement != null)
                {
                    return Equipement.Nom;
                }
                return string.Empty;
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
        public double? NBR_EXPO { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? TPS_EXPO { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? GAIN { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? TEMP { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double? BINNING { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Seeing { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Lune { get; set; }

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
        public TimeSpan TempsTotalExposition
        {
            get
            {
                try
                {
                    // Vérif des inputs
                    if (NBR_EXPO.HasValue && TPS_EXPO.HasValue)
                    {
                        //if (IdTypeObservation == "1")
                            return TimeSpan.FromSeconds(NBR_EXPO.Value * TPS_EXPO.Value);
                    }

                    return TimeSpan.Zero;
                }
                catch (Exception err)
                {
                    // Trace de l'erreur
                    appToolFactory.GetLog().LogException(err, GetType().Name);
                }
                return TimeSpan.Zero;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal ObjObservation(IAppToolFactory appToolFactory, List<IObjTypeObservation> listeTypeObservation, List<IObjEquipement> listeEquipements)
        {
            this.appToolFactory = appToolFactory;
            this.listeTypeObservation = listeTypeObservation;
            this.listeEquipements = listeEquipements;

            // Positionnement des valeurs par défaut
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet technique
        /// </summary>
        private readonly IAppToolFactory appToolFactory = null;

        /// <summary>
        /// Liste complète des types d'observations
        /// </summary>
        List<IObjTypeObservation> listeTypeObservation = null;

        /// <summary>
        /// Liste complète des équipements
        /// </summary>
        List<IObjEquipement> listeEquipements = null;

        #endregion
    }
}
