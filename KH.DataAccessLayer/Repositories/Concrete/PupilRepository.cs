using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Concrete
{
    public class PupilRepository : IPupilRepository
    {
        private static ElnurhContext _context;
        public PupilRepository(ElnurhContext context)
        {
            _context = context;
        }
        public IQueryable<Pupil> GetPupils() => _context.Pupils.AsQueryable();

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
