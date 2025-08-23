using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MatrixMixer
{

 
    public static class MyImageFormat
    {
        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        public static void Save(Bitmap bmp, string filename)
        {
            int height = bmp.Height;
            int width = bmp.Width;

           
            byte[,] Y = new byte[height, width];
            
            byte[,] U = new byte[(height + 1) / 2, (width + 1) / 2];
            byte[,] V = new byte[(height + 1) / 2, (width + 1) / 2];

            double[,] tempU = new double[height, width];
            double[,] tempV = new double[height, width];

            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    double r = c.R;
                    double g = c.G;
                    double b = c.B;

                    double yVal = 0.299 * r + 0.587 * g + 0.114 * b;
                    double uVal = -0.168736 * r - 0.331264 * g + 0.5 * b + 128;
                    double vVal = 0.5 * r - 0.418688 * g - 0.081312 * b + 128;

                    Y[y, x] = (byte)Clamp((int)yVal, 0, 255);
                    tempU[y, x] = uVal;
                    tempV[y, x] = vVal;
                }
            }

            //4:2:0 2
            for (int y = 0; y < height; y += 2)
            {
                for (int x = 0; x < width; x += 2)
                {
                    int row = y / 2;
                    int col = x / 2;

                    double uVal = tempU[y, x];
                    double vVal = tempV[y, x];

                    if (x + 1 < width)
                    {
                        uVal = (uVal + tempU[y, x + 1]) / 2.0;
                        vVal = (vVal + tempV[y, x + 1]) / 2.0;
                        U[row, col] = (byte)Clamp((int)uVal, 0, 255);
                        V[row, col] = (byte)Clamp((int)vVal, 0, 255);
                    }
                }
            }

            
            using (BinaryWriter bw = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                bw.Write(width);
                bw.Write(height);

                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        bw.Write(Y[y, x]);

                for (int y = 0; y < U.GetLength(0); y++)
                    for (int x = 0; x < U.GetLength(1); x++)
                        bw.Write(U[y, x]);

                for (int y = 0; y < V.GetLength(0); y++)
                    for (int x = 0; x < V.GetLength(1); x++)
                        bw.Write(V[y, x]);
            }
        }


        public static Bitmap Load(string filename)
        {
            using (BinaryReader br = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                int width = br.ReadInt32();
                int height = br.ReadInt32();

                byte[,] Y = new byte[height, width];
                byte[,] U = new byte[(height + 1) / 2, (width + 1) / 2];
                byte[,] V = new byte[(height + 1) / 2, (width + 1) / 2];

        
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        Y[y, x] = br.ReadByte();

                for (int y = 0; y < U.GetLength(0); y++)
                    for (int x = 0; x < U.GetLength(1); x++)
                        U[y, x] = br.ReadByte();

                for (int y = 0; y < V.GetLength(0); y++)
                    for (int x = 0; x < V.GetLength(1); x++)
                        V[y, x] = br.ReadByte();

                Bitmap bmp = new Bitmap(width, height);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte yVal = Y[y, x];

               
                        int uRow = y / 2;
                        int uCol = x / 2;
                        int vRow = y / 2;
                        int vCol = x / 2;

                        uRow = Math.Min(uRow, U.GetLength(0) - 1);
                        uCol = Math.Min(uCol, U.GetLength(1) - 1);
                        vRow = Math.Min(vRow, V.GetLength(0) - 1);
                        vCol = Math.Min(vCol, V.GetLength(1) - 1);

                        byte uVal = U[uRow, uCol];
                        byte vVal = V[vRow, vCol];

                        double c = yVal - 16;
                        double d = uVal - 128;
                        double e = vVal - 128;

                        int r = Clamp(((int)(298 * c + 409 * e + 128) >> 8), 0, 255);
                        int g = Clamp(((int)(298 * c - 100 * d - 208 * e + 128) >> 8), 0, 255);
                        int b = Clamp(((int)(298 * c + 516 * d + 128) >> 8), 0, 255);

                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                return bmp;
            }
        }


        public static Bitmap ApplyGaussianFilterFast(Bitmap source , int n)
        {
            _3x3IntMatrix m = new _3x3IntMatrix(1, 2, 1, 2, n, 2, 1, 2, 1);
            return Convolution3x3(m, source, TypeOfFilter.Gaus); 
        }

        public static Bitmap ApplyBlacklight(Bitmap source, int fxWeight)
        {
            int width = source.Width;
            int height = source.Height;
            Bitmap result = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData srcData = source.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            BitmapData dstData = result.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);

            int stride = srcData.Stride;
            int bytes = stride * height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);
            source.UnlockBits(srcData);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * stride + x * 3;

                    byte b = srcBuffer[index + 0];
                    byte g = srcBuffer[index + 1];
                    byte r = srcBuffer[index + 2];

                    int L = (222 * r + 707 * g + 71 * b) / 1000;
                    int R = Math.Abs(r - L) * fxWeight;
                    int G = Math.Abs(g - L) * fxWeight;
                    int B = Math.Abs(b - L) * fxWeight;

                    dstBuffer[index + 0] = (byte)Clamp(B, 0, 255);
                    dstBuffer[index + 1] = (byte)Clamp(G, 0, 255);
                    dstBuffer[index + 2] = (byte)Clamp(R, 0, 255);
                }
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);
            result.UnlockBits(dstData);

            return result;
        }


        public static Bitmap ApplyMeanRemoval(Bitmap source, int n)
        {
            _3x3IntMatrix m = new _3x3IntMatrix(-1, -1, -1, -1, n, -1, -1, -1, -1);
            return Convolution3x3(m, source, TypeOfFilter.Mean);           
        }
        public static Bitmap Convolution3x3(_3x3IntMatrix m , Bitmap source , TypeOfFilter filter) 
        {
            int width = source.Width;
            int height = source.Height;
            Bitmap result = new Bitmap(width, height, PixelFormat.Format24bppRgb);


            int[,] kernel = {
            {m.a00, m.a01, m.a02},
            {m.a10, m.a11, m.a12},
            {m.a20, m.a21, m.a22}
            };
            int kernelSum = m.a11 + (int)filter;


            BitmapData srcData = source.LockBits(new Rectangle(0, 0, width, height),
                                                 ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = result.LockBits(new Rectangle(0, 0, width, height),
                                                 ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int stride = srcData.Stride;
            int bytes = stride * height;
            byte[] srcBuffer = new byte[bytes];
            byte[] dstBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bytes);
            source.UnlockBits(srcData);


            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int r = 0, g = 0, b = 0;

                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            int pixelIndex = ((y + ky) * stride) + ((x + kx) * 3);

                            b += srcBuffer[pixelIndex + 0] * kernel[ky + 1, kx + 1];
                            g += srcBuffer[pixelIndex + 1] * kernel[ky + 1, kx + 1];
                            r += srcBuffer[pixelIndex + 2] * kernel[ky + 1, kx + 1];
                        }
                    }

                    int dstIndex = (y * stride) + (x * 3);
                    dstBuffer[dstIndex + 0] = (byte)Clamp(b / kernelSum, 0, 255);
                    dstBuffer[dstIndex + 1] = (byte)Clamp(g / kernelSum, 0, 255);
                    dstBuffer[dstIndex + 2] = (byte)Clamp(r / kernelSum, 0, 255);
                }
            }

            Marshal.Copy(dstBuffer, 0, dstData.Scan0, bytes);
            result.UnlockBits(dstData);

            return result;
        }
    }


}
