namespace ATM_WinForm.Forme.Klijent
{
    partial class Form_Klijent_Main
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
            this.IzbrisiKlijentaBtn = new System.Windows.Forms.Button();
            this.IzmeniKlijentaBtn = new System.Windows.Forms.Button();
            this.DodajKlijentaBtn = new System.Windows.Forms.Button();
            this.KlijentGrid = new System.Windows.Forms.DataGridView();
            this.KlijentComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PrikaziKomentareBtn = new System.Windows.Forms.Button();
            this.PrikaziRacuneBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.KlijentGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // IzbrisiKlijentaBtn
            // 
            this.IzbrisiKlijentaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzbrisiKlijentaBtn.Enabled = false;
            this.IzbrisiKlijentaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzbrisiKlijentaBtn.Location = new System.Drawing.Point(9, 407);
            this.IzbrisiKlijentaBtn.Name = "IzbrisiKlijentaBtn";
            this.IzbrisiKlijentaBtn.Size = new System.Drawing.Size(200, 33);
            this.IzbrisiKlijentaBtn.TabIndex = 14;
            this.IzbrisiKlijentaBtn.Text = "Izbrisi Klijenta";
            this.IzbrisiKlijentaBtn.UseVisualStyleBackColor = false;
            this.IzbrisiKlijentaBtn.Click += new System.EventHandler(this.IzbrisiKlijentaBtn_Click);
            // 
            // IzmeniKlijentaBtn
            // 
            this.IzmeniKlijentaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzmeniKlijentaBtn.Enabled = false;
            this.IzmeniKlijentaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzmeniKlijentaBtn.Location = new System.Drawing.Point(9, 368);
            this.IzmeniKlijentaBtn.Name = "IzmeniKlijentaBtn";
            this.IzmeniKlijentaBtn.Size = new System.Drawing.Size(200, 33);
            this.IzmeniKlijentaBtn.TabIndex = 13;
            this.IzmeniKlijentaBtn.Text = "Izmeni Klijenta";
            this.IzmeniKlijentaBtn.UseVisualStyleBackColor = false;
            this.IzmeniKlijentaBtn.Click += new System.EventHandler(this.IzmeniKlijentaBtn_Click);
            // 
            // DodajKlijentaBtn
            // 
            this.DodajKlijentaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.DodajKlijentaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.DodajKlijentaBtn.Location = new System.Drawing.Point(9, 328);
            this.DodajKlijentaBtn.Name = "DodajKlijentaBtn";
            this.DodajKlijentaBtn.Size = new System.Drawing.Size(200, 33);
            this.DodajKlijentaBtn.TabIndex = 12;
            this.DodajKlijentaBtn.Text = "Dodaj Klijenta";
            this.DodajKlijentaBtn.UseVisualStyleBackColor = false;
            this.DodajKlijentaBtn.Click += new System.EventHandler(this.DodajKlijentaBtn_Click);
            // 
            // KlijentGrid
            // 
            this.KlijentGrid.AllowUserToOrderColumns = true;
            this.KlijentGrid.AllowUserToResizeRows = false;
            this.KlijentGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.KlijentGrid.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.KlijentGrid.ColumnHeadersHeight = 35;
            this.KlijentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.KlijentGrid.Location = new System.Drawing.Point(9, 13);
            this.KlijentGrid.MultiSelect = false;
            this.KlijentGrid.Name = "KlijentGrid";
            this.KlijentGrid.ReadOnly = true;
            this.KlijentGrid.RowHeadersWidth = 51;
            this.KlijentGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.KlijentGrid.Size = new System.Drawing.Size(893, 309);
            this.KlijentGrid.TabIndex = 11;
            this.KlijentGrid.SelectionChanged += new System.EventHandler(this.KlijentGrid_SelectionChanged);
            // 
            // KlijentComboBox
            // 
            this.KlijentComboBox.Items.AddRange(new object[] {
            "Svi klijenti",
            "Pravna lica",
            "Fizicka lica"});
            this.KlijentComboBox.Location = new System.Drawing.Point(261, 340);
            this.KlijentComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.KlijentComboBox.MaxDropDownItems = 50;
            this.KlijentComboBox.Name = "KlijentComboBox";
            this.KlijentComboBox.Size = new System.Drawing.Size(176, 21);
            this.KlijentComboBox.TabIndex = 20;
            this.KlijentComboBox.SelectedIndexChanged += new System.EventHandler(this.KlijentComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Izaberi vrstu korisnika za prikaz";
            // 
            // PrikaziKomentareBtn
            // 
            this.PrikaziKomentareBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.PrikaziKomentareBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.PrikaziKomentareBtn.Location = new System.Drawing.Point(698, 328);
            this.PrikaziKomentareBtn.Name = "PrikaziKomentareBtn";
            this.PrikaziKomentareBtn.Size = new System.Drawing.Size(200, 33);
            this.PrikaziKomentareBtn.TabIndex = 22;
            this.PrikaziKomentareBtn.Text = "Prikazi Komentare";
            this.PrikaziKomentareBtn.UseVisualStyleBackColor = false;
            this.PrikaziKomentareBtn.Click += new System.EventHandler(this.PrikaziKomentareBtn_Click);
            // 
            // PrikaziRacuneBtn
            // 
            this.PrikaziRacuneBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.PrikaziRacuneBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.PrikaziRacuneBtn.Location = new System.Drawing.Point(698, 367);
            this.PrikaziRacuneBtn.Name = "PrikaziRacuneBtn";
            this.PrikaziRacuneBtn.Size = new System.Drawing.Size(200, 50);
            this.PrikaziRacuneBtn.TabIndex = 23;
            this.PrikaziRacuneBtn.Text = "Prikazi Sve Klijentove Racune";
            this.PrikaziRacuneBtn.UseVisualStyleBackColor = false;
            this.PrikaziRacuneBtn.Click += new System.EventHandler(this.PrikaziRacuneBtn_Click);
            // 
            // Form_Klijent_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(910, 453);
            this.Controls.Add(this.PrikaziRacuneBtn);
            this.Controls.Add(this.PrikaziKomentareBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KlijentComboBox);
            this.Controls.Add(this.IzbrisiKlijentaBtn);
            this.Controls.Add(this.IzmeniKlijentaBtn);
            this.Controls.Add(this.DodajKlijentaBtn);
            this.Controls.Add(this.KlijentGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_Klijent_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Klijenti";
            this.Load += new System.EventHandler(this.Form_Klijent_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KlijentGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button IzbrisiKlijentaBtn;
        private System.Windows.Forms.Button IzmeniKlijentaBtn;
        private System.Windows.Forms.Button DodajKlijentaBtn;
        private System.Windows.Forms.DataGridView KlijentGrid;
        private System.Windows.Forms.ComboBox KlijentComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button PrikaziKomentareBtn;
        private System.Windows.Forms.Button PrikaziRacuneBtn;
    }
}