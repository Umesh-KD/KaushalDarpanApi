using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BterStudentJoinStatus
{
    public class BterStudentsJoinStatusMarksMedel
    {
       
        public int ReportingId { get; set; }
        public int AllotmentId { get; set; }
        public string? ReportingStatus { get; set; }
        public string? ReportingRemark { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }

    }
    public class BterStudentsJoinStatusMarksSearchModel
    {


        public int AllotmentId { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeId { get; set; }
        public int AcademicYearID { get; set; }
        public int AllotmentMasterId { get; set; }

        public int ApplicationID { get; set; }

        public int CollegeId { get; set; }

        public int StreamId { get; set; }
        public string? ReportingStatus { get; set; }
        public int EndTermId { get; set; }
        public int AllotmentStatus { get; set; }
        public string ApplicationNo { get; set; }
 
        public int PageNumber { get; set; }
        public int PageSize { get; set; }


    }
}
