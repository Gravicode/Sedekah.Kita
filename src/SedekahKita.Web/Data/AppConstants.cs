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

        public static Dictionary<string, string> KategoriPenerima { get; set; } = new Dictionary<string, string>
        {
            {"Individu","Individu"},
            {"Keluarga","Keluarga"},
            {"Panti Asuhan","Panti Asuhan"},
            {"Tempat Ibadah","Tempat Ibadah"},
            {"Organisasi","Organisasi"},
            {"Lainnya","Lainnya"},
          
        };

        public static Dictionary<string, string> JenisBantuan { get; set; } = new Dictionary<string, string>
        {
            {"Makanan","Makanan/Sembako"},
            {"Obat","Obat"},
            {"Uang","Uang"},
            {"Buku","Buku"},
            {"Tempat Tinggal","Tempat Tinggal"},
            {"Pekerjaan","Pekerjaan"},
            {"Lainnya","Lainnya"},

        };
    }
}
