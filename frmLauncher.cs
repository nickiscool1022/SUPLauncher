using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using Microsoft.VisualBasic;
using DiscordRPC;

namespace SUPLauncher
{
    public partial class frmLauncher : Form
    {
        Thread t;
        bool appStarted = false;
        string dupePath;
        string server;
        public frmLauncher()
        {
            Thread trd = new Thread(new ThreadStart(Run));
            trd.Start();
            InitializeComponent();
            Thread.Sleep(5000);
            trd.Abort();
        }
        public void Run()
        {
            Application.Run(new Splashscreen1());
        }
        DiscordRpcClient discord = new DiscordRpcClient("594668399653814335");
        private void frmLauncher_Load(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("steam").Length == 0) // Check if steam is running (Thanks Red Means Recording)
            {
                MessageBox.Show("An error occurred. Please restart the program when steam is running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.Start("steam:");
                Interaction.Shell("taskkill /pid " + Process.GetCurrentProcess().Id.ToString() + " /f");
            }
            discord.Initialize();
            GetUsername();
            GetDiscordCheckStatus();
            GetCurrentServer();
            GetDupes();
            try
            {
                if (chkDiscord.Checked)
                {
                    lblServer_TextChanged(this, new EventArgs());
                }
                lblVersion.Text = Application.ProductVersion;
                var steam = new SteamBridge();
                var client = new WebClient();
                client.DownloadFile(new Uri("https://superiorservers.co/api/avatar/" + steam.GetSteamId().ToString()), "avatar.jpg");
                picImage.Image = Image.FromFile("avatar.jpg");
                client.Dispose();
                t = new Thread(GetPlayerCountAllServers); // good idea penguin
                t.Start();
                Activate();
                tmrSteamQuery.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                frmLauncher_FormClosing(this, new FormClosingEventArgs(CloseReason.ApplicationExitCall, false));
            }
        }
        private void btnDanktown_Click(object sender, EventArgs e)
        {
            
            if (chkAFK.Checked && appStarted == false)
            {
                Process.Start("steam://run/4000//-64bit -textmode -single_core -nojoy -low -nosound -sw -noshader -nopix -novid -nopreload -nopreloadmodels -multirun +connect rp.superiorservers.co");
            }
            else
            {
                Process.Start("steam://connect/rp.superiorservers.co:27015");
            }
            appStarted = true;
        }

        //private void btnSundown_Click(object sender, EventArgs e)
        //{
        //    if (chkAFK.Checked && appStarted == false)
        //    {
        //        Process.Start("steam://run/4000//-64bit -textmode -single_core -nojoy -low -nosound -sw -noshader -nopix -novid -nopreload -nopreloadmodels -multirun +connect rp2.superiorservers.co");
        //    }
        //    else
        //    {
        //        Process.Start("steam://connect/rp2.superiorservers.co:27015");
        //    }
        //    appStarted = true;
        //}

        private void btnC18_Click(object sender, EventArgs e)
        {
            if (chkAFK.Checked && appStarted == false)
            {
                Process.Start("steam://run/4000//-64bit -textmode -single_core -nojoy -low -nosound -sw -noshader -nopix -novid -nopreload -nopreloadmodels -multirun +connect rp2.superiorservers.co");
            }
            else
            {
                Process.Start("steam://connect/rp2.superiorservers.co:27015");
            }
            appStarted = true;
        }

        private void btnZombies_Click(object sender, EventArgs e)
        {
            if (chkAFK.Checked && appStarted == false)
            {
                Process.Start("steam://run/4000//-64bit -textmode -single_core -nojoy -low -nosound -sw -noshader -nopix -novid -nopreload -nopreloadmodels -multirun +connect zrp.superiorservers.co");
            }
            else
            {
                Process.Start("steam://connect/zrp.superiorservers.co:27015");
            }
            appStarted = true;
        }

