using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SedekahKita.Web.Models;

namespace SedekahKita.Web.Data
{
    public class RoleService : ICrud<Role>
    {
        TokoDB db;
        public RoleService()
        {
            if (db == null) db = new TokoDB();
            //db.Database.EnsureCreated();
        }
        public bool DeleteData(object Id)
        {
            if (Id is long FID)
            {
                var data = from x in db.Roles
                           where x.Id == FID
                           select x;
                foreach (var item in data)
                {
                    db.Roles.Remove(item);
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public List<Role> FindByKeyword(string Keyword)
        {
            var data = from x in db.Roles
                       where x.RoleName.Contains(Keyword) 
                       select x;
            return data.ToList();
        }

        public List<Role> GetAllData()
        {
            var data = from x in db.Roles
                       select x;
            return data.ToList();
        }

        public Role GetDataById(object Id)
        {
            if (Id is int FID)
            {
                var data = from x in db.Roles
                           where x.Id == FID
                           select x;
                return data.FirstOrDefault();
            }
            return default;
        }

        public long GetLastId()
        {
            var lastId = db.Roles.OrderByDescending(x => x.Id).FirstOrDefault();
            return lastId.Id + 1;
        }

        public bool InsertData(Role data)
        {
            try
            {
                db.Roles.Add(data);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }

        }

        public bool UpdateData(Role data)
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
