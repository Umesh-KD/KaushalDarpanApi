using Kaushal_Darpan.Models.CommonSubjectMaster;
using Kaushal_Darpan.Models.StudentMaster;

namespace Kaushal_Darpan.Models.StudentDetailUpdate
{
    public class StudentDetailUpdateModel
    {
        public string EnrollmentNo { get; set; }
        public string ApplicationNo { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string Document { get; set; }
        public string Dis_Document { get; set; }
        public int CreatedBy { get; set; }
        public int StudentID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng {  get; set; }

    }

    public class StudentEmploymentDetailsModel
    {
        public int AID { get; set; }
        public int StudentID { get; set; }
        public int InstituteID { get; set; }
        public string? EnrollmentNo { get; set; }

        public string CompanyType { get; set; }           // Self / Firm
        public string CompanyName { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public string CompanyAddress { get; set; }

        public string JobType { get; set; }               // FullTime / PartTime
        public string Experience { get; set; }            // Current / Past

        public DateTime? WorkingFromDate { get; set; }
        public DateTime? WorkingToDate { get; set; }

        public string SalaryType { get; set; }            // Stipend / CTC / Salary
        public decimal SalaryAmount { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }

        public DateTime? RTS { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public string IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public List<StudentEmploymentDetailsModel> ListEmployementDetails { get; set; }
    }

    //public class StudentEmploymentDetailsModelList
    //{
        
    //    public List<StudentEmploymentDetailsModel> ListEmployementDetails { get; set; }
    //    public string IPAddress { get; set; }
    //}

}
