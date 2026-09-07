using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_data
{ 
    // elso tabla ( markaid , marka ) masodik tabla ( tipusid, tipus ), harmadik: ( Id, markaid, tipusid, stb... )
    public class Marka
    {
        public static string Márka { get; set; }

    }
    public class Tipus 
    {
        public static string Típus { get; set; }

    }
    public class Auto 
    {
        public static int Id {  get; set; }
        public static string Üzemanyag { get; set; }
        public static int Teljesítmény_LE { get; set; }
        public static int Vételár_EUR { get; set; }
        public static int Gyártási_év { get; set; }
        public static int Átlagos_CO2_g_km { get; set; }
    }
}
