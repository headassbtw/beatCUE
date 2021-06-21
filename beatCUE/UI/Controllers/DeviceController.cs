﻿using System;
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

namespace beatCUE.UI.Controllers
{
    [ViewDefinition("beatCUE.UI.Views.deviceManagement.bsml")]
    [HotReload("./../Views/deviceManagement.bsml")]
    class DeviceController : BSMLAutomaticViewController
    {
        [UIComponent("device-list")] internal CustomListTableData DeviceList = new CustomListTableData();
        [UIValue("lighting-events")]
        private List<object> options = new object[] { "Back Lasers", "Ring Lights", "Left Rotating Lasers", "Right Rotating Lasers", "Center Lights", "Boost Light Colors", "Interscope Left", "Interscope Right" }.ToList();
        [UIAction("#post-parse")]
        internal void Setup()
        {
            DeviceList.data.Clear();
            foreach (var device in Plugin.Devices)
                DeviceList.data.Add(new CustomListTableData.CustomCellInfo(device.DeviceInfo.Model, device.DeviceInfo.Type.ToString()));
            DeviceList.tableView.ReloadData();
        }

        [UIParams]
        BSMLParserParams parserParams;
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
        [UIValue("alphanumeric")] string Alphanumeric
        {
            get => Configuration.PluginConfig.Instance.KB_Alphanumeric.ToNamed();
            set
            {
                Configuration.PluginConfig.Instance.KB_Alphanumeric = value.FromNamed();
                NotifyPropertyChanged();
            }
        }
        [UIValue("functionrow")] string FunctionRow
        {
            get => Configuration.PluginConfig.Instance.KB_FnRow.ToNamed();
            set
            {
                Configuration.PluginConfig.Instance.KB_FnRow = value.FromNamed();
                NotifyPropertyChanged();
            }
        }
        [UIValue("numpad")] string Numpad
        {
            get => Configuration.PluginConfig.Instance.KB_Numpad.ToNamed();
            set
            {
                Configuration.PluginConfig.Instance.KB_Numpad = value.FromNamed();
                NotifyPropertyChanged();
            }
        }
        [UIValue("inbetween")]
        string InBetween
        {
            get => Configuration.PluginConfig.Instance.KB_InBetween.ToNamed();
            set
            {
                Configuration.PluginConfig.Instance.KB_InBetween = value.FromNamed();
                NotifyPropertyChanged();
            }
        }
        [UIValue("mouse")]
        string mouse
        {
            get => Configuration.PluginConfig.Instance.Mouse.ToNamed();
            set
            {
                Configuration.PluginConfig.Instance.Mouse = value.FromNamed();
                NotifyPropertyChanged();
            }
        }


        [UIAction("device-select")]
        public void DeviceSelect(TableView _, int row)
        {
            DeviceName = Plugin.Devices[row].DeviceInfo.Model.ToString();
            if (Plugin.Devices[row].DeviceInfo.Type.ToString().ToLower().Equals("mouse"))
                parserParams.EmitEvent("mouse-modal");
            if (Plugin.Devices[row].DeviceInfo.Type.ToString().ToLower().Equals("keyboard"))
                parserParams.EmitEvent("keyboard-modal");
        }
    }
}
