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

        [UIParams]
#pragma warning disable 649 //BSML handles this lol
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
            Plugin.Client.UpdateZone(CurrentDevice, row, new[] { new Color(255, 0, 0) });
            
            var c = new Color[1];
            c[0] = new Color(255, 255, 255);
            Plugin.Client.UpdateZone(CurrentDevice, row, c);
            var colors = new Color[Plugin.Devices[CurrentDevice].Zones[row].LedCount];
            var colorss = new List<Color>();
            for (int i = 0; i < Plugin.Devices[CurrentDevice].Zones[row].LedCount; i++)
            {
                colorss.Add(new Color(255,255,255));
            }
            colors = colorss.ToArray();
            switch (Plugin.Devices[CurrentDevice].Zones[row].Type)
            {
                case ZoneType.Linear:
                    //var colors = Color.GetHueRainbow((int)Plugin.Devices[CurrentDevice].Zones[row].LedCount);
                    Plugin.Client.UpdateZone(CurrentDevice, row, colors);
                    break;
                case ZoneType.Single:
                    Plugin.Client.UpdateZone(CurrentDevice, row, colors);
                    break;
                case ZoneType.Matrix:
                    var yeet = 2 * Math.PI / Plugin.Devices[CurrentDevice].Zones[row].MatrixMap.Width;
                    var rainbow = Color.GetHueRainbow((int)Plugin.Devices[CurrentDevice].Zones[row].MatrixMap.Width).ToArray();
                    //var rainbow = Color.GetSinRainbow((int)zone.MatrixMap.Width).ToArray();

                    var matrix = Enumerable.Range(0, (int)Plugin.Devices[CurrentDevice].Zones[row].LedCount).Select(__ => new Color()).ToArray();
                    for (int k = 0; k < Plugin.Devices[CurrentDevice].Zones[row].MatrixMap.Width; k++)
                    {
                        for (int l = 0; l < Plugin.Devices[CurrentDevice].Zones[row].MatrixMap.Height; l++)
                        {
                            var index = Plugin.Devices[CurrentDevice].Zones[row].MatrixMap.Matrix[l, k];
                            if (index != uint.MaxValue)
                            {
                                matrix[index] = rainbow[k].Clone();
                            }
                        }
                    }
                    Plugin.Client.UpdateZone(CurrentDevice, row, matrix);
                    break;
            }
            //Plugin.Devices[CurrentDevice].Zones[row].Type = ZoneType.Single;
        }
    }
}
