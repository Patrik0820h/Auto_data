using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_data
{
    internal static class CsvService
    {
        public static void Read(string path) 
        {

            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] splittedline = line.Split(',');

                    Auto.Id = int.Parse(splittedline[0]);
                    Marka.Márka = splittedline[1];
                    Tipus.Típus = splittedline[2];
                    Auto.Üzemanyag = splittedline[3];
                    Auto.Teljesítmény_LE = int.Parse(splittedline[4]);
                    Auto.Vételár_EUR = int.Parse(splittedline[5]);
                    Auto.Gyártási_év = int.Parse(splittedline[6]);
                    Auto.Átlagos_CO2_g_km = int.Parse(splittedline[7]);

                    Database.DatabaseService();
                }
            }
        }
    }
}
