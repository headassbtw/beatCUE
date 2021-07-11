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
            color.Ify();
            var orgbcolor = color.FromUnity();
            var colors = new Color[Plugin.Devices[device].Zones[zone].LedCount];
            var colorss = new List<Color>();
            for (int i = 0; i < Plugin.Devices[device].Zones[zone].LedCount; i++)
            {
                colorss.Add(orgbcolor);
            }
            colors = colorss.ToArray();
            switch (Plugin.Devices[device].Zones[zone].Type)
            {
                case ZoneType.Linear:
                    //var colors = Color.GetHueRainbow((int)Plugin.Devices[device].Zones[row].LedCount);
                    Plugin.Client.UpdateZone(device, zone, colors);
                    break;
                case ZoneType.Single:
                    Plugin.Client.UpdateZone(device, zone, colors);
                    break;
                case ZoneType.Matrix:
                    var yeet = 2 * Math.PI / Plugin.Devices[device].Zones[zone].MatrixMap.Width;
                    var rainbow = Color.GetHueRainbow((int)Plugin.Devices[device].Zones[zone].MatrixMap.Width).ToArray();
                    //var rainbow = Color.GetSinRainbow((int)zone.MatrixMap.Width).ToArray();

                    var matrix = Enumerable.Range(0, (int)Plugin.Devices[device].Zones[zone].LedCount).Select(__ => new Color()).ToArray();
                    for (int k = 0; k < Plugin.Devices[device].Zones[zone].MatrixMap.Width; k++)
                    {
                        for (int l = 0; l < Plugin.Devices[device].Zones[zone].MatrixMap.Height; l++)
                        {
                            var index = Plugin.Devices[device].Zones[zone].MatrixMap.Matrix[l, k];
                            if (index != uint.MaxValue)
                            {
                                matrix[index] = rainbow[k].Clone();
                            }
                        }
                    }
                    Plugin.Client.UpdateZone(device, zone, matrix);
                    break;
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