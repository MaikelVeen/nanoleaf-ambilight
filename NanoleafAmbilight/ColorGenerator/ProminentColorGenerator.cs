using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NanoleafAmbilight.ColorGenerator
{
    public class ProminentColorGenerator
    {
        private class ColorBucket
        {
            public List<HSLColor> colors;

            public ColorBucket()
            {
                colors = new List<HSLColor>();
            }
        }

        public Color GetColor(Bitmap source)
        {
            List<Color> sourcePixels = GetBitmapColors(source);
            uint[] integerRepresentation = new uint[sourcePixels.Count];

            for (int i = 0; i < sourcePixels.Count; i++)
            {
                integerRepresentation[i] = ColorToUInt(sourcePixels[i]);
            }
            
            Array.Sort(integerRepresentation);
            SaveColorRange(integerRepresentation);
            HSLColor[] hslColors = ColorUtils.RGBtoHSL(sourcePixels).ToArray();

            // Sort hsl color
            Array.Sort(hslColors, (x, y) => x.S.CompareTo(y.S));
            SaveColorRange(hslColors);


            List<ColorBucket> buckets = GetColorBuckets(hslColors);
            float[] averageImportance = new float[buckets.Count];

            for (int i = 0; i < buckets.Count; i++)
            {
                averageImportance[i] = GetAverageImportance(buckets[i]);
            }

            ColorBucket mostProminent = buckets[GetIndexOfMaxImportance(averageImportance)];
            return GetAverageRGBofBucket(mostProminent);
        }
        
        private uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8)  | (color.B << 0));
        }
        
        private Color UIntToColor(uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(a, r, g, b);
        }

        private void SaveColorRange(HSLColor[] array)
        {
            Color[] colors = ColorUtils.HSLtoRGB(array).ToArray();

            Bitmap newBitmap = new Bitmap(500, 3 * colors.Length -1);

            int colorIndex = 0;
            for (int x = 0; x < newBitmap.Width; x++)
            {
                int intermediateCount = 0;
                colorIndex = 0;
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
            newBitmap.Dispose();
        }
        
        private void SaveColorRange(uint[] array)
        {
          
            Bitmap newBitmap = new Bitmap(500, 3 * array.Length -1);

            int colorIndex = 0;
            for (int x = 0; x < newBitmap.Width; x++)
            {
                int intermediateCount = 0;
                colorIndex = 0;
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    intermediateCount++;
                    newBitmap.SetPixel(x, y, UIntToColor(array[colorIndex]));
                    if (intermediateCount < 3) continue;
                    colorIndex++;
                    
                    intermediateCount = 0;
                }
            }

            newBitmap.Save("colors2.jpg");
            newBitmap.Dispose();
        }

        private int GetIndexOfMaxImportance(float[] values)
        {
            int index = 0;
            float max = values[0];
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                    index = i;
                }
            }

            return index;
        }

        private List<Color> GetBitmapColors(Bitmap bitmap)
        {
            List<Color> colors = new List<Color>();
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    colors.Add(bitmap.GetPixel(x, y));
                }
            }

            return colors;
        }

        private List<ColorBucket> GetColorBuckets(HSLColor[] allColors)
        {
            int numberOfBuckets = allColors.Length / 5;
            int colorPerBucket = allColors.Length / numberOfBuckets;

            List<ColorBucket> colorBuckets = new List<ColorBucket>();
            for (int i = 0; i < numberOfBuckets; i++)
            {
                ColorBucket bucket = new ColorBucket();


                for (int j = 0; j < colorPerBucket; j++)
                {
                    bucket.colors.Add(allColors[j]);
                }

                colorBuckets.Add(bucket);
            }

            return colorBuckets;
        }

        private float GetAverageImportance(ColorBucket bucket)
        {
            int numberOfElements = bucket.colors.Count;
            float totalImportance = 0;
            foreach (HSLColor color in bucket.colors)
            {
                float importance = (1.0f - Math.Abs(color.L - 0.5f) * 2.0f) * color.S + 0.5f;
                totalImportance += importance;
            }

            return totalImportance;
        }

        private Color GetAverageRGBofBucket(ColorBucket bucket)
        {
            Color[] rgbColors = ColorUtils.HSLtoRGB(bucket.colors).ToArray();
            int r = 0;
            int b = 0;
            int g = 0;

            foreach (Color color in rgbColors)
            {
                r += color.R;
                b += color.B;
                g += color.G;
            }

            return Color.FromArgb(255, r / rgbColors.Length, g / rgbColors.Length, b / rgbColors.Length);
        }
    }
}