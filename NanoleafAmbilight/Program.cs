using System;
using System.Diagnostics;
using System.Drawing;
using NanoleafAmbilight.Color;
using NanoleafAmbilight.ScreenCapture;

namespace NanoleafAmbilight
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Bitmap loadedBitmap =
                Image.FromFile("C:/Users/Maikel/Documents/Work/upWork/Noted Media/Images/5-29-18-a1f.jpg") as Bitmap;

            Bitmap scaleDown = loadedBitmap.ScaleDown();
            scaleDown?.Save("test.jpg");
            stopwatch.Stop();
            Console.WriteLine($"Milliseconds elapsed: {stopwatch.ElapsedMilliseconds}");
        }
    }
}