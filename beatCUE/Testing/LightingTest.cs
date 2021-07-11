using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using static beatCUE.Extensions;
using HarmonyLib;

namespace beatCUE.Testing
{
    class LightingTest
    {
        internal static void Setup()
        {
            var AfterMods = new[] { "com.noodle.BeatSaber.Chroma", "com.noodle.BeatSaber.Technicolor" };
            var targetMethod = AccessTools.Method(typeof(LightSwitchEventEffect), "SetColor");

            var devices = Plugin.Devices;
            Plugin.Log.Notice("Corsair Devices:");
            foreach(var device in devices)
            {
                Plugin.Log.Notice($"{device.Name} is a {device.Type.ToString()} with {device.Leds.Count()} LEDs");
            }
            /*if(keyboard != null)
            {
                Plugin.Log.Notice("Keyboard was found, patching methods");
                Lighting.KeyboardGroupLighting.InitLEDZones(keyboard);
                var postfix = AccessTools.Method(typeof(Harmony_Patches.KeyboardLightPatch), "Postfix");
                var patch = new HarmonyMethod(postfix)
                {
                    after = AfterMods
                };

                Plugin.Harmony.Patch(targetMethod, postfix: patch);
                
            }
            if (mouse != null && mouse.Leds.Count() >= 2)
            {
                Plugin.Log.Notice("Mouse with 2+ LEDs was found, patching methods");
                var postfix = AccessTools.Method(typeof(Harmony_Patches.MouseLightPatch), "Postfix");
                var patch = new HarmonyMethod(postfix)
                {
                    after = AfterMods
                };

                Plugin.Harmony.Patch(targetMethod, postfix: patch);

            }
            if (headset != null)
            {
                Plugin.Log.Notice("Headset was found, patching methods");
                var postfix = AccessTools.Method(typeof(Harmony_Patches.HeadsetLightPatch), "Postfix");
                var patch = new HarmonyMethod(postfix)
                {
                    after = AfterMods
                };

                Plugin.Harmony.Patch(targetMethod, postfix: patch);
            }*/
        }
    }
}
