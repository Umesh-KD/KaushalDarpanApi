namespace Kaushal_Darpan.Models.StaffMaster
{
    public class ITIGovtEMStaffMasterModel
    {

        public int StaffID { get; set; }
        public int StaffTypeID { get; set; }
        public int RoleID { get; set; }
        public int DesignationID { get; set; }
        public int CourseID { get; set; }
        public int SubjectID { get; set; }
        public string SSOID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string DateOfBirth { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public string? District { get; set; }
        public string? InstituteName { get; set; }
        public string? StateName { get; set; }
        public string? InstituteTehsil { get; set; }
        public string? InstituteDivision { get; set; }
        public string? InstituteDistrict { get; set; }
        public string? StreamName { get; set; }
        public string Pincode { get; set; }
        public string Address { get; set; }
        public string HigherQualificationID { get; set; }
        public string ProfilePhoto { get; set; }
        public string Dis_ProfileName { get; set; }
        public string AdharCardNumber { get; set; }
        public string PanCardNumber { get; set; }
        public string AdharCardPhoto { get; set; }
        public string PanCardPhoto { get; set; }
        public string Dis_AdharCardNumber { get; set; }
        public string Dis_PanCardNumber { get; set; }
        public string DateOfAppointment { get; set; }
        public string DateOfJoining { get; set; }
        public string Experience { get; set; }
        public string Certificate { get; set; }
        public string Dis_Certificate { get; set; }
        public string AnnualSalary { get; set; }
        public string StaffStatus { get; set; }
        public string PFDeduction { get; set; }
        public string ResearchGuide { get; set; }
        public int SpecializationSubjectID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int InstituteID { get; set; }
        public int DepartmentID { get; set; }
        public int StatusOfStaff { get; set; }
        public int ProfileStatus { get; set; }
        public int UGQualificationID { get; set; }
        public int PGQualificationID { get; set; }
        public int PHDQualification { get; set; }

        //adding bank details
        public string? BankAccountNo { get; set; }
        public string? BankAccountName { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankName { get; set; }

        public string StaffIDs { get; set; }
        public string? UGQualificationName { get; set; }
        public string? PGQualificationName { get; set; }
        public string? PHDQualificationName { get; set; }
        public string? StreamCode { get; set; }
        public string? Designation { get; set; }
        public int StaffLevelID { get; set; }
        public int TechnicianID { get; set; }
        public string HostelID { get; set; }
        public int StaffLevelChildID { get; set; }
        public int InstituteDistrictID { get; set; }
        public int InstituteDivisionID { get; set; }
        public int InstituteTehsilID { get; set; }
        public List<ITIGovtEMStaff_EduQualificationDetailsModel> EduQualificationDetailsModel { get; set; }
        public List<ITIGovtEMStaffSubjectListModel>? StaffSubjectListModel { get; set; }

        public List<ITIGovtEMStaffHostelListModel>? StaffHostelListModel { get; set; }

        public bool IsExaminer { get; set; }

    }

    public class ITIGovtEMStaff_EduQualificationDetailsModel
    {

        public int QualificationID { get; set; }
        public int StreamID { get; set; }
        public string University { get; set; }
        public string PassingYear { get; set; }
        public int PassingYearID { get; set; }
        public string? StreamName { get; set; }
        public string PercentageGrade { get; set; }
        public string MarksheetPhoto { get; set; }
        public string Dis_Marksheet { get; set; }

    }

    public class ITIGovtEMAddStaffBasicDetailDataModel : RequestBaseModel
    {
        public int ProfileID { get; set; }
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public int StaffTypeID { get; set; }
        public string SSOID { get; set; }

        public int RoleID { get; set; }
        public int DesignationID { get; set; }

        public string DisplayName { get; set; }

        public string? AadhaarId { get; set; }
        public string ?BhamashahId { get; set; }
        public string? BhamashahMemberId { get; set; }
 
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string TelephoneNumber { get; set; }
        public string IpPhone { get; set; }
        public string MailPersonal { get; set; }
        public string PostalAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Photo { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string MailOfficial { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] SldSSOIDs { get; set; }
        public string JanaadhaarId { get; set; }
        public string ManaadhaarMemberId { get; set; }
        public string UserType { get; set; }
        public string Mfa { get; set; }
        public int InstituteID { get; set; }
        public int StatusOfStaff { get; set; }

        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

        public string HostelID { get; set; }
        public int StaffLevelID { get; set; }
        public int BranchID { get; set; }
        public int TechnicianID { get; set; }
        public int StaffLevelChildID { get; set; }
        public int EMTypeID { get; set; }
        public int OfficeID { get; set; }

        public string multiHostelIDs { get; set; }
    }
    public class ITIGovtEMSSOUserResponse
    {
        public string SSOID { get; set; }
        public string aadhaarId { get; set; }
        public string bhamashahId { get; set; }
        public string bhamashahMemberId { get; set; }
        public string displayName { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string telephoneNumber { get; set; }
        public string mailPersonal { get; set; }
        public string postalAddress { get; set; }
        public string postalCode { get; set; }
    }
    
    public class ITIGovtEMSSOUser
    {
        public string SSOID { get; set; }
        public string DISPLAYNAME { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public string AADHAAR { get; set; }

    }


    public class ITIGovtEMStaffSubjectListModel
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

    public class ITIGovtEMStaffHostelListModel
    {
      
        public int ID { get; set; }
        public string? Name { get; set; }
       
    
    }

    public class UpdateSSOIDByPricipleModel
    {
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public string SSOID { get; set; }
    }
    public class ITIGovtEM_OfficeSearchModel
    {


        public int OfficeID { get; set; }
        public string OfficeName { get; set; }
        public int LevelID { get; set; }
        public string LevelName { get; set; }
        public int DepartmentID { get; set; }



    }
    public class ITIGovtEM_OfficeSaveDataModel
    {
        public int OfficeID { get; set; }

        public string OfficeName { get; set; }

        public int DepartmentID { get; set; }

        public int LevelID { get; set; }

        public int EndTermId { get; set; }

        public int CourseTypeID { get; set; }

        public int CreatedBy { get; set; }

        public int ModifyBy { get; set; }

       
    }

    public class ITI_Govt_EM_OFFICERSDataModel
    {

        public int ID { get; set; }
        public int DivisionID { get; set; }
        public int DistrictID { get; set; }
        public int DepartmentID { get; set; }
        public string NameOfDistrict { get; set; }
        public string NameOfInstitution_office { get; set; }
        public string NameOfOfficer_DDO { get; set; }
        public string AdditionalCharage_DDO { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string IPAddress { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }


    }

    public class ITI_Govt_EM_OFFICERSSearchDataModel
    {

        public int ID { get; set; }
        public int DivisionID { get; set; }

    }

    public class ITIGovtEM_PostSearchModel
    {
        public int ID { get; set; }
        public int OfficeID { get; set; }
        public string OfficeName { get; set; }
        public string PostName { get; set; }
        public int DepartmentID { get; set; }
    }
    public class ITIGovtEM_PostSaveDataModel
    {
        public int ID { get; set; }
        public int OfficeID { get; set; }
        public string PostName { get; set; }
        public string OfficeName { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermId { get; set; }
        public int CourseTypeID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }


    }
    public class ITI_Govt_EM_ZonalOFFICERSSearchDataModel
    {
        public int ID { get; set; }
        public int OfficeID { get; set; }
        public int LevelID { get; set; }
        public int StaffTypeID { get; set; }
        public int PostID { get; set; }
        public int DepartmentID { get; set; }
        public int CreatedBy { get; set; }
        public string? SSOID { get; set; }
        public string? Name { get; set; }

    }

    public class ITI_Govt_EM_ZonalOFFICERSDataModel
    {
        public int ID { get; set; }
        public int OfficeID { get; set; }
        public int PostID { get; set; }
        public int CreatedBy { get; set; }
        public bool IsHod { get; set; }
        public string SSOID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int LevelID { get; set; }
        public int RoleID { get; set; }
        public int StaffTypeID { get; set; }
        public int InstituteID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public int DistrictID { get; set; }

    }

    public class ITI_Govt_EM_Already_ZonalOFFICERSDataModel
    {
        public int ID { get; set; }
        public int OfficeID { get; set; }
        public int PostID { get; set; }
        public int CreatedBy { get; set; }
        public bool IsHod { get; set; }
        public string SSOID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }

    }


    public class ITIGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel
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

    public class StaffPostingData
    {
        public int ID { get; set; }
        public int StaffUserID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int OriginalPositionID { get; set; }
        public int CoreBusinessID { get; set; }
        public int DistrictID { get; set; }
        public int BlockID { get; set; }
        public int GramPanchayatID { get; set; }
        public string NameOfRevenueVillage { get; set; }
        public string NameAndLocationOfTheInstitution_Office { get; set; }
        public int City_VillageID { get; set; }
        public int PostingDirectRecruitment_PromotionID { get; set; }
        public string GradationOrderNumberAndDate { get; set; }       
        public int CadreID { get; set; }
        public string ConfirmationDate { get; set; }
        public string DuffelCarriageOrderNoDate { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermId { get; set; }
        public int CourseTypeID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string IPAddress { get; set; }

        public string CasteName { get; set; }
        public string OriginalPositionName { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string GramPanchayatName { get; set; }
        public string City_Village { get; set; }
        public string PostingDirectRecruitment_Promotion { get; set; }
        public string CoreBusinessName { get; set; }
        
    }
    public class ITIGovtEMStaff_PersonalDetailsModel
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
        public int NodalDistrictID { get; set; }
        public bool IsHod { get; set; }

        
        public List<ITIGovtEMStaff_PromotionIsRenouncedModel> PromotionList  = new List<ITIGovtEMStaff_PromotionIsRenouncedModel>();
    }


    public class ITIGovtEMStaff_PromotionIsRenouncedModel
    {
        public string PromotionDate { get; set; }
        public int PostID { get; set; }
        public string Business { get; set; }
        public string PostName { get; set; }
        public int ID { get; set; }

    }


    public class RequestUpdateStatus
    {
        public int ServiceRequestId { get; set; }
        public int ID { get; set; }
        public int StatusIDs { get; set; }
        public string Remark { get; set; }
        public string SSOID { get; set; }
        public int DepartmentID { get; set; }
        public int CreatedBy { get; set; }
        public int ProfileStatusID { get; set; }
        public int RequestType { get; set; }
        public string LastWorkingDate { get; set; }
        public string JoiningDate { get; set; }
        public int UserID { get; set; }

        public bool IsEOL { get; set; }
        public string EOLFromDate { get; set; }
        public string EOLToDate { get; set; }
        public bool IsEnquiries { get; set; }
        public string Comments { get; set; }
        public bool IsAccount { get; set; }
        public string AccountComments { get; set; }
        public int RoleID { get; set; }

    }

    public class RequestUserRequestHistory
    {
        public int ServiceRequestId { get; set; }  
        public int DepartmentID { get; set; }  
   
        
        
    }


    public class ITI_Govt_EM_RoleOfficeMappingSearchDataModel
    {    
        public int DepartmentID { get; set; }
       
    }

    public class PersonalDetailByUserIDSearchModel
    {
        public int StaffID { get; set; }
        public int StaffUserID { get; set; }
        public string Action { get; set; }
        public string SSOID { get; set; }
    }

    public class PersonalDetailByUserIDModel 
    {

        public ITIGovtEMStaff_PersonalDetailsModel iTIGovtEMStaffPersonalDetails = new ITIGovtEMStaff_PersonalDetailsModel();

        public List<StaffPostingData> PostingList = new List<StaffPostingData>();
        public List<ITIGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel> EducationalList = 
        new List<ITIGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel>();

        public List<ITIGovtEMStaff_PromotionIsRenouncedModel> PromotionList = new List<ITIGovtEMStaff_PromotionIsRenouncedModel>();

    }


    public class JoiningLetterSearchModel
    {
        public int UserID { get; set; }
        public int StaffID { get; set; }
        public int StaffUserID { get; set; }
        public string Action { get; set; }
        public string SSOID { get; set; }
    }

    public class RelievingLetterSearchModel
    {
        public int UserID { get; set; }
        public int StaffID { get; set; }
        public int StaffUserID { get; set; }
        public string Action { get; set; }
        public string SSOID { get; set; }
    }

    public class CheckDistrictNodalOfficeSearchModel
    {
        public int DistrictID { get; set; }
        public int DepartmentID { get; set; }
        public int LevelID { get; set; }
       
    }

    public class ITI_Govt_EM_UserRequestHistoryListSearchDataModel
    {
        public int DepartmentID { get; set; } = 0;
        public int StaffUserID { get; set; } = 0;
        public int StaffID { get; set; } = 0;
    }

    public class ITIGovtEM_OfficeDeleteModel
    {
        public int ID { get; set; }
        public int ModifyBy { get; set; }
  
    }

    public class ITIGovtEM_DeleteByIdStaffPromotionHistoryDelete
    {
        public int UserID { get; set; }
        public int StaffID { get; set; }

    }
    public class ITIGovtEM_DeleteByIdStaffEducationDelete
    {
        public int UserID { get; set; }
        public int StaffID { get; set; }

    }
    public class ITIGovtEM_DeleteByIdStaffServiceDelete
    {
        public int UserID { get; set; }
        public int StaffID { get; set; }

    }


    //----------Bter em---------

    public class BterRequestUpdateStatus
    {
        public int ServiceRequestId { get; set; }
        public int ID { get; set; }
        public int StatusIDs { get; set; }
        public string Remark { get; set; }
        public string SSOID { get; set; }
        public int DepartmentID { get; set; }
        public int CreatedBy { get; set; }
        public int ProfileStatusID { get; set; }
        public int RequestType { get; set; }
        public string LastWorkingDate { get; set; }
        public string JoiningDate { get; set; }
        public int UserID { get; set; }
      
        public bool IsEOL { get; set; }
        public string EOLFromDate { get; set; }
        public string EOLToDate { get; set; }
        public bool IsEnquiries { get; set; }
        public string Comments { get; set; }
        public bool IsAccount { get; set; }
        public string AccountComments { get; set; }
        public int RoleID { get; set; }


    }

    public class BterRequestUserRequestHistory
    {
        public int ServiceRequestId { get; set; }
        public int DepartmentID { get; set; }



    }

    public class Bter_Govt_EM_ZonalOFFICERSSearchDataModel
    {
        public int ID { get; set; }
        public int OfficeID { get; set; }
        public int LevelID { get; set; }
        public int StaffTypeID { get; set; }
        public int PostID { get; set; }
        public int DepartmentID { get; set; }
        public int CreatedBy { get; set; }
        public string? SSOID { get; set; }
        public string? Name { get; set; }

    }
    public class BterStaffUserRequestReportSearchModel
    {
        public string Action { get; set; }
        public int UserID { get; set; }
        public int StaffUserID { get; set; }
        public int StaffID { get; set; }
        public int RequestTypeID { get; set; }
        public int StatusID { get; set; }
        public int OfficeID { get; set; }
        public int LevelID { get; set; }
        public int DepartmentID { get; set; }
        public string OrderNo { get; set; } 
        public string OrderDate { get; set; }  
        public string RelievingDate { get; set; } 
        public string JoiningDate { get; set; } 
        public string RequestDate { get; set; } 
        public int PostID { get; set; }
        public int StaffTypeID { get; set; }
    }



}
