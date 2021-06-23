using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Groups;
using static BeatmapSaveData;

namespace beatCUE.Lighting
{
    static class KeyboardGroupLighting
    {
        internal static RectangleLedGroup FunctionRow;
        internal static RectangleLedGroup Numpad;
        internal static RectangleLedGroup AlphaNumeric;
        internal static RectangleLedGroup InBetween;
        internal static CorsairKeyboard keyboard = CueSDK.KeyboardSDK;

        internal static void InitLEDZones(CorsairKeyboard board)
        {
            FunctionRow = new RectangleLedGroup(board, CorsairLedId.Escape, CorsairLedId.ScanNextTrack);
            Numpad = new RectangleLedGroup(board, CorsairLedId.NumLock, CorsairLedId.KeypadEnter, 0.9f);
            AlphaNumeric = new RectangleLedGroup(board, CorsairLedId.LeftCtrl, CorsairLedId.Backspace);
            InBetween = new RectangleLedGroup(board, CorsairLedId.Insert, CorsairLedId.RightArrow);
        }

        public static void KeyboardFunctionRow(UnityEngine.Color color, BeatmapEventType ev)
        {
            if (ev.Equals(Configuration.PluginConfig.Instance.KB_FnRow) && keyboard != null)
            {
                color = color.Ify();
                CorsairColor cc = color.ToCorsair();
                if (keyboard != null)
                {
                    keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
                    keyboard[CorsairLedId.Mute].Color = cc;
                    FunctionRow.Brush = new SolidColorBrush(cc);
                    keyboard.Update();
                }
            }
            
        }
        public static void KeyboardNumpad(UnityEngine.Color color, BeatmapEventType ev)
        {
            if (ev.Equals(Configuration.PluginConfig.Instance.KB_Numpad) && keyboard != null)
            {
                color = color.Ify();
                CorsairColor cc = color.ToCorsair();
                if (keyboard != null)
                {
                    keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
                    var b = new SolidColorBrush(cc);
                    keyboard[CorsairLedId.Keypad0].Color = cc;
                    keyboard[CorsairLedId.KeypadPeriodAndDelete].Color = cc;
                    Numpad.Brush = b;
                    keyboard.Update();
                }
            }
        }
        public static void KeyboardInbetween(UnityEngine.Color color, BeatmapEventType ev)
        {
            if (ev.Equals(Configuration.PluginConfig.Instance.KB_InBetween) && keyboard != null)
            {
                color = color.Ify();
                CorsairColor cc = color.ToCorsair();
                if (keyboard != null)
                {
                    keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
                    var b = new SolidColorBrush(cc);
                    InBetween.Brush = b;
                    keyboard.Update();
                }
            }
        }
        public static void KeyboardAlphanumeric(UnityEngine.Color color, BeatmapEventType ev)
        {
            if (ev.Equals(Configuration.PluginConfig.Instance.KB_Alphanumeric) && keyboard != null)
            {
                color = color.Ify();
                CorsairColor cc = color.ToCorsair();
                if (keyboard != null)
                {
                    keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
                    var b = new SolidColorBrush(cc);
                    AlphaNumeric.Brush = b;
                    keyboard.Update();
                }
            }
        }
    }
}
