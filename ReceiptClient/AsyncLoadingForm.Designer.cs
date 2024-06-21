namespace ReceiptClient
{
    partial class AsyncLoadingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.loadingCircle2 = new ReceiptClient.Controls.LoadingCircle();
            this.loadingCircle1 = new ReceiptClient.Controls.LoadingCircle();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(139, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "読み込み中";
            // 
            // loadingCircle2
            // 
            this.loadingCircle2.Active = true;
            this.loadingCircle2.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.loadingCircle2.InnerCircleRadius = 5;
            this.loadingCircle2.Location = new System.Drawing.Point(29, 47);
            this.loadingCircle2.Name = "loadingCircle2";
            this.loadingCircle2.NumberSpoke = 12;
            this.loadingCircle2.OuterCircleRadius = 11;
            this.loadingCircle2.RotationSpeed = 100;
            this.loadingCircle2.Size = new System.Drawing.Size(85, 25);
            this.loadingCircle2.SpokeThickness = 2;
            this.loadingCircle2.StylePreset = ReceiptClient.Controls.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle2.TabIndex = 4;
            this.loadingCircle2.Text = "loadingCircle2";
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = true;
            this.loadingCircle1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(26, 35);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(74, 0);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = ReceiptClient.Controls.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 2;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // AsyncLoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 122);
            this.ControlBox = false;
            this.Controls.Add(this.loadingCircle2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadingCircle1);
            this.MaximumSize = new System.Drawing.Size(292, 161);
            this.MinimumSize = new System.Drawing.Size(292, 161);
            this.Name = "AsyncLoadingForm";
            this.Text = "LoadingForm";
            this.Load += new System.EventHandler(this.AsyncLoadingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Label label1;
        private Controls.LoadingCircle loadingCircle2;
    }
}