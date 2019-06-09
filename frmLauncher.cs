using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Win32;

namespace SUPLauncher
{
    public partial class frmLauncher : Form
    {
        Thread t;
        bool appStarted = false;
        public frmLauncher()
        {
            InitializeComponent();
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
                do // Don't ask lmfao
                {
                    ThreadHelperClass.SetText(this, lblDT, GetPlayerCount("rp.superiorservers.co").ToString() + "/128");
                    //ThreadHelperClass.SetText(this, lblSD, GetPlayerCount("208.103.169.13").ToString() + "/128");
                    ThreadHelperClass.SetText(this, lblC18, GetPlayerCount("rp2.superiorservers.co").ToString() + "/128");
                    ThreadHelperClass.SetText(this, lblZRP, GetPlayerCount("zrp.superiorservers.co").ToString() + "/128");
                    ThreadHelperClass.SetText(this, lblMRP, GetPlayerCount("milrp.superiorservers.co").ToString() + "/128");
                    ThreadHelperClass.SetText(this, lblCW1, GetPlayerCount("cwrp.superiorservers.co").ToString() + "/128");
                    ThreadHelperClass.SetText(this, lblCW2, GetPlayerCount("cwrp2.superiorservers.co").ToString() + "/128");
                    Thread.Sleep(120000);
                } while (2 == 2); // 2 is always equal to 2
        }
        private void frmLauncher_Load(object sender, EventArgs e)
        {
            t = new Thread(GetPlayerCountAllServers); // good idea penguin
            t.Start();
            Activate();
        }

        private void frmLauncher_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("SUPLauncher"))
            {
                process.Kill();
            }
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
