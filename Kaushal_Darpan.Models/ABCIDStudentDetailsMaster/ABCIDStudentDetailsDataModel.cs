namespace Kaushal_Darpan.Models.AssignRoleRight
{
    public class ABCIDStudentDetailsDataModel
    {
        public int StudentID { get; set; }  
        public string EnrollmentNo { get; set; }  
        public string StudentName { get; set; }  
        public string MotherName { get; set; }  
        public string FatherName { get; set; }  
        public string DOB { get; set; }  
        public int ABCID { get; set; }  
    }

    public class ABCIDStudentDetailsSearchModel
    {
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int FinancialYearID { get; set; }
        public int BranchId { get; set; }
        public int EndTermID { get; set; }
        public int SemesterID { get; set; }
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public string? EnrollmentNo { get; set; }
    }
}

