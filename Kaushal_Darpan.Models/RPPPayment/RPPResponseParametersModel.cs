namespace Kaushal_Darpan.Api.Models
{
    public class RPPResponseParametersModel
    {
        public string MERCHANTCODE { get; set; }
        public string REQTIMESTAMP { get; set; }
        public string PRN { get; set; }
        public decimal? AMOUNT { get; set; }
        public string RPPTXNID { get; set; }
        public string RPPTIMESTAMP { get; set; }
        public string PAYMENTAMOUNT { get; set; }
        public string STATUS { get; set; }
        public string PAYMENTMODE { get; set; }
        public string PAYMENTMODEBID { get; set; }
        public string PAYMENTMODETIMESTAMP { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEMESSAGE { get; set; }
        public string UDF1 { get; set; }
        public string UDF2 { get; set; }
        public string UDF3 { get; set; }
        public string CHECKSUM { get; set; }
        public string CreatedDate { get; set; }

        public string REFUNDID { get; set; }
        public string REFUNDSTATUS { get; set; }
        public string REFUNDTIMESTAMP { get; set; }
        public string REMARKS { get; set; }


    }
}
