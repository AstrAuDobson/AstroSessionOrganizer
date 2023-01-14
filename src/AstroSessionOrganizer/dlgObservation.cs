using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ApplicationTools;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue d'edition d'une observation
    /// </summary>
    public partial class dlgObservation : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgObservation(IAppObjFactory factory,
            List<IObjObservation> listeObservationsSession,
            DateTime sessionDate,
            IObjObservation observation = null)
        {
            InitializeComponent();
            this.factory = factory;
            this.listeObservationsSession = listeObservationsSession;
            this.observation = observation;

            // Initialisation des objets
            dateTimePickerDateObservation.Value = sessionDate;
            dateTimePickerHeureObservation.Value = new DateTime(sessionDate.Year, sessionDate.Month, sessionDate.Day, 22, 00, 00);
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
                SetToolTips();
                InitialisationComboTypeObservation();
                InitialisationComboFiltre();

                // Chargement des données
                LoadObservation();

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
        /// Positionne les ToolTip
        /// </summary>
        private void SetToolTips()
        {
            // ToolTip Comment 
            toolTipComment.ToolTipTitle = Resources.InformationsSurLesCommentaires;
            string toolTipInfoComment = Resources.VousPouvezAjouterUnCommentaireSpecifiqueAVotreObservation
                                + Environment.NewLine + Resources.ParExempleDesInformationsSurLEvolutionDeLaMeteoLeVent;
            toolTipComment.SetToolTip(pictureBoxInfosComment, toolTipInfoComment);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxTypeObservation
        /// </summary>
        private void InitialisationComboTypeObservation()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxTypeObservation", GetType().Name);

            // CLear de la liste
            comboBoxTypeObservation.Items.Clear();
            ComboBoxItems comboBoxItems = new ComboBoxItems();

            // Rechargement depuis la liste chargée
            foreach (IObjTypeObservation typeEnCours in factory.GetListeTypeObservation())
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(typeEnCours.Nom, typeEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxTypeObservation.DisplayMember = "Text";
            comboBoxTypeObservation.ValueMember = "Value";
            comboBoxTypeObservation.DataSource = comboBoxItems;

            // Positionnement par défaut
            comboBoxTypeObservation.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxTypeObservation.Items.Count} éléments et sélection de l'élément : {comboBoxTypeObservation.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxFiltre
        /// </summary>
        private void InitialisationComboFiltre()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxFiltre", GetType().Name);

            // CLear de la liste
            comboBoxFiltre.Items.Clear();
            ComboBoxItems comboBoxItems = new ComboBoxItems();

            // Ajout de l'élément "Aucun"
            ComboBoxItem newItemNone = comboBoxItems.NewItem(Resources.Aucun, "-1");
            comboBoxItems.Rows.Add(newItemNone);

            // Rechargement depuis la liste chargée
            foreach (IObjEquipement equipementEnCours in factory.GetListeEquipements().Where(eq => eq.TypeEquipement.Id == "4").OrderBy(eq => eq.Nom))
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(equipementEnCours.Nom, equipementEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxFiltre.DisplayMember = "Text";
            comboBoxFiltre.ValueMember = "Value";
            comboBoxFiltre.DataSource = comboBoxItems;

            // Positionnement par défaut
            comboBoxFiltre.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxFiltre.Items.Count} éléments et sélection de l'élément : {comboBoxFiltre.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Recalcul le temps total d'expositions
        /// </summary>
        private void UpdateTempsTotal()
        {
            try
            {
                // Clear de la zone de texte
                textBoxTempsTotal.Text = string.Empty;

                // On récupère le nombre d'exposition
                int nbrExpositions;
                if (string.IsNullOrEmpty(textBoxNbrExpositions.Text) || !int.TryParse(textBoxNbrExpositions.Text, out nbrExpositions))
                    throw new WarningException(Resources.FormatNombreDExpositionsIncorrect);

                // On récupère le temps d'exposition
                int tempsExpositions;
                if (string.IsNullOrEmpty(textBoxTempsUnitaire.Text) || !int.TryParse(textBoxTempsUnitaire.Text, out tempsExpositions))
                    throw new WarningException(Resources.FormatTempsDExpositionsIncorrect);

                TimeSpan spanExposition = TimeSpan.FromSeconds(nbrExpositions * tempsExpositions);
                textBoxTempsTotal.Text = spanExposition.ToStandardFormatString();
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
                textBoxTempsTotal.Text = string.Empty;
            }
        }

        /// <summary>
        /// Chargement d'une observation
        /// </summary>
        private void LoadObservation()
        {
            if (observation != null)
            {
                dateTimePickerDateObservation.Value = observation.DateHeure;
                dateTimePickerHeureObservation.Value = observation.DateHeure;
                comboBoxTypeObservation.SelectedValue = observation.IdTypeObservation;
                if (!string.IsNullOrEmpty(observation.IdEquipement))
                    comboBoxFiltre.SelectedValue = observation.IdEquipement;
                textBoxNbrExpositions.Text = observation.NBR_EXPO.HasValue ? observation.NBR_EXPO.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxTempsUnitaire.Text = observation.TPS_EXPO.HasValue ? observation.TPS_EXPO.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxGain.Text = observation.GAIN.HasValue ? observation.GAIN.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxTemperature.Text = observation.TEMP.HasValue ? observation.TEMP.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxBinning.Text = observation.BINNING.HasValue ? observation.BINNING.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxSeeing.Text = observation.Seeing;
                textBoxLune.Text = observation.Lune;
                textBoxComment.Text = observation.Comment;
                checkBoxComment.Checked = observation.CommentDansExif;
            }
        }

        /// <summary>
        /// Sauvegarde d'une observation dans la liste
        /// </summary>
        private void SaveObservation()
        {
            try
            {
                // Vérification des paramètres
                // DateHeure
                DateTime dateHeureObservation = new DateTime(dateTimePickerDateObservation.Value.Year,
                                                dateTimePickerDateObservation.Value.Month,
                                                dateTimePickerDateObservation.Value.Day,
                                                dateTimePickerHeureObservation.Value.Hour,
                                                dateTimePickerHeureObservation.Value.Minute,
                                                0);
                // Type d'observation et Filtre
                string idTypeObservation = comboBoxTypeObservation.SelectedValue.ToString();
                string idFiltre = !string.IsNullOrEmpty(comboBoxFiltre.SelectedValue.ToString()) ?
                                                comboBoxFiltre.SelectedValue.ToString() != "-1" ? comboBoxFiltre.SelectedValue.ToString() : string.Empty
                                            : string.Empty;
                // NBR_EXPO
                double? nbr_expoValue = null;
                double nbr_expo = 0;
                if (!string.IsNullOrEmpty(textBoxNbrExpositions.Text))
                {
                    if (!double.TryParse(textBoxNbrExpositions.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out nbr_expo))
                    {
                        MessageBox.Show(Resources.FormatDuChampNombreDExpositionsIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    nbr_expoValue = nbr_expo;
                }
                // TPS_EXPO
                double? tps_expoValue = null;
                double tps_expo = 0;
                if (!string.IsNullOrEmpty(textBoxTempsUnitaire.Text))
                {
                    if (!double.TryParse(textBoxTempsUnitaire.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out tps_expo))
                    {
                        MessageBox.Show(Resources.FormatDuChampTempsUnitaireIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    tps_expoValue = tps_expo;
                }
                // GAIN
                double? gainValue = null;
                double gain = 0;
                if (!string.IsNullOrEmpty(textBoxGain.Text))
                {
                    if (!double.TryParse(textBoxGain.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out gain))
                    {
                        MessageBox.Show(Resources.FormatDuChampGainIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    gainValue = gain;
                }
                // Température
                double? temperatureValue = null;
                double temperature = 0;
                if (!string.IsNullOrEmpty(textBoxTemperature.Text))
                {
                    if (!double.TryParse(textBoxTemperature.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out temperature))
                    {
                        MessageBox.Show(Resources.FormatDuChampTemperatureIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    temperatureValue = temperature;
                }
                // Binning
                double? binningValue = null;
                double binning = 0;
                if (!string.IsNullOrEmpty(textBoxBinning.Text))
                {
                    if (!double.TryParse(textBoxBinning.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out binning))
                    {
                        MessageBox.Show(Resources.FormatDuChampBinningIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    binningValue = binning;
                }


                // Mode Création
                if (observation == null && listeObservationsSession != null)
                {
                    observation = factory.GetNewObservation();
                    listeObservationsSession.Add(observation);
                }
                observation.DateLtnv = dateHeureObservation.ToString("yyyyMMddHHmmss");
                observation.IdTypeObservation = idTypeObservation;
                observation.IdEquipement = idFiltre;
                observation.NBR_EXPO = nbr_expoValue;
                observation.TPS_EXPO = tps_expoValue;
                observation.GAIN = gainValue;
                observation.TEMP = temperatureValue;
                observation.BINNING = binningValue;
                observation.Seeing = textBoxSeeing.Text;
                observation.Lune = textBoxLune.Text;
                observation.Comment = textBoxComment.Text;
                observation.CommentDansExif = checkBoxComment.Checked;

                // On ferme la boîte de dialogue
                DialogResult = DialogResult.OK;
                Close();
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
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        /// <summary>
        /// Liste des observations de la session
        /// </summary>
        private List<IObjObservation> listeObservationsSession = null;

        /// <summary>
        /// Singleton interne
        /// </summary>
        private IObjObservation observation = null;

        #endregion

        private void dlgObservation_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void textBoxNbrExpositions_TextChanged(object sender, EventArgs e)
        {
            UpdateTempsTotal();
        }

        private void textBoxTempsUnitaire_TextChanged(object sender, EventArgs e)
        {
            UpdateTempsTotal();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveObservation();
        }
    }
}
