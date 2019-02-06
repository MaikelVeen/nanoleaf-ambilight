using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NanoleafAmbilight.ScreenCapture
{
    public static class BitmapScaler
    {
        private const int MINIMUM_DIMENSION = 100;
        
        /// <summary>
        /// Scales a bitmap down to minimum dimensions.
        /// Small bitmap will be returned for manipulation
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap ScaleBitmapDown(Bitmap bitmap)
        {
            int minimumDimension = Math.Min(bitmap.Width, bitmap.Height);
            if (minimumDimension <= MINIMUM_DIMENSION)
            {
                return bitmap;
            }

            float scaleRatio = MINIMUM_DIMENSION / (float) minimumDimension;
            Bitmap newBitmap = new Bitmap(bitmap, (int) Math.Round(bitmap.Width * scaleRatio),
                (int) Math.Round(bitmap.Height * scaleRatio));
            return newBitmap;
        }
    }
}