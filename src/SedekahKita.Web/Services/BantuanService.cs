using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;
using SedekahKita.Web.Data;

namespace SedekahKita.Web.Services
{
    public class BantuanService : ICrud<Bantuan>
    {
        SedekahDB db;
        public BantuanService()
        {
            if (db == null) db = new SedekahDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.Bantuans
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.Bantuans.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }
       

        public List<Bantuan> FindByKeyword(string Keyword)
        {
            var data = from x in db.Bantuans
                       where x.Keterangan.ToLower().Contains(Keyword) || x.Owner.ToLower().Contains(Keyword) || x.Pengirim.ToLower().Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Bantuan> GetAllData()
        {
            var data = from x in db.Bantuans.Include(c=>c.PenerimaBantuan)
                       select x;
            return data.ToList();
        }
        public List<Bantuan> GetDataByPenerima(PenerimaBantuan penerima)
        {
            var data = from x in db.Bantuans.Include(c => c.PenerimaBantuan)
                       where x.PenerimaBantuan.Id == penerima.Id
                       select x;
            return data.ToList();
        }


        public Bantuan GetDataById(object Id)
        {
            if (Id is int FID)
            {
                var data = from x in db.Bantuans
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.Bantuans.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(Bantuan data)
        {
            try
            {
                db.Bantuans.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(Bantuan data)
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
