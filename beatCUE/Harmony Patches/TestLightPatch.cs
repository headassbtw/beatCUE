using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using OpenRGB.NET.Enums;
using OpenRGB.NET.Models;
//using UnityEngine;
using static BeatmapSaveData;
using static beatCUE.UI.Controllers.DeviceController;
namespace beatCUE.Harmony_Patches
{
    public class TestLightPatch
    {
        internal static int zone;
        internal static int device;
        internal static void SHITFUCK(UnityEngine.Color color)
        {
            //color.Ify();
            var orgbcolor = color.FromUnity();
            var colors = new Color[Plugin.Devices[device].Zones[zone].LedCount];
            var colorss = new List<Color>();
            for (int i = 0; i < Plugin.Devices[device].Zones[zone].LedCount; i++)
            {
                colorss.Add(orgbcolor);
            }
            colors = colorss.ToArray();
            for (int di = 0; di < Plugin.Client.GetControllerCount(); di++)
            {
                var Device = Plugin.Client.GetControllerData(di);
                //openRGBColors = new OpenRGB.NET.Models.Color[device.Colors.Length];
                for (int i = 0; i < colors.Length; i++)
                    colors[i] = color.FromUnity();

                Plugin.Client.UpdateLeds(di, colors);
            }
        }
        static void Postfix(BeatmapEventType ____event, UnityEngine.Color color)
        {
            if (____event == BeatmapEventType.Event1)
            {
                SHITFUCK(color);
            }
        }
    }
}