namespace Kaushal_Darpan.Models.EgrassPayment
{
    public class EGrassPaymentDetails_Req_ResModel
    {
        public int ApplyNocApplicationID { get; set; }
        public int DepartmentID { get; set; }
        public string? Head_Name { get; set; }
        public string? Request_AUIN { get; set; }
        public string? Request_CollegeName { get; set; }
        public string? Request_SSOID { get; set; }
        public decimal? Request_AMOUNT { get; set; }
        public string? Request_MerchantCode { get; set; }
        public string? Request_REGTINNO { get; set; }
        public string? Request_OfficeCode { get; set; }
        public string? Request_DepartmentCode { get; set; }
        public string? Request_Checksum { get; set; }
        public string? Request_ENCAUIN { get; set; }
        public string? Request_Json { get; set; }
        public string? Request_JsonENC { get; set; }
        public string? Response_CIN { get; set; }
        public string? Response_BankReferenceNo { get; set; }
        public string? Response_BANK_CODE { get; set; }
        public string? Response_BankDate { get; set; }
        public string? Response_GRN { get; set; }
        public decimal? Response_Amount { get; set; }
        public string? Response_Status { get; set; }
        public string? Response_checkSum { get; set; }
        public string? Response_Json { get; set; }
        public string? Response_JsonENC { get; set; }
    }

}
