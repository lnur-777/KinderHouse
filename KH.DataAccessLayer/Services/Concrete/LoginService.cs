using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using KH.DataAccessLayer.Repositories.Concrete;
using KH.DataAccessLayer.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Services.Concrete
{
    public class LoginService : ILoginService
    {
        private readonly ElnurhContext _context;
        IUserRepository _userRepository; 
        public LoginService()
        {
            _context = new();
            _userRepository ??= new UserRepository(_context);
        }
        public bool Login(string username, string password, out User user)
        {
            user = _userRepository.GetUsers().Where(x => x.UserName == username && x.Password == password).SingleOrDefault();
            return user != null;
        }
    }
}
