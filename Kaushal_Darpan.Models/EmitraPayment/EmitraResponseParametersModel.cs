using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Models.RPPPayment
{
    public class EmitraResponseParametersModel
    {
        public string MERCHANTCODE { get; set; }
        public string REQTIMESTAMP { get; set; }
        public string PRN { get; set; }
        public string AMOUNT { get; set; }
        public string PAIDAMOUNT { get; set; }
        public string SERVICEID { get; set; }
        public string TRANSACTIONID { get; set; }
        public string RECEIPTNO { get; set; }
        public string EMITRATIMESTAMP { get; set; }
        public string STATUS { get; set; }
        public string PAYMENTMODE { get; set; }
        public string PAYMENTMODEBID { get; set; }
        public string PAYMENTMODETIMESTAMP { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEMESSAGE { get; set; }
        public string UDF1 { get; set; }
        public string UDF2 { get; set; }
        public string CHECKSUM { get; set; }

        public string ApplicationIdEnc { get; set; }
        public string UniquerequestId { get; set; }

        public string ResponseString { get; set; }
        public string ExamStudentStatus { get; set; }
        public string TransactionNo { get; set; } = string.Empty;

    }

    public class MobilaAppCancelMerchanttokenResponse
    {
        public int statusCode { get; set; }

        public string? statusMessage { get; set; }

        public DataTokenModel? data { get; set; }
    }

    public class DataTokenModel
    {
        public string? access_token { get; set; }
        public string? token_type { get; set; }
        public string? refresh_token { get; set; }
        public int expires_in { get; set; }
    }


    public class VerifywallettransactionsResponse
    {
        public int statusCode { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public EmitraResponseParametersModel data { get; set; }
        public VerifyError Error { get; set; }
    }
    public class VerifyError
    {
        public string code { get; set; }
        public string reason { get; set; }
    }



}
