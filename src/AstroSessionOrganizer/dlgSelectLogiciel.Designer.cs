namespace AstroSessionOrganizer
{
    partial class dlgSelectLogiciel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSelectLogiciel));
            this.pictureBoxLogiciel = new System.Windows.Forms.PictureBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listViewLogicielSession = new System.Windows.Forms.ListView();
            this.buttonEnleve = new System.Windows.Forms.Button();
            this.buttonAjoute = new System.Windows.Forms.Button();
            this.labelLogicielsAjoute = new System.Windows.Forms.Label();
            this.labelLogicielDisponible = new System.Windows.Forms.Label();
            this.listViewLogicielDisponible = new System.Windows.Forms.ListView();
            this.labelTitre = new System.Windows.Forms.Label();
            this.buttonEditLogiciel = new System.Windows.Forms.Button();
            this.toolTipLogiciel = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipAjoute = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipEnleve = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogiciel)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLogiciel
            // 
            this.pictureBoxLogiciel.Image = global::AstroSessionOrganizer.Properties.Resources._87470_software_icon;
            this.pictureBoxLogiciel.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxLogiciel.Name = "pictureBoxLogiciel";
            this.pictureBoxLogiciel.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxLogiciel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogiciel.TabIndex = 90;
            this.pictureBoxLogiciel.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(503, 248);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(584, 248);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // listViewLogicielSession
            // 
            this.listViewLogicielSession.FullRowSelect = true;
            this.listViewLogicielSession.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewLogicielSession.HideSelection = false;
            this.listViewLogicielSession.Location = new System.Drawing.Point(368, 73);
            this.listViewLogicielSession.MultiSelect = false;
            this.listViewLogicielSession.Name = "listViewLogicielSession";
            this.listViewLogicielSession.Size = new System.Drawing.Size(290, 164);
            this.listViewLogicielSession.TabIndex = 4;
            this.listViewLogicielSession.UseCompatibleStateImageBehavior = false;
            this.listViewLogicielSession.SelectedIndexChanged += new System.EventHandler(this.listViewLogicielSession_SelectedIndexChanged);
            this.listViewLogicielSession.DoubleClick += new System.EventHandler(this.listViewLogicielSession_DoubleClick);
            // 
            // buttonEnleve
            // 
            this.buttonEnleve.Image = global::AstroSessionOrganizer.Properties.Resources.gauche16;
            this.buttonEnleve.Location = new System.Drawing.Point(322, 180);
            this.buttonEnleve.Name = "buttonEnleve";
            this.buttonEnleve.Size = new System.Drawing.Size(26, 23);
            this.buttonEnleve.TabIndex = 3;
            this.buttonEnleve.UseVisualStyleBackColor = true;
            this.buttonEnleve.Click += new System.EventHandler(this.buttonEnleve_Click);
            // 
            // buttonAjoute
            // 
            this.buttonAjoute.Image = global::AstroSessionOrganizer.Properties.Resources.droite16;
            this.buttonAjoute.Location = new System.Drawing.Point(322, 151);
            this.buttonAjoute.Name = "buttonAjoute";
            this.buttonAjoute.Size = new System.Drawing.Size(26, 23);
            this.buttonAjoute.TabIndex = 2;
            this.buttonAjoute.UseVisualStyleBackColor = true;
            this.buttonAjoute.Click += new System.EventHandler(this.buttonAjoute_Click);
            // 
            // labelLogicielsAjoute
            // 
            this.labelLogicielsAjoute.AutoSize = true;
            this.labelLogicielsAjoute.Location = new System.Drawing.Point(365, 57);
            this.labelLogicielsAjoute.Name = "labelLogicielsAjoute";
            this.labelLogicielsAjoute.Size = new System.Drawing.Size(165, 13);
            this.labelLogicielsAjoute.TabIndex = 96;
            this.labelLogicielsAjoute.Text = "Logiciels utilisés lors de la session";
            // 
            // labelLogicielDisponible
            // 
            this.labelLogicielDisponible.AutoSize = true;
            this.labelLogicielDisponible.Location = new System.Drawing.Point(12, 57);
            this.labelLogicielDisponible.Name = "labelLogicielDisponible";
            this.labelLogicielDisponible.Size = new System.Drawing.Size(182, 13);
            this.labelLogicielDisponible.TabIndex = 95;
            this.labelLogicielDisponible.Text = "Logiciels supplémentaires disponibles";
            // 
            // listViewLogicielDisponible
            // 
            this.listViewLogicielDisponible.FullRowSelect = true;
            this.listViewLogicielDisponible.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewLogicielDisponible.HideSelection = false;
            this.listViewLogicielDisponible.Location = new System.Drawing.Point(12, 73);
            this.listViewLogicielDisponible.MultiSelect = false;
            this.listViewLogicielDisponible.Name = "listViewLogicielDisponible";
            this.listViewLogicielDisponible.Size = new System.Drawing.Size(290, 164);
            this.listViewLogicielDisponible.TabIndex = 0;
            this.listViewLogicielDisponible.UseCompatibleStateImageBehavior = false;
            this.listViewLogicielDisponible.SelectedIndexChanged += new System.EventHandler(this.listViewLogicielDisponible_SelectedIndexChanged);
            this.listViewLogicielDisponible.DoubleClick += new System.EventHandler(this.listViewLogicielDisponible_DoubleClick);
            // 
            // labelTitre
            // 
            this.labelTitre.AutoSize = true;
            this.labelTitre.Location = new System.Drawing.Point(55, 23);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(312, 13);
            this.labelTitre.TabIndex = 98;
            this.labelTitre.Text = "Sélectionnez les logiciels utilisés lors de la session d\'observations";
            // 
            // buttonEditLogiciel
            // 
            this.buttonEditLogiciel.BackgroundImage = global::AstroSessionOrganizer.Properties.Resources._87470_software_icon;
            this.buttonEditLogiciel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonEditLogiciel.Location = new System.Drawing.Point(322, 73);
            this.buttonEditLogiciel.Name = "buttonEditLogiciel";
            this.buttonEditLogiciel.Size = new System.Drawing.Size(26, 26);
            this.buttonEditLogiciel.TabIndex = 1;
            this.buttonEditLogiciel.UseVisualStyleBackColor = true;
            this.buttonEditLogiciel.Click += new System.EventHandler(this.buttonEditLogiciel_Click);
            // 
            // dlgSelectLogiciel
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(671, 283);
            this.Controls.Add(this.buttonEditLogiciel);
            this.Controls.Add(this.labelTitre);
            this.Controls.Add(this.listViewLogicielDisponible);
            this.Controls.Add(this.listViewLogicielSession);
            this.Controls.Add(this.buttonEnleve);
            this.Controls.Add(this.buttonAjoute);
            this.Controls.Add(this.labelLogicielsAjoute);
            this.Controls.Add(this.labelLogicielDisponible);
            this.Controls.Add(this.pictureBoxLogiciel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSelectLogiciel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sélection logiciels";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.dlgSelectLogiciel_FormClosed);
            this.Load += new System.EventHandler(this.dlgSelectLogiciel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogiciel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogiciel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListView listViewLogicielSession;
        private System.Windows.Forms.Button buttonEnleve;
        private System.Windows.Forms.Button buttonAjoute;
        private System.Windows.Forms.Label labelLogicielsAjoute;
        private System.Windows.Forms.Label labelLogicielDisponible;
        private System.Windows.Forms.ListView listViewLogicielDisponible;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Button buttonEditLogiciel;
        private System.Windows.Forms.ToolTip toolTipLogiciel;
        private System.Windows.Forms.ToolTip toolTipAjoute;
        private System.Windows.Forms.ToolTip toolTipEnleve;
    }
}