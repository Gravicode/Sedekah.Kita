using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;
using SedekahKita.Web.Data;

namespace SedekahKita.Web.Services
{
    public class DataPhotoService : ICrud<DataPhoto>
    {
        SedekahDB db;
        public DataPhotoService()
        {
            if (db == null) db = new SedekahDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.DataPhotos
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.DataPhotos.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteDataByPhotoKey(object PhotoKey)
        {
            if (PhotoKey is long FID)
            {
                var data = (from x in db.DataPhotos
                           where x.PhotoKey== FID
                            select x).ToList();
                foreach (var item in data)
                {
                    db.DataPhotos.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<DataPhoto> FindByKeyword(string Keyword)
        {
            return GetAllData();
        }

        public List<DataPhoto> GetAllData()
        {
            var data = from x in db.DataPhotos
                       select x;
            return data.ToList();
        }

        public List<DataPhoto> GetDataByPhotoKey(long PhotoKey)
        {
            var data = from x in db.DataPhotos
                       where x.PhotoKey == PhotoKey
                       select x;
            return data.ToList();
        }

        public DataPhoto GetDataById(object Id)
        {
            if (Id is int FID)
            {
                var data = from x in db.DataPhotos
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.DataPhotos.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(DataPhoto data)
        {
            try
            {
                db.DataPhotos.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(DataPhoto data)
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
