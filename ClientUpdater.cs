using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Net;
using System;

namespace SUPLauncher
{
    /// <summary>
    ///  A class for updating the SUP Launcher client
    /// </summary>
    class ClientUpdater
    {

        /// <summary>
        ///  The method for updating the client
        /// </summary>
        public void Update()
        {
            string[] webData;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Secure security protocol for querying the github API
            HttpWebRequest request = WebRequest.CreateHttp("http://api.github.com/repos/nicksuperiorservers/SUPLauncher/releases/latest");
            request.UserAgent = "Nick";
            WebResponse response = null;
            response = request.GetResponse(); // Get Response from webrequest
            StreamReader sr = new StreamReader(response.GetResponseStream()); // Create stream to access web data
            string currentRecord = sr.ReadToEnd(); // Read data from response stream
            webData = currentRecord.Split(Convert.ToChar(",")); // Split raw web data into different elements of array
            string newestVersion = webData[7].Substring(webData[7].LastIndexOf(":") + 2, webData[7].Length - webData[7].LastIndexOf(":") - 3); // Get newest version
            string currentVersion = Application.ProductVersion; // Get current version of assembly
            if (newestVersion.Contains(currentVersion) == false) // If current program is not newest version -
            {
                if (Interaction.MsgBox("You do not have the lastest version(" + newestVersion + "). Would you like to go download the latest version?", MsgBoxStyle.YesNo, "Download latest version") == MsgBoxResult.Yes) // If they choose to update
                {
                    int counter = 0;
                    while (webData[counter].Contains("browser_download_url") == false) // Search for browser download url
                    {
                        counter++;
                    }
                    string downloadURL = webData[counter].Substring(webData[counter].IndexOf("https://"), webData[counter].LastIndexOf("}") - 1 - webData[counter].IndexOf("https://")); // Get download URL
                    HttpWebRequest requestDL = WebRequest.CreateHttp(downloadURL); // Download Request
                    WebResponse responseDL = null; // Download response
                    responseDL = requestDL.GetResponse(); // Get response from Download Request
                    Stream s = responseDL.GetResponseStream(); // Get Response Stream from Download Request and store it
                    FolderBrowserDialog fbd = new FolderBrowserDialog(); // Folder browser dialog
                    fbd.SelectedPath = Application.StartupPath; // Store startup path and automatically go there
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        FileStream fs = new FileStream(fbd.SelectedPath + @"\SUPLauncher-" + newestVersion + ".exe", FileMode.Create); // FileStream for saving file
                        byte[] read = new byte[255]; // Buffer
                        int count = s.Read(read, 0, read.Length); // Start position
                        while (count > 0) // Keep reading bytes until count = -1 (-1 is returned in this when there are no more bytes to read
                        {
                            fs.Write(read, 0, count); // Write
                            count = s.Read(read, 0, read.Length); // Get new count
                        }
                        fs.Close(); // Close file stream after it is done
                        MessageBox.Show("Client updated. Make sure you delete the old .exe.", "Client Updated.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fbd.SelectedPath + @"\SUPLauncher-" + newestVersion + ".exe"); // Start new SUPLauncher
                        foreach (var process in Process.GetProcessesByName("SUPLauncher")) // Kill old one
                        {
                            process.Kill();
                        }
                    }
                }
            }
        }
    }
}
