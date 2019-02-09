using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;


namespace NanoleafAmbilight.ScreenCapture
{
    public class ScreenDuplicater
    {
        public static Bitmap GetCurrentScreen(int adapterNumber = 0, int output = 0)
        {
            Factory1 factory = new Factory1();
            Adapter adapter = factory.GetAdapter1(adapterNumber);

            Device device = new Device(adapter);
            

            throw new NotImplementedException();
        }
    }
}