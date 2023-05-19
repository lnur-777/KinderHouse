using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Concrete
{
    internal class WorkerRepository : IWorkerRepository
    {
        private static ElnurhContext _context;
        public WorkerRepository(ElnurhContext context)
        {
            _context = context;
        }
        public IQueryable<Worker> GetWorkers() => _context.Workers.AsQueryable();
    }
}
