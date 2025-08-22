namespace ATM_WinForm
{
    partial class Form_Banka_Main
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
            this.DodajBankuBtn = new System.Windows.Forms.Button();
            this.IzmeniBankuBtn = new System.Windows.Forms.Button();
            this.IzbrisiBankuBtn = new System.Windows.Forms.Button();
            this.BankaGrid = new System.Windows.Forms.DataGridView();
            this.BtnPregledFilijala = new System.Windows.Forms.Button();
            this.SpisakBrojevaTelefonaBtn = new System.Windows.Forms.Button();
            this.SpisakSvihRacunaBtn = new System.Windows.Forms.Button();
            this.PregledKlijenataBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BankaGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DodajBankuBtn
            // 
            this.DodajBankuBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.DodajBankuBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.DodajBankuBtn.Location = new System.Drawing.Point(12, 327);
            this.DodajBankuBtn.Name = "DodajBankuBtn";
            this.DodajBankuBtn.Size = new System.Drawing.Size(200, 33);
            this.DodajBankuBtn.TabIndex = 1;
            this.DodajBankuBtn.Text = "Dodaj Banku";
            this.DodajBankuBtn.UseVisualStyleBackColor = false;
            this.DodajBankuBtn.Click += new System.EventHandler(this.DodajBankuBtn_Click);
            // 
            // IzmeniBankuBtn
            // 
            this.IzmeniBankuBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzmeniBankuBtn.Enabled = false;
            this.IzmeniBankuBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzmeniBankuBtn.Location = new System.Drawing.Point(12, 366);
            this.IzmeniBankuBtn.Name = "IzmeniBankuBtn";
            this.IzmeniBankuBtn.Size = new System.Drawing.Size(200, 33);
            this.IzmeniBankuBtn.TabIndex = 2;
            this.IzmeniBankuBtn.Text = "Izmeni Banku";
            this.IzmeniBankuBtn.UseVisualStyleBackColor = false;
            this.IzmeniBankuBtn.Click += new System.EventHandler(this.IzmeniBankuBtn_Click);
            // 
            // IzbrisiBankuBtn
            // 
            this.IzbrisiBankuBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzbrisiBankuBtn.Enabled = false;
            this.IzbrisiBankuBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzbrisiBankuBtn.Location = new System.Drawing.Point(12, 405);
            this.IzbrisiBankuBtn.Name = "IzbrisiBankuBtn";
            this.IzbrisiBankuBtn.Size = new System.Drawing.Size(200, 33);
            this.IzbrisiBankuBtn.TabIndex = 3;
            this.IzbrisiBankuBtn.Text = "Izbrisi Banku";
            this.IzbrisiBankuBtn.UseVisualStyleBackColor = false;
            this.IzbrisiBankuBtn.Click += new System.EventHandler(this.IzbrisiBankuBtn_Click);
            // 
            // BankaGrid
            // 
            this.BankaGrid.AllowUserToOrderColumns = true;
            this.BankaGrid.AllowUserToResizeRows = false;
            this.BankaGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.BankaGrid.BackgroundColor = System.Drawing.Color.Beige;
            this.BankaGrid.ColumnHeadersHeight = 29;
            this.BankaGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.BankaGrid.Location = new System.Drawing.Point(12, 12);
            this.BankaGrid.MultiSelect = false;
            this.BankaGrid.Name = "BankaGrid";
            this.BankaGrid.ReadOnly = true;
            this.BankaGrid.RowHeadersWidth = 51;
            this.BankaGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.BankaGrid.Size = new System.Drawing.Size(883, 309);
            this.BankaGrid.TabIndex = 4;
            this.BankaGrid.SelectionChanged += new System.EventHandler(this.BankaGrid_SelectionChanged);
            // 
            // BtnPregledFilijala
            // 
            this.BtnPregledFilijala.BackColor = System.Drawing.Color.SandyBrown;
            this.BtnPregledFilijala.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.BtnPregledFilijala.Location = new System.Drawing.Point(696, 326);
            this.BtnPregledFilijala.Margin = new System.Windows.Forms.Padding(2);
            this.BtnPregledFilijala.Name = "BtnPregledFilijala";
            this.BtnPregledFilijala.Size = new System.Drawing.Size(200, 33);
            this.BtnPregledFilijala.TabIndex = 5;
            this.BtnPregledFilijala.Text = "Pregled filijala";
            this.BtnPregledFilijala.UseVisualStyleBackColor = false;
            this.BtnPregledFilijala.Click += new System.EventHandler(this.BtnPregledFilijala_Click);
            // 
            // SpisakBrojevaTelefonaBtn
            // 
            this.SpisakBrojevaTelefonaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.SpisakBrojevaTelefonaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.SpisakBrojevaTelefonaBtn.Location = new System.Drawing.Point(696, 363);
            this.SpisakBrojevaTelefonaBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SpisakBrojevaTelefonaBtn.Name = "SpisakBrojevaTelefonaBtn";
            this.SpisakBrojevaTelefonaBtn.Size = new System.Drawing.Size(200, 33);
            this.SpisakBrojevaTelefonaBtn.TabIndex = 6;
            this.SpisakBrojevaTelefonaBtn.Text = "Spisak brojeva telefona";
            this.SpisakBrojevaTelefonaBtn.UseVisualStyleBackColor = false;
            this.SpisakBrojevaTelefonaBtn.Click += new System.EventHandler(this.SpisakBrojevaTelefonaBtn_Click);
            // 
            // SpisakSvihRacunaBtn
            // 
            this.SpisakSvihRacunaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.SpisakSvihRacunaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.SpisakSvihRacunaBtn.Location = new System.Drawing.Point(696, 400);
            this.SpisakSvihRacunaBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SpisakSvihRacunaBtn.Name = "SpisakSvihRacunaBtn";
            this.SpisakSvihRacunaBtn.Size = new System.Drawing.Size(200, 33);
            this.SpisakSvihRacunaBtn.TabIndex = 7;
            this.SpisakSvihRacunaBtn.Text = "Spisak svih racuna";
            this.SpisakSvihRacunaBtn.UseVisualStyleBackColor = false;
            this.SpisakSvihRacunaBtn.Click += new System.EventHandler(this.SpisakSvihRacunaBtn_Click);
            // 
            // PregledKlijenataBtn
            // 
            this.PregledKlijenataBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.PregledKlijenataBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.PregledKlijenataBtn.Location = new System.Drawing.Point(343, 327);
            this.PregledKlijenataBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PregledKlijenataBtn.Name = "PregledKlijenataBtn";
            this.PregledKlijenataBtn.Size = new System.Drawing.Size(200, 33);
            this.PregledKlijenataBtn.TabIndex = 8;
            this.PregledKlijenataBtn.Text = "Pregled svih klijenata banke";
            this.PregledKlijenataBtn.UseVisualStyleBackColor = false;
            this.PregledKlijenataBtn.Click += new System.EventHandler(this.PregledKlijenataBtn_Click);
            // 
            // Form_Banka_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(907, 450);
            this.Controls.Add(this.PregledKlijenataBtn);
            this.Controls.Add(this.SpisakSvihRacunaBtn);
            this.Controls.Add(this.SpisakBrojevaTelefonaBtn);
            this.Controls.Add(this.BtnPregledFilijala);
            this.Controls.Add(this.BankaGrid);
            this.Controls.Add(this.IzbrisiBankuBtn);
            this.Controls.Add(this.IzmeniBankuBtn);
            this.Controls.Add(this.DodajBankuBtn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_Banka_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Banke";
            this.Load += new System.EventHandler(this.Form_Banka_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BankaGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button DodajBankuBtn;
        private System.Windows.Forms.Button IzmeniBankuBtn;
        private System.Windows.Forms.Button IzbrisiBankuBtn;
        private System.Windows.Forms.DataGridView BankaGrid;
        private System.Windows.Forms.Button BtnPregledFilijala;
        private System.Windows.Forms.Button SpisakBrojevaTelefonaBtn;
        private System.Windows.Forms.Button SpisakSvihRacunaBtn;
        private System.Windows.Forms.Button PregledKlijenataBtn;
    }
}