        private void btnMilRP_Click(object sender, EventArgs e)
        {
            if (chkAFK.Checked && appStarted == false)
            {
                Process.Start("steam://run/4000//-64bit -textmode -single_core -nojoy -low -nosound -sw -noshader -nopix -novid -nopreload -nopreloadmodels -multirun +connect milrp.superiorservers.co");
            }
            else
            {
                Process.Start("steam://connect/milrp.superiorservers.co:27015");
            }
            appStarted = true;
        }

        private void btnCW1_Click(object sender, EventArgs e)
        {
            if (chkAFK.Checked && appStarted == false)
            {
                Process.Start("steam://run/4000//-64bit -textmode -single_core -nojoy -low -nosound -sw -noshader -nopix -novid -nopreload -nopreloadmodels -multirun +connect cwrp.superiorservers.co");
            }
            else
            {
                Process.Start("steam://connect/cwrp.superiorservers.co:27015");
            }
            appStarted = true;
        }

        private void btnCW2_Click(object sender, EventArgs e)
        {
            if (chkAFK.Checked && appStarted == false)
            {
                Process.Start("steam://run/4000//-64bit -textmode -single_core -nojoy -low -nosound -sw -noshader -nopix -novid -nopreload -nopreloadmodels -multirun +connect cwrp2.superiorservers.co");
            }
            else
            {
                Process.Start("steam://connect/cwrp2.superiorservers.co:27015");
            }
            appStarted = true;
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Keep in mind that this program is still being worked on and is not an official release of the SUP Launcher. In order to use this program, you must just simply click on a button and watch the magic happen. The credit for this idea goes to aStonedPenguin, and all new releases will available on the github (nicksuperiorservers/SUPLauncher). Thanks for using this nice little program I made, and have a fun time playing SuperiorServers." + Environment.NewLine + Environment.NewLine + "-Nick", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnForums_Click(object sender, EventArgs e)
        {
            Process.Start("https://forum.superiorservers.co");
        }

        private void btnTS_Click(object sender, EventArgs e)
        {
            Process.Start("ts3server://TS.SuperiorServers.co:9987");
        }

        private void frmLauncher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chkDiscord.Checked && File.Exists("1") == false)
            {
                File.Create("1");
                File.SetAttributes("1", FileAttributes.Hidden);
            }
            else
                File.Delete("1");
            Interaction.Shell("taskkill /pid " + Process.GetCurrentProcess().Id.ToString() + " /f /t"); // Whoops
        }

