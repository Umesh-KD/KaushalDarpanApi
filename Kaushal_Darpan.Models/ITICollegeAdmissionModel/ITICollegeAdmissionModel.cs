using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITICollegeAdmissionModel
{
    public class ITICollegeAdmissionModel
    {

        //public int OptionID { get; set; }
        public int ApplicationID { get; set; }
        public int InstituteID { get; set; }
        public int TradeLevel { get; set; }
        public int TradeId { get; set; }
        public int DepartmentID { get; set; }
        public int BranchID { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

    }



    public class ITICollegeAdmissionSearch
    {
        public string? ApplicationNo { get; set; }
        public string? DOB { get; set; }
        public string? MobileNumber { get; set; }
        public int DepartmentID { get; set; }
        public int FinancialYearID { get; set; }
    }


    public class ITICollegeAdmissionDetails
    {
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string StreamName { get; set; }
        public string Semester { get; set; }
        public string FeeAmount { get; set; }
        public string? LastDate { get; set; }
        public string FeeStatus { get; set; }
        public string? RollNo { get; set; }
        public string? EndTermName { get; set; }
        public int SemesterID { get; set; }
        public int ExamStudentStatus { get; set; }
        public int StudentSemesterID { get; set; }
        public string SSOID { get; set; }
        public int CurrentSemesterID { get; set; }
        public int ServiceID { get; set; }
        public int ID { get; set; }
        public decimal ExamFee { get; set; }
        public string InstituteName { get; set; }
        public string StrStudenetStatus { get; set; }
        public string DOB { get; set; }
        public string AdmitCard { get; set; }
        public bool IsSelected { get; set; }
        public string[] strSelectedIds { get; set; }
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
    }





}
