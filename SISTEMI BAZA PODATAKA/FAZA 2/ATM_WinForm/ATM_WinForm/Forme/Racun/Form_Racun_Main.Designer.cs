namespace ATM_WinForm.Forme.Racun
{
    partial class Form_Racun_Main
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
            this.RacunGrid = new System.Windows.Forms.DataGridView();
            this.DodajRacunBtn = new System.Windows.Forms.Button();
            this.IzmeniRacunBtn = new System.Windows.Forms.Button();
            this.IzbrisiRacunBtn = new System.Windows.Forms.Button();
            this.PrikaziKorisnikaBtn = new System.Windows.Forms.Button();
            this.PrikaziKarticeBtn = new System.Windows.Forms.Button();
            this.OvlascenaLicaBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RacunGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // RacunGrid
            // 
            this.RacunGrid.AllowUserToOrderColumns = true;
            this.RacunGrid.AllowUserToResizeRows = false;
            this.RacunGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RacunGrid.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.RacunGrid.ColumnHeadersHeight = 29;
            this.RacunGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RacunGrid.Location = new System.Drawing.Point(10, 11);
            this.RacunGrid.MultiSelect = false;
            this.RacunGrid.Name = "RacunGrid";
            this.RacunGrid.ReadOnly = true;
            this.RacunGrid.RowHeadersWidth = 51;
            this.RacunGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.RacunGrid.Size = new System.Drawing.Size(893, 309);
            this.RacunGrid.TabIndex = 6;
            this.RacunGrid.SelectionChanged += new System.EventHandler(this.RacunGrid_SelectionChanged);
            // 
            // DodajRacunBtn
            // 
            this.DodajRacunBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.DodajRacunBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.DodajRacunBtn.Location = new System.Drawing.Point(10, 326);
            this.DodajRacunBtn.Name = "DodajRacunBtn";
            this.DodajRacunBtn.Size = new System.Drawing.Size(200, 33);
            this.DodajRacunBtn.TabIndex = 7;
            this.DodajRacunBtn.Text = "Dodaj Racun";
            this.DodajRacunBtn.UseVisualStyleBackColor = false;
            this.DodajRacunBtn.Click += new System.EventHandler(this.DodajRacunBtn_Click);
            // 
            // IzmeniRacunBtn
            // 
            this.IzmeniRacunBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzmeniRacunBtn.Enabled = false;
            this.IzmeniRacunBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzmeniRacunBtn.Location = new System.Drawing.Point(10, 366);
            this.IzmeniRacunBtn.Name = "IzmeniRacunBtn";
            this.IzmeniRacunBtn.Size = new System.Drawing.Size(200, 33);
            this.IzmeniRacunBtn.TabIndex = 8;
            this.IzmeniRacunBtn.Text = "Izmeni Racun";
            this.IzmeniRacunBtn.UseVisualStyleBackColor = false;
            this.IzmeniRacunBtn.Click += new System.EventHandler(this.IzmeniRacunBtn_Click);
            // 
            // IzbrisiRacunBtn
            // 
            this.IzbrisiRacunBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzbrisiRacunBtn.Enabled = false;
            this.IzbrisiRacunBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzbrisiRacunBtn.Location = new System.Drawing.Point(10, 405);
            this.IzbrisiRacunBtn.Name = "IzbrisiRacunBtn";
            this.IzbrisiRacunBtn.Size = new System.Drawing.Size(200, 33);
            this.IzbrisiRacunBtn.TabIndex = 9;
            this.IzbrisiRacunBtn.Text = "Izbrisi Racun";
            this.IzbrisiRacunBtn.UseVisualStyleBackColor = false;
            this.IzbrisiRacunBtn.Click += new System.EventHandler(this.IzbrisiRacunBtn_Click);
            // 
            // PrikaziKorisnikaBtn
            // 
            this.PrikaziKorisnikaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.PrikaziKorisnikaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.PrikaziKorisnikaBtn.Location = new System.Drawing.Point(742, 328);
            this.PrikaziKorisnikaBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PrikaziKorisnikaBtn.Name = "PrikaziKorisnikaBtn";
            this.PrikaziKorisnikaBtn.Size = new System.Drawing.Size(161, 31);
            this.PrikaziKorisnikaBtn.TabIndex = 10;
            this.PrikaziKorisnikaBtn.Text = "Prikazi korisnika";
            this.PrikaziKorisnikaBtn.UseVisualStyleBackColor = false;
            this.PrikaziKorisnikaBtn.Click += new System.EventHandler(this.PrikaziKorisnikaBtn_Click);
            // 
            // PrikaziKarticeBtn
            // 
            this.PrikaziKarticeBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.PrikaziKarticeBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.PrikaziKarticeBtn.Location = new System.Drawing.Point(742, 364);
            this.PrikaziKarticeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PrikaziKarticeBtn.Name = "PrikaziKarticeBtn";
            this.PrikaziKarticeBtn.Size = new System.Drawing.Size(161, 32);
            this.PrikaziKarticeBtn.TabIndex = 11;
            this.PrikaziKarticeBtn.Text = "Prikazi kartice";
            this.PrikaziKarticeBtn.UseVisualStyleBackColor = false;
            this.PrikaziKarticeBtn.Click += new System.EventHandler(this.PrikaziKarticeBtn_Click);
            // 
            // OvlascenaLicaBtn
            // 
            this.OvlascenaLicaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.OvlascenaLicaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.OvlascenaLicaBtn.Location = new System.Drawing.Point(742, 400);
            this.OvlascenaLicaBtn.Margin = new System.Windows.Forms.Padding(2);
            this.OvlascenaLicaBtn.Name = "OvlascenaLicaBtn";
            this.OvlascenaLicaBtn.Size = new System.Drawing.Size(161, 32);
            this.OvlascenaLicaBtn.TabIndex = 12;
            this.OvlascenaLicaBtn.Text = "Prikazi Ovlascena Lica";
            this.OvlascenaLicaBtn.UseVisualStyleBackColor = false;
            this.OvlascenaLicaBtn.Click += new System.EventHandler(this.OvlascenaLicaBtn_Click);
            // 
            // Form_Racun_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(910, 453);
            this.Controls.Add(this.OvlascenaLicaBtn);
            this.Controls.Add(this.PrikaziKarticeBtn);
            this.Controls.Add(this.PrikaziKorisnikaBtn);
            this.Controls.Add(this.IzbrisiRacunBtn);
            this.Controls.Add(this.IzmeniRacunBtn);
            this.Controls.Add(this.DodajRacunBtn);
            this.Controls.Add(this.RacunGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form_Racun_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Racuni";
            this.Load += new System.EventHandler(this.Form_Racun_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RacunGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView RacunGrid;
        private System.Windows.Forms.Button DodajRacunBtn;
        private System.Windows.Forms.Button IzmeniRacunBtn;
        private System.Windows.Forms.Button IzbrisiRacunBtn;
        private System.Windows.Forms.Button PrikaziKorisnikaBtn;
        private System.Windows.Forms.Button PrikaziKarticeBtn;
        private System.Windows.Forms.Button OvlascenaLicaBtn;
    }
}