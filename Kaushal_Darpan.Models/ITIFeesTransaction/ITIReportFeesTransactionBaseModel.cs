namespace Kaushal_Darpan.Models.ReportFeesTransactionModel
{
    public class ITIReportFeesTransaction : RequestBaseModel
    {
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string? ApplicationNo { get; set; }
    }
    public class ITIReportFeesTransactionSearchModel
    {
        public int ? DepartmentID { get; set; }
        
        public string? TransactionType  { get; set; } 
        public string? Status { get; set; } 
        public string? TransactionId { get; set; } 
        public int? StudentExamID { get; set; }    


    }




}
