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
        }
    }
}
