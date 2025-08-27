namespace Kaushal_Darpan.Models.RPPPayment
{
    public class EmitraTransactionsModel
    {
        public int TransactionId { get; set; }
        public string ApplicationIdEnc { get; set; }

        public string ApplicationNo { get; set; }
        public string FeeFor { get; set; }

        public string KioskID { get; set; }

        public string ReceiptNo { get; set; }

        public string TokenNo { get; set; }
        public string RequestStatus { get; set; }

        public string StatusMsg { get; set; }

        public string RequestString { get; set; }

        public string ResponseString { get; set; }

        public int ActId { get; set; }
        public string SSOID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIP { get; set; }
        public string ServiceID { get; set; }
        public int UniqueServiceID { get; set; }

        public decimal Amount { get; set; }
        public decimal? EnrollFeeAmount { get; set; }
        public string key { get; set; }

        public string PRN { get; set; }
        public int StudentID { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int ExamStudentStatus { get; set; }
        public string TransactionApplicationID { get; set; }
        public bool IsEmitra { get; set; }
        public string KIOSKCODE { get; set; } = string.Empty;


        public string TransactionNo { get; set; } = string.Empty;
        public decimal PaidAmount { get; set; }
        public List<StudentFeesTransactionItems> StudentFeesTransactionItems { get; set; }
    }

    public class EmitraCollegeTransactionsModel : RequestBaseModel
    {
        public int TransactionId { get; set; }
        public int CollegeId { get; set; }
        public string CollegeIdEnc { get; set; }
        public string FeeFor { get; set; }
        public string KioskID { get; set; }
        public string ReceiptNo { get; set; }
        public string TokenNo { get; set; }
        public string RequestStatus { get; set; }
        public string StatusMsg { get; set; }
        public string RequestString { get; set; }
        public string ResponseString { get; set; }
        public int ActId { get; set; }
        public string SSOID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIP { get; set; }
        public string ServiceID { get; set; }
        public int UniqueServiceID { get; set; }
        public decimal Amount { get; set; }
        public string key { get; set; }
        public string PRN { get; set; }
        public bool IsEmitra { get; set; }
        public string KIOSKCODE { get; set; } = string.Empty;
        public string TransactionNo { get; set; } = string.Empty;
        public decimal PaidAmount { get; set; }
        public string STATUS { get; set; }
        public string TRANSACTIONID { get; set; }
        public string RESPONSEMESSAGE { get; set; }
    }
}
