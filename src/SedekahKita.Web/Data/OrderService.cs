using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;

namespace SedekahKita.Web.Data
{
    public class OrderService : ICrud<Order>
    {
        TokoDB db;
        public OrderService()
        {
            if (db == null) db = new TokoDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.Orders
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.Orders.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public List<Order> FindByKeyword(string Keyword)
        {
            var data = from x in db.Orders
                       where x.NoOrder.Contains(Keyword) || x.Pemesan.Contains(Keyword) || x.Alamat.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Order> GetAllData()
        {
            var data = from x in db.Orders
                       select x;
            return data.ToList();
        }

        public Order GetDataById(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.Orders
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.Orders.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(Order data)
        {
            try
            {
                db.Orders.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }
        public bool CloseOrder(Order Header,List<OrderDetail> Detail)
        {
            try
            {
                db.Orders.Add(Header);
                Detail.ForEach(x => {
                    x.Order = Header;
                    db.OrderDetails.Add(x);
                    });
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateData(Order data)
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
