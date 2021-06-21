using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;

namespace beatCUE.UI.Controllers
{
    [ViewDefinition("beatCUE.UI.Views.deviceManagement.bsml")]
    [HotReload("./../Views/deviceManagement.bsml")]
    class DeviceController : BSMLAutomaticViewController
    {
        [UIComponent("device-list")] internal CustomListTableData DeviceList = new CustomListTableData();

        [UIAction("#post-parse")]
        internal void Setup()
        {
            DeviceList.data.Clear();
            foreach (var device in Plugin.Devices)
                DeviceList.data.Add(new CustomListTableData.CustomCellInfo(device.DeviceInfo.Model, device.DeviceInfo.Type.ToString()));
            DeviceList.tableView.ReloadData();
        }
        private string dn = "";
        private string lc = "";
        [UIValue("device-name")] internal string DeviceName {
            get => dn;
            set
            {
                dn = value;
                NotifyPropertyChanged();
            }
        }
        [UIValue("led-count")]
        internal string LEDCount
        {
            get => lc;
            set
            {
                lc = value;
                NotifyPropertyChanged();
            }
        }


        [UIAction("device-select")]
        public void DeviceSelect(TableView _, int row)
        {
            DeviceName = Plugin.Devices[row].DeviceInfo.Model;
            LEDCount = $"{Plugin.Devices[row].Leds.ToList().Count()} LEDs";
        }
    }
}
