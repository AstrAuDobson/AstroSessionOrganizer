using System;
using System.Windows.Forms;
using ApplicationTools;
using System.Diagnostics;
using System.Globalization;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue d'ajout ou de modification d'un site d'observation
    /// </summary>
    public partial class dlgSite : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgSite(IAppObjFactory factory, IObjSite site = null)
        {
            InitializeComponent();
            this.factory = factory;
            this.site = site;

            // Initialisation des objets
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Initialisation du formulaire
        /// </summary>
        private void InitialisationFormulaire()
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Trace
                factory.GetLog().Log("Fonction InitialisationFormulaire DEBUT", GetType().Name);

                // Initialisation des composants du formulaire
                InitCombos();

                // Chargement des données
                LoadSite();

                // Trace
                factory.GetLog().Log("Fonction InitialisationFormulaire FIN", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Initialisations des Combos
        /// </summary>
        private void InitCombos()
        {
            // Trace
            factory.GetLog().Log($"Initialisation des Combos", GetType().Name);

            // Longitude
            comboBoxLongitudeDirection.Items.Clear();
            comboBoxLongitudeDirection.Items.Add(Coordinate.GetDirectionString(CoordinatesDirection.E));
            comboBoxLongitudeDirection.Items.Add(Coordinate.GetDirectionString(CoordinatesDirection.O));
            comboBoxLongitudeDirection.SelectedIndex = 0;

            // Latitude
            comboBoxLatitudeDirection.Items.Clear();
            comboBoxLatitudeDirection.Items.Add(Coordinate.GetDirectionString(CoordinatesDirection.N));
            comboBoxLatitudeDirection.Items.Add(Coordinate.GetDirectionString(CoordinatesDirection.S));
            comboBoxLatitudeDirection.SelectedIndex = 0;
        }

        /// <summary>
        /// Chargement d'un site en mode Edition
        /// </summary>
        private void LoadSite()
        {
            if (site != null)
            {
                textBoxNom.Text = site.Nom;

                if (site.Coordonnee != null)
                {
                    // Lieu d'observation : Longitude
                    textBoxLongitudeDegre.Text = site.Coordonnee.CoordonneeLongitude.Degrees.ToString("0", CultureInfo.InvariantCulture);
                    textBoxLongitudeMinute.Text = site.Coordonnee.CoordonneeLongitude.Minutes.ToString("0", CultureInfo.InvariantCulture);
                    textBoxLongitudeSeconde.Text = site.Coordonnee.CoordonneeLongitude.Seconds.ToString("0.00", CultureInfo.InvariantCulture);
                    comboBoxLongitudeDirection.SelectedItem = site.Coordonnee.CoordonneeLongitude.Direction;

                    // Lieu d'observation : Longitude
                    textBoxLatitudeDegre.Text = site.Coordonnee.CoordonneeLatitude.Degrees.ToString("0", CultureInfo.InvariantCulture);
                    textBoxLatitudeMinute.Text = site.Coordonnee.CoordonneeLatitude.Minutes.ToString("0", CultureInfo.InvariantCulture);
                    textBoxLatitudeSeconde.Text = site.Coordonnee.CoordonneeLatitude.Seconds.ToString("0.00", CultureInfo.InvariantCulture);
                    comboBoxLatitudeDirection.SelectedItem = site.Coordonnee.CoordonneeLatitude.Direction;

                    // Lieu d'observation : indiceBortle
                    textBoxIndiceBortle.Text = site.IndiceBortle.HasValue ? site.IndiceBortle.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                }
            }
        }

        /// <summary>
        /// Enregistrement du site
        /// </summary>
        private void SaveSite()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Enregistrement du site", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // On vérifie d'abord la validité de tous les champs
                // Lieu d'observation
                Coordinates nouveauLieu = factory.GetCoordinates(0, 0);
                if (!Coordinates.TryParse(textBoxLongitudeDegre.Text, textBoxLongitudeMinute.Text, textBoxLongitudeSeconde.Text, comboBoxLongitudeDirection.Text,
                                        textBoxLatitudeDegre.Text, textBoxLatitudeMinute.Text, textBoxLatitudeSeconde.Text, comboBoxLatitudeDirection.Text,
                                        ref nouveauLieu, factory.GetLog()))
                    throw new WarningException(Resources.FormatDuLieuIncorrect);

                // Indice Bortle
                double? indiceBortleValue = null;
                double indiceBortle = 0;
                if (!string.IsNullOrEmpty(textBoxIndiceBortle.Text))
                {
                    if (!double.TryParse(textBoxIndiceBortle.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out indiceBortle))
                    {
                        throw new WarningException(Resources.FormatDuChampIndiceBortleIncorrect);
                    }
                    indiceBortleValue = indiceBortle;
                }
                
                // Mode création
                if (site == null)
                {
                    site = factory.CreateSite(textBoxNom.Text, nouveauLieu, string.Empty, string.Empty, string.Empty, string.Empty, indiceBortleValue);
                }
                // Mode Edition
                else
                {
                    site.Nom = textBoxNom.Text;
                    site.Longitude = nouveauLieu.LongitudeValue;
                    site.Latitude = nouveauLieu.LatitudeValue;
                    site.IndiceBortle = indiceBortle;
                    factory.UpdateSite(site);
                }

                // Trace
                factory.GetLog().Log($"Enregistrement du site {site.Nom} effectué avec succès en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);

                // Fermeture de la Dialogue
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (WarningException err)
            {
                // Trace de l'erreur et information Warning à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name, TypeLog.Warning);
                MessageBox.Show(err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Warning);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name, TypeLog.Fatal);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        /// <summary>
        /// Objet Site en mode Edition
        /// </summary>
        private IObjSite site = null;

        #endregion

        private void dlgSite_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSite();
        }
    }
}
