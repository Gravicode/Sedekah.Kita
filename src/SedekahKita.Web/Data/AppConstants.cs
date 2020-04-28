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

        public const int MaxReport = 5;
        public static string BlobConn { get; set; }

        public static string KondisiInDesc(int kondisi)
        {
            switch (kondisi)
            {
                case int n when (n < 3 ):
                    return ($"cukup");
                    //break;

                case int n when (n >=3 && n <= 6):
                    return ($"sedang");
                    //break;

                case int n when (n >=7 && n <= 8):
                    return ($"buruk");
                    //break;

                case int n when (n > 8):
                    return ($"sangat buruk");
                    //break;
            }
            return "";
            
        }
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
        public static Dictionary<string, string> JenisKebutuhan { get; set; } = new Dictionary<string, string>
        {
            {"ButuhPangan","Butuh Pangan"},
            {"ButuhPenghasilan","Butuh Penghasilan"},
            {"ButuhTempatTinggal","Butuh Tempat Tinggal"},
            {"ButuhPendidikan","Butuh Pendidikan"},
            {"ButuhPengobatan","Butuh Pengobatan"},
          

        };
      
    }
}
