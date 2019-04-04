using System;

namespace NanoleafAmbilight.Color
{
    public class HSBColor
    {
        public HSBColor(int hue, int saturation, int brightness)
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
        }

        public HSBColor()
        {
            Random random = new Random();
            Hue = random.Next(0, 359);
            Saturation = random.Next(0, 100);
            Brightness = 100;
        }

        public int Hue { get; set; }
        public int Saturation { get; set; }
        public int Brightness { get; set; }
    }
}