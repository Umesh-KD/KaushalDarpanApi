namespace Kaushal_Darpan.Api.Models
{
    public class RPPRequestParametersModel
    {
        public string MERCHANTCODE { get; set; }
        public string PRN { get; set; }
        public string REQTIMESTAMP { get; set; }
        public string PURPOSE { get; set; }
        public string AMOUNT { get; set; }
        public string SUCCESSURL { get; set; }
        public string FAILUREURL { get; set; }
        public string CANCELURL { get; set; }
        public string CALLBACKURL { get; set; }
        public string USERNAME { get; set; }
        public string USERMOBILE { get; set; }
        public string USEREMAIL { get; set; }
        public string UDF1 { get; set; }
        public string UDF2 { get; set; }
        public string UDF3 { get; set; }
        public string OFFICECODE { get; set; }
        public string REVENUEHEAD { get; set; }
        public string CHECKSUM { get; set; }
        public string CreatedDate { get; set; }
        public int RequestType { get; set; }
        public string RPPTXNID { get; set; }
    }
}
