using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Liste des équipements, Setup et Sites d'observations
    /// </summary>
    public partial class FormEquipements : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public FormEquipements(IAppObjFactory factory, MainFenetre caller)
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

                // Initialisation des composants du formulaire
                InitialisationListeEquipements();

                // Chargement de la liste
                RechargeListeEquipements();
                UpdatePaneInfo();

                // ToolTips
                SetToolTips();

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
            }
            finally
            {
            }
        }

        /// <summary>
        /// Positionne les ToolTips du formulaire
        /// </summary>
        private void SetToolTips()
        {
            // ToolTip ImageSite 
            toolTipInfosImageSite.ToolTipTitle = Resources.InformationsSurLesImagesDesSitesDObservations;
            string toolTipInfoSite = Resources.VousPouvezAjouterUneImageAVotreSiteDObservation
                                + Environment.NewLine + Resources.LorsDeLaCreationDesEXIFVousPourrezChoisirDInclureOuNonCetteImage;
            toolTipInfosImageSite.SetToolTip(pictureBoxInfosImageSite, toolTipInfoSite);

            // ToolTip Bouton ImageSite Ajouter
            string toolTipInfoSiteAdd = Resources.AjouterUneImageAuSiteDObservations;
            toolTipInfosImageSiteAdd.SetToolTip(buttonImageSiteAdd, toolTipInfoSiteAdd);

            // ToolTip Bouton ImageSite Supprimer
            string toolTipInfoSiteRemove = Resources.SupprimerLImageDuSiteDObservations;
            toolTipInfosImageSiteRemove.SetToolTip(buttonImageSiteRemove, toolTipInfoSiteRemove);

            // ToolTip ImageSetup 
            toolTipInfosImageSetup.ToolTipTitle = Resources.InformationsSurLesImagesDesSetups;
            string toolTipInfoSetup = Resources.VousPouvezAjouterUneImageAVotreSetup
                                + Environment.NewLine + Resources.LorsDeLaCreationDesEXIFVousPourrezChoisirDInclureOuNonCetteImage;
            toolTipInfosImageSetup.SetToolTip(pictureBoxInfosImageSetup, toolTipInfoSetup);

            // ToolTip Bouton ImageSite Ajouter
            string toolTipInfoSetupAdd = Resources.AjouterUneImageAuSetup;
            toolTipInfosImageSetupAdd.SetToolTip(buttonImageSetupAdd, toolTipInfoSetupAdd);

            // ToolTip Bouton ImageSite Supprimer
            string toolTipInfoSetupRemove = Resources.SupprimerLImageDuSetup;
            toolTipInfosImageSetupRemove.SetToolTip(buttonImageSetupRemove, toolTipInfoSetupRemove);
        }

        /// <summary>
        /// Permet l'initialisation de la liste
        /// </summary>
        private void InitialisationListeEquipements()
        {
            try
            {
                // Type de vue
                listViewEquipement.View = View.Tile;

                // Ajout du groupe pour les Sites
                listViewEquipement.Groups.Add(new ListViewGroup("Sites")
                {
                    Name = Resources.Sites
                });

                // Ajout du groupe pour les Setup
                listViewEquipement.Groups.Add(new ListViewGroup("Setup")
                {
                    Name = Resources.Setup
                });

                // Positionnement des groupes pour les types d'équipements
                foreach (IObjTypeEquipement typeEquipementEnCours in factory.GetListeTypeEquipements())
                {
                    listViewEquipement.Groups.Add(new ListViewGroup(typeEquipementEnCours.Nom)
                    {
                        Name = typeEquipementEnCours.Nom
                    });
                }

                // Ajout du groupe pour les Logiciels
                listViewEquipement.Groups.Add(new ListViewGroup("Logiciels")
                {
                    Name = Resources.Logiciels
                });

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
        ///  Permet la mise à jour de la liste
        /// </summary>
        public void RechargeListeEquipements()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log("Chargement de la liste des équipements", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                // Positionnement du Curseur et Status Action text
                Cursor = Cursors.WaitCursor;
                actualisationListeEnCours = true;
                caller.SetStatusActionText(Resources.ActualisationDeLaListeDesEquipements);

                // On récupère l'obje sélectionné pour la resélection après actualisation de la liste
                string selectedItemId = string.Empty;
                string selectedGroupName = string.Empty;
                if (listViewEquipement.SelectedItems.Count > 0)
                {
                    selectedItemId = listViewEquipement.SelectedItems[0].SubItems[1].Text;
                    selectedGroupName = listViewEquipement.SelectedItems[0].Group.Name;
                }

                // Stop le rafraichissement afin d'accélérer le remplissage
                listViewEquipement.BeginUpdate();

                // Clear de la liste et parcours de la liste des Targets pour ajout
                listViewEquipement.Items.Clear();
                ListViewItem itemSelected = null;

                // Ajout des Sites d'observations
                foreach (IObjSite siteEnCours in factory.GetListeSites().OrderBy(e => e.Nom))
                {
                    ListViewItem item = listViewEquipement.Items.Add(new ListViewItem()
                    {
                        Text = siteEnCours.Nom,
                        Group = listViewEquipement.Groups["Sites"],
                        ImageKey = "Site"
                    });
                    item.SubItems.Add(siteEnCours.Id);
                    if (!string.IsNullOrEmpty(selectedItemId) && selectedItemId == siteEnCours.Id
                        && !string.IsNullOrEmpty(selectedGroupName) && selectedGroupName == listViewEquipement.Groups["Sites"].Name)
                    {
                        item.Selected = true;
                        item.Focused = true;
                        itemSelected = item;
                    }
                }

                // Ajout des Setup
                foreach (IObjSetup setupEnCours in factory.GetListeSetup().OrderBy(e => e.Nom))
                {
                    ListViewItem item = listViewEquipement.Items.Add(new ListViewItem()
                    {
                        Text = setupEnCours.Nom,
                        Group = listViewEquipement.Groups["Setup"],
                        ImageKey = "Setup"
                    });
                    item.SubItems.Add(setupEnCours.Id);
                    if (!string.IsNullOrEmpty(selectedItemId) && selectedItemId == setupEnCours.Id
                        && !string.IsNullOrEmpty(selectedGroupName) && selectedGroupName == listViewEquipement.Groups["Setup"].Name)
                    {
                        item.Selected = true;
                        item.Focused = true;
                        itemSelected = item;
                    }
                }

                // Ajout des équipements
                foreach (IObjEquipement equipementEnCours in factory.GetListeEquipements().OrderBy(e => e.Nom))
                {
                    ListViewItem item = listViewEquipement.Items.Add(new ListViewItem()
                    {
                        Text = equipementEnCours.Nom,
                        Group = listViewEquipement.Groups[equipementEnCours.TypeEquipement.Nom],
                        ImageKey = equipementEnCours.TypeEquipement.Icone
                    });
                    item.SubItems.Add(equipementEnCours.Id);
                    if (!string.IsNullOrEmpty(selectedItemId) && selectedItemId == equipementEnCours.Id
                        && !string.IsNullOrEmpty(selectedGroupName) && selectedGroupName != listViewEquipement.Groups["Sites"].Name
                        && selectedGroupName != listViewEquipement.Groups["Setup"].Name)
                    {
                        item.Selected = true;
                        item.Focused = true;
                        itemSelected = item;
                    }
                }

                // Ajout des Logiciels
                foreach (IObjLogiciel logicielEnCours in factory.GetListeLogiciels().OrderBy(e => e.Nom).OrderBy(e => e.IdTypeLogiciel))
                {
                    ListViewItem item = listViewEquipement.Items.Add(new ListViewItem()
                    {
                        Text = $"{logicielEnCours.TypeLogiciel.Nom} - {logicielEnCours.Nom}",
                        Group = listViewEquipement.Groups["Logiciels"],
                        ImageKey = logicielEnCours.TypeLogiciel.Icone
                    });
                    item.SubItems.Add(logicielEnCours.Id);
                    if (!string.IsNullOrEmpty(selectedItemId) && selectedItemId == logicielEnCours.Id
                        && !string.IsNullOrEmpty(selectedGroupName) && selectedGroupName == listViewEquipement.Groups["Logiciels"].Name)
                    {
                        item.Selected = true;
                        item.Focused = true;
                        itemSelected = item;
                    }
                }

                // On force la visibilité de l'élément sélectionné
                if (itemSelected != null)
                    itemSelected.EnsureVisible();

                // Trace
                factory.GetLog().Log($"Chargement de la liste des {listViewEquipement.Items.Count} Equipements/Setup/Sites effectué en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
                caller.SetStatusActionText(string.Empty);
                caller.SetStatusDefaultText(string.Empty);
                MessageBox.Show(ApplicationTools.Properties.Resources.UneErreurEstSurvenue + Environment.NewLine + err.Message
                                , Application.ProductName
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
            finally
            {
                // Réactive la liste
                listViewEquipement.EndUpdate();
                actualisationListeEnCours = false;
                // Texte de la Status
                caller.SetStatusActionText(string.Empty);
                SetStatusText();
                // Positionnement du Curseur
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sélection d'un élément dans la liste
        /// </summary>
        private void SelectItem()
        {
            UpdatePaneInfo();
        }

        /// <summary>
        /// Mise à jour du panneau d'informations
        /// </summary>
        public void UpdatePaneInfo()
        {
            try
            {
                // Trace et Chrono
                factory.GetLog().Log($"Rechargement du Panneau d'informations sur l'équipement sélectionné", GetType().Name);
                Stopwatch debutFonction = new Stopwatch();
                debutFonction.Start();

                SetToolBarEditButtonState();
                groupBoxSites.Visible = false;
                groupBoxImageSite.Visible = false;
                pictureBoxImageSite.Visible = false;
                groupBoxSetup.Visible = false;
                groupBoxImageSetup.Visible = false;
                pictureBoxImageSetup.Visible = false;
                groupBoxEquipements.Visible = false;
                groupBoxLogiciel.Visible = false;
                splitContainerGlobal.Panel2Collapsed = listViewEquipement.SelectedItems.Count != 1;
                if (listViewEquipement.SelectedItems.Count == 1 && !actualisationListeEnCours)
                {
                    // On récupère l'élément sélectionné afin de vérifier le type d'objet Equipement/Setup/Sites
                    ListViewItem selectedItem = listViewEquipement.SelectedItems[0];
                    // Objet de type Setup
                    if (selectedItem.Group == listViewEquipement.Groups["Sites"])
                    {
                        IObjSite site = factory.GetListeSites().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (site != null)
                        {
                            groupBoxSites.Visible = true;
                            groupBoxImageSite.Visible = true;
                            pictureBoxImageSite.ImageLocation = string.Empty;
                            // Infos
                            textBoxNomSite.Text = site.Nom;
                            labelSiteLongitude.Text = string.Empty;
                            labelSiteLatitude.Text = string.Empty;
                            labelSiteBortle.Text = site.IndiceBortle.HasValue ? site.IndiceBortle.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
                            if (site.Coordonnee != null)
                            {
                                labelSiteLongitude.Text = site.Coordonnee.Longitude;
                                labelSiteLatitude.Text = site.Coordonnee.Latitude;
                            }
                            // Image
                            buttonImageSiteAdd.Enabled = true;
                            buttonImageSiteRemove.Enabled = !string.IsNullOrEmpty(site.Thumbnail);
                            if (!string.IsNullOrEmpty(site.Thumbnail))
                            {
                                // On vérifie la présence du fichier
                                if (!File.Exists(site.Thumbnail))
                                {
                                    // Le fichier n'est plus présent => on update l'enregistrement en BDD pour les appels ultérieur
                                    site.Thumbnail = string.Empty;
                                    factory.UpdateSite(site);
                                }
                                else
                                {
                                    pictureBoxImageSite.Visible = true;
                                    pictureBoxImageSite.ImageLocation = site.FormatedThumbnailPathName;
                                    pictureBoxImageSite.Show();
                                }
                            }
                        }
                    }
                    // Objet de type Setup
                    else if (selectedItem.Group == listViewEquipement.Groups["Setup"])
                    {
                        IObjSetup setup = factory.GetListeSetup().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (setup != null)
                        {
                            groupBoxSetup.Visible = true;
                            groupBoxImageSetup.Visible = true;
                            pictureBoxImageSetup.ImageLocation = string.Empty;
                            // Infos
                            textBoxNomSetup.Text = setup.Nom;
                            // Ajout des équipements du setup
                            listBoxEquipementsSetup.Items.Clear();
                            foreach (IObjEquipementSetup equipementSetup in setup.ListeEquipement)
                            {
                                IObjEquipement equipement = factory.GetListeEquipements().Where(e => e.Id == equipementSetup.IdEquipement).FirstOrDefault();
                                if (equipement != null)
                                {
                                    string nom = equipementSetup.Nom;
                                    if (string.IsNullOrEmpty(nom))
                                        nom = equipement.Nom;
                                    listBoxEquipementsSetup.Items.Add(nom);
                                }
                            }
                            // Image
                            buttonImageSetupAdd.Enabled = true;
                            buttonImageSetupRemove.Enabled = !string.IsNullOrEmpty(setup.Thumbnail);
                            if (!string.IsNullOrEmpty(setup.Thumbnail))
                            {
                                // On vérifie la présence du fichier
                                if (!File.Exists(setup.Thumbnail))
                                {
                                    // Le fichier n'est plus présent => on update l'enregistrement en BDD pour les appels ultérieur
                                    setup.Thumbnail = string.Empty;
                                    factory.UpdateSetup(setup);
                                }
                                else
                                {
                                    pictureBoxImageSetup.Visible = true;
                                    pictureBoxImageSetup.ImageLocation = setup.FormatedThumbnailPathName;
                                    pictureBoxImageSetup.Show();
                                }
                            }
                        }

                    }
                    // Objet de type Logiciel
                    else if (selectedItem.Group == listViewEquipement.Groups["Logiciels"])
                    {
                        IObjLogiciel logiciel = factory.GetListeLogiciels().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (logiciel != null)
                        {
                            groupBoxLogiciel.Visible = true;
                            groupBoxLogiciel.Top = 12;
                            // Infos
                            textBoxTypeLogiciel.Text = logiciel.TypeLogiciel.Nom;
                            textBoxNomLogiciel.Text = logiciel.Nom;
                        }

                    }
                    // Objet de type Equipement
                    else
                    {
                        pictureBoxTypeTelescope.Visible = false;
                        pictureBoxTypeMonture.Visible = false;
                        pictureBoxTypeCamera.Visible = false;
                        pictureBoxLens.Visible = false;
                        pictureBoxTypeDivers.Visible = false;
                        IObjEquipement equipement = factory.GetListeEquipements().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (equipement != null)
                        {
                            groupBoxEquipements.Visible = true;
                            textBoxNomTypeEquipement.Text = equipement.TypeEquipement.Nom;
                            textBoxNomEquipement.Text = equipement.Nom;
                            pictureBoxTypeTelescope.Visible = equipement.TypeEquipement.Nom == factory.GetListeTypeEquipements()[0].Nom;
                            pictureBoxTypeMonture.Visible = equipement.TypeEquipement.Nom == factory.GetListeTypeEquipements()[1].Nom;
                            pictureBoxTypeCamera.Visible = equipement.TypeEquipement.Nom == factory.GetListeTypeEquipements()[2].Nom;
                            pictureBoxLens.Visible = equipement.TypeEquipement.Nom == factory.GetListeTypeEquipements()[3].Nom;
                            pictureBoxTypeDivers.Visible = equipement.TypeEquipement.Nom == factory.GetListeTypeEquipements()[4].Nom;
                        }
                    }

                    // Trace
                    factory.GetLog().Log($"Chargement du Panneau d'informations pour l'objet {selectedItem.Text} en {debutFonction.ElapsedMilliseconds} ms", GetType().Name, debutFonction.ElapsedMilliseconds);
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Edition de l'élément sélectionné dans la liste
        /// </summary>
        public void EditItem()
        {
            try
            {
                if (listViewEquipement.SelectedItems.Count == 1)
                {
                    // On récupère l'élément sélectionné afin de vérifier le type d'objet Equipement/Setup/Sites
                    ListViewItem selectedItem = listViewEquipement.SelectedItems[0];
                    // Objet de type Setup
                    if (selectedItem.Group == listViewEquipement.Groups["Sites"])
                    {
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.EditionDUnSite);
                        // Lancement Edition
                        IObjSite site = factory.GetListeSites().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (site != null)
                        {
                            dlgSite dlgEdition = new dlgSite(factory, site);
                            if (dlgEdition.ShowDialog() == DialogResult.OK)
                                RechargeListeEquipements();
                        }
                    }
                    // Objet de type Setup
                    else if (selectedItem.Group == listViewEquipement.Groups["Setup"])
                    {
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.EditionDUnSetup);
                        // Lancement Edition
                        IObjSetup setup = factory.GetListeSetup().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (setup != null)
                        {
                            dlgSetup dlgEdition = new dlgSetup(factory, setup);
                            if (dlgEdition.ShowDialog() == DialogResult.OK)
                                RechargeListeEquipements();
                        }
                    }
                    // Objet de type Logiciel
                    else if (selectedItem.Group == listViewEquipement.Groups["Logiciels"])
                    {
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.EditionDUnLogiciel);
                        // Lancement Edition
                        IObjLogiciel logiciel = factory.GetListeLogiciels().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (logiciel != null)
                        {
                            dlgLogiciel dlgEdition = new dlgLogiciel(factory, logiciel);
                            if (dlgEdition.ShowDialog() == DialogResult.OK)
                                RechargeListeEquipements();
                        }
                    }// Objet de type Equipement
                    else
                    {
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.EditionDUnEquipement);
                        // Lancement Edition
                        IObjEquipement equipement = factory.GetListeEquipements().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (equipement != null)
                        {
                            dlgEquipement dlgEdition = new dlgEquipement(factory, equipement);
                            if (dlgEdition.ShowDialog() == DialogResult.OK)
                                RechargeListeEquipements();
                        }
                    }
                }
                // Update du panneau d'informations
                UpdatePaneInfo();
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
                if (listViewEquipement.SelectedItems.Count == 1)
                {
                    // On récupère l'élément sélectionné afin de vérifier le type d'objet Equipement/Setup/Sites
                    ListViewItem selectedItem = listViewEquipement.SelectedItems[0];
                    // Objet de type Setup
                    if (selectedItem.Group == listViewEquipement.Groups["Sites"])
                    {
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.SuppressionDUnSite);
                        // Lancement Suppression
                        IObjSite site = factory.GetListeSites().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (site != null)
                        {
                            string message = $"{Resources.VoulezVousSupprimerLeSite} '{site.Nom}' ?" + Environment.NewLine
                                            + Resources.CeSiteNApparaitraPlusDansLesSessionsDansLesquellesIlEstPresent;
                            if (MessageBox.Show(message
                                , Application.ProductName
                                , MessageBoxButtons.YesNo
                                , MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                factory.DeleteSite(site);
                                RechargeListeEquipements();
                            }
                        }
                    }
                    // Objet de type Setup
                    else if (selectedItem.Group == listViewEquipement.Groups["Setup"])
                    {
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.SuppressionDUnSetup);
                        // Lancement Suppression
                        IObjSetup setup = factory.GetListeSetup().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (setup != null)
                        {
                            string message = $"{Resources.VoulezVousSupprimerLeSetup} '{setup.Nom}' ?" + Environment.NewLine
                                            + Resources.CeSetupNApparaitraPlusDansLesSessionsDansLesquellesIlEstPresent;
                            if (MessageBox.Show(message
                                , Application.ProductName
                                , MessageBoxButtons.YesNo
                                , MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                factory.DeleteSetup(setup);
                                RechargeListeEquipements();
                            }
                        }
                    }
                    // Objet de type Logiciel
                    else if (selectedItem.Group == listViewEquipement.Groups["Logiciels"])
                    {
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.SuppressionDUnLogiciel);
                        // Lancement Suppression
                        IObjLogiciel logiciel = factory.GetListeLogiciels().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (logiciel != null)
                        {
                            string message = $"{Resources.VoulezVousSupprimerLeLogiciel} '{logiciel.Nom}' ?" + Environment.NewLine
                                            + Resources.CeLogicielNApparaitraPlusDansLesSessionsDansLesquellesIlEstPresent;
                            if (MessageBox.Show(message
                                , Application.ProductName
                                , MessageBoxButtons.YesNo
                                , MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                factory.DeleteLogiciel(logiciel);
                                RechargeListeEquipements();
                            }
                        }
                    }// Objet de type Equipement
                    else
                    {
                        // Positionnement du texte de la Status
                        caller.SetStatusActionText(Resources.SuppressionDUnEquipement);
                        // Lancement Suppression
                        IObjEquipement equipement = factory.GetListeEquipements().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (equipement != null)
                        {
                            string message = $"{Resources.VoulezVousSupprimerLEquipement} '{equipement.Nom}' ?" + Environment.NewLine
                                            + Resources.CetEquipementNApparaitraPlusDansLesSessionsEtObservationsDansLesquellesIlEstPresent;
                            if (MessageBox.Show(message
                                , Application.ProductName
                                , MessageBoxButtons.YesNo
                                , MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                factory.DeleteEquipement(equipement);
                                RechargeListeEquipements();
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
            finally
            {
                caller.SetStatusActionText(string.Empty);
            }
        }

        /// <summary>
        /// Ajout d'un nouvel équipement
        /// </summary>
        public void AddEquipement()
        {
            try
            {
                // Positionnement du texte de la Status
                caller.SetStatusActionText(Resources.CreationDUnEquipement);
                // Lancement Edition
                dlgEquipement dlgEdition = new dlgEquipement(factory);
                if (dlgEdition.ShowDialog() == DialogResult.OK)
                {
                    RechargeListeEquipements();
                    // Update du panneau d'informations
                    UpdatePaneInfo();
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
        /// Ajout d'un nouveau Setup
        /// </summary>
        public void AddSetup()
        {
            try
            {
                // Positionnement du texte de la Status
                caller.SetStatusActionText(Resources.CreationDUnSetup);
                // Lancement Edition
                dlgSetup dlgEdition = new dlgSetup(factory);
                if (dlgEdition.ShowDialog() == DialogResult.OK)
                {
                    RechargeListeEquipements();
                    // Update du panneau d'informations
                    UpdatePaneInfo();
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
        /// Ajout d'un nouveau Site d'observation
        /// </summary>
        public void AddSite()
        {
            try
            {
                // Positionnement du texte de la Status
                caller.SetStatusActionText(Resources.CreationDUnSite);
                // Lancement Edition
                dlgSite dlgEdition = new dlgSite(factory);
                if (dlgEdition.ShowDialog() == DialogResult.OK)
                {
                    RechargeListeEquipements();
                    // Update du panneau d'informations
                    UpdatePaneInfo();
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
        /// Ajout d'un nouveau logiciel
        /// </summary>
        public void AddLogiciel()
        {
            try
            {
                // Positionnement du texte de la Status
                caller.SetStatusActionText(Resources.CreationDUnLogiciel);
                // Lancement Edition
                dlgLogiciel dlgEdition = new dlgLogiciel(factory);
                if (dlgEdition.ShowDialog() == DialogResult.OK)
                {
                    RechargeListeEquipements();
                    // Update du panneau d'informations
                    UpdatePaneInfo();
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
        /// Positionne le texte de la status pour le contexte du formulaire
        /// </summary>
        public void SetStatusText()
        {
            try
            {
                string statusText = string.Empty;
                if (caller != null && listViewEquipement != null && listViewEquipement.Groups.Count > 0)
                {
                    int nombreSites = listViewEquipement.Groups["Sites"].Items.Count;
                    int nombreSetup = listViewEquipement.Groups["Setup"].Items.Count;
                    int nombreEquipements = listViewEquipement.Items.Count - (nombreSites + nombreSetup);
                    string libelleSite = nombreSites > 1 ? Resources.Sites : Resources.Site;
                    string libelleSetup = nombreSetup > 1 ? Resources.Setups : Resources.Setup;
                    string libelleEquipement = nombreEquipements > 1 ? Resources.Equipements : Resources.Equipement;
                    statusText = $"{nombreSites} {libelleSite}, {nombreSetup} {libelleSetup}, {nombreEquipements} {libelleEquipement}";
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
        /// Positionne une nouvelle image pour le site d'observation
        /// </summary>
        private void SetImageSite()
        {
            try
            {
                // Positionnement du Curseur et Status Action text
                Cursor = Cursors.WaitCursor;
                caller.SetStatusActionText(Resources.ChargementDUneNouvelleImagePourLeSiteDObservations);

                // On vérifie qu'on a bien un site de sélectionné, et on récupère l'objet IObjSite correspondant
                if (listViewEquipement.SelectedItems.Count == 1 && !actualisationListeEnCours)
                {
                    // On récupère l'élément sélectionné
                    ListViewItem selectedItem = listViewEquipement.SelectedItems[0];
                    if (selectedItem.Group == listViewEquipement.Groups["Sites"])
                    {
                        IObjSite site = factory.GetListeSites().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (site != null)
                        {
                            // Sélection du fichier image
                            OpenFileDialog dlgOpen = new OpenFileDialog();
                            dlgOpen.Title = $"{Resources.SelectionDUneImagePourLeSite} '{site.Nom}'";
                            dlgOpen.Filter = $"{Resources.Images} JPG|*.jpg;*.jpeg;";
                            if (dlgOpen.ShowDialog() == DialogResult.OK)
                            {
                                // Repositionnement du Curseur
                                Cursor = Cursors.WaitCursor;
                                // On copie l'image dans le répertoire des datas de l'application en la renommant
                                if (CreateThumbnail(dlgOpen.FileName, site.FormatedThumbnailPathName))
                                {
                                    //File.Copy(dlgOpen.FileName, site.FormatedThumbnailPathName, true);
                                    // On enregistre le site en BDD
                                    site.Thumbnail = site.FormatedThumbnailPathName;
                                    factory.UpdateSite(site);
                                    pictureBoxImageSite.ImageLocation = site.FormatedThumbnailPathName;
                                    pictureBoxImageSite.Show();
                                    buttonImageSiteRemove.Enabled = true;
                                }
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
            finally
            {
                // Positionnement du Curseur
                Cursor = Cursors.Default;
                caller.SetStatusActionText(string.Empty);
            }
        }

        /// <summary>
        /// Permet la création d'une ThumbNail
        /// <para>300 max / 300 max</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="newFileName"></param>
        /// <returns></returns>
        private bool CreateThumbnail(string fileName, string newFileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    return false;

                string urlThumbNail = newFileName;

                Image image = Image.FromFile(fileName);
                if (image.Width == 0 || image.Height == 0)
                    return false;
                double factor = 0;
                double new_width = image.Width;
                double new_height = image.Height;
                if (image.Width >= image.Height)
                    factor = 420.0 / image.Width;
                else
                    factor = 420.0 / image.Height;
                new_width = image.Width * factor;
                new_height = image.Height * factor;

                Image thumb = image.GetThumbnailImage((int)new_width, (int)new_height, () => false, IntPtr.Zero);
                thumb.Save(urlThumbNail);

                return true;
            }
            catch (Exception err)
            {
                // Trace de l'erreur et retour
                factory.GetLog().LogException(err, GetType().Name);
                return false;
            }
        }

        /// <summary>
        /// Supprime l'image pour le site d'observation
        /// </summary>
        private void RemoveImageSite()
        {
            try
            {
                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;

                // On vérifie qu'on a bien un site de sélectionné, et on récupère l'objet IObjSite correspondant
                if (listViewEquipement.SelectedItems.Count == 1 && !actualisationListeEnCours)
                {
                    // On récupère l'élément sélectionné
                    ListViewItem selectedItem = listViewEquipement.SelectedItems[0];
                    if (selectedItem.Group == listViewEquipement.Groups["Sites"])
                    {
                        IObjSite site = factory.GetListeSites().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (site != null)
                        {
                            // On enregistre le site en BDD
                            site.Thumbnail = string.Empty;
                            factory.UpdateSite(site);
                            pictureBoxImageSite.ImageLocation = string.Empty;
                            buttonImageSiteRemove.Enabled = false;
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
            }
        }

        /// <summary>
        /// Positionne une nouvelle image pour le setup
        /// </summary>
        private void SetImageSetup()
        {
            try
            {
                // Positionnement du Curseur et Status Action text
                Cursor = Cursors.WaitCursor;
                caller.SetStatusActionText(Resources.ChargementDUneNouvelleImagePourLeSetup);

                // On vérifie qu'on a bien un setup de sélectionné, et on récupère l'objet IObjSite correspondant
                if (listViewEquipement.SelectedItems.Count == 1 && !actualisationListeEnCours)
                {
                    // On récupère l'élément sélectionné
                    ListViewItem selectedItem = listViewEquipement.SelectedItems[0];
                    if (selectedItem.Group == listViewEquipement.Groups["Setup"])
                    {
                        IObjSetup setup = factory.GetListeSetup().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (setup != null)
                        {
                            // Sélection du fichier image
                            OpenFileDialog dlgOpen = new OpenFileDialog();
                            dlgOpen.Title = $"{Resources.SelectionDUneImagePourLeSetup} '{setup.Nom}'";
                            dlgOpen.Filter = $"{Resources.Images} JPG|*.jpg;*.jpeg;";
                            if (dlgOpen.ShowDialog() == DialogResult.OK)
                            {
                                // Repositionnement du Curseur
                                Cursor = Cursors.WaitCursor;
                                // On copie l'image dans le répertoire des datas de l'application en la renommant
                                if (CreateThumbnail(dlgOpen.FileName, setup.FormatedThumbnailPathName))
                                {
                                    //File.Copy(dlgOpen.FileName, site.FormatedThumbnailPathName, true);
                                    // On enregistre le site en BDD
                                    setup.Thumbnail = setup.FormatedThumbnailPathName;
                                    factory.UpdateSetup(setup);
                                    pictureBoxImageSetup.ImageLocation = setup.FormatedThumbnailPathName;
                                    pictureBoxImageSetup.Show();
                                    buttonImageSetupRemove.Enabled = true;
                                }
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
            finally
            {
                // Positionnement du Curseur
                Cursor = Cursors.Default;
                caller.SetStatusActionText(string.Empty);
            }
        }

        /// <summary>
        /// Supprime l'image pour le Setup
        /// </summary>
        private void RemoveImageSetup()
        {
            try
            {
                // Positionnement du Curseur
                Cursor = Cursors.WaitCursor;

                // On vérifie qu'on a bien un Setup de sélectionné, et on récupère l'objet IObjSite correspondant
                if (listViewEquipement.SelectedItems.Count == 1 && !actualisationListeEnCours)
                {
                    // On récupère l'élément sélectionné
                    ListViewItem selectedItem = listViewEquipement.SelectedItems[0];
                    if (selectedItem.Group == listViewEquipement.Groups["Setup"])
                    {
                        IObjSetup setup = factory.GetListeSetup().Where(e => e.Id == selectedItem.SubItems[1].Text).FirstOrDefault();
                        if (setup != null)
                        {
                            // On enregistre le site en BDD
                            setup.Thumbnail = string.Empty;
                            factory.UpdateSetup(setup);
                            pictureBoxImageSetup.ImageLocation = string.Empty;
                            buttonImageSetupRemove.Enabled = false;
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
            }
        }

        /// <summary>
        /// Positionne l'hétat du menu contextuel
        /// </summary>
        private void SetContextMenuState()
        {
            try
            {
                modifierToolStripMenuItem.Enabled = listViewEquipement.SelectedItems.Count == 1;
                supprimerToolStripMenuItem.Enabled = listViewEquipement.SelectedItems.Count == 1;
            }
            catch (Exception err)
            {
                // Trace de l'erreur et information à l'utilisateur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Positionne l'état des boutons d'édition de la toolbar
        /// </summary>
        public void SetToolBarEditButtonState()
        {
            if (caller != null && listViewEquipement != null)
                caller.SetToolBarEditButtonState(listViewEquipement.SelectedItems.Count == 1);
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
        /// Flag permettant de savoir si l'actualisation de la Liste est en cours afin de stopper la transmission des events de modification des contrôles
        /// </summary>
        private bool actualisationListeEnCours = false;

        #endregion

        private void FormEquipements_Load(object sender, EventArgs e)
        {
        }

        private void listViewEquipement_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update du panneau d'information de l'objet sélectionné
            if (listViewEquipement.SelectedItems != null && ((ListView)sender).FocusedItem != null // && listViewEquipement.SelectedItems.Count > 0
                && !actualisationListeEnCours)
            {
                SelectItem();
            }
        }

        private void listViewEquipement_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
        }

        private void listViewEquipement_KeyDown(object sender, KeyEventArgs e)
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

        private void buttonImageSiteAdd_Click(object sender, EventArgs e)
        {
            SetImageSite();
        }

        private void buttonImageSiteRemove_Click(object sender, EventArgs e)
        {
            RemoveImageSite();
        }

        private void buttonImageSetupAdd_Click(object sender, EventArgs e)
        {
            SetImageSetup();
        }

        private void buttonImageSetupRemove_Click(object sender, EventArgs e)
        {
            RemoveImageSetup();
        }

        private void contextMenuStripEquipement_Opening(object sender, CancelEventArgs e)
        {
            SetContextMenuState();
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void siteDobservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSite();
        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSetup();
        }

        private void equipementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEquipement();
        }

        private void pictureBoxImageSetup_Click(object sender, EventArgs e)
        {
            if (pictureBoxImageSetup.Image != null && !string.IsNullOrEmpty(pictureBoxImageSetup.ImageLocation))
                Process.Start(pictureBoxImageSetup.ImageLocation);
        }

        private void pictureBoxImageSite_Click(object sender, EventArgs e)
        {
            if (pictureBoxImageSite.Image != null && !string.IsNullOrEmpty(pictureBoxImageSite.ImageLocation))
                Process.Start(pictureBoxImageSite.ImageLocation);
        }

        private void logicielToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLogiciel();
        }
    }
}
