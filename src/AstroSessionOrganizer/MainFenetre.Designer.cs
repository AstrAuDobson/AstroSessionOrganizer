namespace AstroSessionOrganizer
{
    partial class MainFenetre
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFenetre));
            this.menuStripGlobal = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvegarderLaBaseDeDonnéesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripGlobal = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusDefaultText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripGlobal = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNewSession = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonNewObjet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonNewSite = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNewSetup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNewEquipement = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNewLogiciel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonModifier = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSupprimer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonATS = new System.Windows.Forms.ToolStripButton();
            this.tabPage = new System.Windows.Forms.TabControl();
            this.tabPageSession = new System.Windows.Forms.TabPage();
            this.tabPageObjet = new System.Windows.Forms.TabPage();
            this.tabPageEquipement = new System.Windows.Forms.TabPage();
            this.backgroundWorkerStellarium = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerCdC = new System.ComponentModel.BackgroundWorker();
            this.menuStripGlobal.SuspendLayout();
            this.statusStripGlobal.SuspendLayout();
            this.toolStripGlobal.SuspendLayout();
            this.tabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripGlobal
            // 
            this.menuStripGlobal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.outilsToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStripGlobal.Location = new System.Drawing.Point(0, 0);
            this.menuStripGlobal.Name = "menuStripGlobal";
            this.menuStripGlobal.Size = new System.Drawing.Size(1295, 24);
            this.menuStripGlobal.TabIndex = 0;
            this.menuStripGlobal.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sauvegarderLaBaseDeDonnéesToolStripMenuItem,
            this.toolStripSeparator5,
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "&Fichier";
            // 
            // sauvegarderLaBaseDeDonnéesToolStripMenuItem
            // 
            this.sauvegarderLaBaseDeDonnéesToolStripMenuItem.Name = "sauvegarderLaBaseDeDonnéesToolStripMenuItem";
            this.sauvegarderLaBaseDeDonnéesToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.sauvegarderLaBaseDeDonnéesToolStripMenuItem.Text = "&Sauvegarder la base de données";
            this.sauvegarderLaBaseDeDonnéesToolStripMenuItem.Click += new System.EventHandler(this.sauvegarderLaBaseDeDonnéesToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(239, 6);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.quitterToolStripMenuItem.Text = "&Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // outilsToolStripMenuItem
            // 
            this.outilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.outilsToolStripMenuItem.Name = "outilsToolStripMenuItem";
            this.outilsToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.outilsToolStripMenuItem.Text = "&Outils";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aProposToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "&?";
            // 
            // aProposToolStripMenuItem
            // 
            this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
            this.aProposToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aProposToolStripMenuItem.Text = "A &Propos";
            this.aProposToolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
            // 
            // statusStripGlobal
            // 
            this.statusStripGlobal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusDefaultText,
            this.toolStripStatusAction});
            this.statusStripGlobal.Location = new System.Drawing.Point(0, 639);
            this.statusStripGlobal.Name = "statusStripGlobal";
            this.statusStripGlobal.Size = new System.Drawing.Size(1295, 22);
            this.statusStripGlobal.TabIndex = 1;
            this.statusStripGlobal.Text = "statusStrip1";
            // 
            // toolStripStatusDefaultText
            // 
            this.toolStripStatusDefaultText.Name = "toolStripStatusDefaultText";
            this.toolStripStatusDefaultText.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusAction
            // 
            this.toolStripStatusAction.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusAction.Name = "toolStripStatusAction";
            this.toolStripStatusAction.Size = new System.Drawing.Size(4, 17);
            // 
            // toolStripGlobal
            // 
            this.toolStripGlobal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewSession,
            this.toolStripSeparator1,
            this.toolStripButtonNewObjet,
            this.toolStripSeparator3,
            this.toolStripButtonNewSite,
            this.toolStripButtonNewSetup,
            this.toolStripButtonNewEquipement,
            this.toolStripButtonNewLogiciel,
            this.toolStripSeparator2,
            this.toolStripButtonModifier,
            this.toolStripButtonSupprimer,
            this.toolStripSeparator4,
            this.toolStripButtonATS});
            this.toolStripGlobal.Location = new System.Drawing.Point(0, 24);
            this.toolStripGlobal.Name = "toolStripGlobal";
            this.toolStripGlobal.Size = new System.Drawing.Size(1295, 25);
            this.toolStripGlobal.TabIndex = 2;
            // 
            // toolStripButtonNewSession
            // 
            this.toolStripButtonNewSession.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewSession.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_appareil_photo_3d_fluency_96;
            this.toolStripButtonNewSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewSession.Name = "toolStripButtonNewSession";
            this.toolStripButtonNewSession.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNewSession.Click += new System.EventHandler(this.toolStripButtonNewSession_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonNewObjet
            // 
            this.toolStripButtonNewObjet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewObjet.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_nebula_windows_11_color_32;
            this.toolStripButtonNewObjet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewObjet.Name = "toolStripButtonNewObjet";
            this.toolStripButtonNewObjet.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNewObjet.Click += new System.EventHandler(this.toolStripButtonNewObjet_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonNewSite
            // 
            this.toolStripButtonNewSite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewSite.Image = global::AstroSessionOrganizer.Properties.Resources.home16_centered;
            this.toolStripButtonNewSite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewSite.Name = "toolStripButtonNewSite";
            this.toolStripButtonNewSite.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNewSite.Click += new System.EventHandler(this.toolStripButtonNewSite_Click);
            // 
            // toolStripButtonNewSetup
            // 
            this.toolStripButtonNewSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewSetup.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_télescope_color_96;
            this.toolStripButtonNewSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewSetup.Name = "toolStripButtonNewSetup";
            this.toolStripButtonNewSetup.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNewSetup.Click += new System.EventHandler(this.toolStripButtonNewSetup_Click);
            // 
            // toolStripButtonNewEquipement
            // 
            this.toolStripButtonNewEquipement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewEquipement.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_telescope_mixed_line_solid_96;
            this.toolStripButtonNewEquipement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewEquipement.Name = "toolStripButtonNewEquipement";
            this.toolStripButtonNewEquipement.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNewEquipement.Click += new System.EventHandler(this.toolStripButtonNewEquipement_Click);
            // 
            // toolStripButtonNewLogiciel
            // 
            this.toolStripButtonNewLogiciel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewLogiciel.Image = global::AstroSessionOrganizer.Properties.Resources._87470_software_icon;
            this.toolStripButtonNewLogiciel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewLogiciel.Name = "toolStripButtonNewLogiciel";
            this.toolStripButtonNewLogiciel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNewLogiciel.Click += new System.EventHandler(this.toolStripButtonNewLogiciel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonModifier
            // 
            this.toolStripButtonModifier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonModifier.Image = global::AstroSessionOrganizer.Properties.Resources.EditNom16;
            this.toolStripButtonModifier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonModifier.Name = "toolStripButtonModifier";
            this.toolStripButtonModifier.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonModifier.Click += new System.EventHandler(this.toolStripButtonModifier_Click);
            // 
            // toolStripButtonSupprimer
            // 
            this.toolStripButtonSupprimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSupprimer.Image = global::AstroSessionOrganizer.Properties.Resources.delete16;
            this.toolStripButtonSupprimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSupprimer.Name = "toolStripButtonSupprimer";
            this.toolStripButtonSupprimer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSupprimer.Click += new System.EventHandler(this.toolStripButtonSupprimer_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonATS
            // 
            this.toolStripButtonATS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonATS.Image = global::AstroSessionOrganizer.Properties.Resources.Telescope;
            this.toolStripButtonATS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonATS.Name = "toolStripButtonATS";
            this.toolStripButtonATS.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonATS.ToolTipText = "Ouvrir AstroTargetSelector";
            this.toolStripButtonATS.Click += new System.EventHandler(this.toolStripButtonATS_Click);
            // 
            // tabPage
            // 
            this.tabPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPage.Controls.Add(this.tabPageSession);
            this.tabPage.Controls.Add(this.tabPageObjet);
            this.tabPage.Controls.Add(this.tabPageEquipement);
            this.tabPage.Location = new System.Drawing.Point(0, 52);
            this.tabPage.Name = "tabPage";
            this.tabPage.SelectedIndex = 0;
            this.tabPage.Size = new System.Drawing.Size(1295, 584);
            this.tabPage.TabIndex = 3;
            this.tabPage.SelectedIndexChanged += new System.EventHandler(this.tabPage_SelectedIndexChanged);
            // 
            // tabPageSession
            // 
            this.tabPageSession.Location = new System.Drawing.Point(4, 22);
            this.tabPageSession.Name = "tabPageSession";
            this.tabPageSession.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSession.Size = new System.Drawing.Size(1287, 558);
            this.tabPageSession.TabIndex = 0;
            this.tabPageSession.Text = "Sessions d\'observations";
            this.tabPageSession.UseVisualStyleBackColor = true;
            // 
            // tabPageObjet
            // 
            this.tabPageObjet.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageObjet.Location = new System.Drawing.Point(4, 22);
            this.tabPageObjet.Name = "tabPageObjet";
            this.tabPageObjet.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageObjet.Size = new System.Drawing.Size(1287, 558);
            this.tabPageObjet.TabIndex = 1;
            this.tabPageObjet.Text = "Catalogue des objets célestes";
            // 
            // tabPageEquipement
            // 
            this.tabPageEquipement.Location = new System.Drawing.Point(4, 22);
            this.tabPageEquipement.Name = "tabPageEquipement";
            this.tabPageEquipement.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEquipement.Size = new System.Drawing.Size(1287, 558);
            this.tabPageEquipement.TabIndex = 2;
            this.tabPageEquipement.Text = "Equipements et sites d\'observations";
            this.tabPageEquipement.UseVisualStyleBackColor = true;
            // 
            // backgroundWorkerStellarium
            // 
            this.backgroundWorkerStellarium.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerStellarium_DoWork);
            // 
            // backgroundWorkerCdC
            // 
            this.backgroundWorkerCdC.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerCdC_DoWork);
            // 
            // MainFenetre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 661);
            this.Controls.Add(this.tabPage);
            this.Controls.Add(this.toolStripGlobal);
            this.Controls.Add(this.statusStripGlobal);
            this.Controls.Add(this.menuStripGlobal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripGlobal;
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainFenetre";
            this.Text = "AstroSessionOrganizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFenetre_FormClosing);
            this.Load += new System.EventHandler(this.MainFenetre_Load);
            this.Shown += new System.EventHandler(this.MainFenetre_Shown);
            this.ClientSizeChanged += new System.EventHandler(this.MainFenetre_ClientSizeChanged);
            this.menuStripGlobal.ResumeLayout(false);
            this.menuStripGlobal.PerformLayout();
            this.statusStripGlobal.ResumeLayout(false);
            this.statusStripGlobal.PerformLayout();
            this.toolStripGlobal.ResumeLayout(false);
            this.toolStripGlobal.PerformLayout();
            this.tabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripGlobal;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripGlobal;
        private System.Windows.Forms.ToolStrip toolStripGlobal;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewSession;
        private System.Windows.Forms.TabControl tabPage;
        private System.Windows.Forms.TabPage tabPageSession;
        private System.Windows.Forms.TabPage tabPageObjet;
        private System.Windows.Forms.TabPage tabPageEquipement;
        private System.Windows.Forms.ToolStripMenuItem outilsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDefaultText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusAction;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewEquipement;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewSetup;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewSite;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonModifier;
        private System.Windows.Forms.ToolStripButton toolStripButtonSupprimer;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewObjet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonATS;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewLogiciel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerStellarium;
        private System.ComponentModel.BackgroundWorker backgroundWorkerCdC;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sauvegarderLaBaseDeDonnéesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}