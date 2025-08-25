using Kaushal_Darpan.Api.Models;

using Kaushal_Darpan.Models.RPPPayment;
using Kaushal_Darpan.Models.SSOUserDetails;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace Kaushal_Darpan.Core.Helper
{
    public static class ThirdPartyServiceHelper
    {
        private static string wsUsername = "RAJSAHKAR.TEST";
        private static string wsPassword = "Rajasthan@12";
        public static string GetAadharByVID(string _UUID, string subaua, string url)
        {
            string _AadhaarNo = string.Empty;
            //string subaua = ""; //_configuration["AadharServiceDetails:subaua"].ToString();
            // string subaua = "PNSCL22866";

            if (!string.IsNullOrEmpty(_UUID) && _UUID.Length == 15)
            {
                string ip = CommonFuncationHelper.GetIpAddress();
                //string url = "https://api.sewadwaar.rajasthan.gov.in/app/live/Aadhaar/Prod/detokenizeV2/doitAadhaar/encDec/demo/hsm/auth?client_id=f6de7747-60d3-4cf0-a0ae-71488abd6e95";
                //  string url = ""; //_configuration["AadharServiceDetails:GetAadhaarNoByVIDURL"].ToString();

                string ModifiedData = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><AuthRequest UUID=\"" + _UUID + "\" subaua=\"" + subaua + "\" flagType=\"" + "A" + "\" ver=\"2.5\"></AuthRequest>";

                System.Net.WebRequest req = null;
                WebResponse rsp = null;
                try
                {
                    req = WebRequest.Create(url);
                    req.Method = "POST";
                    req.ContentType = "application/xml";
                    req.Headers["appname"] = "CO-Operative";
                    StreamWriter writer = new StreamWriter(req.GetRequestStream());
                    writer.Write(ModifiedData);
                    writer.Close();
                    rsp = req.GetResponse();
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string results = sr.ReadToEnd();
                    sr.Close();
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(results);
                    XmlNodeList xnList = xml.SelectNodes("/DSMTokanize");
                    string sa = "";
                    foreach (XmlNode xn in xnList)
                    {
                        sa = xn["status"].InnerText;
                        if (sa.ToUpper() == "Y")
                            _AadhaarNo = xn["AadhaarNo"].InnerText;
                        else
                            _AadhaarNo = "NO";
                    }
                    if (sa.ToUpper() == "N")
                    {

                        _AadhaarNo = "NO";
                    }
                }
                catch (Exception ex)
                {
                    _AadhaarNo = "NO#" + ex.Message;
                }
            }
            else
            {
                _AadhaarNo = "NO" + "#" + "ReferenceId Not Found.";
            }
            return _AadhaarNo;
        }

        #region "RPP PAYMENT"
        public static RPPPaymentRequestModel RPPSendRequest(string PRN, string AMOUNT, string PURPOSE, string USERNAME, string USERMOBILE, string USEREMAIL, string ApplyNocApplicationID, RPPPaymentGatewayDataModel Model)
        {
            string REQTIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string CHECKSUM = CommonFuncationHelper.MD5HASHING(Model.MerchantCode + "|" + PRN + "|" + AMOUNT + "|" + Model.CheckSumKey);

            RPPRequestParametersModel REQUESTPARAMS = new RPPRequestParametersModel
            {
                MERCHANTCODE = Model.MerchantCode, //MERCHANTCODE,
                PRN = PRN,
                REQTIMESTAMP = REQTIMESTAMP,
                PURPOSE = PURPOSE,
                AMOUNT = AMOUNT,
                SUCCESSURL = Model.SuccessURL + "?DepartmentID=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(Model.DepartmentID)),// SUCCESSURL,
                FAILUREURL = Model.SuccessURL + "?DepartmentID=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(Model.DepartmentID)),// SUCCESSURL,, //FAILUREURL,
                CANCELURL = Model.CencelURL + "?DepartmentID=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(Model.DepartmentID)),// SUCCESSURL,, //CANCELURL,
                CALLBACKURL = Model.CallBackURL,
                USERNAME = USERNAME,
                USERMOBILE = USERMOBILE,
                USEREMAIL = USEREMAIL,
                UDF1 = ApplyNocApplicationID,
                UDF2 = PURPOSE,
                UDF3 = "PARAM3",
                OFFICECODE = "",
                REVENUEHEAD = "AMOUNT=" + AMOUNT.ToString(),
                CHECKSUM = CHECKSUM
            };



            string REQUESTJSON = JsonConvert.SerializeObject(REQUESTPARAMS);
            string ENCDATA = CommonFuncationHelper.AESEncrypt(REQUESTJSON, Model.ENCRYPTIONKEY);
            RPPPaymentRequestModel PAYMENTREQUEST = new RPPPaymentRequestModel
            {
                MERCHANTCODE = Model.MerchantCode,
                REQUESTJSON = REQUESTJSON,
                REQUESTPARAMETERS = REQUESTPARAMS,
                ENCDATA = ENCDATA,
                PaymentRequestURL = Model.PaymentRequestURL
            };

            return PAYMENTREQUEST;
        }


        public static RPPPaymentResponseModel RPPGetResponse(string STATUS, string ENCDATA, RPPPaymentGatewayDataModel Model)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            string RESPONSEJSON = CommonFuncationHelper.AESDecrypt(ENCDATA, Model.ENCRYPTIONKEY);
            RPPResponseParametersModel RESPONSEPARAMS = JsonConvert.DeserializeObject<RPPResponseParametersModel>(RESPONSEJSON);
            string CHECKSUM = CommonFuncationHelper.MD5HASHING(Model.MerchantCode + "|" + RESPONSEPARAMS.PRN + "|" + RESPONSEPARAMS.RPPTXNID + "|" + RESPONSEPARAMS.PAYMENTAMOUNT + "|" + Model.CheckSumKey);

            RPPPaymentResponseModel PAYMENTRESPONSE = null;
            if (CHECKSUM == RESPONSEPARAMS.CHECKSUM.ToUpper())
            {
                PAYMENTRESPONSE = new RPPPaymentResponseModel
                {
                    RESPONSEJSON = RESPONSEJSON,
                    ENCDATA = ENCDATA,
                    RESPONSEPARAMETERS = RESPONSEPARAMS,
                    STATUS = STATUS,
                    CHECKSUMVALID = true
                };
            }
            else
            {
                PAYMENTRESPONSE = new RPPPaymentResponseModel
                {
                    RESPONSEJSON = RESPONSEJSON,
                    ENCDATA = ENCDATA,
                    RESPONSEPARAMETERS = RESPONSEPARAMS,
                    STATUS = STATUS,
                    CHECKSUMVALID = false
                };
            }
            return PAYMENTRESPONSE;
        }
        #endregion

        #region SSO Landing
        public static ApiResult<SSO_TokenDetailModel> GetSSOTokenDetails(string ssoToken)
        {
            //string SSOID = "vinayyadav329";
            // ssoToken = "R0JoNy8yTHdHaXpNUE9KUjk2RitoL2JLU2M4MG14SG5KU3kwUitGR3p4RVYrQ0RSN2pCbWFRYnJwczVaNzB1ZUIxa2hJbnB3aFRWV3M0ZDRPWXRQMTA5ajhCdFBpVVI2cGRTR2NmSmlybFF6RlBtVUJ5RGdaYU1mc1JZVmx4c3lhYTdzb1o5SHhrdmoyUzZPTlBjOGJJTVRyRCtpNWtaZmRpcG9DVEJrUjB5NUl5bzR5eUdVU0dTbEVSaFkzY3k3";

            string SSOTokenDetailUrl = ConfigurationHelper.SSOTokenDetailUrl;
            var ssoTD = new SSO_TokenDetailModel();
            var isVaildToken = false;

            var result = new ApiResult<SSO_TokenDetailModel>();

            var responseString = Task.Run(() =>
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new System.Net.Http.HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, SSOTokenDetailUrl);
                request.Headers.Add("SSO-TOKEN", ssoToken);
                request.Headers.Add("Authorization", "Basic " +
                Convert.ToBase64String(Encoding.Default.GetBytes(wsUsername + ":" + wsPassword)));
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }).Result;

            if (responseString != null && !responseString.Contains("User Login Id or Password is incorrect"))
            {
                isVaildToken = true;
                ssoTD = JsonConvert.DeserializeObject<SSO_TokenDetailModel>(responseString);
            }

            result.State = isVaildToken ? EnumStatus.Success : EnumStatus.Error;
            result.Data = ssoTD;

            return result;
        }

        public static SSO_UserProfileDetailModel GetSSOUserProfileDetail(string ssoid, string ssoToken)
        {
            string SSOProfileDetailUrl = ConfigurationHelper.SSOProfileDetailUrl;
            var ssoUD = new SSO_UserProfileDetailModel();

            var responseString = Task.Run(() =>
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new System.Net.Http.HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{SSOProfileDetailUrl}/{ssoid}");
                request.Headers.Add("SSO-TOKEN", ssoToken);
                request.Headers.Add("Authorization", "Basic " +
                Convert.ToBase64String(Encoding.Default.GetBytes(wsUsername + ":" + wsPassword)));
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }).Result;

            ssoUD = JsonConvert.DeserializeObject<SSO_UserProfileDetailModel>(responseString);
            return ssoUD;
        }

        public static bool IncreaseSessionTimeSSO(string ssoToken)
        {
            string SSOLogoutUrl = ConfigurationHelper.SSOLogoutUrl;
            var responseString = Task.Run(() =>
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new System.Net.Http.HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{SSOLogoutUrl}");
                request.Headers.Add("SSO-TOKEN", ssoToken);
                request.Headers.Add("Authorization", "Basic " +
                Convert.ToBase64String(Encoding.Default.GetBytes(wsUsername + ":" + wsPassword)));
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }).Result;

            if (responseString == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void LogoutSSO(string ssoToken)
        {
            string SSOLogoutUrl = ConfigurationHelper.SSOLogoutUrl;
            var responseString = Task.Run(() =>
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new System.Net.Http.HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{SSOLogoutUrl}");
                request.Headers.Add("SSO-TOKEN", ssoToken);
                request.Headers.Add("Authorization", "Basic " +
                Convert.ToBase64String(Encoding.Default.GetBytes(wsUsername + ":" + wsPassword)));
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }).Result;
        }
        public static void BackToSSO(string ssoToken)
        {
            string SSOBackToUrl = ConfigurationHelper.SSOBackToUrl;
            var responseString = Task.Run(() =>
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new System.Net.Http.HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{SSOBackToUrl}");
                request.Headers.Add("SSO-TOKEN", ssoToken);
                request.Headers.Add("Authorization", "Basic " +
                Convert.ToBase64String(Encoding.Default.GetBytes(wsUsername + ":" + wsPassword)));
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }).Result;

        }
        // test without token
        public static SSO_UserProfileDetailModel GetSSOUserDetail(string ssoid)
        {
            //string SSOID = "vinayyadav329";
            // ssoToken = "R0JoNy8yTHdHaXpNUE9KUjk2RitoL2JLU2M4MG14SG5KU3kwUitGR3p4RVYrQ0RSN2pCbWFRYnJwczVaNzB1ZUIxa2hJbnB3aFRWV3M0ZDRPWXRQMTA5ajhCdFBpVVI2cGRTR2NmSmlybFF6RlBtVUJ5RGdaYU1mc1JZVmx4c3lhYTdzb1o5SHhrdmoyUzZPTlBjOGJJTVRyRCtpNWtaZmRpcG9DVEJrUjB5NUl5bzR5eUdVU0dTbEVSaFkzY3k3";

            //string SSOProfileDetailUrl = ConfigurationHelper.SSOProfileDetailUrl;
            string SSOProfileDetailUrl = "https://ssotest.rajasthan.gov.in/SSO/GetUserDetails/";
            var ssoUD = new SSO_UserProfileDetailModel();

            var responseString = Task.Run(() =>
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new System.Net.Http.HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{SSOProfileDetailUrl}/{ssoid}");
                request.Headers.Add("Authorization", "Basic " +
                Convert.ToBase64String(Encoding.Default.GetBytes(wsUsername + ":" + wsPassword)));
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }).Result;

            ssoUD = JsonConvert.DeserializeObject<SSO_UserProfileDetailModel>(responseString);
            return ssoUD;
        }


        public static async Task<List<SSOResponseModel>> SSOLoginWithIDPass(string ssoid, string password)
        {
            string SSOAutenticationUrl = ConfigurationHelper.SSOAutenticationUrl;

            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, SSOAutenticationUrl);
            request.Headers.Add("Cookie", "_culture=hi-IN");

            var body = new
            {
                usrnm = "rgavp",
                psw = "rgavp@123",
                srvnm = "RGAVPLogin",
                srvmethodnm = "SSOAuthentication",
                srvparam = JsonConvert.SerializeObject(new
                {
                    sso_id = ssoid,
                    password = password
                })
            };

            string json = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();

                CommonFuncationHelper.WriteTextLog(responseContent);
                // Deserialize to list of SSOResponse
                var result = JsonConvert.DeserializeObject<List<SSOResponseModel>>(responseContent);
                return result ?? new List<SSOResponseModel>();
            }
            catch (Exception ex)
            {
                CommonFuncationHelper.WriteTextLog(ex.Message);
                return new List<SSOResponseModel>();
            }
        }
        #endregion

        #region Emitra
        public static string MakeEmitraTransactionsEncrypted(string URL, string data, string encryptionKey)
        {
            try
            {
                //LogErrorToLogFile error = new LogErrorToLogFile();

                //error.LogEmitra("Kiosk Verify " + data, "TestApp1");

                string encData = EmitraHelper.Encrypt(data, encryptionKey);
                //error.LogEmitra("Encrypted Request Data: Encrypt Data: " + encData, "TestApp2");
                //Base String
                string baseAddress = URL;
                //Post Parameters
                StringBuilder postData = new StringBuilder();
                postData.Append("encData=" + HttpUtility.UrlEncode(encData));

                //Create Web Request
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Method = "POST";
                http.Accept = "application/json";
                http.ContentType = "application/x-www-form-urlencoded";

                //Start Writing Post parameters to request object
                string parsedContent = postData.ToString();
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);
                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                //Read Response for posting done
                var response = http.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                //return contents
                return content;
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }
        #endregion
    }

}
