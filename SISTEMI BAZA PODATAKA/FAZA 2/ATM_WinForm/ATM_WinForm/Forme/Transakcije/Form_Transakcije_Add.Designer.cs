namespace ATM_WinForm.Forme.Transakcije
{
    partial class Form_Transakcije_Add
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
            this.KlijentComboBox = new System.Windows.Forms.ComboBox();
            this.DodajTransakcijuBtn = new System.Windows.Forms.Button();
            this.VremePodizanjanovcaLbl = new System.Windows.Forms.Label();
            this.DatumPodizanjaNovcaLbl = new System.Windows.Forms.Label();
            this.PodignutIznosTxtBx = new System.Windows.Forms.TextBox();
            this.PodignutIznosLbl = new System.Windows.Forms.Label();
            this.KlijentLbl = new System.Windows.Forms.Label();
            this.DatumPodizanjaNovcaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.VremeDatePicker = new System.Windows.Forms.DateTimePicker();
            this.KarticaComboBox = new System.Windows.Forms.ComboBox();
            this.KarticaLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // KlijentComboBox
            // 
            this.KlijentComboBox.Location = new System.Drawing.Point(75, 62);
            this.KlijentComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.KlijentComboBox.MaxDropDownItems = 50;
            this.KlijentComboBox.Name = "KlijentComboBox";
            this.KlijentComboBox.Size = new System.Drawing.Size(238, 21);
            this.KlijentComboBox.TabIndex = 28;
            this.KlijentComboBox.DropDownClosed += new System.EventHandler(this.KlijentComboBox_DropDownClosed);
            // 
            // DodajTransakcijuBtn
            // 
            this.DodajTransakcijuBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.DodajTransakcijuBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.DodajTransakcijuBtn.Location = new System.Drawing.Point(77, 377);
            this.DodajTransakcijuBtn.Name = "DodajTransakcijuBtn";
            this.DodajTransakcijuBtn.Size = new System.Drawing.Size(237, 28);
            this.DodajTransakcijuBtn.TabIndex = 27;
            this.DodajTransakcijuBtn.Text = "Dodaj Transakciju";
            this.DodajTransakcijuBtn.UseVisualStyleBackColor = false;
            this.DodajTransakcijuBtn.Click += new System.EventHandler(this.DodajTransakcijuBtn_Click);
            // 
            // VremePodizanjanovcaLbl
            // 
            this.VremePodizanjanovcaLbl.AutoSize = true;
            this.VremePodizanjanovcaLbl.Location = new System.Drawing.Point(73, 315);
            this.VremePodizanjanovcaLbl.Name = "VremePodizanjanovcaLbl";
            this.VremePodizanjanovcaLbl.Size = new System.Drawing.Size(118, 13);
            this.VremePodizanjanovcaLbl.TabIndex = 25;
            this.VremePodizanjanovcaLbl.Text = "Vreme podizanja novca";
            // 
            // DatumPodizanjaNovcaLbl
            // 
            this.DatumPodizanjaNovcaLbl.AutoSize = true;
            this.DatumPodizanjaNovcaLbl.Location = new System.Drawing.Point(73, 249);
            this.DatumPodizanjaNovcaLbl.Name = "DatumPodizanjaNovcaLbl";
            this.DatumPodizanjaNovcaLbl.Size = new System.Drawing.Size(119, 13);
            this.DatumPodizanjaNovcaLbl.TabIndex = 23;
            this.DatumPodizanjaNovcaLbl.Text = "Datum podizanja novca";
            // 
            // PodignutIznosTxtBx
            // 
            this.PodignutIznosTxtBx.Location = new System.Drawing.Point(76, 214);
            this.PodignutIznosTxtBx.Name = "PodignutIznosTxtBx";
            this.PodignutIznosTxtBx.Size = new System.Drawing.Size(237, 20);
            this.PodignutIznosTxtBx.TabIndex = 22;
            // 
            // PodignutIznosLbl
            // 
            this.PodignutIznosLbl.AutoSize = true;
            this.PodignutIznosLbl.Location = new System.Drawing.Point(75, 189);
            this.PodignutIznosLbl.Name = "PodignutIznosLbl";
            this.PodignutIznosLbl.Size = new System.Drawing.Size(76, 13);
            this.PodignutIznosLbl.TabIndex = 21;
            this.PodignutIznosLbl.Text = "Podignut iznos";
            // 
            // KlijentLbl
            // 
            this.KlijentLbl.AutoSize = true;
            this.KlijentLbl.Location = new System.Drawing.Point(73, 37);
            this.KlijentLbl.Name = "KlijentLbl";
            this.KlijentLbl.Size = new System.Drawing.Size(35, 13);
            this.KlijentLbl.TabIndex = 20;
            this.KlijentLbl.Text = "Klijent";
            // 
            // DatumPodizanjaNovcaDateTimePicker
            // 
            this.DatumPodizanjaNovcaDateTimePicker.Location = new System.Drawing.Point(77, 276);
            this.DatumPodizanjaNovcaDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.DatumPodizanjaNovcaDateTimePicker.Name = "DatumPodizanjaNovcaDateTimePicker";
            this.DatumPodizanjaNovcaDateTimePicker.Size = new System.Drawing.Size(237, 20);
            this.DatumPodizanjaNovcaDateTimePicker.TabIndex = 38;
            // 
            // VremeDatePicker
            // 
            this.VremeDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.VremeDatePicker.Location = new System.Drawing.Point(78, 339);
            this.VremeDatePicker.Margin = new System.Windows.Forms.Padding(2);
            this.VremeDatePicker.Name = "VremeDatePicker";
            this.VremeDatePicker.ShowUpDown = true;
            this.VremeDatePicker.Size = new System.Drawing.Size(237, 20);
            this.VremeDatePicker.TabIndex = 39;
            // 
            // KarticaComboBox
            // 
            this.KarticaComboBox.Location = new System.Drawing.Point(76, 134);
            this.KarticaComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.KarticaComboBox.MaxDropDownItems = 50;
            this.KarticaComboBox.Name = "KarticaComboBox";
            this.KarticaComboBox.Size = new System.Drawing.Size(238, 21);
            this.KarticaComboBox.TabIndex = 41;
            // 
            // KarticaLbl
            // 
            this.KarticaLbl.AutoSize = true;
            this.KarticaLbl.Location = new System.Drawing.Point(75, 108);
            this.KarticaLbl.Name = "KarticaLbl";
            this.KarticaLbl.Size = new System.Drawing.Size(40, 13);
            this.KarticaLbl.TabIndex = 40;
            this.KarticaLbl.Text = "Kartica";
            // 
            // Form_Transakcije_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(388, 440);
            this.Controls.Add(this.KarticaComboBox);
            this.Controls.Add(this.KarticaLbl);
            this.Controls.Add(this.VremeDatePicker);
            this.Controls.Add(this.DatumPodizanjaNovcaDateTimePicker);
            this.Controls.Add(this.KlijentComboBox);
            this.Controls.Add(this.DodajTransakcijuBtn);
            this.Controls.Add(this.VremePodizanjanovcaLbl);
            this.Controls.Add(this.DatumPodizanjaNovcaLbl);
            this.Controls.Add(this.PodignutIznosTxtBx);
            this.Controls.Add(this.PodignutIznosLbl);
            this.Controls.Add(this.KlijentLbl);
            this.Name = "Form_Transakcije_Add";
            this.Text = "Form_Transakcije_Add";
            this.Load += new System.EventHandler(this.Form_Transakcije_Add_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox KlijentComboBox;
        private System.Windows.Forms.Button DodajTransakcijuBtn;
        private System.Windows.Forms.Label VremePodizanjanovcaLbl;
        private System.Windows.Forms.Label DatumPodizanjaNovcaLbl;
        private System.Windows.Forms.TextBox PodignutIznosTxtBx;
        private System.Windows.Forms.Label PodignutIznosLbl;
        private System.Windows.Forms.Label KlijentLbl;
        private System.Windows.Forms.DateTimePicker DatumPodizanjaNovcaDateTimePicker;
        private System.Windows.Forms.DateTimePicker VremeDatePicker;
        private System.Windows.Forms.ComboBox KarticaComboBox;
        private System.Windows.Forms.Label KarticaLbl;
    }
}