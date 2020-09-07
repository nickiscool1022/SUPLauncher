using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPLauncher
{
    class Database
    {
        static SteamBridge steam = new SteamBridge();
        static MySqlConnection connection;
        string connectionString;
        public void Connect()
        {
            string server = "nickiscool.cool";
            string database = "suplauncher";
            string uid = "suplauncher";
            string password = "penis";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }
        public void Insert()
        {
            string query = "REPLACE INTO stats(SteamID, DateUsed) VALUES('" + steam.GetSteamId() + "', '" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "')";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}