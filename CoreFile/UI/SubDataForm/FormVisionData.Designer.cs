namespace Core.UI
{
    partial class FormVisionData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVisionData));
            this.BtnExit = new System.Windows.Forms.Button();
            this.btnCameraDataLoad = new System.Windows.Forms.Button();
            this.btnCameraDataSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnExit.Image = ((System.Drawing.Image)(resources.GetObject("BtnExit.Image")));
            this.BtnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnExit.Location = new System.Drawing.Point(1605, 1216);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(177, 92);
            this.BtnExit.TabIndex = 749;
            this.BtnExit.Text = " Exit";
            this.BtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnCameraDataLoad
            // 
            this.btnCameraDataLoad.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCameraDataLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCameraDataLoad.Location = new System.Drawing.Point(1000, 30);
            this.btnCameraDataLoad.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnCameraDataLoad.Name = "btnCameraDataLoad";
            this.btnCameraDataLoad.Size = new System.Drawing.Size(185, 92);
            this.btnCameraDataLoad.TabIndex = 963;
            this.btnCameraDataLoad.Text = "Data Reload";
            this.btnCameraDataLoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCameraDataLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCameraDataLoad.UseVisualStyleBackColor = true;
            this.btnCameraDataLoad.Click += new System.EventHandler(this.btnCameraDataLoad_Click);
            // 
            // btnCameraDataSave
            // 
            this.btnCameraDataSave.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCameraDataSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCameraDataSave.Image = ((System.Drawing.Image)(resources.GetObject("btnCameraDataSave.Image")));
            this.btnCameraDataSave.Location = new System.Drawing.Point(1000, 130);
            this.btnCameraDataSave.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnCameraDataSave.Name = "btnCameraDataSave";
            this.btnCameraDataSave.Size = new System.Drawing.Size(185, 92);
            this.btnCameraDataSave.TabIndex = 962;
            this.btnCameraDataSave.Text = "Data Save";
            this.btnCameraDataSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCameraDataSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCameraDataSave.UseVisualStyleBackColor = true;
            this.btnCameraDataSave.Click += new System.EventHandler(this.btnCameraDataSave_Click);
            // 
            // FormVisionData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 1326);
            this.Controls.Add(this.btnCameraDataLoad);
            this.Controls.Add(this.btnCameraDataSave);
            this.Controls.Add(this.BtnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FormVisionData";
            this.Text = "Vision Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVisionData_FormClosing);
            this.Load += new System.EventHandler(this.FormVisionData_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button btnCameraDataLoad;
        private System.Windows.Forms.Button btnCameraDataSave;
    }
}