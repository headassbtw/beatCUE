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
            Testing.LightingTest.keyboard.KeyboardAlphanumeric(color, ____event);
            Testing.LightingTest.keyboard.KeyboardFunctionRow(color, ____event);
            Testing.LightingTest.keyboard.KeyboardInbetween(color, ____event);
            Testing.LightingTest.keyboard.KeyboardNumpad(color, ____event);
            Lighting.MouseGroupLighting.MouseLights(color, ____event);
        }
    }
}