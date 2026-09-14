using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Auto_data
{
    internal static class Database
    {
        public static string connection = "server=127.0.0.1;port=3307;uid=root;database=auto";

        public static void DropTables()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();

                string dropAuto = "DROP TABLE IF EXISTS Auto";
                using (MySqlCommand cmd = new MySqlCommand(dropAuto, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string dropTipus = "DROP TABLE IF EXISTS Tipus";
                using (MySqlCommand cmd = new MySqlCommand(dropTipus, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string dropMarka = "DROP TABLE IF EXISTS Marka";
                using (MySqlCommand cmd = new MySqlCommand(dropMarka, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DatabaseService() 
        {
            int markaID;
            int tipusID;

            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();

                string createMarka = "CREATE TABLE IF NOT EXISTS Marka (MarkaID INT AUTO_INCREMENT PRIMARY KEY, Name VARCHAR(255) NOT NULL UNIQUE)";
                using (MySqlCommand cmd = new MySqlCommand(createMarka, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string createTipus = "CREATE TABLE IF NOT EXISTS Tipus (TipusID INT AUTO_INCREMENT PRIMARY KEY, MarkaID INT NOT NULL, Name VARCHAR(255) NOT NULL, UNIQUE (MarkaID, Name), FOREIGN KEY (MarkaID) REFERENCES Marka(MarkaID))";
                using (MySqlCommand cmd = new MySqlCommand(createTipus, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string createAuto = "CREATE TABLE IF NOT EXISTS Auto (AutoID INT AUTO_INCREMENT PRIMARY KEY, TipusID INT NOT NULL, Üzemanyag VARCHAR(255), Teljesítmény_LE INT, VételÁR_EUR INT, Gyártási_Év INT, Átlagos_CO2_g_km INT, FOREIGN KEY (TipusID) REFERENCES Tipus(TipusID))";
                using (MySqlCommand cmd = new MySqlCommand(createAuto, conn))
                {
                    cmd.ExecuteNonQuery();
                }


                string insertMarka = "INSERT INTO Marka (Name) VALUES (@BrandName) ON DUPLICATE KEY UPDATE MarkaID = LAST_INSERT_ID(MarkaID);";

                using (MySqlCommand cmd = new MySqlCommand(insertMarka, conn))
                {
                    cmd.Parameters.AddWithValue("@BrandName", Marka.Márka);
                    cmd.ExecuteNonQuery();

                    markaID = Convert.ToInt32(cmd.LastInsertedId);
                }

                string selectMarkaID = "SELECT MarkaID FROM Marka WHERE Name = @MarkaName";
                string insertTipus = "INSERT INTO Tipus (Name, MarkaID) VALUES (@TipusName, @MarkaID) ON DUPLICATE KEY UPDATE TipusID = LAST_INSERT_ID(TipusID)";
                using (MySqlCommand cmd = new MySqlCommand(selectMarkaID, conn))
                {
                    cmd.Parameters.AddWithValue("@MarkaName", Marka.Márka);

                    markaID = Convert.ToInt32(cmd.ExecuteScalar());
                        
                }

                using (MySqlCommand cmd2 = new MySqlCommand(insertTipus, conn))
                {
                    cmd2.Parameters.AddWithValue("@TipusName", Tipus.Típus);
                    cmd2.Parameters.AddWithValue("@MarkaID", markaID);

                    cmd2.ExecuteNonQuery();

                    tipusID = Convert.ToInt32(cmd2.LastInsertedId);
                }

                string insertAuto = "INSERT INTO Auto ( TipusID, Üzemanyag, Teljesítmény_LE, VételÁR_EUR, Gyártási_Év, Átlagos_CO2_g_km ) VALUES ( @TipusID, @Üzemanyag, @Teljesítmény_LE, @VételÁR_EUR, @Gyártási_Év, @Átlagos_CO2_g_km )";
                using (MySqlCommand cmd2 = new MySqlCommand(insertAuto, conn))
                {

                    cmd2.Parameters.AddWithValue("@TipusID", tipusID);
                    cmd2.Parameters.AddWithValue("@Üzemanyag", Auto.Üzemanyag);
                    cmd2.Parameters.AddWithValue("@Teljesítmény_LE", Auto.Teljesítmény_LE);
                    cmd2.Parameters.AddWithValue("@VételÁR_EUR", Auto.Vételár_EUR);
                    cmd2.Parameters.AddWithValue("@Gyártási_Év", Auto.Gyártási_év);
                    cmd2.Parameters.AddWithValue("@Átlagos_CO2_g_km", Auto.Átlagos_CO2_g_km);

                    cmd2.ExecuteNonQuery();
                }

            }
        }
    }
}
