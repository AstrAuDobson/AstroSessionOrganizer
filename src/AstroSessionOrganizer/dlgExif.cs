using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ApplicationTools;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue d'édition/création d'EXIF
    /// </summary>
    public partial class dlgExif : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgExif(IAppObjFactory factory, IObjSession session = null)
        {
            InitializeComponent();
            this.factory = factory;
            this.session = session;

            // Initialisation des objets
            listeDetailSession = new BindingList<Tuple<string, string>>();
            listeEquipements = new BindingList<Tuple<string, string>>();
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

                // Si pas de session associée, on throw une erreur
                if (session == null)
                    throw new Exception(Resources.AucuneSessionAssociee);

                // Positionnement du flag d'initialisation
                initialisationFormulaire = true;

                // Initialisation des composants du formulaire
                InitialisationListeDetailSession();
                InitialisationListeEquipementsSession();
                InitialisationListeObservation();
                InitialisationListeLogiciels();
                checkBoxDisplaySite.Enabled = session.Site != null;
                checkBoxDisplaySite.Checked = session.Site != null;
                checkBoxDisplayComment.Enabled = !string.IsNullOrEmpty(session.Comment);
                checkBoxDisplayComment.Checked = !string.IsNullOrEmpty(session.Comment);
                checkBoxDisplayObservations.Checked = true;
                checkBoxDisplayImageObjet.Enabled = !string.IsNullOrEmpty(session.DisplayThumbnail);
                checkBoxDisplayImageObjet.Checked = !string.IsNullOrEmpty(session.DisplayThumbnail);
                checkBoxDisplayImageConstellation.Enabled = session.ObjetCeleste.TypeObjet.Icone != "Planete"
                                                            && !string.IsNullOrEmpty(session.ObjetCeleste.Constellation.ThumbnailPosition);
                checkBoxDisplayImageConstellation.Checked = session.ObjetCeleste.TypeObjet.Icone != "Planete"
                                                            && !string.IsNullOrEmpty(session.ObjetCeleste.Constellation.ThumbnailPosition);
                checkBoxDisplayImageSetup.Enabled = session.Setup != null && !string.IsNullOrEmpty(session.Setup.Thumbnail);
                checkBoxDisplayImageSetup.Checked = session.Setup != null && !string.IsNullOrEmpty(session.Setup.Thumbnail);
                checkBoxDisplayImageSite.Enabled = session.Site != null && !string.IsNullOrEmpty(session.Site.Thumbnail);
                checkBoxDisplayImageSite.Checked = session.Site != null && !string.IsNullOrEmpty(session.Site.Thumbnail);

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
                initialisationFormulaire = false;
                UpdatePanels();
            }
        }

        /// <summary>
        /// Permet l'initialisation de la liste des équipements de la session
        /// </summary>
        private void InitialisationListeDetailSession()
        {
            try
            {
                dataGridViewDetailSession.DataSource = listeDetailSession;
                dataGridViewDetailSession.ColumnHeadersVisible = false;
                dataGridViewDetailSession.RowHeadersVisible = false;
                dataGridViewDetailSession.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridViewDetailSession.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewDetailSession.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewDetailSession.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                listeDetailSession.RaiseListChangedEvents = true;
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
        private void InitialisationListeEquipementsSession()
        {
            try
            {
                dataGridViewEquipements.DataSource = listeEquipements;
                dataGridViewEquipements.ColumnHeadersVisible = false;
                dataGridViewEquipements.RowHeadersVisible = false;
                dataGridViewEquipements.Columns[0].Visible = false;
                dataGridViewEquipements.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridViewEquipements.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewEquipements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewEquipements.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                listeEquipements.RaiseListChangedEvents = true;
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
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
                listViewObservations.View = View.Details;

                // Adjout des colonnes
                listViewObservations.Columns.Add(Resources.Date, 60, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.Type, 60, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.Filtre, 120, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.NbExpositions, 30, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.TempsUnitaire, 30, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.TempsTotal, 40, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.GainISO, 40, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.Temperature, 30, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.Binning, 30, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.Seeing, 60, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.Lune, 60, HorizontalAlignment.Left);
                listViewObservations.Columns.Add(Resources.Commentaires, 120, HorizontalAlignment.Left);
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
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
                dataGridViewLogiciels.ColumnHeadersVisible = false;
                dataGridViewLogiciels.RowHeadersVisible = false;
                dataGridViewLogiciels.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridViewLogiciels.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewLogiciels.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewLogiciels.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                listeLogicielAffichage.RaiseListChangedEvents = true;
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Chargement de l'EXIF à partir de la session
        /// </summary>
        private void LoadSession()
        {
            // Titre : Nom objet céleste
            labelNomObjetCeleste.Text = session.ObjetCeleste.Nom;

            // Listes
            PopulateListeDetailSession();
            PopulateListeEquipementsSession();
            PopulateListeObservations();
            PopulateListeLogiciels();

            // Images
            pictureBoxObjetCeleste.ImageLocation = session.DisplayThumbnail;
            pictureBoxConstellation.ImageLocation = session.ObjetCeleste.Constellation.DisplayThumbnailPosition;
            if (session.Site != null)
                pictureBoxSite.ImageLocation = session.Site.Thumbnail;
            if (session.Setup != null)
                pictureBoxSetup.ImageLocation = session.Setup.Thumbnail;
            //UpdatePanels();
            buttonCreerExif.Enabled = !string.IsNullOrEmpty(session.ImagesPath);
        }

        /// <summary>
        /// Charge la liste des détails de la session
        /// </summary>
        private void PopulateListeDetailSession()
        {
            Font boldFont = new Font(dataGridViewDetailSession.Font, FontStyle.Bold);
            dataGridViewDetailSession.Columns[0].DefaultCellStyle.Font = boldFont;
            listeDetailSession.Clear();

            // Date
            listeDetailSession.Add(new Tuple<string, string>(Resources.Date, $"{session.DateHeure.ToString("d")}"));
            // Constellation
            if (session.ObjetCeleste.TypeObjet.Icone != "Planete")
            {
                listeDetailSession.Add(new Tuple<string, string>(Resources.Constellation,
                    $"{session.ObjetCeleste.Constellation.Nom} ({session.ObjetCeleste.Constellation.Abr.ToUpper()})"));
            }
            // Type
            listeDetailSession.Add(new Tuple<string, string>(Resources.Type, $"{session.ObjetCeleste.TypeObjet.Nom}"));
            // RA
            if (session.ObjetCeleste.TypeObjet.Icone != "Planete"
                && session.ObjetCeleste.RA != null && session.ObjetCeleste.RA.Coordonnee != 0
                && session.ObjetCeleste.DEC != null && session.ObjetCeleste.DEC.Coordonnee != 0)
            {
                listeDetailSession.Add(new Tuple<string, string>(Resources.RA, $"{session.ObjetCeleste.RA.FormatedString}"));
                listeDetailSession.Add(new Tuple<string, string>(Resources.DEC, $"{session.ObjetCeleste.DEC.FormatedString}"));
            }
            // Magnitude
            if (session.ObjetCeleste.MAG_VISUAL.HasValue && session.ObjetCeleste.MAG_VISUAL.Value != 0)
            {
                listeDetailSession.Add(new Tuple<string, string>(Resources.Magnitude, $"{session.ObjetCeleste.MAG_VISUAL.Value.ToString(CultureInfo.InvariantCulture)}"));
            }
            // Site
            if (session.Site != null && session.Site.Coordonnee != null && checkBoxDisplaySite.Checked)
            {
                listeDetailSession.Add(new Tuple<string, string>(Resources.Lieu, $"{session.Site.Coordonnee.Longitude}{Environment.NewLine}{session.Site.Coordonnee.Latitude}"));
            }
            // Bortle
            if (session.Site != null && session.Site.IndiceBortle.HasValue)
            {
                listeDetailSession.Add(new Tuple<string, string>(Resources.Bortle, $"{session.Site.IndiceBortle.Value.ToString(CultureInfo.InvariantCulture)}"));
            }
            // Commentaires
            if (!string.IsNullOrEmpty(session.Comment) && checkBoxDisplayComment.Checked)
            {
                listeDetailSession.Add(new Tuple<string, string>(Resources.Commentaires, $"{session.Comment}"));
            }
            // Temps total exposition
            if (!string.IsNullOrEmpty(session.FormatedTempsTotalObservations))
            {
                listeDetailSession.Add(new Tuple<string, string>($"{Resources.TempsTotal} ({Resources.brutes})", $"{session.FormatedTempsTotalObservations}"));
            }
        }

        /// <summary>
        /// Charge la liste des équipements de la session
        /// </summary>
        private void PopulateListeEquipementsSession()
        {
            // Clear de la liste
            listeEquipements.Clear();

            // Ajout des équipements du Setup
            if (session.Setup != null)
            {
                foreach(IObjEquipementSetup equipementSetup in session.Setup.ListeEquipement)
                {
                    IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == equipementSetup.IdEquipement).FirstOrDefault();
                    if (equipement != null)
                    {
                        string denomination = equipementSetup.Nom;
                        if (string.IsNullOrEmpty(denomination))
                            denomination = $"{equipement.Nom}";
                        listeEquipements.Add(new Tuple<string, string>(equipementSetup.IdEquipement, denomination));
                    }
                }
            }

            // Ajout des équipements de la session
            foreach (IObjEquipementSession equipementSession in session.ListeEquipementsSession)
            {
                IObjEquipement equipement = factory.GetListeEquipements().Where(eq => eq.Id == equipementSession.IdEquipement).FirstOrDefault();
                if (equipement != null)
                {
                    string denomination = equipementSession.Nom;
                    if (string.IsNullOrEmpty(denomination))
                        denomination = $"{equipement.Nom}";
                    listeEquipements.Add(new Tuple<string, string>(equipementSession.IdEquipement, denomination));
                }
            }
        }

        /// <summary>
        /// Charge la liste des observations
        /// </summary>
        private void PopulateListeObservations()
        {
            // Ajout des équipements du setup
            if (session.ListeObservationsSession != null)
            {
                // Clear de la liste
                listViewObservations.Items.Clear();

                foreach (IObjObservation observation in session.ListeObservationsSession)
                {
                    listViewObservations.Items.Add(new ListViewItem(new[] {
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
                                                observation.CommentDansExif ? observation.Comment : string.Empty}));

                }
            }

            // AutoFit première colonne
            listViewObservations.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Actualise la liste des logiciels
        /// </summary>
        private void PopulateListeLogiciels()
        {
            // Clear de la liste
            listeLogicielAffichage.Clear();

            // On groupe par Type de logiciel
            foreach (IObjTypeLogiciel typeLogiciel in factory.GetListeTypeLogiciels())
            {
                string valNomSoftware = string.Empty;
                // On défini la valeur depuis la liste des logiciels de la session
                foreach (IObjLogicielSession logicielSession in session.ListeLogicielsSession)
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
        /// Mise à jour de l'affichage des panneaux d'images
        /// </summary>
        private void UpdatePanels()
        {
            if (!initialisationFormulaire)
            {
                // Observations
                splitContainerDetailExif.Panel2Collapsed = !checkBoxDisplayObservations.Checked;

                // Images
                splitContainerExif.Panel2Collapsed = false;
                splitContainerImages.Panel1Collapsed = false;
                splitContainerImages.Panel2Collapsed = false;
                splitContainerImagesObjet.Panel1Collapsed = false;
                splitContainerImagesObjet.Panel2Collapsed = false;
                splitContainerImagesConstellation.Panel1Collapsed = false;
                splitContainerImagesConstellation.Panel2Collapsed = false;
                // Aucune image
                if (checkBoxDisplayImageObjet.Checked == false && checkBoxDisplayImageConstellation.Checked == false
                    && checkBoxDisplayImageSetup.Checked == false && checkBoxDisplayImageSite.Checked == false)
                {
                    splitContainerExif.Panel2Collapsed = true;
                }
                else
                {
                    // Aucune image dans panneau Objet/Constellation
                    if (checkBoxDisplayImageObjet.Checked == false && checkBoxDisplayImageSetup.Checked == false)
                    {
                        splitContainerImages.Panel1Collapsed = true;
                    }
                    else
                    {
                        if (checkBoxDisplayImageObjet.Checked == false)
                        {
                            splitContainerImagesObjet.Panel1Collapsed = true;
                        }
                        if (checkBoxDisplayImageSetup.Checked == false)
                        {
                            splitContainerImagesObjet.Panel2Collapsed = true;
                        }
                    }
                    // Aucune image dans panneau Setup/Site
                    if (checkBoxDisplayImageConstellation.Checked == false && checkBoxDisplayImageSite.Checked == false)
                    {
                        splitContainerImages.Panel2Collapsed = true;
                    }
                    else
                    {
                        if (checkBoxDisplayImageConstellation.Checked == false)
                        {
                            splitContainerImagesConstellation.Panel1Collapsed = true;
                        }
                        if (checkBoxDisplayImageSite.Checked == false)
                        {
                            splitContainerImagesConstellation.Panel2Collapsed = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Lance la création de l'EXIF
        /// </summary>
        private void CreerExif()
        {
            try
            {
                // On repositionne les couleurs de background
                splitContainerExif.BackColor = SystemColors.Window;
                splitContainerDetailExif.BackColor = SystemColors.Window;
                splitContainerDetailSession.BackColor = SystemColors.Window;
                splitContainerExifEquipement.BackColor = SystemColors.Window;
                listViewObservations.Scrollable = false;

                // Création/sauvegarde Image
                Bitmap bmp = new Bitmap(panelExif.Width, panelExif.Height);
                Rectangle rectImage = new Rectangle(0, 0, panelExif.Width, panelExif.Height);
                panelExif.DrawToBitmap(bmp, rectImage);
                string urlImage = Path.Combine(session.ImagesPath, $"{session.ObjetCeleste.Nom.Replace(" ", "").ToLower()}_{session.DateHeure.ToString("yyyyMMdd")}_exif.jpg");
                bmp.Save(urlImage);

                // On repositionne les couleurs de background
                splitContainerExif.BackColor = Color.Gainsboro;
                splitContainerDetailExif.BackColor = Color.Gainsboro;
                splitContainerDetailSession.BackColor = Color.Gainsboro;
                splitContainerExifEquipement.BackColor = Color.Gainsboro;
                listViewObservations.Scrollable = true;

                // Ouverture Image
                if (File.Exists(urlImage))
                    Process.Start(urlImage);
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
        /// Objet IObjSession
        /// </summary>
        private IObjSession session = null;

        /// <summary>
        /// Flag permettant de savoir si l'initialisation du formulaire est en cours
        /// </summary>
        private bool initialisationFormulaire = false;

        /// <summary>
        /// Liste servant à l'affichage du détail de la session
        /// </summary>
        private BindingList<Tuple<string, string>> listeDetailSession = null;

        /// <summary>
        /// Liste servant à l'affichage des équipements
        /// </summary>
        private BindingList<Tuple<string, string>> listeEquipements = null;

        /// <summary>
        /// Liste servant à l'affichage dans la GridView
        /// </summary>
        private BindingList<Tuple<string, string>> listeLogicielAffichage = null;

        #endregion

        private void buttonCreerExif_Click(object sender, EventArgs e)
        {
            CreerExif();
        }

        private void dlgExif_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void checkBoxDisplayImageObjet_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePanels();
        }

        private void checkBoxDisplayImageConstellation_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePanels();
        }

        private void checkBoxDisplayImageSetup_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePanels();
        }

        private void checkBoxDisplayImageSite_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePanels();
        }

        private void checkBoxDisplayObservations_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePanels();
        }

        private void listViewObservations_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewObservations.SelectedItems.Clear();
        }

        private void checkBoxDisplaySite_CheckedChanged(object sender, EventArgs e)
        {
            if (!initialisationFormulaire) 
                PopulateListeDetailSession();
        }

        private void checkBoxDisplayComment_CheckedChanged(object sender, EventArgs e)
        {
            if (!initialisationFormulaire)
                PopulateListeDetailSession();
        }

        private void dlgExif_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Clear des liste internes
            if (listeLogicielAffichage != null)
            {
                listeLogicielAffichage.Clear();
                listeLogicielAffichage = null;
            }
        }
    }
}
