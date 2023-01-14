using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ApplicationTools;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue d'ajout ou de modification d'un objet céleste
    /// </summary>
    public partial class dlgObjetCeleste : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgObjetCeleste(IAppObjFactory factory, IObjObjetCeleste objetCeleste = null)
        {
            InitializeComponent();
            this.factory = factory;
            this.objetCeleste = objetCeleste;

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
                InitialisationComboType();
                InitialisationComboConstellation();
                SetToolTips();

                // Chargement des données
                LoadObjetCeleste();

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
                // Sur erreur dans l'initialisation, on ferme le formulaire
                Close();
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
            // ToolTip Dénominations 
            toolTipInfosDenominations.ToolTipTitle = Resources.InformationsSurLesDenominations;
            string toolTipDenominations = Resources.VeuillezSeparerLesDenominationsParDes;
            toolTipInfosDenominations.SetToolTip(pictureBoxInfosDenominations, toolTipDenominations);

            // ToolTip RA 
            toolTipInfosRA.ToolTipTitle = Resources.InformationsSurLeRA;
            string toolTipRA = Resources.FormatAttenduxxhxxxxx;
            toolTipInfosRA.SetToolTip(pictureBoxInfosRA, toolTipRA);

            // ToolTip DEC 
            toolTipInfosDEC.ToolTipTitle = Resources.InformationsSurLeDEC;
            string toolTipDEC = Resources.FormatAttenduxxDxxxxxx;
            toolTipInfosDEC.SetToolTip(pictureBoxInfosDEC, toolTipDEC);

            // ToolTip Edit not available 
            toolTipInfosEditNotAvailable.ToolTipTitle = Resources.InformationsSurLEnregistrement;
            string toolTipEditNotAvailable = Resources.IlEstImpossibleDeModifierOuDeSupprimerUnObjetCelesteDuCatalogueDOrigine;
            toolTipInfosEditNotAvailable.SetToolTip(pictureBoxInfosEditNotAvailable, toolTipEditNotAvailable);

            // ToolTip GrandeurMax 
            toolTipInfosGrandeur.ToolTipTitle = Resources.InformationsSurLesDimensions;
            string toolTipGrandeur = Resources.DimensionExprimeeEnSecondesDArc;
            toolTipInfosGrandeur.SetToolTip(pictureBoxInfosGrandeurMax, toolTipGrandeur);
            toolTipInfosGrandeur.SetToolTip(pictureBoxInfosGrandeurMin, toolTipGrandeur);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxType
        /// </summary>
        private void InitialisationComboType()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxType", GetType().Name);

            // CLear de la liste
            comboBoxType.Items.Clear();

            // Rechargement depuis la liste chargée
            ComboBoxItems comboBoxItems = new ComboBoxItems();
            foreach (IObjTypeObjet typeEnCours in factory.GetListeTypeObjets())
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(typeEnCours.Nom, typeEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxType.DisplayMember = "Text";
            comboBoxType.ValueMember = "Value";
            comboBoxType.DataSource = comboBoxItems;

            // Positionnement par défaut
            comboBoxType.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxType.Items.Count} éléments et sélection de l'élément : {comboBoxType.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Mise à jour de l'icone en fonction du type d'objet sélectionné
        /// </summary>
        private void UpdateIconeTypeObjet()
        {
            try
            {
                // On commence par masquer tous les icones
                pictureBoxInfosTypeObjetNonDefini.Visible = false;
                pictureBoxInfosTypeObjetStar.Visible = false;
                pictureBoxInfosTypeObjetMultipleStars.Visible = false;
                pictureBoxInfosTypeObjetGalaxie.Visible = false;
                pictureBoxInfosTypeObjetNebuleuse.Visible = false;
                pictureBoxInfosTypeObjetCluster.Visible = false;
                pictureBoxInfosTypeObjetPlanete.Visible = false;

                IObjTypeObjet typeObjet = factory.GetListeTypeObjets().Where(to => to.Id == comboBoxType.SelectedValue.ToString()).FirstOrDefault();
                if (typeObjet != null)
                {
                    pictureBoxInfosTypeObjetNonDefini.Visible = typeObjet.Icone == "NonDefini";
                    pictureBoxInfosTypeObjetStar.Visible = typeObjet.Icone == "Star";
                    pictureBoxInfosTypeObjetMultipleStars.Visible = typeObjet.Icone == "MultipleStars";
                    pictureBoxInfosTypeObjetGalaxie.Visible = typeObjet.Icone == "Galaxie";
                    pictureBoxInfosTypeObjetNebuleuse.Visible = typeObjet.Icone == "Nebuleuse";
                    pictureBoxInfosTypeObjetCluster.Visible = typeObjet.Icone == "Cluster";
                    pictureBoxInfosTypeObjetPlanete.Visible = typeObjet.Icone == "Planete";
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Mise à jour de l'état des champ RA et DEC
        /// </summary>
        private void UpdateRADECConstState()
        {
            try
            {
                // On commence par masquer tous les icones
                textBoxRA.Enabled = false;
                textBoxDEC.Enabled = false;

                IObjTypeObjet typeObjet = factory.GetListeTypeObjets().Where(to => to.Id == comboBoxType.SelectedValue.ToString()).FirstOrDefault();
                if (typeObjet != null)
                {
                    textBoxRA.Enabled = typeObjet.Icone != "Planete";
                    textBoxDEC.Enabled = typeObjet.Icone != "Planete";
                    comboBoxConstellation.Enabled = typeObjet.Icone != "Planete";
                    if (typeObjet.Icone == "Planete")
                    {
                        comboBoxConstellation.SelectedValue = "89";
                    }
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxConstellation
        /// </summary>
        private void InitialisationComboConstellation()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxConstellation", GetType().Name);

            // CLear de la liste
            comboBoxConstellation.Items.Clear();

            // Rechargement depuis la liste chargée
            ComboBoxItems comboBoxItems = new ComboBoxItems();
            foreach (IObjConstellation constellationEnCours in factory.GetListeConstellations())
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(constellationEnCours.Nom, constellationEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxConstellation.DisplayMember = "Text";
            comboBoxConstellation.ValueMember = "Value";
            comboBoxConstellation.DataSource = comboBoxItems;

            // Positionnement par défaut
            comboBoxConstellation.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxConstellation.Items.Count} éléments et sélection de l'élément : {comboBoxConstellation.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Mise à jour de l'image en fonction de la constellation sélectionnée
        /// </summary>
        private void UpdateImageConstellation()
        {
            try
            {
                // Trace
                factory.GetLog().Log("Modification image Constellation", GetType().Name);

                // On commence par masquer l'image
                pictureBoxInfosImageConstellation.Visible = false;

                IObjConstellation constellation = factory.GetListeConstellations().Where(to => to.Id == comboBoxConstellation.SelectedValue.ToString()).FirstOrDefault();
                if (constellation != null)
                {
                    pictureBoxInfosImageConstellation.Visible = true;
                    string url = constellation.DisplayThumbnailPosition;
                    if (File.Exists(url))
                    {
                        pictureBoxInfosImageConstellation.Load(url);
                        pictureBoxInfosImageConstellation.Visible = true;
                    }
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Chargement d'un objet céleste en mode Edition
        /// </summary>
        private void LoadObjetCeleste()
        {
            pictureBoxInfosEditNotAvailable.Visible = false;
            if (objetCeleste != null)
            {
                comboBoxType.SelectedValue = objetCeleste.IdTypeObjet;
                comboBoxConstellation.SelectedValue = objetCeleste.IdConstellation;
                textBoxNom.Text = objetCeleste.Nom;
                textBoxDenominations.Text = objetCeleste.CompleteDenominations;
                textBoxRA.Text = objetCeleste.RA.FormatedString;
                textBoxDEC.Text = objetCeleste.DEC.FormatedString;
                textBoxGrandeurMax.Text = objetCeleste.SIZE_MAX.HasValue ? objetCeleste.SIZE_MAX.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxGrandeurMin.Text = objetCeleste.SIZE_MIN.HasValue ? objetCeleste.SIZE_MIN.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxMagVisuel.Text = objetCeleste.MAG_VISUAL.HasValue ? objetCeleste.MAG_VISUAL.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxMagPhoto.Text = objetCeleste.MAG_PHOTO.HasValue ? objetCeleste.MAG_PHOTO.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxRedshift.Text = objetCeleste.REDSHIFT.HasValue ? objetCeleste.REDSHIFT.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                textBoxDistance.Text = objetCeleste.DISTANCE_RS.HasValue ? objetCeleste.DISTANCE_RS.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                pictureBoxInfosEditNotAvailable.Visible = objetCeleste.CatalogueInitial;
                buttonOK.Enabled = !objetCeleste.CatalogueInitial;
                // Catalogues
                checkBoxCatalogueMessier.Checked = objetCeleste.Catalogues.Where(cat => cat.Code == "M").FirstOrDefault() != null;
                checkBoxCatalogueNGC.Checked = objetCeleste.Catalogues.Where(cat => cat.Code == "NGC").FirstOrDefault() != null;
                checkBoxCatalogueIC.Checked = objetCeleste.Catalogues.Where(cat => cat.Code == "IC").FirstOrDefault() != null;
                checkBoxCatalogueCaldwell.Checked = objetCeleste.Catalogues.Where(cat => cat.Code == "C").FirstOrDefault() != null;
                checkBoxCatalogueSH2.Checked = objetCeleste.Catalogues.Where(cat => cat.Code == "SH2").FirstOrDefault() != null;
            }
        }

        /// <summary>
        /// Sauvegarde d'un Objet Celeste
        /// </summary>
        private void SaveObjetCeleste()
        {
            try
            {
                // Vérif des Inputs
                // Nom
                if (string.IsNullOrEmpty(textBoxNom.Text))
                {
                    MessageBox.Show($"{Resources.ChampObligatoire} : {Resources.Nom}"
                                    , Application.ProductName
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Warning);
                    return;
                }
                // Type
                string idType = string.Empty;
                if (comboBoxType.Items.Count > 0 && comboBoxType.SelectedValue != null)
                {
                    idType = comboBoxType.SelectedValue.ToString();
                }
                if (string.IsNullOrEmpty(idType))
                {
                    MessageBox.Show($"{Resources.ChampObligatoire} : {Resources.Type}"
                                    , Application.ProductName
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Warning);
                    return;
                }
                // Constellation
                string idConstellation = string.Empty;
                if (comboBoxConstellation.Items.Count > 0 && comboBoxConstellation.SelectedValue != null)
                {
                    idConstellation = comboBoxConstellation.SelectedValue.ToString();
                }
                if (string.IsNullOrEmpty(idConstellation))
                {
                    MessageBox.Show($"{Resources.ChampObligatoire} : {Resources.Constellation}"
                                    , Application.ProductName
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Warning);
                    return;
                }
                Coordinate coordinateDEC = factory.GetCoordinate(0, CoordinatesType.DEC);
                Coordinate coordinateRA = factory.GetCoordinate(0, CoordinatesType.RA);
                IObjTypeObjet typeObjet = factory.GetListeTypeObjets().Where(to => to.Id == comboBoxType.SelectedValue.ToString()).FirstOrDefault();
                if (typeObjet != null && typeObjet.Icone != "Planete")
                {
                    // RA
                    if (string.IsNullOrEmpty(textBoxRA.Text) || !Coordinate.TryParseFromFormatedString(textBoxRA.Text, ref coordinateRA))
                    {
                        MessageBox.Show(Resources.FormatDuChampRAIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    // DEC
                    if (string.IsNullOrEmpty(textBoxDEC.Text) || !Coordinate.TryParseFromFormatedString(textBoxDEC.Text.Replace(",", ".").Replace(" ", ""), ref coordinateDEC))
                    {
                        MessageBox.Show(Resources.FormatDuChampDECIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                }
                // SIZE_MAX
                double? size_maxValue = null;
                double size_max = 0;
                if (!string.IsNullOrEmpty(textBoxGrandeurMax.Text))
                {
                    if (!double.TryParse(textBoxGrandeurMax.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out size_max))
                    {
                        MessageBox.Show(Resources.FormatDuChampGrandeurMaxIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    size_maxValue = size_max;
                }
                // SIZE_MIN
                double? size_minValue = null;
                double size_min = 0;
                if (!string.IsNullOrEmpty(textBoxGrandeurMin.Text))
                {
                    if (!double.TryParse(textBoxGrandeurMin.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out size_min))
                    {
                        MessageBox.Show(Resources.FormatDuChampGrandeurMinIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    size_minValue = size_min;
                }
                // MAG_VISUAL
                double? mag_visualValue = null;
                double mag_visual = 0;
                if (!string.IsNullOrEmpty(textBoxMagVisuel.Text))
                {
                    if (!double.TryParse(textBoxMagVisuel.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out mag_visual))
                    {
                        MessageBox.Show(Resources.FormatDuChampMagnitudeVisuelIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                     mag_visualValue = mag_visual;
               }
                // MAG_PHOTO
                double? mag_photoValue = null;
                double mag_photo = 0;
                if (!string.IsNullOrEmpty(textBoxMagPhoto.Text))
                {
                    if (!double.TryParse(textBoxMagPhoto.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out mag_photo))
                    {
                        MessageBox.Show(Resources.FormatDuChampMagnitudePhotoIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    mag_photoValue = mag_photo;
                }
                // REDSHIFT
                double? redshiftValue = null;
                double redshift = 0;
                if (!string.IsNullOrEmpty(textBoxRedshift.Text))
                {
                    if (!double.TryParse(textBoxRedshift.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out redshift))
                    {
                        MessageBox.Show(Resources.FormatDuChampRedshiftIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    redshiftValue = redshift;
                }
                // DISTANCE
                double? distanceValue = null;
                double distance = 0;
                if (!string.IsNullOrEmpty(textBoxDistance.Text))
                {
                    if (!double.TryParse(textBoxDistance.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out distance))
                    {
                        MessageBox.Show(Resources.FormatDuChampDistanceIncorrect
                                        , Application.ProductName
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Warning);
                        return;
                    }
                    distanceValue = distance;
                }
                // Catalogue
                string catalogues = string.Empty;
                catalogues += checkBoxCatalogueMessier.Checked ? "M;" : string.Empty;
                catalogues += checkBoxCatalogueNGC.Checked ? "NGC;" : string.Empty;
                catalogues += checkBoxCatalogueIC.Checked ? "IC;" : string.Empty;
                catalogues += checkBoxCatalogueSH2.Checked ? "SH2;" : string.Empty;
                catalogues += checkBoxCatalogueCaldwell.Checked ? "C;" : string.Empty;
                if (!string.IsNullOrEmpty(catalogues))
                    catalogues = catalogues.Trim(';');

                // Création
                if (objetCeleste == null)
                {
                    factory.CreateObjetCeleste(textBoxNom.Text, string.Empty, textBoxDenominations.Text, idType, idConstellation,
                                                coordinateRA, coordinateDEC, size_maxValue, size_minValue, mag_visualValue, mag_photoValue,
                                                redshiftValue, distanceValue, null, catalogues, string.Empty, string.Empty, string.Empty, "0", string.Empty);
                }
                // Modification
                else
                {
                    objetCeleste.Nom = textBoxNom.Text;
                    objetCeleste.Description = string.Empty;
                    objetCeleste.CompleteDenominations = textBoxDenominations.Text;
                    objetCeleste.IdTypeObjet = idType;
                    objetCeleste.IdConstellation = idConstellation;
                    objetCeleste.RA = coordinateRA;
                    objetCeleste.DEC = coordinateDEC;
                    objetCeleste.SIZE_MAX = size_maxValue;
                    objetCeleste.SIZE_MIN = size_minValue;
                    objetCeleste.MAG_VISUAL = mag_visualValue;
                    objetCeleste.MAG_PHOTO = mag_photoValue;
                    objetCeleste.REDSHIFT = redshiftValue;
                    objetCeleste.DISTANCE_RS = distanceValue;
                    objetCeleste.CompleteCatalogues = catalogues;

                    factory.UpdateObjetCeleste(objetCeleste);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception err)
            {
                // Trace de l'erreur et informations utilisateur
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
        /// Objet IObjObjetCeleste en mode Edition
        /// </summary>
        private IObjObjetCeleste objetCeleste = null;

        #endregion

        private void dlgObjetCeleste_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateIconeTypeObjet();
            UpdateRADECConstState();
        }

        private void comboBoxConstellation_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateImageConstellation();
        }

        private void pictureBoxInfosImageConstellation_Click(object sender, EventArgs e)
        {
            if (pictureBoxInfosImageConstellation.Image != null && !string.IsNullOrEmpty(pictureBoxInfosImageConstellation.ImageLocation))
                Process.Start(pictureBoxInfosImageConstellation.ImageLocation);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveObjetCeleste();
        }
    }
}
