namespace Forms
{
    partial class FormDriver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDriver));
            GroupBoxDetails = new GroupBox();
            ButtonPicture = new Button();
            PictureBoxAvatar = new PictureBox();
            ComboBoxGender = new ComboBox();
            LabelGender = new Label();
            TextBoxPlace = new TextBox();
            LabelPlace = new Label();
            TextBoxDriverLicenseNum = new TextBox();
            LabelDriverLicenseNum = new Label();
            DTPUntilDate = new DateTimePicker();
            LabelUntil = new Label();
            DTPExpiredDate = new DateTimePicker();
            LabelExpired = new Label();
            DTPBirthDate = new DateTimePicker();
            LabelBirthDate = new Label();
            TextBoxLastName = new TextBox();
            TextBoxName = new TextBox();
            LabelLastName = new Label();
            LabelName = new Label();
            GroupBoxCategory = new GroupBox();
            BtnDeleteCategory = new Button();
            BtnNewCategory = new Button();
            DataGridViewCategories = new DataGridView();
            GroupBoxRestriction = new GroupBox();
            BtnDeleteRestriction = new Button();
            BtnNewRestriction = new Button();
            DataGridViewRestrictions = new DataGridView();
            BtnSave = new Button();
            BtnCancel = new Button();
            openFileDialog1 = new OpenFileDialog();
            GroupBoxDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBoxAvatar).BeginInit();
            GroupBoxCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewCategories).BeginInit();
            GroupBoxRestriction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewRestrictions).BeginInit();
            SuspendLayout();
            // 
            // GroupBoxDetails
            // 
            GroupBoxDetails.Controls.Add(ButtonPicture);
            GroupBoxDetails.Controls.Add(PictureBoxAvatar);
            GroupBoxDetails.Controls.Add(ComboBoxGender);
            GroupBoxDetails.Controls.Add(LabelGender);
            GroupBoxDetails.Controls.Add(TextBoxPlace);
            GroupBoxDetails.Controls.Add(LabelPlace);
            GroupBoxDetails.Controls.Add(TextBoxDriverLicenseNum);
            GroupBoxDetails.Controls.Add(LabelDriverLicenseNum);
            GroupBoxDetails.Controls.Add(DTPUntilDate);
            GroupBoxDetails.Controls.Add(LabelUntil);
            GroupBoxDetails.Controls.Add(DTPExpiredDate);
            GroupBoxDetails.Controls.Add(LabelExpired);
            GroupBoxDetails.Controls.Add(DTPBirthDate);
            GroupBoxDetails.Controls.Add(LabelBirthDate);
            GroupBoxDetails.Controls.Add(TextBoxLastName);
            GroupBoxDetails.Controls.Add(TextBoxName);
            GroupBoxDetails.Controls.Add(LabelLastName);
            GroupBoxDetails.Controls.Add(LabelName);
            GroupBoxDetails.Location = new Point(12, 12);
            GroupBoxDetails.Name = "GroupBoxDetails";
            GroupBoxDetails.Size = new Size(640, 335);
            GroupBoxDetails.TabIndex = 0;
            GroupBoxDetails.TabStop = false;
            GroupBoxDetails.Text = "Osnovni Podaci";
            // 
            // ButtonPicture
            // 
            ButtonPicture.Location = new Point(414, 238);
            ButtonPicture.Name = "ButtonPicture";
            ButtonPicture.Size = new Size(220, 52);
            ButtonPicture.TabIndex = 17;
            ButtonPicture.Text = "Dodaj Sliku";
            ButtonPicture.UseVisualStyleBackColor = true;
            ButtonPicture.Click += ButtonPicture_Click;
            // 
            // PictureBoxAvatar
            // 
            PictureBoxAvatar.BorderStyle = BorderStyle.FixedSingle;
            PictureBoxAvatar.Location = new Point(414, 34);
            PictureBoxAvatar.Name = "PictureBoxAvatar";
            PictureBoxAvatar.Size = new Size(220, 185);
            PictureBoxAvatar.TabIndex = 16;
            PictureBoxAvatar.TabStop = false;
            // 
            // ComboBoxGender
            // 
            ComboBoxGender.FormattingEnabled = true;
            ComboBoxGender.Items.AddRange(new object[] { "M", "Z" });
            ComboBoxGender.Location = new Point(152, 262);
            ComboBoxGender.Name = "ComboBoxGender";
            ComboBoxGender.Size = new Size(218, 28);
            ComboBoxGender.TabIndex = 15;
            // 
            // LabelGender
            // 
            LabelGender.AutoSize = true;
            LabelGender.Location = new Point(117, 266);
            LabelGender.Name = "LabelGender";
            LabelGender.Size = new Size(29, 20);
            LabelGender.TabIndex = 14;
            LabelGender.Text = "Pol";
            // 
            // TextBoxPlace
            // 
            TextBoxPlace.Location = new Point(152, 229);
            TextBoxPlace.Name = "TextBoxPlace";
            TextBoxPlace.Size = new Size(218, 27);
            TextBoxPlace.TabIndex = 13;
            // 
            // LabelPlace
            // 
            LabelPlace.AutoSize = true;
            LabelPlace.Location = new Point(29, 232);
            LabelPlace.Name = "LabelPlace";
            LabelPlace.Size = new Size(117, 20);
            LabelPlace.TabIndex = 12;
            LabelPlace.Text = "Mesto Izdavanja";
            // 
            // TextBoxDriverLicenseNum
            // 
            TextBoxDriverLicenseNum.Location = new Point(152, 196);
            TextBoxDriverLicenseNum.Name = "TextBoxDriverLicenseNum";
            TextBoxDriverLicenseNum.Size = new Size(218, 27);
            TextBoxDriverLicenseNum.TabIndex = 11;
            // 
            // LabelDriverLicenseNum
            // 
            LabelDriverLicenseNum.AutoSize = true;
            LabelDriverLicenseNum.Location = new Point(3, 199);
            LabelDriverLicenseNum.Name = "LabelDriverLicenseNum";
            LabelDriverLicenseNum.Size = new Size(143, 20);
            LabelDriverLicenseNum.TabIndex = 10;
            LabelDriverLicenseNum.Text = "Br. Vozacke Dozvole";
            // 
            // DTPUntilDate
            // 
            DTPUntilDate.CustomFormat = "dd.MM.yyyy.";
            DTPUntilDate.Format = DateTimePickerFormat.Custom;
            DTPUntilDate.Location = new Point(152, 163);
            DTPUntilDate.Name = "DTPUntilDate";
            DTPUntilDate.Size = new Size(218, 27);
            DTPUntilDate.TabIndex = 9;
            // 
            // LabelUntil
            // 
            LabelUntil.AutoSize = true;
            LabelUntil.Location = new Point(117, 168);
            LabelUntil.Name = "LabelUntil";
            LabelUntil.Size = new Size(29, 20);
            LabelUntil.TabIndex = 8;
            LabelUntil.Text = "Do";
            // 
            // DTPExpiredDate
            // 
            DTPExpiredDate.CustomFormat = "dd.MM.yyyy.";
            DTPExpiredDate.Format = DateTimePickerFormat.Custom;
            DTPExpiredDate.Location = new Point(152, 130);
            DTPExpiredDate.Name = "DTPExpiredDate";
            DTPExpiredDate.Size = new Size(218, 27);
            DTPExpiredDate.TabIndex = 7;
            // 
            // LabelExpired
            // 
            LabelExpired.AutoSize = true;
            LabelExpired.Location = new Point(3, 135);
            LabelExpired.Name = "LabelExpired";
            LabelExpired.Size = new Size(143, 20);
            LabelExpired.TabIndex = 6;
            LabelExpired.Text = "Vazenje Dozvole Od";
            // 
            // DTPBirthDate
            // 
            DTPBirthDate.CustomFormat = "dd.MM.yyyy.";
            DTPBirthDate.Format = DateTimePickerFormat.Custom;
            DTPBirthDate.Location = new Point(152, 97);
            DTPBirthDate.Name = "DTPBirthDate";
            DTPBirthDate.Size = new Size(218, 27);
            DTPBirthDate.TabIndex = 5;
            // 
            // LabelBirthDate
            // 
            LabelBirthDate.AutoSize = true;
            LabelBirthDate.Location = new Point(29, 101);
            LabelBirthDate.Name = "LabelBirthDate";
            LabelBirthDate.Size = new Size(117, 20);
            LabelBirthDate.TabIndex = 4;
            LabelBirthDate.Text = "Datum Rodjenja";
            // 
            // TextBoxLastName
            // 
            TextBoxLastName.Location = new Point(152, 64);
            TextBoxLastName.Name = "TextBoxLastName";
            TextBoxLastName.Size = new Size(218, 27);
            TextBoxLastName.TabIndex = 3;
            // 
            // TextBoxName
            // 
            TextBoxName.Location = new Point(152, 31);
            TextBoxName.Name = "TextBoxName";
            TextBoxName.Size = new Size(218, 27);
            TextBoxName.TabIndex = 2;
            // 
            // LabelLastName
            // 
            LabelLastName.AutoSize = true;
            LabelLastName.Location = new Point(84, 67);
            LabelLastName.Name = "LabelLastName";
            LabelLastName.Size = new Size(62, 20);
            LabelLastName.TabIndex = 1;
            LabelLastName.Text = "Prezime";
            // 
            // LabelName
            // 
            LabelName.AutoSize = true;
            LabelName.Location = new Point(112, 34);
            LabelName.Name = "LabelName";
            LabelName.Size = new Size(34, 20);
            LabelName.TabIndex = 0;
            LabelName.Text = "Ime";
            // 
            // GroupBoxCategory
            // 
            GroupBoxCategory.Controls.Add(BtnDeleteCategory);
            GroupBoxCategory.Controls.Add(BtnNewCategory);
            GroupBoxCategory.Controls.Add(DataGridViewCategories);
            GroupBoxCategory.Location = new Point(12, 353);
            GroupBoxCategory.Name = "GroupBoxCategory";
            GroupBoxCategory.Size = new Size(640, 230);
            GroupBoxCategory.TabIndex = 1;
            GroupBoxCategory.TabStop = false;
            GroupBoxCategory.Text = "Kategorija";
            // 
            // BtnDeleteCategory
            // 
            BtnDeleteCategory.BackgroundImageLayout = ImageLayout.None;
            BtnDeleteCategory.Image = (Image)resources.GetObject("BtnDeleteCategory.Image");
            BtnDeleteCategory.ImageAlign = ContentAlignment.MiddleLeft;
            BtnDeleteCategory.Location = new Point(482, 180);
            BtnDeleteCategory.Name = "BtnDeleteCategory";
            BtnDeleteCategory.Size = new Size(152, 44);
            BtnDeleteCategory.TabIndex = 18;
            BtnDeleteCategory.Text = "Obrisi Kategoriju";
            BtnDeleteCategory.TextAlign = ContentAlignment.MiddleRight;
            BtnDeleteCategory.UseVisualStyleBackColor = true;
            BtnDeleteCategory.Click += BtnDeleteCategory_Click;
            // 
            // BtnNewCategory
            // 
            BtnNewCategory.BackgroundImageLayout = ImageLayout.None;
            BtnNewCategory.Image = (Image)resources.GetObject("BtnNewCategory.Image");
            BtnNewCategory.Location = new Point(6, 180);
            BtnNewCategory.Name = "BtnNewCategory";
            BtnNewCategory.Size = new Size(194, 44);
            BtnNewCategory.TabIndex = 19;
            BtnNewCategory.Text = "Dodaj Novu Kategoriju";
            BtnNewCategory.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnNewCategory.UseVisualStyleBackColor = true;
            BtnNewCategory.Click += BtnNewCategory_Click;
            // 
            // DataGridViewCategories
            // 
            DataGridViewCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewCategories.Location = new Point(6, 26);
            DataGridViewCategories.Name = "DataGridViewCategories";
            DataGridViewCategories.RowHeadersVisible = false;
            DataGridViewCategories.RowHeadersWidth = 51;
            DataGridViewCategories.RowTemplate.Height = 29;
            DataGridViewCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewCategories.Size = new Size(628, 148);
            DataGridViewCategories.TabIndex = 0;
            // 
            // GroupBoxRestriction
            // 
            GroupBoxRestriction.Controls.Add(BtnDeleteRestriction);
            GroupBoxRestriction.Controls.Add(BtnNewRestriction);
            GroupBoxRestriction.Controls.Add(DataGridViewRestrictions);
            GroupBoxRestriction.Location = new Point(18, 589);
            GroupBoxRestriction.Name = "GroupBoxRestriction";
            GroupBoxRestriction.Size = new Size(628, 226);
            GroupBoxRestriction.TabIndex = 2;
            GroupBoxRestriction.TabStop = false;
            GroupBoxRestriction.Text = "Zabrana Upravljanja";
            // 
            // BtnDeleteRestriction
            // 
            BtnDeleteRestriction.BackgroundImageLayout = ImageLayout.None;
            BtnDeleteRestriction.Image = (Image)resources.GetObject("BtnDeleteRestriction.Image");
            BtnDeleteRestriction.ImageAlign = ContentAlignment.MiddleLeft;
            BtnDeleteRestriction.Location = new Point(491, 176);
            BtnDeleteRestriction.Name = "BtnDeleteRestriction";
            BtnDeleteRestriction.Size = new Size(137, 44);
            BtnDeleteRestriction.TabIndex = 20;
            BtnDeleteRestriction.Text = "Obrisi Zabranu";
            BtnDeleteRestriction.TextAlign = ContentAlignment.MiddleRight;
            BtnDeleteRestriction.UseVisualStyleBackColor = true;
            BtnDeleteRestriction.Click += BtnDeleteRestriction_Click;
            // 
            // BtnNewRestriction
            // 
            BtnNewRestriction.BackgroundImageLayout = ImageLayout.None;
            BtnNewRestriction.Image = (Image)resources.GetObject("BtnNewRestriction.Image");
            BtnNewRestriction.ImageAlign = ContentAlignment.MiddleLeft;
            BtnNewRestriction.Location = new Point(6, 176);
            BtnNewRestriction.Name = "BtnNewRestriction";
            BtnNewRestriction.Size = new Size(177, 44);
            BtnNewRestriction.TabIndex = 20;
            BtnNewRestriction.Text = "Dodaj Novu Zabranu";
            BtnNewRestriction.TextAlign = ContentAlignment.MiddleRight;
            BtnNewRestriction.UseVisualStyleBackColor = true;
            BtnNewRestriction.Click += BtnNewRestriction_Click;
            // 
            // DataGridViewRestrictions
            // 
            DataGridViewRestrictions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewRestrictions.Location = new Point(6, 26);
            DataGridViewRestrictions.Name = "DataGridViewRestrictions";
            DataGridViewRestrictions.RowHeadersVisible = false;
            DataGridViewRestrictions.RowHeadersWidth = 51;
            DataGridViewRestrictions.RowTemplate.Height = 29;
            DataGridViewRestrictions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewRestrictions.Size = new Size(628, 148);
            DataGridViewRestrictions.TabIndex = 0;
            // 
            // BtnSave
            // 
            BtnSave.Location = new Point(164, 845);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(135, 32);
            BtnSave.TabIndex = 18;
            BtnSave.Text = "Prosledi";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(305, 845);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(135, 32);
            BtnCancel.TabIndex = 19;
            BtnCancel.Text = "Zatvori";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormDriver
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 889);
            Controls.Add(BtnCancel);
            Controls.Add(BtnSave);
            Controls.Add(GroupBoxRestriction);
            Controls.Add(GroupBoxCategory);
            Controls.Add(GroupBoxDetails);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormDriver";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Vozac";
            GroupBoxDetails.ResumeLayout(false);
            GroupBoxDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBoxAvatar).EndInit();
            GroupBoxCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewCategories).EndInit();
            GroupBoxRestriction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewRestrictions).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GroupBoxDetails;
        private TextBox TextBoxLastName;
        private TextBox TextBoxName;
        private Label LabelLastName;
        private Label LabelName;
        private GroupBox GroupBoxCategory;
        private Label LabelGender;
        private TextBox TextBoxPlace;
        private Label LabelPlace;
        private TextBox TextBoxDriverLicenseNum;
        private Label LabelDriverLicenseNum;
        private DateTimePicker DTPUntilDate;
        private Label LabelUntil;
        private DateTimePicker DTPExpiredDate;
        private Label LabelExpired;
        private DateTimePicker DTPBirthDate;
        private Label LabelBirthDate;
        private ComboBox ComboBoxGender;
        private PictureBox PictureBoxAvatar;
        private Button ButtonPicture;
        private Button BtnDeleteCategory;
        private Button BtnNewCategory;
        private DataGridView DataGridViewCategories;
        private GroupBox GroupBoxRestriction;
        private Button BtnDeleteRestriction;
        private Button BtnNewRestriction;
        private DataGridView DataGridViewRestrictions;
        private Button BtnSave;
        private Button BtnCancel;
        private OpenFileDialog openFileDialog1;
    }
}