namespace Core.UI
{
    partial class FormLogScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogScreen));
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnSerch = new System.Windows.Forms.Button();
            this.DateEnd = new System.Windows.Forms.DateTimePicker();
            this.DateStart = new System.Windows.Forms.DateTimePicker();
            this.Image = new System.Windows.Forms.ImageList(this.components);
            this.BtnPageTop = new System.Windows.Forms.Button();
            this.BtnPageBot = new System.Windows.Forms.Button();
            this.BtnPageUp = new System.Windows.Forms.Button();
            this.BtnPageDown = new System.Windows.Forms.Button();
            this.ComboType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LabelCount = new System.Windows.Forms.Label();
            this.GridViewCont = new System.Windows.Forms.DataGridView();
            this.pageCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewCont)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnExport
            // 
            this.BtnExport.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnExport.Image = ((System.Drawing.Image)(resources.GetObject("BtnExport.Image")));
            this.BtnExport.Location = new System.Drawing.Point(1564, 111);
            this.BtnExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(130, 57);
            this.BtnExport.TabIndex = 780;
            this.BtnExport.Text = "  Export";
            this.BtnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnClear.Image = ((System.Drawing.Image)(resources.GetObject("BtnClear.Image")));
            this.BtnClear.Location = new System.Drawing.Point(1433, 111);
            this.BtnClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(130, 57);
            this.BtnClear.TabIndex = 777;
            this.BtnClear.Text = "   Clear";
            this.BtnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(1570, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 25);
            this.label1.TabIndex = 776;
            this.label1.Text = "~";
            // 
            // BtnSerch
            // 
            this.BtnSerch.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnSerch.Image = ((System.Drawing.Image)(resources.GetObject("BtnSerch.Image")));
            this.BtnSerch.Location = new System.Drawing.Point(1302, 111);
            this.BtnSerch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSerch.Name = "BtnSerch";
            this.BtnSerch.Size = new System.Drawing.Size(130, 57);
            this.BtnSerch.TabIndex = 775;
            this.BtnSerch.Text = " Search";
            this.BtnSerch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnSerch.UseVisualStyleBackColor = true;
            this.BtnSerch.Click += new System.EventHandler(this.BtnSerch_Click);
            // 
            // DateEnd
            // 
            this.DateEnd.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DateEnd.Location = new System.Drawing.Point(1604, 53);
            this.DateEnd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DateEnd.Name = "DateEnd";
            this.DateEnd.Size = new System.Drawing.Size(250, 29);
            this.DateEnd.TabIndex = 774;
            this.DateEnd.ValueChanged += new System.EventHandler(this.DateEnd_ValueChanged);
            // 
            // DateStart
            // 
            this.DateStart.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DateStart.Location = new System.Drawing.Point(1301, 53);
            this.DateStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DateStart.Name = "DateStart";
            this.DateStart.Size = new System.Drawing.Size(262, 29);
            this.DateStart.TabIndex = 773;
            this.DateStart.ValueChanged += new System.EventHandler(this.DateStart_ValueChanged);
            // 
            // Image
            // 
            this.Image.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Image.ImageStream")));
            this.Image.TransparentColor = System.Drawing.Color.Transparent;
            this.Image.Images.SetKeyName(0, "Led_Off.png");
            this.Image.Images.SetKeyName(1, "Led_On.png");
            // 
            // BtnPageTop
            // 
            this.BtnPageTop.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnPageTop.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPageTop.Image = ((System.Drawing.Image)(resources.GetObject("BtnPageTop.Image")));
            this.BtnPageTop.Location = new System.Drawing.Point(1190, 7);
            this.BtnPageTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnPageTop.Name = "BtnPageTop";
            this.BtnPageTop.Size = new System.Drawing.Size(83, 105);
            this.BtnPageTop.TabIndex = 785;
            this.BtnPageTop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnPageTop.UseVisualStyleBackColor = false;
            this.BtnPageTop.Click += new System.EventHandler(this.BtnPageTop_Click);
            // 
            // BtnPageBot
            // 
            this.BtnPageBot.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnPageBot.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPageBot.Image = ((System.Drawing.Image)(resources.GetObject("BtnPageBot.Image")));
            this.BtnPageBot.Location = new System.Drawing.Point(1190, 790);
            this.BtnPageBot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnPageBot.Name = "BtnPageBot";
            this.BtnPageBot.Size = new System.Drawing.Size(83, 105);
            this.BtnPageBot.TabIndex = 786;
            this.BtnPageBot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnPageBot.UseVisualStyleBackColor = false;
            this.BtnPageBot.Click += new System.EventHandler(this.BtnPageBot_Click);
            // 
            // BtnPageUp
            // 
            this.BtnPageUp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnPageUp.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPageUp.Image = ((System.Drawing.Image)(resources.GetObject("BtnPageUp.Image")));
            this.BtnPageUp.Location = new System.Drawing.Point(1191, 117);
            this.BtnPageUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnPageUp.Name = "BtnPageUp";
            this.BtnPageUp.Size = new System.Drawing.Size(83, 333);
            this.BtnPageUp.TabIndex = 787;
            this.BtnPageUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnPageUp.UseVisualStyleBackColor = false;
            this.BtnPageUp.Click += new System.EventHandler(this.BtnPageUp_Click);
            // 
            // BtnPageDown
            // 
            this.BtnPageDown.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnPageDown.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPageDown.Image = ((System.Drawing.Image)(resources.GetObject("BtnPageDown.Image")));
            this.BtnPageDown.Location = new System.Drawing.Point(1191, 451);
            this.BtnPageDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnPageDown.Name = "BtnPageDown";
            this.BtnPageDown.Size = new System.Drawing.Size(83, 333);
            this.BtnPageDown.TabIndex = 788;
            this.BtnPageDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnPageDown.UseVisualStyleBackColor = false;
            this.BtnPageDown.Click += new System.EventHandler(this.BtnPageDown_Click);
            // 
            // ComboType
            // 
            this.ComboType.DropDownHeight = 200;
            this.ComboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboType.DropDownWidth = 260;
            this.ComboType.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ComboType.FormattingEnabled = true;
            this.ComboType.IntegralHeight = false;
            this.ComboType.ItemHeight = 19;
            this.ComboType.Location = new System.Drawing.Point(1301, 8);
            this.ComboType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ComboType.Name = "ComboType";
            this.ComboType.Size = new System.Drawing.Size(221, 27);
            this.ComboType.TabIndex = 843;
            this.ComboType.SelectedIndexChanged += new System.EventHandler(this.ComboType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightGreen;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1539, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 38);
            this.label2.TabIndex = 844;
            this.label2.Text = "Count";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelCount
            // 
            this.LabelCount.BackColor = System.Drawing.Color.White;
            this.LabelCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LabelCount.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LabelCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCount.Location = new System.Drawing.Point(1610, 7);
            this.LabelCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelCount.Name = "LabelCount";
            this.LabelCount.Size = new System.Drawing.Size(114, 38);
            this.LabelCount.TabIndex = 845;
            this.LabelCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GridViewCont
            // 
            this.GridViewCont.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.GridViewCont.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridViewCont.Location = new System.Drawing.Point(12, 8);
            this.GridViewCont.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GridViewCont.Name = "GridViewCont";
            this.GridViewCont.ReadOnly = true;
            this.GridViewCont.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridViewCont.RowTemplate.Height = 27;
            this.GridViewCont.Size = new System.Drawing.Size(1171, 887);
            this.GridViewCont.TabIndex = 847;
            // 
            // pageCount
            // 
            this.pageCount.BackColor = System.Drawing.Color.White;
            this.pageCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pageCount.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.pageCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageCount.Location = new System.Drawing.Point(1812, 7);
            this.pageCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pageCount.Name = "pageCount";
            this.pageCount.Size = new System.Drawing.Size(95, 38);
            this.pageCount.TabIndex = 849;
            this.pageCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightGreen;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1741, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 38);
            this.label4.TabIndex = 848;
            this.label4.Text = "Page";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormLogScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1918, 1002);
            this.Controls.Add(this.pageCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GridViewCont);
            this.Controls.Add(this.LabelCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboType);
            this.Controls.Add(this.BtnPageDown);
            this.Controls.Add(this.BtnPageUp);
            this.Controls.Add(this.BtnPageBot);
            this.Controls.Add(this.BtnPageTop);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnSerch);
            this.Controls.Add(this.DateEnd);
            this.Controls.Add(this.DateStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormLogScreen";
            this.Activated += new System.EventHandler(this.FormLogScreen_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewCont)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnSerch;
        private System.Windows.Forms.DateTimePicker DateEnd;
        private System.Windows.Forms.DateTimePicker DateStart;
        private System.Windows.Forms.ImageList Image;
        private System.Windows.Forms.Button BtnPageTop;
        private System.Windows.Forms.Button BtnPageBot;
        private System.Windows.Forms.Button BtnPageUp;
        private System.Windows.Forms.Button BtnPageDown;
        private System.Windows.Forms.ComboBox ComboType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LabelCount;
        private System.Windows.Forms.DataGridView GridViewCont;
        private System.Windows.Forms.Label pageCount;
        private System.Windows.Forms.Label label4;
    }
}