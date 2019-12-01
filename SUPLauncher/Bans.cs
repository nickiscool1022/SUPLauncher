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
using System.Windows;
using System.Windows.Forms;
using System.IO;
using CefSharp.WinForms;
using CefSharp;
using System.Reflection;

namespace SUPLauncher
{
    public partial class Bans : Form
    {
        bool isTopPanelDragged = false;
        bool isWindowMaximized = false;
        Point offset;
        Size _normalWindowSize;
        Point _normalWindowLocation = Point.Empty;
        string steamid = "";
        ChromiumWebBrowser chrome = new ChromiumWebBrowser();
        bool opened = false;
        
        //bool opened = false;
        /// <summary>
        /// Open up a profile of the specified steamid.
        /// </summary>
        /// <param name="steamID">The steamid of the profile to display. Can be 32 or 64</param>
        public Bans(string steamID)
        {
            InitializeComponent();
            InitializeChromium(steamID);
            steamid = steamID;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height / 2 + 100;
        }

        private void InitializeChromium(string steamID)
        {
            chrome.Load("https://superiorservers.co/profile/" + steamID);
            this.panel1.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //private void Bans_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    Cef.Shutdown();
        //}

        private void Bans_Load(object sender, EventArgs e)
        {
            InitializeChromium(steamid);
        }
    }
}
