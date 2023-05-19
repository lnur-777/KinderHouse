using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        ElnurhContext _context;
        public UserRepository(ElnurhContext context)
        {
            _context = context;
        }
        public IQueryable<User> GetUsers()
        {
            return _context.Users.AsQueryable();
        }
    }
}
