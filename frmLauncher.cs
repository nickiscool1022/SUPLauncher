using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace SUPLauncher
{
    public partial class frmLauncher : Form
    {
        public frmLauncher()
        {
            InitializeComponent();
        }

        //private void frmLauncher_Load(object sender, EventArgs e)
        //{
        //    DriveInfo[] allDrives = DriveInfo.GetDrives();
        //    var i = 0;
        //    foreach (DriveInfo d in allDrives)
        //    {
        //        if (!Directory.Exists(d.Name + @"\\Steam\steamapps\common\GarrysMod\") && i > allDrives.Length - 1)
        //        {
        //            MessageBox.Show("Garry's Mod executeable not found, closing application.", "hl2.exe not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            Application.Exit();
        //        }
        //        i++;
        //    }
        //}
        private void btnDanktown_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/rp.superiorservers.co:27015");
        }

        private void btnSundown_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/rp2.superiorservers.co:27015");
        }

        private void btnC18_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/rp3.superiorservers.co:27015");
        }

        private void btnZombies_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/zrp.superiorservers.co:27015");
        }

        private void btnMilRP_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/milrp.superiorservers.co:27015");
        }

        private void btnCW1_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/cwrp.superiorservers.co:27015");
        }

        private void btnCW2_Click(object sender, EventArgs e)
        {
            Process.Start("steam://connect/cwrp2.superiorservers.co:27015");
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

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
                lblDT.Text = GetPlayerCount("208.103.169.12").ToString() + "/128";
                lblSD.Text = GetPlayerCount("208.103.169.13").ToString() + "/128";
                lblC18.Text = GetPlayerCount("208.103.169.15").ToString() + "/128";
                lblZRP.Text = GetPlayerCount("208.103.169.14").ToString() + "/128";
                lblMRP.Text = GetPlayerCount("208.103.169.18").ToString() + "/128";
                lblCW1.Text = GetPlayerCount("208.103.169.16").ToString() + "/128";
                lblCW2.Text = GetPlayerCount("208.103.169.17").ToString() + "/128";
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
}
