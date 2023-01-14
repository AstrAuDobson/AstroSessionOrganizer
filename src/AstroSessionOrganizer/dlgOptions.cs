using System;
using System.Diagnostics;
using System.Windows.Forms;
using ApplicationTools;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de Dialogue Options
    /// </summary>
    public partial class dlgOptions : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgOptions(IAppObjFactory factory)
        {
            InitializeComponent();
            this.factory = factory;

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

                // Chargement des données

                // Stellarium
                groupBoxStellarium.Enabled = factory.GetAppStellarium().IsInstalled || factory.GetAppCartesDuCiel().IsInstalled;
                textBoxHostStellarium.Enabled = factory.GetAppStellarium().IsInstalled;
                textBoxPortStellarium.Enabled = factory.GetAppStellarium().IsInstalled;
                textBoxHostStellarium.Text = factory.GetAppStellarium().Host;
                textBoxPortStellarium.Text = factory.GetAppStellarium().Port;
                toolTipInfoStellarium.ToolTipTitle = Resources.ParametresDuPluginDeControleADistanceDeStellarium;
                toolTipInfoStellarium.SetToolTip(pictureBoxIconInfoStellarium,
                        Resources.PositionnezIciLesInformationsNecessairesALaConnexionAuPluginDeCommandeADistanceDeStellarium
                        + Environment.NewLine
                        + Resources.PourExecuterStellariumDirectementSurCetOrdinateurLaissezLaValeurParDefautLocalhost
                        + Environment.NewLine
                        + Resources.LaValeurDuPortDoitCorrespondreACellePositionneeDansStellariumParDefaut8090);

                // Cartes du Ciel
                textBoxHostCartesDuCiel.Enabled = factory.GetAppCartesDuCiel().IsInstalled;
                textBoxHostCartesDuCiel.Text = factory.GetAppCartesDuCiel().Host;
                toolTipInfoCartesDuCiel.ToolTipTitle = Resources.ParametresDeCartesDuCiel;
                toolTipInfoCartesDuCiel.SetToolTip(pictureBoxIconInfoCartesDuCiel,
                        Resources.PourVousConnecterACartesDuCielSurUnServeurSpecifiezIciLAdresseIPDuServeur
                        + Environment.NewLine
                        + Resources.PourExecuterCartesDuCielDirectementSurCetOrdinateurLaissezLaValeurParDefaut127001);

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
        /// Lance l'enregistrement des Settings saisis
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Enregistrement des paramètres", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();
                // Stellarium.
                if (string.IsNullOrEmpty(textBoxHostStellarium.Text) || string.IsNullOrEmpty(textBoxPortStellarium.Text))
                    throw new WarningException(Resources.FormatDesChampsPourLePluginStellariumIncorrect);
                // Cartes du Ciel.
                if (string.IsNullOrEmpty(textBoxHostCartesDuCiel.Text))
                    throw new WarningException(Resources.FormatDuChampServeurPourCartesDuCielIncorrect);

                // Si tous les champs valide, mise à jour des Settings applicatifs
                factory.GetAppStellarium().Host = textBoxHostStellarium.Text;
                factory.GetAppStellarium().Port = textBoxPortStellarium.Text;
                factory.GetAppCartesDuCiel().Host = textBoxHostCartesDuCiel.Text;

                // Trace
                factory.GetLog().Log($"Enregistrement des Settings effectué avec succès en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);

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

        #endregion

        private void dlgOptions_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
