namespace FilipLakicevic_18724_MMS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSmooth = new System.Windows.Forms.Button();
            this.btnContrast = new System.Windows.Forms.Button();
            this.btnHistogram = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.numSmoothStrength = new System.Windows.Forms.NumericUpDown();
            this.numContrastStrength = new System.Windows.Forms.NumericUpDown();
            this.numRedShift = new System.Windows.Forms.NumericUpDown();
            this.numRedScale = new System.Windows.Forms.NumericUpDown();
            this.numGreenShift = new System.Windows.Forms.NumericUpDown();
            this.numGreenScale = new System.Windows.Forms.NumericUpDown();
            this.numBlueShift = new System.Windows.Forms.NumericUpDown();
            this.numBlueScale = new System.Windows.Forms.NumericUpDown();
            this.lblSmoothStrength = new System.Windows.Forms.Label();
            this.lblContrastStrength = new System.Windows.Forms.Label();
            this.lblRedShift = new System.Windows.Forms.Label();
            this.lblRedScale = new System.Windows.Forms.Label();
            this.lblGreenShift = new System.Windows.Forms.Label();
            this.lblGreenScale = new System.Windows.Forms.Label();
            this.lblBlueShift = new System.Windows.Forms.Label();
            this.lblBlueScale = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numSmoothStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numContrastStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRedShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRedScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGreenShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGreenScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlueShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlueScale)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(700, 50);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(80, 30);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(700, 100);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save MMS";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSmooth
            // 
            this.btnSmooth.Location = new System.Drawing.Point(700, 180);
            this.btnSmooth.Name = "btnSmooth";
            this.btnSmooth.Size = new System.Drawing.Size(80, 30);
            this.btnSmooth.TabIndex = 4;
            this.btnSmooth.Text = "Smooth";
            this.btnSmooth.UseVisualStyleBackColor = true;
            this.btnSmooth.Click += new System.EventHandler(this.btnSmooth_Click);
            // 
            // btnContrast
            // 
            this.btnContrast.Location = new System.Drawing.Point(700, 250);
            this.btnContrast.Name = "btnContrast";
            this.btnContrast.Size = new System.Drawing.Size(80, 30);
            this.btnContrast.TabIndex = 6;
            this.btnContrast.Text = "Contrast";
            this.btnContrast.UseVisualStyleBackColor = true;
            this.btnContrast.Click += new System.EventHandler(this.btnContrast_Click);
            // 
            // btnHistogram
            // 
            this.btnHistogram.Location = new System.Drawing.Point(820, 180);
            this.btnHistogram.Name = "btnHistogram";
            this.btnHistogram.Size = new System.Drawing.Size(80, 30);
            this.btnHistogram.TabIndex = 8;
            this.btnHistogram.Text = "Histogram";
            this.btnHistogram.UseVisualStyleBackColor = true;
            this.btnHistogram.Click += new System.EventHandler(this.btnHistogram_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Enabled = false;
            this.btnUndo.Location = new System.Drawing.Point(700, 380);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(80, 30);
            this.btnUndo.TabIndex = 24;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Enabled = false;
            this.btnRedo.Location = new System.Drawing.Point(790, 380);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(80, 30);
            this.btnRedo.TabIndex = 25;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // numSmoothStrength
            // 
            this.numSmoothStrength.Location = new System.Drawing.Point(700, 215);
            this.numSmoothStrength.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numSmoothStrength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSmoothStrength.Name = "numSmoothStrength";
            this.numSmoothStrength.Size = new System.Drawing.Size(80, 23);
            this.numSmoothStrength.TabIndex = 5;
            this.numSmoothStrength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numContrastStrength
            // 
            this.numContrastStrength.Location = new System.Drawing.Point(700, 285);
            this.numContrastStrength.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numContrastStrength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numContrastStrength.Name = "numContrastStrength";
            this.numContrastStrength.Size = new System.Drawing.Size(80, 23);
            this.numContrastStrength.TabIndex = 7;
            this.numContrastStrength.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // numRedShift
            // 
            this.numRedShift.Location = new System.Drawing.Point(820, 235);
            this.numRedShift.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            this.numRedShift.Minimum = new decimal(new int[] { 128, 0, 0, -2147483648 });
            this.numRedShift.Name = "numRedShift";
            this.numRedShift.Size = new System.Drawing.Size(60, 23);
            this.numRedShift.TabIndex = 10;
            this.numRedShift.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // numRedScale
            // 
            this.numRedScale.DecimalPlaces = 2;
            this.numRedScale.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            this.numRedScale.Location = new System.Drawing.Point(890, 235);
            this.numRedScale.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            this.numRedScale.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            this.numRedScale.Name = "numRedScale";
            this.numRedScale.Size = new System.Drawing.Size(60, 23);
            this.numRedScale.TabIndex = 11;
            this.numRedScale.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numGreenShift
            // 
            this.numGreenShift.Location = new System.Drawing.Point(820, 285);
            this.numGreenShift.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            this.numGreenShift.Minimum = new decimal(new int[] { 128, 0, 0, -2147483648 });
            this.numGreenShift.Name = "numGreenShift";
            this.numGreenShift.Size = new System.Drawing.Size(60, 23);
            this.numGreenShift.TabIndex = 13;
            this.numGreenShift.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // numGreenScale
            // 
            this.numGreenScale.DecimalPlaces = 2;
            this.numGreenScale.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            this.numGreenScale.Location = new System.Drawing.Point(890, 285);
            this.numGreenScale.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            this.numGreenScale.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            this.numGreenScale.Name = "numGreenScale";
            this.numGreenScale.Size = new System.Drawing.Size(60, 23);
            this.numGreenScale.TabIndex = 14;
            this.numGreenScale.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numBlueShift
            // 
            this.numBlueShift.Location = new System.Drawing.Point(820, 335);
            this.numBlueShift.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            this.numBlueShift.Minimum = new decimal(new int[] { 128, 0, 0, -2147483648 });
            this.numBlueShift.Name = "numBlueShift";
            this.numBlueShift.Size = new System.Drawing.Size(60, 23);
            this.numBlueShift.TabIndex = 16;
            this.numBlueShift.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // numBlueScale
            // 
            this.numBlueScale.DecimalPlaces = 2;
            this.numBlueScale.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            this.numBlueScale.Location = new System.Drawing.Point(890, 335);
            this.numBlueScale.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            this.numBlueScale.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            this.numBlueScale.Name = "numBlueScale";
            this.numBlueScale.Size = new System.Drawing.Size(60, 23);
            this.numBlueScale.TabIndex = 17;
            this.numBlueScale.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblSmoothStrength
            // 
            this.lblSmoothStrength.AutoSize = true;
            this.lblSmoothStrength.Location = new System.Drawing.Point(700, 162);
            this.lblSmoothStrength.Name = "lblSmoothStrength";
            this.lblSmoothStrength.Size = new System.Drawing.Size(52, 15);
            this.lblSmoothStrength.TabIndex = 8;
            this.lblSmoothStrength.Text = "Strength";
            // 
            // lblContrastStrength
            // 
            this.lblContrastStrength.AutoSize = true;
            this.lblContrastStrength.Location = new System.Drawing.Point(700, 320);
            this.lblContrastStrength.Name = "lblContrastStrength";
            this.lblContrastStrength.Size = new System.Drawing.Size(52, 15);
            this.lblContrastStrength.TabIndex = 9;
            this.lblContrastStrength.Text = "Strength";
            // 
            // lblRedShift
            // 
            this.lblRedShift.AutoSize = true;
            this.lblRedShift.ForeColor = System.Drawing.Color.Red;
            this.lblRedShift.Location = new System.Drawing.Point(820, 217);
            this.lblRedShift.Name = "lblRedShift";
            this.lblRedShift.Size = new System.Drawing.Size(58, 15);
            this.lblRedShift.TabIndex = 18;
            this.lblRedShift.Text = "Red Shift";
            // 
            // lblRedScale
            // 
            this.lblRedScale.AutoSize = true;
            this.lblRedScale.ForeColor = System.Drawing.Color.Red;
            this.lblRedScale.Location = new System.Drawing.Point(890, 217);
            this.lblRedScale.Name = "lblRedScale";
            this.lblRedScale.Size = new System.Drawing.Size(59, 15);
            this.lblRedScale.TabIndex = 19;
            this.lblRedScale.Text = "Red Scale";
            // 
            // lblGreenShift
            // 
            this.lblGreenShift.AutoSize = true;
            this.lblGreenShift.ForeColor = System.Drawing.Color.Green;
            this.lblGreenShift.Location = new System.Drawing.Point(820, 267);
            this.lblGreenShift.Name = "lblGreenShift";
            this.lblGreenShift.Size = new System.Drawing.Size(69, 15);
            this.lblGreenShift.TabIndex = 20;
            this.lblGreenShift.Text = "Green Shift";
            // 
            // lblGreenScale
            // 
            this.lblGreenScale.AutoSize = true;
            this.lblGreenScale.ForeColor = System.Drawing.Color.Green;
            this.lblGreenScale.Location = new System.Drawing.Point(890, 267);
            this.lblGreenScale.Name = "lblGreenScale";
            this.lblGreenScale.Size = new System.Drawing.Size(70, 15);
            this.lblGreenScale.TabIndex = 21;
            this.lblGreenScale.Text = "Green Scale";
            // 
            // lblBlueShift
            // 
            this.lblBlueShift.AutoSize = true;
            this.lblBlueShift.ForeColor = System.Drawing.Color.Blue;
            this.lblBlueShift.Location = new System.Drawing.Point(820, 317);
            this.lblBlueShift.Name = "lblBlueShift";
            this.lblBlueShift.Size = new System.Drawing.Size(59, 15);
            this.lblBlueShift.TabIndex = 22;
            this.lblBlueShift.Text = "Blue Shift";
            // 
            // lblBlueScale
            // 
            this.lblBlueScale.AutoSize = true;
            this.lblBlueScale.ForeColor = System.Drawing.Color.Blue;
            this.lblBlueScale.Location = new System.Drawing.Point(890, 317);
            this.lblBlueScale.Name = "lblBlueScale";
            this.lblBlueScale.Size = new System.Drawing.Size(60, 15);
            this.lblBlueScale.TabIndex = 23;
            this.lblBlueScale.Text = "Blue Scale";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 450);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(680, 450);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 480);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.lblBlueScale);
            this.Controls.Add(this.lblBlueShift);
            this.Controls.Add(this.lblGreenScale);
            this.Controls.Add(this.lblGreenShift);
            this.Controls.Add(this.lblRedScale);
            this.Controls.Add(this.lblRedShift);
            this.Controls.Add(this.numBlueScale);
            this.Controls.Add(this.numBlueShift);
            this.Controls.Add(this.numGreenScale);
            this.Controls.Add(this.numGreenShift);
            this.Controls.Add(this.numRedScale);
            this.Controls.Add(this.numRedShift);
            this.Controls.Add(this.btnHistogram);
            this.Controls.Add(this.lblContrastStrength);
            this.Controls.Add(this.lblSmoothStrength);
            this.Controls.Add(this.numContrastStrength);
            this.Controls.Add(this.numSmoothStrength);
            this.Controls.Add(this.btnContrast);
            this.Controls.Add(this.btnSmooth);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Name = "Form1";
            this.Text = "MMS Image Format Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.numSmoothStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numContrastStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRedShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRedScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGreenShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGreenScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlueShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlueScale)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSmooth;
        private System.Windows.Forms.Button btnContrast;
        private System.Windows.Forms.Button btnHistogram;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.NumericUpDown numSmoothStrength;
        private System.Windows.Forms.NumericUpDown numContrastStrength;
        private System.Windows.Forms.NumericUpDown numRedShift;
        private System.Windows.Forms.NumericUpDown numRedScale;
        private System.Windows.Forms.NumericUpDown numGreenShift;
        private System.Windows.Forms.NumericUpDown numGreenScale;
        private System.Windows.Forms.NumericUpDown numBlueShift;
        private System.Windows.Forms.NumericUpDown numBlueScale;
        private System.Windows.Forms.Label lblSmoothStrength;
        private System.Windows.Forms.Label lblContrastStrength;
        private System.Windows.Forms.Label lblRedShift;
        private System.Windows.Forms.Label lblRedScale;
        private System.Windows.Forms.Label lblGreenShift;
        private System.Windows.Forms.Label lblGreenScale;
        private System.Windows.Forms.Label lblBlueShift;
        private System.Windows.Forms.Label lblBlueScale;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
