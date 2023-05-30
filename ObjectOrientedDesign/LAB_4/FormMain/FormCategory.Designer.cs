namespace FormMain
{
    partial class FormCategory
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
            LabelCategory = new Label();
            ComboBoxCategories = new ComboBox();
            LabelDateFrom = new Label();
            DTPDateFrom = new DateTimePicker();
            DTPDateTo = new DateTimePicker();
            LabelDateTo = new Label();
            BtnSave = new Button();
            BtnCancel = new Button();
            SuspendLayout();
            // 
            // LabelCategory
            // 
            LabelCategory.AutoSize = true;
            LabelCategory.Location = new Point(41, 58);
            LabelCategory.Name = "LabelCategory";
            LabelCategory.Size = new Size(78, 20);
            LabelCategory.TabIndex = 0;
            LabelCategory.Text = "Kategorija";
            // 
            // ComboBoxCategories
            // 
            ComboBoxCategories.FormattingEnabled = true;
            ComboBoxCategories.Items.AddRange(new object[] { "AM", "A1", "A2", "A", "B1", "B", "BE", "C1", "C1E", "C", "CE", "D1", "D1E", "D", "DE", "F", "M" });
            ComboBoxCategories.Location = new Point(125, 55);
            ComboBoxCategories.Name = "ComboBoxCategories";
            ComboBoxCategories.Size = new Size(208, 28);
            ComboBoxCategories.TabIndex = 1;
            // 
            // LabelDateFrom
            // 
            LabelDateFrom.AutoSize = true;
            LabelDateFrom.Location = new Point(41, 99);
            LabelDateFrom.Name = "LabelDateFrom";
            LabelDateFrom.Size = new Size(78, 20);
            LabelDateFrom.TabIndex = 2;
            LabelDateFrom.Text = "Datum Od";
            // 
            // DTPDateFrom
            // 
            DTPDateFrom.CustomFormat = "dd.MM.yyyy.";
            DTPDateFrom.Format = DateTimePickerFormat.Custom;
            DTPDateFrom.Location = new Point(125, 94);
            DTPDateFrom.Name = "DTPDateFrom";
            DTPDateFrom.Size = new Size(208, 27);
            DTPDateFrom.TabIndex = 3;
            // 
            // DTPDateTo
            // 
            DTPDateTo.CustomFormat = "dd.MM.yyyy.";
            DTPDateTo.Format = DateTimePickerFormat.Custom;
            DTPDateTo.Location = new Point(125, 132);
            DTPDateTo.Name = "DTPDateTo";
            DTPDateTo.Size = new Size(208, 27);
            DTPDateTo.TabIndex = 5;
            // 
            // LabelDateTo
            // 
            LabelDateTo.AutoSize = true;
            LabelDateTo.Location = new Point(41, 137);
            LabelDateTo.Name = "LabelDateTo";
            LabelDateTo.Size = new Size(78, 20);
            LabelDateTo.TabIndex = 4;
            LabelDateTo.Text = "Datum Do";
            // 
            // BtnSave
            // 
            BtnSave.Location = new Point(72, 206);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(94, 29);
            BtnSave.TabIndex = 6;
            BtnSave.Text = "Prosledi";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(198, 206);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(94, 29);
            BtnCancel.TabIndex = 7;
            BtnCancel.Text = "Zatvori";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // FormCategory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 261);
            Controls.Add(BtnCancel);
            Controls.Add(BtnSave);
            Controls.Add(DTPDateTo);
            Controls.Add(LabelDateTo);
            Controls.Add(DTPDateFrom);
            Controls.Add(LabelDateFrom);
            Controls.Add(ComboBoxCategories);
            Controls.Add(LabelCategory);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormCategory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kategorija";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelCategory;
        private ComboBox ComboBoxCategories;
        private Label LabelDateFrom;
        private DateTimePicker DTPDateFrom;
        private DateTimePicker DTPDateTo;
        private Label LabelDateTo;
        private Button BtnSave;
        private Button BtnCancel;
    }
}