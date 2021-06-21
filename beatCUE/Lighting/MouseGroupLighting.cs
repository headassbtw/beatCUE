using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Mouse;
using static BeatmapSaveData;

namespace beatCUE.Lighting
{
    class MouseGroupLighting
    {
        internal static CorsairMouse mouse = CueSDK.MouseSDK;
        internal static void MouseLights(UnityEngine.Color color, BeatmapEventType ev)
        {
            if (ev.Equals(Configuration.PluginConfig.Instance.Mouse) && mouse != null)
            {
                color = color.Ify();
                mouse.Brush = (SolidColorBrush)CorsairColor.Transparent;
                mouse[mouse.GetLeds().ToList().ElementAt(0).Id].Color = color.ToCorsair();
                mouse[mouse.GetLeds().ToList().ElementAt(1).Id].Color = color.ToCorsair();
                mouse.Update();
            }
        }
    }
}
