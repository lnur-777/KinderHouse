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
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        private static ElnurhContext _context;
        DbSet<Lesson> Table;
        public LessonRepository(ElnurhContext context) : base(context)
        {
            _context = context;
            Table = _context.Set<Lesson>();
        }

        public new void Create(Lesson entity)
        {
            Table.Add(entity);
        }

        public new void Delete(Lesson entity)
        {
            Table.Remove(entity);
        }

        public new Lesson Get(int id)
        {
            return Table.FirstOrDefault(x => x.Id == id);
        }

        public new IQueryable<Lesson> GetAll() => Table.AsQueryable();

        public new void Update(Lesson entity)
        {
            Table.Update(entity);
        }
    }
}
