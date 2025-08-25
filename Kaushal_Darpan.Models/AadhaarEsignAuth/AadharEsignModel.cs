
namespace Kaushal_Darpan.Models.AadhaarEsignAuth
{
    public class GetSignedXMLModel : RequestBaseModel
    {
        public string Base64PdfFile { get; set; }
        public string? SignatureOnPageNumber { get; set; }
        public string? Xcord { get; set; }
        public string? Ycord { get; set; }
        public string? ResponseUrl { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string? Sigsize { get; set; }
        public string UserNameInAadhar { get; set; }
        public int ModifyBy { get; set; }
    }

    public class GetSignedPDFModel
    {
        public string Txn { get; set; }
        public string UserNameInAadhar { get; set; }
        public string esignData { get; set; }
        public int ModifyBy { get; set; }
    }

    public class GetSignedXMLResponseModel
    {
        public string? responseCode { get; set; }
        public string? responseMsg { get; set; }
        public string? signedXMLData { get; set; }
        public string? txn { get; set; }
    }

    public class GetSignedPDFResponseModel
    {
        public string? responseCode { get; set; }
        public string? responseMsg { get; set; }
        public string? signedPDFUrl { get; set; }
        public string? txn { get; set; }
        public string? fileName { get; set; }

    }

    public class EsignDataHistoryRequestModel : ResponseBaseModel
    {
        public int EsignedDataID { get; set; }
        public string Txn { get; set; }
        public string ApiType { get; set; }
        public string Response { get; set; }
        public string UserNameInAadhar { get; set; }
        public string SignedPDFUrl { get; set; }
    }
    public class EsignDataHistoryReponseModel : ResponseBaseModel
    {
        public int EsignedDataID { get; set; }
        public string Txn { get; set; }
        public string ApiType { get; set; }
        public string Response { get; set; }
        public string UserNameInAadhar { get; set; }
        public string SignedPDFUrl { get; set; }
    }

}
