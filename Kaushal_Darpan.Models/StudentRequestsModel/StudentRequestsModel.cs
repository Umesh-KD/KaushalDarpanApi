using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.StudentApplyForHostel
{
    public class StudentRequestsModel
    {
        public int ReqId { get; set; }
        public int InstituteId { get; set; }
        public int StudentApplicationId { get; set; }
        public int StudentId { get; set; }
        public int SemesterId { get; set; }
        public int AllotmentStatus { get; set; }
        public int BrachId { get; set; }
        public int EndTermId { get; set; }
        public string Remark { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string IPAddress { get; set; }
        public int CourseTypeID { get; set; }
        public int DepartmentID { get; set; }

    }

    public class SearchStudentApplyForHostel
    {
        public int InstituteID { get; set; }
        public int HostelID { get; set; }
        public int SemesterId { get; set; }
        public int BrachId { get; set; }
        public int EndTermId { get; set; }

        public int DepartmentID { get; set; }
        public int AllotmentStatus { get; set; }
        public string Action { get; set; }
        public List<GenerateStudentMeritList> ReqId { get; set; }

    }
    public class GenerateStudentMeritList
    {
       public string? ReqId { get; set; }

    }

    public class PublishHostelMeritListDataModel
    {
        public int? ReqId { get; set; }
        public int? ModifyBy { get; set; }
    }


    public class SearchStudentApplyForHostelWarden
    {
        public int InstituteID { get; set; }
        public int HostelID { get; set; }
        public int SemesterId { get; set; }
        public int BrachId { get; set; }
        public int EndTermId { get; set; }

        public int DepartmentID { get; set; }

    }

    public class SearchStudentAllotment
    {
        public int InstituteID { get; set; }
        public int HostelID { get; set; }
        public int ApplicationId { get; set; }
        public int SemesterId { get; set; }
        public int BrachId { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int AffidavitDoc { get; set; }
        public int SupportingDocument { get; set; }

    }




}
