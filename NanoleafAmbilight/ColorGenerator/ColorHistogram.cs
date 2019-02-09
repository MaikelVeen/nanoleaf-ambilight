using System;
using System.Drawing;

namespace NanoleafAmbilight.ColorGenerator
{
    public class ColorHistogram
    {
        public ColorHistogram(uint[] pixels)
        {
            Array.Sort(pixels);
            int numberOfColors = CountDistinctColors(pixels);

            Colors = new uint[numberOfColors];
            ColorCounts = new uint[numberOfColors];
            CountColorFrequencies(pixels);
        }
        
        /// <summary>
        /// The distinct colors in the histogram
        /// </summary>
        public uint[] Colors { get; set; }
        
        /// <summary>
        /// The frequency of each color
        /// </summary>
        public uint[] ColorCounts { get; set; }
  
        private int CountDistinctColors(uint[] pixels)
        {
            if (pixels.Length < 2)
            {
                return pixels.Length;
            }

            int colorCount = 1;
            uint currentColor = pixels[0];

            foreach (uint pixel in pixels)
            {
                if (pixel == currentColor) continue;
                currentColor = pixel;
                colorCount++;
            }

            return colorCount;
        }

        private void CountColorFrequencies(uint[] pixels)
        {
            if (pixels.Length == 0)
            {
                return;
            }

            int currentColorIndex = 0;
            uint currentColor = pixels[0];

            Colors[currentColorIndex] = currentColor;
            ColorCounts[currentColorIndex] = 1;

            if (pixels.Length == 1)
            {
                return;
            }

            foreach (uint pixel in pixels)
            {
                if (pixel == currentColor)
                {
                    ColorCounts[currentColorIndex]++;
                }
                else
                {
                    currentColor = pixel;
                    currentColorIndex++;
                    Colors[currentColorIndex] = currentColor;
                    ColorCounts[currentColorIndex] = 1;
                }
            }
        }
        
   
    }
}