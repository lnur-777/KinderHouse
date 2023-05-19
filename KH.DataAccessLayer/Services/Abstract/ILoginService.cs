using KH.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Services.Abstract
{
    public interface ILoginService
    {
        bool Login(string username, string password, out User user);
    }
}
