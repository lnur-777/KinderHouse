using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Concrete
{
    public class PositionRepository:IPositionRepository
    {
        private static ElnurhContext _context;
        public PositionRepository(ElnurhContext context)
        {
            _context = context;
        }

        public IQueryable<Position> GetPositions() => _context.Positions.AsQueryable();
    }
}
