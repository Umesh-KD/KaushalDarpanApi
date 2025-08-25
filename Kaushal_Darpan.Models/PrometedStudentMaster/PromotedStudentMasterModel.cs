
namespace Kaushal_Darpan.Models.PrometedStudentMaster
{
    public class PrometedStudentMasterModel
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
        public int EndTermID { get; set; }  // equivalent to `sm.EndTermID`
        public int StreamID { get; set; }  // equivalent to `sm.StreamID`
        public int SemesterID { get; set; }  // equivalent to `sm.SemesterID`
        public string Dis_DOB { get; set; }  // equivalent to `sm.Dis_DOB`
        public bool IsBridge { get; set; } 
        public string? StudentType { get; set; }
    }

    public class PromotedStudentMarkedModel
    {
        public bool Marked { get; set; }   
        public int StudentId { get; set; } 
        public int RoleId { get; set; }    
        public int ModifyBy { get; set; }  
        public string? IPAddress { get; set; }  
        public int EndTermID { get; set; }     
    }

    public class PromotedStudentSearchModel : RequestBaseModel
    {
        public string InstituteID { get; set; }
        public string SemesterID { get; set; }
        public string StreamID { get; set; }
        public string IsBridge { get; set; }
    }

}
