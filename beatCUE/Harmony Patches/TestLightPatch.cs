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

            
        }

        static void Postfix(BeatmapEventType ____event, UnityEngine.Color color)
        {
        }
    }
}