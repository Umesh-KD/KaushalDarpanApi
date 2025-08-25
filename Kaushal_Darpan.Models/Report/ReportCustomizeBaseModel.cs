namespace Kaushal_Darpan.Models.Report
{
    public class ReportCustomizeBaseModel 
    {
        public int InstituteID { get; set; }
        public string GenderID { get; set; }
        public int StateID { get; set; }
        public int StreamID { get; set; }
        public int StudentTypeID { get; set; }
        public int SemesterID { get; set; }
        public int DistrictID { get; set; }
        public int BlockID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public string CategaryCast { get; set; }
        public int DepartmentID { get; set; }
        public int AcademicYearID { get; set; }
        public int CasteCategoryID { get; set; }
        public int Type { get; set; }

        public int ReportFlagID { get; set; }
        public string? action { get; set; } 

    }

    public class ExamResultViewModel
    {
        public int? ObtainedTheory { get; set; }        // Nullable int
        public string? CenterCode { get; set; }
        public string? SubjectCode { get; set; }
        public string? RollNo { get; set; }
        public int? MaxTheory { get; set; }
        public int? InstituteID { get; set; }
        public bool? IsChecked { get; set; }            // Nullable bool for true/false/null
        public string? StreamName { get; set; }
        public string? StreamCode { get; set; }
        public string? SubjectName { get; set; }
        public string? Name { get; set; }
        public string? ExaminerCode { get; set; }
        public string? MobileNumber { get; set; }
    }
}
