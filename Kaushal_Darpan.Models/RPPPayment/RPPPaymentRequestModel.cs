namespace Kaushal_Darpan.Api.Models
{
    public class RPPPaymentRequestModel
    {
        public string MERCHANTCODE { get; set; }
        public RPPRequestParametersModel REQUESTPARAMETERS { get; set; }
        public string REQUESTJSON { get; set; }
        public string ENCDATA { get; set; }
        public string PaymentRequestURL { get; set; }
        public string CreatedBy { get; set; }
        public string SSOID { get; set; }
        public string AUIN { get; set; }
        public bool RequestStatus { get; set; }

    }
}
