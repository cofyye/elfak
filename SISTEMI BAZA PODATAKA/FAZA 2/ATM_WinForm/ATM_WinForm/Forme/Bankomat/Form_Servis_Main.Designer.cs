namespace ATM_WinForm.Forme.Bankomat
{
    partial class Form_Servis_Main
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
            this.ServisiGrid = new System.Windows.Forms.DataGridView();
            this.IzbrisiServisBtn = new System.Windows.Forms.Button();
            this.IzmeniServisBtn = new System.Windows.Forms.Button();
            this.DodajServisBtn = new System.Windows.Forms.Button();
            this.PrikaziGreskeBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ServisiGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ServisiGrid
            // 
            this.ServisiGrid.AllowUserToOrderColumns = true;
            this.ServisiGrid.AllowUserToResizeRows = false;
            this.ServisiGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ServisiGrid.BackgroundColor = System.Drawing.Color.Beige;
            this.ServisiGrid.ColumnHeadersHeight = 35;
            this.ServisiGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ServisiGrid.Location = new System.Drawing.Point(10, 11);
            this.ServisiGrid.MultiSelect = false;
            this.ServisiGrid.Name = "ServisiGrid";
            this.ServisiGrid.ReadOnly = true;
            this.ServisiGrid.RowHeadersWidth = 51;
            this.ServisiGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ServisiGrid.Size = new System.Drawing.Size(494, 309);
            this.ServisiGrid.TabIndex = 20;
            this.ServisiGrid.SelectionChanged += new System.EventHandler(this.ServisiGrid_SelectionChanged);
            // 
            // IzbrisiServisBtn
            // 
            this.IzbrisiServisBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzbrisiServisBtn.Enabled = false;
            this.IzbrisiServisBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzbrisiServisBtn.Location = new System.Drawing.Point(10, 405);
            this.IzbrisiServisBtn.Name = "IzbrisiServisBtn";
            this.IzbrisiServisBtn.Size = new System.Drawing.Size(200, 33);
            this.IzbrisiServisBtn.TabIndex = 25;
            this.IzbrisiServisBtn.Text = "Izbrisi Servis";
            this.IzbrisiServisBtn.UseVisualStyleBackColor = false;
            this.IzbrisiServisBtn.Click += new System.EventHandler(this.IzbrisiServisBtn_Click);
            // 
            // IzmeniServisBtn
            // 
            this.IzmeniServisBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzmeniServisBtn.Enabled = false;
            this.IzmeniServisBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzmeniServisBtn.Location = new System.Drawing.Point(10, 366);
            this.IzmeniServisBtn.Name = "IzmeniServisBtn";
            this.IzmeniServisBtn.Size = new System.Drawing.Size(200, 33);
            this.IzmeniServisBtn.TabIndex = 24;
            this.IzmeniServisBtn.Text = "Izmeni Servis";
            this.IzmeniServisBtn.UseVisualStyleBackColor = false;
            this.IzmeniServisBtn.Click += new System.EventHandler(this.IzmeniServisBtn_Click);
            // 
            // DodajServisBtn
            // 
            this.DodajServisBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.DodajServisBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.DodajServisBtn.Location = new System.Drawing.Point(10, 326);
            this.DodajServisBtn.Name = "DodajServisBtn";
            this.DodajServisBtn.Size = new System.Drawing.Size(200, 33);
            this.DodajServisBtn.TabIndex = 23;
            this.DodajServisBtn.Text = "Dodaj Servis";
            this.DodajServisBtn.UseVisualStyleBackColor = false;
            this.DodajServisBtn.Click += new System.EventHandler(this.DodajServisBtn_Click);
            // 
            // PrikaziGreskeBtn
            // 
            this.PrikaziGreskeBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.PrikaziGreskeBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.PrikaziGreskeBtn.Location = new System.Drawing.Point(272, 326);
            this.PrikaziGreskeBtn.Name = "PrikaziGreskeBtn";
            this.PrikaziGreskeBtn.Size = new System.Drawing.Size(200, 33);
            this.PrikaziGreskeBtn.TabIndex = 26;
            this.PrikaziGreskeBtn.Text = "Pregled otklonjenih gresaka";
            this.PrikaziGreskeBtn.UseVisualStyleBackColor = false;
            this.PrikaziGreskeBtn.Click += new System.EventHandler(this.PrikaziGreskeBtn_Click);
            // 
            // Form_Servis_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(516, 464);
            this.Controls.Add(this.PrikaziGreskeBtn);
            this.Controls.Add(this.IzbrisiServisBtn);
            this.Controls.Add(this.IzmeniServisBtn);
            this.Controls.Add(this.DodajServisBtn);
            this.Controls.Add(this.ServisiGrid);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_Servis_Main";
            this.Text = "ATM - Servisi Bankomata";
            this.Load += new System.EventHandler(this.Form_Servis_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ServisiGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ServisiGrid;
        private System.Windows.Forms.Button IzbrisiServisBtn;
        private System.Windows.Forms.Button IzmeniServisBtn;
        private System.Windows.Forms.Button DodajServisBtn;
        private System.Windows.Forms.Button PrikaziGreskeBtn;
    }
}