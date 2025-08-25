using System.Diagnostics.Metrics;

namespace Kaushal_Darpan.Models.RPPPayment
{
    public class EmitraRequstParametersModel
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
        public bool IsKiosk { get; set; }
        public string ServiceName { get; set; }
        public string? ServiceType { get; set; }

        public string CHECKSUMKEY { get; set; }
        public string REDIRECTURL { get; set; }
        public string EncryptionKey { get; set; }
        public string ServiceURL { get; set; }

        public string AMOUNT { get; set; }

        public string PRN { get; set; }

        public string SUCCESSURL { get; set; }
        public string FAILUREURL { get; set; }
        public string USERNAME { get; set; }
        public string USERMOBILE { get; set; }

        public string USEREMAIL { get; set; }


        public string UDF1 { get; set; }
        public string UDF2 { get; set; }

        public string WebServiceURL { get; set; }
        public string SuccessFailedURL { get; set; }

        public string VerifyURL { get; set; } = string.Empty;

        public int DepartmentID { get; set; } = 0;

        public string ClientSecret { get; set; } = string.Empty;
        public string CleintID { get; set; } = string.Empty;
        public string TokenURL { get; set; } = string.Empty;
        public string ViewName { get; set; } = string.Empty;



    }
}
