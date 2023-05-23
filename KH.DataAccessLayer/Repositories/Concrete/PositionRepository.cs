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
    public class PositionRepository: BaseRepository<Position>, IPositionRepository
    {
        private static ElnurhContext _context;
        DbSet<Position> Table;
        public PositionRepository(ElnurhContext context) : base(context)
        {
            _context = context;
            Table = _context.Set<Position>();
        }

        public new void Create(Position entity)
        {
            Table.Add(entity);
        }

        public new void Delete(Position entity)
        {
            Table.Remove(entity);
        }

        public new Position Get(int id)
        {
            return Table.FirstOrDefault(x => x.Id == id);
        }

        public new IQueryable<Position> GetAll() => Table.AsQueryable();

        public new void Update(Position entity)
        {
            Table.Update(entity);
        }
    }
}
