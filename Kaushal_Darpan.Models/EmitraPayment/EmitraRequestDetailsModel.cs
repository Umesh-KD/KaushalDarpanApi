namespace Kaushal_Darpan.Models.RPPPayment
{
    public class EmitraRequestDetailsModel
    {
        public string AppRequestID { get; set; }

        public int ID { get; set; }
        public string ServiceID { get; set; }
        public string ApplicationIdEnc { get; set; }
        public decimal Amount { get; set; }

        public string UserName { get; set; }
        public string MobileNo { get; set; }

        public string RegistrationNo { get; set; }
        public string FeeFor { get; set; }

        public string SsoID { get; set; }

        public string RESPONSEJSON { get; set; }
        public string STATUS { get; set; }
        public string ENCDATA { get; set; }
        public string PaymentRequestURL { get; set; }
        public string MERCHANTCODE { get; set; }
        public bool IsKiosk { get; set; }
        public bool IsSucccess { get; set; }
        public int StudentID { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int ExamStudentStatus { get; set; }

        public string UserType { get; set; }
        public string TransactionID { get; set; }
        public string VerifyURL { get; set; }
        public string PRN { get; set; }
        public int TypeID { get; set; }
        //public string AMOUNT { get; set; }
        public int[] TransactionApplicationIDs { get; set; }

        public List<StudentFeesTransactionItems>? StudentFeesTransactionItems { get; set; }
        public decimal ProcessingFee { get; set; }
        public decimal FormCommision { get; set; }
        public string USEREMAIL { get; set; } = string.Empty;
        public string KIOSKCODE { get; set; } = string.Empty;
        public string SSoToken { get; set; } = string.Empty;
        public string ViewName { get; set; } = string.Empty;
        public string? InstituteIDEnc { get; set; } = string.Empty;
        public int DirectAdmission { get; set; }

    }

    public class EmitraVerifyRequest
    {
        public string SSOTOKEN { get; set; }
        public string MERCHANTCODE { get; set; }
        public string REQUESTID { get; set; }
        public string SERVICEID { get; set; }
        public string CHECKSUM { get; set; }

    }

    public class EmitraFeeVeryficationEntity
    {
        public string TransactionID { get; set; }
        public int SERICEID { get; set; }
        public int UniqueId { get; set; }
        public string UserType { get; set; }
        public string SocietyIDEnc { get; set; }
        public int applicationid { get; set; }

        public string PRN { get; set; }
        public string AMOUNT { get; set; }


    }

    public class StudentFeesTransactionItems
    {
        public int TransactionItemID { get; set; }
        public int TransactionId { get; set; }
        public int TransactionApplicationID { get; set; }
        public int Status { get; set; }
        public int TranSemesterID { get; set; }
        public int ItemAmount { get; set; }
    }

    public class EmitraServiceAndFeeModel
    {
        public string ServiceId { get; set; }
        public int UniqueServiceID { get; set; }
        public decimal ApplicationFees { get; set; }
    }

    public class EmitraServiceAndFeeRequestModel : RequestBaseModel
    {
        public int TypeID { get; set; }
        public string FeeFor { get; set; }
    }

}
