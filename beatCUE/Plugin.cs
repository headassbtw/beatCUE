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
using CUE.NET;
using CUE.NET.Devices.Keyboard;
using System.IO;
using System.Reflection;
using HarmonyLib;
using BeatSaberMarkupLanguage;

namespace beatCUE
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Harmony Harmony { get; private set; }
        internal static List<CUE.NET.Devices.ICueDevice> Devices;

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
            Instance = this;
            Log = logger;
            Harmony = new Harmony("com.headassbtw.beatcue");
            //Harmony.PatchAll(Assembly.GetExecutingAssembly());
            

            string nativesPath = Path.Combine(UnityGame.InstallPath, "Libs", "Native");
            if (!Directory.Exists(nativesPath))
            {
                Directory.CreateDirectory(nativesPath);
            }
            if (!File.Exists(Path.Combine(nativesPath, "CUESDK_2015_x86.dll")))
            {
                WriteResourceToFile("beatCUE.Libs.x86.CUESDK_2015.dll", Path.Combine(nativesPath, "CUESDK_2015_x86.dll"));
            }
            if (!File.Exists(Path.Combine(nativesPath, "CUESDK_2015_x64.dll")))
            {
                WriteResourceToFile("beatCUE.Libs.x64.CUESDK_2015.dll", Path.Combine(nativesPath, "CUESDK_2015_x64.dll"));
            }
            CueSDK.Initialize();
            Log.Info("beatCUE initialized.");
            Testing.LightingTest.Setup();
            beatCUE.UI.UICreator.CreateMenu();


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
            new GameObject("beatCUEController").AddComponent<beatCUEController>();

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }
    }
}
