using KH.DataAccessLayer.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Services.Abstract
{
    public interface IService
    {
        List<object> GetAll();
        bool Update(BaseViewModel viewModel);
        bool UpdateAll(IEnumerable viewModels);
        bool Create(BaseViewModel viewModel);
        bool Delete(BaseViewModel viewModel);
    }
}
