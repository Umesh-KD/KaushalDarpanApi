using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BTER_EstablishManagement
{
    public class BTER_EstablishManagementDataModel
    {
    }

    public class BTER_EM_AddStaffInitialDetailsDataModel: RequestBaseModel
    {
        public int? ID { get; set; }
        public int? OfficeID { get; set; }
        public int? PostID { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsHod { get; set; }
        public bool? IsNodal { get; set; }
        public string? SSOID { get; set; }
        public int? CourseTypeID { get; set; }
        public int? LevelID { get; set; }
        public int? StaffTypeID { get; set; }
        public int? InstituteID { get; set; }
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailID { get; set; }
        public int? DistrictID { get; set; }
        public int? ModifyBy { get; set; }
    }

    public class BTER_EM_GetStaffListDataModel
    {
        public int? ID { get; set; }
        public int? OfficeID { get; set; }
        public int? LevelID { get; set; }
        public int? StaffTypeID { get; set; }
        public int? PostID { get; set; }
        public int? DepartmentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? RoleID { get; set; }
        public int? UserID { get; set; }
        public string? SSOID { get; set; }
        public string? Name { get; set; }
    }

    public class BTER_EM_GetPersonalDetailByUserID
    {
        public int? StaffUserID { get; set; }
        public string? SSOID { get; set; }
        public int? DepartmentID { get; set; }
    }

    public class BTER_EM_AddStaffPrincipleDataModel : RequestBaseModel
    {
        public int? ProfileID { get; set; }
        public int? StaffID { get; set; }
        public int? UserID { get; set; }
        public int? StaffTypeID { get; set; }
        public string? SSOID { get; set; }

        public int? RoleID { get; set; }
        public int? DesignationID { get; set; }

        public string? DisplayName { get; set; }

        public string? AadhaarId { get; set; }
        public string? BhamashahId { get; set; }
        public string? BhamashahMemberId { get; set; }

        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? IpPhone { get; set; }
        public string? MailPersonal { get; set; }
        public string? PostalAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Photo { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public string? MailOfficial { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string[]? SldSSOIDs { get; set; }
        public string? JanaadhaarId { get; set; }
        public string? ManaadhaarMemberId { get; set; }
        public string? UserType { get; set; }
        public string? Mfa { get; set; }
        public int? InstituteID { get; set; }
        public int? StatusOfStaff { get; set; }

        public int? ModifyBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

        public string? HostelID { get; set; }
        public int? StaffLevelID { get; set; }
        public int? BranchID { get; set; }
        public int? TechnicianID { get; set; }
        public int? StaffLevelChildID { get; set; }
        public int? EMTypeID { get; set; }

        public string? multiHostelIDs { get; set; }
    }

    public class BTER_EM_StaffMasterSearchModel
    {
        public int? StaffID { get; set; }
        public int? StaffTypeID { get; set; }
        public int? RoleID { get; set; }
        public int? CourseID { get; set; }
        public int? SubjectID { get; set; }
        public int? InstituteID { get; set; }
        public int? StateID { get; set; }
        public int? DistrictID { get; set; }
        public string? SSOID { get; set; }
        public string? Action { get; set; }
        public int? DepartmentID { get; set; }
        public int? StaffLevelID { get; set; }
        public int? Status { get; set; }
        public int? CourseTypeId { get; set; }
        public int? CreatedBy { get; set; }
        public int? FilterStaffTypeID { get; set; }
        public string? FilterName { get; set; }
        public string? FilterSSOID { get; set; }
    }

    public class BTER_EM_AddStaffDetailsDataModel
    {
        public int? InstituteID { get; set; }
        public int? BranchID { get; set; }
        public int? DesignationID { get; set; }
        public int? ServiceBookBranchID { get; set; }
        public int? Gender { get; set; }
        public string? Name { get; set; }
        public string? DateOfBirth { get; set; }
        public string? DateOfAppointment { get; set; }
        public string? DateOfJoining { get; set; }
        public string? DateOfRetirement { get; set; }
        public string? MobileNumber { get; set; }
        public string? SSOID { get; set; }
        public string? EmployeeID { get; set; }
        public string? CurrentDesignationID { get; set; }
        public string? Experience { get; set; }
        public string? QualificationAtJoining { get; set; }
        public string? QualificationAfterJoining { get; set; }
        public string? RetirementDate { get; set; }
        public string? Remark { get; set; }

        public int? StaffID { get; set; }
        public int? UserID { get; set; }
        public int? StaffUserID { get; set; }
        public int? DepartmentID { get; set; }
        public int? Eng_NonEng { get; set; }
        public List<BterStaffSubjectListModel>? bterStaffSubjectListModel { get; set; }
    }



    public class BterStaffSubjectListModel
    {
        public int StreamTypeID { get; set; }
        public int BranchID { get; set; }
        public int ExamTypeID { get; set; }
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public string? StreamType { get; set; }
        public string? BranchName { get; set; }
        public string? SemesterName { get; set; }
        public string? ExamType { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectType { get; set; }
        public bool IsOptional { get; set; }
    }
    public class BTER_EM_ApproveStaffDataModel : RequestBaseModel
    {
        public int? InstituteID { get; set; }
        public int? BranchID { get; set; }
        public int? SanctionedPosts { get; set; } // dropdown
        public bool? IsWorking { get; set; } // radio button
        public bool? IsExtraWorking { get; set; }
        public bool? IsVacant { get; set; }
        public int? OccupiedVacant { get; set; }
        public string? SSOID { get; set; } // textbox
        public string? Name { get; set; }
        public int? DesignationID { get; set; }

        public int? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmployeeID { get; set; }
        public string? DateOfBirth { get; set; }
        public int? Experience { get; set; }

        public bool? IsEmpWorkingOnPost { get; set; }

        public bool? IsEmpWorkingOnDeputationFromOther { get; set; }
        public int? EmpInstituteID { get; set; }

        public int? EmpBranchID { get; set; }

        public bool? IsEmpWorkingOnDeputationToOther { get; set; }
        public int? EmpDeputatedInstituteID { get; set; }

        public bool? IsSalaryDrawnFromSamePost { get; set; }
        public int? SalaryDrawnPostID { get; set; }
        public bool? IsSalaryDrawnFromOtherInstitute { get; set; }
        public int? SalaryDrawnInstituteID { get; set; }
        public string? DateOfRetirement { get; set; }
        public bool? AnyCourtCasePending { get; set; }
        public bool? AnyDisciplinaryActionPending { get; set; }
        public bool? ExtraOrdinaryLeave { get; set; }
        public string? SelectionCategory { get; set; }

        public bool? HigherEduPermission { get; set; }
        public string? HigherEduInstitute { get; set; }
        public string? Remark { get; set; }

        public int? StaffID { get; set; }
        public int? UserID { get; set; }
        public int? StaffUserID { get; set; }
        public int? ModifyBy { get; set; }
    }

    public class BTER_EM_UnlockProfileDataModel
    {
        public int? StaffUserID { get; set; }
        public int? StaffID { get; set; }
        public string? SSOID { get; set; }
        public int? ModifyBy { get; set; }
    }


    public class BTERGovtEM_DeleteByIdStaffServiceDelete
    {
        public int UserID { get; set; }
        public int StaffID { get; set; }

    }

    public class BTERGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel
    {
        public int ID { get; set; }
        public int LevelOfExamID { get; set; }
        public string LevelOfExamName { get; set; }
        public string NameOfTheExam { get; set; }
        public int ExamTypeID { get; set; }
        public string ExamTypeName { get; set; }
        public string NameOfTheBoard_University { get; set; }
        public string StateOfTheBoard_University { get; set; }
        public string DateOfPassing { get; set; }
        public string YearOfPassing { get; set; }
        public string Subject_Occupation_Branch { get; set; }
        public string NameOfTheInstituteFromWherePassed { get; set; }
        public int StaffUserID { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermId { get; set; }
        public int CourseTypeID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
    }


    public class BTERPersonalDetailByUserIDSearchModel
    {
        public int StaffID { get; set; }
        public int StaffUserID { get; set; }
        public string Action { get; set; }
        public string SSOID { get; set; }

        public BTERGovtEMStaff_PersonalDetailsModel bTERGovtEMStaffPersonalDetails = new BTERGovtEMStaff_PersonalDetailsModel();

        public List<StaffPostingData> PostingList = new List<StaffPostingData>();
        public List<BTERGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel> EducationalList =
        new List<BTERGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel>();

        public List<BTERGovtEMStaff_PromotionIsRenouncedModel> PromotionList = new List<BTERGovtEMStaff_PromotionIsRenouncedModel>();

    }

    public class BTERGovtEMStaff_PersonalDetailsModel
    {
        public int StaffID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmployeeID { get; set; }
        public int CurrentBasicDesignationID { get; set; }
        public string CoreBusiness { get; set; }
        public int CurrentPostingEmp { get; set; }
        public string? DateofPostingEmp { get; set; }
        public int GenderID { get; set; }
        public string PanCardNumber { get; set; }
        public int BloodGroupID { get; set; }
        public string FatherName { get; set; }
        public string DateOfBirth { get; set; }
        public int MaritalStatusID { get; set; }
        public string Husband_WifeName { get; set; }
        public int ServiceTypeHWID { get; set; }
        public string EmployeeIDOfHusband_Wife { get; set; }
        public int CastID { get; set; }
        public int ReligionID { get; set; }
        public int DivyangID { get; set; }
        public int BeforeChildren { get; set; }
        public int AfterChildren { get; set; }
        public int TotalChildren { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int StateHomeStateID { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AdharCardNumber { get; set; }
        public string BhamashahNo { get; set; }
        public string PassportNo { get; set; }
        public string CITSPassedYears { get; set; }
        public string DateOfJoiningGvernmentOfEmp { get; set; }
        public string FirstPostJoiningDateEmp { get; set; }
        public int JudicialCasePendingID { get; set; }
        public int SpecialAbilityID { get; set; }
        public int DepartmentalEnquiryPendingID { get; set; }
        public int PunishedInDepartmentalInquiryID { get; set; }
        public string DateOfPunishment { get; set; }
        public string DistrictYear { get; set; }
        public string DistrictCommak { get; set; }
        public string DivisionLevelYear { get; set; }
        public string DivisionLevelCommak { get; set; }
        public string StateYear { get; set; }
        public string StateCommak { get; set; }
        public string Remark { get; set; }
        public string ProfileStatusName { get; set; }
        public int ProfileStatusID { get; set; }
        public bool isSeniorInstructor { get; set; }
        public bool isRenounced { get; set; }
        public bool isDepartmentalMixed { get; set; }
        public int PostID { get; set; }
        public int OfficeID { get; set; }
        public int LevelID { get; set; }


        public List<BTERGovtEMStaff_PromotionIsRenouncedModel> PromotionList = new List<BTERGovtEMStaff_PromotionIsRenouncedModel>();
    }

    public class BTER_PersonalDetailByUserIDModel
    {

        public BTERGovtEMStaff_PersonalDetailsModel bTERGovtEMStaffPersonalDetails = new BTERGovtEMStaff_PersonalDetailsModel();

        public List<StaffPostingData> PostingList = new List<StaffPostingData>();
        public List<BTERGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel> EducationalList =
        new List<BTERGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel>();

        public List<BTERGovtEMStaff_PromotionIsRenouncedModel> PromotionList = new List<BTERGovtEMStaff_PromotionIsRenouncedModel>();

    }
    public class BTERGovtEMStaff_PromotionIsRenouncedModel
    {
        public string PromotionDate { get; set; }
        public int PostID { get; set; }
        public string Business { get; set; }
        public string PostName { get; set; }
        public int ID { get; set; }

    }

    public class Bter_Govt_EM_UserRequestHistoryListSearchDataModel
    {
        public int DepartmentID { get; set; } = 0;
        public int StaffUserID { get; set; } = 0;
        public int StaffID { get; set; } = 0;
    }

    public class StaffHostelSearchModel: RequestBaseModel
    {
        public int? StaffID { get; set; }
        public int? StaffUserID { get; set; }
        public string? StaffHostelIDs { get; set; }
    }
}
