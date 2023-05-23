using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        void Create (T entity);
        IQueryable<T> GetAll ();
        T Get (int id);
        void Update (T entity);
        void Delete (T entity);
        void SaveChanges();
    }
}
