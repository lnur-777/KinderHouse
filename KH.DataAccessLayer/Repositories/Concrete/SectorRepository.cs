using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Concrete
{
    public class SectorRepository : ISectorRepository
    {
        private static ElnurhContext _context;
        public SectorRepository(ElnurhContext context)
        {
            _context = context;
        }
        public IQueryable<Sector> GetSectors() => _context.Sectors.AsQueryable();
    }
}
