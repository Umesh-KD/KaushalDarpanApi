namespace Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel
{
    public class ITICollegeStudentMarksheetSearchModel
    {
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int EndTermID { get; set; }
        public string? CollegeCode { get; set; }
        public string? RollNumber { get; set; }
        public int? ExamYearID { get; set; }
        public int TradeScheme { get; set; }
        
    }
}
