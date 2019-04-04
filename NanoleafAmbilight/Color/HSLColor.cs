namespace NanoleafAmbilight.Color
{
    public class HSLColor
    {
        public HSLColor(float h, float s, float l)
        {
            H = h;
            S = s;
            L = l;
        }

        public float H { get; set; }
        public float S { get; set; }
        public float L { get; set; }
    }
}