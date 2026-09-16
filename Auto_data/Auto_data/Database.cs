using MySql.Data.MySqlClient;
using System;

namespace Auto_data
{
    internal static class Database
    {
        public static string connection =
            "server=127.0.0.1;port=3307;uid=root;database=auto;";

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

        public static void DatabaseService(Auto auto,Marka marka,Tipus tipus)
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();

                string createMarka = "CREATE TABLE IF NOT EXISTS Marka ( MarkaID INT AUTO_INCREMENT PRIMARY KEY, Name VARCHAR(255) NOT NULL UNIQUE)";

                using (MySqlCommand cmd = new MySqlCommand(createMarka, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string createTipus = "CREATE TABLE IF NOT EXISTS Tipus ( TipusID INT AUTO_INCREMENT PRIMARY KEY, MarkaID INT NOT NULL, Name VARCHAR(255) NOT NULL, UNIQUE (MarkaID, Name), FOREIGN KEY (MarkaID) REFERENCES Marka(MarkaID))";

                using (MySqlCommand cmd = new MySqlCommand(createTipus, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string createAuto = "CREATE TABLE IF NOT EXISTS Auto ( AutoID INT PRIMARY KEY, TipusID INT NOT NULL, Üzemanyag VARCHAR(255), Teljesítmény_LE INT, Vételár_EUR INT, Gyártási_Év INT, Átlagos_CO2_g_km INT, FOREIGN KEY (TipusID) REFERENCES Tipus(TipusID))";

                using (MySqlCommand cmd = new MySqlCommand(createAuto, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string insertMarka = @"
                    INSERT INTO Marka (Name)
                    VALUES (@MarkaName)
                    ON DUPLICATE KEY UPDATE MarkaID = LAST_INSERT_ID(MarkaID)";

                int markaID;

                using (MySqlCommand cmd = new MySqlCommand(insertMarka, conn))
                {
                    cmd.Parameters.AddWithValue("@MarkaName", marka.Márka);
                    cmd.ExecuteNonQuery();

                    markaID = Convert.ToInt32(cmd.LastInsertedId);
                }

                string insertTipus = @"
                    INSERT INTO Tipus (Name, MarkaID)
                    VALUES (@TipusName, @MarkaID)
                    ON DUPLICATE KEY UPDATE TipusID = LAST_INSERT_ID(TipusID)";

                int tipusID;

                using (MySqlCommand cmd = new MySqlCommand(insertTipus, conn))
                {
                    cmd.Parameters.AddWithValue("@TipusName", tipus.Típus);
                    cmd.Parameters.AddWithValue("@MarkaID", markaID);

                    cmd.ExecuteNonQuery();

                    tipusID = Convert.ToInt32(cmd.LastInsertedId);
                }

                string insertAuto = @"
                    INSERT INTO Auto
                    (
                        AutoID,
                        TipusID,
                        Üzemanyag,
                        Teljesítmény_LE,
                        Vételár_EUR,
                        Gyártási_Év,
                        Átlagos_CO2_g_km
                    )
                    VALUES
                    (
                        @AutoID,
                        @TipusID,
                        @Üzemanyag,
                        @Teljesítmény_LE,
                        @Vételár_EUR,
                        @Gyártási_Év,
                        @Átlagos_CO2_g_km
                    )";

                using (MySqlCommand cmd = new MySqlCommand(insertAuto, conn))
                {
                    cmd.Parameters.AddWithValue("@AutoID", auto.Id);
                    cmd.Parameters.AddWithValue("@TipusID", tipusID);
                    cmd.Parameters.AddWithValue("@Üzemanyag", auto.Üzemanyag);
                    cmd.Parameters.AddWithValue("@Teljesítmény_LE", auto.Teljesítmény_LE);
                    cmd.Parameters.AddWithValue("@Vételár_EUR", auto.Vételár_EUR);
                    cmd.Parameters.AddWithValue("@Gyártási_Év", auto.Gyártási_év);
                    cmd.Parameters.AddWithValue("@Átlagos_CO2_g_km", auto.Átlagos_CO2_g_km);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}