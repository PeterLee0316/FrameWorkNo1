namespace Core.UI
{
    partial class FormUserLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUserLogin));
            this.BtnLogin = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.ComboUser = new System.Windows.Forms.ComboBox();
            this.BtnChangePW = new System.Windows.Forms.Button();
            this.BtnLogout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LabelComment = new System.Windows.Forms.Label();
            this.LabelType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnLogin
            // 
            this.BtnLogin.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnLogin.Image = ((System.Drawing.Image)(resources.GetObject("BtnLogin.Image")));
            this.BtnLogin.Location = new System.Drawing.Point(239, 303);
            this.BtnLogin.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(195, 107);
            this.BtnLogin.TabIndex = 754;
            this.BtnLogin.Text = " Login";
            this.BtnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnExit.Image = ((System.Drawing.Image)(resources.GetObject("BtnExit.Image")));
            this.BtnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnExit.Location = new System.Drawing.Point(644, 303);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(195, 107);
            this.BtnExit.TabIndex = 753;
            this.BtnExit.Text = " Cancel";
            this.BtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // ComboUser
            // 
            this.ComboUser.DropDownHeight = 200;
            this.ComboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboUser.DropDownWidth = 260;
            this.ComboUser.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ComboUser.FormattingEnabled = true;
            this.ComboUser.IntegralHeight = false;
            this.ComboUser.Location = new System.Drawing.Point(237, 46);
            this.ComboUser.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ComboUser.Name = "ComboUser";
            this.ComboUser.Size = new System.Drawing.Size(600, 41);
            this.ComboUser.TabIndex = 842;
            this.ComboUser.SelectedIndexChanged += new System.EventHandler(this.ComboUser_SelectedIndexChanged);
            // 
            // BtnChangePW
            // 
            this.BtnChangePW.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnChangePW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnChangePW.Image = ((System.Drawing.Image)(resources.GetObject("BtnChangePW.Image")));
            this.BtnChangePW.Location = new System.Drawing.Point(36, 303);
            this.BtnChangePW.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnChangePW.Name = "BtnChangePW";
            this.BtnChangePW.Size = new System.Drawing.Size(195, 107);
            this.BtnChangePW.TabIndex = 847;
            this.BtnChangePW.Text = "Change Password";
            this.BtnChangePW.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnChangePW.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnChangePW.UseVisualStyleBackColor = true;
            this.BtnChangePW.Click += new System.EventHandler(this.BtnChangePW_Click);
            // 
            // BtnLogout
            // 
            this.BtnLogout.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnLogout.Image = ((System.Drawing.Image)(resources.GetObject("BtnLogout.Image")));
            this.BtnLogout.Location = new System.Drawing.Point(442, 303);
            this.BtnLogout.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnLogout.Name = "BtnLogout";
            this.BtnLogout.Size = new System.Drawing.Size(195, 107);
            this.BtnLogout.TabIndex = 848;
            this.BtnLogout.Text = " Logout";
            this.BtnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLogout.UseVisualStyleBackColor = true;
            this.BtnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 47);
            this.label1.TabIndex = 849;
            this.label1.Text = "Select User";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 47);
            this.label2.TabIndex = 850;
            this.label2.Text = "Comment";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 47);
            this.label3.TabIndex = 851;
            this.label3.Text = "Group";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelComment
            // 
            this.LabelComment.BackColor = System.Drawing.Color.White;
            this.LabelComment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LabelComment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LabelComment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelComment.Location = new System.Drawing.Point(239, 105);
            this.LabelComment.Name = "LabelComment";
            this.LabelComment.Size = new System.Drawing.Size(600, 47);
            this.LabelComment.TabIndex = 852;
            this.LabelComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelType
            // 
            this.LabelType.BackColor = System.Drawing.Color.White;
            this.LabelType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LabelType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LabelType.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelType.Location = new System.Drawing.Point(237, 168);
            this.LabelType.Name = "LabelType";
            this.LabelType.Size = new System.Drawing.Size(600, 47);
            this.LabelType.TabIndex = 853;
            this.LabelType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormUserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 432);
            this.Controls.Add(this.LabelType);
            this.Controls.Add(this.LabelComment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnLogout);
            this.Controls.Add(this.BtnChangePW);
            this.Controls.Add(this.ComboUser);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.BtnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FormUserLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUserLogin_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.ComboBox ComboUser;
        private System.Windows.Forms.Button BtnChangePW;
        private System.Windows.Forms.Button BtnLogout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LabelType;
        private System.Windows.Forms.Label LabelComment;
    }
}