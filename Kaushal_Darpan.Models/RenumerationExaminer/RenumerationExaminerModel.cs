namespace Kaushal_Darpan.Models.RenumerationExaminer
{
    public class RenumerationExaminerRequestModel : RequestBaseModel
    {
        public string SSOID { get; set; }
        public int ExaminerID { get; set; }
        public int GroupCodeID { get; set; }
        public int RenumerationExaminerStatusID { get; set; }
    }
    
    public class RenumerationExaminerModel : ResponseBaseModel
    {
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


    public class TrackStatusDataModel : ResponseBaseModel
    {
        public int TrnID { get; set; }
        public int GroupCodeID { get; set; }
        public int ExaminerID { get; set; }
        public int TotalSavedCopy { get; set; }
        public decimal PerCopyCharge { get; set; }
        public decimal TotalSavedCopyPayment { get; set; }
        public int Status { get; set; }
        public string FileName { get; set; }
        public bool IsESign { get; set; }
        public string ESignDate { get; set; }
        public int GroupCode { get; set; }
        public string StatusName { get; set; }
        public string SSOID { get; set; }
        public string ModifyDate { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }

    public class RenumerationExaminerPDFModel : ResponseBaseModel
    {
        public int GroupCodeID { get; set; } 
        public int ExaminerID { get; set; } 
        public int TotalSavedCopy { get; set; } 
        public decimal PerCopyCharge { get; set; }
        public int Status { get; set; } 
        public string FileName { get; set; } 
        public bool IsESign { get; set; } 
        public string ESignDate { get; set; } 
        public int CreatedBy { get; set; } 
    }

}
