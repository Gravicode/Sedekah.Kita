using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;

namespace SedekahKita.Web.Data
{
    public class OrderDeliveryService : ICrud<OrderDelivery>
    {
        TokoDB db;
        public OrderDeliveryService()
        {
            if (db == null) db = new TokoDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.OrderDeliveries
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.OrderDeliveries.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public List<OrderDelivery> FindByKeyword(string Keyword)
        {
            var data = from x in db.OrderDeliveries
                       where x.DeskripsiBarang.Contains(Keyword) || x.DriverFullName.Contains(Keyword) || x.Pemesan.Contains(Keyword) || x.Penerima.Contains(Keyword) || x.PemesanAlamat.Contains(Keyword) || x.PenerimaAlamat.Contains(Keyword)
                       select x;
            return data.ToList();
        }
        public List<OrderDelivery> GetLastWeekData()
        {
            var StartDate = DateTime.Now.AddDays(-7);
            var data = from x in db.OrderDeliveries
                       where x.TanggalDelivery > StartDate
                       orderby x.Id descending
                       select x;
            return data.ToList();
        }
        public List<OrderDelivery> GetAllData()
        {
            var data = from x in db.OrderDeliveries
                       select x;
            return data.ToList();
        }
        public List<OrderDelivery> GetDataByUsername(string UName)
        {
            var data = from x in db.OrderDeliveries
                       where x.DriverUserName == UName
                       select x;
            return data.ToList();
        }
        public OrderDelivery GetDataById(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.OrderDeliveries
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.OrderDeliveries.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(OrderDelivery data)
        {
            try
            {
                db.OrderDeliveries.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(OrderDelivery data)
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
