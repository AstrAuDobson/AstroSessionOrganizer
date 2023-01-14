using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue de recherche d'un objet céleste
    /// </summary>
    public partial class dlgSearchObjetCeleste : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public dlgSearchObjetCeleste(IAppObjFactory factory, List<IObjObjetCeleste> listeObjetCeleste)
        {
            InitializeComponent();
            this.factory = factory;
            this.listeObjetCeleste = listeObjetCeleste;

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

                // Vérification de la référence sur l'objet céleste
                if (listeObjetCeleste == null || listeObjetCeleste.Count != 1 && listeObjetCeleste[0] != null)
                    throw new Exception(Resources.ReferenceSurLObjetCelesteIncorrecte);

                // Initialisation des composants
                InitialisationListeRecherche();
                textBoxRechercher.Text = listeObjetCeleste[0].Nom;
                UpdateOKButton();

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
        }

        /// <summary>
        /// Permet l'initialisation de la liste des observations de la session
        /// </summary>
        private void InitialisationListeRecherche()
        {
            // Type de vue
            listViewResultat.View = View.Details;

            // Adjout des colonnes
            listViewResultat.Columns.Add("Id", 0, HorizontalAlignment.Left);
            listViewResultat.Columns.Add(Resources.Nom, 60, HorizontalAlignment.Left);
            listViewResultat.Columns.Add(Resources.Type, 60, HorizontalAlignment.Left);
            listViewResultat.Columns.Add(Resources.Constellation, 120, HorizontalAlignment.Left);
            listViewResultat.Columns.Add(Resources.Denominations, 30, HorizontalAlignment.Left);

            // Trace
            factory.GetLog().Log("Initialisation de la liste des Objets célestes effectuée avec succès", GetType().Name);
        }

        /// <summary>
        /// Mise à jour de l'état du bouton sélectionner
        /// </summary>
        private void UpdateOKButton()
        {
            try
            {
                buttonOK.Enabled = listViewResultat.SelectedItems.Count == 1;
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
                buttonOK.Enabled = false;
            }
        }

        /// <summary>
        /// recherche de l'objet céleste
        /// </summary>
        private void SearchObjetCeleste()
        {
            try
            {
                // Clear de la liste
                listViewResultat.Items.Clear();
                pictureBoxInfosWarning.Visible = false;

                // On rempli la liste si au moins 3 caractères sont saisis
                if (textBoxRechercher.Text.Length > 2)
                {
                    List<IObjObjetCeleste> listObjetCeleste = factory.GetListeObjetCeleste().ListeComplete.Where(
                                            oc => oc.Nom.ToUpper().Replace(" ", "").Contains(textBoxRechercher.Text.ToUpper().Replace(" ", ""))
                                            || oc.TypeObjet.Nom.ToUpper().Replace(" ", "").Contains(textBoxRechercher.Text.ToUpper().Replace(" ", ""))
                                            || oc.CompleteDenominations.ToUpper().Replace(" ", "").Contains(textBoxRechercher.Text.ToUpper().Replace(" ", ""))).ToList();
                    if (listObjetCeleste.Count > 250)
                    {
                        // ToolTip Warning 
                        toolTipWarning.ToolTipTitle = Resources.InformationsSurLaRecherche;
                        string toolTipInfoWarning = $"{Resources.LaRechercheTrouveUnTropGrandNombreDeResultat} [{listObjetCeleste.Count}]"
                                                + Environment.NewLine + Resources.VeuillezAffinerVotreRecherche;
                        toolTipWarning.SetToolTip(pictureBoxInfosWarning, toolTipInfoWarning);
                        pictureBoxInfosWarning.Visible = true;
                    }
                    else
                    {
                        foreach(IObjObjetCeleste objObjetCeleste in listObjetCeleste)
                        {
                            listViewResultat.Items.Add(new ListViewItem(new[] {
                                                objObjetCeleste.Id,
                                                objObjetCeleste.Nom,
                                                objObjetCeleste.TypeObjet.Nom,
                                                objObjetCeleste.Constellation.Nom,
                                                objObjetCeleste.DenominationsFormated}));
                        }
                        // AutoFit première colonne
                        listViewResultat.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        if (listViewResultat.Columns.Count > 0)
                            listViewResultat.Columns[0].Width = 0;
                    }
                }
                else
                {
                    // ToolTip Warning 
                    toolTipWarning.ToolTipTitle = Resources.InformationsSurLaRecherche;
                    string toolTipInfoWarning = Resources.VeuillezSaisirAuMoins3CaracteresAfinDeLancerLaRecherche;
                    toolTipWarning.SetToolTip(pictureBoxInfosWarning, toolTipInfoWarning);
                    pictureBoxInfosWarning.Visible = true;
                }
            }
            catch (Exception err)
            {
                // Trace de l'erreur
                factory.GetLog().LogException(err, GetType().Name);
            }
        }

        /// <summary>
        /// Sélection d'un objet céleste dans la liste de recherche
        /// </summary>
        private void SelectObjetCeleste()
        {
            try
            {
                if (listViewResultat.SelectedItems.Count == 1)
                {
                    IObjObjetCeleste objetSelectionne = factory.GetListeObjetCeleste().ListeComplete.Where(oc => oc.Id == listViewResultat.SelectedItems[0].SubItems[0].Text).FirstOrDefault();
                    if (objetSelectionne != null)
                    {
                        listeObjetCeleste.Clear();
                        listeObjetCeleste.Add(objetSelectionne);
                        DialogResult = DialogResult.OK;
                        Close();
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
        /// Référence sur l'objet céleste du parent
        /// </summary>
        private List<IObjObjetCeleste> listeObjetCeleste  = null;

        #endregion

        private void dlgSearchObjetCeleste_Load(object sender, EventArgs e)
        {
            InitialisationFormulaire();
        }

        private void listViewResultat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOKButton();
        }

        private void textBoxRechercher_TextChanged(object sender, EventArgs e)
        {
            SearchObjetCeleste();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SelectObjetCeleste();
        }

        private void listViewResultat_DoubleClick(object sender, EventArgs e)
        {
            SelectObjetCeleste();
        }
    }
}
