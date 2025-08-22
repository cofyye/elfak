namespace ATM_WinForm.Forme.Kartica
{
    partial class Form_Kartica_Main
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
            this.KarticaGrid = new System.Windows.Forms.DataGridView();
            this.DodajKarticuBtn = new System.Windows.Forms.Button();
            this.IzmeniKarticuBtn = new System.Windows.Forms.Button();
            this.IzbrisiKarticuBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.KarticaGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // KarticaGrid
            // 
            this.KarticaGrid.AllowUserToOrderColumns = true;
            this.KarticaGrid.AllowUserToResizeRows = false;
            this.KarticaGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.KarticaGrid.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.KarticaGrid.ColumnHeadersHeight = 35;
            this.KarticaGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.KarticaGrid.Location = new System.Drawing.Point(10, 11);
            this.KarticaGrid.MultiSelect = false;
            this.KarticaGrid.Name = "KarticaGrid";
            this.KarticaGrid.ReadOnly = true;
            this.KarticaGrid.RowHeadersWidth = 51;
            this.KarticaGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.KarticaGrid.Size = new System.Drawing.Size(890, 309);
            this.KarticaGrid.TabIndex = 10;
            this.KarticaGrid.SelectionChanged += new System.EventHandler(this.KarticaGrid_SelectionChanged);
            // 
            // DodajKarticuBtn
            // 
            this.DodajKarticuBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.DodajKarticuBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.DodajKarticuBtn.Location = new System.Drawing.Point(10, 326);
            this.DodajKarticuBtn.Name = "DodajKarticuBtn";
            this.DodajKarticuBtn.Size = new System.Drawing.Size(200, 33);
            this.DodajKarticuBtn.TabIndex = 11;
            this.DodajKarticuBtn.Text = "Dodaj Karticu";
            this.DodajKarticuBtn.UseVisualStyleBackColor = false;
            this.DodajKarticuBtn.Click += new System.EventHandler(this.DodajKarticuBtn_Click);
            // 
            // IzmeniKarticuBtn
            // 
            this.IzmeniKarticuBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzmeniKarticuBtn.Enabled = false;
            this.IzmeniKarticuBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzmeniKarticuBtn.Location = new System.Drawing.Point(10, 366);
            this.IzmeniKarticuBtn.Name = "IzmeniKarticuBtn";
            this.IzmeniKarticuBtn.Size = new System.Drawing.Size(200, 33);
            this.IzmeniKarticuBtn.TabIndex = 12;
            this.IzmeniKarticuBtn.Text = "Izmeni Karticu";
            this.IzmeniKarticuBtn.UseVisualStyleBackColor = false;
            this.IzmeniKarticuBtn.Click += new System.EventHandler(this.IzmeniKarticuBtn_Click);
            // 
            // IzbrisiKarticuBtn
            // 
            this.IzbrisiKarticuBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzbrisiKarticuBtn.Enabled = false;
            this.IzbrisiKarticuBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzbrisiKarticuBtn.Location = new System.Drawing.Point(10, 405);
            this.IzbrisiKarticuBtn.Name = "IzbrisiKarticuBtn";
            this.IzbrisiKarticuBtn.Size = new System.Drawing.Size(200, 33);
            this.IzbrisiKarticuBtn.TabIndex = 13;
            this.IzbrisiKarticuBtn.Text = "Izbrisi Karticu";
            this.IzbrisiKarticuBtn.UseVisualStyleBackColor = false;
            this.IzbrisiKarticuBtn.Click += new System.EventHandler(this.IzbrisiKarticuBtn_Click);
            // 
            // Form_Kartica_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(910, 453);
            this.Controls.Add(this.IzbrisiKarticuBtn);
            this.Controls.Add(this.IzmeniKarticuBtn);
            this.Controls.Add(this.DodajKarticuBtn);
            this.Controls.Add(this.KarticaGrid);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form_Kartica_Main";
            this.Text = "ATM - Kartice";
            this.Load += new System.EventHandler(this.Form_Kartica_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KarticaGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView KarticaGrid;
        private System.Windows.Forms.Button DodajKarticuBtn;
        private System.Windows.Forms.Button IzmeniKarticuBtn;
        private System.Windows.Forms.Button IzbrisiKarticuBtn;
    }
}