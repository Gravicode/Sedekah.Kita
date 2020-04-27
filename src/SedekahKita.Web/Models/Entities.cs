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
    public class CartTemp
    {
        public Order Header { get; set; }
        public Dictionary<long,OrderDetail> Detail { get; set; }
    }
    public class Cart
    {
        public Order Header { get; set; }
        public List<OrderDetail> Detail { get; set; }
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
    }

    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string RoleName { get; set; }

    }

    public class Store
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public long Id { get; set; }
        [Required(ErrorMessage = "Nama wajib di isi")]
        public string Nama { get; set; }
        [Required(ErrorMessage = "Keterangan wajib di isi")]
        public string Keterangan { get; set; }
        [Required(ErrorMessage = "Alamat wajib di isi")]
        public string Alamat { get; set; }
        [Required(ErrorMessage = "No Hp wajib di isi")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email wajib di isi")]
        public string Email { get; set; }
        public string Owner { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<Product> Products { get; set; }
    }
    public class Product
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public long Id { get; set; }
        public long TokoId { get; set; }
        [Required(ErrorMessage = "Nama wajib di isi")]
        public string Nama { get; set; }
        [Required(ErrorMessage = "Kategori wajib di isi")]
        public string Kategori { get; set; }
        [Required(ErrorMessage = "Keterangan wajib di isi")]
        public string Keterangan { get; set; }
        [Required(ErrorMessage = "Harga wajib di isi")]
        public double Harga { get; set; }
        public string Satuan { get; set; }
        public double PotonganHarga { get; set; }
        public string KodePromo { get; set; }
        public bool Aktif { get; set; }
        public Store Store { get; set; }
    }

    public class Driver
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [Required(ErrorMessage = "Username wajib di isi")]
        public string Username { get; set; }
        [Required(ErrorMessage = "FullName wajib di isi")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Phone wajib di isi")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email wajib di isi")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Alamat wajib di isi")]
        public string Alamat { get; set; }
        [Required(ErrorMessage = "SIM wajib di isi")]
        public string SIM { get; set; }
        [Required(ErrorMessage = "KTP wajib di isi")]
        public string KTP { get; set; }
        
        public string PicUrl { get; set; }
        public bool Aktif { get; set; }
        public bool IsReceiveNotification { get; set; }
    }

    public enum StatusDelivery
    {
        Menunggu=0, Mengambil, Mengirim, Diterima, Batal
    }
    public class OrderDelivery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public DateTime TanggalDelivery { get; set; }
        public long DriverId { get; set; }
        public string DriverUserName { get; set; }
        public string DriverFullName { get; set; }
        public string DriverPhone { get; set; }
        public string DriverEmail { get; set; }
        public long OrderId { get; set; }
        public string Penerima { get; set; }
        public string PenerimaAlamat { get; set; }
        public string PenerimaPhone { get; set; }
        public string PenerimaEmail { get; set; }
        public string DeskripsiBarang { get; set; }
        public double BiayaKirim { get; set; }
        public string Pemesan { get; set; }
        public string PemesanAlamat { get; set; }
        public string PemesanPhone { get; set; }
        public string PemesanEmail { get; set; }
        public StatusDelivery Status { get; set; }

    }
    public class ProductPhoto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [Required(ErrorMessage = "Pemesan wajib di isi")]
        public string Pemesan { get; set; }
        [Required(ErrorMessage = "NoOrder wajib di isi")]
        public string NoOrder { get; set; }
        [Required(ErrorMessage = "Alamat wajib di isi")]
        public string Alamat { get; set; }
        public string Email { get; set; }
        public string NamaPemesan { get; set; }
        public string Phone { get; set; }
        public DateTime TanggalOrder { get; set; }
        public double TotalHarga { get; set; }
        public double TotalPotonganHarga { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public Order Order { set; get; }
        public long TokoId { get; set; }
        [Required(ErrorMessage = "Nama wajib di isi")]
        public string Nama { get; set; }
        [Required(ErrorMessage = "Kategori wajib di isi")]
        public string Kategori { get; set; }
        [Required(ErrorMessage = "Keterangan wajib di isi")]
        public string Keterangan { get; set; }
        [Required(ErrorMessage = "Harga wajib di isi")]
        public double Harga { get; set; }
        public string Satuan { get; set; }
        public double PotonganHarga { get; set; }
        public string KodePromo { get; set; }
        public bool Aktif { get; set; }
        public long ProductId { set; get; }
        public int Qty { get; set; }
        public double TotalHarga { get; set; }
        public double TotalPotonganHarga { get; set; }

    }
}
