using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;

namespace SedekahKita.Web.Data
{
    public class ProductService : ICrud<Product>
    {
        TokoDB db;
        public ProductService()
        {
            if (db == null) db = new TokoDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.Products
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.Products.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Product> FindByKeyword(string Keyword)
        {
            var data = from x in db.Products
                       where x.Nama.Contains(Keyword) || x.Keterangan.Contains(Keyword) || x.Kategori.Contains(Keyword)
                       select x;
            return data.ToList();
        }
       
        public List<Product> FindByKeywordAndCategory(string Keyword,string Category,bool ShowAll=false,int LimitItem=100)
        {

            bool JustPass = ("Semua" == Category);
            var data = from x in db.Products
                       where (x.Nama.ToLower().Contains(Keyword) || x.Keterangan.ToLower().Contains(Keyword)) && (JustPass || Category == x.Kategori)
                       orderby x.Nama
                       select x;
            return ShowAll ? data.ToList() : data.Take(LimitItem).ToList();
        }
        public List<Product> GetAllData()
        {
            var data = from x in db.Products
                       select x;
            return data.ToList();
        }
        public List<Product> GetDataByStoreId(long StoreId)
        {
            var data = from x in db.Products
                       where x.TokoId == StoreId
                       select x;
            return data.ToList();
        }
        public Product GetDataById(object Id)
        {
            if (Id is int FID)
            {
                var data = from x in db.Products
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.Products.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }
       
        public bool InsertData(Product data)
        {
            try
            {
               
                db.Products.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(Product data)
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
