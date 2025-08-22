namespace ATM_WinForm.Forme.Bankomat
{
    partial class Form_Bankomat_Komentari_AddUpdate
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
            this.KomentarLbl = new System.Windows.Forms.Label();
            this.AkcijaKomentaraBtn = new System.Windows.Forms.Button();
            this.AddUpdateTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // KomentarLbl
            // 
            this.KomentarLbl.AutoSize = true;
            this.KomentarLbl.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.KomentarLbl.Location = new System.Drawing.Point(88, 51);
            this.KomentarLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.KomentarLbl.Name = "KomentarLbl";
            this.KomentarLbl.Size = new System.Drawing.Size(71, 15);
            this.KomentarLbl.TabIndex = 9;
            this.KomentarLbl.Text = "Komentar";
            // 
            // AkcijaKomentaraBtn
            // 
            this.AkcijaKomentaraBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.AkcijaKomentaraBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.AkcijaKomentaraBtn.Location = new System.Drawing.Point(91, 90);
            this.AkcijaKomentaraBtn.Margin = new System.Windows.Forms.Padding(2);
            this.AkcijaKomentaraBtn.Name = "AkcijaKomentaraBtn";
            this.AkcijaKomentaraBtn.Size = new System.Drawing.Size(158, 31);
            this.AkcijaKomentaraBtn.TabIndex = 8;
            this.AkcijaKomentaraBtn.Text = "button1";
            this.AkcijaKomentaraBtn.UseVisualStyleBackColor = false;
            this.AkcijaKomentaraBtn.Click += new System.EventHandler(this.AkcijaKomentaraBtn_Click);
            // 
            // AddUpdateTxt
            // 
            this.AddUpdateTxt.Location = new System.Drawing.Point(91, 66);
            this.AddUpdateTxt.Margin = new System.Windows.Forms.Padding(2);
            this.AddUpdateTxt.Name = "AddUpdateTxt";
            this.AddUpdateTxt.Size = new System.Drawing.Size(158, 20);
            this.AddUpdateTxt.TabIndex = 7;
            // 
            // Form_Bankomat_Komentari_AddUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(332, 185);
            this.Controls.Add(this.KomentarLbl);
            this.Controls.Add(this.AkcijaKomentaraBtn);
            this.Controls.Add(this.AddUpdateTxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_Bankomat_Komentari_AddUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Dodaj Komentar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label KomentarLbl;
        private System.Windows.Forms.Button AkcijaKomentaraBtn;
        private System.Windows.Forms.TextBox AddUpdateTxt;
    }
}