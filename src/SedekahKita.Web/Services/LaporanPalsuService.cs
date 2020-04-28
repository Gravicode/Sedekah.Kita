using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;
using SedekahKita.Web.Data;

namespace SedekahKita.Web.Services
{
    public class LaporanPalsuService : ICrud<LaporanPalsu>
    {
        SedekahDB db;
        public LaporanPalsuService()
        {
            if (db == null) db = new SedekahDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.LaporanPalsus
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.LaporanPalsus.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public List<LaporanPalsu> FindByKeyword(string Keyword)
        {
            var data = from x in db.LaporanPalsus
                       where x.Keterangan.Contains(Keyword) || x.Pengirim.Contains(Keyword) 
                       select x;
            return data.ToList();
        }

        public List<LaporanPalsu> GetAllData()
        {
            var data = from x in db.LaporanPalsus.Include(c => c.PenerimaBantuan)
                       select x;
            return data.ToList();
        }
        public List<LaporanPalsu> GetDataByPenerima(PenerimaBantuan penerima)
        {
            var data = from x in db.LaporanPalsus.Include(c => c.PenerimaBantuan)
                       where x.PenerimaBantuan.Id == penerima.Id
                       select x;
            return data.ToList();
        }


        public LaporanPalsu GetDataById(object Id)
        {
            if (Id is int FID)
            {
                var data = from x in db.LaporanPalsus
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.LaporanPalsus.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public int TotalReportByPenerima(PenerimaBantuan penerima)
        {
            var data = from x in db.LaporanPalsus.Include(c => c.PenerimaBantuan)
                       where x.PenerimaBantuan.Id == penerima.Id
                       select x;
            return data.Count();
        }

        public bool InsertData(LaporanPalsu data)
        {
            try
            {
                db.LaporanPalsus.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(LaporanPalsu data)
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
