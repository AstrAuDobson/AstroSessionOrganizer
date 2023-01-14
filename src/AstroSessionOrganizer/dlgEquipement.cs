using System;
using System.Diagnostics;
using System.Windows.Forms;
using ApplicationTools;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue d'ajout ou de modification d'un équipement
    /// </summary>
    public partial class dlgEquipement : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgEquipement(IAppObjFactory factory, IObjEquipement equipement = null)
        {
            InitializeComponent();
            this.factory = factory;
            this.equipement = equipement;
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

                // On masque les images
                pictureBoxTelescope.Visible = false;
                pictureBoxMonture.Visible = false;
                pictureBoxCamera.Visible = false;
                pictureBoxDivers.Visible = false;

                // Initialisation des composants du formulaire
                InitialisationComboTypeEquipement();

                // Chargement des données
                LoadEquipement();

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
        /// Permet l'initialisation de la Combo comboBoxTypeEquipement
        /// </summary>
        private void InitialisationComboTypeEquipement()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxTypeEquipement", GetType().Name);

            // CLear de la liste
            comboBoxTypeEquipement.Items.Clear();

            // Rechargement depuis la liste chargée
            ComboBoxItems comboBoxItems = new ComboBoxItems();
            foreach (IObjTypeEquipement typeEnCours in factory.GetListeTypeEquipements())
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(typeEnCours.Nom, typeEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxTypeEquipement.DisplayMember = "Text";
            comboBoxTypeEquipement.ValueMember = "Value";
            comboBoxTypeEquipement.DataSource = comboBoxItems;

            // Positionnement de "Tous" par défaut
            comboBoxTypeEquipement.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxTypeEquipement.Items.Count} Type d'équipements et sélection de l'élément : {comboBoxTypeEquipement.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Chargement d'un équipement en mode Edition
        /// </summary>
        private void LoadEquipement()
        {
            if (equipement != null)
            {
                comboBoxTypeEquipement.SelectedValue = equipement.TypeEquipement.Id;
                textBoxNom.Text = equipement.Nom;
                pictureBoxTelescope.Visible = equipement.TypeEquipement.Icone == factory.GetListeTypeEquipements()[0].Icone;
                pictureBoxMonture.Visible = equipement.TypeEquipement.Icone == factory.GetListeTypeEquipements()[1].Icone;
                pictureBoxCamera.Visible = equipement.TypeEquipement.Icone == factory.GetListeTypeEquipements()[2].Icone;
                pictureBoxLens.Visible = equipement.TypeEquipement.Icone == factory.GetListeTypeEquipements()[3].Icone;
                pictureBoxDivers.Visible = equipement.TypeEquipement.Icone == factory.GetListeTypeEquipements()[4].Icone;
            }
        }

        /// <summary>
        /// Update de l'image du type d'équipement en fonction de la sélection dans la combo
        /// </summary>
        private void UpdateIconeTypeEquipement()
        {
            try
            {
                pictureBoxTelescope.Visible = comboBoxTypeEquipement.Text == factory.GetListeTypeEquipements()[0].Nom;
                pictureBoxMonture.Visible = comboBoxTypeEquipement.Text == factory.GetListeTypeEquipements()[1].Nom;
                pictureBoxCamera.Visible = comboBoxTypeEquipement.Text == factory.GetListeTypeEquipements()[2].Nom;
                pictureBoxLens.Visible = comboBoxTypeEquipement.Text == factory.GetListeTypeEquipements()[3].Nom;
                pictureBoxDivers.Visible = comboBoxTypeEquipement.Text == factory.GetListeTypeEquipements()[4].Nom;
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Sauvegarde d'un équipement
        /// </summary>
        private void SaveEquipement()
        {
            try
            {
                // Vérif des Inputs
                if (string.IsNullOrEmpty(textBoxNom.Text))
                {
                    MessageBox.Show($"{Resources.ChampObligatoire} : {Resources.Nom}"
                                    , Application.ProductName
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Warning);
                    return;
                }
                string idTypeInstrument = string.Empty;
                if (comboBoxTypeEquipement.Items.Count > 0 && comboBoxTypeEquipement.SelectedValue != null)
                {
                    idTypeInstrument = comboBoxTypeEquipement.SelectedValue.ToString();
                }
                if (string.IsNullOrEmpty(idTypeInstrument))
                {
                    MessageBox.Show($"{Resources.ChampObligatoire} : TypeInstrument"
                                    , Application.ProductName
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Warning);
                    return;
                }

                // Création
                if (equipement == null)
                {
                    factory.CreateEquipement(textBoxNom.Text, idTypeInstrument, string.Empty, string.Empty);
                }
                // Modification
                else
                {
                    equipement.Nom = textBoxNom.Text;
                    equipement.IdTypeEquipement = idTypeInstrument;
                    factory.UpdateEquipement(equipement);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception err)
            {
                // Trace de l'erreur et on la remonte à l'appelant
                factory.GetLog().LogException(err, GetType().Name);
                throw;
            }            
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        /// <summary>
        /// Objet Equipement en mode Edition
        /// </summary>
        private IObjEquipement equipement = null;

        #endregion

        private void dlgEquipement_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveEquipement();
        }

        private void comboBoxTypeEquipement_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateIconeTypeEquipement();
        }
    }
}
