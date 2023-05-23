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
    public class PupilRepository : BaseRepository<Pupil>, IPupilRepository
    {

        private static ElnurhContext _context;
        DbSet<Pupil> Table;
        public PupilRepository(ElnurhContext context) : base(context)
        {
            _context = context;
            Table = _context.Set<Pupil>();
        }

        public new void Create(Pupil entity)
        {
            Table.Add(entity);
        }

        public new void Delete(Pupil entity)
        {
            Table.Remove(entity);
        }

        public new Pupil Get(int id)
        {
            return Table.FirstOrDefault(x => x.Id == id);
        }

        public new IQueryable<Pupil> GetAll() => Table.AsQueryable();

        public new void Update(Pupil entity)
        {
            Table.Update(entity);
        }
    }
}
