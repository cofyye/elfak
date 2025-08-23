namespace MatrixMixer
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
            this.convert_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.findBtn = new System.Windows.Forms.Button();
            this.loadBtn = new System.Windows.Forms.Button();
            this.btnGaus = new System.Windows.Forms.Button();
            this.numericUpDownGaus = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBlackLight = new System.Windows.Forms.NumericUpDown();
            this.btnBlack = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDownMean = new System.Windows.Forms.NumericUpDown();
            this.Undo = new System.Windows.Forms.Button();
            this.Redo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGaus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlackLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMean)).BeginInit();
            this.SuspendLayout();
            // 
            // convert_btn
            // 
            this.convert_btn.Location = new System.Drawing.Point(1006, 98);
            this.convert_btn.Name = "convert_btn";
            this.convert_btn.Size = new System.Drawing.Size(118, 41);
            this.convert_btn.TabIndex = 1;
            this.convert_btn.Text = "save";
            this.convert_btn.UseVisualStyleBackColor = true;
            this.convert_btn.Click += new System.EventHandler(this.convert_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(55, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(927, 346);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // findBtn
            // 
            this.findBtn.Location = new System.Drawing.Point(1006, 43);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(118, 40);
            this.findBtn.TabIndex = 2;
            this.findBtn.Text = "load normal";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(1006, 145);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(118, 41);
            this.loadBtn.TabIndex = 3;
            this.loadBtn.Text = "load myimg";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // btnGaus
            // 
            this.btnGaus.Location = new System.Drawing.Point(1006, 192);
            this.btnGaus.Name = "btnGaus";
            this.btnGaus.Size = new System.Drawing.Size(118, 41);
            this.btnGaus.TabIndex = 4;
            this.btnGaus.Text = "gaus";
            this.btnGaus.UseVisualStyleBackColor = true;
            this.btnGaus.Click += new System.EventHandler(this.btnGaus_Click);
            // 
            // numericUpDownGaus
            // 
            this.numericUpDownGaus.Location = new System.Drawing.Point(1130, 202);
            this.numericUpDownGaus.Name = "numericUpDownGaus";
            this.numericUpDownGaus.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownGaus.TabIndex = 6;
            // 
            // numericUpDownBlackLight
            // 
            this.numericUpDownBlackLight.Location = new System.Drawing.Point(1130, 252);
            this.numericUpDownBlackLight.Name = "numericUpDownBlackLight";
            this.numericUpDownBlackLight.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownBlackLight.TabIndex = 7;
            // 
            // btnBlack
            // 
            this.btnBlack.Location = new System.Drawing.Point(1006, 239);
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(118, 46);
            this.btnBlack.TabIndex = 8;
            this.btnBlack.Text = "blackLight";
            this.btnBlack.UseVisualStyleBackColor = true;
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1006, 291);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 45);
            this.button1.TabIndex = 9;
            this.button1.Text = "meanRemoval";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDownMean
            // 
            this.numericUpDownMean.Location = new System.Drawing.Point(1130, 303);
            this.numericUpDownMean.Name = "numericUpDownMean";
            this.numericUpDownMean.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownMean.TabIndex = 10;
            // 
            // Undo
            // 
            this.Undo.Location = new System.Drawing.Point(55, 420);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(430, 45);
            this.Undo.TabIndex = 11;
            this.Undo.Text = "<--------";
            this.Undo.UseVisualStyleBackColor = true;
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // Redo
            // 
            this.Redo.Location = new System.Drawing.Point(556, 420);
            this.Redo.Name = "Redo";
            this.Redo.Size = new System.Drawing.Size(426, 45);
            this.Redo.TabIndex = 12;
            this.Redo.Text = "-------->";
            this.Redo.UseVisualStyleBackColor = true;
            this.Redo.Click += new System.EventHandler(this.Redo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 618);
            this.Controls.Add(this.Redo);
            this.Controls.Add(this.Undo);
            this.Controls.Add(this.numericUpDownMean);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBlack);
            this.Controls.Add(this.numericUpDownBlackLight);
            this.Controls.Add(this.numericUpDownGaus);
            this.Controls.Add(this.btnGaus);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.convert_btn);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "MatrixMixer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGaus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlackLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMean)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button convert_btn;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button btnGaus;
        private System.Windows.Forms.NumericUpDown numericUpDownGaus;
        private System.Windows.Forms.NumericUpDown numericUpDownBlackLight;
        private System.Windows.Forms.Button btnBlack;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDownMean;
        private System.Windows.Forms.Button Undo;
        private System.Windows.Forms.Button Redo;
    }
}

