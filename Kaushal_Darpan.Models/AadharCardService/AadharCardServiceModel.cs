namespace Kaushal_Darpan.Models.AadharCardService
{
    public class AadharCardServiceDataModel
    {
        public string AadharNo { get; set; }
        public string TransactionNo { get; set; }
        public string OTP { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string AadharID { get; set; }
    }
    public class ResponseDataModal
    {
        public string message { get; set; }
        public string status { get; set; }
        public object data { get; set; }
    }
}
