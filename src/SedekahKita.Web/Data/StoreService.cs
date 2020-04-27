using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;

namespace SedekahKita.Web.Data
{
    public class StoreService : ICrud<Store>
    {
        TokoDB db;
        public StoreService()
        {
            if (db == null) db = new TokoDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.Stores
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.Stores.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public List<Store> FindByKeyword(string Keyword)
        {
            var data = from x in db.Stores
                       where x.Nama.Contains(Keyword) || x.Owner.Contains(Keyword) || x.Keterangan.Contains(Keyword) || x.Alamat.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Store> GetAllData()
        {
            var data = from x in db.Stores
                       select x;
            return data.ToList();
        }
        public List<Store> GetDataByOwner(string Owner)
        {
            var data = from x in db.Stores
                       where x.Owner == Owner
                       select x;
            return data.ToList();
        }
        public Store GetDataById(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.Stores
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.Stores.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(Store data)
        {
            try
            {
                db.Stores.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(Store data)
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
