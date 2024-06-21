namespace ReceiptClient
{
    partial class FrmShowProject
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.textFilterStr = new System.Windows.Forms.TextBox();
            this.toggleButton = new System.Windows.Forms.Button();
            this.btnOutputExcel = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeletePattern = new System.Windows.Forms.Button();
            this.btnEditPattern = new System.Windows.Forms.Button();
            this.cmbPattern = new System.Windows.Forms.ComboBox();
            this.btnCopyPattern = new System.Windows.Forms.Button();
            this.btnNewPattern = new System.Windows.Forms.Button();
            this.BtnSetting = new System.Windows.Forms.Button();
            this.lblProjectAllCount = new System.Windows.Forms.Label();
            this.c1ComboBox1 = new C1.Win.C1Input.C1ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.c1PictureBox1 = new C1.Win.C1Input.C1PictureBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textFilterStr
            // 
            this.textFilterStr.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textFilterStr.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F);
            this.textFilterStr.Location = new System.Drawing.Point(179, 55);
            this.textFilterStr.Multiline = true;
            this.textFilterStr.Name = "textFilterStr";
            this.textFilterStr.ReadOnly = true;
            this.textFilterStr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textFilterStr.Size = new System.Drawing.Size(385, 43);
            this.textFilterStr.TabIndex = 32;
            // 
            // toggleButton
            // 
            this.toggleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.toggleButton.Enabled = false;
            this.toggleButton.Location = new System.Drawing.Point(27, 817);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(103, 23);
            this.toggleButton.TabIndex = 30;
            this.toggleButton.Text = "全件表示";
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.toggleButton_Click);
            // 
            // btnOutputExcel
            // 
            this.btnOutputExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputExcel.Enabled = false;
            this.btnOutputExcel.Location = new System.Drawing.Point(735, 63);
            this.btnOutputExcel.Name = "btnOutputExcel";
            this.btnOutputExcel.Size = new System.Drawing.Size(103, 23);
            this.btnOutputExcel.TabIndex = 28;
            this.btnOutputExcel.Text = "Excel出力";
            this.btnOutputExcel.UseVisualStyleBackColor = true;
            this.btnOutputExcel.Click += new System.EventHandler(this.btnOutputExcel_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Location = new System.Drawing.Point(27, 26);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(103, 23);
            this.BtnEdit.TabIndex = 26;
            this.BtnEdit.Text = "編集";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnUpdate.Enabled = false;
            this.BtnUpdate.Location = new System.Drawing.Point(735, 28);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(103, 23);
            this.BtnUpdate.TabIndex = 18;
            this.BtnUpdate.Text = "更新";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(179, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 23);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Location = new System.Drawing.Point(27, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 707);
            this.panel1.TabIndex = 23;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnDeletePattern);
            this.groupBox2.Controls.Add(this.btnEditPattern);
            this.groupBox2.Controls.Add(this.cmbPattern);
            this.groupBox2.Controls.Add(this.btnCopyPattern);
            this.groupBox2.Controls.Add(this.btnNewPattern);
            this.groupBox2.Controls.Add(this.BtnSetting);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(857, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(399, 84);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表示パターン";
            // 
            // btnDeletePattern
            // 
            this.btnDeletePattern.Enabled = false;
            this.btnDeletePattern.Location = new System.Drawing.Point(322, 50);
            this.btnDeletePattern.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeletePattern.Name = "btnDeletePattern";
            this.btnDeletePattern.Size = new System.Drawing.Size(65, 23);
            this.btnDeletePattern.TabIndex = 15;
            this.btnDeletePattern.Text = "削除";
            this.btnDeletePattern.UseVisualStyleBackColor = true;
            this.btnDeletePattern.Click += new System.EventHandler(this.btnDeletePattern_Click);
            // 
            // btnEditPattern
            // 
            this.btnEditPattern.Enabled = false;
            this.btnEditPattern.Location = new System.Drawing.Point(184, 49);
            this.btnEditPattern.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditPattern.Name = "btnEditPattern";
            this.btnEditPattern.Size = new System.Drawing.Size(65, 23);
            this.btnEditPattern.TabIndex = 14;
            this.btnEditPattern.Text = "編集";
            this.btnEditPattern.UseVisualStyleBackColor = true;
            this.btnEditPattern.Click += new System.EventHandler(this.btnEditPattern_Click);
            // 
            // cmbPattern
            // 
            this.cmbPattern.FormattingEnabled = true;
            this.cmbPattern.Location = new System.Drawing.Point(13, 21);
            this.cmbPattern.Name = "cmbPattern";
            this.cmbPattern.Size = new System.Drawing.Size(185, 20);
            this.cmbPattern.TabIndex = 13;
            // 
            // btnCopyPattern
            // 
            this.btnCopyPattern.Enabled = false;
            this.btnCopyPattern.Location = new System.Drawing.Point(253, 49);
            this.btnCopyPattern.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopyPattern.Name = "btnCopyPattern";
            this.btnCopyPattern.Size = new System.Drawing.Size(65, 23);
            this.btnCopyPattern.TabIndex = 12;
            this.btnCopyPattern.Text = "複製";
            this.btnCopyPattern.UseVisualStyleBackColor = true;
            this.btnCopyPattern.Click += new System.EventHandler(this.btnCopyPattern_Click);
            // 
            // btnNewPattern
            // 
            this.btnNewPattern.Enabled = false;
            this.btnNewPattern.Location = new System.Drawing.Point(115, 49);
            this.btnNewPattern.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewPattern.Name = "btnNewPattern";
            this.btnNewPattern.Size = new System.Drawing.Size(65, 23);
            this.btnNewPattern.TabIndex = 11;
            this.btnNewPattern.Text = "新規";
            this.btnNewPattern.UseVisualStyleBackColor = true;
            this.btnNewPattern.Click += new System.EventHandler(this.btnNewPattern_Click);
            // 
            // BtnSetting
            // 
            this.BtnSetting.Enabled = false;
            this.BtnSetting.Location = new System.Drawing.Point(13, 50);
            this.BtnSetting.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSetting.Name = "BtnSetting";
            this.BtnSetting.Size = new System.Drawing.Size(65, 23);
            this.BtnSetting.TabIndex = 10;
            this.BtnSetting.Text = "設定";
            this.BtnSetting.UseVisualStyleBackColor = true;
            this.BtnSetting.Click += new System.EventHandler(this.BtnSetting_Click);
            // 
            // lblProjectAllCount
            // 
            this.lblProjectAllCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblProjectAllCount.AutoSize = true;
            this.lblProjectAllCount.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProjectAllCount.Location = new System.Drawing.Point(125, -32);
            this.lblProjectAllCount.Name = "lblProjectAllCount";
            this.lblProjectAllCount.Size = new System.Drawing.Size(66, 13);
            this.lblProjectAllCount.TabIndex = 31;
            this.lblProjectAllCount.Text = "検索結果：";
            // 
            // c1ComboBox1
            // 
            this.c1ComboBox1.AllowSpinLoop = false;
            this.c1ComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c1ComboBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.c1ComboBox1.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.c1ComboBox1.GapHeight = 0;
            this.c1ComboBox1.ImagePadding = new System.Windows.Forms.Padding(0);
            this.c1ComboBox1.Items.Add("25");
            this.c1ComboBox1.Items.Add("50");
            this.c1ComboBox1.Items.Add("75");
            this.c1ComboBox1.Items.Add("100");
            this.c1ComboBox1.Items.Add("125");
            this.c1ComboBox1.Items.Add("150");
            this.c1ComboBox1.Items.Add("175");
            this.c1ComboBox1.Items.Add("200");
            this.c1ComboBox1.Items.Add("500");
            this.c1ComboBox1.Items.Add("1000");
            this.c1ComboBox1.Location = new System.Drawing.Point(603, 65);
            this.c1ComboBox1.Name = "c1ComboBox1";
            this.c1ComboBox1.Size = new System.Drawing.Size(103, 19);
            this.c1ComboBox1.TabIndex = 36;
            this.c1ComboBox1.Tag = null;
            this.c1ComboBox1.SelectedItemChanged += new System.EventHandler(this.c1ComboBox1_SelectedItemChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(600, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "表示件数設定";
            // 
            // c1PictureBox1
            // 
            this.c1PictureBox1.Location = new System.Drawing.Point(746, 104);
            this.c1PictureBox1.Name = "c1PictureBox1";
            this.c1PictureBox1.Size = new System.Drawing.Size(510, 707);
            this.c1PictureBox1.TabIndex = 38;
            this.c1PictureBox1.TabStop = false;
            // 
            // FrmShowProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1278, 847);
            this.Controls.Add(this.c1PictureBox1);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.c1ComboBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.lblProjectAllCount);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.btnOutputExcel);
            this.Controls.Add(this.textFilterStr);
            this.Controls.Add(this.btnSearch);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmShowProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物件一覧";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmShowProject_FormClosing);
            this.Load += new System.EventHandler(this.FrmShowProject_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button btnOutputExcel;
        private System.Windows.Forms.Button toggleButton;
        private System.Windows.Forms.TextBox textFilterStr;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeletePattern;
        private System.Windows.Forms.Button btnEditPattern;
        private System.Windows.Forms.ComboBox cmbPattern;
        private System.Windows.Forms.Button btnCopyPattern;
        private System.Windows.Forms.Button btnNewPattern;
        private System.Windows.Forms.Button BtnSetting;
        private System.Windows.Forms.Label lblProjectAllCount;
        private C1.Win.C1Input.C1ComboBox c1ComboBox1;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1PictureBox c1PictureBox1;
    }
}

