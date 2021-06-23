using HarmonyLib;
using UnityEngine;
using static BeatmapSaveData;
using static beatCUE.Lighting.KeyboardGroupLighting;
using beatCUE.Testing;

namespace beatCUE.Harmony_Patches
{
    //[HarmonyPatch(typeof(LightSwitchEventEffect))]
    //[HarmonyPatch("SetColor")]
    public class KeyboardLightPatch
    {

        
        internal static void Postfix(BeatmapEventType ____event, Color color)
        {
            color.Ify();
            
            LightingTest.keyboard.KeyboardAlphanumeric(color, ____event);
            LightingTest.keyboard.KeyboardFunctionRow(color, ____event);
            LightingTest.keyboard.KeyboardInbetween(color, ____event);
            LightingTest.keyboard.KeyboardNumpad(color, ____event);
        }
    }
}
