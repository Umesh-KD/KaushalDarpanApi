namespace Kaushal_Darpan.Models.RPPPayment
{
    public class PGRequestModel
    {
        public string MERCHANTCODE { get; set; }
        public string PRN { get; set; }
        public string REQTIMESTAMP { get; set; }
        public string AMOUNT { get; set; }
        public string SUCCESSURL { get; set; }
        public string FAILUREURL { get; set; }
        public string USERNAME { get; set; }
        public string USERMOBILE { get; set; }
        public string USEREMAIL { get; set; }
        public string UDF1 { get; set; }
        public string UDF2 { get; set; }
        public string SERVICEID { get; set; }
        public string OFFICECODE { get; set; }
        public string REVENUEHEAD { get; set; }
        public string COMMTYPE { get; set; }
        public string CHECKSUM { get; set; }
        public string ApplicationIdEnc { get; set; }
        public string UniquerequestId { get; set; }
        public string PaymentFor { get; set; }
        public string CONSUMERKEY { get; set; } = string.Empty;
        public string LOOKUPID { get; set; } = string.Empty;
        public string CHANNEL { get; set; } = string.Empty;
        public string SSOTOKEN { get; set; } = string.Empty;
        public string SSOID { get; set; } = string.Empty;
        public string REQUESTID { get; set; } = string.Empty;
        public string SUBSERVICEID { get; set; } = string.Empty;
        public string CONSUMERNAME { get; set; } = string.Empty;
        

    }
}
