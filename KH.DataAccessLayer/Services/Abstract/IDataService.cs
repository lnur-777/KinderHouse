using KH.DataAccessLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Services.Abstract
{
    public interface IDataService
    {
        List<object> GetData<T>() where T : class;
        void UpdateData(IEnumerable viewModels, string type);
    }
}
