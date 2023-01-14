using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ApplicationTools;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue d'ajout ou de modification d'une session
    /// </summary>
    public partial class dlgSession : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgSession(IAppObjFactory factory, MainFenetre caller, IObjSession session = null)
        {
            InitializeComponent();
            this.factory = factory;
            this.caller = caller;
            this.session = session;

            // Initialisation des objets
            listeEquipementSetup = new List<IObjEquipementSetup>();
            listeEquipementAdditionnels = new List<IObjEquipementSession>();
            listeObservationsSession = new List<IObjObservation>();
            listeLogicielsSession = new List<IObjLogicielSession>();
            listeLogicielAffichage = new BindingList<Tuple<string, string>>();
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
                InitialisationComboSite();
                InitialisationComboSetup();
                InitialisationListeEquipementSession();
                InitialisationListeObservation();
                InitialisationListeLogiciels();
                SetToolTips();

                // Chargement des données
                LoadSession();

                // Update des listes des équipements
                UpdateListBoxEquipementsDispo();
                UpdateListeViewEquipementsSession();
                UpdateEquipementButtonsState();

                // Update des logiciels
                PopulateListeLogiciels();

                // Update des observations
                UpdateListeViewObservations();
                UpdateObservationButtonsState();

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
            // ToolTip Bouton Rechercher 
            toolTipRechercher.SetToolTip(buttonSearchObjetCeleste, Resources.SelectionnerUnObjetCeleste);

            // ToolTip Bouton Nouveau Site 
            toolTipNewSite.SetToolTip(buttonNewSite, Resources.CreerUnNouveauSiteDObservations);

            // ToolTip Bouton Nouveau Site 
            toolTipImagesPath.SetToolTip(buttonPathImages, Resources.SelectionnerLeRepertoireOuSeTrouveLesImagesDeLaSession);

            // ToolTip Bouton Nouvel Equipement 
            toolTipNewEquipement.SetToolTip(buttonNewEquipement, Resources.CreerUnNouvelEquipement);

            // ToolTip Bouton Ajouter un Equipement à la session
            toolTipAjoute.SetToolTip(buttonAjoute, Resources.AjouterLEquipementSelectionneALaSession);

            // ToolTip Bouton Enlever un Equipement à la session
            toolTipEnleve.SetToolTip(buttonEnleve, Resources.RetirerLEquipementSelectionneDeLaSession);

            // ToolTip Bouton Dénomination
            toolTipDenomination.SetToolTip(buttonDenomination, Resources.ModifierLeNomDeLEquipementPourLaSession);

            // ToolTip Bouton Logiciel
            toolTipLogiciel.SetToolTip(buttonEditLogiciel, Resources.AjouterSupprimerDesLogicielsALaSession);

            // ToolTip Bouton Nouvelle Observation
            toolTipNewObservation.SetToolTip(buttonNewObservation, Resources.AjouterUneObservationALaSession);

            // ToolTip Bouton Modifier Observation
            toolTipEditObservation.SetToolTip(buttonEditObservation, Resources.ModifierLObservation);

            // ToolTip Bouton Supprimer Observation
            toolTipDeleteObservation.SetToolTip(buttonDeleteObservation, Resources.SupprimerLObservationDeLaSession);

            // ToolTip Comment 
            toolTipInfosComment.ToolTipTitle = Resources.InformationsSurLesCommentaires;
            string toolTipInfoComment = Resources.VousPouvezAjouterUnCommentaireAVotreSessionDObservations
                                + Environment.NewLine + Resources.ParExempleDesInformationsSurLaMeteoLeVent;
            toolTipInfosComment.SetToolTip(pictureBoxInfosComment, toolTipInfoComment);

            // ToolTip Dénomination 
            toolTipInfosDenomination.ToolTipTitle = Resources.InformationsSurLesDenominationsDesEquipements;
            string toolTipInfoDenomination = Resources.VousPouvezSpecifierUnNomDifferentPourLEquipementTelQuIlApparaitraDansLaSession
                                + Environment.NewLine + Resources.ParExempleGuidageSWEvoguideED50
                                + Environment.NewLine + Resources.AttentionLesModificationsDuNomDeLEquipementNeSerontPlusPrisesEnCompte
                                + Environment.NewLine + Resources.PourRemettreLeNomParDefautSupprimerLeTexteEtLaisserLaZoneVide
                                + Environment.NewLine + Resources.LesSignifientQueLeNomParDefautNomDeLEquipementEstUtilise
                                + Environment.NewLine + Resources.LesEquipementsAvecUnFondGrisFontPartieDUnSetupEtNeSontPasModifiables;
            toolTipInfosDenomination.SetToolTip(pictureBoxInfosDenomination, toolTipInfoDenomination);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxSite
        /// </summary>
        private void InitialisationComboSite()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxSite", GetType().Name);

            // CLear de la liste
            comboBoxSite.DataSource = null;
            comboBoxSite.Items.Clear();
            ComboBoxItems comboBoxItems = new ComboBoxItems();

            // Ajout de l'élément "Aucun"
            ComboBoxItem newItemNone = comboBoxItems.NewItem(Resources.Aucun, "-1");
            comboBoxItems.Rows.Add(newItemNone);

            // Rechargement depuis la liste chargée
            foreach (IObjSite siteEnCours in factory.GetListeSites().OrderBy(s => s.Nom))
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(siteEnCours.Nom, siteEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxSite.DisplayMember = "Text";
            comboBoxSite.ValueMember = "Value";
            comboBoxSite.DataSource = comboBoxItems;

            // Positionnement par défaut
            comboBoxSite.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxSite.Items.Count} éléments et sélection de l'élément : {comboBoxSite.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxSetup
        /// </summary>
        private void InitialisationComboSetup()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxSetup", GetType().Name);

            // CLear de la liste
            comboBoxSetup.Items.Clear();
            ComboBoxItems comboBoxItems = new ComboBoxItems();

            // Ajout de l'élément "Aucun"
            ComboBoxItem newItemNone = comboBoxItems.NewItem(Resources.Aucun, "-1");
            comboBoxItems.Rows.Add(newItemNone);

            // Rechargement depuis la liste chargée
            foreach (IObjSetup setupEnCours in factory.GetListeSetup().OrderBy(s => s.Nom))
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(setupEnCours.Nom, setupEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxSetup.DisplayMember = "Text";
            comboBoxSetup.ValueMember = "Value";
            comboBoxSetup.DataSource = comboBoxItems;

            // Positionnement par défaut
            comboBoxSetup.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxSetup.Items.Count} éléments et sélection de l'élément : {comboBoxSetup.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la liste des équipements de la session
        /// </summary>
        private void InitialisationListeEquipementSession()
        {
            try
            {
                // Type de vue
                listViewEquipementSession.View = View.Details;

                // Adjout des colonnes
                listViewEquipementSession.Columns.Add(Resources.Equipement, -2, HorizontalAlignment.Left);
                listViewEquipementSession.Columns.Add("IdEquipement", 0, HorizontalAlignment.Left);

                // Trace
                factory.GetLog().Log("Initialisation de la liste des Observations effectuée avec succès", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
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

                IObjTypeObjet typeObjet = factory.GetListeTypeObjets().Where(to => to.Id == objetCeleste.IdTypeObjet).FirstOrDefault();
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
        /// Chargement d'une session en mode Edition
        /// </summary>
        private void LoadSession()
        {
            // Clear des objets internes
            listeEquipementSetup.Clear();
            listeEquipementAdditionnels.Clear();

            if (session != null)
            {
                objetCeleste = session.ObjetCeleste;
                textBoxNomObjetCeleste.Text = objetCeleste.Nom;
                textBoxInfosObjet.Text = $"{objetCeleste.TypeObjet.Nom}{Environment.NewLine}{objetCeleste.Constellation.Nom} [{objetCeleste.Constellation.Abr.ToUpper()}]{Environment.NewLine}{objetCeleste.DenominationsFormated}";
                dateTimePickerSession.Value = session.DateHeure;
                if (session.Site != null)
                    comboBoxSite.SelectedValue = session.IdSite;
                else
                    comboBoxSite.SelectedValue = "-1";
                textBoxPathImages.Text = session.ImagesPath;
                textBoxComment.Text = session.Comment;
                //checkBoxComment.Checked = session.CommentDansExif;
                checkBoxComment.Checked = true;
                listeEquipementAdditionnels = session.ListeEquipementsSession.ToList();
                listeObservationsSession = session.ListeObservationsSession.ToList();
                listeLogicielsSession = session.ListeLogicielsSession.ToList();
                if (session.Setup != null)
                {
                    comboBoxSetup.SelectedValue = session.IdSetup;
                }
                else
                {
                    comboBoxSetup.SelectedValue = "-1";
                    UpdateListeEquipementSetup();
                    UpdateListeEquipementAdditionnels();
                }
            }
            else
            {
                objetCeleste = factory.GetListeObjetCeleste().ListeComplete[0];
                textBoxNomObjetCeleste.Text = objetCeleste.Nom;
                textBoxInfosObjet.Text = $"{objetCeleste.TypeObjet.Nom}{Environment.NewLine}{objetCeleste.Constellation.Nom} [{objetCeleste.Constellation.Abr.ToUpper()}]{Environment.NewLine}{objetCeleste.DenominationsFormated}";
                checkBoxComment.Checked = true;
            }
        }

        /// <summary>
        /// Chargement des équipements disponibles
        /// </summary>
        private void UpdateListBoxEquipementsDispo()
        {
            // Clear de la liste
            listBoxEquipementDisponible.Items.Clear();

            // Parcours des équipements
            foreach (IObjEquipement equipement in factory.GetListeEquipements().OrderBy(eq => eq.Nom).OrderBy(eq => eq.IdTypeEquipement))
            {
                // On ajoute l'équipements s'il ne fait pas déjà parti du setup et des équipements additionnels
                if ((listeEquipementSetup != null && listeEquipementSetup.Where(es => es.IdEquipement == equipement.Id).Count() == 0)
                    && (listeEquipementAdditionnels != null && listeEquipementAdditionnels.Where(ea => ea.IdEquipement == equipement.Id).Count() == 0))
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
        /// Chargement des équipements ajoutés à la session
        /// </summary>
        private void UpdateListeViewEquipementsSession()
        {
            // Clear de la liste
            listViewEquipementSession.Items.Clear();

            // Ajout des équipements du setup
            if (listeEquipementSetup != null)
            {
                foreach (IObjEquipementSetup equipementSetup in listeEquipementSetup)
                {
                    IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == equipementSetup.IdEquipement).FirstOrDefault();
                    if (equipement != null)
                    {
                        string denomination = equipementSetup.Nom;
                        if (string.IsNullOrEmpty(denomination))
                            denomination = $"{equipement.Nom}";
                        ListViewItem item = listViewEquipementSession.Items.Add(new ListViewItem(new[] {
                                                denomination,
                                                equipement.Id}));
                        item.BackColor = Color.LightGray;
                    }
                }
            }

            // Ajout des équipements de la session
            if (listeEquipementAdditionnels != null)
            {
                foreach (IObjEquipementSession equipementSession in listeEquipementAdditionnels)
                {
                    IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == equipementSession.IdEquipement).FirstOrDefault();
                    if (equipement != null)
                    {
                        string denomination = equipementSession.Nom;
                        if (string.IsNullOrEmpty(denomination))
                            denomination = $"[{equipement.Nom}]";
                        ListViewItem item = listViewEquipementSession.Items.Add(new ListViewItem(new[] {
                                                denomination,
                                                equipement.Id}));
                    }
                }
            }

            // AutoFit première colonne
            listViewEquipementSession.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            if (listViewEquipementSession.Columns.Count > 0)
                listViewEquipementSession.Columns[1].Width = 0;
        }

        /// <summary>
        /// Update de l'état des boutons
        /// </summary>
        private void UpdateEquipementButtonsState()
        {
            buttonAjoute.Enabled = listBoxEquipementDisponible.SelectedItems.Count == 1;
            buttonEnleve.Enabled = listViewEquipementSession.SelectedItems.Count == 1
                            && listeEquipementSetup.Where(eqs => eqs.IdEquipement == listViewEquipementSession.SelectedItems[0].SubItems[1].Text).FirstOrDefault() == null;
            buttonDenomination.Enabled = buttonEnleve.Enabled;
        }

        /// <summary>
        /// Mise à jour de la liste interne des équipements du setup
        /// </summary>
        private void UpdateListeEquipementSetup()
        {
            if (comboBoxSetup.SelectedValue != null
                && !string.IsNullOrEmpty(comboBoxSetup.SelectedValue.ToString())
                && comboBoxSetup.SelectedValue.ToString() != "-1")
            {
                IObjSetup setupSelected = factory.GetListeSetup().Where(s => s.Id == comboBoxSetup.SelectedValue.ToString()).FirstOrDefault();
                if (setupSelected != null)
                    listeEquipementSetup = setupSelected.ListeEquipement.ToList();
                else
                    listeEquipementSetup.Clear();
            }
            else
                listeEquipementSetup.Clear();
        }

        /// <summary>
        /// Mise à jour de la liste interne des équipements du setup
        /// </summary>
        private void UpdateListeEquipementAdditionnels()
        {
            // On utilise une liste temporaire
            List<IObjEquipementSession> listeEquipementTemp = new List<IObjEquipementSession>();

            // On ajoute les équipements de la liste en cours en vérifiant qu'ils ne sont pas dans la liste Setup
            foreach(IObjEquipementSession equipementSession in listeEquipementAdditionnels)
            {
                IObjEquipementSetup equipementSetup = listeEquipementSetup.Where(eqs => eqs.IdEquipement == equipementSession.IdEquipement).FirstOrDefault();
                if (equipementSetup == null)
                {
                    IObjEquipementSession newEquipementSession = factory.GetNewEquipementSession();
                    newEquipementSession.IdEquipement = equipementSession.IdEquipement;
                    newEquipementSession.Nom = equipementSession.Nom;
                    listeEquipementTemp.Add(newEquipementSession);
                }
            }

            // On met à jour la liste en cours
            listeEquipementAdditionnels.Clear();
            listeEquipementAdditionnels = null;
            listeEquipementAdditionnels = listeEquipementTemp;
        }

        /// <summary>
        /// Intervient lors d'un changement de setup dans la Combo
        /// </summary>
        private void ChangeSetup()
        {
            try
            {
                UpdateListeEquipementSetup();
                UpdateListeEquipementAdditionnels();
                UpdateListBoxEquipementsDispo();
                UpdateListeViewEquipementsSession();
                UpdateEquipementButtonsState();
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
        /// Ajoute un équipement à la session
        /// </summary>
        private void AjouteEquipementSession()
        {
            try
            {
                if (listBoxEquipementDisponible.SelectedItems.Count == 1)
                {
                    IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == ((ListItem)listBoxEquipementDisponible.SelectedItem).Value).FirstOrDefault();
                    if (equipement != null
                        && listeEquipementSetup.Where(eq => eq.IdEquipement == equipement.Id).FirstOrDefault() == null
                        && listeEquipementAdditionnels.Where(eq => eq.IdEquipement == equipement.Id).FirstOrDefault() == null)
                    {
                        IObjEquipementSession equipementSession = factory.GetNewEquipementSession();
                        equipementSession.IdEquipement = equipement.Id;
                        listeEquipementAdditionnels.Add(equipementSession);
                        UpdateListBoxEquipementsDispo();
                        UpdateListeViewEquipementsSession();
                        UpdateEquipementButtonsState();
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
        /// Enlève un équipement de la session
        /// </summary>
        private void EnleveEquipemenSessiont()
        {
            try
            {
                if (listViewEquipementSession.SelectedItems.Count == 1)
                {
                    IObjEquipementSession equipementSession = listeEquipementAdditionnels.Where(eqa => eqa.IdEquipement == listViewEquipementSession.SelectedItems[0].SubItems[1].Text).FirstOrDefault();
                    // On l'enlève s'il ne fait pas partie d'un setup
                    if (equipementSession != null
                        && listeEquipementSetup.Where(eqs => eqs.IdEquipement == equipementSession.IdEquipement).FirstOrDefault() == null)
                    {
                        listeEquipementAdditionnels.Remove(equipementSession);
                        UpdateListBoxEquipementsDispo();
                        UpdateListeViewEquipementsSession();
                        UpdateEquipementButtonsState();
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
        /// Positionne un nouveau pour un équipement de la session
        /// </summary>
        private void SetNouveauNom()
        {
            try
            {
                if (listViewEquipementSession.SelectedItems.Count == 1)
                {
                    IObjEquipementSession equipementSession = listeEquipementAdditionnels.Where(eqa => eqa.IdEquipement == listViewEquipementSession.SelectedItems[0].SubItems[1].Text).FirstOrDefault();
                    if (equipementSession != null)
                    {
                        string nomEquipement = equipementSession.Nom;
                        if (string.IsNullOrEmpty(nomEquipement))
                        {
                            IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == equipementSession.IdEquipement).FirstOrDefault();
                            if (equipement != null)
                                nomEquipement = equipement.Nom;
                        }
                        if (InputBox.ShowDialog(Resources.NomDeLEquipementDansLaSession, Resources.VeuillezDonnerUnNomPourLEquipementDansLaSession, ref nomEquipement) == DialogResult.OK)
                        {
                            equipementSession.Nom = nomEquipement;
                            UpdateListBoxEquipementsDispo();
                            UpdateListeViewEquipementsSession();
                            UpdateEquipementButtonsState();
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
        /// Ajout d'un nouvel équipement
        /// </summary>
        private void AddEquipement()
        {
            try
            {
                if (caller != null)
                {
                    // Lancement Edition
                    caller.CreateEquipement();
                    UpdateListBoxEquipementsDispo();
                    UpdateListeViewEquipementsSession();
                    UpdateEquipementButtonsState();
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
            finally
            {
                // Repositionnement du texte de la Status
                if (session == null)
                    caller.SetStatusActionText(Resources.CreationDUneSessionDObservations);
                else
                    caller.SetStatusActionText(Resources.EditionDUneSessionDObservations);
            }
        }

        /// <summary>
        /// Ajout d'un nouveau site d'observations
        /// </summary>
        private void AddSite()
        {
            try
            {
                if (caller != null)
                {
                    // Lancement Edition
                    caller.CreateSite();
                    InitialisationComboSite();
                    if (session != null && session.Site != null)
                        comboBoxSite.SelectedValue = session.IdSite;
                    else
                        comboBoxSite.SelectedValue = "-1";
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
            finally
            {
                // Repositionnement du texte de la Status
                if (session == null)
                    caller.SetStatusActionText(Resources.CreationDUneSessionDObservations);
                else
                    caller.SetStatusActionText(Resources.EditionDUneSessionDObservations);
            }
        }

        /// <summary>
        /// Permet l'initialisation de la liste des observations de la session
        /// </summary>
        private void InitialisationListeObservation()
        {
            try
            {
                // Type de vue
                listViewObservation.View = View.Details;

                // Adjout des colonnes
                listViewObservation.Columns.Add(Resources.Date, 60, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.Type, 60, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.Filtre, 120, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.NbExpositions, 30, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.TempsUnitaire, 30, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.TempsTotal, 40, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.GainISO, 40, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.Temperature, 30, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.Binning, 30, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.Seeing, 60, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.Lune, 60, HorizontalAlignment.Left);
                listViewObservation.Columns.Add(Resources.Commentaires, 120, HorizontalAlignment.Left);
                listViewObservation.Columns.Add("IdObservation", 0, HorizontalAlignment.Left);

                // Trace
                factory.GetLog().Log("Initialisation de la liste des Observations effectuée avec succès", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Chargement des observations de la session
        /// </summary>
        private void UpdateListeViewObservations()
        {
            // Clear de la liste
            listViewObservation.Items.Clear();

            // Ajout des équipements du setup
            if (listeObservationsSession != null)
            {
                foreach (IObjObservation observation in listeObservationsSession)
                {
                    listViewObservation.Items.Add(new ListViewItem(new[] {
                                                $"{observation.DateHeure.ToString("d")} - {observation.DateHeure.ToString("t")}",
                                                observation.TypeObservation.Nom,
                                                observation.Equipement != null ? observation.Equipement.Nom : string.Empty,
                                                observation.NBR_EXPO.HasValue ? observation.NBR_EXPO.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                                observation.TPS_EXPO.HasValue ? observation.TPS_EXPO.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                                observation.TempsTotalExposition.ToStandardFormatString(),
                                                observation.GAIN.HasValue ? observation.GAIN.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                                observation.TEMP.HasValue ? observation.TEMP.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                                observation.BINNING.HasValue ? observation.BINNING.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                                observation.Seeing,
                                                observation.Lune,
                                                observation.Comment,
                                                observation.Id}));
                    
                }
            }

            // AutoFit première colonne
            listViewObservation.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            if (listViewObservation.Columns.Count > 11)
                listViewObservation.Columns[12].Width = 0;
        }

        /// <summary>
        /// Update de l'état des boutons
        /// </summary>
        private void UpdateObservationButtonsState()
        {
            buttonNewObservation.Enabled = true;
            buttonEditObservation.Enabled = listViewObservation.SelectedItems.Count == 1;
            buttonDeleteObservation.Enabled = listViewObservation.SelectedItems.Count == 1;
        }

        /// <summary>
        /// Edition d'une observation
        /// </summary>
        private void EditObservation()
        {
            try
            {
                // Repositionnement du texte de la Status
                caller.SetStatusActionText(Resources.EditionDUneObsevation);

                // on récupère l'observation sélectionnnée
                if (listViewObservation.SelectedItems.Count == 1)
                {
                    IObjObservation observation = listeObservationsSession[listViewObservation.SelectedItems[0].Index];
                    if (observation != null)
                    {
                        // Lancement Edition
                        dlgObservation dlgEdition = new dlgObservation(factory, listeObservationsSession, observation.DateHeure, observation);
                        if (dlgEdition.ShowDialog() == DialogResult.OK)
                        {
                            UpdateListeViewObservations();
                            UpdateObservationButtonsState();
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
            finally
            {
                // Repositionnement du texte de la Status
                if (session == null)
                    caller.SetStatusActionText(Resources.CreationDUneSessionDObservations);
                else
                    caller.SetStatusActionText(Resources.EditionDUneSessionDObservations);
            }
        }

        /// <summary>
        /// Ajout d'une observation
        /// </summary>
        private void AddObservation()
        {
            try
            {
                // Repositionnement du texte de la Status
                caller.SetStatusActionText(Resources.AjoutDUneObsevation);

                // Lancement Edition
                dlgObservation dlgEdition = new dlgObservation(factory, listeObservationsSession, dateTimePickerSession.Value);
                if (dlgEdition.ShowDialog() == DialogResult.OK)
                {
                    UpdateListeViewObservations();
                    UpdateObservationButtonsState();
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
            finally
            {
                // Repositionnement du texte de la Status
                if (session == null)
                    caller.SetStatusActionText(Resources.CreationDUneSessionDObservations);
                else
                    caller.SetStatusActionText(Resources.EditionDUneSessionDObservations);
            }
        }

        /// <summary>
        /// Suppression d'une observation
        /// </summary>
        private void DeleteObservation()
        {
            try
            {
                // on récupère l'observation sélectionnnée
                if (listViewObservation.SelectedItems.Count == 1)
                {
                    IObjObservation observation = listeObservationsSession[listViewObservation.SelectedItems[0].Index];
                    if (observation != null)
                    {
                        listeObservationsSession.RemoveAt(listViewObservation.SelectedItems[0].Index);
                        UpdateListeViewObservations();
                        UpdateObservationButtonsState();
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
        /// Ouvre la boîte de dialogue de recherche d'un objet céleste
        /// </summary>
        private void SearchObjetCeleste()
        {
            try
            {
                List<IObjObjetCeleste> listeRecherche = new List<IObjObjetCeleste>();
                listeRecherche.Add(objetCeleste);
                dlgSearchObjetCeleste dlgSearch = new dlgSearchObjetCeleste(factory, listeRecherche);
                if (dlgSearch.ShowDialog() == DialogResult.OK && listeRecherche.Count == 1)
                {
                    objetCeleste = listeRecherche[0];
                    textBoxNomObjetCeleste.Text = listeRecherche[0].Nom;
                    textBoxInfosObjet.Text = $"{listeRecherche[0].TypeObjet.Nom}{Environment.NewLine}{listeRecherche[0].Constellation.Nom} [{listeRecherche[0].Constellation.Abr.ToUpper()}]{Environment.NewLine}{listeRecherche[0].DenominationsFormated}";
                    listeRecherche.Clear();
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
        /// Ouvre la boîte de dialogue de sélection d'un répertoire images
        /// </summary>
        private void SearchPathImages()
        {
            try
            {
                FolderBrowserDialog openDlg = new FolderBrowserDialog();
                openDlg.SelectedPath = textBoxPathImages.Text;
                openDlg.Description = Resources.VeuillezSelectionnerLeRepertoireContenantLesImagesDeLaSessionDObservations;
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxPathImages.Text = openDlg.SelectedPath;
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
                textBoxPathImages.Text = string.Empty;
            }
        }

        /// <summary>
        /// Ouvre la boîte de dialogue de sélections des logiciels de la session
        /// </summary>
        private void SelectLogiciels()
        {
            try
            {
                // Repositionnement du texte de la Status
                caller.SetStatusActionText(Resources.SelectionDesLogiciels);

                List<IObjLogicielSession> listeEdition = listeLogicielsSession.ToList();
                dlgSelectLogiciel dlgEdition = new dlgSelectLogiciel(factory, caller, listeEdition);
                if (dlgEdition.ShowDialog() == DialogResult.OK)
                {
                    listeLogicielsSession.Clear();
                    listeLogicielsSession = listeEdition;
                    PopulateListeLogiciels();
                }
                else
                {
                    listeEdition.Clear();
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
                textBoxPathImages.Text = string.Empty;
            }
            finally
            {
                // Repositionnement du texte de la Status
                if (session == null)
                    caller.SetStatusActionText(Resources.CreationDUneSessionDObservations);
                else
                    caller.SetStatusActionText(Resources.EditionDUneSessionDObservations);
            }
        }

        /// <summary>
        /// Permet l'initialisation de la liste des logiciels de la session
        /// </summary>
        private void InitialisationListeLogiciels()
        {
            try
            {
                dataGridViewLogiciels.DataSource = listeLogicielAffichage;
                dataGridViewLogiciels.Columns[0].Width = 200;
                dataGridViewLogiciels.Columns[1].Width = 400;
                dataGridViewLogiciels.ColumnHeadersVisible = false;
                dataGridViewLogiciels.RowHeadersVisible = false;
                dataGridViewLogiciels.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridViewLogiciels.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                listeLogicielAffichage.RaiseListChangedEvents = true;

                // Trace
                factory.GetLog().Log("Initialisation de la liste des Logiciels effectuée avec succès", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Actualise la liste des logiciels
        /// </summary>
        private void PopulateListeLogiciels()
        {
            // Clear de la liste
            listeLogicielAffichage.Clear();

            // On groupe par Type de logiciel
            foreach(IObjTypeLogiciel typeLogiciel in factory.GetListeTypeLogiciels())
            {
                string valNomSoftware = string.Empty;
                // On défini la valeur depuis la liste des logiciels de la session
                foreach (IObjLogicielSession logicielSession in listeLogicielsSession)
                {
                    // On récupère l'objet Logiciel correspondant
                    IObjLogiciel logiciel = factory.GetListeLogiciels().Where(l => l.Id == logicielSession.IdLogiciel).FirstOrDefault();
                    if (logiciel != null && logiciel.IdTypeLogiciel == typeLogiciel.Id)
                    {
                        valNomSoftware += logiciel.Nom;
                        valNomSoftware += " / ";
                    }
                }
                valNomSoftware = valNomSoftware.TrimEnd();
                valNomSoftware = valNomSoftware.TrimEnd('/');
                valNomSoftware = valNomSoftware.TrimEnd();
                if (!string.IsNullOrEmpty(valNomSoftware))
                    listeLogicielAffichage.Add(new Tuple<string, string>(typeLogiciel.Nom, valNomSoftware));
            }
        }

        /// <summary>
        /// Sauvegarde de la Session
        /// </summary>
        private void SaveSession()
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();
                
                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;
                
                // Vérif des paramètres
                if (objetCeleste == null)
                {
                    MessageBox.Show(Resources.ObjetCelesteIndéfini
                                    , Application.ProductName
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Warning);
                    return;
                }
                string idSite = !string.IsNullOrEmpty(comboBoxSite.SelectedValue.ToString()) ?
                                                comboBoxSite.SelectedValue.ToString() != "-1" ? comboBoxSite.SelectedValue.ToString() : string.Empty
                                            : string.Empty;
                string idSetup = !string.IsNullOrEmpty(comboBoxSetup.SelectedValue.ToString()) ?
                                                comboBoxSetup.SelectedValue.ToString() != "-1" ? comboBoxSetup.SelectedValue.ToString() : string.Empty
                                            : string.Empty;

                // Mode création
                if (session == null)
                {
                    session = factory.CreateSession(objetCeleste.Id, idSetup, idSite, dateTimePickerSession.Value.ToString("yyyyMMddHHmmss"), string.Empty,
                                                    textBoxComment.Text, checkBoxComment.Checked, string.Empty, textBoxPathImages.Text, null);
                }
                // Mode Edition
                else
                {
                    session.IdObjetCeleste = objetCeleste.Id;
                    session.IdSetup = idSetup;
                    session.IdSite = idSite;
                    session.DateLtnv = dateTimePickerSession.Value.ToString("yyyyMMddHHmmss");
                    session.Description = string.Empty;
                    session.Comment = textBoxComment.Text;
                    session.CommentDansExif = checkBoxComment.Checked;
                    //session.Thumbnail = string.Empty;
                    session.ImagesPath = textBoxPathImages.Text;
                    session.Rank = null;
                    factory.UpdateSession(session);
                }

                // Equipements additionnels
                // On supprime les équipements présents
                session.DeleteEquipements();
                // On ajoute les équipements de la liste
                foreach (IObjEquipementSession equipementSession in listeEquipementAdditionnels)
                {
                    equipementSession.IdSession = session.Id;
                    //session.CreateEquipementSession(equipementSession.Nom, equipementSession.IdEquipement);
                }
                factory.CreateEquipementsSession(listeEquipementAdditionnels);

                // Observations
                // On supprime les observations présentes
                session.DeleteObservations();
                // On ajoute les observations à la liste
                foreach(IObjObservation observationSession in listeObservationsSession)
                {
                    observationSession.IdSession = session.Id;
                    //session.CreateObservationSession(observationSession);
                }
                factory.CreateObservations(listeObservationsSession);

                // Logiciels
                // On supprime les logiciels présents
                session.DeleteLogiciels();
                // On ajoute les logiciels de la liste
                foreach (IObjLogicielSession logicielSession in listeLogicielsSession)
                {
                    logicielSession.IdSession = session.Id;
                    //session.CreateLogicielSession(logicielSession.Nom, logicielSession.IdLogiciel);
                }
                factory.CreateLogicielsSession(listeLogicielsSession);

                // Fermeture de la dialogue
                DialogResult = DialogResult.OK;
                Close();

                // Trace
                factory.GetLog().Log("Fonction SaveSession FIN", GetType().Name, debutInitialisation.ElapsedMilliseconds);
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
                // Positionnement du Curseur
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        /// <summary>
        /// Instance de la MainFenetre
        /// </summary>
        private MainFenetre caller = null;

        /// <summary>
        /// Objet IObjSession en mode Edition
        /// </summary>
        private IObjSession session = null;

        /// <summary>
        /// Objet IObjObjetCeleste en mémoire servant à la sélection de l'objet
        /// </summary>
        private IObjObjetCeleste objetCeleste = null;

        /// <summary>
        /// Liste des équipements du Setup ajouté à la session
        /// </summary>
        private List<IObjEquipementSetup> listeEquipementSetup = null;

        /// <summary>
        /// Liste des équipements addictionnels ajoutés à la session
        /// </summary>
        private List<IObjEquipementSession> listeEquipementAdditionnels = null;

        /// <summary>
        /// Liste des observations de la session
        /// </summary>
        private List<IObjObservation> listeObservationsSession = null;

        /// <summary>
        /// Liste des logiciels de la session
        /// </summary>
        private List<IObjLogicielSession> listeLogicielsSession = null;

        /// <summary>
        /// Liste servant à l'affichage dans la GridView
        /// </summary>
        private BindingList<Tuple<string, string>> listeLogicielAffichage = null;

        #endregion

        private void textBoxNomObjetCeleste_TextChanged(object sender, EventArgs e)
        {
            UpdateIconeTypeObjet();
        }

        private void dlgSession_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void listBoxEquipementDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEquipementButtonsState();
        }

        private void listViewEquipementSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEquipementButtonsState();
        }

        private void comboBoxSetup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSetup();
        }

        private void buttonAjoute_Click(object sender, EventArgs e)
        {
            AjouteEquipementSession();
        }

        private void listBoxEquipementDisponible_DoubleClick(object sender, EventArgs e)
        {
            AjouteEquipementSession();
        }

        private void buttonEnleve_Click(object sender, EventArgs e)
        {
            EnleveEquipemenSessiont();
        }

        private void listViewEquipementSession_DoubleClick(object sender, EventArgs e)
        {
            EnleveEquipemenSessiont();
        }

        private void buttonDenomination_Click(object sender, EventArgs e)
        {
            SetNouveauNom();
        }

        private void buttonNewEquipement_Click(object sender, EventArgs e)
        {
            AddEquipement();
        }

        private void buttonNewObservation_Click(object sender, EventArgs e)
        {
            AddObservation();
        }

        private void listViewObservation_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateObservationButtonsState();
        }

        private void buttonEditObservation_Click(object sender, EventArgs e)
        {
            EditObservation();
        }

        private void listViewObservation_DoubleClick(object sender, EventArgs e)
        {
            EditObservation();
        }

        private void buttonDeleteObservation_Click(object sender, EventArgs e)
        {
            DeleteObservation();
        }

        private void buttonSearchObjetCeleste_Click(object sender, EventArgs e)
        {
            SearchObjetCeleste();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSession();
        }

        private void buttonPathImages_Click(object sender, EventArgs e)
        {
            SearchPathImages();
        }

        private void buttonNewSite_Click(object sender, EventArgs e)
        {
            AddSite();
        }

        private void buttonEditLogiciel_Click(object sender, EventArgs e)
        {
            SelectLogiciels();
        }

        private void dlgSession_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Clear des liste internes
            if (listeEquipementAdditionnels != null)
            {
                listeEquipementAdditionnels.Clear();
                listeEquipementAdditionnels = null;
            }
            if (listeObservationsSession != null)
            {
                listeObservationsSession.Clear();
                listeObservationsSession = null;
            }
            if (listeLogicielsSession != null)
            {
                listeLogicielsSession.Clear();
                listeLogicielsSession = null;
            }
            if (listeLogicielAffichage != null)
            {
                listeLogicielAffichage.Clear();
                listeLogicielAffichage = null;
            }
        }
    }
}
