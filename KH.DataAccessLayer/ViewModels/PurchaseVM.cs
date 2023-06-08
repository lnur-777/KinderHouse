using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.ViewModels
{
    public class PurchaseVM : BaseViewModel
    {
        public new int ID { get; set; }
        public string? PupilName { get; set; }
        public string? PaidAmount { get; set; }
        public string? MonthlyAmount { get; set; }
        public string? Date { get; set; }
        public string? Note { get; set; }
    }
}
