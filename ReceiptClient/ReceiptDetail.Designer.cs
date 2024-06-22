namespace ReceiptClient
{
    partial class ReceiptDetail
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
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnAI = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.c1PictureBox1 = new C1.Win.C1Input.C1PictureBox();
            this.btnImageDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.c1FlexGrid1.Location = new System.Drawing.Point(570, 101);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Size = new System.Drawing.Size(683, 655);
            this.c1FlexGrid1.TabIndex = 40;
            // 
            // btnAI
            // 
            this.btnAI.Location = new System.Drawing.Point(226, 34);
            this.btnAI.Name = "btnAI";
            this.btnAI.Size = new System.Drawing.Size(173, 23);
            this.btnAI.TabIndex = 41;
            this.btnAI.Text = "AIで解析";
            this.btnAI.UseVisualStyleBackColor = true;
            this.btnAI.Click += new System.EventHandler(this.btnAI_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(570, 34);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(173, 23);
            this.btnPaste.TabIndex = 42;
            this.btnPaste.Text = "貼り付ける";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // c1PictureBox1
            // 
            this.c1PictureBox1.Location = new System.Drawing.Point(35, 63);
            this.c1PictureBox1.Name = "c1PictureBox1";
            this.c1PictureBox1.Size = new System.Drawing.Size(510, 707);
            this.c1PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.c1PictureBox1.TabIndex = 43;
            this.c1PictureBox1.TabStop = false;
            // 
            // btnImageDownload
            // 
            this.btnImageDownload.Location = new System.Drawing.Point(35, 34);
            this.btnImageDownload.Name = "btnImageDownload";
            this.btnImageDownload.Size = new System.Drawing.Size(173, 23);
            this.btnImageDownload.TabIndex = 44;
            this.btnImageDownload.Text = "イメージファイルダウンロード";
            this.btnImageDownload.UseVisualStyleBackColor = true;
            this.btnImageDownload.Click += new System.EventHandler(this.btnImageDownload_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(749, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Excelでコピーした内容を貼り付けできます。Ctrl + Vでも出来ます。";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1126, 797);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(127, 23);
            this.btnClose.TabIndex = 46;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(570, 69);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(173, 23);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "編集内容の保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // ReceiptDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1278, 847);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImageDownload);
            this.Controls.Add(this.c1PictureBox1);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnAI);
            this.Controls.Add(this.c1FlexGrid1);
            this.Name = "ReceiptDetail";
            this.Text = "領収書詳細";
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.Button btnAI;
        private System.Windows.Forms.Button btnPaste;
        private C1.Win.C1Input.C1PictureBox c1PictureBox1;
        private System.Windows.Forms.Button btnImageDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}