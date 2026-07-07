
namespace Kaushal_Darpan.Models.EmitraPayment
{
    public class DResponse
    {
        public string REQUESTID { get; set; }
        public string TRANSACTIONSTATUSCODE { get; set; }
        public string RECEIPTNO { get; set; }
        public string TRANSACTIONID { get; set; }
        public string TRANSAMT { get; set; }
        public string REMAININGWALLET { get; set; }
        public string EMITRATIMESTAMP { get; set; }
        public string TRANSACTIONSTATUS { get; set; }
        public string MSG { get; set; }
        public string AppNo { get; set; }
        public string ApplicationIdEnc { get; set; }
        public string OtherStatus { get; set; }
        public string RECEIPT_URL { get; set; }

    }


    public class DRequestChecksum
    {
        public string SSOID { get; set; }
        public string REQUESTID { get; set; }
        public string REQTIMESTAMP { get; set; }
        public string SSOTOKEN { get; set; }

    }

    public class VerifyCheckSumEmitra
    {
        public string MERCHANTCODE { get; set; }
        public string REQUESTID { get; set; }
        public string SSOTOKEN { get; set; }

    }


    public class EmitraVerifyModel
    {
        public string MERCHANTCODE { get; set; }
        public string SERVICEID { get; set; }
        public string REQUESTID { get; set; }
        public string SSOTOKEN { get; set; }
        public string CHECKSUM { get; set; }
    }



public class DRequest
{
    public string MERCHANTCODE { get; set; }
    public string REQUESTID { get; set; }
    public string REQTIMESTAMP { get; set; }
    public string SERVICEID { get; set; }
    public string SUBSERVICEID { get; set; }
    public string REVENUEHEAD { get; set; }
    public string CONSUMERKEY { get; set; }
    public string CONSUMERNAME { get; set; }
    public string COMMTYPE { get; set; }
    public string SSOID { get; set; }
    public string OFFICECODE { get; set; }
    public string SSOTOKEN { get; set; }
    public string CHECKSUM { get; set; }

}
    public class DResponseWhatsAppModel
    {
        public string REQUESTID { get; set; }//PRN
        public string TRANSACTIONSTATUSCODE { get; set; }
        public string RECEIPTNO { get; set; }
        public string TRANSACTIONID { get; set; }
        public string TRANSAMT { get; set; }
        public string EMITRATIMESTAMP { get; set; }
        public string TRANSACTIONSTATUS { get; set; }
        public string MSG { get; set; }  
        public string RECEIPT_URL { get; set; }
        public string PRN { get; set; }

        public string STATUS { get; set; }
        
        public string SERVICEID { get; set; }
        public string UniquerequestId { get; set; }
        public string DepartmentID { get; set; }
        public string TransactionNo { get; set; }


    }
}
