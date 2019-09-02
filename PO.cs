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
            GetOffenderAvatar();
            GetAdminAvatar();
            Admin_label.Text = Admin;
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
            this.Location = new Point(this.Location.X + 4, this.Location.Y);
        }
        void GetOffenderAvatar()
        {
            OffenderPic.BackgroundImage = Bans.avatarImage;
            //try
            //{
            //    byte[] adminimageData = new WebClient().DownloadData("https://superiorservers.co/api/avatar/" + offenderID);
            //    using (var ms = new MemoryStream(adminimageData))
            //    {
            //        OffenderPic.BackgroundImage = Image.FromStream(ms);
            //        ms.Close();
            //        return;
            //    }
            //}
            //catch
            //{
            //    return;
            //}
        }
        void GetAdminAvatar()
        {
            try
            {
                byte[] adminimageData = new WebClient().DownloadData("https://superiorservers.co/api/avatar/" + adminID);
                using (var ms = new MemoryStream(adminimageData))
                {
                    AdminPic.BackgroundImage = Image.FromStream(ms);
                    ms.Close();
                    return;
                }
            }
            catch
            {
                return;
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
            Offender_label.ForeColor = Color.White;
        }
        private void Admin_label_MouseHover(object sender, EventArgs e)
        {
            Admin_label.ForeColor = Color.White;
        }
        private void PO_Load(object sender, EventArgs e)
        {
            BackColor = Ban_panel.BackColor;

        }

        private void Offender_label_Click(object sender, EventArgs e)
        {
            new Bans(offenderID).Show();
        }

        private void Admin_label_Click(object sender, EventArgs e)
        {
            new Bans(adminID).Show();
        }

        private void Offender_label_MouseLeave(object sender, EventArgs e)
        {
            Offender_label.ForeColor = Color.Silver;
        }

        private void Admin_label_MouseLeave(object sender, EventArgs e)
        {
            Admin_label.ForeColor = Color.Silver;
        }
    }
}
