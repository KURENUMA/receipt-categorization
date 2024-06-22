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
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.c1FlexGrid1.Location = new System.Drawing.Point(570, 63);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Size = new System.Drawing.Size(683, 707);
            this.c1FlexGrid1.TabIndex = 40;
            // 
            // btnAI
            // 
            this.btnAI.Location = new System.Drawing.Point(372, 34);
            this.btnAI.Name = "btnAI";
            this.btnAI.Size = new System.Drawing.Size(173, 23);
            this.btnAI.TabIndex = 41;
            this.btnAI.Text = "AIで解析";
            this.btnAI.UseVisualStyleBackColor = true;
            this.btnAI.Click += new System.EventHandler(this.btnAI_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(1080, 34);
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
            // ReceiptDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1278, 847);
            this.Controls.Add(this.c1PictureBox1);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnAI);
            this.Controls.Add(this.c1FlexGrid1);
            this.Name = "ReceiptDetail";
            this.Text = "領収書詳細";
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.Button btnAI;
        private System.Windows.Forms.Button btnPaste;
        private C1.Win.C1Input.C1PictureBox c1PictureBox1;
    }
}