        private void chkAFK_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("hl2"))
            {
                process.Kill();
            }
            foreach (var process in Process.GetProcessesByName("gmod"))
            {
                process.Kill();
            }
            appStarted = false;
        }

        private void picImage_Click(object sender, EventArgs e)
        {
            var steam = new SteamBridge();
            Process.Start("https://superiorservers.co/profile/" + steam.GetSteamId().ToString());
        }
        private void btnDRPRules_Click(object sender, EventArgs e)
        {
            Process.Start("https://superiorservers.co/darkrp/rules");
        }

        private void btnMilRPRules_Click(object sender, EventArgs e)
        {
            Process.Start("https://superiorservers.co/ssrp/milrp/rules");
        }

        private void btnCWRPRules_Click(object sender, EventArgs e)
        {
            Process.Start("https://superiorservers.co/ssrp/cwrp/rules");
        }
        private void lblVersion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Auto-update temporary disabled. Sorry for any inconvenience.", "Disabled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //var updater = new ClientUpdater();
            //updater.Update();
        }
        void GetUsername()
        {
            var steam = new SteamBridge();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Secure security protocol for querying the github API
            HttpWebRequest request = WebRequest.CreateHttp("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=27A2CB958256FB97DCFFEE9B634CD02E&steamids=" + steam.GetSteamId());
            request.UserAgent = "Nick";
            WebResponse response = null;
            response = request.GetResponse(); // Get Response from webrequest
            StreamReader sr = new StreamReader(response.GetResponseStream()); // Create stream to access web data
            string currentRecord = sr.ReadToEnd(); // Read data from response stream
            string raw = currentRecord.Substring(currentRecord.IndexOf("personaname") + "personaname".Length + 3, (currentRecord.IndexOf("lastlogoff") - (currentRecord.IndexOf("personaname") + "personaname".Length + 6)));
            this.Text = "SUP Launcher (" + raw.ToString() + ")";
        }
        void GetDiscordCheckStatus()
        {
            if (File.Exists("1"))
                chkDiscord.Checked = true;
            else
                chkDiscord.Checked = false;
        }
        void GetDupes()
        {
            // GetValue() only returns X:\Program Files (x86)\Steam
            string SteamInstallPathDir;
            if (Environment.Is64BitOperatingSystem)
                SteamInstallPathDir = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", "InstallPath", null).ToString();
            else
                SteamInstallPathDir = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "InstallPath", null).ToString();
            if (Directory.Exists(SteamInstallPathDir + @"\steamapps\common\GarrysMod\garrysmod\data\advdupe2") == false)
            {
                var sr = new StreamReader(SteamInstallPathDir + @"\steamapps\libraryfolders.vdf");
                string raw;
                do
                {
                    raw = sr.ReadLine();
                } while (raw.Contains(@"""1""") == false);
                string refined = raw.Substring(raw.IndexOf("\t\t") + 3);
                for (int i = 0; i < refined.Length; i++)
                {
                    if (refined.Substring(i, 1) == "\\".ToString())
                    {
                        refined = refined.Remove(i, 1);
                    }
                }
                dupePath = refined.Substring(0, refined.Length - 1) + @"\steamapps\common\GarrysMod\garrysmod\data\advdupe2";
            }
            else
                dupePath = SteamInstallPathDir + @"\steamapps\common\GarrysMod\garrysmod\data\advdupe2";
            string[] files = new string[Directory.GetFiles(dupePath).Length];
            int z = 0;
            foreach (var i in Directory.GetFiles(dupePath))
            {
                files[z] = Path.GetFileName(i);
                z++;
            }
            Dupes.Items.AddRange(files);
        }
        void GetCurrentServer()
        {
            try
            {
                var steam = new SteamBridge();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Secure security protocol for querying the github API
                HttpWebRequest request = WebRequest.CreateHttp("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=27A2CB958256FB97DCFFEE9B634CD02E&steamids=" + steam.GetSteamId());
                request.UserAgent = "Nick";
                WebResponse response = null;
                response = request.GetResponse(); // Get Response from webrequest
                StreamReader sr = new StreamReader(response.GetResponseStream()); // Create stream to access web data
                string currentRecord = sr.ReadToEnd(); // Read data from response stream
                string raw = currentRecord.Substring(currentRecord.IndexOf("gameserverip") + "gameserverip".Length + 3, (currentRecord.IndexOf("27015") - (currentRecord.IndexOf("gameserverip") + "gameserverip".Length + 3)));
                string refined = raw.Substring(0, raw.Length - 1);
                switch (refined)
                {
                    case "208.103.169.12":
                        btnDanktown.BackColor = Color.SpringGreen;
                        btnC18.BackColor = Color.White;
                        btnZombies.BackColor = Color.White;
                        btnMilRP.BackColor = Color.White;
                        btnCW1.BackColor = Color.White;
                        btnCW2.BackColor = Color.White;
                        server = "Danktown";
                        break;
                    case "208.103.169.13":
                        btnDanktown.BackColor = Color.White;
                        btnC18.BackColor = Color.SpringGreen;
                        btnZombies.BackColor = Color.White;
                        btnMilRP.BackColor = Color.White;
                        btnCW1.BackColor = Color.White;
                        btnCW2.BackColor = Color.White;
                        server = "C18";
                        break;
                    case "208.103.169.14":
                        btnDanktown.BackColor = Color.White;
                        btnC18.BackColor = Color.White;
                        btnZombies.BackColor = Color.SpringGreen;
                        btnMilRP.BackColor = Color.White;
                        btnCW1.BackColor = Color.White;
                        btnCW2.BackColor = Color.White;
                        server = "ZRP";
                        break;
                    case "208.103.169.18":
                        btnDanktown.BackColor = Color.White;
                        btnC18.BackColor = Color.White;
                        btnZombies.BackColor = Color.White;
                        btnMilRP.BackColor = Color.SpringGreen;
                        btnCW1.BackColor = Color.White;
                        btnCW2.BackColor = Color.White;
                        server = "MilRP";
                        break;
                    case "208.103.169.16":
                        btnDanktown.BackColor = Color.White;
                        btnC18.BackColor = Color.White;
                        btnZombies.BackColor = Color.White;
                        btnMilRP.BackColor = Color.White;
                        btnCW1.BackColor = Color.SpringGreen;
                        btnCW2.BackColor = Color.White;
                        server = "CWRP #1";
                        break;
                    case "208.103.169.17":
                        btnDanktown.BackColor = Color.White;
                        btnC18.BackColor = Color.White;
                        btnZombies.BackColor = Color.White;
                        btnMilRP.BackColor = Color.White;
                        btnCW1.BackColor = Color.White;
                        btnCW2.BackColor = Color.SpringGreen;
                        server = "CWRP #2";
                        break;
                }
                lblServer.Text = server;
            }
            catch (Exception)
            {
                btnDanktown.BackColor = Color.White;
                btnC18.BackColor = Color.White;
                btnZombies.BackColor = Color.White;
                btnMilRP.BackColor = Color.White;
                btnCW1.BackColor = Color.White;
                btnCW2.BackColor = Color.White;
                server = "";
            }
        }
    private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete " + Dupes.SelectedItem.ToString() + "?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                File.Delete(dupePath + @"\" + Dupes.SelectedItem);
                Dupes.Items.RemoveAt(Dupes.SelectedIndex);
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Dupes.SelectedIndex == -1)
                e.Cancel = true;
        }

        private void tmrSteamQuery_Tick(object sender, EventArgs e)
        {
            GetCurrentServer();
        }

        private void chkDiscord_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiscord.Checked)
            {
                lblServer_TextChanged(this, new EventArgs());
            }
            else
            {
                discord.ClearPresence();
            }
        }

        private void lblServer_TextChanged(object sender, EventArgs e)
        {
            if (discord.IsInitialized && chkDiscord.Checked)
            {
                switch (server)
                {
                    case "Danktown":
                        discord.SetPresence(new RichPresence()
                        {
                            Details = "Playing on Danktown",
                            State = "",
                            Timestamps = Timestamps.Now,
                            Assets = new Assets()
                            {
                                LargeImageKey = "suplogo",
                                LargeImageText = "SuperiorServers.co"
                            }
                        });
                        break;
                    case "C18":
                        discord.SetPresence(new RichPresence()
                        {
                            Details = "Playing on C18",
                            State = "",
                            Timestamps = Timestamps.Now,
                            Assets = new Assets()
                            {
                                LargeImageKey = "suplogo",
                                LargeImageText = "SuperiorServers.co"
                            }
                        });
                        break;
                    case "ZRP":
                        discord.SetPresence(new RichPresence()
                        {
                            Details = "Playing on ZRP",
                            State = "",
                            Timestamps = Timestamps.Now,
                            Assets = new Assets()
                            {
                                LargeImageKey = "suplogo",
                                LargeImageText = "SuperiorServers.co"
                            }
                        });
                        break;
                    case "MilRP":
                        discord.SetPresence(new RichPresence()
                        {
                            Details = "Playing on MilRP",
                            State = "",
                            Timestamps = Timestamps.Now,
                            Assets = new Assets()
                            {
                                LargeImageKey = "suplogo",
                                LargeImageText = "SuperiorServers.co"
                            }
                        });
                        break;
                    case "CWRP #1":
                        discord.SetPresence(new RichPresence()
                        {
                            Details = "Playing on CWRP #1",
                            State = "",
                            Timestamps = Timestamps.Now,
                            Assets = new Assets()
                            {
                                LargeImageKey = "suplogo",
                                LargeImageText = "SuperiorServers.co"
                            }
                        });
                        break;
                    case "CWRP #2":
                        discord.SetPresence(new RichPresence()
                        {
                            Details = "Playing on CWRP #2",
                            State = "",
                            Timestamps = Timestamps.Now,
                            Assets = new Assets()
                            {
                                LargeImageKey = "suplogo",
                                LargeImageText = "SuperiorServers.co"
                            }
                        });
                        break;
                    default:
                        discord.SetPresence(new RichPresence()
                        {
                            Details = "Waiting to join a server...",
                            State = "",
                            Timestamps = Timestamps.Now,
                            Assets = new Assets()
                            {
                                LargeImageKey = "suplogo",
                                LargeImageText = "SuperiorServers.co"
                            }
                        });
                        break;
                }
                
            }
        }
        private byte GetPlayerCount(string ip)
        {
            // DT: 208.103.169.12
            // SD: 208.103.169.13 
            // C18: 208.103.169.15
            // ZRP: 208.103.169.14 
            // MilRP: 208.103.169.18 
            // CWRP: 208.103.169.16 
            // CWRP #2: 208.103.169.17 

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            byte[] rawData = new byte[512];
            socket.Connect(ip, 27015);
            byte[] sendBytes = { 0xFF, 0xFF, 0xFF, 0xFF, 0x54, 0x53, 0x6F, 0x75, 0x72, 0x63, 0x65, 0x20, 0x45, 0x6E, 0x67, 0x69, 0x6E, 0x65, 0x20, 0x51, 0x75, 0x65, 0x72, 0x79, 0x00 };
            socket.Send(sendBytes);

            socket.Receive(rawData);
            using (var ms = new MemoryStream(rawData))
            {
                ms.ReadByte();
                ms.ReadByte();
                ms.ReadByte();
                ms.ReadByte();

                ms.ReadByte();
                ms.ReadByte();

                ms.ReadTerminatedString(); // FAIR WARNING THIS CODE PORTION IS ACTUALLY RETARDED & OBFUSCATED FOR A REASON
                ms.ReadTerminatedString();
                ms.ReadTerminatedString();
                ms.ReadTerminatedString();

                ms.ReadByte();
                ms.ReadByte();

                return Convert.ToByte(ms.ReadByte());
            }
        }
        private void GetPlayerCountAllServers()
        {
            do
            {
                ThreadHelperClass.SetText(this, lblDT, GetPlayerCount("rp.superiorservers.co").ToString() + "/128");
                //ThreadHelperClass.SetText(this, lblSD, GetPlayerCount("208.103.169.13").ToString() + "/128");
                ThreadHelperClass.SetText(this, lblC18, GetPlayerCount("rp2.superiorservers.co").ToString() + "/128");
                ThreadHelperClass.SetText(this, lblZRP, GetPlayerCount("zrp.superiorservers.co").ToString() + "/128");
                ThreadHelperClass.SetText(this, lblMRP, GetPlayerCount("milrp.superiorservers.co").ToString() + "/128");
                ThreadHelperClass.SetText(this, lblCW1, GetPlayerCount("cwrp.superiorservers.co").ToString() + "/128");
                ThreadHelperClass.SetText(this, lblCW2, GetPlayerCount("cwrp2.superiorservers.co").ToString() + "/128");
                Thread.Sleep(120000);
            } while (true);
        }
    }
    public static class MemoryStreamExtensions
    {
        public static string ReadTerminatedString(this MemoryStream ms)
        {
            List<byte> res = new List<byte>();

            byte last;
            while ((last = (byte)ms.ReadByte()) != 0x00)
            {
                res.Add(last);
            }

            return System.Text.Encoding.ASCII.GetString(res.ToArray());
        }
    }
    public static class ThreadHelperClass // Because fuck threads and me not allowing to just set text on a label like a normal person
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);
        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl">The control being modified</param>
        /// <param name="text">The text to set</param>
        public static void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

    }
}
