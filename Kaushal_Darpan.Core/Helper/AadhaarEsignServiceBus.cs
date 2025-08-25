using Kaushal_Darpan.Models.AadhaarEsignAuth;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Helper
{
    public static class AadharEsignServiceBus
    {
        public static string GetSignedXML(GetSignedXMLModel objModel)
        {
            var objTxn = CommonDynamicCodes.EsignClientCode + "_" + System.DateTime.Now.ToString("ddMMyyyyHHmmssfffff");
            try
            {
                var url = $"{CommonDynamicUrls.GetSignedXMLUrl}?client_id={CommonDynamicCodes.EsignClientId}";

                var requestBody = new
                {
                    pdfFile1 = objModel.Base64PdfFile,
                    signatureOnPageNumber = objModel.SignatureOnPageNumber,
                    clientCode = CommonDynamicCodes.EsignClientCode,
                    xcord = objModel.Xcord,
                    ycord = objModel.Ycord,
                    responseUrl = objModel.ResponseUrl,
                    txn = objTxn,
                    designation = objModel.Designation,
                    location = objModel.Location,
                    sigsize = objModel.Sigsize,
                };
                var jsonBody = JsonConvert.SerializeObject(requestBody);
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                HttpClient httpClient = new HttpClient();
                var response = httpClient.SendAsync(request).Result;//call
                response.EnsureSuccessStatusCode();
                var responseData = response.Content.ReadAsStringAsync().Result;                
                //
                return responseData;
            }
            catch (Exception Ex)
            {

            }
            return null;
        }

        public static string GetSignedPDF(GetSignedPDFModel objModel)
        {
            try
            {
                var url = $"{CommonDynamicUrls.GetSignedPDFUrl}?client_id={CommonDynamicCodes.EsignClientId}";

                var base64Esign = CommonFuncationHelper.EncodeUTF8ToBase64(objModel?.esignData);
                var requestBody = new
                {
                    esignResponse = base64Esign,
                    clientCode = CommonDynamicCodes.EsignClientCode,
                    txn = objModel.Txn,
                    username = objModel.UserNameInAadhar,
                };
                var jsonBody = JsonConvert.SerializeObject(requestBody);
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                HttpClient httpClient = new HttpClient();
                var response = httpClient.SendAsync(request).Result;//call
                response.EnsureSuccessStatusCode();
                var responseData = response.Content.ReadAsStringAsync().Result;
                
                return responseData;
            }
            catch (Exception Ex)
            {

            }
            return null;
        }
    }
}
