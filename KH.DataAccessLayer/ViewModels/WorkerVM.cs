using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.ViewModels
{
    public class WorkerVM : BaseViewModel
    {
        public new int ID { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? FatherName { get; set; }
        public string? Position { get; set; }
        public string? Lesson { get; set; }
        public string? Salary { get; set; }
        public string? RegisterDate { get; set; }
        public string? Note { get; set; }
    }
}
