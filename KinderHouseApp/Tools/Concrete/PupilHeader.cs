using KH.DataAccessLayer.StaticData;
using KinderHouseApp.Tools.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderHouseApp.Tools.Concrete
{
    public class PupilHeader : Header
    {
        public override string[] Name => Constants.PupilGridHeaderTexts;
    }
}
