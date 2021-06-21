using CUE.NET.Devices.Generic;

namespace beatCUE
{
    static class Extensions
    {
        public static UnityEngine.Color Ify(this UnityEngine.Color color)
        {
            float H = 0;
            float S = 0;
            float V = 0;
            UnityEngine.Color.RGBToHSV(color, out H, out S, out V);
            V = color.a * 255;
            return UnityEngine.Color.HSVToRGB(H, S, V);
        }
        public static CorsairColor ToCorsair(this UnityEngine.Color color)
        {
            return new CorsairColor((byte)color.r, (byte)color.g, (byte)color.b);
        }
    }
}
