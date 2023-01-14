namespace AstroSessionOrganizer
{
    partial class dlgOptions
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgOptions));
            this.groupBoxStellarium = new System.Windows.Forms.GroupBox();
            this.labelServeurCdC = new System.Windows.Forms.Label();
            this.textBoxHostCartesDuCiel = new System.Windows.Forms.TextBox();
            this.labelSepPlanetarium = new System.Windows.Forms.Label();
            this.labelServeurStellarium = new System.Windows.Forms.Label();
            this.textBoxHostStellarium = new System.Windows.Forms.TextBox();
            this.labelPortStellarium = new System.Windows.Forms.Label();
            this.textBoxPortStellarium = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.pictureBoxIconInfoCartesDuCiel = new System.Windows.Forms.PictureBox();
            this.pictureBoxIconInfoStellarium = new System.Windows.Forms.PictureBox();
            this.groupBoxATS = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxATSServeur = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxATSPort = new System.Windows.Forms.TextBox();
            this.groupBoxBDD = new System.Windows.Forms.GroupBox();
            this.radioButtonBDDLocal = new System.Windows.Forms.RadioButton();
            this.radioButtonBDDReseau = new System.Windows.Forms.RadioButton();
            this.labelBDDPath = new System.Windows.Forms.Label();
            this.textBoxBDDPath = new System.Windows.Forms.TextBox();
            this.buttonSearchBDDReseeau = new System.Windows.Forms.Button();
            this.toolTipInfoStellarium = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfoCartesDuCiel = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxStellarium.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIconInfoCartesDuCiel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIconInfoStellarium)).BeginInit();
            this.groupBoxATS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxBDD.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStellarium
            // 
            this.groupBoxStellarium.Controls.Add(this.pictureBoxIconInfoCartesDuCiel);
            this.groupBoxStellarium.Controls.Add(this.labelServeurCdC);
            this.groupBoxStellarium.Controls.Add(this.textBoxHostCartesDuCiel);
            this.groupBoxStellarium.Controls.Add(this.labelSepPlanetarium);
            this.groupBoxStellarium.Controls.Add(this.pictureBoxIconInfoStellarium);
            this.groupBoxStellarium.Controls.Add(this.labelServeurStellarium);
            this.groupBoxStellarium.Controls.Add(this.textBoxHostStellarium);
            this.groupBoxStellarium.Controls.Add(this.labelPortStellarium);
            this.groupBoxStellarium.Controls.Add(this.textBoxPortStellarium);
            this.groupBoxStellarium.Location = new System.Drawing.Point(12, 210);
            this.groupBoxStellarium.Name = "groupBoxStellarium";
            this.groupBoxStellarium.Size = new System.Drawing.Size(385, 129);
            this.groupBoxStellarium.TabIndex = 7;
            this.groupBoxStellarium.TabStop = false;
            this.groupBoxStellarium.Text = "Paramètres des planétariums (Stellarium / Cartes du Ciel)";
            // 
            // labelServeurCdC
            // 
            this.labelServeurCdC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelServeurCdC.Location = new System.Drawing.Point(92, 97);
            this.labelServeurCdC.Name = "labelServeurCdC";
            this.labelServeurCdC.Size = new System.Drawing.Size(60, 21);
            this.labelServeurCdC.TabIndex = 28;
            this.labelServeurCdC.Text = "Serveur";
            this.labelServeurCdC.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxHostCartesDuCiel
            // 
            this.textBoxHostCartesDuCiel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxHostCartesDuCiel.Location = new System.Drawing.Point(158, 94);
            this.textBoxHostCartesDuCiel.Name = "textBoxHostCartesDuCiel";
            this.textBoxHostCartesDuCiel.Size = new System.Drawing.Size(100, 20);
            this.textBoxHostCartesDuCiel.TabIndex = 2;
            // 
            // labelSepPlanetarium
            // 
            this.labelSepPlanetarium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSepPlanetarium.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSepPlanetarium.Location = new System.Drawing.Point(9, 82);
            this.labelSepPlanetarium.Name = "labelSepPlanetarium";
            this.labelSepPlanetarium.Size = new System.Drawing.Size(248, 1);
            this.labelSepPlanetarium.TabIndex = 26;
            // 
            // labelServeurStellarium
            // 
            this.labelServeurStellarium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelServeurStellarium.Location = new System.Drawing.Point(92, 30);
            this.labelServeurStellarium.Name = "labelServeurStellarium";
            this.labelServeurStellarium.Size = new System.Drawing.Size(60, 21);
            this.labelServeurStellarium.TabIndex = 14;
            this.labelServeurStellarium.Text = "Serveur";
            this.labelServeurStellarium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxHostStellarium
            // 
            this.textBoxHostStellarium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxHostStellarium.Location = new System.Drawing.Point(158, 27);
            this.textBoxHostStellarium.Name = "textBoxHostStellarium";
            this.textBoxHostStellarium.Size = new System.Drawing.Size(100, 20);
            this.textBoxHostStellarium.TabIndex = 0;
            // 
            // labelPortStellarium
            // 
            this.labelPortStellarium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPortStellarium.Location = new System.Drawing.Point(92, 56);
            this.labelPortStellarium.Name = "labelPortStellarium";
            this.labelPortStellarium.Size = new System.Drawing.Size(60, 17);
            this.labelPortStellarium.TabIndex = 11;
            this.labelPortStellarium.Text = "Port";
            this.labelPortStellarium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxPortStellarium
            // 
            this.textBoxPortStellarium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPortStellarium.Location = new System.Drawing.Point(158, 53);
            this.textBoxPortStellarium.Name = "textBoxPortStellarium";
            this.textBoxPortStellarium.Size = new System.Drawing.Size(100, 20);
            this.textBoxPortStellarium.TabIndex = 1;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.Location = new System.Drawing.Point(241, 347);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 8;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(322, 347);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 9;
            this.btCancel.Text = "Annuler";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // pictureBoxIconInfoCartesDuCiel
            // 
            this.pictureBoxIconInfoCartesDuCiel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxIconInfoCartesDuCiel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxIconInfoCartesDuCiel.Image = global::AstroSessionOrganizer.Properties.Resources.cartesduciel;
            this.pictureBoxIconInfoCartesDuCiel.Location = new System.Drawing.Point(54, 87);
            this.pictureBoxIconInfoCartesDuCiel.Name = "pictureBoxIconInfoCartesDuCiel";
            this.pictureBoxIconInfoCartesDuCiel.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxIconInfoCartesDuCiel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxIconInfoCartesDuCiel.TabIndex = 29;
            this.pictureBoxIconInfoCartesDuCiel.TabStop = false;
            // 
            // pictureBoxIconInfoStellarium
            // 
            this.pictureBoxIconInfoStellarium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxIconInfoStellarium.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxIconInfoStellarium.Image = global::AstroSessionOrganizer.Properties.Resources.stellarium;
            this.pictureBoxIconInfoStellarium.Location = new System.Drawing.Point(54, 34);
            this.pictureBoxIconInfoStellarium.Name = "pictureBoxIconInfoStellarium";
            this.pictureBoxIconInfoStellarium.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxIconInfoStellarium.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxIconInfoStellarium.TabIndex = 25;
            this.pictureBoxIconInfoStellarium.TabStop = false;
            // 
            // groupBoxATS
            // 
            this.groupBoxATS.Controls.Add(this.pictureBox1);
            this.groupBoxATS.Controls.Add(this.label1);
            this.groupBoxATS.Controls.Add(this.textBoxATSServeur);
            this.groupBoxATS.Controls.Add(this.label2);
            this.groupBoxATS.Controls.Add(this.textBoxATSPort);
            this.groupBoxATS.Location = new System.Drawing.Point(12, 119);
            this.groupBoxATS.Name = "groupBoxATS";
            this.groupBoxATS.Size = new System.Drawing.Size(385, 85);
            this.groupBoxATS.TabIndex = 10;
            this.groupBoxATS.TabStop = false;
            this.groupBoxATS.Text = "Paramètre de connexion à AstroTargetSelector (bientôt disponible)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::AstroSessionOrganizer.Properties.Resources.Telescope;
            this.pictureBox1.Location = new System.Drawing.Point(53, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(91, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 21);
            this.label1.TabIndex = 29;
            this.label1.Text = "Serveur";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxATSServeur
            // 
            this.textBoxATSServeur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxATSServeur.Enabled = false;
            this.textBoxATSServeur.Location = new System.Drawing.Point(157, 27);
            this.textBoxATSServeur.Name = "textBoxATSServeur";
            this.textBoxATSServeur.ReadOnly = true;
            this.textBoxATSServeur.Size = new System.Drawing.Size(100, 20);
            this.textBoxATSServeur.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(91, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "Port";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxATSPort
            // 
            this.textBoxATSPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxATSPort.Enabled = false;
            this.textBoxATSPort.Location = new System.Drawing.Point(157, 53);
            this.textBoxATSPort.Name = "textBoxATSPort";
            this.textBoxATSPort.ReadOnly = true;
            this.textBoxATSPort.Size = new System.Drawing.Size(100, 20);
            this.textBoxATSPort.TabIndex = 27;
            // 
            // groupBoxBDD
            // 
            this.groupBoxBDD.Controls.Add(this.buttonSearchBDDReseeau);
            this.groupBoxBDD.Controls.Add(this.labelBDDPath);
            this.groupBoxBDD.Controls.Add(this.textBoxBDDPath);
            this.groupBoxBDD.Controls.Add(this.radioButtonBDDReseau);
            this.groupBoxBDD.Controls.Add(this.radioButtonBDDLocal);
            this.groupBoxBDD.Location = new System.Drawing.Point(12, 12);
            this.groupBoxBDD.Name = "groupBoxBDD";
            this.groupBoxBDD.Size = new System.Drawing.Size(385, 100);
            this.groupBoxBDD.TabIndex = 11;
            this.groupBoxBDD.TabStop = false;
            this.groupBoxBDD.Text = "Base de données (bientôt disponible)";
            // 
            // radioButtonBDDLocal
            // 
            this.radioButtonBDDLocal.AutoSize = true;
            this.radioButtonBDDLocal.Checked = true;
            this.radioButtonBDDLocal.Enabled = false;
            this.radioButtonBDDLocal.Location = new System.Drawing.Point(53, 20);
            this.radioButtonBDDLocal.Name = "radioButtonBDDLocal";
            this.radioButtonBDDLocal.Size = new System.Drawing.Size(162, 17);
            this.radioButtonBDDLocal.TabIndex = 0;
            this.radioButtonBDDLocal.TabStop = true;
            this.radioButtonBDDLocal.Text = "Utilisation sur ce poste (local)";
            this.radioButtonBDDLocal.UseVisualStyleBackColor = true;
            // 
            // radioButtonBDDReseau
            // 
            this.radioButtonBDDReseau.AutoSize = true;
            this.radioButtonBDDReseau.Enabled = false;
            this.radioButtonBDDReseau.Location = new System.Drawing.Point(53, 43);
            this.radioButtonBDDReseau.Name = "radioButtonBDDReseau";
            this.radioButtonBDDReseau.Size = new System.Drawing.Size(120, 17);
            this.radioButtonBDDReseau.TabIndex = 1;
            this.radioButtonBDDReseau.Text = "Utilisation en réseau";
            this.radioButtonBDDReseau.UseVisualStyleBackColor = true;
            // 
            // labelBDDPath
            // 
            this.labelBDDPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBDDPath.Enabled = false;
            this.labelBDDPath.Location = new System.Drawing.Point(14, 69);
            this.labelBDDPath.Name = "labelBDDPath";
            this.labelBDDPath.Size = new System.Drawing.Size(137, 17);
            this.labelBDDPath.TabIndex = 31;
            this.labelBDDPath.Text = "Base de données";
            this.labelBDDPath.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxBDDPath
            // 
            this.textBoxBDDPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxBDDPath.Enabled = false;
            this.textBoxBDDPath.Location = new System.Drawing.Point(156, 66);
            this.textBoxBDDPath.Name = "textBoxBDDPath";
            this.textBoxBDDPath.ReadOnly = true;
            this.textBoxBDDPath.Size = new System.Drawing.Size(187, 20);
            this.textBoxBDDPath.TabIndex = 30;
            // 
            // buttonSearchBDDReseeau
            // 
            this.buttonSearchBDDReseeau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSearchBDDReseeau.Enabled = false;
            this.buttonSearchBDDReseeau.Location = new System.Drawing.Point(349, 64);
            this.buttonSearchBDDReseeau.Name = "buttonSearchBDDReseeau";
            this.buttonSearchBDDReseeau.Size = new System.Drawing.Size(30, 23);
            this.buttonSearchBDDReseeau.TabIndex = 32;
            this.buttonSearchBDDReseeau.Text = "...";
            this.buttonSearchBDDReseeau.UseVisualStyleBackColor = true;
            // 
            // toolTipInfoStellarium
            // 
            this.toolTipInfoStellarium.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // toolTipInfoCartesDuCiel
            // 
            this.toolTipInfoCartesDuCiel.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // dlgOptions
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(409, 382);
            this.Controls.Add(this.groupBoxBDD);
            this.Controls.Add(this.groupBoxATS);
            this.Controls.Add(this.groupBoxStellarium);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.dlgOptions_Load);
            this.groupBoxStellarium.ResumeLayout(false);
            this.groupBoxStellarium.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIconInfoCartesDuCiel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIconInfoStellarium)).EndInit();
            this.groupBoxATS.ResumeLayout(false);
            this.groupBoxATS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxBDD.ResumeLayout(false);
            this.groupBoxBDD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxStellarium;
        private System.Windows.Forms.PictureBox pictureBoxIconInfoCartesDuCiel;
        private System.Windows.Forms.Label labelServeurCdC;
        private System.Windows.Forms.TextBox textBoxHostCartesDuCiel;
        private System.Windows.Forms.Label labelSepPlanetarium;
        private System.Windows.Forms.PictureBox pictureBoxIconInfoStellarium;
        private System.Windows.Forms.Label labelServeurStellarium;
        private System.Windows.Forms.TextBox textBoxHostStellarium;
        private System.Windows.Forms.Label labelPortStellarium;
        private System.Windows.Forms.TextBox textBoxPortStellarium;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox groupBoxATS;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxATSServeur;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxATSPort;
        private System.Windows.Forms.GroupBox groupBoxBDD;
        private System.Windows.Forms.RadioButton radioButtonBDDLocal;
        private System.Windows.Forms.Label labelBDDPath;
        private System.Windows.Forms.TextBox textBoxBDDPath;
        private System.Windows.Forms.RadioButton radioButtonBDDReseau;
        private System.Windows.Forms.Button buttonSearchBDDReseeau;
        private System.Windows.Forms.ToolTip toolTipInfoStellarium;
        private System.Windows.Forms.ToolTip toolTipInfoCartesDuCiel;
    }
}