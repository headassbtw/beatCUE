using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Utilities;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using System.IO;
using System.Reflection;
using beatCUE.Harmony_Patches;
using HarmonyLib;
using BeatSaberMarkupLanguage;
using OpenRGB.NET;
using OpenRGB.NET.Models;
using Color = UnityEngine.Color;

namespace beatCUE
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Harmony Harmony { get; private set; }
        internal Device[] Devices { get; private set; }
        internal OpenRGBClient Client { get; private set; }

        public void WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }


        [Init]
        public void Init(IPALogger logger)
        {
            var libPath = Path.Combine(UnityGame.InstallPath, "Libs", "OpenRGB.NET.dll");
            if(!File.Exists(libPath))
                WriteResourceToFile("BeatCUE.Libs.OpenRGB.NET.dll", libPath);
            Client = new OpenRGBClient(name: Assembly.GetExecutingAssembly().GetName().Name, autoconnect: true, timeout: 100000);
            Devices = Client.GetAllControllerData();
            Instance = this;
            Log = logger;
            Harmony = new Harmony("com.headassbtw.beatcue");
            //Harmony.PatchAll(Assembly.GetExecutingAssembly());

            
             
            //if the client is connected, start up and set up the rest of the mod
            //this is to prevent people without openRGB running from saying "hurdurr no devices"
            if (Client.Connected)
            {
                Testing.LightingTest.Setup();
                beatCUE.UI.UICreator.CreateMenu();
                TestLightPatch.SHITFUCK(UnityEngine.Color.green);
                //beatCUEController go = new GameObject("beatCUEController").AddComponent<beatCUEController>();
            }
            
        }

        
        [Init]
        public void InitWithConfig(IPA.Config.Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
        }
        
        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            beatCUEController go = new GameObject("beatCUEController").AddComponent<beatCUEController>();
            go.gameObject.AddComponent<GetKeyPress>();
        }

        internal static void AllLights(Color color)
        {
            color.Ify();
            for (int i = 0; i < Plugin.Instance.Devices.Length; i++)
            {
                var leds = Enumerable.Range(0, Plugin.Instance.Devices[i].Colors.Length)
                    .Select(_ => color.FromUnity())
                    .ToArray();
                Plugin.Instance.Client.UpdateLeds(i, leds);
            }
        }
        
        
        
        [OnExit]
        public void OnApplicationQuit() => Client.Dispose();
    }
    internal class GetKeyPress : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                beatCUE.Plugin.AllLights(Color.red);
            if (Input.GetKeyDown(KeyCode.S))
                beatCUE.Plugin.AllLights(Color.green);
            if (Input.GetKeyDown(KeyCode.D))
                beatCUE.Plugin.AllLights(Color.blue);
        }
    }
}
