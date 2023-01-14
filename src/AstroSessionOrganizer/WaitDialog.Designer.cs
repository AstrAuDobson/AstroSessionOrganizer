namespace AstroSessionOrganizer
{
    partial class WaitDialog
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
            this.labelWorker = new System.Windows.Forms.Label();
            this.progressBarWorker = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // labelWorker
            // 
            this.labelWorker.AutoSize = true;
            this.labelWorker.Location = new System.Drawing.Point(12, 9);
            this.labelWorker.Name = "labelWorker";
            this.labelWorker.Size = new System.Drawing.Size(0, 13);
            this.labelWorker.TabIndex = 0;
            // 
            // progressBarWorker
            // 
            this.progressBarWorker.Location = new System.Drawing.Point(12, 47);
            this.progressBarWorker.MarqueeAnimationSpeed = 25;
            this.progressBarWorker.Name = "progressBarWorker";
            this.progressBarWorker.Size = new System.Drawing.Size(397, 23);
            this.progressBarWorker.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarWorker.TabIndex = 1;
            // 
            // WaitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 93);
            this.Controls.Add(this.progressBarWorker);
            this.Controls.Add(this.labelWorker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WaitDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WaitDialog";
            this.Load += new System.EventHandler(this.WaitDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWorker;
        private System.Windows.Forms.ProgressBar progressBarWorker;
    }
}