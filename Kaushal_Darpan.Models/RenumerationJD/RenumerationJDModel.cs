namespace Kaushal_Darpan.Models.RenumerationJD
{
    public class RenumerationJDRequestModel : RequestBaseModel
    {
        public string SSOID { get; set; }
        public int ExaminerID { get; set; }
        public int GroupCodeID { get; set; }
        public int RenumerationExaminerStatusID { get; set; }
    }
    
    public class RenumerationJDModel : ResponseBaseModel
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
    } 
    
    public class RenumerationJDSaveModel : ResponseBaseModel
    {
        public int RenumerationExaminerID { get; set; }
    }

}
