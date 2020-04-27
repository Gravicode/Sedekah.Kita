using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;

namespace SedekahKita.Web.Data
{
    public class OrderDetailService : ICrud<OrderDetail>
    {
        TokoDB db;
        public OrderDetailService()
        {
            if (db == null) db = new TokoDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.OrderDetails
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.OrderDetails.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public List<OrderDetail> FindByKeyword(string Keyword)
        {
            var data = from x in db.OrderDetails
                       where x.Nama.Contains(Keyword) || x.Keterangan.Contains(Keyword) 
                       select x;
            return data.ToList();
        }

        public List<OrderDetail> GetAllData()
        {
            var data = from x in db.OrderDetails
                       select x;
            return data.ToList();
        }

        public OrderDetail GetDataById(object Id)
        {
            if (Id is int FID)
            {
                var data = from x in db.OrderDetails
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.OrderDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(OrderDetail data)
        {
            try
            {
                db.OrderDetails.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(OrderDetail data)
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
