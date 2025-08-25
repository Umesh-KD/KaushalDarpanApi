namespace Kaushal_Darpan.Models.MarksheetDownloadModel
{
    public class MarksheetDownloadSearchModel
    {

        public int? SemesterID { get; set; }
        public int? InstituteID { get; set; }
        public string? IsRevised { get; set; }
        public int? IsBridge { get; set; }
        public int? ResultTypeID { get; set; }
        public int? RollNo { get; set; }
        public int? DepartmentID { get; set; }
        public int? StudentID { get; set; }
        public int? Eng_NonEngID { get; set; }
        public int? EndTermID { get; set; }
        public int? ExamTypeID { get; set; }
        public int? RWHResultID { get; set; }
        public int? AcademicYearID { get; set; }
        public string? IPAddress { get; set; }
        public string? MarksheetPath { get; set; }
        public string? Marksheet { get; set; }

        public bool? IsReval {  get; set; }
        public bool? IsRWHResult { get; set; }
        public bool? IsLateral { get; set; }

    }

    public class BackPaperReportDataModel : RequestBaseModel
    {
        public int? InstituteID { set; get; }
        public int? SemesterID { set; get; }
    }

    public class GenerateMarksheetModel: RequestBaseModel
    {
        public string? MarksheetPath { get; set; }
        public string? MarksheetFile { get; set; }
        public int? StudentID { get; set; }
        public int? SemesterID { get; set; }
    }
}
