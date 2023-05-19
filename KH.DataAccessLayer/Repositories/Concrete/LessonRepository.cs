using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Concrete
{
    public class LessonRepository : ILessonRepository
    {
        private static ElnurhContext _context;
        public LessonRepository(ElnurhContext context)
        {
            _context = context;
        }
        public IQueryable<Lesson> GetLessons() => _context.Lessons.AsQueryable();
    }
}
