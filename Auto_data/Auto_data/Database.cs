using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Auto_data
{
    internal static class Database
    {
        public static string connection = "server=127.0.0.1;port=3307;uid=root;database=auto";
        public static void DatabaseService() 
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                Console.WriteLine("Connection was successfull.");

                string createMarka = "CREATE TABLE IF NOT EXISTS Marka (MarkaID INT AUTO_INCREMENT PRIMARY KEY, Name VARCHAR(255)";
                using (MySqlCommand cmd = new MySqlCommand(createMarka, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string createTipus = "CREATE TABLE IF NOT EXISTS Tipus (TipusID INT AUTO_INCREMENT PRIMARY KEY, Name VARCHAR(255)";
                using (MySqlCommand cmd = new MySqlCommand(createTipus, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
