using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue de gestion des logiciels d'une session
    /// </summary>
    public partial class dlgSelectLogiciel : Form
    {
        #region Propriétés
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgSelectLogiciel(IAppObjFactory factory, MainFenetre caller, List<IObjLogicielSession> listeLogicielsSession)
        {
            InitializeComponent();
            this.factory = factory;
            this.caller = caller;
            this.listeLogicielsSession = listeLogicielsSession;

            // Initialisation des objets
            //listeLogicielsAjoutes = new List<IObjLogicielSession>();
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
                InitialisationListeLogicielDisponible();
                InitialisationListeLogicielAjoute();
                SetToolTips();

                // Chargement des données
                LoadSession();

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
            // ToolTip Bouton Ajouter un Equipement à la session
            toolTipAjoute.SetToolTip(buttonAjoute, Resources.AjouterLeLogicielSelectionneALaSession);

            // ToolTip Bouton Enlever un Equipement à la session
            toolTipEnleve.SetToolTip(buttonEnleve, Resources.RetirerLeLogicielSelectionneDeLaSession);

            // ToolTip Bouton Logiciel
            toolTipLogiciel.SetToolTip(buttonEditLogiciel, Resources.CreerUnNouveauLogiciel);
        }

        /// <summary>
        /// Permet l'initialisation de la liste des logiciels disponible
        /// </summary>
        private void InitialisationListeLogicielDisponible()
        {
            try
            {
                // Type de vue
                listViewLogicielDisponible.View = View.Details;

                // Adjout des colonnes
                listViewLogicielDisponible.Columns.Add(Resources.Logiciel, -2, HorizontalAlignment.Left);
                listViewLogicielDisponible.Columns.Add("IdLogiciel", 0, HorizontalAlignment.Left);

                // Trace
                factory.GetLog().Log("Initialisation de la liste des Logiciels disponibles effectuée avec succès", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Permet l'initialisation de la liste des ajoutés disponible
        /// </summary>
        private void InitialisationListeLogicielAjoute()
        {
            try
            {
                // Type de vue
                listViewLogicielSession.View = View.Details;

                // Adjout des colonnes
                listViewLogicielSession.Columns.Add(Resources.Logiciel, -2, HorizontalAlignment.Left);
                listViewLogicielSession.Columns.Add("IdLogiciel", 0, HorizontalAlignment.Left);

                // Trace
                factory.GetLog().Log("Initialisation de la liste des Logiciels session effectuée avec succès", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Mise à jour de la liste des logiciels disponibles
        /// </summary>
        private void UpdateListeLogicielDisponible()
        {
            // Clear de la liste
            listViewLogicielDisponible.Items.Clear();

            // Parcours des logiciels
            foreach (IObjLogiciel logiciel in factory.GetListeLogiciels().OrderBy(eq => eq.Nom).OrderBy(eq => eq.IdTypeLogiciel))
            {
                // On ajoute l'équipements s'il ne fait pas déjà parti des logiciels ajoutés
                if (listeLogicielsSession != null && listeLogicielsSession.Where(la => la.IdLogiciel == logiciel.Id).Count() == 0)
                {
                    ListViewItem item = listViewLogicielDisponible.Items.Add(new ListViewItem()
                    {
                        Text = $"{logiciel.TypeLogiciel.Nom} : {logiciel.Nom}"
                    });
                    item.SubItems.Add(logiciel.Id);
                    item.SubItems.Add($"{logiciel.TypeLogiciel.Nom} : {logiciel.Nom}");
                }
            }
        }

        /// <summary>
        /// Mise à jour de la liste des logiciels ajoutés
        /// </summary>
        private void UpdateListeLogicielAjoute()
        {
            // Clear de la liste
            listViewLogicielSession.Items.Clear();

            // Parcours des logiciels session
            foreach (IObjLogicielSession logicielSession in listeLogicielsSession)
            {
                // On récupère l'objet Logiciel concerné
                IObjLogiciel logiciel = factory.GetListeLogiciels().Where(l => l.Id == logicielSession.IdLogiciel).FirstOrDefault();
                if (logiciel != null)
                {
                    ListViewItem item = listViewLogicielSession.Items.Add(new ListViewItem()
                    {
                        Text = $"{logiciel.TypeLogiciel.Nom} : {logiciel.Nom}"
                    });
                    item.SubItems.Add(logiciel.Id);
                    item.SubItems.Add($"{logiciel.TypeLogiciel.Nom} : {logiciel.Nom}");
                }
            }
        }

        /// <summary>
        /// Update de l'état des boutons
        /// </summary>
        private void UpdateButtonsState()
        {
            buttonAjoute.Enabled = listViewLogicielDisponible.SelectedItems.Count == 1;
            buttonEnleve.Enabled = listViewLogicielSession.SelectedItems.Count == 1;
        }

        /// <summary>
        /// Chargement des logiciels de la session
        /// </summary>
        private void LoadSession()
        {
            //listeLogicielsAjoutes = listeLogicielsSession.ToList();
            UpdateListeLogicielDisponible();
            UpdateListeLogicielAjoute();
            UpdateButtonsState();
        }

        /// <summary>
        /// Ajoute un logiciel à la session
        /// </summary>
        private void AjouteLogicielSession()
        {
            try
            {
                if (listViewLogicielDisponible.SelectedItems.Count == 1)
                {
                    // On récupère l'élément sélectionné afin de vérifier le type d'objet Equipement/Setup/Sites
                    ListViewItem selectedItem = listViewLogicielDisponible.SelectedItems[0];
                    if (selectedItem != null)
                    {
                        IObjLogiciel logiciel = factory.GetListeLogiciels().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (logiciel != null && listeLogicielsSession.Where(l => l.Id == logiciel.Id).FirstOrDefault() == null)
                        {
                            IObjLogicielSession logicielSession = factory.GetNewLogicielSession();
                            logicielSession.IdLogiciel = logiciel.Id;
                            listeLogicielsSession.Add(logicielSession);
                            UpdateListeLogicielDisponible();
                            UpdateListeLogicielAjoute();
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
        /// Enlève un logiciel à la session
        /// </summary>
        private void EnleveLogicielSession()
        {
            try
            {
                if (listViewLogicielSession.SelectedItems.Count == 1)
                {
                    // On récupère l'élément sélectionné
                    ListViewItem selectedItem = listViewLogicielSession.SelectedItems[0];
                    if (selectedItem != null)
                    {
                        IObjLogicielSession logicielSession = listeLogicielsSession.Where(la => la.IdLogiciel == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (logicielSession != null)
                        {
                            listeLogicielsSession.Remove(logicielSession);
                            UpdateListeLogicielDisponible();
                            UpdateListeLogicielAjoute();
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
        /// Ajout d'un nouvel logiciel
        /// </summary>
        private void AddLogiciel()
        {
            try
            {
                if (caller != null)
                {
                    // Lancement Edition
                    caller.CreateLogiciel();
                    UpdateListeLogicielDisponible();
                    UpdateListeLogicielAjoute();
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
            finally
            {
                // Repositionnement du texte de la Status
                caller.SetStatusActionText(Resources.SelectionDesLogiciels);
            }
        }

        /// <summary>
        /// Validation de la liste des logiciels pour la session
        /// </summary>
        private void ValidateListeLogicielSession()
        {
            try
            {
                // On quitte le formulaire
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
        /// Instance de la MainFenetre
        /// </summary>
        private MainFenetre caller = null;

        /// <summary>
        /// Liste des logiciels de la session appelante
        /// </summary>
        private List<IObjLogicielSession> listeLogicielsSession = null;

        /// <summary>
        /// Liste des logiciels ajoutés à la session
        /// </summary>
        //private List<IObjLogicielSession> listeLogicielsAjoutes = null;

        #endregion

        private void dlgSelectLogiciel_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void listViewLogicielDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void listViewLogicielSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void buttonAjoute_Click(object sender, EventArgs e)
        {
            AjouteLogicielSession();
        }

        private void buttonEnleve_Click(object sender, EventArgs e)
        {
            EnleveLogicielSession();
        }

        private void listViewLogicielDisponible_DoubleClick(object sender, EventArgs e)
        {
            AjouteLogicielSession();
        }

        private void listViewLogicielSession_DoubleClick(object sender, EventArgs e)
        {
            EnleveLogicielSession();
        }

        private void buttonEditLogiciel_Click(object sender, EventArgs e)
        {
            AddLogiciel();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ValidateListeLogicielSession();
        }

        private void dlgSelectLogiciel_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
