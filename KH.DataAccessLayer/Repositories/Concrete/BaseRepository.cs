using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Concrete
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        DbSet<T> Table;
        ElnurhContext _context;
        public BaseRepository(ElnurhContext context)
        {
            _context = context;
            Table = _context.Set<T>();
        }
        public void Create(T entity)
        {
            Table.Add(entity);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public T Get(int id)
        {
            return Table.FirstOrDefault(x=>x.Equals(id));
        }

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

    }
}
