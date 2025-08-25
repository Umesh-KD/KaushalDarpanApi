using AutoMapper;
using EmitraEmitraEncrytDecryptClient;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.EgrassPayment;
using Kaushal_Darpan.Models.RPPPayment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]

    public class EgrassPaymentServiceController : BaseController
    {
        public override string PageName => "EgrassPaymentServiceController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly M_AadharCardServiceMaster _m_AadharCardServiceMaster;

        public EgrassPaymentServiceController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _m_AadharCardServiceMaster = _unitOfWork.CommonFunctionRepository.GetAadharCardServiceMaster().Result;
        }

        #region "Egrass PAYMENT INTEGRAION"
        [HttpPost("EmitraPayment")]
        public async Task<ApiResult<EmitraRequestDetailsModel>> EmitraPayment(EmitraRequestDetailsModel Model)
        {
            ActionName = "EmitraPayment(EmitraRequestDetailsModel Model)";

            var requestDetailsModel = new ApiResult<EmitraRequestDetailsModel>();
            try
            {
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);

                if (EmitraServiceDetail.REVENUEHEAD == null)
                {
                    requestDetailsModel.Data = Model;
                    requestDetailsModel.State = EnumStatus.Error;
                    requestDetailsModel.ErrorMessage = "Service Id Not Mapped";
                    return requestDetailsModel;
                }

                EmitraTransactionsModel objEmitra = new EmitraTransactionsModel();
                objEmitra.key = "_InsertDetails";
                objEmitra.ApplicationIdEnc = Model.ApplicationIdEnc;
                objEmitra.Amount = Model.Amount;
                var result = await _unitOfWork.CommonFunctionRepository.CreateEmitraTransation(objEmitra);
                _unitOfWork.SaveChanges();

                if (result.TransactionId > 0)
                {
                    PGRequestModel data = new PGRequestModel();
                    data.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;
                    Random rnd = new Random();
                    data.PRN = "NOC" + rnd.Next(100000, 999999) + rnd.Next(100000, 999999);

                    data.REQTIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    data.AMOUNT = Convert.ToString(Model.Amount);
                    data.SUCCESSURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&ServiceID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("NO");
                    data.FAILUREURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&ServiceID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("YES");
                    data.USERNAME = Model.UserName;
                    data.USERMOBILE = Model.MobileNo;
                    data.COMMTYPE = EmitraServiceDetail.COMMTYPE;
                    data.OFFICECODE = EmitraServiceDetail.OFFICECODE;
                    data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("##", Model.Amount.ToString());
                    data.SERVICEID = EmitraServiceDetail.SERVICEID;
                    data.UDF1 = Model.RegistrationNo;
                    data.UDF2 = Model.SsoID;
                    data.USEREMAIL = "";
                    data.CHECKSUM = CommonFuncationHelper.CreateMD5(data.PRN + "|" + data.AMOUNT + "|" + EmitraServiceDetail.CHECKSUMKEY);

                    EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();

                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, EmitraServiceDetail.WebServiceURL);
                    EmitraEncryptStringResponse response = await emitraencsev.EmitraEncryptStringAsync(EmitraServiceDetail.EncryptionKey, JsonConvert.SerializeObject(data));

                    if (data != null)
                    {
                        try
                        {
                            objEmitra.key = "_UpdateDetails";
                            objEmitra.RequestString = JsonConvert.SerializeObject(data);
                            objEmitra.TransactionId = result.TransactionId;
                            objEmitra.ServiceID = EmitraServiceDetail.SERVICEID;
                            objEmitra.PRN = data.PRN;
                            var UpdateStatus = await _unitOfWork.CommonFunctionRepository.CreateEmitraTransation(objEmitra);
                            _unitOfWork.SaveChanges();
                        }
                        catch (System.Exception ex)
                        {
                            throw ex;
                        }

                    }

                    Model.ENCDATA = response.Body.EmitraEncryptStringResult;
                    Model.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;
                    Model.PaymentRequestURL = EmitraServiceDetail.ServiceURL;
                    Model.ServiceID = EmitraServiceDetail.SERVICEID;
                    Model.IsSucccess = true;

                    requestDetailsModel.Data = Model;
                    requestDetailsModel.State = EnumStatus.Success;
                    requestDetailsModel.Message = "successfully .!";
                }
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                requestDetailsModel.State = EnumStatus.Error;
                requestDetailsModel.ErrorMessage = ex.Message;
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }

            return requestDetailsModel;
        }

        [HttpPost("GRAS_PaymentRequest")]
        public async Task<ApiResult<RPPPaymentRequestModel>> GRAS_PaymentRequest(RPPRequestDetailsModel request)
        {
            var result = new ApiResult<RPPPaymentRequestModel>();
            result.Data = new RPPPaymentRequestModel();
            Random rnd = new Random();
            try
            {
                EGrassPaymentDetails_Req_ResModel eGrassPaymentDetails_Req_Res = new EGrassPaymentDetails_Req_ResModel();
                DataTable dataTable = new DataTable();
                dataTable = await _unitOfWork.CommonFunctionRepository.GetEgrassDetails_DepartmentWise(request.DepartmentID, request.PaymentType);
                if (dataTable == null)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "E-Grass Budget Head Details Not Found.!";
                    result.Data.RequestStatus = false;
                    return result;
                }

                string PRN = DateTime.Now.ToString("yyyyMMddHHmmss");
                string key = "N*($%^$#)il^%$OC";
                string keypath = Path.Combine(Directory.GetCurrentDirectory(), "PaymentKey", "rajnoc.key");
                string dtFrom = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string dtTo = DateTime.Now.AddYears(1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                string finyear = CommonFuncationHelper.FinancialYear.Current.ToString();
                string CHECKSUM = CommonFuncationHelper.EgrassEncrypt(PRN + "|" + Convert.ToDecimal(request.AMOUNT).ToString() + ".00" + "|" + key, keypath);
                request.City = "Jaipur";
                string encAUIN = CommonFuncationHelper.EgrassEncrypt("AUIN=" + PRN + "|MERCHANTCODE=" + dataTable.Rows[0]["MerchantCode"].ToString() + "|TOTALAMOUNT=" + request.AMOUNT.ToString() + ".00", keypath);

                string paystring = "AUIN=" + PRN + "|Head_Name1=" + dataTable.Rows[0]["Head_Name1"].ToString() + "|Head_Amount1=" + Convert.ToDecimal(request.AMOUNT).ToString() + ".00" + "|Head_Name2=0|Head_Amount2=0|Head_Name3=0|Head_Amount3=0|Head_Name4=0|Head_Amount4=0|Head_Name5=0|Head_Amount5=0|Head_Name6=0|Head_Amount6=0|Head_Name7=0|Head_Amount7=0|Head_Name8=0|Head_Amount8=0|Head_Name9=0|Head_Amount9=0|RemitterName=" + request.RemitterName + "|Discount=0|TotalAmount=" + Convert.ToDecimal(request.AMOUNT).ToString() + ".00" + "|MerchantCode=" + dataTable.Rows[0]["MerchantCode"].ToString() + "|PaymentMode=N|REGTINNO=" + request.REGTINNO + "|Location=" + dataTable.Rows[0]["Location"].ToString() + "|DistrictCode=12|OfficeCode=" + dataTable.Rows[0]["OfficeCode"].ToString() + "|DepartmentCode=" + dataTable.Rows[0]["DepartmentCode"].ToString() + "|FromDate=" + dtFrom + "|ToDate=" + dtTo + "|Address=" + request.Adrees + "|PIN=" + request.Pincode + "|City=" + request.City + "|Remarks=SampleRemark|Filler=A|ChallanYear=2024|Checksum=" + CHECKSUM + "";

                string ENCDATA1 = CommonFuncationHelper.EgrassEncrypt(paystring, keypath);
                string decriptString = CommonFuncationHelper.EgrassDecrypt(ENCDATA1, keypath);

                result.Data.MERCHANTCODE = Convert.ToString(dataTable.Rows[0]["MerchantCode"]);
                result.Data.ENCDATA = ENCDATA1;
                result.Data.AUIN = PRN;
                result.Data.PaymentRequestURL = Convert.ToString(dataTable.Rows[0]["PaymentRequestURL"]);

                string json = JsonConvert.SerializeObject(result);
                eGrassPaymentDetails_Req_Res.ApplyNocApplicationID = request.ApplyNocApplicationID;
                eGrassPaymentDetails_Req_Res.DepartmentID = request.DepartmentID;
                eGrassPaymentDetails_Req_Res.Head_Name = Convert.ToString(dataTable.Rows[0]["Head_Name1"]);
                eGrassPaymentDetails_Req_Res.Request_AUIN = PRN;
                eGrassPaymentDetails_Req_Res.Request_CollegeName = request.RemitterName;
                eGrassPaymentDetails_Req_Res.Request_SSOID = request.SSOID;
                eGrassPaymentDetails_Req_Res.Request_AMOUNT = request.AMOUNT;
                eGrassPaymentDetails_Req_Res.Request_MerchantCode = Convert.ToString(dataTable.Rows[0]["MerchantCode"]);
                eGrassPaymentDetails_Req_Res.Request_REGTINNO = request.REGTINNO;
                eGrassPaymentDetails_Req_Res.Request_OfficeCode = Convert.ToString(dataTable.Rows[0]["OfficeCode"]);
                eGrassPaymentDetails_Req_Res.Request_DepartmentCode = Convert.ToString(dataTable.Rows[0]["DepartmentCode"]);
                eGrassPaymentDetails_Req_Res.Request_Checksum = CHECKSUM;
                eGrassPaymentDetails_Req_Res.Request_ENCAUIN = encAUIN;
                eGrassPaymentDetails_Req_Res.Request_Json = paystring;
                eGrassPaymentDetails_Req_Res.Request_JsonENC = ENCDATA1;
                eGrassPaymentDetails_Req_Res.Response_Amount = 0;

                int row = await _unitOfWork.CommonFunctionRepository.EGrassPaymentDetails_Req_Res(eGrassPaymentDetails_Req_Res);
                _unitOfWork.SaveChanges();
                if (row > 0)
                {
                    result.Data.RequestStatus = true;
                }
                else
                {
                    result.Data.RequestStatus = false;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
                result.Data.RequestStatus = false;
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }

        [HttpPost("GRAS_PaymentResponse")]
        public async Task<IActionResult> GRAS_PaymentResponse(string ENCDATA)
        {
            EGrassPaymentDetails_Req_ResModel eGrassPaymentDetails_Req_Res = new EGrassPaymentDetails_Req_ResModel();
            try
            {
                if (ENCDATA != null)
                {
                    string keypath = Path.Combine(Directory.GetCurrentDirectory(), "PaymentKey", "rajnoc.key");
                    string EncryptString = CommonFuncationHelper.EgrassDecrypt(ENCDATA, keypath);

                    eGrassPaymentDetails_Req_Res.Response_JsonENC = ENCDATA;
                    eGrassPaymentDetails_Req_Res.Response_Json = EncryptString;
                    foreach (string kvp in EncryptString.Split('|'))
                    {
                        string Key = kvp.Split('=')[0];
                        string Value = kvp.Split('=')[1];
                        if (Key == "AUIN")
                        {
                            eGrassPaymentDetails_Req_Res.Request_AUIN = Value;
                        }
                        else if (Key == "CIN")
                        {
                            eGrassPaymentDetails_Req_Res.Response_CIN = Value;
                        }
                        else if (Key == "BankReferenceNo")
                        {
                            eGrassPaymentDetails_Req_Res.Response_BankReferenceNo = Value;
                        }
                        else if (Key == "BANK_CODE")
                        {
                            eGrassPaymentDetails_Req_Res.Response_BANK_CODE = Value;
                        }
                        else if (Key == "BankDate")
                        {
                            eGrassPaymentDetails_Req_Res.Response_BankDate = Value;
                        }
                        else if (Key == "GRN")
                        {
                            eGrassPaymentDetails_Req_Res.Response_GRN = Value;
                        }
                        else if (Key == "Amount")
                        {
                            eGrassPaymentDetails_Req_Res.Response_Amount = Convert.ToDecimal(Value);
                            eGrassPaymentDetails_Req_Res.Request_AMOUNT = Convert.ToDecimal(Value);
                        }
                        else if (Key == "Status")
                        {
                            eGrassPaymentDetails_Req_Res.Response_Status = Value;
                        }
                        else if (Key == "checkSum")
                        {
                            eGrassPaymentDetails_Req_Res.Response_checkSum = Value;
                        }

                    }
                    int row = await _unitOfWork.CommonFunctionRepository.EGrassPaymentDetails_Req_Res(eGrassPaymentDetails_Req_Res);
                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return Redirect($"{ConfigurationHelper.EgrassPaymentSuccessUrl}{eGrassPaymentDetails_Req_Res.Request_AUIN}");
        }

        [HttpGet("GetOfflinePaymentDetails/{CollegeID}")]
        public async Task<ApiResult<DataTable>> GetOfflinePaymentDetails(int CollegeID)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetOfflinePaymentDetails(CollegeID));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No record found.!";
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }

        [HttpGet("GRAS_GetPaymentStatus/{EGrassPaymentAID}/{DepartmentID}/{PaymentType}")]
        public async Task<ApiResult<RPPResponseParametersModel>> GRAS_GetPaymentStatus(int EGrassPaymentAID, int DepartmentID, string PaymentType)
        {
            var result = new ApiResult<RPPResponseParametersModel>();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //get payment details form database
                DataTable dataTable = new DataTable();
                dataTable = await _unitOfWork.CommonFunctionRepository.GetEgrassDetails_DepartmentWise(DepartmentID, PaymentType);
                if (dataTable.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Data = new RPPResponseParametersModel();
                    result.Message = "Transaction Updated Successfully.!";
                    return result;
                }
                string ReqURL = dataTable.Rows[0]["VerificationTransactionURL"].ToString();
                //EGrass_AUIN_Verify_Data
                DataTable dt_AUIN_Verify_Data = new DataTable();
                dt_AUIN_Verify_Data = await _unitOfWork.CommonFunctionRepository.GetEGrass_AUIN_Verify_Data(EGrassPaymentAID);

                if (dt_AUIN_Verify_Data == null)
                {
                    result.State = EnumStatus.Success;
                    result.Data = new RPPResponseParametersModel();
                    result.Message = "Transaction Updated Successfully.!";
                    return result;
                }

                ReqURL = ReqURL.Replace("#encAUIN", dt_AUIN_Verify_Data.Rows[0]["Request_ENCAUIN"].ToString());
                ReqURL = ReqURL.Replace("#MerchantCode", dataTable.Rows[0]["MerchantCode"].ToString());

                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(ReqURL);
                webrequest.Method = "GET";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.ContentLength = 0;

                //Stream stream = webrequest.GetRequestStream();
                //stream.Close();
                string RESPONSEJSON = "";
                using (WebResponse response = webrequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        RESPONSEJSON = reader.ReadToEnd();
                        EGrassPaymentDetails_Req_ResModel eGrassPaymentDetails_Req_Res = new EGrassPaymentDetails_Req_ResModel();

                        XmlDocument xmltest = new XmlDocument();
                        xmltest.LoadXml(RESPONSEJSON);
                        XmlNodeList elemlist = xmltest.GetElementsByTagName("string");
                        string result11111 = elemlist[0].InnerXml;

                        eGrassPaymentDetails_Req_Res.Request_JsonENC = ReqURL.ToString();
                        eGrassPaymentDetails_Req_Res.Request_Json = ReqURL.ToString();

                        string keypath = Path.Combine(Directory.GetCurrentDirectory(), "PaymentKey", "rajnoc.key");
                        string EncryptString = CommonFuncationHelper.EgrassDecrypt(result11111, keypath);

                        eGrassPaymentDetails_Req_Res.Response_JsonENC = result11111;
                        eGrassPaymentDetails_Req_Res.Response_Json = EncryptString;

                        foreach (string kvp in EncryptString.Split('|'))
                        {
                            string Key = kvp.Split('=')[0];
                            string Value = kvp.Split('=')[1];
                            if (Key == "AUIN")
                            {
                                eGrassPaymentDetails_Req_Res.Request_AUIN = Value;
                            }
                            else if (Key == "CIN")
                            {
                                eGrassPaymentDetails_Req_Res.Response_CIN = Value;
                            }
                            else if (Key == "BankReferenceNo")
                            {
                                eGrassPaymentDetails_Req_Res.Response_BankReferenceNo = Value;
                            }
                            else if (Key == "BANK_CODE")
                            {
                                eGrassPaymentDetails_Req_Res.Response_BANK_CODE = Value;
                            }
                            else if (Key == "BankDate")
                            {
                                eGrassPaymentDetails_Req_Res.Response_BankDate = Value;
                            }
                            else if (Key == "GRN")
                            {
                                eGrassPaymentDetails_Req_Res.Response_GRN = Value;
                            }
                            else if (Key == "Amount")
                            {
                                eGrassPaymentDetails_Req_Res.Response_Amount = Convert.ToDecimal(Value);
                                eGrassPaymentDetails_Req_Res.Request_AMOUNT = Convert.ToDecimal(Value);
                            }
                            else if (Key == "Status")
                            {
                                eGrassPaymentDetails_Req_Res.Response_Status = Value;
                            }
                            else if (Key == "checkSum")
                            {
                                eGrassPaymentDetails_Req_Res.Response_checkSum = Value;
                            }

                        }
                        int row = await _unitOfWork.CommonFunctionRepository.GRAS_GetPaymentStatus_Req_Res(eGrassPaymentDetails_Req_Res);
                        _unitOfWork.SaveChanges();
                        result.State = EnumStatus.Success;
                        result.Message = "Transaction Updated Successfully .!";
                    }
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }
        #endregion


        //public async Task<string> MakeTransactionsEncrypted(string URL, string data, string encryptionKey,string WebServiceURL)
        //{
        //    try
        //    {

        //        EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();

        //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //        EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, WebServiceURL);
        //        EmitraEncryptStringResponse response = await emitraencsev.EmitraEncryptStringAsync(encryptionKey, JsonConvert.SerializeObject(data));

        //        string encData = response.Body.EmitraEncryptStringResult;
        //        //error.LogEmitra("Encrypted Request Data: Encrypt Data: " + encData, "TestApp2");
        //        //Base String
        //        string baseAddress = URL;
        //        //Post Parameters
        //        StringBuilder postData = new StringBuilder();
        //        postData.Append("encData=" + HttpUtility.UrlEncode(encData));

        //        //Create Web Request
        //        var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
        //        http.Method = "POST";
        //        http.Accept = "application/json";
        //        http.ContentType = "application/x-www-form-urlencoded";

        //        //Start Writing Post parameters to request object
        //        string parsedContent = postData.ToString();
        //        ASCIIEncoding encoding = new ASCIIEncoding();
        //        Byte[] bytes = encoding.GetBytes(parsedContent);
        //        Stream newStream = http.GetRequestStream();
        //        newStream.Write(bytes, 0, bytes.Length);
        //        newStream.Close();

        //        //Read Response for posting done
        //        var responses = http.GetResponse();
        //        var stream = responses.GetResponseStream();
        //        var sr = new StreamReader(stream);
        //        var content = sr.ReadToEnd();
        //        //return contents
        //        return content;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;

        //    }
        //}




        #region "EMITRA ENCRIPT DECRIPT"
        //public async Task<string> EmitraEncryptStringAsync(string encryptionKey,string data,string WebServiceURL)
        //{
        //    EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();

        //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //    EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, WebServiceURL);
        //    EmitraEncryptStringResponse response = await emitraencsev.EmitraEncryptStringAsync(encryptionKey, JsonConvert.SerializeObject(data));
        //    return response.Body.EmitraEncryptStringResult;
        //}
        //public async Task<string> EmitraDecriptStringAsync(string encryptionKey, string data, string WebServiceURL)
        //{
        //    EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();
          
        //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //    EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, WebServiceURL);
        //    EmitraDecriptStringResponse response = await emitraencsev.EmitraDecriptStringAsync(encryptionKey, JsonConvert.SerializeObject(data));
        //    return response.Body.EmitraDecriptStringResult;
        //}

        #endregion

    }
}
