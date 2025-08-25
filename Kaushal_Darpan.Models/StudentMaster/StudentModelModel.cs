using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CommonSubjectMaster;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.SubjectMaster;

namespace Kaushal_Darpan.Models.StudentMaster
{
    public class StudentMasterModel
    {
        //public int AID { get; set; }
        public int ApplicationID { get; set; }
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string? StudentTypeName { get; set; }
        public int? AcademicYearID { get; set; }
        public int EndTermID { get; set; }
        public string? EntryDate { get; set; }
        public string? SSOID { get; set; }
        public int AdmissionCategoryID { get; set; }
        public int InstituteID { get; set; }
        public int status { get; set; }
        public int StreamID { get; set; }
        public Int64 ABCID { get; set; }
        public int InstituteStreamID { get; set; }
        public string StudentName { get; set; }
        public string StudentNameHindi { get; set; }
        public string FatherName { get; set; }
        public string StudentExamStatus { get; set; } = string.Empty;
        public string FatherNameHindi { get; set; }
        public string MotherName { get; set; }
        public string MotherNameHindi { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public int CategoryA_ID { get; set; }
        public int CategoryB_ID { get; set; }
        public string MobileNo { get; set; }
        public string? TelephoneNo { get; set; }
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public int? StateID { get; set; }
        public int? DistrictID { get; set; }
        public int? BlockID { get; set; }
        public int? PanchayatSamityID { get; set; }
        public int? GramPanchayatID { get; set; }
        public string? Pincode { get; set; }
        public string AadharNo { get; set; }
        public string? FatherAadharNo { get; set; }
        public string? JanAadharNo { get; set; }
        public string? JanAadharMobileNo { get; set; }
        public string? JanAadharName { get; set; }
        public string? BankAccountNo { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankName { get; set; }
        public string Remark { get; set; }
        public int TypeOfAdmissionID { get; set; }
        public int StudentStatusID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int SemesterID { get; set; }
        public String? SemesterName { get; set; }
        public int StudentTypeID { get; set; }
        public string BhamashahNo { get; set; }
        public string? JanAadharMemberId { get; set; }
        public string? Papers { get; set; }
        public string? Dis_DOB { get; set; }
        public int Status_old { get; set; }
        public int StudentFilterStatusId { get; set; }
        public List<StudentMaster_QualificationDetails>? QualificationDetails { get; set; }
        public List<DocumentDetailsModel>? DocumentDetails { get; set; }
        public int? SubCategoryA_ID { get; set; }
        public string? StudentPaper { get; set; }
        public string? InstituteName { get; set; }

        //document
        public string? StudentPhoto { get; set; }
        public string? StudentSign { get; set; }
        public string? Dis_StudentPhoto { get; set; }
        public string? Dis_StudentSign { get; set; }
        //end
        public int? StudentExamID { get; set; }
        public string? GenderName { get; set; }
        public List<Kaushal_Darpan.Models.SubjectMaster.SubjectMaster>? BackSubjectList { get; set; }

        public bool IsYearly { get; set; }
        public int? His_StatusId { get; set; } = 0;
        public bool? IsVerified { get; set; } = false;
    }
    public class StudentMaster_QualificationDetails
    {
        public int StudentQualificationID { get; set; }
        public int StudentID { get; set; }
        public string? Qualification { get; set; }
        public string? ClassBoard { get; set; }
        public string? ClassSubject { get; set; }
        public int? PasssingYear { get; set; }
        public string? ClassDocument { get; set; }
        public float ClassAgMaxMarks { get; set; }
        public float ClassPercentage { get; set; }
        public float ClassAgObtMarks { get; set; }
        public string? OtherDoc { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
    }


}
