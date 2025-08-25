using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BterMeritMaster
{
    public class BterMeritSearchModel
    {
        public int MeritMasterId { get; set; }
        public string? SearchText { get; set; }
        public string? Category { get; set; }
        public string? Gender { get; set; }
        public int? CourseType { get; set; }
        public int AcademicYearID { get; set; }
        public int EndTermId { get; set; }
        public int DepartmentId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int CreatedBy { get; set; }
        public string? Action { get; set; }
    }

    public class BterUploadMeritDataModel
    {
        public int CourseType { get; set; }
        public int AcademicYearID { get; set; }
        public int AllotmentMasterId { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }
        public List<BterMeritDataModel> MeritData{ get; set; }


}
    public class BterMeritDataModel
    {
        public int ApplicationNo { get; set; }
        public int MeritNo { get; set; }
    }

}
