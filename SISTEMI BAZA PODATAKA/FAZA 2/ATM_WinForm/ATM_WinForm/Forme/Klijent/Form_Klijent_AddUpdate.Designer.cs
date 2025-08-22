namespace ATM_WinForm.Forme.Klijent
{
    partial class Form_Klijent_AddUpdate
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
            this.AkcijaKlijentaBtn = new System.Windows.Forms.Button();
            this.EmailTxtBx = new System.Windows.Forms.TextBox();
            this.EmailLbl = new System.Windows.Forms.Label();
            this.AdresaTxtBx = new System.Windows.Forms.TextBox();
            this.AdresaLbl = new System.Windows.Forms.Label();
            this.BrTelTxtBx = new System.Windows.Forms.TextBox();
            this.BrTelLbl = new System.Windows.Forms.Label();
            this.NazivTxtBx = new System.Windows.Forms.TextBox();
            this.NazivLbl = new System.Windows.Forms.Label();
            this.KlijentComboBox = new System.Windows.Forms.ComboBox();
            this.JMBGTxtBx = new System.Windows.Forms.TextBox();
            this.jmbgLbl = new System.Windows.Forms.Label();
            this.PrezimeTxtBx = new System.Windows.Forms.TextBox();
            this.PrezimeLbl = new System.Windows.Forms.Label();
            this.ImeRoditeljaTxtBx = new System.Windows.Forms.TextBox();
            this.ImeRoditeljaLbl = new System.Windows.Forms.Label();
            this.LimeTxtBx = new System.Windows.Forms.TextBox();
            this.LimeLbl = new System.Windows.Forms.Label();
            this.PoreskiIdTxtBx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DatumRodjenjaLbl = new System.Windows.Forms.Label();
            this.MestoIzdavanjaTxtBx = new System.Windows.Forms.TextBox();
            this.MestoIzdavanjaLbl = new System.Windows.Forms.Label();
            this.BrLicneKarteTxtBx = new System.Windows.Forms.TextBox();
            this.BrLicneLbl = new System.Windows.Forms.Label();
            this.DatumRodjenjaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // AkcijaKlijentaBtn
            // 
            this.AkcijaKlijentaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.AkcijaKlijentaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.AkcijaKlijentaBtn.Location = new System.Drawing.Point(158, 326);
            this.AkcijaKlijentaBtn.Name = "AkcijaKlijentaBtn";
            this.AkcijaKlijentaBtn.Size = new System.Drawing.Size(237, 28);
            this.AkcijaKlijentaBtn.TabIndex = 10;
            this.AkcijaKlijentaBtn.Text = "Dodaj Klijenta";
            this.AkcijaKlijentaBtn.UseVisualStyleBackColor = false;
            this.AkcijaKlijentaBtn.Click += new System.EventHandler(this.AkcijaKlijentaBtn_Click);
            // 
            // EmailTxtBx
            // 
            this.EmailTxtBx.Location = new System.Drawing.Point(79, 288);
            this.EmailTxtBx.Name = "EmailTxtBx";
            this.EmailTxtBx.Size = new System.Drawing.Size(184, 20);
            this.EmailTxtBx.TabIndex = 18;
            // 
            // EmailLbl
            // 
            this.EmailLbl.AutoSize = true;
            this.EmailLbl.Location = new System.Drawing.Point(76, 262);
            this.EmailLbl.Name = "EmailLbl";
            this.EmailLbl.Size = new System.Drawing.Size(32, 13);
            this.EmailLbl.TabIndex = 17;
            this.EmailLbl.Text = "Email";
            // 
            // AdresaTxtBx
            // 
            this.AdresaTxtBx.Location = new System.Drawing.Point(79, 217);
            this.AdresaTxtBx.Name = "AdresaTxtBx";
            this.AdresaTxtBx.Size = new System.Drawing.Size(184, 20);
            this.AdresaTxtBx.TabIndex = 16;
            // 
            // AdresaLbl
            // 
            this.AdresaLbl.AutoSize = true;
            this.AdresaLbl.Location = new System.Drawing.Point(76, 191);
            this.AdresaLbl.Name = "AdresaLbl";
            this.AdresaLbl.Size = new System.Drawing.Size(40, 13);
            this.AdresaLbl.TabIndex = 15;
            this.AdresaLbl.Text = "Adresa";
            // 
            // BrTelTxtBx
            // 
            this.BrTelTxtBx.Location = new System.Drawing.Point(79, 159);
            this.BrTelTxtBx.Name = "BrTelTxtBx";
            this.BrTelTxtBx.Size = new System.Drawing.Size(184, 20);
            this.BrTelTxtBx.TabIndex = 14;
            // 
            // BrTelLbl
            // 
            this.BrTelLbl.AutoSize = true;
            this.BrTelLbl.Location = new System.Drawing.Point(76, 123);
            this.BrTelLbl.Name = "BrTelLbl";
            this.BrTelLbl.Size = new System.Drawing.Size(66, 13);
            this.BrTelLbl.TabIndex = 13;
            this.BrTelLbl.Text = "Broj telefona";
            // 
            // NazivTxtBx
            // 
            this.NazivTxtBx.Location = new System.Drawing.Point(79, 32);
            this.NazivTxtBx.Name = "NazivTxtBx";
            this.NazivTxtBx.Size = new System.Drawing.Size(184, 20);
            this.NazivTxtBx.TabIndex = 12;
            // 
            // NazivLbl
            // 
            this.NazivLbl.AutoSize = true;
            this.NazivLbl.Location = new System.Drawing.Point(76, 6);
            this.NazivLbl.Name = "NazivLbl";
            this.NazivLbl.Size = new System.Drawing.Size(34, 13);
            this.NazivLbl.TabIndex = 11;
            this.NazivLbl.Text = "Naziv";
            // 
            // KlijentComboBox
            // 
            this.KlijentComboBox.Items.AddRange(new object[] {
            "Pravna lica",
            "Fizicka lica"});
            this.KlijentComboBox.Location = new System.Drawing.Point(308, 31);
            this.KlijentComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.KlijentComboBox.MaxDropDownItems = 50;
            this.KlijentComboBox.Name = "KlijentComboBox";
            this.KlijentComboBox.Size = new System.Drawing.Size(176, 21);
            this.KlijentComboBox.TabIndex = 21;
            this.KlijentComboBox.SelectedIndexChanged += new System.EventHandler(this.KlijentComboBox_SelectedIndexChanged);
            // 
            // JMBGTxtBx
            // 
            this.JMBGTxtBx.Location = new System.Drawing.Point(308, 288);
            this.JMBGTxtBx.Name = "JMBGTxtBx";
            this.JMBGTxtBx.Size = new System.Drawing.Size(184, 20);
            this.JMBGTxtBx.TabIndex = 27;
            // 
            // jmbgLbl
            // 
            this.jmbgLbl.AutoSize = true;
            this.jmbgLbl.Location = new System.Drawing.Point(305, 262);
            this.jmbgLbl.Name = "jmbgLbl";
            this.jmbgLbl.Size = new System.Drawing.Size(36, 13);
            this.jmbgLbl.TabIndex = 26;
            this.jmbgLbl.Text = "JMBG";
            // 
            // PrezimeTxtBx
            // 
            this.PrezimeTxtBx.Location = new System.Drawing.Point(308, 217);
            this.PrezimeTxtBx.Name = "PrezimeTxtBx";
            this.PrezimeTxtBx.Size = new System.Drawing.Size(184, 20);
            this.PrezimeTxtBx.TabIndex = 25;
            // 
            // PrezimeLbl
            // 
            this.PrezimeLbl.AutoSize = true;
            this.PrezimeLbl.Location = new System.Drawing.Point(305, 191);
            this.PrezimeLbl.Name = "PrezimeLbl";
            this.PrezimeLbl.Size = new System.Drawing.Size(44, 13);
            this.PrezimeLbl.TabIndex = 24;
            this.PrezimeLbl.Text = "Prezime";
            // 
            // ImeRoditeljaTxtBx
            // 
            this.ImeRoditeljaTxtBx.Location = new System.Drawing.Point(308, 149);
            this.ImeRoditeljaTxtBx.Name = "ImeRoditeljaTxtBx";
            this.ImeRoditeljaTxtBx.Size = new System.Drawing.Size(184, 20);
            this.ImeRoditeljaTxtBx.TabIndex = 23;
            // 
            // ImeRoditeljaLbl
            // 
            this.ImeRoditeljaLbl.AutoSize = true;
            this.ImeRoditeljaLbl.Location = new System.Drawing.Point(305, 123);
            this.ImeRoditeljaLbl.Name = "ImeRoditeljaLbl";
            this.ImeRoditeljaLbl.Size = new System.Drawing.Size(63, 13);
            this.ImeRoditeljaLbl.TabIndex = 22;
            this.ImeRoditeljaLbl.Text = "Ime roditelja";
            // 
            // LimeTxtBx
            // 
            this.LimeTxtBx.Location = new System.Drawing.Point(308, 90);
            this.LimeTxtBx.Name = "LimeTxtBx";
            this.LimeTxtBx.Size = new System.Drawing.Size(184, 20);
            this.LimeTxtBx.TabIndex = 31;
            // 
            // LimeLbl
            // 
            this.LimeLbl.AutoSize = true;
            this.LimeLbl.Location = new System.Drawing.Point(305, 64);
            this.LimeLbl.Name = "LimeLbl";
            this.LimeLbl.Size = new System.Drawing.Size(29, 13);
            this.LimeLbl.TabIndex = 30;
            this.LimeLbl.Text = "Lime";
            // 
            // PoreskiIdTxtBx
            // 
            this.PoreskiIdTxtBx.Location = new System.Drawing.Point(79, 90);
            this.PoreskiIdTxtBx.Name = "PoreskiIdTxtBx";
            this.PoreskiIdTxtBx.Size = new System.Drawing.Size(184, 20);
            this.PoreskiIdTxtBx.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Poreski Id";
            // 
            // DatumRodjenjaLbl
            // 
            this.DatumRodjenjaLbl.AutoSize = true;
            this.DatumRodjenjaLbl.Location = new System.Drawing.Point(514, 64);
            this.DatumRodjenjaLbl.Name = "DatumRodjenjaLbl";
            this.DatumRodjenjaLbl.Size = new System.Drawing.Size(78, 13);
            this.DatumRodjenjaLbl.TabIndex = 36;
            this.DatumRodjenjaLbl.Text = "Datum rodjenja";
            // 
            // MestoIzdavanjaTxtBx
            // 
            this.MestoIzdavanjaTxtBx.Location = new System.Drawing.Point(517, 217);
            this.MestoIzdavanjaTxtBx.Name = "MestoIzdavanjaTxtBx";
            this.MestoIzdavanjaTxtBx.Size = new System.Drawing.Size(184, 20);
            this.MestoIzdavanjaTxtBx.TabIndex = 35;
            // 
            // MestoIzdavanjaLbl
            // 
            this.MestoIzdavanjaLbl.AutoSize = true;
            this.MestoIzdavanjaLbl.Location = new System.Drawing.Point(514, 191);
            this.MestoIzdavanjaLbl.Name = "MestoIzdavanjaLbl";
            this.MestoIzdavanjaLbl.Size = new System.Drawing.Size(84, 13);
            this.MestoIzdavanjaLbl.TabIndex = 34;
            this.MestoIzdavanjaLbl.Text = "Mesto izdavanja";
            // 
            // BrLicneKarteTxtBx
            // 
            this.BrLicneKarteTxtBx.Location = new System.Drawing.Point(517, 149);
            this.BrLicneKarteTxtBx.Name = "BrLicneKarteTxtBx";
            this.BrLicneKarteTxtBx.Size = new System.Drawing.Size(184, 20);
            this.BrLicneKarteTxtBx.TabIndex = 33;
            // 
            // BrLicneLbl
            // 
            this.BrLicneLbl.AutoSize = true;
            this.BrLicneLbl.Location = new System.Drawing.Point(514, 123);
            this.BrLicneLbl.Name = "BrLicneLbl";
            this.BrLicneLbl.Size = new System.Drawing.Size(77, 13);
            this.BrLicneLbl.TabIndex = 32;
            this.BrLicneLbl.Text = "Broj licne karte";
            // 
            // DatumRodjenjaDateTimePicker
            // 
            this.DatumRodjenjaDateTimePicker.Location = new System.Drawing.Point(517, 87);
            this.DatumRodjenjaDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.DatumRodjenjaDateTimePicker.Name = "DatumRodjenjaDateTimePicker";
            this.DatumRodjenjaDateTimePicker.Size = new System.Drawing.Size(206, 20);
            this.DatumRodjenjaDateTimePicker.TabIndex = 37;
            // 
            // Form_Klijent_AddUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(734, 368);
            this.Controls.Add(this.DatumRodjenjaDateTimePicker);
            this.Controls.Add(this.DatumRodjenjaLbl);
            this.Controls.Add(this.MestoIzdavanjaTxtBx);
            this.Controls.Add(this.MestoIzdavanjaLbl);
            this.Controls.Add(this.BrLicneKarteTxtBx);
            this.Controls.Add(this.BrLicneLbl);
            this.Controls.Add(this.LimeTxtBx);
            this.Controls.Add(this.LimeLbl);
            this.Controls.Add(this.PoreskiIdTxtBx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.JMBGTxtBx);
            this.Controls.Add(this.jmbgLbl);
            this.Controls.Add(this.PrezimeTxtBx);
            this.Controls.Add(this.PrezimeLbl);
            this.Controls.Add(this.ImeRoditeljaTxtBx);
            this.Controls.Add(this.ImeRoditeljaLbl);
            this.Controls.Add(this.KlijentComboBox);
            this.Controls.Add(this.EmailTxtBx);
            this.Controls.Add(this.EmailLbl);
            this.Controls.Add(this.AdresaTxtBx);
            this.Controls.Add(this.AdresaLbl);
            this.Controls.Add(this.BrTelTxtBx);
            this.Controls.Add(this.BrTelLbl);
            this.Controls.Add(this.NazivTxtBx);
            this.Controls.Add(this.NazivLbl);
            this.Controls.Add(this.AkcijaKlijentaBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_Klijent_AddUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Dodaj Klijenta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AkcijaKlijentaBtn;
        private System.Windows.Forms.TextBox EmailTxtBx;
        private System.Windows.Forms.Label EmailLbl;
        private System.Windows.Forms.TextBox AdresaTxtBx;
        private System.Windows.Forms.Label AdresaLbl;
        private System.Windows.Forms.TextBox BrTelTxtBx;
        private System.Windows.Forms.Label BrTelLbl;
        private System.Windows.Forms.TextBox NazivTxtBx;
        private System.Windows.Forms.Label NazivLbl;
        private System.Windows.Forms.ComboBox KlijentComboBox;
        private System.Windows.Forms.TextBox JMBGTxtBx;
        private System.Windows.Forms.Label jmbgLbl;
        private System.Windows.Forms.TextBox PrezimeTxtBx;
        private System.Windows.Forms.Label PrezimeLbl;
        private System.Windows.Forms.TextBox ImeRoditeljaTxtBx;
        private System.Windows.Forms.Label ImeRoditeljaLbl;
        private System.Windows.Forms.TextBox LimeTxtBx;
        private System.Windows.Forms.Label LimeLbl;
        private System.Windows.Forms.TextBox PoreskiIdTxtBx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label DatumRodjenjaLbl;
        private System.Windows.Forms.TextBox MestoIzdavanjaTxtBx;
        private System.Windows.Forms.Label MestoIzdavanjaLbl;
        private System.Windows.Forms.TextBox BrLicneKarteTxtBx;
        private System.Windows.Forms.Label BrLicneLbl;
        private System.Windows.Forms.DateTimePicker DatumRodjenjaDateTimePicker;
    }
}