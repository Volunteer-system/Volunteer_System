using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models
{
    public interface IRepository<T>
    {
        T GetByid(int id);
        IEnumerable<T> GetAll();
        void Create(T _entity);
        void Update(T _entity);
        void Delete(T _entity);
    }
}