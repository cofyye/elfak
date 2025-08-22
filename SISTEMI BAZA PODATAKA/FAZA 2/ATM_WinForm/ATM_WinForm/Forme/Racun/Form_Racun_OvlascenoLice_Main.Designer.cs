namespace ATM_WinForm.Forme.Racun
{
    partial class Form_Racun_OvlascenoLice_Main
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
            this.IzbrisiLiceBtn = new System.Windows.Forms.Button();
            this.IzmeniLiceBtn = new System.Windows.Forms.Button();
            this.DodajLiceBtn = new System.Windows.Forms.Button();
            this.LicaGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.LicaGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // IzbrisiLiceBtn
            // 
            this.IzbrisiLiceBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzbrisiLiceBtn.Enabled = false;
            this.IzbrisiLiceBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzbrisiLiceBtn.Location = new System.Drawing.Point(11, 409);
            this.IzbrisiLiceBtn.Name = "IzbrisiLiceBtn";
            this.IzbrisiLiceBtn.Size = new System.Drawing.Size(316, 33);
            this.IzbrisiLiceBtn.TabIndex = 13;
            this.IzbrisiLiceBtn.Text = "Izbrisi Lice";
            this.IzbrisiLiceBtn.UseVisualStyleBackColor = false;
            this.IzbrisiLiceBtn.Click += new System.EventHandler(this.IzbrisiLiceBtn_Click);
            // 
            // IzmeniLiceBtn
            // 
            this.IzmeniLiceBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzmeniLiceBtn.Enabled = false;
            this.IzmeniLiceBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzmeniLiceBtn.Location = new System.Drawing.Point(11, 370);
            this.IzmeniLiceBtn.Name = "IzmeniLiceBtn";
            this.IzmeniLiceBtn.Size = new System.Drawing.Size(316, 33);
            this.IzmeniLiceBtn.TabIndex = 12;
            this.IzmeniLiceBtn.Text = "Izmeni Lice";
            this.IzmeniLiceBtn.UseVisualStyleBackColor = false;
            this.IzmeniLiceBtn.Click += new System.EventHandler(this.IzmeniLiceBtn_Click);
            // 
            // DodajLiceBtn
            // 
            this.DodajLiceBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.DodajLiceBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.DodajLiceBtn.Location = new System.Drawing.Point(11, 330);
            this.DodajLiceBtn.Name = "DodajLiceBtn";
            this.DodajLiceBtn.Size = new System.Drawing.Size(316, 33);
            this.DodajLiceBtn.TabIndex = 11;
            this.DodajLiceBtn.Text = "Dodaj Lice";
            this.DodajLiceBtn.UseVisualStyleBackColor = false;
            this.DodajLiceBtn.Click += new System.EventHandler(this.DodajLiceBtn_Click);
            // 
            // LicaGrid
            // 
            this.LicaGrid.AllowUserToOrderColumns = true;
            this.LicaGrid.AllowUserToResizeRows = false;
            this.LicaGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.LicaGrid.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.LicaGrid.ColumnHeadersHeight = 29;
            this.LicaGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.LicaGrid.Location = new System.Drawing.Point(11, 15);
            this.LicaGrid.MultiSelect = false;
            this.LicaGrid.Name = "LicaGrid";
            this.LicaGrid.ReadOnly = true;
            this.LicaGrid.RowHeadersWidth = 51;
            this.LicaGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.LicaGrid.Size = new System.Drawing.Size(316, 309);
            this.LicaGrid.TabIndex = 10;
            this.LicaGrid.SelectionChanged += new System.EventHandler(this.LicaGrid_SelectionChanged);
            // 
            // Form_Racun_OvlascenoLice_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(339, 460);
            this.Controls.Add(this.IzbrisiLiceBtn);
            this.Controls.Add(this.IzmeniLiceBtn);
            this.Controls.Add(this.DodajLiceBtn);
            this.Controls.Add(this.LicaGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_Racun_OvlascenoLice_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Racun_OvlascenoLice_Main";
            this.Load += new System.EventHandler(this.Form_Racun_OvlascenoLice_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LicaGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button IzbrisiLiceBtn;
        private System.Windows.Forms.Button IzmeniLiceBtn;
        private System.Windows.Forms.Button DodajLiceBtn;
        private System.Windows.Forms.DataGridView LicaGrid;
    }
}