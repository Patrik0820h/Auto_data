using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_data
{
    internal class Program
    {
        public static string path = "auto_adatok.csv";
        static void Main(string[] args)
        {
            Database.DropTables();
            var adatok = CsvService.Read(path);

            foreach (var item in adatok) 
            {
                Database.DatabaseService(item.auto, item.marka, item.tipus);
            }
        }
    }
}
