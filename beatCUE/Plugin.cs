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
using HarmonyLib;
using BeatSaberMarkupLanguage;
using OpenRGB.NET;
using OpenRGB.NET.Models;

namespace beatCUE
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Harmony Harmony { get; private set; }
        internal static List<Device> Devices { get; private set; }
        internal static OpenRGBClient Client { get; private set; }

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
            Devices = Client.GetAllControllerData().ToList();
            Instance = this;
            Log = logger;
            Harmony = new Harmony("com.headassbtw.beatcue");
            //Harmony.PatchAll(Assembly.GetExecutingAssembly());

            
             
            Log.Info("beatCUE initialized.");
            if (Client.Connected)
            {
                Testing.LightingTest.Setup();
                beatCUE.UI.UICreator.CreateMenu();
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
            new GameObject("beatCUEController").AddComponent<beatCUEController>();

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Client.Dispose();

        }
    }
}
