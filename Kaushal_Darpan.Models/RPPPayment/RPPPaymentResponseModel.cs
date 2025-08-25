namespace Kaushal_Darpan.Api.Models
{
    public class RPPPaymentResponseModel
    {
        public RPPResponseParametersModel RESPONSEPARAMETERS { get; set; }
        public string RESPONSEJSON { get; set; }
        public string STATUS { get; set; }
        public string ENCDATA { get; set; }
        public bool CHECKSUMVALID { get; set; }
        public string PaymentRequestURL { get; set; }
        public string CreatedBy { get; set; }
        public string SSOID { get; set; }

    }
}
