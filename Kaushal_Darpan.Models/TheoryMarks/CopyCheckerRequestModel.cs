namespace Kaushal_Darpan.Models.TheoryMarks

{
    public class CopyCheckerRequestModel : RequestBaseModel
    {
        public string SSOID { get; set; }
        public string? ExaminerCode { get; set; }
        public int? GroupCodeID { get; set; }
    }

    public class ExaminerDashboardModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int InstituteID { get; set; }
        public int InvigilatorAppointmentID { get; set; }
        public string SSOID { get; set; }
    }
}
