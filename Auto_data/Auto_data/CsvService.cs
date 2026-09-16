using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Auto_data
{
    public static class CsvService
    {
        public static List<(Auto auto, Marka marka, Tipus tipus)> Read(string path)
        {
            var result = new List<(Auto, Marka, Tipus)>();

            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] splittedLine = line.Split(',');

                    var auto = new Auto
                    {
                        Id = int.Parse(splittedLine[0]),
                        Üzemanyag = splittedLine[3],
                        Teljesítmény_LE = int.Parse(splittedLine[4]),
                        Vételár_EUR = int.Parse(splittedLine[5]),
                        Gyártási_év = int.Parse(splittedLine[6]),
                        Átlagos_CO2_g_km = int.Parse(splittedLine[7])
                    };

                    var marka = new Marka
                    {
                        Márka = splittedLine[1]
                    };

                    var tipus = new Tipus
                    {
                        Típus = splittedLine[2]
                    };

                    result.Add((auto, marka, tipus));
                }
            }

            return result;
        }

    }

}
