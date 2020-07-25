using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUPLauncher
{
    public partial class Noffication : Form
    {

        public struct RECT
        {
            public int left, top, right, bottom;
        }
        RECT rect;

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string ipClassName, string ipWindowName);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT ipRect);



        /// <summary>
        /// Shows a noffication attached to a process in the top right corner
        /// </summary>
        /// <param name="Title">The title showed at the top.</param>
        /// <param name="Text">The text to display in the noffication</param>
        /// 

        Process process2;
        public Noffication(string Text, Process process, string Title= "NOFFICATION")
        {
            InitializeComponent();
            title.Text = Title;
            this.TopMost = true;
            text.Text = Text;
            process2 = process;
            
        }

        private void closeOn_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Noffication_Load(object sender, EventArgs e)
        {


            

            IntPtr handle = FindWindow(null, process2.MainWindowTitle);
            GetWindowRect(handle, out rect);
            this.Size = new Size(this.Width, this.Height);
            this.Top = rect.top;
            this.Left = rect.right - this.Bounds.Width;
            
        }
    }
}
