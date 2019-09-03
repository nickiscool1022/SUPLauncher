
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUPLauncher
{
    public partial class Overlay : Form
    {
        public Overlay()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://forum.superiorservers.co");
        }
        public struct RECT
        {
            public int left, top, right, bottom;
        }
        RECT rect;
        public const string WINDOW_NAME = "Garry's Mod";
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string ipClassName, string ipWindowName);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT ipRect);
        private void Overlay_Load(object sender, EventArgs e)
        {
            IntPtr handle = FindWindow(null, WINDOW_NAME);
            pictureBox1.Focus();
            GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Left = rect.right - this.Bounds.Width;
            this.Focus();
            frmLauncher.overlayVisable = true;
        }
        private void Button14_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("https://superiorservers.co/darkrp/rules");
        }
        private void Button11_Click(object sender, EventArgs e)
        {
            Process.Start("https://superiorservers.co/darkrp/rules");
        }
        private void Button12_Click(object sender, EventArgs e)
        {
            Process.Start("https://superiorservers.co/ssrp/milrp/rules");
        }
        private void Button15_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("https://superiorservers.co/ssrp/milrp/rules");
        }
        private void Button13_Click(object sender, EventArgs e)
        {
            Process.Start("https://superiorservers.co/ssrp/cwrp/rules");
        }
        private void Button16_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("https://superiorservers.co/ssrp/cwrp/rules");
        }
        private void Button17_Click(object sender, EventArgs e)
        {
            DupeManager dm = new DupeManager();
            dm.Show();
            dm.TopMost = true;
        }
        private void Overlay_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmLauncher.overlayVisable = false;
        }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((textBox1.Text.Contains("STEAM_0:0:") || textBox1.Text.Contains("STEAM_0:1:")) || (textBox1.Text.StartsWith("7") && textBox1.Text.Length == 76561197960265728.ToString().Length))
                {
                   Bans ban = new Bans(textBox1.Text);
                    ban.Show();
                    ban.TopMost = true;
                }
                else
                {
                    MessageBox.Show("Invalid SteamID. Make sure you have the correct SteamID", "Error");
                }

            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Process.Start("ts3server://TS.SuperiorServers.co:9987");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/rp.superiorservers.co:27015");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/rp2.superiorservers.co:27015");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/zrp.superiorservers.co:27015");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/milrp.superiorservers.co:27015");
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/cwrp.superiorservers.co:27015");
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/cwrp2.superiorservers.co:27015");
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Process.Start("https://superiorservers.co/bans");
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Process.Start("https://superiorservers.co/staff");
        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Text = "STEAM_0:X:XXXXXXXXX";
        }

        private void OverlayPanel_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void Overlay_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void Overlay_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                Overlay_Load(this, new EventArgs());
            else
                Overlay_FormClosing(this, new FormClosingEventArgs(CloseReason.None, false));
        }
    }
}


