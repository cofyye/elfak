namespace ATM_WinForm.Forme.Banka
{
    partial class Form_BankaBrTelefona_AddUpdate
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
            this.AddUpdateTxt = new System.Windows.Forms.TextBox();
            this.AkcijaBrTelefonaBtn = new System.Windows.Forms.Button();
            this.labelaAddEditLBL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddUpdateTxt
            // 
            this.AddUpdateTxt.Location = new System.Drawing.Point(56, 109);
            this.AddUpdateTxt.Margin = new System.Windows.Forms.Padding(2);
            this.AddUpdateTxt.Name = "AddUpdateTxt";
            this.AddUpdateTxt.Size = new System.Drawing.Size(158, 20);
            this.AddUpdateTxt.TabIndex = 1;
            // 
            // AkcijaBrTelefonaBtn
            // 
            this.AkcijaBrTelefonaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.AkcijaBrTelefonaBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.AkcijaBrTelefonaBtn.Location = new System.Drawing.Point(56, 143);
            this.AkcijaBrTelefonaBtn.Margin = new System.Windows.Forms.Padding(2);
            this.AkcijaBrTelefonaBtn.Name = "AkcijaBrTelefonaBtn";
            this.AkcijaBrTelefonaBtn.Size = new System.Drawing.Size(158, 36);
            this.AkcijaBrTelefonaBtn.TabIndex = 2;
            this.AkcijaBrTelefonaBtn.Text = "button1";
            this.AkcijaBrTelefonaBtn.UseVisualStyleBackColor = false;
            this.AkcijaBrTelefonaBtn.Click += new System.EventHandler(this.AkcijaBrTelefonaBtn_Click);
            // 
            // labelaAddEditLBL
            // 
            this.labelaAddEditLBL.AutoSize = true;
            this.labelaAddEditLBL.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.labelaAddEditLBL.Location = new System.Drawing.Point(53, 84);
            this.labelaAddEditLBL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelaAddEditLBL.Name = "labelaAddEditLBL";
            this.labelaAddEditLBL.Size = new System.Drawing.Size(44, 15);
            this.labelaAddEditLBL.TabIndex = 3;
            this.labelaAddEditLBL.Text = "label1";
            // 
            // Form_BankaBrTelefona_AddUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(272, 271);
            this.Controls.Add(this.labelaAddEditLBL);
            this.Controls.Add(this.AkcijaBrTelefonaBtn);
            this.Controls.Add(this.AddUpdateTxt);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_BankaBrTelefona_AddUpdate";
            this.Text = "ATM - Broj Telefona Banke";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox AddUpdateTxt;
        private System.Windows.Forms.Button AkcijaBrTelefonaBtn;
        private System.Windows.Forms.Label labelaAddEditLBL;
    }
}