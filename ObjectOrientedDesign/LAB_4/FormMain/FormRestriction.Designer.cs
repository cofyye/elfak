namespace FormMain
{
    partial class FormRestriction
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
            BtnCancel = new Button();
            BtnSave = new Button();
            DTPDateTo = new DateTimePicker();
            LabelDateTo = new Label();
            DTPDateFrom = new DateTimePicker();
            LabelDateFrom = new Label();
            ComboBoxCategories = new ComboBox();
            LabelCategory = new Label();
            SuspendLayout();
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(210, 191);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(94, 29);
            BtnCancel.TabIndex = 15;
            BtnCancel.Text = "Zatvori";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // BtnSave
            // 
            BtnSave.Location = new Point(84, 191);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(94, 29);
            BtnSave.TabIndex = 14;
            BtnSave.Text = "Prosledi";
            BtnSave.UseVisualStyleBackColor = true;
            // 
            // DTPDateTo
            // 
            DTPDateTo.CustomFormat = "dd.MM.yyyy.";
            DTPDateTo.Format = DateTimePickerFormat.Custom;
            DTPDateTo.Location = new Point(137, 117);
            DTPDateTo.Name = "DTPDateTo";
            DTPDateTo.Size = new Size(208, 27);
            DTPDateTo.TabIndex = 13;
            // 
            // LabelDateTo
            // 
            LabelDateTo.AutoSize = true;
            LabelDateTo.Location = new Point(53, 122);
            LabelDateTo.Name = "LabelDateTo";
            LabelDateTo.Size = new Size(78, 20);
            LabelDateTo.TabIndex = 12;
            LabelDateTo.Text = "Datum Do";
            // 
            // DTPDateFrom
            // 
            DTPDateFrom.CustomFormat = "dd.MM.yyyy.";
            DTPDateFrom.Format = DateTimePickerFormat.Custom;
            DTPDateFrom.Location = new Point(137, 79);
            DTPDateFrom.Name = "DTPDateFrom";
            DTPDateFrom.Size = new Size(208, 27);
            DTPDateFrom.TabIndex = 11;
            // 
            // LabelDateFrom
            // 
            LabelDateFrom.AutoSize = true;
            LabelDateFrom.Location = new Point(53, 84);
            LabelDateFrom.Name = "LabelDateFrom";
            LabelDateFrom.Size = new Size(78, 20);
            LabelDateFrom.TabIndex = 10;
            LabelDateFrom.Text = "Datum Od";
            // 
            // ComboBoxCategories
            // 
            ComboBoxCategories.FormattingEnabled = true;
            ComboBoxCategories.Location = new Point(137, 40);
            ComboBoxCategories.Name = "ComboBoxCategories";
            ComboBoxCategories.Size = new Size(208, 28);
            ComboBoxCategories.TabIndex = 9;
            // 
            // LabelCategory
            // 
            LabelCategory.AutoSize = true;
            LabelCategory.Location = new Point(53, 43);
            LabelCategory.Name = "LabelCategory";
            LabelCategory.Size = new Size(78, 20);
            LabelCategory.TabIndex = 8;
            LabelCategory.Text = "Kategorija";
            // 
            // FormRestriction
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
            Name = "FormRestriction";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Zabrana";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnCancel;
        private Button BtnSave;
        private DateTimePicker DTPDateTo;
        private Label LabelDateTo;
        private DateTimePicker DTPDateFrom;
        private Label LabelDateFrom;
        private ComboBox ComboBoxCategories;
        private Label LabelCategory;
    }
}