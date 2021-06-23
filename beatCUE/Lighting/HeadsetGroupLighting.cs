using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Headset;
using static BeatmapSaveData;

namespace beatCUE.Lighting
{
    class HeadsetGroupLighting
    {
        internal static CorsairHeadset headset = CueSDK.HeadsetSDK;
        internal static void HeadsetLeft(UnityEngine.Color color, BeatmapEventType ev)
        {
            if (ev.Equals(Configuration.PluginConfig.Instance.HeadsetLeft) && headset != null)
            {
                color = color.Ify();
                headset.Brush = (SolidColorBrush)CorsairColor.Transparent;
                headset[headset.GetLeds().ToList().ElementAt(0).Id].Color = color.ToCorsair();
                headset.Update();
            }
        }
        internal static void HeadsetRight(UnityEngine.Color color, BeatmapEventType ev)
        {
            if (ev.Equals(Configuration.PluginConfig.Instance.HeadsetRight) && headset != null)
            {
                color = color.Ify();
                headset.Brush = (SolidColorBrush)CorsairColor.Transparent;
                headset[headset.GetLeds().ToList().ElementAt(1).Id].Color = color.ToCorsair();
                headset.Update();
            }
        }
    }
}
