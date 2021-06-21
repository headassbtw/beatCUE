using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
using static BeatmapSaveData;
using static beatCUE.Lighting.KeyboardGroupLighting;

/// <summary>
/// See https://github.com/pardeike/Harmony/wiki for a full reference on Harmony.
/// </summary>
namespace beatCUE.Harmony_Patches
{
    
    [HarmonyPatch(typeof(LightSwitchEventEffect))]
    [HarmonyPatch("SetColor")]
    public class FloorLightPatch
    {

        [HarmonyAfter(new string[] { "com.noodle.BeatSaber.Chroma" })]
        static void Postfix(BeatmapEventType ____event, Color color)
        {
            color.Ify();
            switch (____event)
            {
                case BeatmapEventType.Event4:
                    Testing.LightingTest.MouseLights(color, 0);
                    break;
                case BeatmapEventType.Event3:
                    Testing.LightingTest.keyboard.KeyboardAlphanumeric(color);
                    break;
                case BeatmapEventType.Event2:
                    Testing.LightingTest.keyboard.KeyboardInbetween(color);
                    break;
                case BeatmapEventType.Event1:
                    Testing.LightingTest.keyboard.KeyboardFunctionRow(color);
                    Testing.LightingTest.keyboard.KeyboardLogoRow(color);
                    break;
                case BeatmapEventType.Event0:
                    Testing.LightingTest.keyboard.KeyboardNumpad(color);
                    break;
            }
        }
    }
}