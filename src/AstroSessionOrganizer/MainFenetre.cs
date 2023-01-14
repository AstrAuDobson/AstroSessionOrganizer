using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using ApplicationTools;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Fenêtre principale de l'application
    /// </summary>
    public partial class MainFenetre : Form
    {
        #region Constantes

        /// <summary>
        /// Nom du Serveur FTP de téléchargement des fichiers de configuration
        /// </summary>
        public const string ftpHost = "xxx";

        /// <summary>
        /// Identifiant de connexion au serveur FTP
        /// </summary>
        public const string ftpCredentialLogin = "xxx";

        /// <summary>
        /// Mot de passe de connexion au serveur FTP
        /// </summary>
        public const string ftpCredentialPwd = "xxx";

        /// <summary>
        /// Répertoire sur le Serveur FTP de téléchargement contenant les fichiers de configuration
        /// </summary>
        public const string ftpDirectory = "xxx";

        #endregion

        #region Propriétés

        /// <summary>
        /// Determine si la fenêtre principale est à l'état Maximisé
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public bool WindowMaximized
        {
            get
            {
                return Properties.Settings.Default.WindowState == "Maximized";
            }
            set
            {
                Properties.Settings.Default.WindowState = value ? "Maximized" : "NormalState";
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Positionne en asynchrone le texte de la Status
        /// </summary>
        private string StatusLabelDefault
        {
            set
            {
                BeginInvoke(new Action(() =>
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        toolStripStatusDefaultText.Visible = false;
                        toolStripStatusDefaultText.Text = string.Empty;
                    }
                    else
                    {
                        toolStripStatusDefaultText.Visible = true;
                        toolStripStatusDefaultText.Text = value;
                    }
                }), null);
            }
        }

        /// <summary>
        /// Positionne en asynchrone le texte de la Status Action
        /// </summary>
        private string StatusLabelAction
        {
            set
            {
                BeginInvoke(new Action(() =>
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        toolStripStatusAction.Visible = false;
                        toolStripStatusAction.Text = string.Empty;
                    }
                    else
                    {
                        toolStripStatusAction.Visible = true;
                        toolStripStatusAction.Text = value;
                    }
                }), null);
            }
        }

        /// <summary>
        /// ATS installé sur le poste ?
        /// </summary>
        public bool IsAstroTargetSelectorInstalled { get; set; }

        /// <summary>
        /// Stellarium installé sur le poste ?
        /// </summary>
        public bool IsStellariumInstalled { get; set; }

        /// <summary>
        /// Stellarium background en cours ?
        /// </summary>
        public bool IsStellariumRunning { get; set; }

        /// <summary>
        /// Carte du Ciel installé sur le poste ?
        /// </summary>
        public bool IsCdCInstalled { get; set; }

        /// <summary>
        /// CdC background en cours ?
        /// </summary>
        public bool IsCdCRunning { get; set; }

        /// <summary>
        /// Url complète du fichier remote des versions de l'application
        /// </summary>
        public string ftpNewVersionFullPathFile
        {
            get
            {
                return $"ftp://{ftpHost}/{ftpDirectory}/{newVersionFileName}";
            }
        }

        /// <summary>
        /// Nom du fichier remote des versions de l'application
        /// </summary>
        public string newVersionFileName
        {
            get
            {
                return $"Version.csv";
            }
        }

        /// <summary>
        /// Path et nom du fichier de destination du téléchargement du fichier des versions
        /// </summary>
        private string newVersionFullPathFile
        {
            get
            {
                return $"{factory.GetApplicationDataPath()}/{newVersionFileName}";
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public MainFenetre()
        {
            InitializeComponent();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Initialisation de l'application
        /// </summary>
        private void InitialisationApplication()
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Initialisation de la Fabrique d'objet globale à l'application
                factory = new AppObjFactory();

                // Trace
                factory.GetLog().Log("Fonction InitialisationApplication DEBUT", GetType().Name);
                factory.GetLog().Log($"ProductName : {factory.GetAppContext().ProductName}", GetType().Name);
                factory.GetLog().Log($"Version : {factory.GetAppContext().ProductVersion}", GetType().Name);
                factory.GetLog().Log($"Application file : {factory.GetAppContext().ExecutablePath}", GetType().Name);
                factory.GetLog().Log($"Répertoire UserAppDataPath : {factory.GetAppContext().UserAppDataPath}", GetType().Name);
                factory.GetLog().Log($"Répertoire StartupPath : {factory.GetAppContext().StartupPath}", GetType().Name);
                factory.GetLog().Log($"Windows Version : {factory.GetAppContext().OSVersion}", GetType().Name);
                factory.GetLog().Log($"Code Langue - Code Pays : {factory.GetAppContext().CodeLangue}-{factory.GetAppContext().CodePays}", GetType().Name);

                // Force le scaling DPI mode
                AutoScaleDimensions = new SizeF(6F, 13F);
                AutoScaleMode = AutoScaleMode.Font;

                // Repositionne l'état de la fenêtre principale sur la valeur positionnée en settings
                factory.GetLog().Log($"Valeur du Settings WindowMaximized : {WindowMaximized}", GetType().Name);
                if (WindowMaximized)
                {
                    factory.GetLog().Log($"Maximisation de la fenêtre.", GetType().Name);
                    WindowState = FormWindowState.Maximized;
                }

                // Trace
                factory.GetLog().Log("Fonction InitialisationApplication FIN", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                if (factory != null)
                    factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
                // Sur erreur dans l'initialisation de la fenêtre principale, on quitte l'appli
                Application.Exit();
            }
        }

        /// <summary>
        /// Initialisation du Formulaire principal
        /// </summary>
        private void InitialisationFormulaire()
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Initialisation des textes de la status
                StatusLabelDefault = string.Empty;
                StatusLabelAction = string.Empty;
                SetToolTips();

                // Update des membres
                IsStellariumInstalled = factory.GetAppStellarium().IsInstalled;
                IsStellariumRunning = false;
                IsCdCInstalled = factory.GetAppCartesDuCiel().IsInstalled;
                IsCdCRunning = false;
                IsAstroTargetSelectorInstalled = factory.GetAppAstroTargetSelector().IsInstalled;
                toolStripButtonATS.Enabled = IsAstroTargetSelectorInstalled;

                // Vérification répertoire application dans Profil Utilisateur (1ère utilisation)
                if (!Directory.Exists(factory.GetApplicationDataPath())
                    || !File.Exists(Path.Combine(factory.GetApplicationDataPath(), "ASO.db")))
                {
                    string libelleAction = Resources.PremiereUtilisationDeLApplication + Environment.NewLine + Resources.MiseAJourDesFichiersDeConfiguration;
                    using (WaitDialog waitDownloadDlg = new WaitDialog(ManageConfigurationFiles, libelleAction))
                    {
                        waitDownloadDlg.ShowDialog();
                    }
                }
                else
                {
                    // Vérification d'une nouvelle version de l'application
                    string libelleAction = Resources.VerificationDeLaPresenceDUneNouvelleVersionDeLApplication;
                    using (WaitDialog waitDownloadDlg = new WaitDialog(CheckIfNouvelleVersion, libelleAction))
                    {
                        waitDownloadDlg.ShowDialog();
                    }
                    // On vérifie la version par rapport à la courante
                    if (!string.IsNullOrEmpty(updateVersion))
                    {
                        Version nouvelleVersionDispo = new Version(updateVersion);
                        if (nouvelleVersionDispo > Assembly.GetExecutingAssembly().GetName().Version)
                        {
                            // Une version plus récente est dispo, on affiche la boîte de dialogue
                            factory.GetLog().Log($"Nouvelle version disponible : {nouvelleVersionDispo}", GetType().Name);
                            // On affiche la boîte de dialogue informant d'une nouvelle version disponible
                            dlgNewVersion dialogNewVersion = new dlgNewVersion(factory, updateVersion, updateNom, updateDescription, updateUrl);
                            dialogNewVersion.ShowDialog();
                        }
                        else
                        {
                            factory.GetLog().Log($"Pas de nouvelle version disponible : {Assembly.GetExecutingAssembly().GetName().Version} / {nouvelleVersionDispo}", GetType().Name);
                        }
                    }
                }

                // Lecture de la version de la BDD
                Version versionBDD = factory.GetBDDVersion();

                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;
                // Texte de la Status
                SetStatusActionText(Resources.InitialisationDeLApplication);
                Application.DoEvents();

                // Initiliastion Formulaire Listing des Observations
                formSession = new FormSessions(factory, this);
                formSession.TopLevel = false;
                formSession.Dock = DockStyle.Fill;
                tabPageSession.Controls.Add(formSession);
                formSession.Show();
                formSession.InitialisationFormulaire();

                // Initiliastion Formulaire Listing des Objets Célestes
                formObjet = new FormObjets(factory, this);
                formObjet.TopLevel = false;
                formObjet.Dock = DockStyle.Fill;
                tabPageObjet.Controls.Add(formObjet);
                formObjet.Show();
                formObjet.InitialisationFormulaire();

                // Initiliastion Formulaire Listing des Equipements
                formEquipement = new FormEquipements(factory, this);
                formEquipement.TopLevel = false;
                formEquipement.Dock = DockStyle.Fill;
                tabPageEquipement.Controls.Add(formEquipement);
                formEquipement.Show();
                formEquipement.InitialisationFormulaire();

                // On affiche le status text de l'onglet par défaut
                formSession.SetStatusText();

                // Trace
                factory.GetLog().Log("Fonction InitialisationFormulaire FIN", GetType().Name, debutInitialisation.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                if (factory != null)
                    factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
                // Sur erreur dans l'initialisation de la fenêtre principale, on quitte l'appli
                Application.Exit();
            }
            finally
            {
                // Positionnement du Curseur
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Vérifie si les fichiers nécessaire à l'application sont présents
        /// </summary>
        private void ManageConfigurationFiles()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la procédure de mise à jour des fichiers de configuration", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Texte de la Status
                SetStatusActionText(Resources.MiseAJourDesFichiersDeConfiguration);

                // Si le répertoire AstrAuDobson n'existe pas, on le créer
                if (!Directory.Exists(factory.GetApplicationDataPath()))
                {
                    factory.GetLog().Log($"Création du répertoire {factory.GetApplicationDataPath()}", GetType().Name);
                    Directory.CreateDirectory(factory.GetApplicationDataPath());
                }

                // Si le répertoire data n'existe pas, on le créer
                if (!Directory.Exists(Path.Combine(factory.GetApplicationDataPath(), "data")))
                {
                    string path = Path.Combine(factory.GetApplicationDataPath(), "data");
                    factory.GetLog().Log($"Création du répertoire {path}", GetType().Name);
                    Directory.CreateDirectory(path);
                }

                // Si la base n'existe pas, on la copie depuis les datas
                if (!File.Exists(Path.Combine(factory.GetApplicationDataPath(), "ASO.db")))
                {
                    string path = Path.Combine(factory.GetApplicationDataPath(), "ASO.db");
                    factory.GetLog().Log($"Copie du fichier {path}", GetType().Name);
                    File.Copy(Path.Combine(factory.GetAppContext().StartupPath, "ASO.db"), path);
                }

                // Recopie des fichiers des constellations
                foreach(string file in Directory.GetFiles(Path.Combine(factory.GetAppContext().StartupPath, "data")))
                {
                    string path = Path.Combine(factory.GetApplicationDataPath(), "data", Path.GetFileName(file));
                    factory.GetLog().Log($"Copie du fichier {path}", GetType().Name);
                    File.Copy(file, path, true);
                }

                // Texte de la Status
                SetStatusActionText(string.Empty);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name, TypeLog.Warning);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                    , Application.ProductName
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
            finally
            {
                // Texte de la Status
                SetStatusActionText(string.Empty);
            }
        }

        /// <summary>
        /// Vérifie si une nouvelle version de l'application est disponible
        /// </summary>
        private void CheckIfNouvelleVersion()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la vérification de la présence d'une nouvelle version de l'application", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Positionnement status
                SetStatusActionText(Resources.VerificationDeLaPresenceDUneNouvelleVersionDeLApplication);
                // Lancement du téléchargement
                using (WebClient request = new WebClient())
                {
                    // Trace
                    factory.GetLog().Log($"Téléchargement du fichier {newVersionFileName}", GetType().Name);

                    // Téléchargement du fichier
                    request.Credentials = new NetworkCredential(ftpCredentialLogin, ftpCredentialPwd);
                    byte[] fileData = request.DownloadData(ftpNewVersionFullPathFile);
                    using (FileStream file = File.Create(newVersionFullPathFile))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }

                    // Trace
                    factory.GetLog().Log($"Téléchargement du fichier effectué en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);

                    // On vérifie la présence du fichier téléchargé
                    if (File.Exists(newVersionFullPathFile))
                    {
                        using (var reader = new StreamReader(newVersionFullPathFile))
                        {
                            // On passe la ligne d'en-tête
                            var lineTitre = reader.ReadLine();
                            // On lit la ligne Version en cours
                            var line = reader.ReadLine();
                            var values = line.Split('\t');
                            updateVersion = values[0];
                            updateNom = values[1];
                            updateDescription = values[2];
                            updateUrl = values[3];
                        }
                    }
                }

                // Texte de la Status
                SetStatusActionText(string.Empty);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name, TypeLog.Warning);
                updateVersion = string.Empty;
            }
            finally
            {
                // Texte de la Status
                SetStatusActionText(string.Empty);
            }
        }

        /// <summary>
        /// Positionne les ToolTip
        /// </summary>
        private void SetToolTips()
        {
            // ToolBar ToolTips
            toolStripButtonNewSession.ToolTipText = Resources.NouvelleSessionDObservations;
            toolStripButtonNewObjet.ToolTipText = Resources.NouvelObjetCeleste;
            toolStripButtonNewSite.ToolTipText = Resources.NouveauSiteDObservations;
            toolStripButtonNewSetup.ToolTipText = Resources.NouveauSetup;
            toolStripButtonNewEquipement.ToolTipText = Resources.NouvelEquipement;
            toolStripButtonNewLogiciel.ToolTipText = Resources.NouveauLogiciel;
            toolStripButtonModifier.ToolTipText = Resources.Modifier;
            toolStripButtonSupprimer.ToolTipText = Resources.Supprimer;
            toolStripButtonATS.ToolTipText = Resources.DemarrerLApplicationAstroTargetSelector;
        }

        /// <summary>
        /// Release des objets du formulaire avant Fermeture
        /// </summary>
        private void ReleaseObject()
        {
            try
            {
                // Fermeture et Release de la BDD
                //factory.GetISQLiteDatabase().Release();
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Positionne le texte de la zone standard de la status
        /// </summary>
        /// <param name="statusText"></param>
        public void SetStatusDefaultText(string statusText)
        {
            StatusLabelDefault = statusText;
        }

        /// <summary>
        /// Positionne le texte de la zone Action de la status
        /// </summary>
        /// <param name="statusText"></param>
        public void SetStatusActionText(string statusText)
        {
            StatusLabelAction = statusText;
        }

        /// <summary>
        /// Positionne l'état des boutons d'édition de la toolbar
        /// </summary>
        /// <param name="enabled"></param>
        public void SetToolBarEditButtonState(bool enabled)
        {
            toolStripButtonModifier.Enabled = enabled;
            toolStripButtonSupprimer.Enabled = enabled;
        }

        /// <summary>
        /// Actualisation Onglet et texte de la statusbar en fonction de l'onglet sélectionné
        /// </summary>
        private void ActivateTab()
        {
            try
            {
                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;
                if (tabPage.SelectedTab.Name == "tabPageSession")
                {
                    formSession.UpdateForm();
                    formSession.SetStatusText();
                }
                else if (tabPage.SelectedTab.Name == "tabPageObjet")
                {
                    formObjet.UpdateForm();
                    formObjet.SetStatusText();
                }
                else if (tabPage.SelectedTab.Name == "tabPageEquipement")
                {
                    formEquipement.SetStatusText();
                    formEquipement.UpdatePaneInfo();
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                if (factory != null)
                    factory.GetLog().LogException(err, GetType().Name);
            }
            finally
            {
                // Positionnement du Curseur
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Lance l'appel de la création d'un équipement
        /// </summary>
        public void CreateEquipement()
        {
            if (formEquipement != null)
                formEquipement.AddEquipement();
        }

        /// <summary>
        /// Lance l'appel de la création d'un site
        /// </summary>
        public void CreateSite()
        {
            if (formEquipement != null)
                formEquipement.AddSite();
        }

        /// <summary>
        /// Lance l'appel de la création d'un site
        /// </summary>
        public void CreateLogiciel()
        {
            if (formEquipement != null)
                formEquipement.AddLogiciel();
        }

        /// <summary>
        /// Lance l'édition de l'élément sélectionné en fonction du panneau affiché
        /// </summary>
        public void EditItem()
        {
            if (tabPage.SelectedTab.Name == "tabPageSession")
            {
                if (formSession != null)
                    formSession.EditItem();
            }
            else if (tabPage.SelectedTab.Name == "tabPageObjet")
            {
                if (formObjet != null)
                    formObjet.EditItem();
            }
            else if (tabPage.SelectedTab.Name == "tabPageEquipement")
            {
                if (formEquipement != null)
                    formEquipement.EditItem();
            }
        }

        /// <summary>
        /// Lance la suppression de l'élément sélectionné en fonction du panneau affiché
        /// </summary>
        public void DeleteItem()
        {
            if (tabPage.SelectedTab.Name == "tabPageSession")
            {
                if (formSession != null)
                    formSession.DeleteItem();
            }
            else if (tabPage.SelectedTab.Name == "tabPageObjet")
            {
                if (formObjet != null)
                    formObjet.DeleteItem();
            }
            else if (tabPage.SelectedTab.Name == "tabPageEquipement")
            {
                if (formEquipement != null)
                    formEquipement.DeleteItem();
            }
        }

        /// <summary>
        /// Lance la commande de sélection dans Stellarium pour l'objet sélectionné
        /// </summary>
        public void StellariumFocusTo(IObjObjetCeleste objetCeleste)
        {
            try
            {
                if (objetCeleste == null || string.IsNullOrEmpty(objetCeleste.Nom))
                    return;

                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la commande FocusTo de {ApplicationTools.Properties.Resources.Stellarium}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Avant de lancer le traitement, on disable le bouton pour éviter les double-clic
                IsStellariumRunning = true;
                BeginInvoke(new Action(() =>
                {
                    if (formSession != null)
                        formSession.SetStellariumButtonState(false);
                    if (formObjet != null)
                        formObjet.SetStellariumButtonState(false);
                }), null);

                // Lancement de la tâche de fond si aucune autre action en cours de traitement
                if (!backgroundWorkerStellarium.IsBusy)
                    backgroundWorkerStellarium.RunWorkerAsync(objetCeleste.Nom);
                else
                    factory.GetLog().Log($"backgroundWorkerStellarium BUSY", GetType().Name, null, TypeLog.Warning);

                // Trace
                factory.GetLog().Log($"Retour au process principal après {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (ApplicationTools.WarningException err)
            {
                // Sur WarningException, le background n'a pas été lancé, donc on remet le bouton Enable
                IsStellariumRunning = false;
                BeginInvoke(new Action(() =>
                {
                    if (formSession != null)
                        formSession.SetStellariumButtonState(true);
                    if (formObjet != null)
                        formObjet.SetStellariumButtonState(true);
                }), null);
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
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
        /// Traitement asynchrone de la commande de sélection dans Stellarium
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerStellarium_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Exécution asynchrone de la commande FocusTo de {ApplicationTools.Properties.Resources.Stellarium}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Status Text
                SetStatusActionText($"{ApplicationTools.Properties.Resources.Stellarium} : {Resources.EnvoiDeLaCommande}");

                // Récup du nom de l'objet passé en paramètre du Thread
                string nomTarget = (string)e.Argument;
                if (!string.IsNullOrEmpty(nomTarget))
                {
                    // On lance la commande
                    factory.GetAppStellarium().FocusTo(nomTarget, DateTime.Now);

                    // Trace
                    factory.GetLog().Log($"Exécution de la commande FocusTo de {ApplicationTools.Properties.Resources.Stellarium} pour l'objet {nomTarget} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                }
                else
                {
                    // Trace
                    factory.GetLog().Log($"Aucun objet sélectionné", GetType().Name, null, TypeLog.Warning);
                }

                // On flush le texte de la Status Text
                SetStatusActionText(string.Empty);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
                // On positionne le message d'erreur retour dans la Status Text
                SetStatusActionText($"{ApplicationTools.Properties.Resources.Stellarium} : {err.Message}");
            }
            finally
            {
                // Dans tous les cas, on remet le bouton Stellarium Enable
                IsStellariumRunning = false;
                BeginInvoke(new Action(() =>
                {
                    if (formSession != null)
                        formSession.SetStellariumButtonState(true);
                    if (formObjet != null)
                        formObjet.SetStellariumButtonState(true);
                }), null);
            }
        }

        /// <summary>
        /// Lance la commande de sélection dans CdC pour l'objet sélectionné
        /// </summary>
        public void CdCFocusTo(IObjObjetCeleste objetCeleste)
        {
            try
            {
                if (objetCeleste == null || string.IsNullOrEmpty(objetCeleste.Nom))
                    return;

                // Trace et Chrono
                factory.GetLog().Log($"Lancement de la commande FocusTo de {ApplicationTools.Properties.Resources.CartesDuCiel}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Avant de lancer le traitement, on disable le bouton pour éviter les double-clic
                IsCdCRunning = true;
                BeginInvoke(new Action(() =>
                {
                    if (formSession != null)
                        formSession.SetCdCButtonState(false);
                    if (formObjet != null)
                        formObjet.SetCdCButtonState(false);
                }), null);

                // Lancement de la tâche de fond si aucune autre action en cours de traitement
                if (!backgroundWorkerCdC.IsBusy)
                    backgroundWorkerCdC.RunWorkerAsync(objetCeleste.Nom);
                else
                    factory.GetLog().Log($"backgroundWorkerCdC BUSY", GetType().Name, null, TypeLog.Warning);

                // Trace
                factory.GetLog().Log($"Retour au process principal après {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (ApplicationTools.WarningException err)
            {
                // Sur WarningException, le background n'a pas été lancé, donc on remet le bouton Enable
                IsCdCRunning = false;
                BeginInvoke(new Action(() =>
                {
                    if (formSession != null)
                        formSession.SetCdCButtonState(true);
                    if (formObjet != null)
                        formObjet.SetCdCButtonState(true);
                }), null);
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
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
        /// Traitement asynchrone de la commande de sélection dans CdC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerCdC_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Exécution asynchrone de la commande FocusTo de {ApplicationTools.Properties.Resources.CartesDuCiel}", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Status Text
                SetStatusActionText($"{ApplicationTools.Properties.Resources.CartesDuCiel} : {Resources.EnvoiDeLaCommande}");

                // Récup du nom de l'objet passé en paramètre du Thread
                string nomTarget = (string)e.Argument;
                if (!string.IsNullOrEmpty(nomTarget))
                {
                    // On lance la commande
                    factory.GetAppCartesDuCiel().FocusTo(nomTarget, DateTime.Now);

                    // Trace
                    factory.GetLog().Log($"Exécution de la commande FocusTo de {ApplicationTools.Properties.Resources.CartesDuCiel} pour l'objet {nomTarget} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                }
                else
                {
                    // Trace
                    factory.GetLog().Log($"Aucun objet sélectionné", GetType().Name, null, TypeLog.Warning);
                }

                // On flush le texte de la Status Text
                SetStatusActionText(string.Empty);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
                // On positionne le message d'erreur retour dans la Status Text
                SetStatusActionText($"{ApplicationTools.Properties.Resources.CartesDuCiel} : {err.Message}");
            }
            finally
            {
                // Dans tous les cas, on remet le bouton Stellarium Enable
                IsCdCRunning = false;
                BeginInvoke(new Action(() =>
                {
                    if (formSession != null)
                        formSession.SetCdCButtonState(true);
                    if (formObjet != null)
                        formObjet.SetCdCButtonState(true);
                }), null);
            }
        }

        /// <summary>
        /// Permet la sélection d'un objet dans le formulaire
        /// <para>Flush les filtres et la sélection dans le TreeView</para>
        /// </summary>
        /// <param name="objetSelectionne"></param>
        /// <param name="activePane"></param>
        public void SelectObjetCeleste(IObjObjetCeleste objetSelectionne, bool activePane)
        {
            if (formObjet != null)
            {
                formObjet.SelectObjetCeleste(objetSelectionne);
                if (activePane)
                    tabPage.SelectTab(tabPageObjet.Name);
            }
        }

        /// <summary>
        /// Permet la sélection d'une session dans le formulaire
        /// <para>Flush les filtres et la sélection dans le TreeView</para>
        /// </summary>
        public void SelectSession(IObjSession sessionSelectionnee, bool activePane)
        {
            if (formSession != null)
            {
                formSession.SelectSession(sessionSelectionnee);
                if (activePane)
                    tabPage.SelectTab(tabPageSession.Name);
            }
        }

        /// <summary>
        /// Ouvre la boîte de dialogue A propos
        /// </summary>
        public void OpenAPropos()
        {
            try
            {
                // Ouverture de la boîte de dialogue APropos
                dlgAPropos dialogAPropos = new dlgAPropos(factory);
                dialogAPropos.ShowDialog();
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
        /// Ouvre la boîte de dialogue des Options
        /// </summary>
        public void OpenOptions()
        {
            try
            {
                // Ouverture de la boîte de dialogue Paramètres
                dlgOptions dialogOptions = new dlgOptions(factory);
                dialogOptions.ShowDialog();
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
        /// Ouvre l'application ATS
        /// </summary>
        public void OpenAstroTargetSelector()
        {
            try
            {
                factory.GetAppAstroTargetSelector().Start();
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
        /// Lance le processus de sauvegarde de la BDD
        /// </summary>
        public void SaveDataBase()
        {
            try
            {
                // Status Text
                SetStatusActionText(Resources.SauvegardeDeLaBaseDeDonneesDeLApplication);

                // Initialisation SaveFileDialog
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.FileName = "ASO_" + DateTime.Now.Date.ToString("yyyyMMdd");
                saveDlg.OverwritePrompt = true;
                saveDlg.DefaultExt = "db";
                saveDlg.AddExtension = true;
                saveDlg.Filter = $"{Resources.BaseDeDonnees} SQLite|*.db";
                saveDlg.InitialDirectory = factory.GetApplicationSauvegardeBDDPath();
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(factory.GetBDDFileName(), saveDlg.FileName, true);
                    // Trace de l'erreur et information à l'utilisateur
                    factory.GetLog().Log($"{Resources.SauvegardeDeLaBaseDeDonnees} [{saveDlg.FileName}] {Resources.EffectueeAvecSucces}");
                    MessageBox.Show($"{Resources.SauvegardeDeLaBaseDeDonnees}{Environment.NewLine}'{saveDlg.FileName}'{Environment.NewLine}{Resources.EffectueeAvecSucces}."
                                    , Application.ProductName
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Information);
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
                // Texte de la Status
                SetStatusActionText("");
            }
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objets
        /// </summary>
        private IAppObjFactory factory = null;

        /// <summary>
        /// Formulaire Listing des observations
        /// </summary>
        private FormSessions formSession = null;

        /// <summary>
        /// Formulaire Listing des objets
        /// </summary>
        private FormObjets formObjet = null;

        /// <summary>
        /// Formulaire Listing des équipements
        /// </summary>
        private FormEquipements formEquipement = null;

        /// <summary>
        /// Numéro de version d'update
        /// </summary>
        private string updateVersion = string.Empty;

        /// <summary>
        /// Nom d'update
        /// </summary>
        private string updateNom = string.Empty;

        /// <summary>
        /// Description d'update
        /// </summary>
        private string updateDescription = string.Empty;

        /// <summary>
        /// Url d'update
        /// </summary>
        private string updateUrl = string.Empty;

        #endregion

        private void MainFenetre_Load(object sender, EventArgs e)
        {
            // Initilisation de l'application
            InitialisationApplication();
        }

        private void MainFenetre_Shown(object sender, EventArgs e)
        {
            // Initilisation du Formulaire
            InitialisationFormulaire();
        }

        private void MainFenetre_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseObject();
        }

        private void toolStripButtonNewEquipement_Click(object sender, EventArgs e)
        {
            CreateEquipement();
        }

        private void toolStripButtonNewSetup_Click(object sender, EventArgs e)
        {
            if (formEquipement != null)
                formEquipement.AddSetup();
        }

        private void toolStripButtonNewSite_Click(object sender, EventArgs e)
        {
            CreateSite();
        }

        private void tabPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateTab();
        }

        private void toolStripButtonNewObjet_Click(object sender, EventArgs e)
        {
            if (formObjet != null)
                formObjet.AddObjetCeleste();
        }

        private void toolStripButtonNewSession_Click(object sender, EventArgs e)
        {
            if (formSession != null)
                formSession.AddSession();
            ActivateTab();
        }

        private void toolStripButtonNewLogiciel_Click(object sender, EventArgs e)
        {
            CreateLogiciel();
        }

        private void toolStripButtonModifier_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void toolStripButtonSupprimer_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAPropos();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenOptions();
        }

        private void toolStripButtonATS_Click(object sender, EventArgs e)
        {
            OpenAstroTargetSelector();
        }

        private void sauvegarderLaBaseDeDonnéesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDataBase();
        }

        private void MainFenetre_ClientSizeChanged(object sender, EventArgs e)
        {
            // Sauvegarde du WindowState dans les Settings
            WindowMaximized = WindowState == FormWindowState.Maximized;
        }
    }
}
