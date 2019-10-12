using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinRecorder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
        }

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out Point lpPoint);

        int CalculateAbsoluteCoordinateX(int x)
        {
            return (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        int CalculateAbsoluteCoordinateY(int y)
        {
            return (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thrd_SimClick = new Thread(delegate()
            {
                int counter = 0;

                int x = CalculateAbsoluteCoordinateX(100);
                int y = CalculateAbsoluteCoordinateY(100);
                Thread.Sleep(10000);
                while (counter++ < 5)
                {
                    Thread.Sleep(2000);

                    this.Invoke(new Action(() =>
                    {

                       // MouseInput.ClickLeftMouseButton(x += 1, y += 1);
                        //  txtResponse.Text += "[" + DateTime.Now.ToString("hh:mm:ss tt") + "]" + Environment.NewLine + ErrorMessage;
                    }));
                }

            });
            thrd_SimClick.Start();
            //  MouseInput.ClickLeftMouseButton()
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Point p;
            if (GetCursorPos(out p))
            {
                lblMousePostion.Text = string.Format("x={0:0000}; y={1:0000};", p.X, p.Y) + Environment.NewLine +
                    string.Format("x={0:0000}; y={1:0000};", CalculateAbsoluteCoordinateX(p.X), CalculateAbsoluteCoordinateY(p.Y));
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            txtResponse.Text += "Mouse Moved : " + string.Format("x={0:0000}; y={1:0000};", e.X, e.Y) + Environment.NewLine;
            lblMousePostion.Text = string.Format("x={0:0000}; y={1:0000};", e.X, e.Y) + Environment.NewLine +
                    string.Format("x={0:0000}; y={1:0000};", CalculateAbsoluteCoordinateX(e.X), CalculateAbsoluteCoordinateY(e.Y));
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            txtResponse.Text += "Mouse Clicked : " + string.Format("x={0:0000}; y={1:0000};B={2}", e.X, e.Y, e.Button) + Environment.NewLine;
        }

        private void txtResponse_TextChanged(object sender, EventArgs e)
        {
            txtResponse.SelectionStart = txtResponse.Text.Length;
            txtResponse.ScrollToCaret();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            txtResponse.Text += "Mouse Down : " + string.Format("x={0:0000}; y={1:0000};B={2}", e.X, e.Y, e.Button) + Environment.NewLine;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            txtResponse.Text += "Mouse UP : " + string.Format("x={0:0000}; y={1:0000};B={2}", e.X, e.Y, e.Button) + Environment.NewLine;
        }

    }
}
