using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUPLauncher
{
    class ClientUpdater
    {

    public static void Update()
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




                    if (File.Exists(Application.StartupPath + "/update.bat"))
                    {
                        File.Delete(Application.StartupPath + "/update.bat");
                    }

                    FileStream fs = new FileStream(Application.StartupPath + "/update.temp", FileMode.Create);
                    byte[] read = new byte[255]; // Buffer
                    int count = s.Read(read, 0, read.Length); // Start position

                    while (count > 0) // Keep reading bytes until count = -1 (-1 is returned in this when there are no more bytes to read
                    {
                        fs.Write(read, 0, count); // Write
                        count = s.Read(read, 0, read.Length); // Get new count
                    }
                    fs.Close(); // Close file stream after it is done
                    using (var batFile = new StreamWriter(File.Create(Application.StartupPath + "/Update.bat")))
                    {
                        batFile.WriteLine("@ECHO OFF");

                        batFile.WriteLine("TIMEOUT /t 1 /nobreak > NUL");
                        batFile.WriteLine("TASKKILL /IM \"{0}\" > NUL", Application.ExecutablePath);
                        batFile.WriteLine("DEL \"{0}\"", Application.ExecutablePath);
                        batFile.WriteLine("COPY \"{0}\" \"{1}\"", Application.StartupPath + "\\update.temp", Application.ExecutablePath);
                        batFile.WriteLine("DEL \"{1}\" & DEL \"{2}\" & START \"\" /B \"{0}\"", Application.ExecutablePath, Application.StartupPath + "\\Update.bat", Application.StartupPath + "\\update.temp");
                    }
                    Properties.Settings.Default.updatePopup = false;
                    Properties.Settings.Default.Save();
                    ProcessStartInfo startInfo = new ProcessStartInfo(Application.StartupPath + "/Update.bat");
                    // Hide the terminal window
                    startInfo.CreateNoWindow = true;
                    startInfo.UseShellExecute = false;
                    startInfo.WorkingDirectory = Application.StartupPath;
                    Process.Start(startInfo);
                    Process.GetCurrentProcess().Kill();

                }
                else
                {
                    Properties.Settings.Default.updatePopup = true;
                    Properties.Settings.Default.Save();

                }
            }
    }

        public static bool checkForUpdates()
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
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
