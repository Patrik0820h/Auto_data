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
        public string Márka { get; set; }

    }
    public class Tipus 
    {
        public string Típus { get; set; }

    }
    public class Auto 
    {
        public int Id {  get; set; }
        public string Üzemanyag { get; set; }
        public int Teljesítmény_LE { get; set; }
        public int Vételár_EUR { get; set; }
        public int Gyártási_év { get; set; }
        public int Átlagos_CO2_g_km { get; set; }
    }
}
