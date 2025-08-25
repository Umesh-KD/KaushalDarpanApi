using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ItiMerit
{
    public class ItiMeritMasterModel
    {
        public int MeritMasterId { get; set; }
        public string? SearchText { get; set; }
        public int Category { get; set; }
        public int Gender { get; set; }
        public string? Class { get; set; }
        public int AcademicYearID { get; set; }
        public int DepartmentId { get; set; }
        public string? Action { get; set; }
    }
}
