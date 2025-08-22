namespace ATM_WinForm
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SectionBankeBtn = new System.Windows.Forms.Button();
            this.SectionKlijentiBtn = new System.Windows.Forms.Button();
            this.SectionBankomatiBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SectionBankeBtn
            // 
            this.SectionBankeBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.SectionBankeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.SectionBankeBtn.Location = new System.Drawing.Point(102, 311);
            this.SectionBankeBtn.Name = "SectionBankeBtn";
            this.SectionBankeBtn.Size = new System.Drawing.Size(227, 57);
            this.SectionBankeBtn.TabIndex = 4;
            this.SectionBankeBtn.Text = "Banke";
            this.SectionBankeBtn.UseVisualStyleBackColor = false;
            this.SectionBankeBtn.Click += new System.EventHandler(this.SectionBanka_Click);
            // 
            // SectionKlijentiBtn
            // 
            this.SectionKlijentiBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.SectionKlijentiBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.SectionKlijentiBtn.Location = new System.Drawing.Point(102, 437);
            this.SectionKlijentiBtn.Name = "SectionKlijentiBtn";
            this.SectionKlijentiBtn.Size = new System.Drawing.Size(227, 57);
            this.SectionKlijentiBtn.TabIndex = 10;
            this.SectionKlijentiBtn.Text = "Klijenti";
            this.SectionKlijentiBtn.UseVisualStyleBackColor = false;
            this.SectionKlijentiBtn.Click += new System.EventHandler(this.SectionKlijentiBtn_Click);
            // 
            // SectionBankomatiBtn
            // 
            this.SectionBankomatiBtn.BackColor = System.Drawing.Color.SandyBrown;
            this.SectionBankomatiBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.SectionBankomatiBtn.Location = new System.Drawing.Point(102, 374);
            this.SectionBankomatiBtn.Name = "SectionBankomatiBtn";
            this.SectionBankomatiBtn.Size = new System.Drawing.Size(227, 57);
            this.SectionBankomatiBtn.TabIndex = 11;
            this.SectionBankomatiBtn.Text = "Bankomati";
            this.SectionBankomatiBtn.UseVisualStyleBackColor = false;
            this.SectionBankomatiBtn.Click += new System.EventHandler(this.SectionBankomatiBtn_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 74);
            this.label1.TabIndex = 12;
            this.label1.Text = "ATM Bankomati";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(102, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(228, 197);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(434, 533);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SectionBankomatiBtn);
            this.Controls.Add(this.SectionKlijentiBtn);
            this.Controls.Add(this.SectionBankeBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - Glavna";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button SectionBankeBtn;
        private System.Windows.Forms.Button SectionKlijentiBtn;
        private System.Windows.Forms.Button SectionBankomatiBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

