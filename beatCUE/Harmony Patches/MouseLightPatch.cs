using HarmonyLib;
using UnityEngine;
using static BeatmapSaveData;

namespace beatCUE.Harmony_Patches
{
    public class MouseLightPatch
    {
        static void Postfix(BeatmapEventType ____event, Color color)
        {
            color.Ify();
        }
    }
}
