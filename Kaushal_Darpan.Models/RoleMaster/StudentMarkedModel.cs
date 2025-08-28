
namespace Kaushal_Darpan.Models.PreExamStudent
{
    public class StudentMarkedModel:RequestBaseModel
    {
        public bool Marked { get; set; }
        public int StudentId { get; set; }
        public int Status { get; set; }
        public int StudentFilterStatusId { get; set; }
        public int ModifyBy { get; set; }
        public int RoleId { get; set; }
        public string? IPAddress { get; set; }
        public int? StudentExamID { get; set; }
        //public int? EndTermID { get; set; }
    }
    public class StudentMarkedModelForJoined
    {
        public bool Marked { get; set; }
        public int ApplicationID { get; set; }
        public int Status { get; set; }
        public int StudentFilterStatusId { get; set; }
        public int ModifyBy { get; set; }
        public int RoleId { get; set; }
        public string? IPAddress { get; set; }
        public int UnitNo { get; set; }
        public int ShiftID { get; set; }
        public int AllotmentId { get; set; }
        public int TradeId { get; set ; }
        public int CollegeId { get; set ; }
        public int TradeSchemeID { get; set ; }
        public int CollegeTradeId { get; set ; }
    }


    public class RejectMarkModel
    {
        public bool Marked { get; set; }
        public int StudentId { get; set; }
        public int Status { get; set; }
        public int StudentFilterStatusId { get; set; }
        public int ModifyBy { get; set; }
        public int RoleId { get; set; }
        public string? IPAddress { get; set; }
        public int RejectFlag { get; set; }
    }


    public class EligibleStudentButPendingForVerification 
    {
       public int? RoleID { get; set; }
        public int StudentId { get; set; }
       public int? ModifyBy { get; set; }
        public int? EndTermID { get; set; }
        public int? DepartmentID { get; set; }
        public int? Eng_NonEng { get; set; }
        public string? Remark { get; set; }
    }

    public class ForSMSEnrollmentStudentMarkedModel : RequestBaseModel
    {
      
        public int StudentId { get; set; }
        public int Status { get; set; }
        public int RoleId { get; set; }
        public string? ApplicationNo  { get; set; }
        public string? MobileNo  { get; set; }
        public string? MessageType { get; set; }
      
        //public int? EndTermID { get; set; }
    }
}
