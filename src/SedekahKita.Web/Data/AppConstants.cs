using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SedekahKita.Web.Data
{
    public class AppConstants
    {
        public static string SQLConn { get; set; }
        public static string GMapApiKey { get; set; }
        public static int BiayaKirim { get; set; } = 10000;
        public static string BlobConn { get; set; }

        public static Dictionary<string, string> KategoriBarang { get; set; } = new Dictionary<string, string>
        {
            {"Makanan","Makanan"},
            {"Pakaian","Pakaian"},
            {"Jasa","Jasa"},
            {"ATK","ATK"},
            {"Elektronik","Elektronik"},
            {"Bahan Pokok","Bahan Pokok"},
            {"Kosmetik","Kosmetik"},
            {"Obat","Obat"},
            {"Peralatan Rumah Tangga","Peralatan Rumah Tangga"}
        };
    }
}
