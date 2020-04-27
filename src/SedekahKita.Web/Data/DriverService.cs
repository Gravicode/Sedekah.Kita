using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;

namespace SedekahKita.Web.Data
{
    public class DriverService : ICrud<Driver>
    {
        TokoDB db;
        public DriverService()
        {
            if (db == null) db = new TokoDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.Drivers
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.Drivers.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public List<Driver> FindByKeyword(string Keyword)
        {
            var data = from x in db.Drivers
                       where x.FullName.Contains(Keyword) || x.Email.Contains(Keyword) || x.SIM.Contains(Keyword) || x.KTP.Contains(Keyword) || x.Alamat.Contains(Keyword)
                       select x;
            return data.ToList();
        }
        public bool IsDriverExists(string Username)
        {
            try
            {
                var data = db.Drivers.Any(x => x.Username == Username);
                return data;
            }
            catch
            {
                return false;
            }
        }
        public List<Driver> GetAvailableDriver()
        {
            var data = from x in db.Drivers
                       where x.Aktif && x.IsReceiveNotification
                       select x;
            return data.ToList();
        }
        public List<Driver> GetAllData()
        {
            var data = from x in db.Drivers
                       select x;
            return data.ToList();
        }
        public Driver GetDataByUsername(string UName)
        {
            var data = from x in db.Drivers
                       where x.Username == UName
                       select x;
            return data.FirstOrDefault();
        }
        public Driver GetDataById(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.Drivers
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.Drivers.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(Driver data)
        {
            try
            {
                db.Drivers.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(Driver data)
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
