namespace ATM_WinForm.Forme.Kartica
{
    partial class Form_Kartica_AddUpdate
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
            this.AkcijaKarticeBtn = new System.Windows.Forms.Button();
            this.DnevniLimitTxtBx = new System.Windows.Forms.TextBox();
            this.RadnoVremeLbl = new System.Windows.Forms.Label();
            this.DatumIstekaLbl = new System.Windows.Forms.Label();
            this.DatumIzdavanjaLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TipComboBox = new System.Windows.Forms.ComboBox();
            this.MaxIznosZaduzenjaLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MaxIznosZaduzenjaTxtBx = new System.Windows.Forms.TextBox();
            this.DatumIstekaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.MaxDatumVracanjaDugaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DatumIzdavanjaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // AkcijaKarticeBtn
            // 
            this.AkcijaKarticeBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.AkcijaKarticeBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.AkcijaKarticeBtn.Location = new System.Drawing.Point(74, 333);
            this.AkcijaKarticeBtn.Name = "AkcijaKarticeBtn";
            this.AkcijaKarticeBtn.Size = new System.Drawing.Size(237, 28);
            this.AkcijaKarticeBtn.TabIndex = 27;
            this.AkcijaKarticeBtn.UseVisualStyleBackColor = false;
            this.AkcijaKarticeBtn.Click += new System.EventHandler(this.AkcijaKarticeBtn_Click);
            // 
            // DnevniLimitTxtBx
            // 
            this.DnevniLimitTxtBx.Location = new System.Drawing.Point(77, 138);
            this.DnevniLimitTxtBx.Name = "DnevniLimitTxtBx";
            this.DnevniLimitTxtBx.Size = new System.Drawing.Size(237, 20);
            this.DnevniLimitTxtBx.TabIndex = 26;
            // 
            // RadnoVremeLbl
            // 
            this.RadnoVremeLbl.AutoSize = true;
            this.RadnoVremeLbl.Location = new System.Drawing.Point(75, 122);
            this.RadnoVremeLbl.Name = "RadnoVremeLbl";
            this.RadnoVremeLbl.Size = new System.Drawing.Size(61, 13);
            this.RadnoVremeLbl.TabIndex = 25;
            this.RadnoVremeLbl.Text = "Dnevni limit";
            // 
            // DatumIstekaLbl
            // 
            this.DatumIstekaLbl.AutoSize = true;
            this.DatumIstekaLbl.Location = new System.Drawing.Point(74, 65);
            this.DatumIstekaLbl.Name = "DatumIstekaLbl";
            this.DatumIstekaLbl.Size = new System.Drawing.Size(69, 13);
            this.DatumIstekaLbl.TabIndex = 23;
            this.DatumIstekaLbl.Text = "Datum isteka";
            // 
            // DatumIzdavanjaLbl
            // 
            this.DatumIzdavanjaLbl.AutoSize = true;
            this.DatumIzdavanjaLbl.Location = new System.Drawing.Point(73, 18);
            this.DatumIzdavanjaLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DatumIzdavanjaLbl.Name = "DatumIzdavanjaLbl";
            this.DatumIzdavanjaLbl.Size = new System.Drawing.Size(86, 13);
            this.DatumIzdavanjaLbl.TabIndex = 29;
            this.DatumIzdavanjaLbl.Text = "Datum izdavanja";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 168);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Tip";
            // 
            // TipComboBox
            // 
            this.TipComboBox.Items.AddRange(new object[] {
            "kreditna",
            "debitna"});
            this.TipComboBox.Location = new System.Drawing.Point(77, 191);
            this.TipComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.TipComboBox.MaxDropDownItems = 50;
            this.TipComboBox.Name = "TipComboBox";
            this.TipComboBox.Size = new System.Drawing.Size(238, 21);
            this.TipComboBox.TabIndex = 31;
            this.TipComboBox.SelectedIndexChanged += new System.EventHandler(this.TipComboBox_SelectedIndexChanged);
            // 
            // MaxIznosZaduzenjaLbl
            // 
            this.MaxIznosZaduzenjaLbl.AutoSize = true;
            this.MaxIznosZaduzenjaLbl.Location = new System.Drawing.Point(74, 224);
            this.MaxIznosZaduzenjaLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MaxIznosZaduzenjaLbl.Name = "MaxIznosZaduzenjaLbl";
            this.MaxIznosZaduzenjaLbl.Size = new System.Drawing.Size(141, 13);
            this.MaxIznosZaduzenjaLbl.TabIndex = 32;
            this.MaxIznosZaduzenjaLbl.Text = "Maksimalan iznos zaduzenja";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 273);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Maksimalan datum vracanja duga";
            // 
            // MaxIznosZaduzenjaTxtBx
            // 
            this.MaxIznosZaduzenjaTxtBx.Location = new System.Drawing.Point(77, 240);
            this.MaxIznosZaduzenjaTxtBx.Name = "MaxIznosZaduzenjaTxtBx";
            this.MaxIznosZaduzenjaTxtBx.Size = new System.Drawing.Size(237, 20);
            this.MaxIznosZaduzenjaTxtBx.TabIndex = 34;
            // 
            // DatumIstekaDateTimePicker
            // 
            this.DatumIstekaDateTimePicker.Location = new System.Drawing.Point(75, 93);
            this.DatumIstekaDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.DatumIstekaDateTimePicker.Name = "DatumIstekaDateTimePicker";
            this.DatumIstekaDateTimePicker.Size = new System.Drawing.Size(235, 20);
            this.DatumIstekaDateTimePicker.TabIndex = 35;
            // 
            // MaxDatumVracanjaDugaDateTimePicker
            // 
            this.MaxDatumVracanjaDugaDateTimePicker.Location = new System.Drawing.Point(77, 288);
            this.MaxDatumVracanjaDugaDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.MaxDatumVracanjaDugaDateTimePicker.Name = "MaxDatumVracanjaDugaDateTimePicker";
            this.MaxDatumVracanjaDugaDateTimePicker.Size = new System.Drawing.Size(238, 20);
            this.MaxDatumVracanjaDugaDateTimePicker.TabIndex = 36;
            // 
            // DatumIzdavanjaDateTimePicker
            // 
            this.DatumIzdavanjaDateTimePicker.Location = new System.Drawing.Point(74, 33);
            this.DatumIzdavanjaDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.DatumIzdavanjaDateTimePicker.Name = "DatumIzdavanjaDateTimePicker";
            this.DatumIzdavanjaDateTimePicker.Size = new System.Drawing.Size(233, 20);
            this.DatumIzdavanjaDateTimePicker.TabIndex = 37;
            // 
            // Form_Kartica_AddUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(387, 396);
            this.Controls.Add(this.DatumIzdavanjaDateTimePicker);
            this.Controls.Add(this.MaxDatumVracanjaDugaDateTimePicker);
            this.Controls.Add(this.DatumIstekaDateTimePicker);
            this.Controls.Add(this.MaxIznosZaduzenjaTxtBx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MaxIznosZaduzenjaLbl);
            this.Controls.Add(this.TipComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DatumIzdavanjaLbl);
            this.Controls.Add(this.AkcijaKarticeBtn);
            this.Controls.Add(this.DnevniLimitTxtBx);
            this.Controls.Add(this.RadnoVremeLbl);
            this.Controls.Add(this.DatumIstekaLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form_Kartica_AddUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Kartica_AddUpdate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AkcijaKarticeBtn;
        private System.Windows.Forms.TextBox DnevniLimitTxtBx;
        private System.Windows.Forms.Label RadnoVremeLbl;
        private System.Windows.Forms.Label DatumIstekaLbl;
        private System.Windows.Forms.Label DatumIzdavanjaLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TipComboBox;
        private System.Windows.Forms.Label MaxIznosZaduzenjaLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MaxIznosZaduzenjaTxtBx;
        private System.Windows.Forms.DateTimePicker DatumIstekaDateTimePicker;
        private System.Windows.Forms.DateTimePicker MaxDatumVracanjaDugaDateTimePicker;
        private System.Windows.Forms.DateTimePicker DatumIzdavanjaDateTimePicker;
    }
}