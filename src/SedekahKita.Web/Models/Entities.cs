using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SedekahKita.Web.Models
{
    public class Entities
    {
    }
    public class UserProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public ICollection<Role> Roles { get; set; }
    }

    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]

        public long Id { get; set; }

        public UserProfile UserProfile { set; get; }
        public string RoleName { get; set; }

    }

    public class Bantuan
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public long Id { get; set; }
        [Required(ErrorMessage = "Keterangan wajib di isi")]
        public string Keterangan { get; set; }
        [Required(ErrorMessage = "Jenis wajib di isi")]
        public string Jenis { get; set; }
        [Required(ErrorMessage = "Jumlah wajib di isi")]
        public double Jumlah { get; set; }
        [Required(ErrorMessage = "Satuan wajib di isi")]
        public string Satuan { get; set; }
        [Required(ErrorMessage = "Tanggal terima wajib di isi")]
        public DateTime TanggalTerima { get; set; }
        [Required(ErrorMessage = "Tanggal kirim wajib di isi")]
        public DateTime TanggalKirim { get; set; }
        [Required(ErrorMessage = "Pengirim wajib di isi")]
        public string Pengirim { get; set; }
        [Required(ErrorMessage = "Alamat pengirim wajib di isi")]
        public string AlamatPengirim { get; set; }
        [Required(ErrorMessage = "Hp pengirim wajib di isi")]
        public string HpPengirim { get; set; }
        public StatusBantuan Status { get; set; }
        public string PhotoUrl { get; set; }
        public string Owner { set; get; }
        public PenerimaBantuan PenerimaBantuan { set; get; }

    }

    public enum StatusBantuan { Menunggu, Dikirim, Diterima, Batal };

    public class PenerimaBantuan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [Required(ErrorMessage = "Nama wajib di isi")]
        public string Nama { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Alamat wajib di isi")]
        public string Alamat { get; set; }
        [Required(ErrorMessage = "Kategori wajib di isi")]
        public string Kategori { get; set; }
        [Required(ErrorMessage = "Jumlah jiwa wajib di isi")]
        public int JumlahJiwa { get; set; }

        [Required(ErrorMessage = "Kondisi di isi")]
        public int Kondisi { get; set; } // 1 - 10 (baik - buruk)
        [Required(ErrorMessage = "Status di isi")]
        public StatusKebutuhan Kebutuhan { get; set; } // 1 - 10 (baik - buruk)
        [Required(ErrorMessage = "Photo harus di upload")]
        public string PhotoUrl { get; set; }
        public bool Aktif { get; set; } = true;
        public ICollection<Bantuan> Bantuans { get; set; }
    }
    public enum StatusKebutuhan { ButuhPangan, ButuhPenghasilan, ButuhTempatTinggal, ButuhPendidikan, ButuhPengobatan };

    public class DataPhoto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public long PhotoKey { get; set; }
        public string TableRef { get; set; }
        public string PhotoUrl { get; set; }
    }


}
