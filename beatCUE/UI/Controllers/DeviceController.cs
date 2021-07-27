using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using OpenRGB.NET.Enums;
using OpenRGB.NET.Models;
using UnityEngine;
using Color = UnityEngine.Color;

namespace beatCUE.UI.Controllers
{
    [ViewDefinition("beatCUE.UI.Views.deviceManagement.bsml")]
    [HotReload("./../Views/deviceManagement.bsml")]
    class DeviceController : BSMLAutomaticViewController
    {
        internal static int CurrentDevice = 0;
        internal static int CurrentZone = 0;
        [UIComponent("device-list")] internal CustomListTableData DeviceList = new CustomListTableData();
        [UIComponent("zone-list")] internal CustomListTableData ZoneList = new CustomListTableData();
        [UIValue("lighting-events")]
        private List<object> options = new object[] { "Back Lasers", "Ring Lights", "Left Rotating Lasers", "Right Rotating Lasers", "Center Lights", "Boost Light Colors", "Interscope Left", "Interscope Right" }.ToList();
        [UIAction("#post-parse")]
        internal void Setup()
        {
            DeviceList.data.Clear();
            foreach (var device in Plugin.Devices)
                DeviceList.data.Add(new CustomListTableData.CustomCellInfo(device.Name, device.Type.ToString()));
            DeviceList.tableView.ReloadData();
        }
        
#pragma warning disable 649 //BSML handles this lol
        [UIParams]
        BSMLParserParams parserParams;
#pragma warning restore 649
        private string dn = "";
        [UIValue("device-name")]
        internal string DeviceName
        {
            get => dn;
            set
            {
                dn = value;
                NotifyPropertyChanged();
            }
        }
        
        [UIAction("device-select")]
        public void DeviceSelect(TableView _, int row)
        {
            CurrentDevice = row;
            DeviceName = Plugin.Devices[row].Name.ToString();
            ZoneList.data.Clear();
            foreach (var zone in Plugin.Devices[row].Zones)
            {
                ZoneList.data.Add((new CustomListTableData.CustomCellInfo(zone.Name, $"{zone.LedCount} LEDs")));
            }
            ZoneList.tableView.ReloadData();
        }
        [UIAction("zone-select")]
        public void ZoneSelect(TableView _, int row)
        {
            CurrentZone = row;
            Harmony_Patches.TestLightPatch.device = CurrentDevice;
            Harmony_Patches.TestLightPatch.zone = row;
            if(Plugin.Devices[CurrentDevice].Zones[row].LedCount > 0)
                Harmony_Patches.TestLightPatch.SHITFUCK(Color.white);
        }
    }
}
