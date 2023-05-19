using KH.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Abstract
{
    public interface IWorkerRepository
    {
        IQueryable<Worker> GetWorkers();
    }
}
