﻿using System;
using System.Diagnostics;
using System.Drawing;
using NanoleafAmbilight.ColorGenerator;
using NanoleafAmbilight.ScreenCapture;

namespace NanoleafAmbilight
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Test code for scaling down of bitmap
            Bitmap test = Bitmap.FromFile("C:/Users/Maikel/AppData/Roaming/.minecraft/screenshots/2019-02-04_13.45.25.png") as Bitmap;
            Bitmap newB = BitmapScaler.ScaleBitmapDown(test);
            //newB.Save("test.jpg");
            ProminentColorGenerator gen = new ProminentColorGenerator();
            Color rgb = gen.GetColor(newB);
            stopwatch.Stop();;
            Console.WriteLine(rgb.ToString() +  $"returned in {stopwatch.ElapsedMilliseconds}");
        }
    }
}