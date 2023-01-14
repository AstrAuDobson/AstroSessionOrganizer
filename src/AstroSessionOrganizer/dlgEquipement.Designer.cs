namespace AstroSessionOrganizer
{
    partial class dlgEquipement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgEquipement));
            this.comboBoxTypeEquipement = new System.Windows.Forms.ComboBox();
            this.labelTypeEquipement = new System.Windows.Forms.Label();
            this.textBoxNom = new System.Windows.Forms.TextBox();
            this.labelNom = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.pictureBoxLens = new System.Windows.Forms.PictureBox();
            this.pictureBoxDivers = new System.Windows.Forms.PictureBox();
            this.pictureBoxCamera = new System.Windows.Forms.PictureBox();
            this.pictureBoxMonture = new System.Windows.Forms.PictureBox();
            this.pictureBoxTelescope = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMonture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTelescope)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxTypeEquipement
            // 
            this.comboBoxTypeEquipement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeEquipement.FormattingEnabled = true;
            this.comboBoxTypeEquipement.Location = new System.Drawing.Point(181, 21);
            this.comboBoxTypeEquipement.Name = "comboBoxTypeEquipement";
            this.comboBoxTypeEquipement.Size = new System.Drawing.Size(243, 21);
            this.comboBoxTypeEquipement.TabIndex = 0;
            this.comboBoxTypeEquipement.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeEquipement_SelectedIndexChanged);
            // 
            // labelTypeEquipement
            // 
            this.labelTypeEquipement.AutoSize = true;
            this.labelTypeEquipement.Location = new System.Drawing.Point(78, 24);
            this.labelTypeEquipement.Name = "labelTypeEquipement";
            this.labelTypeEquipement.Size = new System.Drawing.Size(97, 13);
            this.labelTypeEquipement.TabIndex = 1;
            this.labelTypeEquipement.Text = "Type d\'équipement";
            // 
            // textBoxNom
            // 
            this.textBoxNom.Location = new System.Drawing.Point(181, 49);
            this.textBoxNom.Name = "textBoxNom";
            this.textBoxNom.Size = new System.Drawing.Size(426, 20);
            this.textBoxNom.TabIndex = 1;
            // 
            // labelNom
            // 
            this.labelNom.AutoSize = true;
            this.labelNom.Location = new System.Drawing.Point(18, 52);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(157, 13);
            this.labelNom.TabIndex = 3;
            this.labelNom.Text = "Nom / Marque / Type / Modèle";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(532, 82);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(451, 82);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // pictureBoxLens
            // 
            this.pictureBoxLens.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_lens_those_icons_lineal_color_32;
            this.pictureBoxLens.Location = new System.Drawing.Point(21, 15);
            this.pictureBoxLens.Name = "pictureBoxLens";
            this.pictureBoxLens.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxLens.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLens.TabIndex = 10;
            this.pictureBoxLens.TabStop = false;
            // 
            // pictureBoxDivers
            // 
            this.pictureBoxDivers.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_hardware_others_96;
            this.pictureBoxDivers.Location = new System.Drawing.Point(21, 15);
            this.pictureBoxDivers.Name = "pictureBoxDivers";
            this.pictureBoxDivers.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxDivers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDivers.TabIndex = 9;
            this.pictureBoxDivers.TabStop = false;
            // 
            // pictureBoxCamera
            // 
            this.pictureBoxCamera.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_appareil_photo_3d_fluency_96;
            this.pictureBoxCamera.Location = new System.Drawing.Point(21, 15);
            this.pictureBoxCamera.Name = "pictureBoxCamera";
            this.pictureBoxCamera.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCamera.TabIndex = 8;
            this.pictureBoxCamera.TabStop = false;
            // 
            // pictureBoxMonture
            // 
            this.pictureBoxMonture.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_telescope_mixed_line_solid_96;
            this.pictureBoxMonture.Location = new System.Drawing.Point(21, 15);
            this.pictureBoxMonture.Name = "pictureBoxMonture";
            this.pictureBoxMonture.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxMonture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMonture.TabIndex = 7;
            this.pictureBoxMonture.TabStop = false;
            // 
            // pictureBoxTelescope
            // 
            this.pictureBoxTelescope.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_scope_flat_96;
            this.pictureBoxTelescope.Location = new System.Drawing.Point(21, 15);
            this.pictureBoxTelescope.Name = "pictureBoxTelescope";
            this.pictureBoxTelescope.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxTelescope.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTelescope.TabIndex = 6;
            this.pictureBoxTelescope.TabStop = false;
            // 
            // dlgEquipement
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(619, 117);
            this.Controls.Add(this.pictureBoxLens);
            this.Controls.Add(this.pictureBoxDivers);
            this.Controls.Add(this.pictureBoxCamera);
            this.Controls.Add(this.pictureBoxMonture);
            this.Controls.Add(this.pictureBoxTelescope);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelNom);
            this.Controls.Add(this.textBoxNom);
            this.Controls.Add(this.labelTypeEquipement);
            this.Controls.Add(this.comboBoxTypeEquipement);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgEquipement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Equipement";
            this.Load += new System.EventHandler(this.dlgEquipement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMonture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTelescope)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTypeEquipement;
        private System.Windows.Forms.Label labelTypeEquipement;
        private System.Windows.Forms.TextBox textBoxNom;
        private System.Windows.Forms.Label labelNom;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.PictureBox pictureBoxTelescope;
        private System.Windows.Forms.PictureBox pictureBoxMonture;
        private System.Windows.Forms.PictureBox pictureBoxCamera;
        private System.Windows.Forms.PictureBox pictureBoxDivers;
        private System.Windows.Forms.PictureBox pictureBoxLens;
    }
}