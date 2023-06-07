using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.ViewModels
{
    public class PupilVM : BaseViewModel
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public string? Birthday { get; set; }
        public string? Orientation { get; set; }
        public string? RegisterDate { get; set; }
        public string? SectorName { get; set; }
        public string? ParentMaritalStatus { get; set; }
    }
}
