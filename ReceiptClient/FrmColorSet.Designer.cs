namespace ReceiptClient
{
    partial class FrmColorSet
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
            this.BtnDefined = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.RestoreDefault = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.c1FlexGrid1.Location = new System.Drawing.Point(12, 13);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Size = new System.Drawing.Size(415, 184);
            this.c1FlexGrid1.TabIndex = 0;
            // 
            // BtnDefined
            // 
            this.BtnDefined.Location = new System.Drawing.Point(241, 215);
            this.BtnDefined.Name = "BtnDefined";
            this.BtnDefined.Size = new System.Drawing.Size(81, 32);
            this.BtnDefined.TabIndex = 1;
            this.BtnDefined.Text = "確定";
            this.BtnDefined.UseVisualStyleBackColor = true;
            this.BtnDefined.Click += new System.EventHandler(this.BtnDefined_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(346, 215);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(81, 32);
            this.BtnClose.TabIndex = 2;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // RestoreDefault
            // 
            this.RestoreDefault.Location = new System.Drawing.Point(12, 215);
            this.RestoreDefault.Name = "RestoreDefault";
            this.RestoreDefault.Size = new System.Drawing.Size(118, 32);
            this.RestoreDefault.TabIndex = 3;
            this.RestoreDefault.Text = "初期設定に戻す";
            this.RestoreDefault.UseVisualStyleBackColor = true;
            this.RestoreDefault.Click += new System.EventHandler(this.RestoreDefault_Click);
            // 
            // FrmColorSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 271);
            this.Controls.Add(this.RestoreDefault);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnDefined);
            this.Controls.Add(this.c1FlexGrid1);
            this.Name = "FrmColorSet";
            this.Text = "FrmColorSet";
            this.Load += new System.EventHandler(this.FrmColorSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.Button BtnDefined;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button RestoreDefault;
    }
}