using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Mouse;
using CUE.NET.Devices.Headset;
using UnityEngine;
using static beatCUE.Extensions;

namespace beatCUE.Testing
{
    class LightingTest
    {
        internal static CorsairKeyboard keyboard = CueSDK.KeyboardSDK;
        internal static CorsairMouse mouse = CueSDK.MouseSDK;
        internal static CorsairHeadset headset = CueSDK.HeadsetSDK;
        internal static void Setup()
        {
            var devices = CueSDK.InitializedDevices.ToList();
            Plugin.Devices = devices;
            foreach(var device in devices)
            {
                Plugin.Log.Notice($"Device \"{device.DeviceInfo.Model}\" is a {device.DeviceInfo.Type.ToString()} and has {device.Leds.Count()} LEDs");
            }

            if(headset != null)
            {
                headset.Brush = (SolidColorBrush)CorsairColor.Transparent;
                headset[headset.GetLeds().ToList().ElementAt(0).Id].Color = new CorsairColor(0, 0, 0);
                headset[headset.GetLeds().ToList().ElementAt(1).Id].Color = new CorsairColor(0, 0, 0);
                headset.Update();
            }

            
            if (keyboard != null)
            {
                keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
                keyboard['F'].Color = new CorsairColor(255, 0, 0);
                
                keyboard.Update();
            }
            
        }
        internal static void MouseLights(UnityEngine.Color color, int light)
        {
            color = color.Ify();
            mouse.Brush = (SolidColorBrush)CorsairColor.Transparent;

            mouse[mouse.GetLeds().ToList().ElementAt(light).Id].Color = color.ToCorsair();

            mouse.Update();
        }
    }
}
