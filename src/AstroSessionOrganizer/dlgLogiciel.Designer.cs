namespace AstroSessionOrganizer
{
    partial class dlgLogiciel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgLogiciel));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelNom = new System.Windows.Forms.Label();
            this.textBoxNom = new System.Windows.Forms.TextBox();
            this.labelTypeLogiciel = new System.Windows.Forms.Label();
            this.comboBoxTypeLogiciel = new System.Windows.Forms.ComboBox();
            this.pictureBoxLogiciel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogiciel)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(264, 78);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 14;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(345, 78);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelNom
            // 
            this.labelNom.AutoSize = true;
            this.labelNom.Location = new System.Drawing.Point(102, 49);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(29, 13);
            this.labelNom.TabIndex = 16;
            this.labelNom.Text = "Nom";
            // 
            // textBoxNom
            // 
            this.textBoxNom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNom.Location = new System.Drawing.Point(137, 46);
            this.textBoxNom.Name = "textBoxNom";
            this.textBoxNom.Size = new System.Drawing.Size(283, 20);
            this.textBoxNom.TabIndex = 12;
            // 
            // labelTypeLogiciel
            // 
            this.labelTypeLogiciel.AutoSize = true;
            this.labelTypeLogiciel.Location = new System.Drawing.Point(50, 21);
            this.labelTypeLogiciel.Name = "labelTypeLogiciel";
            this.labelTypeLogiciel.Size = new System.Drawing.Size(81, 13);
            this.labelTypeLogiciel.TabIndex = 13;
            this.labelTypeLogiciel.Text = "Type de logiciel";
            // 
            // comboBoxTypeLogiciel
            // 
            this.comboBoxTypeLogiciel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTypeLogiciel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeLogiciel.FormattingEnabled = true;
            this.comboBoxTypeLogiciel.Location = new System.Drawing.Point(137, 18);
            this.comboBoxTypeLogiciel.Name = "comboBoxTypeLogiciel";
            this.comboBoxTypeLogiciel.Size = new System.Drawing.Size(283, 21);
            this.comboBoxTypeLogiciel.TabIndex = 11;
            // 
            // pictureBoxLogiciel
            // 
            this.pictureBoxLogiciel.Image = global::AstroSessionOrganizer.Properties.Resources._87470_software_icon;
            this.pictureBoxLogiciel.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxLogiciel.Name = "pictureBoxLogiciel";
            this.pictureBoxLogiciel.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxLogiciel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogiciel.TabIndex = 17;
            this.pictureBoxLogiciel.TabStop = false;
            // 
            // dlgLogiciel
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(432, 113);
            this.Controls.Add(this.pictureBoxLogiciel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelNom);
            this.Controls.Add(this.textBoxNom);
            this.Controls.Add(this.labelTypeLogiciel);
            this.Controls.Add(this.comboBoxTypeLogiciel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgLogiciel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Logiciel";
            this.Load += new System.EventHandler(this.dlgLogiciel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogiciel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogiciel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelNom;
        private System.Windows.Forms.TextBox textBoxNom;
        private System.Windows.Forms.Label labelTypeLogiciel;
        private System.Windows.Forms.ComboBox comboBoxTypeLogiciel;
    }
}