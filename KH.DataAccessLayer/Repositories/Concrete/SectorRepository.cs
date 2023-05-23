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
    public class SectorRepository : BaseRepository<Sector>, ISectorRepository
    {
        private static ElnurhContext _context;
        DbSet<Sector> Table;
        public SectorRepository(ElnurhContext context) : base(context)
        {
            _context = context;
            Table = _context.Set<Sector>();
        }

        public new void Create(Sector entity)
        {
            Table.Add(entity);
        }

        public new void Delete(Sector entity)
        {
            Table.Remove(entity);
        }

        public new Sector Get(int id)
        {
            return Table.FirstOrDefault(x => x.Id == id);
        }

        public new IQueryable<Sector> GetAll() => Table.AsQueryable();

        public new void Update(Sector entity)
        {
            Table.Update(entity);
        }
    }
}
