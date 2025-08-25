namespace Kaushal_Darpan.Models
{
    public class RequestBaseModel
    {
        public int? DepartmentID { get; set; } = 0;
        public int? Eng_NonEng { get; set; } = 0;// course type
        public int? EndTermID { get; set; } = 0;
        public int? TermPart { get; set; } = 0;
        public int? FinancialYearID { get; set; } = 0;
        public int? IsYearly { get; set; } = 0;
        public int? RoleID { get; set; } = 0;
        public int StudentEntryType { get; set; }
        public int InstituteId { get; set; } = 0;
        public int? ServiceID { get; set; } = 0;
        public int? UserID { get; set; } = 0;
        public string? ExamDate { get; set; }

          public int? SessionTypeID { get; set; } = 0;
    }
}
