namespace Forms
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            DataGridViewDrivers = new DataGridView();
            ColName = new DataGridViewTextBoxColumn();
            ColLastName = new DataGridViewTextBoxColumn();
            ColDriverLicense = new DataGridViewTextBoxColumn();
            DateTimeValue = new Label();
            GroupBoxListDrivers = new GroupBox();
            button1 = new Button();
            BtnChangeDriver = new Button();
            BtnAddDriver = new Button();
            BtnSort = new Button();
            ComboBoxSort = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDrivers).BeginInit();
            GroupBoxListDrivers.SuspendLayout();
            SuspendLayout();
            // 
            // DataGridViewDrivers
            // 
            DataGridViewDrivers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewDrivers.Columns.AddRange(new DataGridViewColumn[] { ColName, ColLastName, ColDriverLicense });
            DataGridViewDrivers.Location = new Point(6, 26);
            DataGridViewDrivers.Name = "DataGridViewDrivers";
            DataGridViewDrivers.RowHeadersVisible = false;
            DataGridViewDrivers.RowHeadersWidth = 51;
            DataGridViewDrivers.RowTemplate.Height = 29;
            DataGridViewDrivers.Size = new Size(764, 295);
            DataGridViewDrivers.TabIndex = 1;
            // 
            // ColName
            // 
            ColName.HeaderText = "Ime";
            ColName.MinimumWidth = 6;
            ColName.Name = "ColName";
            ColName.ReadOnly = true;
            ColName.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColName.Width = 125;
            // 
            // ColLastName
            // 
            ColLastName.HeaderText = "Prezime";
            ColLastName.MinimumWidth = 6;
            ColLastName.Name = "ColLastName";
            ColLastName.ReadOnly = true;
            ColLastName.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColLastName.Width = 125;
            // 
            // ColDriverLicense
            // 
            ColDriverLicense.HeaderText = "Broj vozacke dozvole";
            ColDriverLicense.MinimumWidth = 6;
            ColDriverLicense.Name = "ColDriverLicense";
            ColDriverLicense.ReadOnly = true;
            ColDriverLicense.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColDriverLicense.Width = 125;
            // 
            // DateTimeValue
            // 
            DateTimeValue.AutoSize = true;
            DateTimeValue.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            DateTimeValue.Location = new Point(12, 22);
            DateTimeValue.Name = "DateTimeValue";
            DateTimeValue.Size = new Size(180, 23);
            DateTimeValue.TabIndex = 0;
            DateTimeValue.Text = "22:44:33 09.05.2023.";
            // 
            // GroupBoxListDrivers
            // 
            GroupBoxListDrivers.Controls.Add(button1);
            GroupBoxListDrivers.Controls.Add(BtnChangeDriver);
            GroupBoxListDrivers.Controls.Add(DataGridViewDrivers);
            GroupBoxListDrivers.Controls.Add(BtnAddDriver);
            GroupBoxListDrivers.Location = new Point(12, 59);
            GroupBoxListDrivers.Name = "GroupBoxListDrivers";
            GroupBoxListDrivers.Size = new Size(776, 379);
            GroupBoxListDrivers.TabIndex = 1;
            GroupBoxListDrivers.TabStop = false;
            GroupBoxListDrivers.Text = "Lista Vozaca";
            // 
            // button1
            // 
            button1.BackgroundImageLayout = ImageLayout.None;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(641, 333);
            button1.Name = "button1";
            button1.Size = new Size(129, 46);
            button1.TabIndex = 3;
            button1.Text = "Obrisi Vozaca";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.UseVisualStyleBackColor = true;
            // 
            // BtnChangeDriver
            // 
            BtnChangeDriver.BackgroundImageLayout = ImageLayout.None;
            BtnChangeDriver.Image = (Image)resources.GetObject("BtnChangeDriver.Image");
            BtnChangeDriver.ImageAlign = ContentAlignment.MiddleLeft;
            BtnChangeDriver.Location = new Point(142, 327);
            BtnChangeDriver.Name = "BtnChangeDriver";
            BtnChangeDriver.Size = new Size(134, 46);
            BtnChangeDriver.TabIndex = 2;
            BtnChangeDriver.Text = "Izmeni Vozaca";
            BtnChangeDriver.TextAlign = ContentAlignment.MiddleRight;
            BtnChangeDriver.UseVisualStyleBackColor = true;
            // 
            // BtnAddDriver
            // 
            BtnAddDriver.BackgroundImageLayout = ImageLayout.None;
            BtnAddDriver.Image = (Image)resources.GetObject("BtnAddDriver.Image");
            BtnAddDriver.ImageAlign = ContentAlignment.MiddleLeft;
            BtnAddDriver.Location = new Point(6, 327);
            BtnAddDriver.Name = "BtnAddDriver";
            BtnAddDriver.Size = new Size(130, 46);
            BtnAddDriver.TabIndex = 0;
            BtnAddDriver.Text = "Dodaj Vozaca";
            BtnAddDriver.TextAlign = ContentAlignment.MiddleRight;
            BtnAddDriver.UseVisualStyleBackColor = true;
            BtnAddDriver.Click += BtnAddDriver_Click;
            // 
            // BtnSort
            // 
            BtnSort.Location = new Point(396, 15);
            BtnSort.Name = "BtnSort";
            BtnSort.Size = new Size(94, 39);
            BtnSort.TabIndex = 2;
            BtnSort.Text = "Sortiraj";
            BtnSort.UseVisualStyleBackColor = true;
            // 
            // ComboBoxSort
            // 
            ComboBoxSort.FormattingEnabled = true;
            ComboBoxSort.Items.AddRange(new object[] { "Broj vozacke dozvole", "Ime", "Prezime" });
            ComboBoxSort.Location = new Point(496, 21);
            ComboBoxSort.Name = "ComboBoxSort";
            ComboBoxSort.Size = new Size(292, 28);
            ComboBoxSort.TabIndex = 3;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ComboBoxSort);
            Controls.Add(BtnSort);
            Controls.Add(GroupBoxListDrivers);
            Controls.Add(DateTimeValue);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lista Vozaca";
            FormClosing += FormMain_FormClosing;
            ((System.ComponentModel.ISupportInitialize)DataGridViewDrivers).EndInit();
            GroupBoxListDrivers.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label DateTimeValue;
        private GroupBox GroupBoxListDrivers;
        private Button BtnAddDriver;
        private Button BtnSort;
        private ComboBox ComboBoxSort;
        private DataGridView DataGridViewDrivers;
        private Button button1;
        private Button BtnChangeDriver;
        private DataGridViewTextBoxColumn ColName;
        private DataGridViewTextBoxColumn ColLastName;
        private DataGridViewTextBoxColumn ColDriverLicense;
    }
}