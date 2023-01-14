using System;
using System.Collections;
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
    /// Formulaire Listing des Observations
    /// </summary>
    public partial class FormSessions : Form
    {
        #region Constantes

        // TreeViewItems Date
        private const string TreeRootDate = "rootDate";
        private const string TreePrefixDate = "Date";
        private const string TreeImageKeyDate = "Date";
        private const string TreeImageKeyDateSelected = "Date";

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
                if (string.IsNullOrEmpty(Properties.Settings.Default.SortColumnSession))
                {
                    Properties.Settings.Default.SortColumnSession = "1";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"SortColumnSession non présent dans les Settings. Positionnement de 0 par défaut", GetType().Name);
                }
                return Properties.Settings.Default.SortColumnSession;
            }
            set
            {
                Properties.Settings.Default.SortColumnSession = value;
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
                if (string.IsNullOrEmpty(Properties.Settings.Default.SortOrderSession))
                {
                    Properties.Settings.Default.SortOrderSession = "Descending";
                    Properties.Settings.Default.Save();
                    factory.GetLog().Log($"SortOrderSession non présent dans les Settings. Positionnement de Descending par défaut", GetType().Name);
                }
                return Properties.Settings.Default.SortOrderSession;
            }
            set
            {
                Properties.Settings.Default.SortOrderSession = value;
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
        public FormSessions(IAppObjFactory factory, MainFenetre caller)
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

                // Création d'une instance du ListView column sorter et assignation à la liste
                lvwColumnSorter = new ListViewColumnSorter();
                listViewSession.ListViewItemSorter = lvwColumnSorter;

                // Initialisation des composants du formulaire
                InitialisationListeSession();
                InitialisationListeInfosObservation();
                InitialisationListeInfosEquipement();
                SetToolTips();

                // Rempli la treeview
                RemplirTreeViewMain();
                RechargeListeSession();

                // Actualisation du panneau d'informations
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
            // ToolTip Bouton buttonDisplayObjetCeleste 
            toolTipSelectObjetCeleste.SetToolTip(buttonDisplayObjetCeleste, Resources.AfficherLObjetCeleste);

            // ToolTip Bouton buttonImageSiteAdd
            toolTipAddImage.SetToolTip(buttonImageSiteAdd, Resources.ModifierLImageParDefautDeLaSession);

            // ToolTip Bouton buttonImageSiteRemove
            toolTipDeleteImage.SetToolTip(buttonImageSiteRemove, Resources.RemettreLImageParDefautDeLaSession);

            // ToolTip Bouton buttonInfosStellarium
            toolTipStellarium.SetToolTip(buttonInfosStellarium, Resources.AfficherLObjetCelesteDansStellarium);

            // ToolTip Bouton buttonInfosCdC
            toolTipCdC.SetToolTip(buttonInfosCdC, Resources.AfficherLObjetCelesteDansCartesDuCiel);

            // ToolTip Bouton buttonInfosATS
            toolTipATS.SetToolTip(buttonInfosATS, Resources.AfficherLObjetCelesteDansAstroTargetSelector);

            // ToolTip pictureBoxInfosExif 
            toolTipInfosImagesPath.ToolTipTitle = Resources.InformationsSurLesImagesEtEXIF;
            string toolTipInfoImagesPath = Resources.VousDevezDefinirLeRepertoireDesImagesDeLaSessionPourAfficherLesFonctionnalites;
            toolTipInfosImagesPath.SetToolTip(pictureBoxInfosExif, toolTipInfoImagesPath);

            // ToolTip pictureBoxInfosImageCustom 
            toolTipInfosImageCustom.ToolTipTitle = Resources.InformationsSurLesImagesPersonnalisees;
            string toolTipInfoImageCustom = Resources.VousPouvezSpecifierUneImagePersonnaliseePourLaSession
                    + Environment.NewLine + Resources.ParDefautLImageUtiliseeEstLaPremiereDuRepertoireDesImagesDeLaSession;
            toolTipInfosImageCustom.SetToolTip(pictureBoxInfosImageCustom, toolTipInfoImageCustom);
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
                oldNodes = SaveTreeNodeState(treeViewSession.Nodes, oldNodes);

                // Clear de la TreeView
                TreeNode selectedNode = treeViewSession.SelectedNode;

                treeViewSession.BeginUpdate();
                treeViewSession.Nodes.Clear();

                // Date
                // Ajout de l'élément racine
                TreeNode rootDejaObserve = treeViewSession.Nodes.Add(TreeRootDate, Resources.Date, TreeImageKeyDate, TreeImageKeyDateSelected);
                // Parcours des Sessions et ajout dans la TreeView
                var listeAnnee = factory.GetListeSession().ListeComplete
                    .OrderByDescending(x => x.DateHeure)
                    .GroupBy(s => s.DateHeure.Year).ToList();
                foreach(IGrouping<int, IObjSession> annee in listeAnnee)
                {
                    TreeNode nodeAnnee = rootDejaObserve.Nodes.Add(TreePrefixDate + annee.Key.ToString(), annee.Key.ToString(), TreeImageKeyDate, TreeImageKeyDateSelected);

                    // Pour chaque année, on ajoute les mois
                    var listeMois = factory.GetListeSession().ListeComplete
                        .OrderByDescending(x => x.DateHeure)
                        .Where(s => s.DateHeure.Year == annee.Key)
                        .GroupBy(s => s.DateHeure.Month).ToList();
                    foreach (IGrouping<int, IObjSession> mois in listeMois)
                    {
                        string nomMois = factory.GetMonthName(mois.Key);
                        nodeAnnee.Nodes.Add(TreePrefixDate + annee.Key.ToString() + mois.Key.ToString("00"), nomMois, TreeImageKeyDate, TreeImageKeyDateSelected);
                    }
                }

                // Constellations
                // Ajout de l'élément racine
                TreeNode rootConstellation = treeViewSession.Nodes.Add(TreeRootConstellation, Resources.Constellations, TreeImageKeyConstellation, TreeImageKeyConstellationSelected);
                // Parcours des Objets et ajout dans la TreeView
                var listeConstellation = factory.GetListeSession().ListeComplete
                    .OrderBy(s => s.ObjetCeleste.Constellation.Nom)
                    .GroupBy(s => s.ObjetCeleste.Constellation).ToList();
                foreach (IGrouping<IObjConstellation, IObjSession>  coonstellationEnCours in listeConstellation)
                {
                    rootConstellation.Nodes.Add(TreePrefixConstellation + coonstellationEnCours.Key.Id, coonstellationEnCours.Key.Nom, TreeImageKeyConstellation, TreeImageKeyConstellationSelected);
                }

                // Type d'objets
                // Ajout de l'élément racine
                TreeNode rootTypeObjets = treeViewSession.Nodes.Add(TreeRootTypeObjets, Resources.TypeDObjetsCelestes, TreeImageKeyTypeObjets, TreeImageKeyTypeObjetsSelected);
                // Parcours des Objets et ajout dans la TreeView
                var listeTypeObjets = factory.GetListeSession().ListeComplete
                    .OrderBy(s => s.ObjetCeleste.TypeObjet.Nom)
                    .GroupBy(s => s.ObjetCeleste.TypeObjet).ToList();
                foreach (IGrouping<IObjTypeObjet, IObjSession> typeObjetEnCours in listeTypeObjets)
                {
                    rootTypeObjets.Nodes.Add(TreePrefixTypeObjets + typeObjetEnCours.Key.Id, typeObjetEnCours.Key.Nom, typeObjetEnCours.Key.Icone, typeObjetEnCours.Key.Icone);
                }

                // On repositionne l'état des noeuds
                foreach (TreeNode nodeEnCours in oldNodes)
                {
                    if (nodeEnCours.IsExpanded)
                    {
                        TreeNode[] nodeResult = treeViewSession.Nodes.Find(nodeEnCours.Name, true);
                        if (nodeResult.Length > 0)
                            nodeResult[0].Expand();
                    }
                }

                // Sélection de l'élément précedemment sélectionné
                if (selectedNode != null && string.IsNullOrEmpty(forceIdSession))
                {
                    TreeNode[] nodeResult = treeViewSession.Nodes.Find(selectedNode.Name, true);
                    if (nodeResult.Length > 0)
                        treeViewSession.SelectedNode = nodeResult[0];
                }
                else
                {
                    treeViewSession.SelectedNode = rootDejaObserve;
                }
                if (treeViewSession.SelectedNode != null)
                    treeViewSession.SelectedNode.EnsureVisible();

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
                treeViewSession.EndUpdate();
            }
        }

        /// <summary>
        /// The basic VirtualMode function.  Dynamically returns a ListViewItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewSession_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
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
        private void listViewSession_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
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
        /// Permet l'initialisation de la liste des observations de la session
        /// </summary>
        private void InitialisationListeInfosObservation()
        {
            try
            {
                // Type de vue
                listViewInfosObservations.View = View.Details;

                // Adjout des colonnes
                listViewInfosObservations.Columns.Add(Resources.Date, 60, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.Type, 60, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.Filtre, 120, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.NbExpositions, 30, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.TempsUnitaire, 30, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.TempsTotal, 40, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.GainISO, 40, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.Temperature, 30, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.Binning, 30, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.Seeing, 60, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.Lune, 60, HorizontalAlignment.Left);
                listViewInfosObservations.Columns.Add(Resources.Commentaires, 120, HorizontalAlignment.Left);

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
        /// Permet l'initialisation de la liste des équipements de la session
        /// </summary>
        private void InitialisationListeInfosEquipement()
        {
            try
            {
                // Type de vue
                listViewInfosEquipement.View = View.Details;

                // Adjout des colonnes
                listViewInfosEquipement.Columns.Add(Resources.Type, 60, HorizontalAlignment.Left);
                listViewInfosEquipement.Columns.Add(Resources.Equipement, 60, HorizontalAlignment.Left);

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
        /// Permet l'initialisation de la liste des Sessions
        /// </summary>
        private void InitialisationListeSession()
        {
            try
            {
                // Gestion mode Virtuel de la liste
                listViewSession.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listViewSession_RetrieveVirtualItem);
                listViewSession.SearchForVirtualItem += new SearchForVirtualItemEventHandler(listViewSession_SearchForVirtualItem);
                listViewSession.VirtualMode = true;

                // Liste des objets ListViewItem contenu dans la liste
                listViewItems = new List<ListViewItem>();

                // Type de vue
                listViewSession.View = View.Details;

                // Adjout des colonnes
                listViewSession.Columns.Add("", 30, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.Date, 100, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.ObjetCeleste, 100, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.TypeObjetCeleste, 140, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.Constellation, 120, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.IAU, 60, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.Commentaires, 140, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.Site, 100, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.Bortle, 60, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.NbObservations, 60, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.TempsDExpositionTotal, 80, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.Setup, 100, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.NbEquipements, 60, HorizontalAlignment.Left);
                listViewSession.Columns.Add(Resources.NbLogiciels, 60, HorizontalAlignment.Left);

                // Tri par défaut
                if (!string.IsNullOrEmpty(SortColumn) && !string.IsNullOrEmpty(SortOrder))
                {
                    lvwColumnSorter.SortColumn = int.Parse(SortColumn);
                    if (SortOrder == "Descending")
                        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    else
                        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    listViewSession.SetSortIcon(lvwColumnSorter.SortColumn, lvwColumnSorter.Order);
                    listViewItems.Sort(new ListViewItemComparer(new int[] { lvwColumnSorter.SortColumn }, lvwColumnSorter.Order));
                }

                // Trace
                factory.GetLog().Log("Initialisation de la liste des Sessions effectuée avec succès", GetType().Name);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        ///  Permet la mise à jour de la liste avec la collection
        /// </summary>
        public void RechargeListeSession()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // Trace et Chrono
                factory.GetLog().Log("Chargement de la liste des sessions", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Positionnement du Curseur et Status Action text
                caller.SetStatusActionText(Resources.ActualisationDeLaListeDesSessions);
                //Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                actualisationListeEnCours = true;

                // Stop le rafraichissement afin d'accélérer le remplissage
                listViewSession.BeginUpdate();

                // On récupère l'élément sélectionné pour le resélectionné après actualisation de la liste
                string selectedTarget = string.Empty;
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                if (indexCollection.Count > 0)
                    selectedTarget = listViewItems[indexCollection[0]].Name;
                if (!string.IsNullOrEmpty(forceIdSession))
                    selectedTarget = forceIdSession;
                forceIdSession = string.Empty;
                listViewSession.SelectedIndices.Clear();

                // Clear de la liste et parcours de la liste des Targets pour ajout
                listViewItems.Clear();
                listViewSession.Items.Clear();
                ListViewItem itemSelected = null;

                // Restriction TreeView
                TreeNode nodeSelectionne = treeViewSession.SelectedNode;
                factory.GetListeSession().SelectedConstellation = string.Empty;
                factory.GetListeSession().SelectedTypeObjet = string.Empty;
                factory.GetListeSession().SelectedDate = string.Empty;
                if (nodeSelectionne != null && nodeSelectionne.Name.StartsWith(TreePrefixConstellation))
                {
                    factory.GetListeSession().SelectedConstellation = nodeSelectionne.Name.Replace(TreePrefixConstellation, "");
                }
                else if (nodeSelectionne != null && nodeSelectionne.Name.StartsWith(TreePrefixTypeObjets))
                {
                    factory.GetListeSession().SelectedTypeObjet = nodeSelectionne.Name.Replace(TreePrefixTypeObjets, "");
                }
                else if (nodeSelectionne != null && nodeSelectionne.Name.StartsWith(TreePrefixDate))
                {
                    factory.GetListeSession().SelectedDate = nodeSelectionne.Name.Replace(TreePrefixDate, "");
                }

                foreach (IObjSession objetEnCours in factory.GetListeSession().Liste)
                {
                    ListViewItem item = new ListViewItem()
                    {
                        Name = objetEnCours.Id,
                        BackColor = Color.Transparent,
                        ImageIndex = objetEnCours.IconeIndex
                    };
                    item.SubItems.Add(objetEnCours.DateHeure.ToString("d"));
                    item.SubItems.Add(objetEnCours.ObjetCeleste != null ? objetEnCours.ObjetCeleste.Nom : string.Empty);
                    item.SubItems.Add(objetEnCours.ObjetCeleste != null ? objetEnCours.ObjetCeleste.TypeObjet.Nom : string.Empty);
                    item.SubItems.Add(objetEnCours.ObjetCeleste != null ? objetEnCours.ObjetCeleste.Constellation.Nom : string.Empty);
                    item.SubItems.Add(objetEnCours.ObjetCeleste != null ? objetEnCours.ObjetCeleste.Constellation.Abr.ToUpper() : string.Empty);
                    item.SubItems.Add(string.IsNullOrEmpty(objetEnCours.Comment) ? string.Empty : objetEnCours.Comment.Replace(Environment.NewLine, " / "));
                    item.SubItems.Add(objetEnCours.Site != null ? objetEnCours.Site.Nom : string.Empty);
                    item.SubItems.Add(objetEnCours.Site != null && objetEnCours.Site.IndiceBortle.HasValue ?
                                                    objetEnCours.Site.IndiceBortle.Value.ToString(CultureInfo.InvariantCulture) : string.Empty);
                    item.SubItems.Add(objetEnCours.ListeObservationsSession != null ? objetEnCours.ListeObservationsSession.Count.ToString() : string.Empty);
                    item.SubItems.Add(objetEnCours.FormatedTempsTotalObservations);
                    item.SubItems.Add(objetEnCours.Setup != null ? objetEnCours.Setup.Nom : string.Empty);
                    item.SubItems.Add(objetEnCours.Setup != null ?
                                            objetEnCours.Setup.ListeEquipement != null ?
                                                objetEnCours.ListeEquipementsSession != null ?
                                                    (objetEnCours.Setup.ListeEquipement.Count + objetEnCours.ListeEquipementsSession.Count).ToString()
                                                    : objetEnCours.Setup.ListeEquipement.Count.ToString()
                                                : objetEnCours.ListeEquipementsSession != null ?
                                                    objetEnCours.ListeEquipementsSession.Count.ToString()
                                                    : string.Empty
                                            : string.Empty);
                    item.SubItems.Add(objetEnCours.ListeLogicielsSession != null ? objetEnCours.ListeLogicielsSession.Count.ToString() : string.Empty);

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
                listViewItems.Sort(new ListViewItemComparer(new int[] { lvwColumnSorter.SortColumn }, lvwColumnSorter.Order));

                // AutoFit des colonnes et affichage de l'élément sélectionné
                listViewSession.FullRowSelect = true;

                // Nombre d'éléments dans la liste virtuelle
                listViewSession.VirtualListSize = 0;
                listViewSession.VirtualListSize = listViewItems.Count;

                // Sélection de l'objet souhaité
                if (itemSelected != null && !string.IsNullOrEmpty(itemSelected.Name))
                {
                    ListViewItem[] item = listViewSession.Items.Find(itemSelected.Name, false);
                    if (item != null && item.Length > 0)
                    {
                        listViewSession.SelectedIndices.Add(item[0].Index);
                        item[0].EnsureVisible();
                    }
                }

                // Trace
                factory.GetLog().Log($"Chargement de la liste des {listViewSession.Items.Count} sessions effectué en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
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
                listViewSession.EndUpdate();
                actualisationListeEnCours = false;
                // Texte de la Status
                caller.SetStatusActionText(string.Empty);
                SetStatusText();
                // Positionnement du Curseur
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Tri de la liste
        /// </summary>
        /// <param name="indexColonne">Index de la colonne à trier</param>
        private void TriListeSession(int indexColonne)
        {
            try
            {
                // Trace
                factory.GetLog().Log($"Tri de la liste sur l'index de colonne : {indexColonne}", GetType().Name);

                // On ne tri pas la première colonne
                if (indexColonne == 0)
                    return;

                // Stop le rafraichissement afin d'accélérer le remplissage
                listViewSession.BeginUpdate();

                // Reset de l'élément sélectionné
                listViewSession.SelectedIndices.Clear();

                // Colonne actuellement en cours de tri ? Dans ce cas, on inverse le tri en cours
                if (indexColonne == lvwColumnSorter.SortColumn)
                {
                    if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    else
                        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
                else
                {
                    // Sinon, on positionne le tri sur la colonne en cours en mode Ascendant
                    lvwColumnSorter.SortColumn = indexColonne;
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }

                // On lance le tri de la liste
                listViewSession.SetSortIcon(lvwColumnSorter.SortColumn, lvwColumnSorter.Order);
                listViewItems.Sort(new ListViewItemComparer(new int[] { lvwColumnSorter.SortColumn }, lvwColumnSorter.Order));

                // MAJ des settings
                SortColumn = lvwColumnSorter.SortColumn.ToString();
                SortOrder = lvwColumnSorter.Order.ToString();
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
                listViewSession.EndUpdate();
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
        /// Mise à jour du panneau d'informations
        /// </summary>
        public void UpdatePaneInfo()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // Flush du panneau
                SetToolBarEditButtonState();

                // Boutons Action
                SetStellariumButtonState(caller.IsStellariumInstalled && !caller.IsStellariumRunning);
                SetCdCButtonState(caller.IsCdCInstalled && !caller.IsCdCRunning);
                SetATSButtonState(caller.IsAstroTargetSelectorInstalled);

                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);     
                }

                splitContainerSecondary.Panel2Collapsed = sessionSelectionne == null;
                if (sessionSelectionne != null)
                {
                    // Objet Céleste
                    if (sessionSelectionne.ObjetCeleste != null)
                    {
                        textBoxInfosNomObjetCeleste.Text = sessionSelectionne.ObjetCeleste.Nom;
                        textBoxInfosTypeObjet.Text = sessionSelectionne.ObjetCeleste.TypeObjet.Nom;
                        textBoxInfosNomConstellation.Text = sessionSelectionne.ObjetCeleste.Constellation.Nom;
                        textBoxInfosIAU.Text = sessionSelectionne.ObjetCeleste.Constellation.Abr.ToUpper();
                    }

                    // Icone Type Objet
                    if (sessionSelectionne.ObjetCeleste != null)
                    {
                        pictureBoxInfosTypeObjetNonDefini.Visible = sessionSelectionne.ObjetCeleste.TypeObjet.Icone == "NonDefini";
                        pictureBoxInfosTypeObjetStar.Visible = sessionSelectionne.ObjetCeleste.TypeObjet.Icone == "Star";
                        pictureBoxInfosTypeObjetMultipleStars.Visible = sessionSelectionne.ObjetCeleste.TypeObjet.Icone == "MultipleStars";
                        pictureBoxInfosTypeObjetGalaxie.Visible = sessionSelectionne.ObjetCeleste.TypeObjet.Icone == "Galaxie";
                        pictureBoxInfosTypeObjetNebuleuse.Visible = sessionSelectionne.ObjetCeleste.TypeObjet.Icone == "Nebuleuse";
                        pictureBoxInfosTypeObjetCluster.Visible = sessionSelectionne.ObjetCeleste.TypeObjet.Icone == "Cluster";
                        pictureBoxInfosTypeObjetPlanete.Visible = sessionSelectionne.ObjetCeleste.TypeObjet.Icone == "Planete";
                    }

                    // Image Objet
                    pictureBoxInfosImageSession.ImageLocation = sessionSelectionne.ResizedDisplayThumbnail;

                    // Infos Session
                    labelInfosDate.Text = sessionSelectionne.DateHeure.ToString("d");
                    textBoxInfosTempsTotal.Text = sessionSelectionne.FormatedTempsTotalObservations;
                    textBoxInfosSite.Text = sessionSelectionne.Site != null ? sessionSelectionne.Site.Nom : string.Empty;
                    textBoxInfosSetup.Text = sessionSelectionne.Setup != null ? sessionSelectionne.Setup.Nom : string.Empty;
                    textBoxInfosComment.Text = string.IsNullOrEmpty(sessionSelectionne.Comment) ? string.Empty : sessionSelectionne.Comment.Replace(Environment.NewLine, " / ");
                    textBoxInfosPathImages.Text = sessionSelectionne.ImagesPath;
                    textBoxInfosLogiciels.Text = sessionSelectionne.FullLogicielsListe;

                    // Boutons Images et EXIF
                    buttonOpenPathImages.Enabled = !string.IsNullOrEmpty(sessionSelectionne.ImagesPath);
                    buttonCreerExif.Enabled = !string.IsNullOrEmpty(sessionSelectionne.ImagesPath);
                    pictureBoxInfosExif.Visible = string.IsNullOrEmpty(sessionSelectionne.ImagesPath);
                    buttonImageSiteAdd.Enabled = !string.IsNullOrEmpty(sessionSelectionne.ImagesPath);
                    buttonImageSiteRemove.Enabled = !string.IsNullOrEmpty(sessionSelectionne.ImagesPath) && !string.IsNullOrEmpty(sessionSelectionne.Thumbnail);
                    pictureBoxInfosImageCustom.Visible = !string.IsNullOrEmpty(sessionSelectionne.ImagesPath);

                    // ListView des observations
                    listViewInfosObservations.BeginUpdate();
                    listViewInfosObservations.Items.Clear();
                    foreach (IObjObservation observation in sessionSelectionne.ListeObservationsSession)
                    {
                        listViewInfosObservations.Items.Add(new ListViewItem(new[] {
                                                $"{observation.DateHeure.ToString("d")} - {observation.DateHeure.ToString("t")}",
                                                observation.TypeObservation.Nom,
                                                observation.Equipement != null ? observation.Equipement.Nom : string.Empty,
                                                observation.NBR_EXPO.HasValue ? observation.NBR_EXPO.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                                observation.TPS_EXPO.HasValue ? observation.TPS_EXPO.Value.ToString(CultureInfo.InvariantCulture) + " s" : string.Empty,
                                                observation.TempsTotalExposition.ToStandardFormatString(),
                                                observation.GAIN.HasValue ? observation.GAIN.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                                observation.TEMP.HasValue ? observation.TEMP.Value.ToString(CultureInfo.InvariantCulture) + " °" : string.Empty,
                                                observation.BINNING.HasValue ? observation.BINNING.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                                observation.Seeing,
                                                observation.Lune,
                                                observation.Comment}));

                    }
                    listViewInfosObservations.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    listViewInfosObservations.EndUpdate();

                    // Liste des équipements
                    listViewInfosEquipement.BeginUpdate();
                    listViewInfosEquipement.Items.Clear();
                    // Equipement Setup
                    if (sessionSelectionne.Setup != null)
                    {
                        foreach (IObjEquipementSetup equipementSetup in sessionSelectionne.Setup.ListeEquipement)
                        {
                            IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == equipementSetup.IdEquipement).FirstOrDefault();
                            if (equipement != null)
                            {
                                string nom = equipementSetup.Nom;
                                if (string.IsNullOrEmpty(nom))
                                    nom = equipement.Nom;
                                listViewInfosEquipement.Items.Add(new ListViewItem(new[] {
                                                equipement.TypeEquipement.Nom,
                                                nom
                                            }));
                            }

                        }
                    }
                    // Equipement Session
                    foreach (IObjEquipementSession equipementSession in sessionSelectionne.ListeEquipementsSession)
                    {
                        IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == equipementSession.IdEquipement).FirstOrDefault();
                        if (equipement != null)
                        {
                            string nom = equipementSession.Nom;
                            if (string.IsNullOrEmpty(nom))
                                nom = equipement.Nom;
                            listViewInfosEquipement.Items.Add(new ListViewItem(new[] {
                                            equipement.TypeEquipement.Nom,
                                            nom
                                        }));
                        }

                    }
                    listViewInfosEquipement.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    listViewInfosEquipement.EndUpdate();
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
            finally
            {
                // Positionnement du Curseur
                Cursor = Cursors.Default;
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
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        // Trace
                        factory.GetLog().Log($"{Resources.EditionDeLaSessionDu} {sessionSelectionne.DateHeure} {Resources.SurLObjet} {sessionSelectionne.ObjetCeleste.Nom}", GetType().Name);

                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.EditionDUneSessionDObservations);
                        // Lancement Edition
                        dlgSession dlgEdition = new dlgSession(factory, caller, sessionSelectionne);
                        if (dlgEdition.ShowDialog() == DialogResult.OK)
                        {
                            UpdateForm();
                            //RechargeListeSession();
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
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        // Trace
                        factory.GetLog().Log($"Suppression de la session du {sessionSelectionne.DateHeure.ToString("d")} sur {sessionSelectionne.ObjetCeleste.Nom}", GetType().Name);

                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.SuppressionDUneSessionDObservations);
                        // Lancement Suppression
                        string message = $"{Resources.VoulezVousSupprimerLaSessionDu} {sessionSelectionne.DateHeure.ToString("d")} {Resources.Sur} {sessionSelectionne.ObjetCeleste.Nom} ?"
                            + Environment.NewLine + Resources.LesObservationsAssocieesACetteSessionSerontEgalementSupprimees;
                        if (MessageBox.Show(message
                            , Application.ProductName
                            , MessageBoxButtons.YesNo
                            , MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            factory.DeleteSession(sessionSelectionne);
                            UpdateForm();
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
        /// Ajout d'un nouvelle Session
        /// </summary>
        public void AddSession()
        {
            try
            {
                // Positionnement du texte de la Status
                caller.SetStatusActionText(Resources.CreationDUneSessionDObservations);
                // Lancement Création
                dlgSession dlgEdition = new dlgSession(factory, caller);
                if (dlgEdition.ShowDialog() == DialogResult.OK)
                {
                    UpdateForm();
                    //RechargeListeSession();
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
        /// Ouvre la boîte de dialogue de l'EXIF de la session sélectionnée
        /// </summary>
        private void ShowExif()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.VisualisationEXIFDeLaSession);

                        dlgExif dlgEditionExif = new dlgExif(factory, sessionSelectionne);
                        dlgEditionExif.ShowDialog();
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
        /// Ouvre le répertoire "ImagesPath" pour la session sélectionnée
        /// </summary>
        private void OpenImagesPath()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        Process.Start(sessionSelectionne.ImagesPath);
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
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        if (caller != null)
                            caller.StellariumFocusTo(sessionSelectionne.ObjetCeleste);
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
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        if (caller != null)
                            caller.CdCFocusTo(sessionSelectionne.ObjetCeleste);
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
                if (listViewSession != null)
                {
                    string objetRepertorie = Resources.SessionRepertoriee;
                    if (factory.GetListeSession().NombreTotalSessions > 1)
                        objetRepertorie = Resources.SessionsRepertoriees;
                    string objetAffiche = Resources.SessionAffichee;
                    if (listViewItems.Count > 1)
                        objetAffiche = Resources.SessionsAffichees;
                    statusText = $"{factory.GetListeSession().NombreTotalSessions} {objetRepertorie} : {listViewItems.Count} {objetAffiche}";
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
        /// Positionne une nouvelle image pour la session
        /// </summary>
        private void SetImageSession()
        {
            try
            {
                // Positionnement du Curseur et Status Action text
                Cursor = Cursors.WaitCursor;
                caller.SetStatusActionText(Resources.ChargementDUneNouvelleImagePourLaSession);

                // On vérifie qu'on a bien un site de sélectionné, et on récupère l'objet IObjSession correspondant
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1 && !actualisationListeEnCours)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        // Sélection du fichier image
                        OpenFileDialog dlgOpen = new OpenFileDialog();
                        dlgOpen.InitialDirectory = sessionSelectionne.ImagesPath;
                        dlgOpen.Title = $"{Resources.SelectionDUneImagePourLaSessionDu} '{sessionSelectionne.DateHeure.ToString("d")}'";
                        dlgOpen.Filter = $"{Resources.Images} |*.jpg;*.jpeg;*.png;*.tif";
                        if (dlgOpen.ShowDialog() == DialogResult.OK)
                        {
                            // Repositionnement du Curseur
                            Cursor = Cursors.WaitCursor;
                            //pictureBoxInfosImageSession.Show();
                            // On enregistre BDD
                            sessionSelectionne.Thumbnail = dlgOpen.FileName;
                            factory.UpdateSession(sessionSelectionne);
                            // On positionne l'image
                            pictureBoxInfosImageSession.ImageLocation = sessionSelectionne.ResizedDisplayThumbnail;
                            buttonImageSiteRemove.Enabled = true;
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
                // Positionnement du Curseur
                Cursor = Cursors.Default;
                caller.SetStatusActionText(string.Empty);
            }
        }

        /// <summary>
        /// Ouvre l'image e la session
        /// </summary>
        private void OpenImageSession()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        if (!string.IsNullOrEmpty(sessionSelectionne.DisplayThumbnail))
                            Process.Start(sessionSelectionne.DisplayThumbnail);
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
        /// Supprime l'image custom pour la session
        /// </summary>
        private void RemoveImageSite()
        {
            try
            {
                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;

                // On vérifie qu'on a bien un site de sélectionné, et on récupère l'objet IObjSession correspondant
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1 && !actualisationListeEnCours)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        // On enregistre BDD
                        sessionSelectionne.Thumbnail = string.Empty;
                        factory.UpdateSession(sessionSelectionne);
                        pictureBoxInfosImageSession.ImageLocation = sessionSelectionne.DisplayThumbnail;
                        buttonImageSiteRemove.Enabled = false;
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
                // Positionnement du Curseur
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Permet la sélection d'une session dans le formulaire
        /// <para>Flush les filtres et la sélection dans le TreeView</para>
        /// </summary>
        /// <param name="sessionSelectionnee"></param>
        public void SelectSession(IObjSession sessionSelectionnee)
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                if (sessionSelectionnee != null)
                {
                    forceIdSession = sessionSelectionnee.Id;

                    // Trace
                    factory.GetLog().Log($"Sélection de la session du {sessionSelectionnee.DateHeure.ToString("d")} sur {sessionSelectionnee.ObjetCeleste.Nom}", GetType().Name);
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
        /// Permet la sélection d'un objet dans le formulaire
        /// <para>Flush les filtres et la sélection dans le TreeView</para>
        /// </summary>
        private void SelectObjetCeleste()
        {
            try
            {
                // Sécurité si le formulaire n'a pas encore été initialisé
                if (listViewItems == null)
                    return;

                // On récupère l'objet sélectionné
                ListView.SelectedIndexCollection indexCollection = listViewSession.SelectedIndices;
                IObjSession sessionSelectionne = null;
                if (indexCollection.Count == 1)
                {
                    sessionSelectionne = factory.GetListeSession().GetSession(listViewItems[indexCollection[0]].Name);
                    if (sessionSelectionne != null)
                    {
                        if (caller != null)
                        {
                            splitContainerSecondary.Panel2Collapsed = true;
                            caller.SelectObjetCeleste(sessionSelectionne.ObjetCeleste, true);
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
            RechargeListeSession();
            UpdatePaneInfo();

            flushFormEnCours = false;
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
        /// Positionne l'état des boutons d'édition de la toolbar
        /// </summary>
        public void SetToolBarEditButtonState()
        {
            if (caller != null && listViewSession != null)
                caller.SetToolBarEditButtonState(listViewSession.SelectedIndices.Count == 1);
        }

        /// <summary>
        /// Positionne l'hétat du menu contextuel
        /// </summary>
        private void SetContextMenuState()
        {
            try
            {
                modifierToolStripMenuItem.Enabled = listViewSession.SelectedIndices.Count == 1;
                supprimerToolStripMenuItem.Enabled = listViewSession.SelectedIndices.Count == 1;
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
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
        /// Positionne l'id de la session à sélectionner pour les demandes externes
        /// </summary>
        private string forceIdSession = string.Empty;

        #endregion

        private void FormSessions_Load(object sender, EventArgs e)
        {
            // Avant tout, on masque le panel pour le chargement
            splitContainerSecondary.Panel2Collapsed = true;
        }

        private void treeViewSession_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!flushFormEnCours && !actualisationListeEnCours)
            {
                RechargeListeSession();
            }
        }

        private void listViewSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                SelectItem();
            }), null);
        }

        private void listViewSession_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
        }

        private void buttonExif_Click(object sender, EventArgs e)
        {
            ShowExif();
        }

        private void listViewSession_KeyDown(object sender, KeyEventArgs e)
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

        private void buttonOpenPathImages_Click(object sender, EventArgs e)
        {
            OpenImagesPath();
        }

        private void pictureBoxInfosImageSession_Click(object sender, EventArgs e)
        {
            OpenImageSession();
        }

        private void buttonImageSiteAdd_Click(object sender, EventArgs e)
        {
            SetImageSession();
        }

        private void buttonImageSiteRemove_Click(object sender, EventArgs e)
        {
            RemoveImageSite();
        }

        private void buttonInfosStellarium_Click(object sender, EventArgs e)
        {
            CallStellarium();
        }

        private void buttonInfosCdC_Click(object sender, EventArgs e)
        {
            CallCdC();
        }

        private void buttonDisplayObjetCeleste_Click(object sender, EventArgs e)
        {
            SelectObjetCeleste();
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSession();
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void contextMenuStripSession_Opening(object sender, CancelEventArgs e)
        {
            SetContextMenuState();
        }

        private void listViewSession_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            TriListeSession(e.Column);
        }
    }
}
