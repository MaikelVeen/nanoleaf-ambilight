using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NanoleafAmbilight.ColorGenerator
{
    public class ColorUtils
    {
        /// <summary>
        /// Delegate that describes color transformation
        /// </summary>
        /// <param name="input"></param>
        /// <typeparam name="T">The type of the output</typeparam>
        /// <typeparam name="T2">The type of the input</typeparam>
        private delegate T Transform<out T, in T2>(T2 input);

        /// <summary>
        /// Generic helper function for color transformation classes
        /// Goes through each element and transforms it. 
        /// </summary>
        /// <param name="inputCollection"></param>
        /// <param name="transformation"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        private static IEnumerable<T> Transformation<T, T2>(IEnumerable<T2> inputCollection,
            Transform<T, T2> transformation)
        {
            return inputCollection.Select(value => transformation(value)).ToList();
        }

        /// <summary>
        /// Converts collection of RGB colors to HSL colors
        /// </summary>
        /// <param name="rgbColors"></param>
        /// <returns></returns>
        public static IEnumerable<HSLColor> RGBtoHSL(IEnumerable<Color> rgbColors)
        {
            return Transformation(rgbColors, RGBToHSLpixel);
        }

        /// <summary>
        /// Converts collection of HSL colors to RGB colors
        /// </summary>
        /// <param name="hslColors"></param>
        /// <returns></returns>
        public static IEnumerable<Color> HSLtoRGB(IEnumerable<HSLColor> hslColors)
        {
            return Transformation(hslColors, HslToRGBpixel);
        }

       /* public static uint[] ColorToUints(Color[] color)
        {
            
        }*/

        /// <summary>
        /// Converts a RGB to a HSL pixel
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static HSLColor RGBToHSLpixel(Color color)
        {
            float normalizedR = (color.R / 255f);
            float normalizedG = (color.G / 255f);
            float normalizedB = (color.B / 255f);

            float min = Math.Min(Math.Min(normalizedR, normalizedG), normalizedB);
            float max = Math.Max(Math.Max(normalizedR, normalizedG), normalizedB);
            float delta = max - min;

            float h = 0;
            float s = 0;
            float l = ((max + min) / 2.0f);

            if (delta == 0) return new HSLColor(h, s, l);
            if (l < 0.5f)
            {
                s = (delta / (max + min));
            }
            else
            {
                s = (delta / (2.0f - max - min));
            }

            if (normalizedR == max)
            {
                h = (normalizedG - normalizedB) / delta;
            }
            else if (normalizedG == max)
            {
                h = 2f + (normalizedB - normalizedR) / delta;
            }
            else if (normalizedB == max)
            {
                h = 4f + (normalizedR - normalizedG) / delta;
            }

            return new HSLColor(h, s, l);
        }

        /// <summary>
        /// Converts a HSL color value to RGB color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color HslToRGBpixel(HSLColor color)
        {
            float Saturation = color.S;
            float Luminosity = color.L;
            float Hue = color.H;

            byte r, g, b;
            if (Saturation == 0)
            {
                r = (byte) Math.Round(Luminosity * 255d);
                g = (byte) Math.Round(Luminosity * 255d);
                b = (byte) Math.Round(Luminosity * 255d);
            }
            else
            {
                double t2;
                double th = Hue / 6.0d;

                if (Luminosity < 0.5d)
                {
                    t2 = Luminosity * (1d + Saturation);
                }
                else
                {
                    t2 = (Luminosity + Saturation) - (Luminosity * Saturation);
                }

                double t1 = 2d * Luminosity - t2;

                double tr = th + (1.0d / 3.0d);
                double tg = th;
                double tb = th - (1.0d / 3.0d);

                tr = ColorCalculation(tr, t1, t2);
                tg = ColorCalculation(tg, t1, t2);
                tb = ColorCalculation(tb, t1, t2);
                
                r = (byte) Math.Round(tr * 255d);
                g = (byte) Math.Round(tg * 255d);
                b = (byte) Math.Round(tb * 255d);
            }

            return Color.FromArgb(r, g, b);
        }
        
        /// <summary>
        /// Helper function for HSL to RGB conversion
        /// </summary>
        /// <param name="c"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        private static double ColorCalculation(double c, double t1, double t2)
        {
            if (c < 0) c += 1d;
            if (c > 1) c -= 1d;
            if (6.0d * c < 1.0d) return t1 + (t2 - t1) * 6.0d * c;
            if (2.0d * c < 1.0d) return t2;
            if (3.0d * c < 2.0d) return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
            return t1;
        }
    }
}