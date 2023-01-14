using System;
using System.Diagnostics;
using System.Windows.Forms;
using ApplicationTools;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue d'ajout ou de modification d'un logiciel
    /// </summary>
    public partial class dlgLogiciel : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgLogiciel(IAppObjFactory factory, IObjLogiciel logiciel = null)
        {
            InitializeComponent();
            this.factory = factory;
            this.logiciel = logiciel;
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
                InitialisationComboTypeLogiciel();

                // Chargement des données
                LoadLogiciel();

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
        /// Permet l'initialisation de la Combo comboBoxTypeLogiciel
        /// </summary>
        private void InitialisationComboTypeLogiciel()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxTypeLogiciel", GetType().Name);

            // CLear de la liste
            comboBoxTypeLogiciel.Items.Clear();

            // Rechargement depuis la liste chargée
            ComboBoxItems comboBoxItems = new ComboBoxItems();
            foreach (IObjTypeLogiciel typeEnCours in factory.GetListeTypeLogiciels())
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(typeEnCours.Nom, typeEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxTypeLogiciel.DisplayMember = "Text";
            comboBoxTypeLogiciel.ValueMember = "Value";
            comboBoxTypeLogiciel.DataSource = comboBoxItems;

            // Positionnement de "Tous" par défaut
            comboBoxTypeLogiciel.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxTypeLogiciel.Items.Count} Type de logiciel et sélection de l'élément : {comboBoxTypeLogiciel.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Chargement d'un logiciel en mode Edition
        /// </summary>
        private void LoadLogiciel()
        {
            if (logiciel != null)
            {
                comboBoxTypeLogiciel.SelectedValue = logiciel.IdTypeLogiciel;
                textBoxNom.Text = logiciel.Nom;
            }
        }

        /// <summary>
        /// Sauvegarde d'un logiciel
        /// </summary>
        private void SaveLogiciel()
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
                string idTypeLogiciel = string.Empty;
                if (comboBoxTypeLogiciel.Items.Count > 0 && comboBoxTypeLogiciel.SelectedValue != null)
                {
                    idTypeLogiciel = comboBoxTypeLogiciel.SelectedValue.ToString();
                }
                if (string.IsNullOrEmpty(idTypeLogiciel))
                {
                    MessageBox.Show($"{Resources.ChampObligatoire} : TypeLogiciel"
                                    , Application.ProductName
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Warning);
                    return;
                }

                // Création
                if (logiciel == null)
                {
                    factory.CreateLogiciel(textBoxNom.Text, idTypeLogiciel, string.Empty, string.Empty);
                }
                // Modification
                else
                {
                    logiciel.Nom = textBoxNom.Text;
                    logiciel.IdTypeLogiciel = idTypeLogiciel;
                    factory.UpdateLogiciel(logiciel);
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
        /// Objet IObjLogiciel en mode Edition
        /// </summary>
        private IObjLogiciel logiciel = null;

        #endregion

        private void dlgLogiciel_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveLogiciel();
        }
    }
}
