using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.AadhaarEsignAuth
{
    public class EncryptionResultModel
    {
        public bool IsSuccess { get; set; }
        public byte[] EncriptedData { get; set; } = new byte[0];
        public string DecriptedData { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
    public class DecryptedAadhaarResponseModel
    {
        public string RequestId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string ResponseCode { get; set; } = string.Empty;
    }

    public class ResultMessageModel
    {
        public string Status { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string Responce { get; set; } = string.Empty;
    }
    public class EsignAuthRequestModel
    {
        public string SsoToken { get; set; } = string.Empty;
        public string AadhaarNo { get; set; } = string.Empty;
        public bool IsReturnFormBodyHtml { get; set; }
    }
    public class EsignAuthResponseModel
    {
        public string Status { get; set; } = string.Empty;
        public string ResponseCode { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
    }
}
