using System.Data;

namespace Kaushal_Darpan.Models.SSOUserDetails
{
    public class SSOUserDetailsModel
    {
        public int ProfileID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int ApplicationID { get; set; }
        public int ApplicationFinalSubmit { get; set; }
        public string SSOID { get; set; }
        public string AadhaarId { get; set; }
        public string BhamashahId { get; set; }
        public string BhamashahMemberId { get; set; }
        public string DisplayName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Mobileno { get; set; }
        public string TelephoneNumber { get; set; }
        public string IpPhone { get; set; }
        public string MailPersonal { get; set; }
        public string PostalAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Photo { get; set; }
        public string Designation { get; set; }
        public string DepartmentName { get; set; }
        public string MailOfficial { get; set; }
        public string EmployeeNumber { get; set; }
        public int DepartmentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[]? SldSSOIDs { get; set; }
        public string JanaadhaarId { get; set; }
        public string ManaadhaarMemberId { get; set; }
        public string UserType { get; set; }
        public string Mfa { get; set; }
        public int InstituteID { get; set; }
        public int StudentID { get; set; }
        public string? InstituteName { get; set; }
        public int Eng_NonEng { get; set; }//1 for eng 2 for non eng
        public string? Eng_NonEngName { get; set; }
        public int FinancialYearID { get; set; }
        public int EndTermID { get; set; }
        public int FinancialYearID_Session { get; set; }// working session
        public int EndTermID_Session { get; set; }// working session
        public int HostelID { get; set; }// working session
        public string HostelIDs { get; set; } = string.Empty;

        public string? UserRequestStatus { get; set; }
        public string? LoginStatus { get; set; }
        public bool? IsCitizenQueryUser { get; set; }
        public int? QueryType { get; set; }
        public int StaffID { get; set; }
        public int ExamScheme { get; set; }
        public int ServiceID { get; set; }
        public bool IsKiosk { get; set; } = false;
        public string KIOSKCODE { get; set; } = string.Empty;
        public string SSoToken { get; set; } = string.Empty;
        public int EmTypeId { get; set; }
        public int LevelId { get; set; }
        public int ShowSessionSelection { get; set; }
        public int SelectedValue { get; set; }
        public int OfficeID { get; set; }
        public DataTable SSOMenu { get; set; }
        public int DistrictID { get; set; }

    }


    public class UpdateStudentDetailsModel
    {
        public int UserID { get; set; }
        public int ProfileID { get; set; }
        public string? SSOID { get; set; }
        public string? UserType { get; set; }
        public int RoleID { get; set; }

    }
    public class ITICollegeSSoMAP
    {
        public int CollegeID { get; set; }
        public string SSOID { get; set; } = string.Empty;
        public string? CollegeName { get; set; }
        public string? Code { get; set; }
        public int CollegeExists { get; set; }

    }
    public class BTERCollegeSSoMAP
    {
        public int CollegeID { get; set; }
        public string SSOID { get; set; } = string.Empty;
        public string? CollegeName { get; set; }
        public string? Code { get; set; }
        public int CollegeExists { get; set; }
    }

    public class SsoLoginPassModel
    {
        public string SSOID { get; set; }
        public string Password { get; set; }
    }


}
