using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SUPLauncher
{
    public partial class PO : UserControl
    {
        string offenderID = "";
        string adminID = "";
        /// <summary>
        /// Create a PO Item
        /// </summary>
        public PO(String BanID, String Date, String Server, String Offender, string OffenderSteamID64, String Admin, String AdminSteamID64, String length, String reason, String unbanreason, bool isActive)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            BanID_Label.Text = BanID;
            Date_label.Text = Date;
            Server_label.Text = Server;
            Offender_label.Text = Offender;
            offenderID = OffenderSteamID64;
            adminID = AdminSteamID64;
            byte[] offenderimageData = new WebClient().DownloadData("https://superiorservers.co/api/avatar/" + OffenderSteamID64);
            using (var ms = new MemoryStream(offenderimageData))
            {
                OffenderPic.BackgroundImage = Image.FromStream(ms);
                ms.Close();
            }
            byte[] adminimageData = new WebClient().DownloadData("https://superiorservers.co/api/avatar/" + AdminSteamID64);
            using (var ms = new MemoryStream(adminimageData))
            {
                AdminPic.BackgroundImage = Image.FromStream(ms);
                ms.Close();
            }
            //OffenderPic.Load("https://superiorservers.co/api/avatar/" + OffenderSteamID64);
            Admin_label.Text = Admin;
            //AdminPic.Load("https://superiorservers.co/api/avatar/" + AdminSteamID64);
            Length_label.Text = length;
            Reason_label.Text = reason;

            if (BanID == "") // Jail
            {
                Ban_panel.BackColor = Color.FromArgb(48, 43, 40);
                BanActive.BackColor = Color.FromArgb(113, 65, 18);
                foreach (Control control in Ban_panel.Controls)
                {
                    if (control != BanActive && control != OffenderPic && control != AdminPic)
                    {
                        control.BackColor = Color.FromArgb(48, 43, 40);
                    }
                }
            }
            else if (isActive) // Active Ban
            {
                Ban_panel.BackColor = Color.FromArgb(49, 32, 39);
                BanActive.BackColor = Color.FromArgb(114, 14, 18);
                foreach (Control control in Ban_panel.Controls)
                {

                    if (control != BanActive && control != OffenderPic && control != AdminPic)
                    {
                        control.BackColor = Color.FromArgb(49, 32, 39);
                    }
                }

            }
            else if (unbanreason != "") // Unban
            {
                Ban_panel.BackColor = Color.FromArgb(25, 44, 36);
                BanActive.BackColor = Color.FromArgb(12, 113, 18);
                foreach (Control control in Ban_panel.Controls)
                {

                    if (control != BanActive && control != OffenderPic && control != AdminPic)
                    {
                        control.BackColor = Color.FromArgb(25, 44, 36);
                    }
                }
            }
            else
            {
                Ban_panel.BackColor = Color.FromArgb(18, 23, 30);
                BanActive.BackColor = Color.FromArgb(53, 67, 91);

                foreach (Control control in Ban_panel.Controls)
                {
                    if (control != BanActive && control != OffenderPic && control != AdminPic)
                    {
                        control.BackColor = Color.FromArgb(18, 23, 30);
                    }
                }
            }

        }
        private void Offender_label_MouseClick(object sender, MouseEventArgs e)
        {
            new Bans(offenderID).Show();
        }
        private void Admin_label_MouseClick(object sender, MouseEventArgs e)
        {
            new Bans(adminID).Show();
        }
        private void Offender_label_MouseHover(object sender, EventArgs e)
        {
            //Offender_label.ForeColor = Color.DimGray;
        }
        private void Admin_label_MouseHover(object sender, EventArgs e)
        {
            //Admin_label.ForeColor = Color.DimGray;
        }
        private void PO_Load(object sender, EventArgs e)
        {
            BackColor = Ban_panel.BackColor;

        }

        private void Offender_label_MouseHover_1(object sender, EventArgs e)
        {
            Offender_label.ForeColor = Color.White;
        }

        private void Offender_label_MouseLeave(object sender, EventArgs e)
        {
            Offender_label.ForeColor = Color.Silver;
        }

        private void Admin_label_MouseHover_1(object sender, EventArgs e)
        {
            Admin_label.ForeColor = Color.White;
        }

        private void Admin_label_MouseLeave(object sender, EventArgs e)
        {
            Admin_label.ForeColor = Color.Silver;
        }

        private void Offender_label_Click(object sender, EventArgs e)
        {
            new Bans(offenderID).Show();
        }

        private void Admin_label_Click(object sender, EventArgs e)
        {
            new Bans(adminID).Show();
        }
    }
}
