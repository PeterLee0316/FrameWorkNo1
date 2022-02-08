namespace Core.UI
{
    partial class FormMotorData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMotorData));
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnImageDataSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnExit.Image = ((System.Drawing.Image)(resources.GetObject("BtnExit.Image")));
            this.BtnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnExit.Location = new System.Drawing.Point(1559, 1116);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(177, 92);
            this.BtnExit.TabIndex = 749;
            this.BtnExit.Text = " Exit";
            this.BtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnImageDataSave
            // 
            this.BtnImageDataSave.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnImageDataSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnImageDataSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnImageDataSave.Image")));
            this.BtnImageDataSave.Location = new System.Drawing.Point(1357, 1116);
            this.BtnImageDataSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnImageDataSave.Name = "BtnImageDataSave";
            this.BtnImageDataSave.Size = new System.Drawing.Size(177, 92);
            this.BtnImageDataSave.TabIndex = 751;
            this.BtnImageDataSave.Text = " Save";
            this.BtnImageDataSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnImageDataSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnImageDataSave.UseVisualStyleBackColor = true;
            this.BtnImageDataSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // FormMotorData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 1222);
            this.Controls.Add(this.BtnImageDataSave);
            this.Controls.Add(this.BtnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormMotorData";
            this.Text = "Motor Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMotorData_FormClosing);
            this.Load += new System.EventHandler(this.FormMotorData_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnImageDataSave;
    }
}