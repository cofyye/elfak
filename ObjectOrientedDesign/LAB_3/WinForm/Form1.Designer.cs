using WinFormData;

namespace WinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelPersonFields = new Panel();
            btnCancel = new Button();
            saveBtn = new Button();
            fieldAddress = new RichTextBox();
            fieldBirthDate = new DateTimePicker();
            labelAddress = new Label();
            labelBirthDate = new Label();
            labelPhone = new Label();
            fieldPhone = new TextBox();
            labelLastName = new Label();
            fieldLastName = new TextBox();
            labelFirstName = new Label();
            fieldFirstName = new TextBox();
            listPerson = new ListBox();
            labelPersonList = new Label();
            deletePersonBtn = new Button();
            deletePersonListBtn = new Button();
            sortListBtn = new Button();
            fieldSortListValue = new ComboBox();
            showTimeBtn = new Button();
            panelPersonFields.SuspendLayout();
            SuspendLayout();
            // 
            // panelPersonFields
            // 
            panelPersonFields.Controls.Add(btnCancel);
            panelPersonFields.Controls.Add(saveBtn);
            panelPersonFields.Controls.Add(fieldAddress);
            panelPersonFields.Controls.Add(fieldBirthDate);
            panelPersonFields.Controls.Add(labelAddress);
            panelPersonFields.Controls.Add(labelBirthDate);
            panelPersonFields.Controls.Add(labelPhone);
            panelPersonFields.Controls.Add(fieldPhone);
            panelPersonFields.Controls.Add(labelLastName);
            panelPersonFields.Controls.Add(fieldLastName);
            panelPersonFields.Controls.Add(labelFirstName);
            panelPersonFields.Controls.Add(fieldFirstName);
            panelPersonFields.Dock = DockStyle.Top;
            panelPersonFields.Location = new Point(0, 0);
            panelPersonFields.Name = "panelPersonFields";
            panelPersonFields.Size = new Size(951, 209);
            panelPersonFields.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancel.Image = (Image)resources.GetObject("btnCancel.Image");
            btnCancel.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancel.Location = new Point(537, 164);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(98, 36);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Ponisti";
            btnCancel.TextAlign = ContentAlignment.MiddleRight;
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // saveBtn
            // 
            saveBtn.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            saveBtn.Image = (Image)resources.GetObject("saveBtn.Image");
            saveBtn.ImageAlign = ContentAlignment.MiddleLeft;
            saveBtn.Location = new Point(281, 164);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(111, 36);
            saveBtn.TabIndex = 6;
            saveBtn.Text = "Zapamti";
            saveBtn.TextAlign = ContentAlignment.MiddleRight;
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += SaveBtn_Click;
            // 
            // fieldAddress
            // 
            fieldAddress.Location = new Point(537, 94);
            fieldAddress.Name = "fieldAddress";
            fieldAddress.Size = new Size(404, 47);
            fieldAddress.TabIndex = 5;
            fieldAddress.Text = "";
            // 
            // fieldBirthDate
            // 
            fieldBirthDate.CustomFormat = "dd.MM.yyyy.";
            fieldBirthDate.Format = DateTimePickerFormat.Custom;
            fieldBirthDate.Location = new Point(678, 25);
            fieldBirthDate.Name = "fieldBirthDate";
            fieldBirthDate.Size = new Size(263, 27);
            fieldBirthDate.TabIndex = 4;
            // 
            // labelAddress
            // 
            labelAddress.AutoSize = true;
            labelAddress.Location = new Point(537, 71);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(135, 20);
            labelAddress.TabIndex = 5;
            labelAddress.Text = "Adresa Stanovanja:";
            // 
            // labelBirthDate
            // 
            labelBirthDate.AutoSize = true;
            labelBirthDate.Location = new Point(552, 29);
            labelBirthDate.Name = "labelBirthDate";
            labelBirthDate.Size = new Size(120, 20);
            labelBirthDate.TabIndex = 4;
            labelBirthDate.Text = "Datum Rodjenja:";
            // 
            // labelPhone
            // 
            labelPhone.AutoSize = true;
            labelPhone.Location = new Point(12, 117);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(61, 20);
            labelPhone.TabIndex = 3;
            labelPhone.Text = "Telefon:";
            // 
            // fieldPhone
            // 
            fieldPhone.Location = new Point(97, 114);
            fieldPhone.Name = "fieldPhone";
            fieldPhone.Size = new Size(295, 27);
            fieldPhone.TabIndex = 3;
            fieldPhone.Leave += FieldPhone_Leave;
            // 
            // labelLastName
            // 
            labelLastName.AutoSize = true;
            labelLastName.Location = new Point(12, 74);
            labelLastName.Name = "labelLastName";
            labelLastName.Size = new Size(65, 20);
            labelLastName.TabIndex = 2;
            labelLastName.Text = "Prezime:";
            // 
            // fieldLastName
            // 
            fieldLastName.Location = new Point(97, 71);
            fieldLastName.Name = "fieldLastName";
            fieldLastName.Size = new Size(295, 27);
            fieldLastName.TabIndex = 2;
            fieldLastName.KeyPress += FieldLastName_KeyPress;
            // 
            // labelFirstName
            // 
            labelFirstName.AutoSize = true;
            labelFirstName.Location = new Point(12, 32);
            labelFirstName.Name = "labelFirstName";
            labelFirstName.Size = new Size(37, 20);
            labelFirstName.TabIndex = 1;
            labelFirstName.Text = "Ime:";
            // 
            // fieldFirstName
            // 
            fieldFirstName.Location = new Point(97, 29);
            fieldFirstName.Name = "fieldFirstName";
            fieldFirstName.Size = new Size(295, 27);
            fieldFirstName.TabIndex = 1;
            fieldFirstName.KeyPress += FieldFirstName_KeyPress;
            // 
            // listPerson
            // 
            listPerson.FormattingEnabled = true;
            listPerson.ItemHeight = 20;
            listPerson.Location = new Point(12, 261);
            listPerson.Name = "listPerson";
            listPerson.Size = new Size(929, 224);
            listPerson.TabIndex = 8;
            listPerson.MouseDoubleClick += ListPerson_MouseDoubleClick;
            // 
            // labelPersonList
            // 
            labelPersonList.AutoSize = true;
            labelPersonList.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelPersonList.Location = new Point(394, 223);
            labelPersonList.Name = "labelPersonList";
            labelPersonList.Size = new Size(145, 35);
            labelPersonList.TabIndex = 8;
            labelPersonList.Text = "Lista Osoba";
            labelPersonList.TextAlign = ContentAlignment.TopCenter;
            // 
            // deletePersonBtn
            // 
            deletePersonBtn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            deletePersonBtn.Image = (Image)resources.GetObject("deletePersonBtn.Image");
            deletePersonBtn.ImageAlign = ContentAlignment.MiddleLeft;
            deletePersonBtn.Location = new Point(12, 491);
            deletePersonBtn.Name = "deletePersonBtn";
            deletePersonBtn.Size = new Size(183, 50);
            deletePersonBtn.TabIndex = 9;
            deletePersonBtn.Text = "Obrisi izabranu osobu";
            deletePersonBtn.TextAlign = ContentAlignment.MiddleRight;
            deletePersonBtn.UseVisualStyleBackColor = true;
            deletePersonBtn.Click += DeletePersonBtn_Click;
            // 
            // deletePersonListBtn
            // 
            deletePersonListBtn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            deletePersonListBtn.Image = (Image)resources.GetObject("deletePersonListBtn.Image");
            deletePersonListBtn.ImageAlign = ContentAlignment.MiddleLeft;
            deletePersonListBtn.Location = new Point(238, 491);
            deletePersonListBtn.Name = "deletePersonListBtn";
            deletePersonListBtn.Size = new Size(154, 50);
            deletePersonListBtn.TabIndex = 10;
            deletePersonListBtn.Text = "Obrisi listu osoba";
            deletePersonListBtn.TextAlign = ContentAlignment.MiddleRight;
            deletePersonListBtn.UseVisualStyleBackColor = true;
            deletePersonListBtn.Click += DeletePersonListBtn_Click;
            // 
            // sortListBtn
            // 
            sortListBtn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sortListBtn.Image = (Image)resources.GetObject("sortListBtn.Image");
            sortListBtn.ImageAlign = ContentAlignment.MiddleLeft;
            sortListBtn.Location = new Point(431, 491);
            sortListBtn.Name = "sortListBtn";
            sortListBtn.Size = new Size(123, 50);
            sortListBtn.TabIndex = 11;
            sortListBtn.Text = "Sortiraj listu";
            sortListBtn.TextAlign = ContentAlignment.MiddleRight;
            sortListBtn.UseVisualStyleBackColor = true;
            sortListBtn.Click += SortListBtn_Click;
            // 
            // fieldSortListValue
            // 
            fieldSortListValue.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldSortListValue.FormattingEnabled = true;
            fieldSortListValue.Items.AddRange(new object[] { "PO IMENU", "PO PREZIMENU", "PO DATUMU RODJENJA" });
            fieldSortListValue.Location = new Point(560, 503);
            fieldSortListValue.Name = "fieldSortListValue";
            fieldSortListValue.Size = new Size(216, 28);
            fieldSortListValue.TabIndex = 12;
            fieldSortListValue.SelectedIndex = fieldSortListValue.FindStringExact("PO IMENU");
            // 
            // showTimeBtn
            // 
            showTimeBtn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            showTimeBtn.Image = (Image)resources.GetObject("showTimeBtn.Image");
            showTimeBtn.ImageAlign = ContentAlignment.MiddleLeft;
            showTimeBtn.Location = new Point(810, 491);
            showTimeBtn.Name = "showTimeBtn";
            showTimeBtn.Size = new Size(131, 50);
            showTimeBtn.TabIndex = 13;
            showTimeBtn.Text = "Prikazi vreme";
            showTimeBtn.TextAlign = ContentAlignment.MiddleRight;
            showTimeBtn.UseVisualStyleBackColor = true;
            showTimeBtn.Click += ShowTimeBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(951, 553);
            Controls.Add(showTimeBtn);
            Controls.Add(fieldSortListValue);
            Controls.Add(sortListBtn);
            Controls.Add(deletePersonListBtn);
            Controls.Add(deletePersonBtn);
            Controls.Add(labelPersonList);
            Controls.Add(listPerson);
            Controls.Add(panelPersonFields);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Person System";
            FormClosing += Form1_FormClosing;
            MouseDoubleClick += Form1_MouseDoubleClick;
            panelPersonFields.ResumeLayout(false);
            panelPersonFields.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelPersonFields;
        private Label labelFirstName;
        private TextBox fieldFirstName;
        private Label labelPhone;
        private TextBox fieldPhone;
        private Label labelLastName;
        private TextBox fieldLastName;
        private Label labelAddress;
        private Label labelBirthDate;
        private DateTimePicker fieldBirthDate;
        private RichTextBox fieldAddress;
        private Button btnCancel;
        private Button saveBtn;
        private ListBox listPerson;
        private Label labelPersonList;
        private Button deletePersonBtn;
        private Button deletePersonListBtn;
        private Button sortListBtn;
        private ComboBox fieldSortListValue;
        private Button showTimeBtn;
    }
}