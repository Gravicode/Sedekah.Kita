using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;

namespace SedekahKita.Web.Data
{
    public class ProductPhotoService : ICrud<ProductPhoto>
    {
        TokoDB db;
        public ProductPhotoService()
        {
            if (db == null) db = new TokoDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.ProductPhotos
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.ProductPhotos.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteDataByProductId(object ProductId)
        {
            if (ProductId is long FID)
            {
                var data = (from x in db.ProductPhotos
                           where x.ProductId== FID
                           select x).ToList();
                foreach (var item in data)
                {
                    db.ProductPhotos.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ProductPhoto> FindByKeyword(string Keyword)
        {
            return GetAllData();
        }

        public List<ProductPhoto> GetAllData()
        {
            var data = from x in db.ProductPhotos
                       select x;
            return data.ToList();
        }

        public List<ProductPhoto> GetDataByProductId(long ProductId)
        {
            var data = from x in db.ProductPhotos
                       where x.ProductId == ProductId
                       select x;
            return data.ToList();
        }

        public ProductPhoto GetDataById(object Id)
        {
            if (Id is int FID)
            {
                var data = from x in db.ProductPhotos
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.ProductPhotos.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(ProductPhoto data)
        {
            try
            {
                db.ProductPhotos.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(ProductPhoto data)
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
