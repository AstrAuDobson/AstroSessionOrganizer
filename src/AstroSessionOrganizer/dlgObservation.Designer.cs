namespace AstroSessionOrganizer
{
    partial class dlgObservation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgObservation));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.dateTimePickerDateObservation = new System.Windows.Forms.DateTimePicker();
            this.labelDate = new System.Windows.Forms.Label();
            this.dateTimePickerHeureObservation = new System.Windows.Forms.DateTimePicker();
            this.comboBoxTypeObservation = new System.Windows.Forms.ComboBox();
            this.labelTypeObservation = new System.Windows.Forms.Label();
            this.labelFiltre = new System.Windows.Forms.Label();
            this.comboBoxFiltre = new System.Windows.Forms.ComboBox();
            this.checkBoxComment = new System.Windows.Forms.CheckBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.textBoxNbrExpositions = new System.Windows.Forms.TextBox();
            this.labelNbExpositions = new System.Windows.Forms.Label();
            this.labelTempsUnitaire = new System.Windows.Forms.Label();
            this.textBoxTempsUnitaire = new System.Windows.Forms.TextBox();
            this.labelGain = new System.Windows.Forms.Label();
            this.textBoxGain = new System.Windows.Forms.TextBox();
            this.labelTemperature = new System.Windows.Forms.Label();
            this.textBoxTemperature = new System.Windows.Forms.TextBox();
            this.labelBinning = new System.Windows.Forms.Label();
            this.textBoxBinning = new System.Windows.Forms.TextBox();
            this.labelSeeing = new System.Windows.Forms.Label();
            this.textBoxSeeing = new System.Windows.Forms.TextBox();
            this.labelLune = new System.Windows.Forms.Label();
            this.textBoxLune = new System.Windows.Forms.TextBox();
            this.toolTipComment = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxTempsTotal = new System.Windows.Forms.TextBox();
            this.labelTempsTotal = new System.Windows.Forms.Label();
            this.pictureBoxInfosComment = new System.Windows.Forms.PictureBox();
            this.pictureBoxObservation = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxObservation)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(248, 358);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 13;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(329, 358);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerDateObservation
            // 
            this.dateTimePickerDateObservation.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDateObservation.Location = new System.Drawing.Point(148, 12);
            this.dateTimePickerDateObservation.Name = "dateTimePickerDateObservation";
            this.dateTimePickerDateObservation.Size = new System.Drawing.Size(125, 20);
            this.dateTimePickerDateObservation.TabIndex = 0;
            // 
            // labelDate
            // 
            this.labelDate.Location = new System.Drawing.Point(101, 9);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(41, 23);
            this.labelDate.TabIndex = 60;
            this.labelDate.Text = "Date";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePickerHeureObservation
            // 
            this.dateTimePickerHeureObservation.CustomFormat = "HH:mm tt";
            this.dateTimePickerHeureObservation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerHeureObservation.Location = new System.Drawing.Point(279, 12);
            this.dateTimePickerHeureObservation.Name = "dateTimePickerHeureObservation";
            this.dateTimePickerHeureObservation.ShowUpDown = true;
            this.dateTimePickerHeureObservation.Size = new System.Drawing.Size(125, 20);
            this.dateTimePickerHeureObservation.TabIndex = 1;
            // 
            // comboBoxTypeObservation
            // 
            this.comboBoxTypeObservation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeObservation.FormattingEnabled = true;
            this.comboBoxTypeObservation.Location = new System.Drawing.Point(148, 39);
            this.comboBoxTypeObservation.Name = "comboBoxTypeObservation";
            this.comboBoxTypeObservation.Size = new System.Drawing.Size(125, 21);
            this.comboBoxTypeObservation.TabIndex = 2;
            // 
            // labelTypeObservation
            // 
            this.labelTypeObservation.Location = new System.Drawing.Point(101, 37);
            this.labelTypeObservation.Name = "labelTypeObservation";
            this.labelTypeObservation.Size = new System.Drawing.Size(41, 23);
            this.labelTypeObservation.TabIndex = 64;
            this.labelTypeObservation.Text = "Type";
            this.labelTypeObservation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelFiltre
            // 
            this.labelFiltre.Location = new System.Drawing.Point(101, 64);
            this.labelFiltre.Name = "labelFiltre";
            this.labelFiltre.Size = new System.Drawing.Size(41, 23);
            this.labelFiltre.TabIndex = 66;
            this.labelFiltre.Text = "Filtre";
            this.labelFiltre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxFiltre
            // 
            this.comboBoxFiltre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFiltre.FormattingEnabled = true;
            this.comboBoxFiltre.Location = new System.Drawing.Point(148, 66);
            this.comboBoxFiltre.Name = "comboBoxFiltre";
            this.comboBoxFiltre.Size = new System.Drawing.Size(256, 21);
            this.comboBoxFiltre.TabIndex = 3;
            // 
            // checkBoxComment
            // 
            this.checkBoxComment.AutoSize = true;
            this.checkBoxComment.Location = new System.Drawing.Point(148, 328);
            this.checkBoxComment.Name = "checkBoxComment";
            this.checkBoxComment.Size = new System.Drawing.Size(243, 17);
            this.checkBoxComment.TabIndex = 12;
            this.checkBoxComment.Text = "Faire apparaître le commentaire dans les EXIF";
            this.checkBoxComment.UseVisualStyleBackColor = true;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(148, 301);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(256, 20);
            this.textBoxComment.TabIndex = 11;
            // 
            // labelComment
            // 
            this.labelComment.Location = new System.Drawing.Point(63, 299);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(79, 23);
            this.labelComment.TabIndex = 74;
            this.labelComment.Text = "Commentaires";
            this.labelComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNbrExpositions
            // 
            this.textBoxNbrExpositions.Location = new System.Drawing.Point(148, 93);
            this.textBoxNbrExpositions.Name = "textBoxNbrExpositions";
            this.textBoxNbrExpositions.Size = new System.Drawing.Size(125, 20);
            this.textBoxNbrExpositions.TabIndex = 4;
            this.textBoxNbrExpositions.TextChanged += new System.EventHandler(this.textBoxNbrExpositions_TextChanged);
            // 
            // labelNbExpositions
            // 
            this.labelNbExpositions.Location = new System.Drawing.Point(12, 91);
            this.labelNbExpositions.Name = "labelNbExpositions";
            this.labelNbExpositions.Size = new System.Drawing.Size(130, 23);
            this.labelNbExpositions.TabIndex = 78;
            this.labelNbExpositions.Text = "Nb. d\'expositions";
            this.labelNbExpositions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTempsUnitaire
            // 
            this.labelTempsUnitaire.Location = new System.Drawing.Point(3, 117);
            this.labelTempsUnitaire.Name = "labelTempsUnitaire";
            this.labelTempsUnitaire.Size = new System.Drawing.Size(139, 23);
            this.labelTempsUnitaire.TabIndex = 80;
            this.labelTempsUnitaire.Text = "Temps de pose unitaire (s)";
            this.labelTempsUnitaire.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTempsUnitaire
            // 
            this.textBoxTempsUnitaire.Location = new System.Drawing.Point(148, 119);
            this.textBoxTempsUnitaire.Name = "textBoxTempsUnitaire";
            this.textBoxTempsUnitaire.Size = new System.Drawing.Size(125, 20);
            this.textBoxTempsUnitaire.TabIndex = 5;
            this.textBoxTempsUnitaire.TextChanged += new System.EventHandler(this.textBoxTempsUnitaire_TextChanged);
            // 
            // labelGain
            // 
            this.labelGain.Location = new System.Drawing.Point(12, 169);
            this.labelGain.Name = "labelGain";
            this.labelGain.Size = new System.Drawing.Size(130, 23);
            this.labelGain.TabIndex = 82;
            this.labelGain.Text = "Gain / ISO";
            this.labelGain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxGain
            // 
            this.textBoxGain.Location = new System.Drawing.Point(148, 171);
            this.textBoxGain.Name = "textBoxGain";
            this.textBoxGain.Size = new System.Drawing.Size(125, 20);
            this.textBoxGain.TabIndex = 6;
            // 
            // labelTemperature
            // 
            this.labelTemperature.Location = new System.Drawing.Point(12, 195);
            this.labelTemperature.Name = "labelTemperature";
            this.labelTemperature.Size = new System.Drawing.Size(130, 23);
            this.labelTemperature.TabIndex = 84;
            this.labelTemperature.Text = "Température capteur";
            this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTemperature
            // 
            this.textBoxTemperature.Location = new System.Drawing.Point(148, 197);
            this.textBoxTemperature.Name = "textBoxTemperature";
            this.textBoxTemperature.Size = new System.Drawing.Size(125, 20);
            this.textBoxTemperature.TabIndex = 7;
            // 
            // labelBinning
            // 
            this.labelBinning.Location = new System.Drawing.Point(12, 221);
            this.labelBinning.Name = "labelBinning";
            this.labelBinning.Size = new System.Drawing.Size(130, 23);
            this.labelBinning.TabIndex = 86;
            this.labelBinning.Text = "Binning";
            this.labelBinning.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxBinning
            // 
            this.textBoxBinning.Location = new System.Drawing.Point(148, 223);
            this.textBoxBinning.Name = "textBoxBinning";
            this.textBoxBinning.Size = new System.Drawing.Size(125, 20);
            this.textBoxBinning.TabIndex = 8;
            // 
            // labelSeeing
            // 
            this.labelSeeing.Location = new System.Drawing.Point(12, 247);
            this.labelSeeing.Name = "labelSeeing";
            this.labelSeeing.Size = new System.Drawing.Size(130, 23);
            this.labelSeeing.TabIndex = 88;
            this.labelSeeing.Text = "Seeing";
            this.labelSeeing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxSeeing
            // 
            this.textBoxSeeing.Location = new System.Drawing.Point(148, 249);
            this.textBoxSeeing.Name = "textBoxSeeing";
            this.textBoxSeeing.Size = new System.Drawing.Size(125, 20);
            this.textBoxSeeing.TabIndex = 9;
            // 
            // labelLune
            // 
            this.labelLune.Location = new System.Drawing.Point(12, 273);
            this.labelLune.Name = "labelLune";
            this.labelLune.Size = new System.Drawing.Size(130, 23);
            this.labelLune.TabIndex = 90;
            this.labelLune.Text = "Lune";
            this.labelLune.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxLune
            // 
            this.textBoxLune.Location = new System.Drawing.Point(148, 275);
            this.textBoxLune.Name = "textBoxLune";
            this.textBoxLune.Size = new System.Drawing.Size(125, 20);
            this.textBoxLune.TabIndex = 10;
            // 
            // toolTipComment
            // 
            this.toolTipComment.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // textBoxTempsTotal
            // 
            this.textBoxTempsTotal.Location = new System.Drawing.Point(148, 145);
            this.textBoxTempsTotal.Name = "textBoxTempsTotal";
            this.textBoxTempsTotal.ReadOnly = true;
            this.textBoxTempsTotal.Size = new System.Drawing.Size(125, 20);
            this.textBoxTempsTotal.TabIndex = 91;
            this.textBoxTempsTotal.TabStop = false;
            // 
            // labelTempsTotal
            // 
            this.labelTempsTotal.Location = new System.Drawing.Point(3, 143);
            this.labelTempsTotal.Name = "labelTempsTotal";
            this.labelTempsTotal.Size = new System.Drawing.Size(139, 23);
            this.labelTempsTotal.TabIndex = 92;
            this.labelTempsTotal.Text = "Temps de pose total (s)";
            this.labelTempsTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBoxInfosComment
            // 
            this.pictureBoxInfosComment.Image = global::AstroSessionOrganizer.Properties.Resources.ico16783;
            this.pictureBoxInfosComment.Location = new System.Drawing.Point(410, 301);
            this.pictureBoxInfosComment.Name = "pictureBoxInfosComment";
            this.pictureBoxInfosComment.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxInfosComment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxInfosComment.TabIndex = 76;
            this.pictureBoxInfosComment.TabStop = false;
            // 
            // pictureBoxObservation
            // 
            this.pictureBoxObservation.Image = global::AstroSessionOrganizer.Properties.Resources.icons8_appareil_photo_3d_fluency_96;
            this.pictureBoxObservation.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxObservation.Name = "pictureBoxObservation";
            this.pictureBoxObservation.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxObservation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxObservation.TabIndex = 16;
            this.pictureBoxObservation.TabStop = false;
            // 
            // dlgObservation
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(446, 393);
            this.Controls.Add(this.labelTempsTotal);
            this.Controls.Add(this.textBoxTempsTotal);
            this.Controls.Add(this.labelLune);
            this.Controls.Add(this.textBoxLune);
            this.Controls.Add(this.labelSeeing);
            this.Controls.Add(this.textBoxSeeing);
            this.Controls.Add(this.labelBinning);
            this.Controls.Add(this.textBoxBinning);
            this.Controls.Add(this.labelTemperature);
            this.Controls.Add(this.textBoxTemperature);
            this.Controls.Add(this.labelGain);
            this.Controls.Add(this.textBoxGain);
            this.Controls.Add(this.labelTempsUnitaire);
            this.Controls.Add(this.textBoxTempsUnitaire);
            this.Controls.Add(this.labelNbExpositions);
            this.Controls.Add(this.textBoxNbrExpositions);
            this.Controls.Add(this.pictureBoxInfosComment);
            this.Controls.Add(this.checkBoxComment);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.labelFiltre);
            this.Controls.Add(this.comboBoxFiltre);
            this.Controls.Add(this.labelTypeObservation);
            this.Controls.Add(this.comboBoxTypeObservation);
            this.Controls.Add(this.dateTimePickerHeureObservation);
            this.Controls.Add(this.dateTimePickerDateObservation);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.pictureBoxObservation);
            this.Controls.Add(this.labelDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgObservation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Observation";
            this.Load += new System.EventHandler(this.dlgObservation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxObservation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxObservation;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateObservation;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerHeureObservation;
        private System.Windows.Forms.ComboBox comboBoxTypeObservation;
        private System.Windows.Forms.Label labelTypeObservation;
        private System.Windows.Forms.Label labelFiltre;
        private System.Windows.Forms.ComboBox comboBoxFiltre;
        private System.Windows.Forms.PictureBox pictureBoxInfosComment;
        private System.Windows.Forms.CheckBox checkBoxComment;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.TextBox textBoxNbrExpositions;
        private System.Windows.Forms.Label labelNbExpositions;
        private System.Windows.Forms.Label labelTempsUnitaire;
        private System.Windows.Forms.TextBox textBoxTempsUnitaire;
        private System.Windows.Forms.Label labelGain;
        private System.Windows.Forms.TextBox textBoxGain;
        private System.Windows.Forms.Label labelTemperature;
        private System.Windows.Forms.TextBox textBoxTemperature;
        private System.Windows.Forms.Label labelBinning;
        private System.Windows.Forms.TextBox textBoxBinning;
        private System.Windows.Forms.Label labelSeeing;
        private System.Windows.Forms.TextBox textBoxSeeing;
        private System.Windows.Forms.Label labelLune;
        private System.Windows.Forms.TextBox textBoxLune;
        private System.Windows.Forms.ToolTip toolTipComment;
        private System.Windows.Forms.TextBox textBoxTempsTotal;
        private System.Windows.Forms.Label labelTempsTotal;
    }
}