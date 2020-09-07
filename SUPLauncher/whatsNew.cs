using MarkdownSharp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUPLauncher
{
    public partial class whatsNew : Form
    {
        public whatsNew()
        {
            InitializeComponent();
            
        }

        private void whatsNew_Load(object sender, EventArgs e)
        {
            Panel p1 = new Panel();
            p1.Dock = DockStyle.Top;
            p1.BackColor = Color.Transparent;
            p1.Size = new Size(0, 105);
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.UseDefaultCredentials = true;
            HttpWebRequest request = WebRequest.CreateHttp("https://api.github.com/repos/nickiscool1022/SUPLauncher/releases/tags/" + Application.ProductVersion);
            request.UserAgent = "Browser";
            string markdown = "";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse(); // Get Response from webrequest
                StreamReader sr = new StreamReader(response.GetResponseStream()); // Create stream to access web data
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(sr.ReadToEnd());

                markdown = "**" + result.tag_name.ToString() + "** \n\r" + result.body.ToString();
            } catch(System.Net.WebException)
            {
                markdown = "**Developer Version** \n\rYou are currently on a version that has not been released on github yet!";
            }

            



            
            Markdown m = new Markdown();
            webBrowser1.DocumentText = @"
            <link href='https://fonts.googleapis.com/css2?family=Mulish:wght@700&family=Roboto&display=swap' rel='stylesheet'>
            <style>
                body {
                    color: white;
                    background: rgb(25, 25, 25);
                    font-family: 'Roboto';
                    font-size: 14px
                    -webkit-highlight: none;
                    user-select: none;
                    -webkit-touch-callout: none;
                }

                .launcher {
                    font-family: 'Mulish';
                    font-size: 28px;
                    margin-top: -10px;
                }

            </style>
            
            <img src='https://i.imgur.com/doWamuT.png'></img><br>

            " + m.Transform(markdown);
            this.BringToFront();
            
        }
        bool isTopPanelDragged = false;
        Point offset;
        Size _normalWindowSize;
        Point _normalWindowLocation = Point.Empty;
        private void TopBar_MouseUp(object sender, MouseEventArgs e)
        {
            isTopPanelDragged = false;
            if (this.Location.Y <= 5)
            {

                _normalWindowSize = this.Size;
                _normalWindowLocation = this.Location;

                Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                this.Location = new Point(0, 0);
                this.Size = new System.Drawing.Size(rect.Width, rect.Height);


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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
