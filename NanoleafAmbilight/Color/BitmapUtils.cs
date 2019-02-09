using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace NanoleafAmbilight.Color
{
    /// <summary>
    /// Utilities for Bitmaps
    /// </summary>
    public static class BitmapUtils
    {
        private const int MinimumDimension = 50;
        
        /// <summary>
        /// Get all the colors in an bitmap as unsigned integer
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static uint[] GetBitmapColors(this Bitmap bitmap)
        {
            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bitmap.PixelFormat);

            int length = bitmapData.Stride * bitmap.Height;
            byte[] bytes = new byte[length];

            Marshal.Copy(bitmapData.Scan0, bytes, 0, length);
            bitmap.UnlockBits(bitmapData);

            uint[] pixels = new uint[bytes.Length / 4];

            for (int i = 0, j = 0; i < bytes.Length; i += 4, j++)
            {
                pixels[j] = ColorUtils.ColorToUInt(bytes[i], bytes[i + 1], bytes[i + 2], bytes[i + 3]);
            }

            return pixels;
        }
        
        /// <summary>
        /// Scales a bitmap down to minimum dimensions.
        /// Small bitmap will be returned for manipulation
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap ScaleDown(this Bitmap bitmap)
        {
            int minimumDimension = Math.Min(bitmap.Width, bitmap.Height);
            if (minimumDimension <= MinimumDimension)
            {
                return bitmap;
            }

            float scaleRatio = MinimumDimension / (float) minimumDimension;
            Bitmap newBitmap = new Bitmap(bitmap, (int) Math.Round(bitmap.Width * scaleRatio),
                (int) Math.Round(bitmap.Height * scaleRatio));
            return newBitmap;
        }

        /// <summary>
        /// Saves an array of colors to a bitmap showing the colors
        /// </summary>
        /// <param name="array"></param>
        public static void SaveColorRange(HSLColor[] array)
        {
            System.Drawing.Color[] colors = ColorUtils.HSLtoRGB(array).ToArray();

            using (Bitmap newBitmap = new Bitmap(500, 3 * colors.Length - 1))
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    int intermediateCount = 0;
                    int colorIndex = 0;
                    for (int y = 0; y < newBitmap.Height; y++)
                    {
                        intermediateCount++;
                        newBitmap.SetPixel(x, y, colors[colorIndex]);
                        if (intermediateCount < 3) continue;
                        colorIndex++;

                        intermediateCount = 0;
                    }
                }

                newBitmap.Save("colors.jpg");
            }
        }

        /// <summary>
        /// Saves an array of colors to a bitmap showing the colors
        /// </summary>
        /// <param name="array"></param>
        public static void SaveColorRange(uint[] array)
        {
            using (Bitmap newBitmap = new Bitmap(500, 3 * array.Length - 1))
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    int intermediateCount = 0;
                    int colorIndex = 0;
                    for (int y = 0; y < newBitmap.Height; y++)
                    {
                        intermediateCount++;
                        newBitmap.SetPixel(x, y, ColorUtils.UIntToColor(array[colorIndex]));
                        if (intermediateCount < 3) continue;
                        colorIndex++;

                        intermediateCount = 0;
                    }
                }

                newBitmap.Save("colors.jpg");
            }
        }
    }
}