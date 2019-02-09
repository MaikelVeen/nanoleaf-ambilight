using System;
using System.Diagnostics;
using System.Drawing;
using NanoleafAmbilight.Color;
using NanoleafAmbilight.ScreenCapture;

namespace NanoleafAmbilight
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Bitmap test = Bitmap.FromFile("C:/Users/Maikel/Documents/Work/Projects/nanoleaf-ambilight/NanoleafAmbilight/distinctColorTest2.png") as Bitmap;
           
            stopwatch.Stop();;
            Console.WriteLine(  $"returned in {stopwatch.ElapsedMilliseconds}");
        }
    }
}