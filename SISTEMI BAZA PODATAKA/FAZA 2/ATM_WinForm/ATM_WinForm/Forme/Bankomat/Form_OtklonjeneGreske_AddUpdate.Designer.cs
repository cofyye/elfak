namespace ATM_WinForm.Forme.Bankomat
{
    partial class Form_OtklonjeneGreske_AddUpdate
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
            this.GreskaLbl = new System.Windows.Forms.Label();
            this.AkcijaGreskaBtn = new System.Windows.Forms.Button();
            this.AddUpdateTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GreskaLbl
            // 
            this.GreskaLbl.AutoSize = true;
            this.GreskaLbl.Location = new System.Drawing.Point(144, 50);
            this.GreskaLbl.Name = "GreskaLbl";
            this.GreskaLbl.Size = new System.Drawing.Size(53, 15);
            this.GreskaLbl.TabIndex = 12;
            this.GreskaLbl.Text = "Greska";
            // 
            // AkcijaGreskaBtn
            // 
            this.AkcijaGreskaBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.AkcijaGreskaBtn.Location = new System.Drawing.Point(148, 123);
            this.AkcijaGreskaBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AkcijaGreskaBtn.Name = "AkcijaGreskaBtn";
            this.AkcijaGreskaBtn.Size = new System.Drawing.Size(211, 36);
            this.AkcijaGreskaBtn.TabIndex = 11;
            this.AkcijaGreskaBtn.Text = "button1";
            this.AkcijaGreskaBtn.UseVisualStyleBackColor = false;
            this.AkcijaGreskaBtn.Click += new System.EventHandler(this.AkcijaGreskaBtn_Click);
            // 
            // AddUpdateTxt
            // 
            this.AddUpdateTxt.Location = new System.Drawing.Point(148, 77);
            this.AddUpdateTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddUpdateTxt.Name = "AddUpdateTxt";
            this.AddUpdateTxt.Size = new System.Drawing.Size(209, 21);
            this.AddUpdateTxt.TabIndex = 10;
            // 
            // Form_OtklonjeneGreske_AddUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(525, 217);
            this.Controls.Add(this.GreskaLbl);
            this.Controls.Add(this.AkcijaGreskaBtn);
            this.Controls.Add(this.AddUpdateTxt);
            this.Font = new System.Drawing.Font("Georgia", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form_OtklonjeneGreske_AddUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_OtklonjeneGreske_AddUpdate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GreskaLbl;
        private System.Windows.Forms.Button AkcijaGreskaBtn;
        private System.Windows.Forms.TextBox AddUpdateTxt;
    }
}