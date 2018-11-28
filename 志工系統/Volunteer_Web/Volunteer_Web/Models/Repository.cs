using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        VolunteerEntities dbContext = null;
        DbSet<T> Dbset = null;

        public Repository()
        {
            dbContext = new VolunteerEntities();
            Dbset = dbContext.Set<T>();
        }

        public void Create(T _entity)
        {
            Dbset.Add(_entity);
            dbContext.SaveChanges();
        }

        public void Delete(T _entity)
        {
            Dbset.Remove(_entity);
            dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return Dbset;
        }

        public T GetByid(int id)
        {
            return Dbset.Find(id);
        }

        public void Update(T _entity)
        {
            dbContext.Entry(_entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
        //========================================三益預排表用▼=======================================================
        public void Insert_Volunteer_Service_period(int Volunteer_no, int[] Wish_order, List<int[]> Service_period_no)
        {
           var user_detail = dbContext.Service_period2.Where(v => v.Volunteer_no == Volunteer_no).ToList();
            foreach (var del in user_detail)
            {
                dbContext.Service_period2.Remove(del);
            }
            for (int i = 0; i < Wish_order.Length; i += 1)
            {
                for (int j = 0; j < Service_period_no[i].Length; j += 1)
                {
                    Service_period2 sp = new Service_period2();
                    sp.Volunteer_no = Volunteer_no;
                    sp.Wish_order = Wish_order[i];
                    sp.Service_period_no = Service_period_no[i][j];
                    dbContext.Service_period2.Add(sp);
                }
            }
            dbContext.SaveChanges();
        }
        //========================================三益預排表用▲=======================================================
    }

}