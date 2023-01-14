using System;
using System.Diagnostics;
using System.Windows.Forms;
using AstroSessionOrganizerModule;
using System.Collections.Generic;
using System.Linq;
using ApplicationTools;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue d'ajout ou de modification d'un setup
    /// </summary>
    public partial class dlgSetup : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgSetup(IAppObjFactory factory, IObjSetup setup = null)
        {
            InitializeComponent();
            this.factory = factory;
            this.setup = setup;

            // Initialisation des objets
            listeEquipementSetup = new List<IObjEquipementSetup>();
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
                LoadSetup();

                // Initialisation des composants du formulaire
                UpdateListBoxDispo();
                UpdateListBoxAjoutes();
                SetToolTips();
                UpdateButtonsState();

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
            // ToolTip Setup 
            toolTipInfosSetup.ToolTipTitle = Resources.InformationsSurLesSetup;
            string toolTipInfo = Resources.VousPouvezIciAjouterOuSupprimerDesEquipementsAVotreSetup
                                + Environment.NewLine + Resources.AttentionLesModificationsApporteesAVosSetupVontConcernerToutesLesSessionsDObservationsUtilisantCeSetup
                                + Environment.NewLine + Resources.NousVousRecommandonsDeGarderUnHistoriqueDeVosSetupEtDeLeursEvolutions;
            toolTipInfosSetup.SetToolTip(pictureBoxInfoSetup, toolTipInfo);

            // ToolTip Dénomination 
            toolTipInfosDenomination.ToolTipTitle = Resources.InformationsSurLesDenominationsDesEquipements;
            string toolTipInfoDenomination = Resources.VousPouvezSpecifierUnNomDifferentPourLEquipementTelQuIlApparaitraDansLeSetup
                                + Environment.NewLine + Resources.ParExempleGuidageSWEvoguideED50
                                + Environment.NewLine + Resources.AttentionLesModificationsDuNomDeLEquipementNeSerontPlusPrisesEnCompte
                                + Environment.NewLine + Resources.PourRemettreLeNomParDefautSupprimerLeTexteEtLaisserLaZoneVide
                                + Environment.NewLine + Resources.LesSignifientQueLeNomParDefautNomDeLEquipementEstUtilise;
            toolTipInfosDenomination.SetToolTip(pictureBoxInfosDenomination, toolTipInfoDenomination);

            // ToolTip Bouton Ajoute 
            toolTipInfosButtonAjoute.SetToolTip(buttonAjoute, Resources.AjouterLEquipementAuSetup);

            // ToolTip Bouton Enlève 
            toolTipInfosButtonEnleve.SetToolTip(buttonEnleve, Resources.EnleverLEquipementDuSetup);

            // ToolTip Bouton Dénominations 
            toolTipInfosButtonDenomination.SetToolTip(buttonDenomination, Resources.ModifierLeNomDeLEquipementDansLeSetup
                                                + Environment.NewLine + Resources.PourRemettreLeNomParDefautSupprimerLeTexteEtLaisserLaZoneVide);
        }

        /// <summary>
        /// Chargement des équipements disponibles
        /// </summary>
        private void UpdateListBoxDispo()
        {
            // Clear de la liste
            listBoxEquipementDisponible.Items.Clear();
            
            // Parcours des équipements
            foreach (IObjEquipement equipement in factory.GetListeEquipements().OrderBy(eq => eq.Nom).OrderBy(eq => eq.IdTypeEquipement))
            {
                // On ajoute l'équipements s'il ne fait pas déjà parti du setup
                if (listeEquipementSetup != null && listeEquipementSetup.Where(es => es.IdEquipement == equipement.Id).Count() == 0)
                {
                    ListItem item = new ListItem()
                    {
                        Name = $"{equipement.TypeEquipement.Nom} : {equipement.Nom}",
                        Value = equipement.Id
                    };
                    listBoxEquipementDisponible.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Chargement des équipements ajoutés au Setup
        /// </summary>
        private void UpdateListBoxAjoutes()
        {
            // Clear de la liste
            listBoxEquipementAjoute.Items.Clear();
            if (listeEquipementSetup != null)
            {
                int maxWidth = 0;
                foreach (IObjEquipementSetup equipementSetup in listeEquipementSetup)
                {
                    IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == equipementSetup.IdEquipement).FirstOrDefault();
                    if (equipement != null)
                    {
                        string denomination = equipementSetup.Nom;
                        if (string.IsNullOrEmpty(denomination))
                            denomination = $"[{equipement.Nom}]";
                        ListItem item = new ListItem()
                        {
                            Name = $"{denomination}",
                            Value = equipement.Id
                        };
                        listBoxEquipementAjoute.Items.Add(item);
                        int itemWidth = TextRenderer.MeasureText(item.ToString(),
                                                    listBoxEquipementAjoute.Font, listBoxEquipementAjoute.ClientSize,
                                                    TextFormatFlags.NoPrefix).Width;
                        if (maxWidth < itemWidth)
                            maxWidth = itemWidth;
                    }
                }
                listBoxEquipementAjoute.HorizontalExtent = maxWidth;
            }
        }

        /// <summary>
        /// Update de l'état des boutons
        /// </summary>
        private void UpdateButtonsState()
        {
            buttonAjoute.Enabled = listBoxEquipementDisponible.SelectedItems.Count == 1;
            buttonEnleve.Enabled = listBoxEquipementAjoute.SelectedItems.Count == 1;
            buttonDenomination.Enabled = listBoxEquipementAjoute.SelectedItems.Count == 1;
        }

        /// <summary>
        /// Chargement d'un setup en mode Edition
        /// </summary>
        private void LoadSetup()
        {
            if (setup != null)
            {
                textBoxNom.Text = setup.Nom;
                // Chargement des équipements du Setup
                foreach(IObjEquipementSetup equipementSetup in setup.ListeEquipement)
                {
                    listeEquipementSetup.Add(equipementSetup);
                }
            }
        }

        /// <summary>
        /// Ajoute un équipement au setup
        /// </summary>
        private void AjouteEquipement()
        {
            try
            {
                if (listBoxEquipementDisponible.SelectedItems.Count == 1)
                {
                    IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == ((ListItem)listBoxEquipementDisponible.SelectedItem).Value).FirstOrDefault();
                    if (equipement != null && listeEquipementSetup.Where(eq => eq.IdEquipement == equipement.Id).FirstOrDefault() == null)
                    {
                        IObjEquipementSetup equipementSetup = factory.GetNewEquipementSetup();
                        equipementSetup.IdEquipement = equipement.Id;
                        //equipementSetup.Nom = equipement.Nom;
                        listeEquipementSetup.Add(equipementSetup);
                        UpdateListBoxDispo();
                        UpdateListBoxAjoutes();
                        UpdateButtonsState();
                    }
                }
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

        /// <summary>
        /// Enlève un équipement du setup
        /// </summary>
        private void EnleveEquipement()
        {
            try
            {
                if (listBoxEquipementAjoute.SelectedItems.Count == 1)
                {
                    listeEquipementSetup.RemoveAt(listBoxEquipementAjoute.SelectedIndex);
                    UpdateListBoxDispo();
                    UpdateListBoxAjoutes();
                    UpdateButtonsState();
                }
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

        /// <summary>
        /// Positionne un nouveau pour un équipement dans le setup
        /// </summary>
        private void SetNouveauNom()
        {
            try
            {
                if (listBoxEquipementAjoute.SelectedItems.Count == 1)
                {
                    IObjEquipementSetup equipementSetup = listeEquipementSetup.Where(eqS => eqS.IdEquipement == ((ListItem)listBoxEquipementAjoute.SelectedItem).Value).FirstOrDefault();
                    if (equipementSetup != null)
                    {
                        string nomEquipement = equipementSetup.Nom;
                        if (string.IsNullOrEmpty(nomEquipement))
                        {
                            IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == equipementSetup.IdEquipement).FirstOrDefault();
                            if (equipement != null)
                                nomEquipement = equipement.Nom;
                        }
                        if (InputBox.ShowDialog(Resources.NomDeLEquipementDansLeSetup, Resources.VeuillezDonnerUnNomPourLEquipementDansLeSetup, ref nomEquipement) == DialogResult.OK)
                        {
                            equipementSetup.Nom = nomEquipement;

                            UpdateListBoxDispo();
                            UpdateListBoxAjoutes();
                            UpdateButtonsState();
                        }
                    }
                }
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

        /// <summary>
        /// Sauvegarde du setup et fermeture
        /// </summary>
        private void SaveSetup()
        {
            try
            {
                // Vérif des paramètres
                if (string.IsNullOrEmpty(textBoxNom.Text))
                {
                    MessageBox.Show($"{Resources.ChampObligatoire} : {Resources.Nom}"
                                    , Application.ProductName
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Warning);
                    return;
                }

                // Mode création
                if (setup == null)
                {
                    setup = factory.CreateSetup(textBoxNom.Text, string.Empty, string.Empty);
                }
                // Mode Edition
                else
                {
                    setup.Nom = textBoxNom.Text;
                    factory.UpdateSetup(setup);
                }

                // On supprime les équipements présents
                setup.DeleteEquipements();

                // On ajoute les équipements de la liste
                foreach(IObjEquipementSetup equipementSetup in listeEquipementSetup)
                {
                    factory.CreateEquipementSetup(equipementSetup.Nom, equipementSetup.IdEquipement, setup.Id);
                }

                // Retour
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
        /// Objet Setup en mode Edition
        /// </summary>
        private IObjSetup setup = null;

        /// <summary>
        /// Liste des équipements ajoutés au Setup
        /// </summary>
        private List<IObjEquipementSetup> listeEquipementSetup = null;

        #endregion

        private void dlgSetup_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void listBoxEquipementDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void listBoxEquipementAjoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void buttonAjoute_Click(object sender, EventArgs e)
        {
            AjouteEquipement();
        }

        private void listBoxEquipementDisponible_DoubleClick(object sender, EventArgs e)
        {
            AjouteEquipement();
        }

        private void buttonEnleve_Click(object sender, EventArgs e)
        {
            EnleveEquipement();
        }

        private void listBoxEquipementAjoute_DoubleClick(object sender, EventArgs e)
        {
            EnleveEquipement();
        }

        private void buttonDenomination_Click(object sender, EventArgs e)
        {
            SetNouveauNom();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSetup();
        }
    }
}
