using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FilipLakicevic_18724_MMS
{
    public partial class Form1 : Form
    {
        private Bitmap currentImage;
        private ImageHistory imageHistory;

        public Form1()
        {
            InitializeComponent();
            imageHistory = new ImageHistory();
        }

        private void UpdateUndoRedoButtons()
        {
            btnUndo.Enabled = imageHistory.CanUndo();
            btnRedo.Enabled = imageHistory.CanRedo();
        }

        private void SaveImageState()
        {
            if (currentImage != null)
            {
                imageHistory.SaveState(currentImage);
                UpdateUndoRedoButtons();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.mms|JPEG Files|*.jpg;*.jpeg|PNG Files|*.png|Bitmap Files|*.bmp|MMS Files|*.mms|All Files|*.*";
            openFileDialog.Title = "Select an image to load";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentImage?.Dispose(); // Release previous image
                    currentImage = CustomImageFormat.LoadImage(openFileDialog.FileName);
                    pictureBox1.Image = currentImage;
                    
                    // Clear history when loading new image
                    imageHistory.ClearHistory();
                    UpdateUndoRedoButtons();
                    
                    // Display image information
                    this.Text = $"MMS Image Format Viewer - {Path.GetFileName(openFileDialog.FileName)} ({currentImage.Width}x{currentImage.Height})";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentImage == null)
            {
                MessageBox.Show("No image loaded to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "MMS Files|*.mms";
            saveFileDialog.Title = "Save image as MMS format";
            saveFileDialog.DefaultExt = "mms";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CustomImageFormat.SaveMms(currentImage, saveFileDialog.FileName);
                    
                    // Display file size information
                    FileInfo originalInfo = new FileInfo(saveFileDialog.FileName);
                    MessageBox.Show($"Image saved successfully as MMS format!\nFile size: {originalInfo.Length} bytes", 
                                  "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSmooth_Click(object sender, EventArgs e)
        {
            if (currentImage == null)
            {
                MessageBox.Show("No image loaded to smooth.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Save current state before applying filter
                SaveImageState();

                // Create a copy of the current image for smoothing
                Bitmap smoothedImage = new Bitmap(currentImage);
                
                // Get smooth strength from numeric up-down control
                int smoothStrength = (int)numSmoothStrength.Value;
                
                // Apply smooth filter with specified strength
                bool success = BitmapFilter.Smooth(smoothedImage, smoothStrength);
                
                if (success)
                {
                    // Replace current image with smoothed version
                    currentImage?.Dispose();
                    currentImage = smoothedImage;
                    pictureBox1.Image = currentImage;
                    
                    UpdateUndoRedoButtons();
                    MessageBox.Show($"Smooth filter applied successfully with strength {smoothStrength}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    smoothedImage.Dispose();
                    MessageBox.Show("Failed to apply smooth filter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying smooth filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnContrast_Click(object sender, EventArgs e)
        {
            if (currentImage == null)
            {
                MessageBox.Show("No image loaded to adjust contrast.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Save current state before applying filter
                SaveImageState();

                // Create a copy of the current image for contrast adjustment
                Bitmap contrastImage = new Bitmap(currentImage);
                
                // Get contrast strength from numeric up-down control and convert to sbyte
                // Map 1-20 range to reasonable contrast values (-100 to 100)
                int strengthValue = (int)numContrastStrength.Value;
                sbyte contrastValue = (sbyte)((strengthValue - 10) * 10); // Maps 1-20 to -90 to 100
                
                // Apply contrast filter
                bool success = BitmapFilter.Contrast(contrastImage, contrastValue);
                
                if (success)
                {
                    // Replace current image with contrast-adjusted version
                    currentImage?.Dispose();
                    currentImage = contrastImage;
                    pictureBox1.Image = currentImage;
                    
                    UpdateUndoRedoButtons();
                    MessageBox.Show($"Contrast filter applied successfully with strength {strengthValue} (contrast: {contrastValue})!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    contrastImage.Dispose();
                    MessageBox.Show("Failed to apply contrast filter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying contrast filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHistogram_Click(object sender, EventArgs e)
        {
            if (currentImage == null)
            {
                MessageBox.Show("No image loaded to adjust histogram.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Save current state before applying filter
                SaveImageState();

                // Create a copy of the current image for histogram adjustment
                Bitmap histogramImage = new Bitmap(currentImage);
                
                // Get shift and scale values for each RGB channel
                int redShift = (int)numRedShift.Value;
                double redScale = (double)numRedScale.Value;
                int greenShift = (int)numGreenShift.Value;
                double greenScale = (double)numGreenScale.Value;
                int blueShift = (int)numBlueShift.Value;
                double blueScale = (double)numBlueScale.Value;
                
                // Apply histogram shift and scale filter
                bool success = BitmapFilter.HistogramShiftScale(histogramImage, redShift, redScale, greenShift, greenScale, blueShift, blueScale);
                
                if (success)
                {
                    // Replace current image with histogram-adjusted version
                    currentImage?.Dispose();
                    currentImage = histogramImage;
                    pictureBox1.Image = currentImage;
                    
                    UpdateUndoRedoButtons();
                    MessageBox.Show($"Histogram filter applied successfully!\nRed: Shift={redShift}, Scale={redScale:F2}\nGreen: Shift={greenShift}, Scale={greenScale:F2}\nBlue: Shift={blueShift}, Scale={blueScale:F2}", 
                                  "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    histogramImage.Dispose();
                    MessageBox.Show("Failed to apply histogram filter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying histogram filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (currentImage == null || !imageHistory.CanUndo())
            {
                return;
            }

            try
            {
                Bitmap previousImage = imageHistory.PerformUndo(currentImage);
                if (previousImage != null)
                {
                    currentImage?.Dispose();
                    currentImage = previousImage;
                    pictureBox1.Image = currentImage;
                    UpdateUndoRedoButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing undo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (currentImage == null || !imageHistory.CanRedo())
            {
                return;
            }

            try
            {
                Bitmap nextImage = imageHistory.PerformRedo(currentImage);
                if (nextImage != null)
                {
                    currentImage?.Dispose();
                    currentImage = nextImage;
                    pictureBox1.Image = currentImage;
                    UpdateUndoRedoButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing redo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            currentImage?.Dispose();
            imageHistory?.ClearHistory();
            base.OnFormClosed(e);
        }
    }
}
