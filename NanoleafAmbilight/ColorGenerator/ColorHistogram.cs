using System.Drawing;

namespace NanoleafAmbilight.ColorGenerator
{
    public class ColorHistogram
    {
        public ColorHistogram()
        {
        }

        public Color[] Colors { get; set; }
        public int[] ColorCounts { get; set; }
        public int NumberOfColors { get; set; }
        
        
    }
}