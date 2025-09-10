
namespace Kaushal_Darpan.Models.PrometedStudentMaster
{
    public class PrometedStudentMasterModel : ResponseBaseModel
    {
        public bool Selected { get; set; }  // equivalent to `convert(bit, 0) as 'Selected'`
        public int StudentID { get; set; }  // equivalent to `sm.StudentID`
        public string ApplicationNo { get; set; }  // equivalent to `sm.ApplicationNo`
        public string StudentName { get; set; }  // equivalent to `sm.StudentName`
        public string FatherName { get; set; }  // equivalent to `sm.FatherName`
        public string EnrollmentNo { get; set; }  // equivalent to `sm.EnrollmentNo`
        public string MobileNo { get; set; }  // equivalent to `sm.MobileNo`
        public string InstituteName { get; set; }  // equivalent to `sm.InstituteCode+' '+sm.InstituteName as 'InstituteName'`
        public string BranchName { get; set; }  // equivalent to `sm.StreamName as 'BranchName'`
        public string SemesterName { get; set; }  // equivalent to `sm.SemesterName`
        public string DistrictName { get; set; }  // equivalent to `sm.DistrictName`
        public int StreamID { get; set; }  // equivalent to `sm.StreamID`
        public int SemesterID { get; set; }  // equivalent to `sm.SemesterID`
        public string Dis_DOB { get; set; }  // equivalent to `sm.Dis_DOB`
        public bool IsBridge { get; set; }
        public string? StudentType { get; set; }
        public decimal EarnedCreditsSem1 { get; set; }
        public decimal EarnedCreditsSem2 { get; set; }
        public decimal TotalEarnedCredits { get; set; }
        public bool Detain { get; set; }
        public bool UFM { get; set; }
        public int UFMCategory { get; set; }
        public int InstituteId { get; set; }
    }

    public class PromotedStudentMarkedModel : RequestBaseModel
    {
        public bool Marked { get; set; }
        public int StudentId { get; set; }
        public string EnrollmentNo { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string? DOB { get; set; }
        public string Gender { get; set; }
        public string? InstituteNameEnglish { get; set; }
        public string? StreamName { get; set; }
        public string StudentType { get; set; }
        public int SemesterId { get; set; }
        public bool IsDetain { get; set; }
        public bool IsUFM { get; set; }
        public int UFMCategory { get; set; }
        public bool IsBridge { get; set; }
        public int StreamId { get; set; }
        public int ModifyBy { get; set; }
        public string IPAddress { get; set; }
    }

    public class PromotedStudentSearchModel : RequestBaseModel
    {
        public string InstituteID { get; set; }
        public string SemesterID { get; set; }
        public string StreamID { get; set; }
        public string IsBridge { get; set; }
    }

}
