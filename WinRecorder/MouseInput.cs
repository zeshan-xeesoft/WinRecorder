using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinRecorder
{
    public static class MouseInput
    {

        //[DllImport("user32.dll", SetLastError = true)]
        //static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out Point lpPoint);

        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800,
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }
        // Use the values of this enum for the 'dwData' parameter
        // to specify an X button when using MouseEventFlags.XDOWN or
        // MouseEventFlags.XUP for the dwFlags parameter.
        public enum MouseEventDataXButtons : uint
        {
            XBUTTON1 = 0x00000001,
            XBUTTON2 = 0x00000002
        }
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);
        enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
        }
        static int CalculateAbsoluteCoordinateX(int x)
        {
            return (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

       static  int CalculateAbsoluteCoordinateY(int y)
        {
            return (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        // The mouse's target location.
        private static Point m_Target = new Point(150, 100);

        public static void PerformMouseEvent(string EventType, int X, int Y,Form Parent)
        {
            uint _x = 0;
            uint _y = 0;
            ////Point pt = new Point(0, 0);

            ////m_Target = new Point(X, Y);
            ////Parent.Invoke(new Action(() =>
            ////{
            ////    pt = Parent.PointToScreen(m_Target);
            ////}));

            ////// mouse_event moves in a coordinate system where
            ////// (0, 0) is in the upper left corner and
            ////// (65535,65535) is in the lower right corner.
            ////// Convert the coordinates.
            ////Rectangle screen_bounds = Screen.GetBounds(pt);


            ////_x = (uint)(pt.X * 65535 / screen_bounds.Width);
            ////_y = (uint)(pt.Y * 65535 / screen_bounds.Height);

            _x = (uint)CalculateAbsoluteCoordinateX(X);
            _y = (uint)CalculateAbsoluteCoordinateY(Y);



            // Move the mouse.
            switch (EventType)
            {
                case "MOVE":
                    mouse_event((uint)(MouseEventFlags.ABSOLUTE | MouseEventFlags.MOVE), _x, _y, 0, 0);
                    break;
                case "UP":
                    mouse_event((uint)(MouseEventFlags.ABSOLUTE | MouseEventFlags.MOVE | MouseEventFlags.LEFTUP), _x, _y, 0, 0);
                    break;
                case "DOWN":
                    mouse_event((uint)(MouseEventFlags.ABSOLUTE | MouseEventFlags.MOVE | MouseEventFlags.LEFTDOWN), _x, _y, 0, 0);
                    break;
                default:
                    break;
            }
        }
    }
}
