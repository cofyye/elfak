namespace ATM_WinForm.Forme.Bankomat
{
    partial class Form_Bankomat_AddUpdate
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
            this.FilijalaComboBox = new System.Windows.Forms.ComboBox();
            this.AkcijaBankomataBtn = new System.Windows.Forms.Button();
            this.DatumPoslDogLbl = new System.Windows.Forms.Label();
            this.ProizvodjacTxtBx = new System.Windows.Forms.TextBox();
            this.ProizvodjacLbl = new System.Windows.Forms.Label();
            this.LokacijaTxtBx = new System.Windows.Forms.TextBox();
            this.LokacijaLbl = new System.Windows.Forms.Label();
            this.FilijalaLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StatusComboBox = new System.Windows.Forms.ComboBox();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // FilijalaComboBox
            // 
            this.FilijalaComboBox.Location = new System.Drawing.Point(77, 53);
            this.FilijalaComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.FilijalaComboBox.MaxDropDownItems = 50;
            this.FilijalaComboBox.Name = "FilijalaComboBox";
            this.FilijalaComboBox.Size = new System.Drawing.Size(238, 21);
            this.FilijalaComboBox.TabIndex = 28;
            // 
            // AkcijaBankomataBtn
            // 
            this.AkcijaBankomataBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.AkcijaBankomataBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.AkcijaBankomataBtn.Location = new System.Drawing.Point(78, 380);
            this.AkcijaBankomataBtn.Name = "AkcijaBankomataBtn";
            this.AkcijaBankomataBtn.Size = new System.Drawing.Size(237, 28);
            this.AkcijaBankomataBtn.TabIndex = 27;
            this.AkcijaBankomataBtn.Text = "Dodaj Bankomat";
            this.AkcijaBankomataBtn.UseVisualStyleBackColor = false;
            this.AkcijaBankomataBtn.Click += new System.EventHandler(this.AkcijaBankomataBtn_Click);
            // 
            // DatumPoslDogLbl
            // 
            this.DatumPoslDogLbl.AutoSize = true;
            this.DatumPoslDogLbl.Location = new System.Drawing.Point(74, 230);
            this.DatumPoslDogLbl.Name = "DatumPoslDogLbl";
            this.DatumPoslDogLbl.Size = new System.Drawing.Size(147, 13);
            this.DatumPoslDogLbl.TabIndex = 25;
            this.DatumPoslDogLbl.Text = "Datum poslednjeg servisiranja";
            // 
            // ProizvodjacTxtBx
            // 
            this.ProizvodjacTxtBx.Location = new System.Drawing.Point(77, 185);
            this.ProizvodjacTxtBx.Name = "ProizvodjacTxtBx";
            this.ProizvodjacTxtBx.Size = new System.Drawing.Size(237, 20);
            this.ProizvodjacTxtBx.TabIndex = 24;
            // 
            // ProizvodjacLbl
            // 
            this.ProizvodjacLbl.AutoSize = true;
            this.ProizvodjacLbl.Location = new System.Drawing.Point(74, 159);
            this.ProizvodjacLbl.Name = "ProizvodjacLbl";
            this.ProizvodjacLbl.Size = new System.Drawing.Size(62, 13);
            this.ProizvodjacLbl.TabIndex = 23;
            this.ProizvodjacLbl.Text = "Proizvodjac";
            // 
            // LokacijaTxtBx
            // 
            this.LokacijaTxtBx.Location = new System.Drawing.Point(77, 117);
            this.LokacijaTxtBx.Name = "LokacijaTxtBx";
            this.LokacijaTxtBx.Size = new System.Drawing.Size(237, 20);
            this.LokacijaTxtBx.TabIndex = 22;
            // 
            // LokacijaLbl
            // 
            this.LokacijaLbl.AutoSize = true;
            this.LokacijaLbl.Location = new System.Drawing.Point(74, 91);
            this.LokacijaLbl.Name = "LokacijaLbl";
            this.LokacijaLbl.Size = new System.Drawing.Size(47, 13);
            this.LokacijaLbl.TabIndex = 21;
            this.LokacijaLbl.Text = "Lokacija";
            // 
            // FilijalaLbl
            // 
            this.FilijalaLbl.AutoSize = true;
            this.FilijalaLbl.Location = new System.Drawing.Point(74, 27);
            this.FilijalaLbl.Name = "FilijalaLbl";
            this.FilijalaLbl.Size = new System.Drawing.Size(35, 13);
            this.FilijalaLbl.TabIndex = 20;
            this.FilijalaLbl.Text = "Filijala";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Status";
            // 
            // StatusComboBox
            // 
            this.StatusComboBox.Items.AddRange(new object[] {
            "operativan",
            "neoperativan"});
            this.StatusComboBox.Location = new System.Drawing.Point(78, 327);
            this.StatusComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.StatusComboBox.MaxDropDownItems = 50;
            this.StatusComboBox.Name = "StatusComboBox";
            this.StatusComboBox.Size = new System.Drawing.Size(238, 21);
            this.StatusComboBox.TabIndex = 31;
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.Location = new System.Drawing.Point(77, 256);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(238, 20);
            this.DateTimePicker.TabIndex = 32;
            // 
            // Form_Bankomat_AddUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(388, 434);
            this.Controls.Add(this.DateTimePicker);
            this.Controls.Add(this.StatusComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilijalaComboBox);
            this.Controls.Add(this.AkcijaBankomataBtn);
            this.Controls.Add(this.DatumPoslDogLbl);
            this.Controls.Add(this.ProizvodjacTxtBx);
            this.Controls.Add(this.ProizvodjacLbl);
            this.Controls.Add(this.LokacijaTxtBx);
            this.Controls.Add(this.LokacijaLbl);
            this.Controls.Add(this.FilijalaLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_Bankomat_AddUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Dodaj Bankomat";
            this.Load += new System.EventHandler(this.Form_Bankomat_AddUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox FilijalaComboBox;
        private System.Windows.Forms.Button AkcijaBankomataBtn;
        private System.Windows.Forms.Label DatumPoslDogLbl;
        private System.Windows.Forms.TextBox ProizvodjacTxtBx;
        private System.Windows.Forms.Label ProizvodjacLbl;
        private System.Windows.Forms.TextBox LokacijaTxtBx;
        private System.Windows.Forms.Label LokacijaLbl;
        private System.Windows.Forms.Label FilijalaLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox StatusComboBox;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
    }
}