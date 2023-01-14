using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using AstroSessionOrganizerModule;
using AstroSessionOrganizerResources;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de Dialogue "A Propos"
    /// </summary>
    partial class dlgAPropos : Form
    {
        #region Propriétés

        /// <summary>
        /// Titre de l'Assembly
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Version
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Produit
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Copyright
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Company
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="factory"></param>
        public dlgAPropos(IAppObjFactory factory)
        {
            InitializeComponent();
            this.factory = factory;

            // Positionnement des libellés
            this.Text = $"{Resources.AProposDe} {AssemblyTitle}";
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = $"{Resources.Version} {Resources.BETA} {AssemblyVersion}";
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.labelContributors.Text = $"{Resources.Contributeurs}: Xavier SCHILDKNECHT / Jean-Luc VIVO";
            this.textBoxDescription.Text = AssemblyDescription;

            textBoxDescription.Text += Environment.NewLine + Environment.NewLine;
            textBoxDescription.Text += Resources.TouteUtilisationDesDonneesDuCatalogueADesFinsCommercialesEstStrictementProhibee + Environment.NewLine;
            textBoxDescription.Text += Resources.PourUneUtilisationDeCesDonneesMemeADesFinsNonCommerciale + Environment.NewLine;
            textBoxDescription.Text += Resources.CesDonneesFontPartieDUnProjetScientifiqueSoumisAuCopyright;

            // Positionne le mode Jour/Nuit
            SetAffichage();

            // Trace
            factory.GetLog().Log($"Ouverture de la boîte de dialogue", GetType().Name);
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Ouvre le fichier de Log
        /// </summary>
        public void OpenLogFile()
        {
            try
            {
                // Trace
                factory.GetLog().Log($"Ouverture du fichier de log : {factory.GetLog().FullPathName}", GetType().Name);

                // Ouverture du fichier de log
                Process.Start(factory.GetLog().FullPathName);
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
        /// Positionne l'affichage en mode Jour / Nuit
        /// </summary>
        private void SetAffichage()
        {
            //TODO
        }

        #endregion

        #region Champs

        /// <summary>
        /// Instance de la fabrique d'objet métier
        /// </summary>
        private readonly IAppObjFactory factory = null;

        #endregion

        private void buttonSendLog_Click(object sender, EventArgs e)
        {
            OpenLogFile();
        }
    }
}
