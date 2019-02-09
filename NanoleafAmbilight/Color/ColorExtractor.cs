using System;
using System.Collections.Generic;


namespace NanoleafAmbilight.Color
{
    public static class ColorExtractor
    {
        public static System.Drawing.Color GetColor(uint[] colors)
        {
            throw new NotImplementedException();
        }

        private static System.Drawing.Color[] QuantizePixels(ColorHistogram colorHistogram)
        {
            List<System.Drawing.Color> rgb = new List<System.Drawing.Color>();

            foreach (uint t in colorHistogram.Colors)
            {
                System.Drawing.Color color = ColorUtils.UIntToColor(t);
                if (color != System.Drawing.Color.Black && color != System.Drawing.Color.White)
                {
                    rgb.Add(color);
                }
            }


            return null;
        }

        // TODO implement iterative case for median cut
        
        /// <summary>
        /// Returns which color component has the biggest range,
        /// 0 for red, 1 for green, 2 for blue
        /// </summary>
        /// <param name="colorBucket"></param>
        /// <returns></returns>
        private static int FindGreatestRangeDimension(IEnumerable<System.Drawing.Color> colorBucket)
        {
            int maxGreen = int.MaxValue;
            int maxRed = int.MaxValue;
            int maxBlue = int.MaxValue;
            int minGreen = int.MinValue;
            int minRed = int.MinValue;
            int minBlue = int.MinValue;

            foreach (System.Drawing.Color color in colorBucket)
            {
                minRed = Math.Min(color.R, minRed);
                maxRed = Math.Max(color.R, maxRed);

                minGreen = Math.Min(color.G, minGreen);
                maxGreen = Math.Max(color.G, maxGreen);

                minBlue = Math.Min(color.B, minBlue);
                maxBlue = Math.Max(color.B, maxBlue);
            }

            int redRange = maxRed - minRed;
            int greenRange = maxGreen - minGreen;
            int blueRange = maxBlue - minBlue;

            int biggestRange = Math.Max(redRange, Math.Max(greenRange, blueRange));
            if (biggestRange == redRange)
            {
                return 0;
            }

            return biggestRange == greenRange ? 1 : 2;
        }
    }
}