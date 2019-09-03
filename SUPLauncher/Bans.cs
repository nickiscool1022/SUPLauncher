using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SUPLauncher
{
    // FORM SIZE (W/O BANS): 1049, 650
    // FORM SIZE (W/ BANS) :
    public partial class Bans : Form
    {
        private readonly string steamid = "";
        bool isTopPanelDragged = false;
        bool isWindowMaximized = false;
        Point offset;
        Size _normalWindowSize;
        Point _normalWindowLocation = Point.Empty;
        JObject accountData = null;

        /// <summary>
        /// Open up a profile of the specified steamid.
        /// </summary>
        /// <param name="steamID">The steamid of the profile to display. Can be 32 or 64</param>
        public Bans(string steamID)
        {
            InitializeComponent();
            steamid = steamID;
        }
        
        
        // NORMAL: 499, 321
        // NOT NORMAL: 499, 251

        private void Bans_Load(object sender, EventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                string s = wc.DownloadString("https://superiorservers.co/api/profile/" + steamid);
                accountData = JObject.Parse(s);
            }
            lblUsername.Text = accountData["Badmin"]["Name"].ToString();
            Avatar.Size = new Size(100, 100);
            byte[] imageData = new WebClient().DownloadData("https://superiorservers.co/api/avatar/" + accountData["SteamID64"].ToString());
            using (var ms = new MemoryStream(imageData))
            {
                Avatar.BackgroundImage = Image.FromStream(ms);
                ms.Close();
            }
            lblFirstJoin.Text = Epoch2string(Convert.ToInt32(accountData["Badmin"]["FirstJoin"]), "M/d/yyyy, h:mm:ss tt");
            var time = int.Parse(accountData["Badmin"]["LastSeen"].ToString());
            if ((time / 60) <= 2)
                lblLastSeen.Text = "RIGHT NOW";
            else
            {
                lblLastSeen.Text = ConvertTime(time, false, "MO,W,D,H,M,") + "AGO";
            }
            lblTimePlayed.Text = ConvertTime(int.Parse(accountData["Badmin"]["PlayTime"].ToString()), true, "");
            lblDarkRP.Text = accountData["Badmin"]["Ranks"]["DarkRP & Zombies"].ToString();
            lblMilRP.Text = accountData["Badmin"]["Ranks"]["MilRP"].ToString();
            lblCWRP.Text = accountData["Badmin"]["Ranks"]["CWRP"].ToString();
            if (lblDarkRP.Text.ToLower() != "user" || lblDarkRP.Text.ToLower() != "vip")
            {
                JArray staffData = null;
                using (WebClient wc = new WebClient())
                {
                    string s = wc.DownloadString("https://superiorservers.co/api/staff/RP?_=0" + steamid);
                    staffData = JArray.Parse(s);
                }

                foreach (JArray staff in staffData)
                {
                 if (accountData["SteamID64"].ToString() == staff[1].ToString())
                    {
                        if (Int32.Parse(staff[3].ToString()) >= 0)
                        {
                            drpExpire.Text = "Expires in: " + ConvertTime(Int32.Parse(staff[3].ToString()), false, "W,D,H,M,");
                        } else
                        {
                            drpExpire.Text = "Never Expires";
                        }
                        drpExpire.Visible = true;
                        
                    }   
                }
            }
            if (milrpExpire.Text.ToLower() != "user" || milrpExpire.Text.ToLower() != "vip")
            {
                JArray staffData = null;
                using (WebClient wc = new WebClient())
                {
                    string s = wc.DownloadString("https://superiorservers.co/api/staff/MilRP?_=0" + steamid);
                    staffData = JArray.Parse(s);
                }
                foreach (JArray staff in staffData)
                {
                    if (accountData["SteamID64"].ToString() == staff[1].ToString())
                    {
                        if (Int32.Parse(staff[3].ToString()) >= 0)
                        {
                            milrpExpire.Text = "Expires in: " + ConvertTime(Int32.Parse(staff[3].ToString()), false, "W,D,H,M,");
                        }
                        else
                        {
                            milrpExpire.Text = "Never Expires";
                        }
                        milrpExpire.Visible = true;

                    }
                }
            }
            if (cwrpExpire.Text.ToLower() != "user" || cwrpExpire.Text.ToLower() != "vip")
            {
                JArray staffData = null;
                using (WebClient wc = new WebClient())
                {
                    string s = wc.DownloadString("https://superiorservers.co/api/staff/SWRP?_=0" + steamid);
                    staffData = JArray.Parse(s);
                }
                foreach (JArray staff in staffData)
                {
                    if (accountData["SteamID64"].ToString() == staff[1].ToString())
                    {
                        if (Int32.Parse(staff[3].ToString()) >= 0)
                        {
                            cwrpExpire.Text = "Expires in: " + ConvertTime(Int32.Parse(staff[3].ToString()), false, "W,D,H,M,");
                        }
                        else
                        {
                            cwrpExpire.Text = "Never Expires";
                        }
                        cwrpExpire.Visible = true;
                    }
                }
            }
            txtS32.Text = System.Environment.NewLine + accountData["SteamID32"].ToString();
            txtS64.Text = System.Environment.NewLine + accountData["SteamID64"].ToString();
            txtSProfile.Text = System.Environment.NewLine + accountData["SteamURL"].ToString();
            if (accountData["ForumURL"].ToString() != "")
            {
                lblForumHeader.Visible = true;
                Links.Size = new Size(499, 321);
                txtFProfile.Text = System.Environment.NewLine + accountData["ForumURL"].ToString();
            }
            else
            {
                Links.Size = new Size(499, 251);
                lblForumHeader.Visible = false;
            }

            
            lblUsername.Text = lblUsername.Text.ToUpper();
            lblFirstJoin.Text = lblFirstJoin.Text.ToUpper();
            lblLastSeen.Text = lblLastSeen.Text.ToUpper();
            lblTimePlayed.Text = lblTimePlayed.Text.ToUpper();

            lblDarkRP.Text = lblDarkRP.Text.ToUpper();
            lblMilRP.Text = lblMilRP.Text.ToUpper();
            lblCWRP.Text = lblCWRP.Text.ToUpper();
            int Move = 5;
            for (int iii = 0; iii < lblUsername.Text.Length; iii++) // Keep everything in the form and make everything neat
            {
                InfoPanel.Size = new System.Drawing.Size(InfoPanel.Size.Width + Move, InfoPanel.Height);
                RanksPanel.Size = new System.Drawing.Size(InfoPanel.Size.Width + Move, InfoPanel.Height);
                Links.Location = new Point(Links.Location.X + Move, Links.Location.Y);
                panel1.Size = new System.Drawing.Size(panel1.Size.Width + Move, panel1.Height);
                Infolabel1.Location = new Point(Infolabel1.Location.X + (Move / 2), Infolabel1.Location.Y);
                Infolabel2.Location = new Point(Infolabel2.Location.X + Move, Infolabel2.Location.Y);
                Infolabel3.Location = new Point(Infolabel3.Location.X + Move, Infolabel3.Location.Y);
                Infolabel4.Location = new Point(Infolabel4.Location.X + Move, Infolabel4.Location.Y);
                panel7.Location = new Point(panel7.Location.X + Move, panel7.Location.Y);
                panel8.Location = new Point(panel8.Location.X + Move, panel8.Location.Y);
                panel12.Location = new Point(panel12.Location.X + Move, panel12.Location.Y);
                panel9.Location = new Point(panel9.Location.X + (Move / 2), panel9.Location.Y);
                panel13.Location = new Point(panel13.Location.X + (Move / 2), panel13.Location.Y);
                panel10.Location = new Point(panel10.Location.X + (Move / 2), panel10.Location.Y);
                label19.Location = new Point(label19.Location.X + (Move / 2), label19.Location.Y);
                label15.Location = new Point(label15.Location.X + (Move / 2), label15.Location.Y); 
                label17.Location = new Point(label17.Location.X + (Move / 2), label17.Location.Y);
                Avatar.Location = new Point(Avatar.Location.X + Move, Avatar.Location.Y);
                POsPanel.Size = new System.Drawing.Size(POsPanel.Size.Width + Move, POsPanel.Size.Height);
                topBar.Size = new Size(this.Size.Width + Move, topBar.Height);
                Width = this.Size.Width + Move;
                drpExpire.Location = new Point(drpExpire.Location.X + (Move / 2), drpExpire.Location.Y);
                milrpExpire.Location = new Point(milrpExpire.Location.X + (Move / 2), milrpExpire.Location.Y);
                cwrpExpire.Location = new Point(cwrpExpire.Location.X + (Move / 2), cwrpExpire.Location.Y);
            }
            //MaximumSize = new Size(this.Size.Width + Move, this.Size.Height);
            //MaximumSize = new System.Drawing.Size(this.Size.Width + Move, this.Size.Height);
            //Size = new System.Drawing.Size(this.Size.Width + Move, this.Size.Height);
            panel15.Size = new System.Drawing.Size(POsPanel.Size.Width, panel15.Size.Height);
            int i = 0;
            foreach (JObject Ban in accountData["Badmin"]["Bans"])
            {
                i = i + 1;
                CreatePOItem(Ban, i);
            }
            if (i == 0)
            {
                this.Size = new Size(this.Size.Width, 475);
            }
            MaximumSize = new Size(this.Size.Width + Move, this.Size.Height);
            this.TopMost = true;
        }
        private string Epoch2string(int epoch, string format) // https://www.epochconverter.com/
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch).ToString(format);
        }
        /// <summary>
        /// Turn seconds into a string format. You can specify a format with the format param.
        /// </summary>
        /// <param name="secs">Seconds</param>
        /// <param name="Short">Instead of using the format param it will be formatted as playtime like "H:M:S". format param can be blank</param>
        /// <param name="format">The format you would like to turn the seconds into. Valid formats: "Y," "M," "W," "D," "M," "S,". They all must end with a ","</param>
        /// <returns></returns>
        public static string ConvertTime(long secs, bool Short, string format)
        {
            TimeSpan t = TimeSpan.FromSeconds(secs);
            format = format.ToUpper();
            double days = t.Days;
            if (!Short)
            {
            if (days >= 365)
                {
                    format = format.Replace("Y,", (days / 365) + "Y ");
                    while (days >= 365)
                        days = days - 365;
                } else format = format.Replace("Y,", "");

                if (days >= 30.417)
                {
                    format = format.Replace("MO,", Math.Floor(days / 30.417) + "MO ");
                    while (days >= 30.417)
                        days = days - 30.417;
                } else format = format.Replace("MO,", "");
                
                if (days >= 7)
                {
                    format = format.Replace("W,", Math.Floor(days / 7) + "W ");
                    days = days - (days / 7) * 7;
                } else format = format.Replace("W,", "");
                if (days > 0) format = format.Replace("D,", Math.Floor(days) + "D "); else format = format.Replace("D,", "");
                if (t.Hours > 0) format = format.Replace("H,", t.Hours + "H "); else format = format.Replace("H,", "");
                if (t.Minutes < 1 & t.Seconds > 0) format.Replace("M,", t.Seconds + "S "); format.Replace("S,", t.Seconds + "");
                if (t.Minutes > 0) format = format.Replace("M,", t.Minutes + "M "); else format = format.Replace("M,", "");
                if (t.Seconds > 0) format = format.Replace("S,", t.Seconds + "S "); else format = format.Replace("S,", "");
            }
            else
            {
                if (Math.Floor(t.TotalHours) > 0)
                {
                    format += (Math.Floor(t.TotalHours)) + ":";
                }
                else
                    format += "00:";
                if (t.Minutes > 0)
                {
                    format += t.Minutes + ":";
                }
                else
                    format += "00";
                if (t.Seconds > 0)
                    format += t.Seconds;
                else
                    format += "0" + t.Seconds;
            }
            return format;

        }
        private void BtnCopy32_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtS32.Text.Trim());
        }
        private void BtnCopy64_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtS64.Text.Trim());
        }
        private void BtnOpenSteam_Click(object sender, EventArgs e)
        {
            Process.Start(txtSProfile.Text.Trim());
        }
        private void BtnOpenForum_Click(object sender, EventArgs e)
        {
            Process.Start(txtFProfile.Text.Trim());
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void TopBar_MouseUp(object sender, MouseEventArgs e)
        {
            isTopPanelDragged = false;
            if (this.Location.Y <= 5)
            {
                if (!isWindowMaximized)
                {
                    _normalWindowSize = this.Size;
                    _normalWindowLocation = this.Location;

                    Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                    this.Location = new Point(0, 0);
                    this.Size = new System.Drawing.Size(rect.Width, rect.Height);

                    isWindowMaximized = true;
                }
            }
        }
        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isTopPanelDragged)
            {
                Point newPoint = topBar.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(offset);
                this.Location = newPoint;
                if (this.Location.X > 2 || this.Location.Y > 2)
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.Location = _normalWindowLocation;
                        this.Size = _normalWindowSize;
                        isWindowMaximized = false;
                    }
                }
            }
        }
        private void TopBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isTopPanelDragged = true;
                Point pointStartPosition = this.PointToScreen(new Point(e.X, e.Y));
                offset = new Point
                {
                    X = this.Location.X - pointStartPosition.X,
                    Y = this.Location.Y - pointStartPosition.Y
                };
            }
            else
            {
                isTopPanelDragged = false;
            }
            if (e.Clicks == 2)
            {
                isTopPanelDragged = false;
            }
        }
        /// <summary>
        /// Create a PO item on the bans form
        /// </summary>
        /// <param name="PO">The JSON Object of the PO to display</param>
        /// <param name="index">The index of the PO</param>
        private void CreatePOItem(JObject PO, int index)
        {
            string date = Epoch2string(Convert.ToInt32(PO["Time"]), "M/d/yyyy, h:mm:ss tt");
            string length = ConvertTime(Convert.ToInt32(PO["Length"]), false, "Y,MO,W,D,H,M,");
            if(PO["Length"].ToString() == "0")
            {
                length = "Permanent";
            }
            PO poitem = new PO(PO["BanID"].ToString(), date, PO["Server"].ToString(), PO["Name"].ToString(), PO["SteamID64"].ToString(), PO["AdminName"].ToString(), PO["AdminSteamID64"].ToString(), length,PO["Reason"].ToString(), PO["UnbanReason"].ToString(), bool.Parse(PO["IsActive"].ToString()));
            POsPanel.Controls.Add(poitem);
            poitem.Location = new Point(0, (index * 50) + 47);
            poitem.Size = new Size(1025, 52);
            poitem.Show();
            POsPanel.Size = new Size(POsPanel.Size.Width, POsPanel.Size.Height + 47);
            poitem.Size = new Size(POsPanel.Size.Width, poitem.Height);
            this.MaximumSize = new Size(this.Size.Width, this.Size.Height + 47);
            this.Size = new Size(this.Size.Width, this.Size.Height + 47);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
