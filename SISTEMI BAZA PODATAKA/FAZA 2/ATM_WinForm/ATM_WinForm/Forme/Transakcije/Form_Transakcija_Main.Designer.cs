namespace ATM_WinForm.Forme.Transakcije
{
    partial class Form_Transakcija_Main
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
            this.TransakcijaGrid = new System.Windows.Forms.DataGridView();
            this.DodajTransakcijuBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TransakcijaGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TransakcijaGrid
            // 
            this.TransakcijaGrid.AllowUserToOrderColumns = true;
            this.TransakcijaGrid.AllowUserToResizeRows = false;
            this.TransakcijaGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TransakcijaGrid.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.TransakcijaGrid.ColumnHeadersHeight = 29;
            this.TransakcijaGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TransakcijaGrid.Location = new System.Drawing.Point(12, 12);
            this.TransakcijaGrid.MultiSelect = false;
            this.TransakcijaGrid.Name = "TransakcijaGrid";
            this.TransakcijaGrid.ReadOnly = true;
            this.TransakcijaGrid.RowHeadersWidth = 51;
            this.TransakcijaGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.TransakcijaGrid.Size = new System.Drawing.Size(890, 309);
            this.TransakcijaGrid.TabIndex = 10;
            // 
            // DodajTransakcijuBtn
            // 
            this.DodajTransakcijuBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.DodajTransakcijuBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.DodajTransakcijuBtn.Location = new System.Drawing.Point(12, 327);
            this.DodajTransakcijuBtn.Name = "DodajTransakcijuBtn";
            this.DodajTransakcijuBtn.Size = new System.Drawing.Size(222, 38);
            this.DodajTransakcijuBtn.TabIndex = 11;
            this.DodajTransakcijuBtn.Text = "Dodaj Transakciju";
            this.DodajTransakcijuBtn.UseVisualStyleBackColor = false;
            this.DodajTransakcijuBtn.Click += new System.EventHandler(this.DodajTransakcijuBtn_Click);
            // 
            // Form_Transakcija_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(918, 461);
            this.Controls.Add(this.DodajTransakcijuBtn);
            this.Controls.Add(this.TransakcijaGrid);
            this.Name = "Form_Transakcija_Main";
            this.Text = "Form_Transakcija_Main";
            this.Load += new System.EventHandler(this.Form_Transakcija_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TransakcijaGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TransakcijaGrid;
        private System.Windows.Forms.Button DodajTransakcijuBtn;
    }
}