using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;
using SedekahKita.Web.Data;
using SedekahKita.Web.Helpers;

namespace SedekahKita.Web.Services
{
    public class PenerimaBantuanService : ICrud<PenerimaBantuan>
    {
        SedekahDB db;
        public PenerimaBantuanService()
        {
            if (db == null) db = new SedekahDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.PenerimaBantuans
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.PenerimaBantuans.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public List<PenerimaBantuan> FindByKeyword(string Keyword)
        {
            var data = from x in db.PenerimaBantuans
                       where x.Alamat.Contains(Keyword) || x.Nama.Contains(Keyword) || x.Phone.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<PenerimaBantuan> GetAllData()
        {
            var data = from x in db.PenerimaBantuans
                       select x;
            return data.ToList();
        }
        //GetAllDataByLocation
        public List<PenerimaBantuanExt> GetAllDataByLocation(string Keyword, GeoLocation currentLoc)
        {
            var data = (from x in db.PenerimaBantuans
                        where (string.IsNullOrEmpty(Keyword) || x.Alamat.Contains(Keyword) || x.Nama.Contains(Keyword) || x.Phone.Contains(Keyword) || x.Keterangan.Contains(Keyword)) && x.Aktif
                        select new PenerimaBantuanExt() { Aktif = x.Aktif, Alamat = x.Alamat, CreatedBy = x.CreatedBy, Bantuans=x.Bantuans,
                            Distance = (currentLoc==null ? 0 : GeoTool.Distance(currentLoc.Latitude, currentLoc.Longitude, x.Latitude, x.Longitude, 'k')),
                            CreatedDate = x.CreatedDate, Email = x.Email, Id = x.Id , JumlahJiwa=x.JumlahJiwa, Kategori=x.Kategori, Kebutuhan=x.Kebutuhan,
                             Keterangan=x.Keterangan, Kondisi=x.Kondisi, Latitude=x.Latitude, Longitude=x.Longitude, Nama=x.Nama, Phone=x.Phone, PhotoUrl=x.PhotoUrl
                       });
            return data.ToList();
        }


        public PenerimaBantuan GetDataById(object Id)
        {
            if (Id is int FID)
            {
                var data = from x in db.PenerimaBantuans
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.PenerimaBantuans.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(PenerimaBantuan data)
        {
            try
            {
                db.PenerimaBantuans.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(PenerimaBantuan data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;


            }

        }
    }
}
