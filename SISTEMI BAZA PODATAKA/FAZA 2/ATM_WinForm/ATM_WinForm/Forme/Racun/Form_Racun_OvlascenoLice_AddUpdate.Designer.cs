namespace ATM_WinForm.Forme.Racun
{
    partial class Form_Racun_OvlascenoLice_AddUpdate
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
            this.labelaAddEditLBL = new System.Windows.Forms.Label();
            this.AddUpdateTxt = new System.Windows.Forms.TextBox();
            this.AkcijaOvlascenoLiceBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelaAddEditLBL
            // 
            this.labelaAddEditLBL.AutoSize = true;
            this.labelaAddEditLBL.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.labelaAddEditLBL.Location = new System.Drawing.Point(47, 64);
            this.labelaAddEditLBL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelaAddEditLBL.Name = "labelaAddEditLBL";
            this.labelaAddEditLBL.Size = new System.Drawing.Size(44, 15);
            this.labelaAddEditLBL.TabIndex = 4;
            this.labelaAddEditLBL.Text = "label1";
            // 
            // AddUpdateTxt
            // 
            this.AddUpdateTxt.Location = new System.Drawing.Point(50, 88);
            this.AddUpdateTxt.Margin = new System.Windows.Forms.Padding(2);
            this.AddUpdateTxt.Name = "AddUpdateTxt";
            this.AddUpdateTxt.Size = new System.Drawing.Size(158, 20);
            this.AddUpdateTxt.TabIndex = 5;
            // 
            // AkcijaOvlascenoLiceBtn
            // 
            this.AkcijaOvlascenoLiceBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.AkcijaOvlascenoLiceBtn.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.AkcijaOvlascenoLiceBtn.Location = new System.Drawing.Point(50, 126);
            this.AkcijaOvlascenoLiceBtn.Margin = new System.Windows.Forms.Padding(2);
            this.AkcijaOvlascenoLiceBtn.Name = "AkcijaOvlascenoLiceBtn";
            this.AkcijaOvlascenoLiceBtn.Size = new System.Drawing.Size(158, 46);
            this.AkcijaOvlascenoLiceBtn.TabIndex = 6;
            this.AkcijaOvlascenoLiceBtn.Text = "button1";
            this.AkcijaOvlascenoLiceBtn.UseVisualStyleBackColor = false;
            this.AkcijaOvlascenoLiceBtn.Click += new System.EventHandler(this.AkcijaOvlascenoLiceBtn_Click);
            // 
            // Form_Racun_OvlascenoLice_AddUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(272, 271);
            this.Controls.Add(this.AkcijaOvlascenoLiceBtn);
            this.Controls.Add(this.AddUpdateTxt);
            this.Controls.Add(this.labelaAddEditLBL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form_Racun_OvlascenoLice_AddUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Ovlascena Lica";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelaAddEditLBL;
        private System.Windows.Forms.TextBox AddUpdateTxt;
        private System.Windows.Forms.Button AkcijaOvlascenoLiceBtn;
    }
}