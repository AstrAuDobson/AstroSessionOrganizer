namespace AstroSessionOrganizer
{
    partial class dlgSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSetup));
            this.labelNom = new System.Windows.Forms.Label();
            this.textBoxNom = new System.Windows.Forms.TextBox();
            this.groupBoxEquipement = new System.Windows.Forms.GroupBox();
            this.labelEquipementAjoute = new System.Windows.Forms.Label();
            this.listBoxEquipementAjoute = new System.Windows.Forms.ListBox();
            this.labelEquipementDisponible = new System.Windows.Forms.Label();
            this.listBoxEquipementDisponible = new System.Windows.Forms.ListBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.toolTipInfosSetup = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosDenomination = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosButtonAjoute = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosButtonEnleve = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfosButtonDenomination = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxInfoSetup = new System.Windows.Forms.PictureBox();
            this.pictureBoxInfosDenomination = new System.Windows.Forms.PictureBox();
            this.buttonDenomination = new System.Windows.Forms.Button();
            this.buttonEnleve = new System.Windows.Forms.Button();
            this.buttonAjoute = new System.Windows.Forms.Button();
            this.pictureBoxSetup = new System.Windows.Forms.PictureBox();
            this.groupBoxEquipement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoSetup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosDenomination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetup)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNom
            // 
            this.labelNom.AutoSize = true;
            this.labelNom.Location = new System.Drawing.Point(78, 27);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(29, 13);
            this.labelNom.TabIndex = 12;
            this.labelNom.Text = "Nom";
            // 
            // textBoxNom
            // 
            this.textBoxNom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNom.Location = new System.Drawing.Point(113, 24);
            this.textBoxNom.Name = "textBoxNom";
            this.textBoxNom.Size = new System.Drawing.Size(629, 20);
            this.textBoxNom.TabIndex = 0;
            // 
            // groupBoxEquipement
            // 
            this.groupBoxEquipement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEquipement.Controls.Add(this.pictureBoxInfosDenomination);
            this.groupBoxEquipement.Controls.Add(this.buttonDenomination);
            this.groupBoxEquipement.Controls.Add(this.buttonEnleve);
            this.groupBoxEquipement.Controls.Add(this.buttonAjoute);
            this.groupBoxEquipement.Controls.Add(this.labelEquipementAjoute);
            this.groupBoxEquipement.Controls.Add(this.listBoxEquipementAjoute);
            this.groupBoxEquipement.Controls.Add(this.labelEquipementDisponible);
            this.groupBoxEquipement.Controls.Add(this.listBoxEquipementDisponible);
            this.groupBoxEquipement.Location = new System.Drawing.Point(13, 51);
            this.groupBoxEquipement.Name = "groupBoxEquipement";
            this.groupBoxEquipement.Size = new System.Drawing.Size(775, 225);
            this.groupBoxEquipement.TabIndex = 1;
            this.groupBoxEquipement.TabStop = false;
            this.groupBoxEquipement.Text = "Liste des équipements du setup";
            // 
            // labelEquipementAjoute
            // 
            this.labelEquipementAjoute.AutoSize = true;
            this.labelEquipementAjoute.Location = new System.Drawing.Point(393, 28);
            this.labelEquipementAjoute.Name = "labelEquipementAjoute";
            this.labelEquipementAjoute.Size = new System.Drawing.Size(149, 13);
            this.labelEquipementAjoute.TabIndex = 3;
            this.labelEquipementAjoute.Text = "Equipements ajoutés au setup";
            // 
            // listBoxEquipementAjoute
            // 
            this.listBoxEquipementAjoute.FormattingEnabled = true;
            this.listBoxEquipementAjoute.HorizontalScrollbar = true;
            this.listBoxEquipementAjoute.Location = new System.Drawing.Point(396, 43);
            this.listBoxEquipementAjoute.Name = "listBoxEquipementAjoute";
            this.listBoxEquipementAjoute.Size = new System.Drawing.Size(333, 173);
            this.listBoxEquipementAjoute.TabIndex = 5;
            this.listBoxEquipementAjoute.SelectedIndexChanged += new System.EventHandler(this.listBoxEquipementAjoute_SelectedIndexChanged);
            this.listBoxEquipementAjoute.DoubleClick += new System.EventHandler(this.listBoxEquipementAjoute_DoubleClick);
            // 
            // labelEquipementDisponible
            // 
            this.labelEquipementDisponible.AutoSize = true;
            this.labelEquipementDisponible.Location = new System.Drawing.Point(6, 28);
            this.labelEquipementDisponible.Name = "labelEquipementDisponible";
            this.labelEquipementDisponible.Size = new System.Drawing.Size(123, 13);
            this.labelEquipementDisponible.TabIndex = 1;
            this.labelEquipementDisponible.Text = "Equipements disponibles";
            // 
            // listBoxEquipementDisponible
            // 
            this.listBoxEquipementDisponible.FormattingEnabled = true;
            this.listBoxEquipementDisponible.Location = new System.Drawing.Point(6, 43);
            this.listBoxEquipementDisponible.Name = "listBoxEquipementDisponible";
            this.listBoxEquipementDisponible.Size = new System.Drawing.Size(333, 173);
            this.listBoxEquipementDisponible.TabIndex = 2;
            this.listBoxEquipementDisponible.SelectedIndexChanged += new System.EventHandler(this.listBoxEquipementDisponible_SelectedIndexChanged);
            this.listBoxEquipementDisponible.DoubleClick += new System.EventHandler(this.listBoxEquipementDisponible_DoubleClick);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(586, 282);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(667, 282);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // toolTipInfosSetup
            // 
            this.toolTipInfosSetup.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // toolTipInfosDenomination
            // 
            this.toolTipInfosDenomination.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // pictureBoxInfoSetup
            // 
            this.pictureBoxInfoSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxInfoSetup.Image = global::AstroSessionOrganizer.Properties.Resources.ico16783;
            this.pictureBoxInfoSetup.Location = new System.Drawing.Point(751, 24);
            this.pictureBoxInfoSetup.Name = "pictureBoxInfoSetup";
            this.pictureBoxInfoSetup.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxInfoSetup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxInfoSetup.TabIndex = 16;
            this.pictureBoxInfoSetup.TabStop = false;
            // 
            // pictureBoxInfosDenomination
            // 
            this.pictureBoxInfosDenomination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxInfosDenomination.Image = global::AstroSessionOrganizer.Properties.Resources.ico16783;
            this.pictureBoxInfosDenomination.Location = new System.Drawing.Point(738, 44);
            this.pictureBoxInfosDenomination.Name = "pictureBoxInfosDenomination";
            this.pictureBoxInfosDenomination.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxInfosDenomination.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxInfosDenomination.TabIndex = 17;
            this.pictureBoxInfosDenomination.TabStop = false;
            // 
            // buttonDenomination
            // 
            this.buttonDenomination.Image = global::AstroSessionOrganizer.Properties.Resources.EditNom16;
            this.buttonDenomination.Location = new System.Drawing.Point(735, 73);
            this.buttonDenomination.Name = "buttonDenomination";
            this.buttonDenomination.Size = new System.Drawing.Size(26, 26);
            this.buttonDenomination.TabIndex = 6;
            this.buttonDenomination.UseVisualStyleBackColor = true;
            this.buttonDenomination.Click += new System.EventHandler(this.buttonDenomination_Click);
            // 
            // buttonEnleve
            // 
            this.buttonEnleve.Image = global::AstroSessionOrganizer.Properties.Resources.gauche16;
            this.buttonEnleve.Location = new System.Drawing.Point(353, 88);
            this.buttonEnleve.Name = "buttonEnleve";
            this.buttonEnleve.Size = new System.Drawing.Size(26, 23);
            this.buttonEnleve.TabIndex = 4;
            this.buttonEnleve.UseVisualStyleBackColor = true;
            this.buttonEnleve.Click += new System.EventHandler(this.buttonEnleve_Click);
            // 
            // buttonAjoute
            // 
            this.buttonAjoute.Image = global::AstroSessionOrganizer.Properties.Resources.droite16;
            this.buttonAjoute.Location = new System.Drawing.Point(354, 59);
            this.buttonAjoute.Name = "buttonAjoute";
            this.buttonAjoute.Size = new System.Drawing.Size(26, 23);
            this.buttonAjoute.TabIndex = 3;
            this.buttonAjoute.UseVisualStyleBackColor = true;
            this.buttonAjoute.Click += new System.EventHandler(this.buttonAjoute_Click);
            // 
            // pictureBoxSetup
            // 
            this.pictureBoxSetup.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_télescope_color_96;
            this.pictureBoxSetup.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxSetup.Name = "pictureBoxSetup";
            this.pictureBoxSetup.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxSetup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSetup.TabIndex = 10;
            this.pictureBoxSetup.TabStop = false;
            // 
            // dlgSetup
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(800, 317);
            this.Controls.Add(this.pictureBoxInfoSetup);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxEquipement);
            this.Controls.Add(this.labelNom);
            this.Controls.Add(this.textBoxNom);
            this.Controls.Add(this.pictureBoxSetup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSetup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.dlgSetup_Load);
            this.groupBoxEquipement.ResumeLayout(false);
            this.groupBoxEquipement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoSetup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosDenomination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSetup;
        private System.Windows.Forms.Label labelNom;
        private System.Windows.Forms.TextBox textBoxNom;
        private System.Windows.Forms.GroupBox groupBoxEquipement;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListBox listBoxEquipementDisponible;
        private System.Windows.Forms.Label labelEquipementDisponible;
        private System.Windows.Forms.Label labelEquipementAjoute;
        private System.Windows.Forms.ListBox listBoxEquipementAjoute;
        private System.Windows.Forms.Button buttonAjoute;
        private System.Windows.Forms.Button buttonEnleve;
        private System.Windows.Forms.PictureBox pictureBoxInfoSetup;
        private System.Windows.Forms.ToolTip toolTipInfosSetup;
        private System.Windows.Forms.Button buttonDenomination;
        private System.Windows.Forms.PictureBox pictureBoxInfosDenomination;
        private System.Windows.Forms.ToolTip toolTipInfosDenomination;
        private System.Windows.Forms.ToolTip toolTipInfosButtonAjoute;
        private System.Windows.Forms.ToolTip toolTipInfosButtonEnleve;
        private System.Windows.Forms.ToolTip toolTipInfosButtonDenomination;
    }
}