using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.AadhaarEsignAuth;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace Kaushal_Darpan.Api.Validators
{
    public class AadhaarEsignAuthValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="esignAuth"></param>
        /// <returns></returns>
        public static string GetEsignAuthData_old(EsignAuthRequestModel esignAuth)
        {
            string _txnid = "";
            try
            {
                if (!string.IsNullOrEmpty(esignAuth.AadhaarNo))
                {
                    string encAadharNo = esignAuth.AadhaarNo.ToString();
                    string RequestId = Constants.AADHAAR_ESIGN_AUTH_RETURN_URL.Substring(0, 10) + new Random().Next(10000000, 99999999).ToString();
                    string checksumStr = Constants.AADHAAR_ESIGN_AUTH_APP_CODE + RequestId + encAadharNo + "3";

                    string checksumHash = AadhaarEsignAesEncryption.ComputeSHA256Hash(checksumStr);
                    string data = JsonConvert.SerializeObject(new
                    {
                        SSOToken = esignAuth.SsoToken,
                        AppCode = Constants.AADHAAR_ESIGN_AUTH_APP_CODE,
                        RequestId = RequestId,
                        AadhaarNo = esignAuth.AadhaarNo.ToString(),
                        RequestType = 3,
                        ReturnURL = Constants.AADHAAR_ESIGN_AUTH_RETURN_URL,
                        Checksum = checksumHash,
                        VerificationType = 0,
                        OperatorIdType = 0,
                        OperatorId = "2",
                        Purpose = "Send OTP For Esign Authentication",
                        AuthModality = "1"
                    });

                    string encData = "";
                    EncryptionResultModel encryptionResult = new EncryptionResultModel();
                    encryptionResult = AadhaarEsignAesEncryption.Encrypt(data, Constants.AADHAAR_ESIGN_AUTH_ENC_KEY);
                    string formHtml = "";
                    if (esignAuth.IsReturnFormBodyHtml)
                        formHtml = GenerateForm(Constants.AADHAAR_ESIGN_AUTH_URL, Constants.AADHAAR_ESIGN_AUTH_APP_CODE, Convert.ToBase64String(encryptionResult.EncriptedData));
                    else
                        formHtml = Convert.ToBase64String(encryptionResult.EncriptedData);
                    if (encryptionResult.IsSuccess)
                    {
                        encData = formHtml;
                    }
                    else
                    {
                        encData = "NO" + "#" + encryptionResult.Message.ToString();
                    }
                  


                    var JsonData = new
                    {
                        EncData = encData,
                        RequestID = RequestId
                    };

                    // Convert object to JSON string
                    string jsonString = JsonConvert.SerializeObject(JsonData);


                    return jsonString;


                }
            }
            catch (Exception ex)
            {

            }
            return _txnid;
        }



        public static string GetEsignAuthData(EsignAuthRequestModel esignAuth)
        {
    


            string _txnid = "";
            string ResponseCode = "";
            string status = "";
            string errormsg = string.Empty;
            string succmsg = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(esignAuth.AadhaarNo))
                {
                    string AppCode = "KAUSHAL_D_DCE_TEST_EM2GMO";
       
                    Random rnd1 = new Random();
                    var iRnd1 = rnd1.Next(10000000, 99999999);
                    string SSOToken = iRnd1.ToString();
                    string RequestId = "PRAVL22868" + iRnd1.ToString();
                    //string ReturnURL = HttpContext.Current.Request.UrlReferrer.ToString();
                    //string ReturnURL = "https://rajsahakarapp.rajasthan.gov.in/pldb/biokyc";
                    string ReturnURL = "http://localhost:5230/";



                    string encAadharNo = esignAuth.AadhaarNo.ToString(); // EncryptWithKey(hdn_aadhar.ToString(), GlobalConstant.AadharAuthEsigEncKey_Test, GlobalConstant.AadharAuthEsigAppCode_Test);
                    string checksumStr = AppCode + RequestId + encAadharNo + "3";

                    //GenerateSha256HashNew
                    byte[] inputBytes = Encoding.UTF8.GetBytes(checksumStr);
                    string Checksum = ComputeSHA256Hash(inputBytes);

                    try
                        {


                            string data = JsonConvert.SerializeObject(new
                            {
                                SSOToken = SSOToken,
                                AppCode = AppCode,
                                RequestId = RequestId,
                                AadhaarNo = esignAuth.AadhaarNo.ToString(),
                                RequestType = 3,
                                ReturnURL = ReturnURL,
                                Checksum = Checksum,
                                VerificationType = 0,
                                OperatorIdType = 0,
                                OperatorId = "2",
                                Purpose = "For Esign Authentication."
                               

                            });


                       



                        //string encData = EsignAesEncryption.Encrypt(data, GlobalConstant.AadharAuthEsigEncKey_Test);

                        string encData = "";
                            EncryptionResultModel e = new EncryptionResultModel();
                            
                                e = AadhaarEsignAesEncryption.Encrypt_New(data, Constants.AADHAAR_ESIGN_AUTH_ENC_KEY);


                            if (e.IsSuccess)
                            {
                                encData = Convert.ToBase64String(e.EncriptedData);
                            }
                            else
                            {
                                encData = "NO" + "#" + e.Message.ToString();
                            }

                            // Sp_callTrackingInsert("csCommon GetEncDataForEsign :-", encData);
                            var JsonData = new
                            {
                                EncData = encData,
                                RequestID = RequestId
                            };

                            // Convert object to JSON string
                            string jsonString = JsonConvert.SerializeObject(JsonData);


                            return jsonString;

                        }
                        catch (Exception ex)
                        {
                           
                        }
                    
                }
            }
            catch (Exception ex)
            {
               
            }
            return _txnid;
        }









































        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId">Request ID generated from Aadhaar authentication</param>
        /// <param name="docFilePath"> You can send multiple document file path with '#' seperator</param>
        /// <param name="location"></param>
        /// <param name="designation"></param>
        /// <param name="llx"></param>
        /// <param name="lly"></param>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <param name="page">This is optional parameter for page number, if you want to eSign specific page then pass page number.</param>
        /// <returns></returns>
        public async Task<ResultMessageModel> EsignPdfDocAsync(string requestId, string docFilePath, string location = "", string designation = "", string llx = "300", string lly = "-100", string positionX = "", string positionY = "", string page = "")
        {
            string final_status = "";
            string final_document = "";
            string openFile = "";
            ResultMessageModel resultMessage = new ResultMessageModel();

            if (requestId != "")
            {
                try
                {
                    docFilePath = docFilePath.Replace("\\", "/");
                    foreach (string file in docFilePath.Split('#'))
                    {
                        string filename = file.Split('/')[file.Split('/').Length - 1];
                        byte[] pdfBytes = File.ReadAllBytes(file);
                        string pdfBase64 = Convert.ToBase64String(pdfBytes);
                        string esignUrl = Constants.AADHAAR_GENERIC_MULTIPLE_ESIGN_PAGE_URL;
                        string jsonData = JsonConvert.SerializeObject(new
                        {
                            inputJson = new
                            {
                                File = pdfBase64
                            },
                            filetype = "PDF",
                            transactionid = requestId,
                            docname = filename,
                            designation = designation,
                            status = (designation != "" ? "Approved" : "SelfAttested"),
                            llx = llx,
                            lly = lly,
                            positionX = positionX,
                            positionY = positionY,
                            mode = "3",
                            page = page
                        });

                        string result = await PostDataAsync(esignUrl, jsonData, "application/json");
                        JObject root2 = (JObject)JObject.Parse(result);
                        foreach (var item in root2)
                        {
                            if (item.Key.ToLower() == "status")
                                final_status = item.Value.ToString();
                            if (item.Key.ToLower() == "document")
                            {
                                final_document = item.Value.ToString().Replace("\"", "");
                                byte[] sPDFDecoded = Convert.FromBase64String(final_document);
                                FileStream stream = new FileStream(file, FileMode.Create);
                                BinaryWriter writer = new BinaryWriter(stream);
                                writer.Write(sPDFDecoded, 0, sPDFDecoded.Length);
                                writer.Close();
                                openFile = "esign";
                                resultMessage = new ResultMessageModel { Status = "1", Data = "Success" };
                            }
                        }
                        if (final_status != "1" && !openFile.Equals("esign"))
                        {
                            resultMessage = new ResultMessageModel { Status = "0", Data = "Final Status Failed -" + final_status };
                            return resultMessage;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return resultMessage;
        }

        public async Task<string> PostDataAsync(string url, string postData, string contentType)
        {
            string result = string.Empty;
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(postData, Encoding.UTF8, contentType);
                var response = await httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }


        // Method to generate the HTML form string
        public static string GenerateForm(string authRequest, string appCode, string data)
        {
            StringBuilder formBuilder = new StringBuilder();
            formBuilder.Append("<html>");
            formBuilder.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            formBuilder.AppendLine("<form id='eSignAuthForm' action=\"" + authRequest + "\" method=\"POST\">");
            formBuilder.AppendLine("<input type=\"hidden\" name=\"AppCode\" value=\"" + appCode + "\" />");
            formBuilder.AppendLine("<input type=\"hidden\" name=\"Data\" value=\"" + data + "\" />");
            formBuilder.AppendLine("</form>");
            formBuilder.Append("</body>");
            formBuilder.Append("</html>");
            return formBuilder.ToString();
        }

        public static string ComputeSHA256Hash(byte[] inputBytes)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder hex = new StringBuilder(hashBytes.Length * 2);
                foreach (byte b in hashBytes)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                return hex.ToString();
            }
        }



        public static string GenerateSha256HashNew(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }




    }
    }
