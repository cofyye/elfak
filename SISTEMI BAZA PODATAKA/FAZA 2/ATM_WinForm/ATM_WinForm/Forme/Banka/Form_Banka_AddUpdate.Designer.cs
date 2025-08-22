namespace ATM_WinForm
{
    partial class Form_Banka_AddUpdate
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
            this.ImeLbl = new System.Windows.Forms.Label();
            this.ImeTxtBx = new System.Windows.Forms.TextBox();
            this.EmailTxtBx = new System.Windows.Forms.TextBox();
            this.EmailLbl = new System.Windows.Forms.Label();
            this.WebAdresaTxtBx = new System.Windows.Forms.TextBox();
            this.WebAdresaLbl = new System.Windows.Forms.Label();
            this.AdresaCentraleTxtBx = new System.Windows.Forms.TextBox();
            this.AdresaCentraleLbl = new System.Windows.Forms.Label();
            this.AkcijaBankeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ImeLbl
            // 
            this.ImeLbl.AutoSize = true;
            this.ImeLbl.Location = new System.Drawing.Point(69, 25);
            this.ImeLbl.Name = "ImeLbl";
            this.ImeLbl.Size = new System.Drawing.Size(24, 13);
            this.ImeLbl.TabIndex = 0;
            this.ImeLbl.Text = "Ime";
            // 
            // ImeTxtBx
            // 
            this.ImeTxtBx.Location = new System.Drawing.Point(72, 51);
            this.ImeTxtBx.Name = "ImeTxtBx";
            this.ImeTxtBx.Size = new System.Drawing.Size(237, 20);
            this.ImeTxtBx.TabIndex = 2;
            // 
            // EmailTxtBx
            // 
            this.EmailTxtBx.Location = new System.Drawing.Point(72, 116);
            this.EmailTxtBx.Name = "EmailTxtBx";
            this.EmailTxtBx.Size = new System.Drawing.Size(237, 20);
            this.EmailTxtBx.TabIndex = 4;
            // 
            // EmailLbl
            // 
            this.EmailLbl.AutoSize = true;
            this.EmailLbl.Location = new System.Drawing.Point(69, 90);
            this.EmailLbl.Name = "EmailLbl";
            this.EmailLbl.Size = new System.Drawing.Size(32, 13);
            this.EmailLbl.TabIndex = 3;
            this.EmailLbl.Text = "Email";
            // 
            // WebAdresaTxtBx
            // 
            this.WebAdresaTxtBx.Location = new System.Drawing.Point(72, 184);
            this.WebAdresaTxtBx.Name = "WebAdresaTxtBx";
            this.WebAdresaTxtBx.Size = new System.Drawing.Size(237, 20);
            this.WebAdresaTxtBx.TabIndex = 6;
            // 
            // WebAdresaLbl
            // 
            this.WebAdresaLbl.AutoSize = true;
            this.WebAdresaLbl.Location = new System.Drawing.Point(69, 158);
            this.WebAdresaLbl.Name = "WebAdresaLbl";
            this.WebAdresaLbl.Size = new System.Drawing.Size(66, 13);
            this.WebAdresaLbl.TabIndex = 5;
            this.WebAdresaLbl.Text = "Web Adresa";
            // 
            // AdresaCentraleTxtBx
            // 
            this.AdresaCentraleTxtBx.Location = new System.Drawing.Point(72, 255);
            this.AdresaCentraleTxtBx.Name = "AdresaCentraleTxtBx";
            this.AdresaCentraleTxtBx.Size = new System.Drawing.Size(237, 20);
            this.AdresaCentraleTxtBx.TabIndex = 8;
            // 
            // AdresaCentraleLbl
            // 
            this.AdresaCentraleLbl.AutoSize = true;
            this.AdresaCentraleLbl.Location = new System.Drawing.Point(69, 229);
            this.AdresaCentraleLbl.Name = "AdresaCentraleLbl";
            this.AdresaCentraleLbl.Size = new System.Drawing.Size(82, 13);
            this.AdresaCentraleLbl.TabIndex = 7;
            this.AdresaCentraleLbl.Text = "Adresa Centrale";
            // 
            // AkcijaBankeBtn
            // 
            this.AkcijaBankeBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.AkcijaBankeBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.AkcijaBankeBtn.Location = new System.Drawing.Point(72, 308);
            this.AkcijaBankeBtn.Name = "AkcijaBankeBtn";
            this.AkcijaBankeBtn.Size = new System.Drawing.Size(237, 28);
            this.AkcijaBankeBtn.TabIndex = 9;
            this.AkcijaBankeBtn.Text = "Dodaj Banku";
            this.AkcijaBankeBtn.UseVisualStyleBackColor = false;
            this.AkcijaBankeBtn.Click += new System.EventHandler(this.AkcijaBankeBtn_Click);
            // 
            // Form_Banka_AddUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(378, 350);
            this.Controls.Add(this.AkcijaBankeBtn);
            this.Controls.Add(this.AdresaCentraleTxtBx);
            this.Controls.Add(this.AdresaCentraleLbl);
            this.Controls.Add(this.WebAdresaTxtBx);
            this.Controls.Add(this.WebAdresaLbl);
            this.Controls.Add(this.EmailTxtBx);
            this.Controls.Add(this.EmailLbl);
            this.Controls.Add(this.ImeTxtBx);
            this.Controls.Add(this.ImeLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_Banka_AddUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Dodaj Banku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ImeLbl;
        private System.Windows.Forms.TextBox ImeTxtBx;
        private System.Windows.Forms.TextBox EmailTxtBx;
        private System.Windows.Forms.Label EmailLbl;
        private System.Windows.Forms.TextBox WebAdresaTxtBx;
        private System.Windows.Forms.Label WebAdresaLbl;
        private System.Windows.Forms.TextBox AdresaCentraleTxtBx;
        private System.Windows.Forms.Label AdresaCentraleLbl;
        private System.Windows.Forms.Button AkcijaBankeBtn;
    }
}