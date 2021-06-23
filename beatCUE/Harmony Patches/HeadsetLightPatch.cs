using HarmonyLib;
using UnityEngine;
using static BeatmapSaveData;

namespace beatCUE.Harmony_Patches
{
    public class HeadsetLightPatch
    {
        static void Postfix(BeatmapEventType ____event, Color color)
        {
            color.Ify();
            Lighting.HeadsetGroupLighting.HeadsetLeft(color, ____event);
            Lighting.HeadsetGroupLighting.HeadsetRight(color, ____event);
        }
    }
}
