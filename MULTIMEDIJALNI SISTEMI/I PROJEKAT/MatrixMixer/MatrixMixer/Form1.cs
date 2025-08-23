using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixMixer
{
    public partial class Form1 : Form
    {
       UndoRedo undoRedo;
        public Form1()
        {
            InitializeComponent();
            Undo.Enabled = false;
            Redo.Enabled = false;
            undoRedo = new UndoRedo();

        }
        private void convert_btn_Click(object sender, EventArgs e)
        {
            
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nema slike u PictureBox-u.");
                return;
            }

            Bitmap bmp = new Bitmap(pictureBox1.Image);

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Title = "Odaberite lokaciju";
                    sfd.Filter = "myImg|*.myimg";
                    if (sfd.ShowDialog() == DialogResult.OK) 
                    {
                        string filePath = sfd.FileName;
                        MyImageFormat.Save(bmp, filePath);
                    }
                }
                
                MessageBox.Show("Slika sacuvana");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Odaberite sliku";
                ofd.Filter = "Slike|*.bmp;*.jpg;*.jpeg;*.png;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap bmp = new Bitmap(ofd.FileName);
                        pictureBox1.Image = bmp;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Greška pri učitavanju slike: " + ex.Message);
                    }
                }
                undoRedo.Clear();
                Undo.Enabled = false;
                Redo.Enabled = false;
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
           
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Odaberite sliku";
                ofd.Filter = "Slike|*.myimg";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image = MyImageFormat.Load(ofd.FileName);
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Greška pri učitavanju slike: " + ex.Message);
                    }
                }
            }
            undoRedo.Clear();
            Undo.Enabled = false;
            Redo.Enabled = false;
        }

        private void btnGaus_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null)
                { 
                    MessageBox.Show("Prvo ucitajte sliku");
                    return;
                }
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Bitmap filtered = MyImageFormat.ApplyGaussianFilterFast(bmp , (int)numericUpDownGaus.Value);
                undoRedo.CommitAction(bmp);
                pictureBox1.Image = filtered;
                Undo.Enabled = true;
                Redo.Enabled = false;
                MessageBox.Show("Primenjen gaus filter");
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Greška pri primenjivanju filtra: " + ex.Message);
            }
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Prvo ucitajte sliku");
                    return;
                }
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Bitmap filtered = MyImageFormat.ApplyBlacklight(bmp, (int)numericUpDownBlackLight.Value);
                undoRedo.CommitAction(bmp);
                pictureBox1.Image = filtered;
                Undo.Enabled = true;
                Redo.Enabled = false;
                MessageBox.Show("Primenjen black light filter");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri primenjivanju filtra: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Prvo ucitajte sliku");
                    return;
                }
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Bitmap filtered = MyImageFormat.ApplyMeanRemoval(bmp, (int)numericUpDownMean.Value);
                undoRedo.CommitAction(bmp);
                pictureBox1.Image = filtered;
                Undo.Enabled = true;
                Redo.Enabled = false;
                MessageBox.Show("Primenjen mean removal filter");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri primenjivanju filtra: " + ex.Message);
            }
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = undoRedo.UndoAction(new Bitmap(pictureBox1.Image));
            if (undoRedo.EmptyUndo())
                Undo.Enabled = false;
            Redo.Enabled = true;
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = undoRedo.RedoAction(new Bitmap(pictureBox1.Image));
            if (undoRedo.EmptyRedo())
                Redo.Enabled = false;
            Undo.Enabled = true;
        }
    }
}
