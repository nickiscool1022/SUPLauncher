
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        public static DupeManager dupemanager;
        private void Button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://forum.superiorservers.co");
        }
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

        IntPtr ClipboardViewerNext;

        private void Overlay_Load(object sender, EventArgs e)
        {
                                    
            IntPtr handle = FindWindow(null, frmLauncher.getGmodProcess().MainWindowTitle);
            pictureBox1.Focus();
            GetWindowRect(handle, out rect);
            this.Size = new Size(this.Width, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Left = rect.right - this.Bounds.Width;
            this.Focus();
            frmLauncher.overlayVisable = true;
            
            // Set Clipboard listener    
            if (ClipboardViewerNext.ToInt32() == 0)
            {
                ClipboardViewerNext = SetClipboardViewer(this.Handle);
                
            }

            HttpWebRequest request = WebRequest.CreateHttp("https://superiorservers.co/api/profile/" + frmLauncher.steam.GetSteamId());
            request.UserAgent = "Browser";
            WebResponse response = null;
            response = request.GetResponse(); // Get Response from webrequest
            StreamReader sr = new StreamReader(response.GetResponseStream()); // Create stream to access web data
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(sr.ReadToEnd());
            string[] staffRanks = { "Moderator", "Admin", "Double Admin", "Super Admin", "Council", "Root" };


            foreach (string x in staffRanks)
            {

                if (result.Badmin.Ranks.GetValue("DarkRP & Zombies") == x)
                {
                    staffTools.Visible = true;
                }

                if (result.Badmin.Ranks.GetValue("CWRP") == x)
                {
                    staffTools.Visible = true;
                }

                if (result.Badmin.Ranks.GetValue("MilRP") == x)
                {
                    staffTools.Visible = true;
                }
            }


            //if (staffRanks.Contains(result.Badmin.Ranks)



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
            if (dupemanager == null || dupemanager.IsDisposed)
            {
                dupemanager = new DupeManager();
                dupemanager.Show();
            }
            else
            {
                dupemanager.Visible = true;
            }
            dupemanager.TopMost = true;

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
                    Bans ban;
                    ban = new Bans(textBox1.Text);
                    ban.Show();
                    ban.TopMost = true;
                   //Bans ban = new Bans(textBox1.Text);
                    
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
            if (dupemanager != null && dupemanager.IsDisposed == false) { dupemanager.Visible = this.Visible; }

            if (this.Visible)
                Overlay_Load(this, new EventArgs());
            else
                Overlay_FormClosing(this, new FormClosingEventArgs(CloseReason.None, false));
        }


        public static event EventHandler ClipboardUpdate;

        private static void OnClipboardUpdate(EventArgs e)
        {
            var handler = ClipboardUpdate;
            if (handler != null)
            {
                handler(null, e);
            }
        }
        profile profile;
        protected override void WndProc(ref Message m)
        {

            // Listen for operating system Hot Key messages    

            base.WndProc(ref m);
            // Listen for operating system Clipboard changes    

            if (m.Msg == 0x308)
            {
                if (checkBox1.Checked)
                {
                    bool steamid = false;
                    long s = 0;
                    if (Clipboard.GetText().StartsWith("STEAM_") && Clipboard.GetText().Length > 17)
                    {
                        steamid = true;
                    }
                    else if (long.TryParse(Clipboard.GetText(), out s) && Clipboard.GetText().Length == 17)
                    {
                        steamid = true;
                    }

                    if (steamid)
                    {
                        if (profile == null || profile.IsDisposed)
                        {
                            profile = new profile();
                            profile.TopMost = true;

                        }


                        profile.Visible = true;
                        profile.steam = Clipboard.GetText();
                        profile.initProfile(profile.steam);

                    }

                }
            }
            
                
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {



            this.Visible = false;
            if (!checkBox1.Checked)
            {
                Noffication noffication = new Noffication("Profile overlays have now been disabled.", frmLauncher.getGmodProcess(), "STAFF TOOLS");
                noffication.Show();
            } else
            {
                Noffication noffication = new Noffication("Profile overlays have now been enabled.\n(Opens whenever you copy SteamID's)", frmLauncher.getGmodProcess(), "STAFF TOOLS");
                noffication.Show();
            }
            

        }
    }
}


