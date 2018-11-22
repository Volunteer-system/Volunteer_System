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


    }
        
}