namespace Kaushal_Darpan.Models.ITIPrivateEstablish
{
    public class ITIPrivateEstablishModel
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
       
        public List<ITIPrivateEstablish_Staff_EduQualificationDetailsModel> EduQualificationDetailsModel { get; set; }
        public List<ITIPrivateEstablish_StaffSubjectListModel>? StaffSubjectListModel { get; set; }

        public List<ITIPrivateEstablish_StaffHostelListModel>? StaffHostelListModel { get; set; }

        public bool IsExaminer { get; set; }

    }

    public class ITIPrivateEstablish_Staff_EduQualificationDetailsModel
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

    public class ITIPrivateEstablish_AddStaffBasicDetailDataModel : RequestBaseModel
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
        public string? BhamashahId { get; set; }
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

        public string multiHostelIDs { get; set; }
        public int EMTypeID { get; set; }
    }
    public class ITIPrivateEstablish_SSOUserResponse
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
    
    public class ITIPrivateEstablish_SSOUser
    {
        public string SSOID { get; set; }
        public string DISPLAYNAME { get; set; }
        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public string AADHAAR { get; set; }

    }


    public class ITIPrivateEstablish_StaffSubjectListModel
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

    public class ITIPrivateEstablish_StaffHostelListModel
    {
      
        public int ID { get; set; }
        public string? Name { get; set; }
       
    
    }

}
