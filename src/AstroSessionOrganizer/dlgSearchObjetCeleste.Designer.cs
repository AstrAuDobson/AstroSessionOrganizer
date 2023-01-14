namespace AstroSessionOrganizer
{
    partial class dlgSearchObjetCeleste
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSearchObjetCeleste));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelRecherche = new System.Windows.Forms.Label();
            this.textBoxRechercher = new System.Windows.Forms.TextBox();
            this.listViewResultat = new System.Windows.Forms.ListView();
            this.labelResultat = new System.Windows.Forms.Label();
            this.pictureBoxInfosWarning = new System.Windows.Forms.PictureBox();
            this.pictureBoxRecherche = new System.Windows.Forms.PictureBox();
            this.toolTipWarning = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecherche)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(338, 342);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(93, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Sélectionner";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(437, 342);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelRecherche
            // 
            this.labelRecherche.Location = new System.Drawing.Point(50, 22);
            this.labelRecherche.Name = "labelRecherche";
            this.labelRecherche.Size = new System.Drawing.Size(95, 23);
            this.labelRecherche.TabIndex = 80;
            this.labelRecherche.Text = "Rechercher";
            this.labelRecherche.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxRechercher
            // 
            this.textBoxRechercher.Location = new System.Drawing.Point(151, 24);
            this.textBoxRechercher.Name = "textBoxRechercher";
            this.textBoxRechercher.Size = new System.Drawing.Size(361, 20);
            this.textBoxRechercher.TabIndex = 0;
            this.textBoxRechercher.TextChanged += new System.EventHandler(this.textBoxRechercher_TextChanged);
            // 
            // listViewResultat
            // 
            this.listViewResultat.FullRowSelect = true;
            this.listViewResultat.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewResultat.HideSelection = false;
            this.listViewResultat.Location = new System.Drawing.Point(13, 82);
            this.listViewResultat.MultiSelect = false;
            this.listViewResultat.Name = "listViewResultat";
            this.listViewResultat.Size = new System.Drawing.Size(499, 254);
            this.listViewResultat.TabIndex = 1;
            this.listViewResultat.UseCompatibleStateImageBehavior = false;
            this.listViewResultat.SelectedIndexChanged += new System.EventHandler(this.listViewResultat_SelectedIndexChanged);
            this.listViewResultat.DoubleClick += new System.EventHandler(this.listViewResultat_DoubleClick);
            // 
            // labelResultat
            // 
            this.labelResultat.AutoSize = true;
            this.labelResultat.Location = new System.Drawing.Point(13, 63);
            this.labelResultat.Name = "labelResultat";
            this.labelResultat.Size = new System.Drawing.Size(123, 13);
            this.labelResultat.TabIndex = 82;
            this.labelResultat.Text = "Résultat de la recherche";
            // 
            // pictureBoxInfosWarning
            // 
            this.pictureBoxInfosWarning.Image = global::AstroSessionOrganizer.Properties.Resources.ico16721;
            this.pictureBoxInfosWarning.Location = new System.Drawing.Point(151, 56);
            this.pictureBoxInfosWarning.Name = "pictureBoxInfosWarning";
            this.pictureBoxInfosWarning.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxInfosWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxInfosWarning.TabIndex = 83;
            this.pictureBoxInfosWarning.TabStop = false;
            // 
            // pictureBoxRecherche
            // 
            this.pictureBoxRecherche.Image = global::AstroSessionOrganizer.Properties.Resources.ico23;
            this.pictureBoxRecherche.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxRecherche.Name = "pictureBoxRecherche";
            this.pictureBoxRecherche.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxRecherche.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRecherche.TabIndex = 19;
            this.pictureBoxRecherche.TabStop = false;
            // 
            // toolTipWarning
            // 
            this.toolTipWarning.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            // 
            // dlgSearchObjetCeleste
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(524, 377);
            this.Controls.Add(this.pictureBoxInfosWarning);
            this.Controls.Add(this.labelResultat);
            this.Controls.Add(this.listViewResultat);
            this.Controls.Add(this.labelRecherche);
            this.Controls.Add(this.textBoxRechercher);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.pictureBoxRecherche);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSearchObjetCeleste";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rechercher un objet céleste";
            this.Load += new System.EventHandler(this.dlgSearchObjetCeleste_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfosWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecherche)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureBoxRecherche;
        private System.Windows.Forms.Label labelRecherche;
        private System.Windows.Forms.TextBox textBoxRechercher;
        private System.Windows.Forms.ListView listViewResultat;
        private System.Windows.Forms.Label labelResultat;
        private System.Windows.Forms.PictureBox pictureBoxInfosWarning;
        private System.Windows.Forms.ToolTip toolTipWarning;
    }
}