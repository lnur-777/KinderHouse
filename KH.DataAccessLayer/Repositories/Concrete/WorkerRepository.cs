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
    public class WorkerRepository :BaseRepository<Worker>, IWorkerRepository
    {
        private static ElnurhContext _context;
        DbSet<Worker> Table;
        public WorkerRepository(ElnurhContext context) : base(context)
        {
            _context = context;
            Table = _context.Set<Worker>();
        }

        public new void Create(Worker entity)
        {
            Table.Add(entity);
        }

        public new void Delete(Worker entity)
        {
            Table.Remove(entity);
        }

        public new Worker Get(int id)
        {
            return Table.FirstOrDefault(x => x.Id == id);
        }

        public new IQueryable<Worker> GetAll() => Table.AsQueryable();

        public new void Update(Worker entity)
        {
            Table.Update(entity);
        }
    }
}
