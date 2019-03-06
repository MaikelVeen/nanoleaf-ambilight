using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using NanoleafAmbilight.Color;
using NanoleafAmbilight.Nanoleaf;
using NanoleafAmbilight.ScreenCapture;

namespace NanoleafAmbilight
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            HSBColor color = new HSBColor(39, 99, 99);

            NanoleafClient nanoleafClient = new NanoleafClient("http://192.168.192.50:16021");
            nanoleafClient.Start();
        }
    }
}