using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinRecorder
{
    public enum EventTypes { MMove, MClick, MDown, MUp, MDoubleClick, MWheel, KeyUp, KeyDown, KeyPress, ScreenShot, OpenApp }
    public class WinEvent
    {
        public WinEvent()
        {
            PressedKey = ' ';
        }
        /// <summary>
        /// Delay between events in milliseconds.
        /// </summary>
        public string Delay { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Wheel { get; set; }
        public string Button { get; set; }
        public string PerformedEvent { get; set; }
        /// <summary>
        /// Integer Value of Keyboard Key
        /// </summary>
        public int KeyCode { get; set; }
        public string AppPath { get; set; }
        public string SSPath { get; set; }

        public Rectangle AppPosition { get; set; }
        /// <summary>
        /// Used for KeyPress Event only
        /// </summary>
        public char PressedKey { get; set; }

    }
    public static class Session
    {
        static Session()
        {
            StartTime = DateTime.Now;
            Delay = 0;

        }
        public static DateTime StartTime { get; set; }
        public static double Delay { get; set; }
        public static int X { get; set; }
        public static int Y { get; set; }
        public static int Wheel { get; set; }
        public static bool IsMouseDown { get; set; }
        public static bool IsMouseUp { get; set; }
        public static EventTypes PerformedEvent { get; set; }

        public static string ClickedButton { get; set; }
    }
}
