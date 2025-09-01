namespace Kaushal_Darpan.Models
{
    public class PapersMasterModel
    {
        public int AID { get; set; }
        public int PaperID { get; set; }
        public int FinancialYearID { get; set; }
        public int EndTermID { get; set; }
        public int CommonSubjectID { get; set; }
        public string PaperSlug { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public bool IsBridgeCourse { get; set; }
        public int ParentID { get; set; }
        public int SemesterID { get; set; }
        public string SubstreamSubject_Cat { get; set; } = string.Empty;
        public string StreamSubjectCode { get; set; } = string.Empty;
        public string SubjectCat { get; set; } = string.Empty;
        public int SubjectID { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public float L { get; set; }
        public float T { get; set; }
        public float P { get; set; }
        public float Th { get; set; }
        public float Pr { get; set; }
        public float Ct { get; set; }
        public float Tu { get; set; }
        public float Prs { get; set; }
        public float Credit { get; set; }

        //public float Max_th { get; set; }
        //public float Max_pr { get; set; }
        //public float Max_ia { get; set; }
        //public float Sca_grade { get; set; }
        //public float Min_th { get; set; }
        //public float Min_pr { get; set; }
        //public float Min_ia { get; set; }
        public int Paper_id { get; set; }
        public int StreamID { get; set; }
        //public string StreamName { get; set; } = string.Empty;
        //public string StreamCode { get; set; } = string.Empty;

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int CourseTypeID { get; set; }
        public List<int> Paper_IDs { get; set; } = new List<int>();
    }

    public class PaperUploadModel
    {
        public string? PaperUploadID { get; set; }
        public string? ExamID { get; set; }
        public string? ExamName { get; set; }
        public string? StreamID { get; set; }
        public string? SemesterID { get; set; }
        public string? Password { get; set; }
        public string? PaperID { get; set; }
        public string? EndTermID { get; set; }
        public string? FinancialYearID { get; set; }
        public string? FileName { get; set; }
        public string? Dis_FileName { get; set; }
        public DateTime? PaperDate { get; set; }
        public string? CenterCode { get; set; }
        public bool? Active { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
    }

    public class PaperUploadSearchModel
    {
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
    }
}
