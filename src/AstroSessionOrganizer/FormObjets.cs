using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Collections;
using ApplicationTools;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Formulaire Listing des Observations
    /// </summary>
    public partial class FormObjets : Form
    {
        #region Constantes

        // TreeViewItems DejaObserve
        private const string TreeRootDejaObserve = "rootDejaObserve";
        private const string TreePrefixDejaObserve = "DejaObserve";
        private const string TreeImageKeyDejaObserve = "DejaObserve";
        private const string TreeImageKeyDejaObserveSelected = "DejaObserve";

        // TreeViewItems Constellations
        private const string TreeRootConstellation = "rootConstellation";
        private const string TreePrefixConstellation = "Constellation";
        private const string TreeImageKeyConstellation = "Constellation";
        private const string TreeImageKeyConstellationSelected = "Constellation";

        // TreeViewItems TypeObjets
        private const string TreeRootTypeObjets = "rootTypeObjet";
        private const string TreePrefixTypeObjets = "TypeObjet";
        private const string TreeImageKeyTypeObjets = "NonDefini";
        private const string TreeImageKeyTypeObjetsSelected = "NonDefini";

        #endregion

        #region Propriéts

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public string SortColumn
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.SortColumn))
                {
                    Properties.Settings.Default.SortColumn = "1";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"SortColumn non présent dans les Settings. Positionnement de 0 par défaut", GetType().Name);
                }
                return Properties.Settings.Default.SortColumn;
            }
            set
            {
                Properties.Settings.Default.SortColumn = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Ordre de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public string SortOrder
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.SortOrder))
                {
                    Properties.Settings.Default.SortOrder = "Descending";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"SortOrder non présent dans les Settings. Positionnement de Descending par défaut", GetType().Name);
                }
                return Properties.Settings.Default.SortOrder;
            }
            set
            {
                Properties.Settings.Default.SortOrder = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol0
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol0))
                {
                    Properties.Settings.Default.WidthCol0 = "25";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol0 non présent dans les Settings. Positionnement de 25 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol0);
            }
            set
            {
                Properties.Settings.Default.WidthCol0 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol1
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol1))
                {
                    Properties.Settings.Default.WidthCol1 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol1 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol1);
            }
            set
            {
                Properties.Settings.Default.WidthCol1 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol2
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol2))
                {
                    Properties.Settings.Default.WidthCol2 = "150";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol2 non présent dans les Settings. Positionnement de 150 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol2);
            }
            set
            {
                Properties.Settings.Default.WidthCol2 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol3
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol3))
                {
                    Properties.Settings.Default.WidthCol3 = "120";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol3 non présent dans les Settings. Positionnement de 120 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol3);
            }
            set
            {
                Properties.Settings.Default.WidthCol3 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol4
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol4))
                {
                    Properties.Settings.Default.WidthCol4 = "50";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol4 non présent dans les Settings. Positionnement de 50 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol4);
            }
            set
            {
                Properties.Settings.Default.WidthCol4 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol5
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol5))
                {
                    Properties.Settings.Default.WidthCol5 = "300";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol5 non présent dans les Settings. Positionnement de 300 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol5);
            }
            set
            {
                Properties.Settings.Default.WidthCol5 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol6
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol6))
                {
                    Properties.Settings.Default.WidthCol6 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol6 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol6);
            }
            set
            {
                Properties.Settings.Default.WidthCol6 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol7
        {
            get
            {
                Properties.Settings.Default.WidthCol7 = "80";
                Properties.Settings.Default.Save();
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol7))
                {
                    Properties.Settings.Default.WidthCol7 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol7 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol7);
            }
            set
            {
                Properties.Settings.Default.WidthCol7 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol8
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol8))
                {
                    Properties.Settings.Default.WidthCol8 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol8 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol8);
            }
            set
            {
                Properties.Settings.Default.WidthCol8 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol9
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol9))
                {
                    Properties.Settings.Default.WidthCol9 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol9 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol9);
            }
            set
            {
                Properties.Settings.Default.WidthCol9 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol10
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol10))
                {
                    Properties.Settings.Default.WidthCol10 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol10 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol10);
            }
            set
            {
                Properties.Settings.Default.WidthCol10 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol11
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol11))
                {
                    Properties.Settings.Default.WidthCol11 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol11 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol11);
            }
            set
            {
                Properties.Settings.Default.WidthCol11 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol12
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol12))
                {
                    Properties.Settings.Default.WidthCol12 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol12 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol12);
            }
            set
            {
                Properties.Settings.Default.WidthCol12 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol13
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol13))
                {
                    Properties.Settings.Default.WidthCol13 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol13 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol13);
            }
            set
            {
                Properties.Settings.Default.WidthCol13 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Colonne de tri pour la liste des Targets
        /// <para>Get : Récupère la valeur stockée en Settings</para>
        /// <para>Set : Positionne la valeur stockée en Settings</para>
        /// </summary>
        public int WidthCol14
        {
            get
            {
                // Valeur par défaut
                if (string.IsNullOrEmpty(Properties.Settings.Default.WidthCol14))
                {
                    Properties.Settings.Default.WidthCol14 = "80";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"WidthCol14 non présent dans les Settings. Positionnement de 80 par défaut", GetType().Name);
                }
                return Convert.ToInt32(Properties.Settings.Default.WidthCol14);
            }
            set
            {
                Properties.Settings.Default.WidthCol14 = value.ToString();
                Properties.Settings.Default.Save();
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public FormObjets(IAppObjFactory factory, MainFenetre caller)
        {
            InitializeComponent();
            this.factory = factory;
            this.caller = caller;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Initialisation du Formulaire principal
        /// </summary>
        public void InitialisationFormulaire()
        {
            try
            {
                // Chrono
                Stopwatch debutInitialisation = new Stopwatch();
                debutInitialisation.Start();

                // Trace
                factory.GetLog().Log("Fonction InitialisationFormulaire DEBUT", GetType().Name);

                // Positionnement du flag
                flushFormEnCours = true;

                // Avant tout, on masque le panel pour le chargement
                //splitContainerSecondary.Panel2Collapsed = true;

                // Création d'une instance du ListView column sorter et assignation à la liste
                lvwColumnSorter = new ListViewColumnSorter();
                listViewObjet.ListViewItemSorter = lvwColumnSorter;

                // Initialisation des composants du formulaire
                InitialisationListeTarget();
                InitialisationListeInfosSession();
                SetToolTips();

                // Initialisation des Combos Filtre
                InitialisationComboFiltreType();
                InitialisationComboFiltreCatalogue();
                InitialisationComboFiltreConstellation();
                InitialisationComboFiltreMagnitude();

                // Rempli la treeview
                RemplirTreeViewMain();

                // Chargement de la liste
                FlushFiltre();
                RechargeListeTarget();
                UpdatePaneInfo();

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
                // Sur erreur dans l'initialisation du formulaire, on quitte l'appli
                throw;
            }
            finally
            {
                // Positionnement du flag
                flushFormEnCours = false;
            }
        }

        /// <summary>
        /// Positionne les ToolTip
        /// </summary>
        private void SetToolTips()
        {
            // ToolTip Infos toolTipInfosBigListe
            toolTipInfosBigListe.ToolTipTitle = Resources.InformationsSurLeTriDeLaListeDesObjetsCelestes;
            toolTipInfosBigListe.SetToolTip(pictureBoxInfosListeNonTriable, Resources.PourDesRaisonsDePerformancesLeTriEstDesactivePourLesListesContenantPlusDe1000ObjetsCelestes);

            // ToolTip Bouton buttonDisplaySession 
            toolTipDisplaySession.SetToolTip(buttonDisplaySession, Resources.AfficherLaSessionDObservationsSelectionnee);

            // ToolTip Bouton buttonInfosStellarium
            toolTipStellarium.SetToolTip(buttonInfosStellarium, Resources.AfficherLObjetCelesteDansStellarium);

            // ToolTip Bouton buttonInfosCdC
            toolTipCdC.SetToolTip(buttonInfosCdC, Resources.AfficherLObjetCelesteDansCartesDuCiel);

            // ToolTip Bouton buttonInfosATS
            toolTipATS.SetToolTip(buttonInfosATS, Resources.AfficherLObjetCelesteDansAstroTargetSelector);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxFiltreType
        /// </summary>
        private void InitialisationComboFiltreType()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxFiltreType", GetType().Name);

            // CLear de la liste
            comboBoxFiltreType.Items.Clear();
            ComboBoxItems comboBoxItems = new ComboBoxItems();

            // Ajout de "Tous"
            ComboBoxItem newItemTous = comboBoxItems.NewItem(Resources.Tous, "-1");
            comboBoxItems.Rows.Add(newItemTous);

            // Rechargement depuis la liste chargée
            foreach (IObjTypeObjet filtreEnCours in factory.GetListeTypeObjets().OrderBy(o => o.Nom))
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(filtreEnCours.Nom, filtreEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxFiltreType.DisplayMember = "Text";
            comboBoxFiltreType.ValueMember = "Value";
            comboBoxFiltreType.DataSource = comboBoxItems;

            // Positionnement de "Tous" par défaut
            comboBoxFiltreType.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxFiltreType.Items.Count} Filtre de Type et sélection de l'élément : {comboBoxFiltreType.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxFiltreCatalogue
        /// </summary>
        private void InitialisationComboFiltreCatalogue()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxFiltreCatalogue", GetType().Name);

            // CLear de la liste
            comboBoxFiltreCatalogue.Items.Clear();
            ComboBoxItems comboBoxItems = new ComboBoxItems();

            // Ajout de "Tous"
            ComboBoxItem newItemTous = comboBoxItems.NewItem(Resources.Tous, "-1");
            comboBoxItems.Rows.Add(newItemTous);
            // Rechargement depuis la liste chargée
            foreach (IObjCatalogue catalogueEnCours in factory.GetListeCatalogues())
            {
                ComboBoxItem newItem = comboBoxItems.NewItem(catalogueEnCours.Nom, catalogueEnCours.Id);
                comboBoxItems.Rows.Add(newItem);
            }
            comboBoxFiltreCatalogue.DisplayMember = "Text";
            comboBoxFiltreCatalogue.ValueMember = "Value";
            comboBoxFiltreCatalogue.DataSource = comboBoxItems;

            // Positionnement de "Tous" par défaut
            comboBoxFiltreCatalogue.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxFiltreCatalogue.Items.Count} Filtre de Catalogue et sélection de l'élément : {comboBoxFiltreCatalogue.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxFiltreConstellation
        /// </summary>
        private void InitialisationComboFiltreConstellation()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxFiltreConstellation", GetType().Name);

            // CLear de la liste
            comboBoxFiltreConstellation.Items.Clear();

            // Ajout de "Tous"
            comboBoxFiltreConstellation.Items.Add(Resources.Tous);
            // Rechargement depuis la liste chargée
            foreach (IObjConstellation filtreEnCours in factory.GetListeConstellations().OrderBy(o => o.Nom))
            {
                comboBoxFiltreConstellation.Items.Add(filtreEnCours.Nom);
            }

            // Positionnement de "Tous" par défaut
            comboBoxFiltreConstellation.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxFiltreConstellation.Items.Count} Filtre de Constellation et sélection de l'élément : {comboBoxFiltreConstellation.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Permet l'initialisation de la Combo comboBoxFiltreMagnitude
        /// </summary>
        private void InitialisationComboFiltreMagnitude()
        {
            // Trace
            factory.GetLog().Log("Chargement de la comboBoxFiltreMagnitude", GetType().Name);

            // CLear de la liste
            comboBoxFiltreMagnitude.Items.Clear();

            // Rechargement depuis la liste chargée
            foreach (KeyValuePair<string, string> filtreEnCours in factory.GetListeFiltreMagnitude())
            {
                comboBoxFiltreMagnitude.Items.Add(filtreEnCours.Value);
            }

            // Positionnement de "Tous" par défaut
            comboBoxFiltreMagnitude.SelectedIndex = 0;

            // Trace
            factory.GetLog().Log($"Chargement de {comboBoxFiltreMagnitude.Items.Count} Filtre de Magnitude et sélection de l'élément : {comboBoxFiltreMagnitude.SelectedItem}", GetType().Name);
        }

        /// <summary>
        /// Fonction récursive permettant la sauvegarde de l'état des noeuds du TreeView
        /// </summary>
        /// <param name="parcoursNodes"></param>
        /// <param name="saveNodes"></param>
        /// <returns></returns>
        private ArrayList SaveTreeNodeState(TreeNodeCollection parcoursNodes, ArrayList saveNodes)
        {
            foreach (TreeNode nodeEnCours in parcoursNodes)
            {
                saveNodes.Add(nodeEnCours);
                // Appel récursif des noeuds enfants
                if (nodeEnCours.Nodes.Count > 0)
                    SaveTreeNodeState(nodeEnCours.Nodes, saveNodes);

            }

            return saveNodes;
        }

        /// <summary>
        /// Rempli la treeview
        /// </summary>
        private void RemplirTreeViewMain()
        {
            try
            {
                // On sauvegarde l'état expanded des nodes pour remise en état
                ArrayList oldNodes = new ArrayList();
                oldNodes = SaveTreeNodeState(treeViewObjet.Nodes, oldNodes);

                // Clear de la TreeView
                TreeNode selectedNode = treeViewObjet.SelectedNode;

                treeViewObjet.BeginUpdate();
                treeViewObjet.Nodes.Clear();

                // Déjà observés
                // Ajout de l'élément racine
                TreeNode rootDejaObserve = treeViewObjet.Nodes.Add(TreeRootDejaObserve, Resources.DejaObserves, TreeImageKeyDejaObserve, TreeImageKeyDejaObserveSelected);
                // Parcours des Sessions et ajout dans la TreeView
                var listeAnnee = factory.GetListeSession().ListeComplete
                    .OrderByDescending(x => x.DateHeure)
                    .GroupBy(s => s.DateHeure.Year).ToList();
                foreach (IGrouping<int, IObjSession> annee in listeAnnee)
                {
                    TreeNode nodeAnnee = rootDejaObserve.Nodes.Add(TreePrefixDejaObserve + annee.Key.ToString(), annee.Key.ToString(),
                        TreeImageKeyDejaObserve, TreeImageKeyDejaObserve);

                    // Pour chaque année, on ajoute les mois
                    var listeMois = factory.GetListeSession().ListeComplete
                        .OrderByDescending(x => x.DateHeure)
                        .Where(s => s.DateHeure.Year == annee.Key)
                        .GroupBy(s => s.DateHeure.Month).ToList();
                    foreach (IGrouping<int, IObjSession> mois in listeMois)
                    {
                        string nomMois = factory.GetMonthName(mois.Key);
                        nodeAnnee.Nodes.Add(TreePrefixDejaObserve + annee.Key.ToString() + mois.Key.ToString("00"), nomMois,
                            TreeImageKeyDejaObserve, TreeImageKeyDejaObserve);
                    }
                }

                // Constellations
                // Ajout de l'élément racine
                TreeNode rootConstellation = treeViewObjet.Nodes.Add(TreeRootConstellation, Resources.Constellations, TreeImageKeyConstellation, TreeImageKeyConstellationSelected);
                // Parcours des Objets et ajout dans la TreeView
                foreach (IObjConstellation coonstellationEnCours in factory.GetListeConstellations())
                {
                    rootConstellation.Nodes.Add(TreePrefixConstellation + coonstellationEnCours.Id, coonstellationEnCours.Nom, TreeImageKeyConstellation, TreeImageKeyConstellationSelected);
                }

                // Type d'objets
                // Ajout de l'élément racine
                TreeNode rootTypeObjets = treeViewObjet.Nodes.Add(TreeRootTypeObjets, Resources.TypeDObjetsCelestes, TreeImageKeyTypeObjets, TreeImageKeyTypeObjetsSelected);
                // Parcours des Objets et ajout dans la TreeView
                foreach (IObjTypeObjet typeobjetsEnCours in factory.GetListeTypeObjets().OrderBy(t => t.Nom))
                {
                    rootTypeObjets.Nodes.Add(TreePrefixTypeObjets + typeobjetsEnCours.Id, typeobjetsEnCours.Nom, typeobjetsEnCours.Icone, typeobjetsEnCours.Icone);
                }

                // On repositionne l'état des noeuds
                foreach (TreeNode nodeEnCours in oldNodes)
                {
                    if (nodeEnCours.IsExpanded)
                    {
                        TreeNode[] nodeResult = treeViewObjet.Nodes.Find(nodeEnCours.Name, true);
                        if (nodeResult.Length > 0)
                            nodeResult[0].Expand();
                    }
                }

                // Sélection de l'élément précedemment sélectionné
                if (selectedNode != null && string.IsNullOrEmpty(forceIdSelectedObjet))
                {
                    TreeNode[] nodeResult = treeViewObjet.Nodes.Find(selectedNode.Name, true);
                    if (nodeResult.Length > 0)
                        treeViewObjet.SelectedNode = nodeResult[0];
                }
                else
                {
                    treeViewObjet.SelectedNode = rootDejaObserve;
                }
                if (treeViewObjet.SelectedNode != null)
                    treeViewObjet.SelectedNode.EnsureVisible();

                // Clear de la collection temporaire des noeuds
                if (oldNodes != null)
                {
                    oldNodes.Clear();
                    oldNodes = null;
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
                treeViewObjet.EndUpdate();
            }
        }

        /// <summary>
        /// The basic VirtualMode function.  Dynamically returns a ListViewItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewObjet_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                if (listViewItems != null && listViewItems.Count > e.ItemIndex)
                {
                    e.Item = listViewItems[e.ItemIndex];
                }
                else
                    e.Item = new ListViewItem(new[]
                    {
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty
                    });
            }
            catch
            {
                e.Item = new ListViewItem(new[]
                {
                    "Erreur",
                    "RetrieveVirtualItem",
                    $"index : {e.ItemIndex}",
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty
                });
            }
        }

        /// <summary>
        /// Search Virtual Mode function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewObjet_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            try
            {
                e.Index = listViewItems.FindIndex(lv => lv.SubItems[1].Text == e.Text);

            }
            catch
            {
                e.Index = -1;
            }
        }

        /// <summary>
        /// Permet l'initialisation de la liste des Targets
        /// </summary>
        private void InitialisationListeTarget()
        {
            try
            {
                // Gestion mode Virtuel de la liste
                listViewObjet.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listViewObjet_RetrieveVirtualItem);
                listViewObjet.SearchForVirtualItem += new SearchForVirtualItemEventHandler(listViewObjet_SearchForVirtualItem);
                listViewObjet.VirtualMode = true;

                // Liste des objets ListViewItem contenu dans la liste
                listViewItems = new List<ListViewItem>();

                // Type de vue
                listViewObjet.View = View.Details;

                // Adjout des colonnes
                listViewObjet.Columns.Add("", WidthCol0, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.Nom, WidthCol1, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.Type, WidthCol2, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.Constellation, WidthCol3, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.IAU, WidthCol4, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.Denominations, WidthCol5, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.Catalogues, WidthCol6, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.RA, WidthCol7, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.DEC, WidthCol8, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.GrandeurMax, WidthCol9, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.GrandeurMin, WidthCol10, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.Magnitude, WidthCol11, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.MagnitudeBMag, WidthCol12, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.Redshift, WidthCol13, HorizontalAlignment.Left);
                listViewObjet.Columns.Add(Resources.DistanceMpc, WidthCol14, HorizontalAlignment.Left);

                // Tri par défaut
                if (!string.IsNullOrEmpty(SortColumn) && !string.IsNullOrEmpty(SortOrder))
                {
                    TriListeObjets(int.Parse(SortColumn));
                }

                // Trace
                factory.GetLog().Log("Initialisation de la liste des Targets effectuée avec succès", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Tri de la liste des Objets
        /// </summary>
        /// <param name="indexColonne">Index de la colonne à trier</param>
        /// <param name="forceOrder">Index de la colonne à trier</param>
        private void TriListeObjets(int indexColonne, SortOrder forceOrder = System.Windows.Forms.SortOrder.None)
        {
            try
            {
                // Trace
                factory.GetLog().Log($"Tri de la liste sur l'index de colonne : {indexColonne}", GetType().Name);

                // On ne tri pas la première colonne
                if (indexColonne == 0)
                    return;

                if (listViewItems.Count > 1000)
                {
                    listViewObjet.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                    return;
                }

                // Stop le rafraichissement afin d'accélérer le remplissage
                listViewObjet.BeginUpdate();

                // Reset de l'élément sélectionné
                listViewObjet.SelectedIndices.Clear();

                // Colonne actuellement en cours de tri ? Dans ce cas, on inverse le tri en cours
                if (indexColonne == lvwColumnSorter.SortColumn)
                {
                    if (forceOrder != System.Windows.Forms.SortOrder.None)
                    {
                        lvwColumnSorter.Order = forceOrder;
                    }
                    else
                    {
                        if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                            lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                        else
                            lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    // Sinon, on positionne le tri sur la colonne en cours en mode Ascendant
                    lvwColumnSorter.SortColumn = indexColonne;
                    if (forceOrder != System.Windows.Forms.SortOrder.None)
                    {
                        lvwColumnSorter.Order = forceOrder;
                    }
                    else
                    {
                        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }

                // On lance le tri de la liste
                listViewObjet.SetSortIcon(lvwColumnSorter.SortColumn, lvwColumnSorter.Order);
                if (listViewItems.Count <= 1000)
                {
                    listViewItems.Sort(new ListViewItemComparer(new int[] { lvwColumnSorter.SortColumn }, lvwColumnSorter.Order));
                    // MAJ des settings
                    SortColumn = lvwColumnSorter.SortColumn.ToString();
                    SortOrder = lvwColumnSorter.Order.ToString();
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
                // Réactive la liste
                listViewObjet.EndUpdate();
            }
        }

        /// <summary>
        ///  Permet la mise à jour de la liste avec la collection Targets
        /// </summary>
        public void RechargeListeTarget()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // Trace et Chrono
                factory.GetLog().Log("Chargement de la liste des targets", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Positionnement du Curseur et Status Action text
                caller.SetStatusActionText(Resources.ActualisationDeLaListeDesObjetsCelestes);
                //Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                actualisationListeEnCours = true;

                // Stop le rafraichissement afin d'accélérer le remplissage
                listViewObjet.BeginUpdate();

                // On récupère la target sélectionnée pour la resélectionnée après actialisation de la liste
                string selectedTarget = string.Empty;
                ListView.SelectedIndexCollection indexCollection = listViewObjet.SelectedIndices;
                if (indexCollection.Count > 0)
                    selectedTarget = listViewItems[indexCollection[0]].Name;
                if (!string.IsNullOrEmpty(forceIdSelectedObjet))
                    selectedTarget = forceIdSelectedObjet;
                forceIdSelectedObjet = string.Empty;
                listViewObjet.SelectedIndices.Clear();

                // Clear de la liste et parcours de la liste des Targets pour ajout
                listViewItems.Clear();
                listViewObjet.Items.Clear();
                ListViewItem itemSelected = null;

                // Restriction TreeView
                TreeNode nodeSelectionne = treeViewObjet.SelectedNode;
                factory.GetListeObjetCeleste().SelectedConstellation = string.Empty;
                factory.GetListeObjetCeleste().SelectedTypeObjet = string.Empty;
                string selectedDate = string.Empty;
                if (nodeSelectionne != null && nodeSelectionne.Name.StartsWith(TreePrefixConstellation))
                {
                    factory.GetListeObjetCeleste().SelectedConstellation = nodeSelectionne.Name.Replace(TreePrefixConstellation, "");
                }
                else if (nodeSelectionne != null && nodeSelectionne.Name.StartsWith(TreePrefixTypeObjets))
                {
                    factory.GetListeObjetCeleste().SelectedTypeObjet = nodeSelectionne.Name.Replace(TreePrefixTypeObjets, "");
                }
                else if (nodeSelectionne != null && nodeSelectionne.Name == TreeRootDejaObserve)
                {
                    selectedDate = "AllSelected";
                }
                else if (nodeSelectionne != null && nodeSelectionne.Name.StartsWith(TreePrefixDejaObserve))
                {
                    selectedDate = nodeSelectionne.Name.Replace(TreePrefixDejaObserve, "");
                }
                // En mode Filtre sur Type dans TreeView, on désactive le filtre sur Type dans Combo
                if(!string.IsNullOrEmpty(factory.GetListeObjetCeleste().SelectedTypeObjet))
                {
                    comboBoxFiltreType.SelectedValue = "-1";
                }
                comboBoxFiltreType.Enabled = string.IsNullOrEmpty(factory.GetListeObjetCeleste().SelectedTypeObjet);
                // Positionnement des filtres
                factory.GetListeObjetCeleste().FiltreNomDescription = textBoxFiltreNomDescription.Text;
                factory.GetListeObjetCeleste().FiltreIdType = comboBoxFiltreType.SelectedValue.ToString();
                factory.GetListeObjetCeleste().FiltreIdCatalogue = comboBoxFiltreCatalogue.SelectedValue.ToString();
                factory.GetListeObjetCeleste().FiltreMagnitude = comboBoxFiltreMagnitude.Text;

                List<IObjObjetCeleste> listeObjetsCeleste = factory.GetListeObjetCeleste().Liste;
                if (!string.IsNullOrEmpty(selectedDate) && selectedDate.Length == 4)
                {
                    listeObjetsCeleste = (from IObjObjetCeleste in factory.GetListeObjetCeleste().Liste
                                          join IObjSession in factory.GetListeSession().ListeComplete.Where(s => s.DateHeure.Year.ToString() == selectedDate)
                                          on IObjObjetCeleste.Id equals IObjSession.IdObjetCeleste
                                          select IObjObjetCeleste).Distinct().ToList();
                }
                else if (!string.IsNullOrEmpty(selectedDate) && selectedDate.Length == 6)
                {
                    int annee = int.Parse(selectedDate.Substring(0, 4));
                    int mois = int.Parse(selectedDate.Substring(4, 2));
                    listeObjetsCeleste = (from IObjObjetCeleste in factory.GetListeObjetCeleste().Liste
                                          join IObjSession in factory.GetListeSession().ListeComplete
                                            .Where(s => s.DateHeure.Year == annee && s.DateHeure.Month == mois)
                                          on IObjObjetCeleste.Id equals IObjSession.IdObjetCeleste
                                          select IObjObjetCeleste).Distinct().ToList();
                }
                else if (!string.IsNullOrEmpty(selectedDate) && selectedDate == "AllSelected")
                {
                    listeObjetsCeleste = (from IObjObjetCeleste in factory.GetListeObjetCeleste().Liste
                                          join IObjSession in factory.GetListeSession().ListeComplete
                                          on IObjObjetCeleste.Id equals IObjSession.IdObjetCeleste
                                          select IObjObjetCeleste).Distinct().ToList();
                }
                foreach (IObjObjetCeleste objetEnCours in listeObjetsCeleste)
                {
                    ListViewItem item = new ListViewItem()
                    {
                        Name = objetEnCours.Id,
                        BackColor = Color.Transparent,
                        ImageIndex = objetEnCours.IconeIndex
                    };
                    item.SubItems.Add(objetEnCours.Nom);
                    item.SubItems.Add(objetEnCours.TypeObjet.Nom);
                    item.SubItems.Add(objetEnCours.Constellation.Nom);
                    item.SubItems.Add(objetEnCours.Constellation.Abr.ToUpper());
                    item.SubItems.Add(objetEnCours.DenominationsFormated);
                    item.SubItems.Add(objetEnCours.CataloguesFormated);
                    item.SubItems.Add(objetEnCours.RA.FormatedString);
                    item.SubItems.Add(objetEnCours.DEC.FormatedString);
                    item.SubItems.Add(objetEnCours.GrandeurMaxFormated);
                    item.SubItems.Add(objetEnCours.GrandeurMinFormated);
                    item.SubItems.Add(objetEnCours.MAG_VISUAL.HasValue ? objetEnCours.MAG_VISUAL.ToString() : string.Empty);
                    item.SubItems.Add(objetEnCours.MAG_PHOTO.HasValue ? objetEnCours.MAG_PHOTO.ToString() : string.Empty);
                    item.SubItems.Add(objetEnCours.REDSHIFT.HasValue ? objetEnCours.REDSHIFT.ToString() : string.Empty);
                    item.SubItems.Add(objetEnCours.DISTANCE_RS.HasValue ? objetEnCours.DISTANCE_RS.ToString() : string.Empty);

                    // Sélection de l'Item si nécessaire
                    if (!string.IsNullOrEmpty(selectedTarget) && selectedTarget == objetEnCours.Id)
                    {
                        item.Selected = true;
                        item.Focused = true;
                        itemSelected = item;
                    }
                    else
                    {
                        item.Selected = false;
                        item.Focused = false;
                    }
                    // Ajout de l'Item dans la liste
                    listViewItems.Add(item);
                }

                // Repositionnement du Tri
                pictureBoxInfosListeNonTriable.Visible = listViewItems.Count > 1000;
                if (listViewItems.Count > 1000)
                {
                    listViewObjet.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                }
                else
                {
                    listViewObjet.HeaderStyle = ColumnHeaderStyle.Clickable;
                    TriListeObjets(int.Parse(SortColumn), SortOrder == "Descending" ? System.Windows.Forms.SortOrder.Descending : System.Windows.Forms.SortOrder.Ascending);
                    //listViewItems.Sort(new ListViewItemComparer(new int[] { lvwColumnSorter.SortColumn }, lvwColumnSorter.Order));
                }

                // AutoFit des colonnes et affichage de l'élément sélectionné
                //FlushPaneInfo();
                listViewObjet.FullRowSelect = true;

                // Nombre d'éléments dans la liste virtuelle
                listViewObjet.VirtualListSize = 0;
                listViewObjet.VirtualListSize = listViewItems.Count;

                // Sélection de l'objet souhaité
                if (itemSelected != null && !string.IsNullOrEmpty(itemSelected.Name))
                {
                    ListViewItem[] item = listViewObjet.Items.Find(itemSelected.Name, false);
                    if (item != null && item.Length > 0)
                    {
                        listViewObjet.SelectedIndices.Add(item[0].Index);
                        item[0].EnsureVisible();
                    }
                }

                // Trace
                factory.GetLog().Log($"Chargement de la liste des {listViewObjet.Items.Count} targets effectué en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                caller.SetStatusActionText(string.Empty);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
            finally
            {
                // Réactive la liste
                listViewObjet.EndUpdate();
                actualisationListeEnCours = false;
                // Texte de la Status
                caller.SetStatusActionText(string.Empty);
                SetStatusText();
                // Positionnement du Curseur
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Selection d'un nouvel objet dans la liste
        /// </summary>
        private void SelectItem()
        {
            try
            {
                UpdatePaneInfo();
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Permet l'initialisation de la liste des sessions
        /// </summary>
        private void InitialisationListeInfosSession()
        {
            try
            {
                // Type de vue
                listViewInfosSessions.View = View.Details;

                // Adjout des colonnes
                listViewInfosSessions.Columns.Add("", 0, HorizontalAlignment.Left);
                listViewInfosSessions.Columns.Add(Resources.Date, 60, HorizontalAlignment.Left);
                listViewInfosSessions.Columns.Add(Resources.Site, 80, HorizontalAlignment.Left);
                listViewInfosSessions.Columns.Add(Resources.Setup, 80, HorizontalAlignment.Left);
                listViewInfosSessions.Columns.Add(Resources.TpsTotalBrutes, 60, HorizontalAlignment.Left);
                listViewInfosSessions.Columns.Add(Resources.Commentaires, 120, HorizontalAlignment.Left);

                // Trace
                factory.GetLog().Log("Initialisation de la liste des sessions effectuée avec succès", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Clear du panneau d'informations
        /// </summary>
        private void FlushPaneInfo()
        {
            try
            {
                // Trace
                //factory.GetLog().Log($"Flush du Panneau d'informations", GetType().Name);

                // Constellation
                pictureBoxInfosImageConstellation.Visible = false;
                textBoxInfosNomConstellation.Text = string.Empty;
                textBoxInfosIAUConstellation.Text = string.Empty;
                textBoxInfosConstellationRA.Text = string.Empty;
                textBoxInfosConstellationDEC.Text = string.Empty;

                // Objet
                labelInfosNomObjetCeleste.Text = string.Empty;
                textBoxInfosObjetType.Text = string.Empty;
                textBoxInfosObjetDenomination.Text = string.Empty;
                textBoxInfosObjetDenomination.Text = string.Empty;
                textBoxInfosObjetCatalogue.Text = string.Empty;
                textBoxInfosObjetRA.Text = string.Empty;
                textBoxInfosObjetDEC.Text = string.Empty;
                textBoxInfosObjetGrandeurMax.Text = string.Empty;
                textBoxInfosObjetGrandeurMin.Text = string.Empty;
                textBoxInfosObjetMagnitude.Text = string.Empty;
                textBoxInfosObjetMagnitudeB.Text = string.Empty;
                textBoxInfosObjetRedShift.Text = string.Empty;
                textBoxInfosObjetDistance.Text = string.Empty;

                // Icone Type Objet
                pictureBoxInfosTypeObjetNonDefini.Visible = false;
                pictureBoxInfosTypeObjetStar.Visible = false;
                pictureBoxInfosTypeObjetMultipleStars.Visible = false;
                pictureBoxInfosTypeObjetGalaxie.Visible = false;
                pictureBoxInfosTypeObjetNebuleuse.Visible = false;
                pictureBoxInfosTypeObjetCluster.Visible = false;
                pictureBoxInfosTypeObjetPlanete.Visible = false;
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Mise à jour du panneau d'informations
        /// </summary>
        public void UpdatePaneInfo()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // Trace et Chrono
                //factory.GetLog().Log($"Rechargement du Panneau d'informations sur l'objet sélectionné", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Flush du panneau
                FlushPaneInfo();
                SetToolBarEditButtonState();

                // Boutons Action
                SetStellariumButtonState(caller.IsStellariumInstalled && !caller.IsStellariumRunning);
                SetCdCButtonState(caller.IsCdCInstalled && !caller.IsCdCRunning);
                SetATSButtonState(caller.IsAstroTargetSelectorInstalled);

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewObjet.SelectedIndices;
                IObjObjetCeleste objetSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    objetSelectionne = factory.GetListeObjetCeleste().GetObjetCeleste(listViewItems[indexCollection[0]].SubItems[1].Text);
                }

                splitContainerSecondary.Panel2Collapsed = objetSelectionne == null;
                if (objetSelectionne != null)
                {
                    // Image et infos Constellation
                    string url = objetSelectionne.Constellation.DisplayThumbnailPosition;
                    if (File.Exists(url))
                    {
                        pictureBoxInfosImageConstellation.CancelAsync();
                        pictureBoxInfosImageConstellation.LoadAsync(url);
                        pictureBoxInfosImageConstellation.Visible = true;
                    }
                    textBoxInfosNomConstellation.Text = objetSelectionne.Constellation.Nom;
                    textBoxInfosIAUConstellation.Text = objetSelectionne.Constellation.Abr.ToUpper();
                    textBoxInfosConstellationRA.Text = objetSelectionne.Constellation.RA.FormatedString;
                    textBoxInfosConstellationDEC.Text = objetSelectionne.Constellation.DEC.FormatedString;

                    // Infos objet
                    labelInfosNomObjetCeleste.Text = objetSelectionne.Nom;
                    textBoxInfosObjetType.Text = objetSelectionne.TypeObjet.Nom;
                    textBoxInfosObjetDenomination.Text = objetSelectionne.DenominationsFormated;
                    textBoxInfosObjetDenomination.Text = objetSelectionne.DenominationsFormated;
                    textBoxInfosObjetCatalogue.Text = objetSelectionne.CataloguesFormated;
                    textBoxInfosObjetRA.Text = objetSelectionne.RA.FormatedString;
                    textBoxInfosObjetDEC.Text = objetSelectionne.DEC.FormatedString;
                    textBoxInfosObjetGrandeurMax.Text = objetSelectionne.GrandeurMaxFormated;
                    textBoxInfosObjetGrandeurMin.Text = objetSelectionne.GrandeurMinFormated;
                    textBoxInfosObjetMagnitude.Text = objetSelectionne.MAG_VISUAL.HasValue ? objetSelectionne.MAG_VISUAL.ToString() : string.Empty;
                    textBoxInfosObjetMagnitudeB.Text = objetSelectionne.MAG_PHOTO.HasValue ? objetSelectionne.MAG_PHOTO.ToString() : string.Empty;
                    textBoxInfosObjetRedShift.Text = objetSelectionne.REDSHIFT.HasValue ? objetSelectionne.REDSHIFT.ToString() : string.Empty;
                    textBoxInfosObjetDistance.Text = objetSelectionne.DISTANCE_RS.HasValue ? objetSelectionne.DISTANCE_RS.ToString() : string.Empty;

                    // Icone Type Objet
                    pictureBoxInfosTypeObjetNonDefini.Visible = objetSelectionne.TypeObjet.Icone == "NonDefini";
                    pictureBoxInfosTypeObjetStar.Visible = objetSelectionne.TypeObjet.Icone == "Star";
                    pictureBoxInfosTypeObjetMultipleStars.Visible = objetSelectionne.TypeObjet.Icone == "MultipleStars";
                    pictureBoxInfosTypeObjetGalaxie.Visible = objetSelectionne.TypeObjet.Icone == "Galaxie";
                    pictureBoxInfosTypeObjetNebuleuse.Visible = objetSelectionne.TypeObjet.Icone == "Nebuleuse";
                    pictureBoxInfosTypeObjetCluster.Visible = objetSelectionne.TypeObjet.Icone == "Cluster";
                    pictureBoxInfosTypeObjetPlanete.Visible = objetSelectionne.TypeObjet.Icone == "Planete";

                    // Liste des sessions
                    listViewInfosSessions.BeginUpdate();
                    listViewInfosSessions.Items.Clear();
                    foreach (IObjSession session in factory.GetListeSession().ListeComplete
                                                                            .OrderByDescending(sess => sess.DateHeure)
                                                                            .Where(sess => sess.IdObjetCeleste == objetSelectionne.Id))
                    {
                        ListViewItem item = new ListViewItem()
                        {
                            Name = session.Id
                        };
                        item.SubItems.Add($"{session.DateHeure.ToString("d")}");
                        item.SubItems.Add(session.NomSite);
                        item.SubItems.Add(session.NomSetup);
                        item.SubItems.Add(session.FormatedTempsTotalObservations);
                        item.SubItems.Add(session.Comment);
                        listViewInfosSessions.Items.Add(item);
                    }
                    listViewInfosSessions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    listViewInfosSessions.Columns[0].Width = 0;
                    buttonDisplaySession.Enabled = false;
                    UpdateImageSession();
                    // Si y'a au moins une session, on sélectionne par défaut la première
                    if (listViewInfosSessions.Items.Count > 0)
                        listViewInfosSessions.Items[0].Selected = true;

                    listViewInfosSessions.EndUpdate();

                    // Trace
                    //factory.GetLog().Log($"Chargement du Panneau d'informations pour l'objet {objetSelectionne.Nom} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Flush de la zone des filtres
        /// </summary>
        private void FlushFiltre()
        {
            try
            {
                // Flush des filtres de l'objet applicatif

                // Flush zones des filtres
                textBoxFiltreNomDescription.Text = string.Empty;
                comboBoxFiltreType.SelectedIndex = 0;
                comboBoxFiltreConstellation.SelectedIndex = 0;
                comboBoxFiltreCatalogue.SelectedIndex = 0;
                comboBoxFiltreMagnitude.SelectedIndex = 0;
                checkBoxFiltreHasSession.Checked = false;
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Positionne le texte de la status pour le contexte du formulaire
        /// </summary>
        public void SetStatusText()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                string statusText = string.Empty;
                if (listViewObjet != null)
                {
                    string objetRepertorie = Resources.ObjetsRepertories;
                    string objetAffiche = Resources.ObjetAffiche;
                    if (listViewItems.Count > 1)
                        objetAffiche = Resources.ObjetsAffiches;
                    statusText = $"{factory.GetListeObjetCeleste().NombreObjetsRepertories} {objetRepertorie} : {listViewItems.Count} {objetAffiche}";
                }
                caller.SetStatusDefaultText(statusText);
                factory.GetLog().Log($"Positionnement du texte de la Status [{statusText}]", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Edition d'un élément de la liste
        /// </summary>
        public void EditItem()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewObjet.SelectedIndices;
                IObjObjetCeleste objetSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    objetSelectionne = factory.GetListeObjetCeleste().GetObjetCeleste(listViewItems[indexCollection[0]].SubItems[1].Text);
                    if (objetSelectionne != null)
                    {
                        // Trace
                        factory.GetLog().Log($"{Resources.EditionDeLObjetCeleste} : {objetSelectionne.Nom}", GetType().Name);

                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.EditionDUnObjetCeleste);
                        // Lancement Edition
                        dlgObjetCeleste dlgEdition = new dlgObjetCeleste(factory, objetSelectionne);
                        if (dlgEdition.ShowDialog() == DialogResult.OK)
                            RechargeListeTarget();
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
                caller.SetStatusActionText(string.Empty);
            }
        }

        /// <summary>
        /// Suppression de l'élément sélectionné dans la liste
        /// </summary>
        public void DeleteItem()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewObjet.SelectedIndices;
                IObjObjetCeleste objetSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    objetSelectionne = factory.GetListeObjetCeleste().GetObjetCeleste(listViewItems[indexCollection[0]].SubItems[1].Text);
                    if (objetSelectionne != null)
                    {
                        // Trace
                        factory.GetLog().Log($"{Resources.SuppressionDeLObjetCeleste} : {objetSelectionne.Nom}", GetType().Name);

                        // On ne supprime pas un objet du catalogue initial
                        if (objetSelectionne.CatalogueInitial)
                        {
                            string messageOrigin = Resources.IlEstImpossibleDeModifierOuDeSupprimerUnObjetCelesteDuCatalogueDOrigine;
                            MessageBox.Show(messageOrigin
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Information);
                            return;
                        }
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.SuppressionDUnObjetCeleste);
                        // Lancement Suppression
                        string message = $"{Resources.VoulezVousSupprimerLObjetCeleste} '{objetSelectionne.Nom}' ?" + Environment.NewLine
                                        + Resources.LesSessionsEtObservationsAssocieesACetObjetCelesteSerontEgalementSupprimees;
                        if (MessageBox.Show(message
                            , Application.ProductName
                            , MessageBoxButtons.YesNo
                            , MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            factory.DeleteObjetCeleste(objetSelectionne);
                            RechargeListeTarget();
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
                caller.SetStatusActionText(string.Empty);
            }
        }

        /// <summary>
        /// Ajout d'un nouvel Objet céleste
        /// </summary>
        public void AddObjetCeleste()
        {
            try
            {
                // Positionnement du texte de la Status
                caller.SetStatusActionText(Resources.CreationDUnObjetCeleste);
                // Lancement Création
                dlgObjetCeleste dlgEdition = new dlgObjetCeleste(factory);
                if (dlgEdition.ShowDialog() == DialogResult.OK)
                {
                    RechargeListeTarget();
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
                caller.SetStatusActionText(string.Empty);
            }
        }

        /// <summary>
        /// Positionne l'état des boutons d'édition de la toolbar
        /// </summary>
        public void SetToolBarEditButtonState()
        {
            if (listViewItems != null && caller != null && listViewObjet != null && listViewObjet.SelectedIndices != null)
                caller.SetToolBarEditButtonState(listViewObjet.SelectedIndices.Count == 1);
        }

        /// <summary>
        /// Permet la sélection d'un objet dans le formulaire
        /// <para>Flush les filtres et la sélection dans le TreeView</para>
        /// </summary>
        /// <param name="objetSelectionne"></param>
        public void SelectObjetCeleste(IObjObjetCeleste objetSelectionne)
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;
                if (objetSelectionne != null)
                {
                    forceIdSelectedObjet = objetSelectionne.Id;

                    // Flush des filtres
                    FlushFiltre();

                    // Trace
                    factory.GetLog().Log($"Sélection de l'objet céleste : {objetSelectionne.Nom}", GetType().Name);
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                flushFormEnCours = false;
                factory.GetLog().LogException(err, GetType().Name);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Permet la sélection d'une session dans le formulaire
        /// </summary>
        private void SelectSession()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // On vérifie qu'on a bien une session de sélectionnée
                if (listViewInfosSessions.SelectedItems.Count == 1)
                {
                    ListViewItem selectedItem = listViewInfosSessions.SelectedItems[0];
                    if (selectedItem != null)
                    {
                        IObjSession sessionSelectionnee = factory.GetListeSession().ListeComplete.Where(sess => sess.Id == selectedItem.Name).FirstOrDefault();
                        if (sessionSelectionnee != null)
                        {
                            if (caller != null)
                            {
                                splitContainerSecondary.Panel2Collapsed = true;
                                caller.SelectSession(sessionSelectionnee, true);
                            }
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
        /// Actualisation de l'affichage
        /// </summary>
        public void UpdateForm()
        {
            flushFormEnCours = true;

            // Flush de la TreeView
            RemplirTreeViewMain();
            RechargeListeTarget();
            UpdatePaneInfo();

            flushFormEnCours = false;
        }

        /// <summary>
        /// Positionne l'état du bouton Afficher la session
        /// </summary>
        private void SetSelectSessionButtonState()
        {
            buttonDisplaySession.Enabled = listViewInfosSessions.SelectedItems.Count == 1;
        }

        /// <summary>
        /// Positionne l'image de la session sélectionnée
        /// </summary>
        private void UpdateImageSession()
        {
            try
            {
                pictureBoxImageSession.ImageLocation = string.Empty;

                // On vérifie qu'on a bien une session de sélectionnée
                if (listViewInfosSessions.SelectedItems.Count == 1)
                {
                    ListViewItem selectedItem = listViewInfosSessions.SelectedItems[0];
                    if (selectedItem != null)
                    {
                        IObjSession sessionSelectionnee = factory.GetListeSession().ListeComplete.Where(sess => sess.Id == selectedItem.Name).FirstOrDefault();
                        if (sessionSelectionnee != null)
                        {
                            //LoadAsyncImageSession(sessionSelectionnee.DisplayThumbnail);
                            pictureBoxImageSession.ImageLocation = sessionSelectionnee.ResizedDisplayThumbnail;
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
        /// Ouvre l'image e la session
        /// </summary>
        private void OpenImageSession()
        {
            try
            {
                // On vérifie qu'on a bien une session de sélectionnée
                if (listViewInfosSessions.SelectedItems.Count == 1)
                {
                    ListViewItem selectedItem = listViewInfosSessions.SelectedItems[0];
                    if (selectedItem != null)
                    {
                        IObjSession sessionSelectionnee = factory.GetListeSession().ListeComplete.Where(sess => sess.Id == selectedItem.Name).FirstOrDefault();
                        if (sessionSelectionnee != null)
                        {
                            if (!string.IsNullOrEmpty(sessionSelectionnee.DisplayThumbnail))
                                Process.Start(sessionSelectionnee.DisplayThumbnail);
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
        /// Positionne en BeginInvoke l'état du bouton Stellarium
        /// </summary>
        /// <param name="enable"></param>
        public void SetStellariumButtonState(bool enable)
        {
            BeginInvoke(new Action(() => buttonInfosStellarium.Enabled = enable), null);
        }

        /// <summary>
        /// Positionne en BeginInvoke l'état du bouton CdC
        /// </summary>
        /// <param name="enable"></param>
        public void SetCdCButtonState(bool enable)
        {
            BeginInvoke(new Action(() => buttonInfosCdC.Enabled = enable), null);
        }

        /// <summary>
        /// Positionne en BeginInvoke l'état du bouton ATS
        /// </summary>
        /// <param name="enable"></param>
        public void SetATSButtonState(bool enable)
        {
            BeginInvoke(new Action(() => buttonInfosATS.Enabled = enable), null);
        }

        /// <summary>
        /// Positionne l'hétat du menu contextuel
        /// </summary>
        private void SetContextMenuState()
        {
            try
            {
                modifierToolStripMenuItem.Enabled = listViewObjet.SelectedIndices.Count == 1;
                supprimerToolStripMenuItem.Enabled = listViewObjet.SelectedIndices.Count == 1;
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Ouvre l'objet céleste dans Stellarium
        /// </summary>
        private void CallStellarium()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewObjet.SelectedIndices;
                IObjObjetCeleste objetSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    objetSelectionne = factory.GetListeObjetCeleste().GetObjetCeleste(listViewItems[indexCollection[0]].SubItems[1].Text);
                    if (objetSelectionne != null)
                    {
                        if (caller != null)
                            caller.StellariumFocusTo(objetSelectionne);
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
        /// Ouvre l'objet céleste dans CdC
        /// </summary>
        private void CallCdC()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewObjet.SelectedIndices;
                IObjObjetCeleste objetSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    objetSelectionne = factory.GetListeObjetCeleste().GetObjetCeleste(listViewItems[indexCollection[0]].SubItems[1].Text);
                    if (objetSelectionne != null)
                    {
                        if (caller != null)
                            caller.CdCFocusTo(objetSelectionne);
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

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        /// <summary>
        /// Référence sur la MainFenetre appelante
        /// </summary>
        private readonly MainFenetre caller = null;

        /// <summary>
        /// IComparer permettant le tri de la listview
        /// </summary>
        private ListViewColumnSorter lvwColumnSorter = null;

        /// <summary>
        /// Liste des éléments de la liste
        /// </summary>
        private List<ListViewItem> listViewItems = null;

        /// <summary>
        /// Flag permettant de savoir si le formulaire en cours de rechargement
        /// </summary>
        private bool flushFormEnCours = false;

        /// <summary>
        /// Flag permettant de savoir si l'actualisation de la Liste est en cours afin de stopper la transmission des events de modification des contrôles
        /// </summary>
        private bool actualisationListeEnCours = false;

        /// <summary>
        /// Positionne l'id de l'objet à sélectionner pour les demandes externes
        /// </summary>
        private string forceIdSelectedObjet = string.Empty;

        #endregion

        private void FormObjetListing_Load(object sender, EventArgs e)
        {
            // Avant tout, on masque le panel pour le chargement
            splitContainerSecondary.Panel2Collapsed = true;
        }

        private void treeViewObjet_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!flushFormEnCours && !actualisationListeEnCours)
            {
                actualisationListeEnCours = true;
                textBoxFiltreNomDescription.Text = string.Empty;
                RechargeListeTarget();
            }
        }

        private void textBoxFiltreNomDescription_TextChanged(object sender, EventArgs e)
        {
            if (!flushFormEnCours && !actualisationListeEnCours)
                RechargeListeTarget();
        }

        private void listViewObjet_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            TriListeObjets(e.Column);
        }

        private void listViewObjet_SelectedIndexChanged(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                SelectItem();
            }), null);
        }

        private void pictureBoxInfosImageConstellation_Click(object sender, EventArgs e)
        {
            if (pictureBoxInfosImageConstellation.Image != null && !string.IsNullOrEmpty(pictureBoxInfosImageConstellation.ImageLocation))
                Process.Start(pictureBoxInfosImageConstellation.ImageLocation);
        }

        private void comboBoxFiltreType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flushFormEnCours)
                RechargeListeTarget();
        }

        private void comboBoxFiltreMagnitude_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flushFormEnCours)
                RechargeListeTarget();
        }

        private void checkBoxFiltreHasSession_CheckedChanged(object sender, EventArgs e)
        {
            if (!flushFormEnCours)
                RechargeListeTarget();
        }

        private void comboBoxFiltreCatalogue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flushFormEnCours)
                RechargeListeTarget();
        }

        private void listViewObjet_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
        }

        private void listViewObjet_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    EditItem();
                    break;
                case Keys.Delete:
                    DeleteItem();
                    break;
                default:
                    break;
            }
        }

        private void listViewInfosSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectSessionButtonState();
            UpdateImageSession();
        }

        private void pictureBoxImageSession_Click(object sender, EventArgs e)
        {
            OpenImageSession();
        }

        private void buttonDisplaySession_Click(object sender, EventArgs e)
        {
            SelectSession();
        }

        private void buttonFiltreReset_Click(object sender, EventArgs e)
        {
            textBoxFiltreNomDescription.Text = string.Empty;
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddObjetCeleste();
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void contextMenuStripObjetCeleste_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetContextMenuState();
        }

        private void buttonInfosStellarium_Click(object sender, EventArgs e)
        {
            CallStellarium();
        }

        private void buttonInfosCdC_Click(object sender, EventArgs e)
        {
            CallCdC();
        }

        private void listViewInfosSessions_DoubleClick(object sender, EventArgs e)
        {
            SelectSession();
        }
    }
}
