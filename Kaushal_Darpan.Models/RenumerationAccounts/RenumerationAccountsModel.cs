namespace Kaushal_Darpan.Models.RenumerationAccounts
{
    public class RenumerationAccountsRequestModel : RequestBaseModel
    {
        public string SSOID { get; set; }
        public int ExaminerID { get; set; }
        public int GroupCodeID { get; set; }
        public int RenumerationExaminerStatusID { get; set; }
    }
    
    public class RenumerationAccountsModel : ResponseBaseModel
    {
        public int RenumerationExaminerID { get; set; }
        public bool Selected { get; set; }
        public int GroupCodeID { get; set; }
        public int GroupCode { get; set; }
        public string ExaminerCode { get; set; }
        public int ExaminerID { get; set; }
        public string SSOID { get; set; }
        public int TotalAllotedCopy { get; set; }
        public int TotalPendingCopy { get; set; }
        public int TotalSavedCopy { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string FileName { get; set; }

        public int TVNo { get; set; }
        public string VoucharNo { get; set; }
        public string ClearDate { get; set; }
        public string Remark { get; set; }
        public int BillStatus { get; set; }
        public bool? IsBillGenerated { get; set; }
        public string? BillGeneratedDate { get; set; }
    } 
    
    public class RenumerationAccountsSaveModel : ResponseBaseModel
    {
        public int RenumerationExaminerID { get; set; }
        public int TVNo { get; set; }
        public string VoucharNo { get; set; }
        public string ClearDate { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public int BillStatus { get; set; }
        public int CourseTypeID { get; set; }
        public int UserId { get; set; }
        public int? IsBillGenerated { get; set; }
        public string? BillGeneratedDate { get; set; }
    }

}
