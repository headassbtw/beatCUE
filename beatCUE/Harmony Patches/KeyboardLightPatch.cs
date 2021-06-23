using HarmonyLib;
using UnityEngine;
using static BeatmapSaveData;
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
            Lighting.KeyboardGroupLighting.KeyboardAlphanumeric(color, ____event);
            Lighting.KeyboardGroupLighting.KeyboardFunctionRow(color, ____event);
            Lighting.KeyboardGroupLighting.KeyboardInbetween(color, ____event);
            Lighting.KeyboardGroupLighting.KeyboardNumpad(color, ____event);
        }
    }
}
