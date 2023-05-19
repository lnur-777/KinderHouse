using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.StaticData.Dictionaries
{
    public static class MaritalStatus
    {
        public static IDictionary<int, string> StatusList = new Dictionary<int, string>()
        {
            { 1,"Evli" },
            { 2,"Boşanmış" }
        };
    }
}
