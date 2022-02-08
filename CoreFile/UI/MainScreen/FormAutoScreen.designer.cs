namespace Core.UI
{
    partial class FormAutoScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoScreen));
            this.TimerUI = new System.Windows.Forms.Timer(this.components);
            this.pnlCamView = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // TimerUI
            // 
            this.TimerUI.Tick += new System.EventHandler(this.TimerUI_Tick);
            // 
            // pnlCamView
            // 
            this.pnlCamView.BackColor = System.Drawing.Color.MidnightBlue;
            this.pnlCamView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCamView.BackgroundImage")));
            this.pnlCamView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlCamView.Location = new System.Drawing.Point(11, 11);
            this.pnlCamView.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCamView.Name = "pnlCamView";
            this.pnlCamView.Size = new System.Drawing.Size(1252, 876);
            this.pnlCamView.TabIndex = 5;
            // 
            // FormAutoScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1806, 898);
            this.Controls.Add(this.pnlCamView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAutoScreen";
            this.ShowIcon = false;
            this.Activated += new System.EventHandler(this.FormAutoScreen_Activated);
            this.Load += new System.EventHandler(this.FormAutoScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TimerUI;
        private System.Windows.Forms.Panel pnlCamView;
    }
}