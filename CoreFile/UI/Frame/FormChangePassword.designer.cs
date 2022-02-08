namespace Core.UI
{
    partial class FormChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangePassword));
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnChange = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentPW = new System.Windows.Forms.Label();
            this.NewPW1 = new System.Windows.Forms.Label();
            this.NewPW2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnExit.Image = ((System.Drawing.Image)(resources.GetObject("BtnExit.Image")));
            this.BtnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnExit.Location = new System.Drawing.Point(654, 256);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(201, 124);
            this.BtnExit.TabIndex = 757;
            this.BtnExit.Text = " Exit";
            this.BtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnChange
            // 
            this.BtnChange.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnChange.Image = ((System.Drawing.Image)(resources.GetObject("BtnChange.Image")));
            this.BtnChange.Location = new System.Drawing.Point(448, 256);
            this.BtnChange.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnChange.Name = "BtnChange";
            this.BtnChange.Size = new System.Drawing.Size(201, 124);
            this.BtnChange.TabIndex = 756;
            this.BtnChange.Text = "  Save";
            this.BtnChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnChange.UseVisualStyleBackColor = true;
            this.BtnChange.Click += new System.EventHandler(this.BtnChange_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(349, 67);
            this.label1.TabIndex = 764;
            this.label1.Text = "1. Old PassWord";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(349, 67);
            this.label2.TabIndex = 765;
            this.label2.Text = "2. New PassWord";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(349, 67);
            this.label3.TabIndex = 766;
            this.label3.Text = "3. New PassWord";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrentPW
            // 
            this.CurrentPW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.CurrentPW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CurrentPW.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CurrentPW.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentPW.Location = new System.Drawing.Point(395, 16);
            this.CurrentPW.Name = "CurrentPW";
            this.CurrentPW.Size = new System.Drawing.Size(460, 67);
            this.CurrentPW.TabIndex = 767;
            this.CurrentPW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CurrentPW.Click += new System.EventHandler(this.CurrentPW_Click);
            // 
            // NewPW1
            // 
            this.NewPW1.BackColor = System.Drawing.Color.LemonChiffon;
            this.NewPW1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.NewPW1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NewPW1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewPW1.Location = new System.Drawing.Point(395, 96);
            this.NewPW1.Name = "NewPW1";
            this.NewPW1.Size = new System.Drawing.Size(460, 67);
            this.NewPW1.TabIndex = 768;
            this.NewPW1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NewPW1.Click += new System.EventHandler(this.NewPW1_Click);
            // 
            // NewPW2
            // 
            this.NewPW2.BackColor = System.Drawing.Color.LemonChiffon;
            this.NewPW2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.NewPW2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NewPW2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewPW2.Location = new System.Drawing.Point(395, 170);
            this.NewPW2.Name = "NewPW2";
            this.NewPW2.Size = new System.Drawing.Size(460, 67);
            this.NewPW2.TabIndex = 769;
            this.NewPW2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NewPW2.Click += new System.EventHandler(this.NewPW2_Click);
            // 
            // FormChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 401);
            this.Controls.Add(this.NewPW2);
            this.Controls.Add(this.NewPW1);
            this.Controls.Add(this.CurrentPW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnChange);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FormChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInputPassword_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnChange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label NewPW2;
        private System.Windows.Forms.Label NewPW1;
        private System.Windows.Forms.Label CurrentPW;
    }
}