using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ItiMerit
{
    public class ItiMeritSearchModel
    {
        public int MeritMasterId { get; set; }
        public string? SearchText { get; set; }
        public string? Category { get; set; }
        public string? Gender { get; set; }
        public string? Class { get; set; }
        public int AcademicYearID { get; set; }
        public int DepartmentId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int CreatedBy { get; set; }
        public string? Action { get; set; }
    }
}
