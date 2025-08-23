using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FilipLakicevic_18724_MMS
{
    public static class CustomImageFormat
    {
        // Conversion from RGB to YUV using JPEG formulas
        public static void RgbToYuv(byte r, byte g, byte b, out byte y, out byte u, out byte v)
        {
            // JPEG conversion formulas
            double yy = 0.299 * r + 0.587 * g + 0.114 * b;
            double uu = -0.14713 * r - 0.28886 * g + 0.436 * b + 128;
            double vv = 0.615 * r - 0.51499 * g - 0.10001 * b + 128;

            y = (byte)Math.Max(0, Math.Min(255, yy));
            u = (byte)Math.Max(0, Math.Min(255, uu));
            v = (byte)Math.Max(0, Math.Min(255, vv));
        }

        // Conversion from YUV to RGB
        public static void YuvToRgb(byte y, byte u, byte v, out byte r, out byte g, out byte b)
        {
            double yy = y;
            double uu = u - 128;
            double vv = v - 128;

            double rr = yy + 1.402 * vv;
            double gg = yy - 0.34414 * uu - 0.71414 * vv;
            double bb = yy + 1.772 * uu;

            r = (byte)Math.Max(0, Math.Min(255, rr));
            g = (byte)Math.Max(0, Math.Min(255, gg));
            b = (byte)Math.Max(0, Math.Min(255, bb));
        }

        // 4:2:2 downsampling - take only even columns, full height
        private static byte[,] Downsample422(byte[,] channel)
        {
            int height = channel.GetLength(0);
            int width = channel.GetLength(1);

            int newWidth = (width + 1) / 2; // half width
            byte[,] downsampled = new byte[height, newWidth];

            for (int y = 0; y < height; y++)
            {
                int newX = 0;
                for (int x = 0; x < width; x += 2) // only even columns
                {
                    downsampled[y, newX] = channel[y, x];
                    newX++;
                }
            }

            return downsampled;
        }

        // 4:2:2 upsampling - restore width by duplicating each column
        private static byte[,] Upsample422(byte[,] channel, int targetHeight, int targetWidth)
        {
            int height = channel.GetLength(0);
            int width = channel.GetLength(1);

            byte[,] upsampled = new byte[targetHeight, targetWidth];

            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    int srcX = x / 2; // every 2 pixels horizontally use the same chroma value
                    srcX = Math.Min(srcX, width - 1);

                    upsampled[y, x] = channel[y, srcX];
                }
            }

            return upsampled;
        }

        public static void SaveMms(Bitmap bitmap, string filePath)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            // Creating YUV channels
            byte[,] yChannel = new byte[height, width];
            byte[,] uChannel = new byte[height, width];
            byte[,] vChannel = new byte[height, width];

            // RGB to YUV conversion
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    RgbToYuv(pixel.R, pixel.G, pixel.B, out byte yVal, out byte uVal, out byte vVal);

                    yChannel[y, x] = yVal;
                    uChannel[y, x] = uVal;
                    vChannel[y, x] = vVal;
                }
            }

            // 4:2:2 downsampling of U and V channels
            byte[,] uDownsampled = Downsample422(uChannel);
            byte[,] vDownsampled = Downsample422(vChannel);

            // Writing to file
            using (BinaryWriter writer = new BinaryWriter(File.Create(filePath)))
            {
                // Header
                writer.Write("MMS"); // Magic number
                writer.Write(width);
                writer.Write(height);

                // Y channel (full resolution)
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        writer.Write(yChannel[y, x]);
                    }
                }

                // U channel (4:2:2 downsampled)
                int uHeight = uDownsampled.GetLength(0);
                int uWidth = uDownsampled.GetLength(1);
                writer.Write(uHeight);
                writer.Write(uWidth);

                for (int y = 0; y < uHeight; y++)
                {
                    for (int x = 0; x < uWidth; x++)
                    {
                        writer.Write(uDownsampled[y, x]);
                    }
                }

                // V channel (4:2:2 downsampled)
                int vHeight = vDownsampled.GetLength(0);
                int vWidth = vDownsampled.GetLength(1);
                writer.Write(vHeight);
                writer.Write(vWidth);

                for (int y = 0; y < vHeight; y++)
                {
                    for (int x = 0; x < vWidth; x++)
                    {
                        writer.Write(vDownsampled[y, x]);
                    }
                }
            }
        }

        public static Bitmap LoadMms(string filePath)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(filePath)))
            {
                // Reading header
                string magic = reader.ReadString();
                if (magic != "MMS")
                {
                    throw new InvalidDataException("Not a valid MMS file");
                }

                int width = reader.ReadInt32();
                int height = reader.ReadInt32();

                // Reading Y channel
                byte[,] yChannel = new byte[height, width];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        yChannel[y, x] = reader.ReadByte();
                    }
                }

                // Reading U channel
                int uHeight = reader.ReadInt32();
                int uWidth = reader.ReadInt32();
                byte[,] uDownsampled = new byte[uHeight, uWidth];

                for (int y = 0; y < uHeight; y++)
                {
                    for (int x = 0; x < uWidth; x++)
                    {
                        uDownsampled[y, x] = reader.ReadByte();
                    }
                }

                // Reading V channel
                int vHeight = reader.ReadInt32();
                int vWidth = reader.ReadInt32();
                byte[,] vDownsampled = new byte[vHeight, vWidth];

                for (int y = 0; y < vHeight; y++)
                {
                    for (int x = 0; x < vWidth; x++)
                    {
                        vDownsampled[y, x] = reader.ReadByte();
                    }
                }

                // 4:2:2 upsampling of U and V channels
                byte[,] uChannel = Upsample422(uDownsampled, height, width);
                byte[,] vChannel = Upsample422(vDownsampled, height, width);

                // Creating bitmap
                Bitmap bitmap = new Bitmap(width, height);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        YuvToRgb(yChannel[y, x], uChannel[y, x], vChannel[y, x],
                                out byte r, out byte g, out byte b);
                        bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                return bitmap;
            }
        }

        // Method for loading any supported format
        public static Bitmap LoadImage(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            if (extension == ".mms")
            {
                return LoadMms(filePath);
            }
            else
            {
                // Standard formats (jpg, png, jpeg, bmp, etc.)
                return new Bitmap(filePath);
            }
        }
    }
}