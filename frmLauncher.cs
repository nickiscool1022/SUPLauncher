using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

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
    }
}
