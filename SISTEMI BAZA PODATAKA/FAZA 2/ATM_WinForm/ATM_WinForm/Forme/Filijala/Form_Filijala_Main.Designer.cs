namespace ATM_WinForm
{
    partial class Form_Filijala_Main
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
            this.FilijalaGrid = new System.Windows.Forms.DataGridView();
            this.DodajFilijaluBtn = new System.Windows.Forms.Button();
            this.IzmeniFilijaluBtn = new System.Windows.Forms.Button();
            this.IzbrisiFilijaluBtn = new System.Windows.Forms.Button();
            this.PrikaziBankomateBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FilijalaGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // FilijalaGrid
            // 
            this.FilijalaGrid.AllowUserToOrderColumns = true;
            this.FilijalaGrid.AllowUserToResizeRows = false;
            this.FilijalaGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FilijalaGrid.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.FilijalaGrid.ColumnHeadersHeight = 29;
            this.FilijalaGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.FilijalaGrid.Location = new System.Drawing.Point(10, 11);
            this.FilijalaGrid.MultiSelect = false;
            this.FilijalaGrid.Name = "FilijalaGrid";
            this.FilijalaGrid.ReadOnly = true;
            this.FilijalaGrid.RowHeadersWidth = 51;
            this.FilijalaGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.FilijalaGrid.Size = new System.Drawing.Size(890, 309);
            this.FilijalaGrid.TabIndex = 5;
            this.FilijalaGrid.SelectionChanged += new System.EventHandler(this.FilijalaGrid_SelectionChanged);
            // 
            // DodajFilijaluBtn
            // 
            this.DodajFilijaluBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.DodajFilijaluBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.DodajFilijaluBtn.Location = new System.Drawing.Point(10, 326);
            this.DodajFilijaluBtn.Name = "DodajFilijaluBtn";
            this.DodajFilijaluBtn.Size = new System.Drawing.Size(200, 33);
            this.DodajFilijaluBtn.TabIndex = 6;
            this.DodajFilijaluBtn.Text = "Dodaj Filijalu";
            this.DodajFilijaluBtn.UseVisualStyleBackColor = false;
            this.DodajFilijaluBtn.Click += new System.EventHandler(this.DodajFilijaluBtn_Click);
            // 
            // IzmeniFilijaluBtn
            // 
            this.IzmeniFilijaluBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzmeniFilijaluBtn.Enabled = false;
            this.IzmeniFilijaluBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzmeniFilijaluBtn.Location = new System.Drawing.Point(10, 366);
            this.IzmeniFilijaluBtn.Name = "IzmeniFilijaluBtn";
            this.IzmeniFilijaluBtn.Size = new System.Drawing.Size(200, 33);
            this.IzmeniFilijaluBtn.TabIndex = 7;
            this.IzmeniFilijaluBtn.Text = "Izmeni Filijalu";
            this.IzmeniFilijaluBtn.UseVisualStyleBackColor = false;
            this.IzmeniFilijaluBtn.Click += new System.EventHandler(this.IzmeniFilijaluBtn_Click);
            // 
            // IzbrisiFilijaluBtn
            // 
            this.IzbrisiFilijaluBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.IzbrisiFilijaluBtn.Enabled = false;
            this.IzbrisiFilijaluBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.IzbrisiFilijaluBtn.Location = new System.Drawing.Point(10, 405);
            this.IzbrisiFilijaluBtn.Name = "IzbrisiFilijaluBtn";
            this.IzbrisiFilijaluBtn.Size = new System.Drawing.Size(200, 33);
            this.IzbrisiFilijaluBtn.TabIndex = 8;
            this.IzbrisiFilijaluBtn.Text = "Izbrisi Filijalu";
            this.IzbrisiFilijaluBtn.UseVisualStyleBackColor = false;
            this.IzbrisiFilijaluBtn.Click += new System.EventHandler(this.IzbrisiFilijaluBtn_Click);
            // 
            // PrikaziBankomateBtn
            // 
            this.PrikaziBankomateBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.PrikaziBankomateBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.PrikaziBankomateBtn.Location = new System.Drawing.Point(700, 326);
            this.PrikaziBankomateBtn.Name = "PrikaziBankomateBtn";
            this.PrikaziBankomateBtn.Size = new System.Drawing.Size(200, 33);
            this.PrikaziBankomateBtn.TabIndex = 9;
            this.PrikaziBankomateBtn.Text = "Prikazi Bankomate";
            this.PrikaziBankomateBtn.UseVisualStyleBackColor = false;
            this.PrikaziBankomateBtn.Click += new System.EventHandler(this.PrikaziBankomateBtn_Click);
            // 
            // Form_Filijala_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(910, 453);
            this.Controls.Add(this.PrikaziBankomateBtn);
            this.Controls.Add(this.IzbrisiFilijaluBtn);
            this.Controls.Add(this.IzmeniFilijaluBtn);
            this.Controls.Add(this.DodajFilijaluBtn);
            this.Controls.Add(this.FilijalaGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form_Filijala_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM -  Filijale";
            this.Load += new System.EventHandler(this.Form_Filijala_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FilijalaGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView FilijalaGrid;
        private System.Windows.Forms.Button DodajFilijaluBtn;
        private System.Windows.Forms.Button IzmeniFilijaluBtn;
        private System.Windows.Forms.Button IzbrisiFilijaluBtn;
        private System.Windows.Forms.Button PrikaziBankomateBtn;
    }
}