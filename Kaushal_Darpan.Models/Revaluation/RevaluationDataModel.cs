namespace Kaushal_Darpan.Models.RevaluationDataModel
{
    public class RevaluationDataModel
    {

        public string DOB {  get; set; }
        public int? RollNo { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeIDs { get; set; }
        
    }
    
    public class StudentDetailsByRollNoModel
    {

        public string DOB {  get; set; }
        public int? RollNo { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int ServiceID { get; set; }
        public int SemesterID { get; set; }
        public int EndTermID { get; set; }
        public int StudentExamID { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string InstituteNameEnglish { get; set; }
        public string Name { get; set; }
        public string? MobileNo { get; set; }
        public string Year { get; set; }
        public string? EnrollmentNo { get; set; }
        public string StudentType { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public bool IsSelected { get; set; }
        
    }
}
