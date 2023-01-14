using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstroSessionOrganizer
{
    /// <summary>
    /// Boîte de dialogue d'attente pour traitement long
    /// </summary>
    public partial class WaitDialog : Form
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public WaitDialog(Action worker, string libelleAction)
        {
            InitializeComponent();
            this.worker = worker;

            // Vérif des paramètres
            if (worker == null || string.IsNullOrEmpty(libelleAction))
                throw new ArgumentNullException();
            labelWorker.Text = libelleAction;
        }

        #endregion

        #region Champs

        /// <summary>
        /// Background worker d'exécution de l'action
        /// </summary>
        private Action worker = null;

        #endregion

        private void WaitDialog_Load(object sender, EventArgs e)
        {
            // Après exécution de la tâche passée à la consruction, on Close
            Task.Factory.StartNew(worker).ContinueWith(t => Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
