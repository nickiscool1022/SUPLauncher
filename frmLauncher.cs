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

        private void frmLauncher_Load(object sender, EventArgs e)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            var i = 0;
            foreach (DriveInfo d in allDrives)
            {
                if (!File.Exists(d.Name + @"\Program Files (x86)\Steam\steamapps\common\GarrysMod\hl2.exe") && i > allDrives.Length - 1)
                {
                    MessageBox.Show("Garry's Mod executeable not found, closing application.", "hl2.exe not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                i++;
            }
        }
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
    }
}
