using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.StaffMaster
{
    public class ITIGovtEMStaffMasterSearchModel
    {
       

        public int StaffID { get; set; }
        public int StaffTypeID { get; set; }
        public int RoleID { get; set; }

        public int CourseID { get; set; }
        public int SubjectID { get; set; }
        
        public int InstituteID { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public string SSOID { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public int DepartmentID { get; set; }
        public int StaffLevelID { get; set; }
        public int Status { get; set; }
        public int? CourseTypeId { get; set; }
        public int CreatedBy { get; set; }
        public int? FilterStaffTypeID { get; set; }
        public string? FilterName { get; set; } 
        public string? FilterSSOID { get; set; } 
    }

    public class ITIGovtStudentSearchModel : RequestBaseModel
    {
        public int StudentID { get; set; }
        public int RoleId { get; set; }
        public string SsoID { get; set; }
        public string Action { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public string PrnNo { get; set; }
        public string ApplicationNo { get; set; }
        public string EnrollmentNo { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string CreateDate { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int DocumentMasterID { get; set; }
        public int ChallanNo { get; set; }
        public int FinancialYearID { get; set; }
        public int InstituteID { get; set; }
        public int TrasactionStatus { get; set; }
        public int? StudentExamID { get; set; }

    }


    public class ITI_Govt_EM_SanctionedPostBasedInstituteModel
    {

        public int InstituteID { get; set; }
        public int SanctionedBudgetPostID { get; set; }
        public int P_GID { get; set; }
        public int PersonnelPostID { get; set; }
        public string SSOID { get; set; }
        public string EmpName { get; set; }
        public string SanctionedBudgetBusiness { get; set; }
        public string PersonnelBusiness { get; set; }
        public string PostingType { get; set; }
        public string DateOfJoiningRetiredPersonnelHonorarium { get; set; }
        public string DateDepartureAssignedWork { get; set; }
        public string BudgetChief { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int CreatedBy { get; set; }

    }

}
