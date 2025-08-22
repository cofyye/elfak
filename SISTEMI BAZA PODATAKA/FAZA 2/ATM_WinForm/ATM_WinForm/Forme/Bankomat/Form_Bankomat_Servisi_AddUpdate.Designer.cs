namespace ATM_WinForm.Forme.Bankomat
{
    partial class Form_Bankomat_Servisi_AddUpdate
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
            this.FirmaLbl = new System.Windows.Forms.Label();
            this.AkcijaServisaBtn = new System.Windows.Forms.Button();
            this.AddUpdateTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FirmaLbl
            // 
            this.FirmaLbl.AutoSize = true;
            this.FirmaLbl.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.FirmaLbl.Location = new System.Drawing.Point(56, 43);
            this.FirmaLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FirmaLbl.Name = "FirmaLbl";
            this.FirmaLbl.Size = new System.Drawing.Size(294, 15);
            this.FirmaLbl.TabIndex = 12;
            this.FirmaLbl.Text = "Unesite naziv firmu koja servisira bankomat:";
            // 
            // AkcijaServisaBtn
            // 
            this.AkcijaServisaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.AkcijaServisaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.AkcijaServisaBtn.Location = new System.Drawing.Point(115, 110);
            this.AkcijaServisaBtn.Margin = new System.Windows.Forms.Padding(2);
            this.AkcijaServisaBtn.Name = "AkcijaServisaBtn";
            this.AkcijaServisaBtn.Size = new System.Drawing.Size(158, 31);
            this.AkcijaServisaBtn.TabIndex = 11;
            this.AkcijaServisaBtn.Text = "button1";
            this.AkcijaServisaBtn.UseVisualStyleBackColor = false;
            this.AkcijaServisaBtn.Click += new System.EventHandler(this.AkcijaServisaBtn_Click);
            // 
            // AddUpdateTxt
            // 
            this.AddUpdateTxt.Location = new System.Drawing.Point(115, 70);
            this.AddUpdateTxt.Margin = new System.Windows.Forms.Padding(2);
            this.AddUpdateTxt.Name = "AddUpdateTxt";
            this.AddUpdateTxt.Size = new System.Drawing.Size(158, 20);
            this.AddUpdateTxt.TabIndex = 10;
            // 
            // Form_Bankomat_Servisi_AddUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(394, 188);
            this.Controls.Add(this.FirmaLbl);
            this.Controls.Add(this.AkcijaServisaBtn);
            this.Controls.Add(this.AddUpdateTxt);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_Bankomat_Servisi_AddUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Dodaj servis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FirmaLbl;
        private System.Windows.Forms.Button AkcijaServisaBtn;
        private System.Windows.Forms.TextBox AddUpdateTxt;
    }
}