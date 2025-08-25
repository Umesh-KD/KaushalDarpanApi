using AutoMapper;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.AadharCardService;
using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]

    public class AadharCardServiceController : BaseController
    {
        public override string PageName => "AadharCardServiceController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly M_AadharCardServiceMaster _m_AadharCardServiceMaster;

        public AadharCardServiceController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _m_AadharCardServiceMaster = _unitOfWork.CommonFunctionRepository.GetAadharCardServiceMaster().Result;
        }

        [HttpPost("SendOtpByAadharNo_Esign")]
        public async Task<ApiResult<List<DataTable>>> SendOtpByAadharNo_Esign(AadharCardServiceDataModel Model)
        {

            var result = new ApiResult<List<DataTable>>();

            //var urldt = new System.Data.DataTable("tableName");
            //urldt.Columns.Add("message", typeof(string));
            //urldt.Columns.Add("status", typeof(int));
            //urldt.Columns.Add("data", typeof(string));
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("txn", typeof(string));
                string errormsg = string.Empty;
                string succmsg = string.Empty;
                string _txnid = string.Empty;
                string json = "";
                string ss = "";
                string status = "";
                string responseCode = "";
                if (Model.AadharNo.Trim().ToString() != "")
                {
                    if (!string.IsNullOrEmpty(Model.AadharNo) && Convert.ToString(Model.AadharNo).Length == 15)
                    {

                        Model.AadharNo = ThirdPartyServiceHelper.GetAadharByVID(Model.AadharNo,
                            _m_AadharCardServiceMaster.Subaua,
                            _m_AadharCardServiceMaster.GetAadhaarNoByVIDURL);
                    }
                    //
                    json = "{\"aadharid\":\"" + Model.AadharNo.ToString() + "\", \"departmentname\" : \"Agriculture\"}";
                    try
                    {
                        string auacode = _m_AadharCardServiceMaster.eSignOTP;// "";//_configuration["AadharServiceDetails:eSignOTP"].ToString();
                        string lickey = _m_AadharCardServiceMaster.AadhaarLicKey; //_configuration["AadharServiceDetails:AadhaarLicKey"].ToString();
                        ss = CommonFuncationHelper.WebRequestinJson(auacode, json, "application/json");
                        JObject root = (JObject)JObject.Parse(ss);
                        foreach (var item in root)
                        {
                            if (item.Key == "Status")
                                status = item.Value.ToString().Replace("\"", ""); ;
                            if (item.Key == "TransactionId")
                                _txnid = item.Value.ToString().Replace("\"", ""); ;
                        }
                        if (!status.Equals("1"))
                        {
                            errormsg = "status - " + status;
                            _txnid = "NO" + "#" + _txnid;
                        }
                        else if (status.Equals("1"))
                        {
                            _txnid.Replace("\"", "");

                        }
                    }
                    catch (Exception ex)
                    {

                        _txnid = "NO" + "#" + ex.Message;
                    }
                }
                else
                {
                    errormsg = "Fill Aadhar ID!";
                }
                dt.Rows.Add(new Object[] { _txnid });


                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().Contains("Service"))
                    {
                        result.ErrorMessage = dt.Rows[0][0].ToString();
                        result.State = EnumStatus.Error;
                    }
                    else if (dt.Rows[0][0].ToString().Contains("NO#"))
                    {
                        result.ErrorMessage = dt.Rows[0][0].ToString();
                        result.State = EnumStatus.Error;
                    }
                    else
                    {
                        result.Message = dt.Rows[0][0].ToString();
                        result.ErrorMessage = dt.Rows[0][0].ToString();
                        result.State = EnumStatus.Success;
                    }
                }
                else
                {
                    result.ErrorMessage = "AadharID is null";
                    result.State = EnumStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.State = EnumStatus.Error;
            }
            return result;
        }



        //[HttpPost("ValidateAadharOTP_Esign")]
        //public DataTable ValidateAadharOTP_Esign(AadharCardServiceDataModel Model)
        //{
        //    var urldt = new System.Data.DataTable("tableName");
        //    // create fields
        //    urldt.Columns.Add("message", typeof(string));
        //    urldt.Columns.Add("status", typeof(int));
        //    urldt.Columns.Add("data", typeof(string));
        //    try
        //    {
        //        //return string.Empty;
        //        CommonFuncationHelper.EmitraEncrypt(Model.OTP);
        //        CommonFuncationHelper.EmitraEncrypt(Model.TransactionNo);
        //        CommonFuncationHelper.EmitraEncrypt(Model.AadharNo);
        //        DataTable dt = _unitOfWork.AadharCardServiceRepository.ValidateAadhaarOTP_Esign(Model);

        //        List<DataTable> dataModels = new List<DataTable>();
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                //data = CommonFuncationHelper.ConvertDataTable<List<DataTable>>(dataTable);

        //                DataTable dataModel = new DataTable();
        //                dataModel = dt;
        //                dataModels.Add(dataModel);
        //                //return dataModels;
        //            }
        //        }
        //        //create table
        //        if (dt.Rows[0][0].ToString().ToLower().Contains("success"))
        //        {
        //            urldt.Rows.Add(new Object[]{
        //                        "success",0,dataModels
        //        });
        //        }
        //        else
        //        {
        //            urldt.Rows.Add(new Object[]{
        //                        "Invalid OTP!",1,
        //                        dt.Rows[0][0].ToString() });

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        urldt.Rows.Add(new Object[]{
        //                        "Please try again",1,
        //                        "Please try again"
        //                        });
        //        var errorDesc = new ErrorDescription
        //        {
        //            Message = ex.Message,
        //            PageName = "AadharCardService",
        //            ActionName = "ValidateAadharOTP_Esign",
        //            SqlExecutableQuery = ""
        //        };
        //        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //    }
        //    return urldt;
        //}

        //[HttpPost("GetAadharByVID")]
        //public DataTable GetAadharByVID(AadharCardServiceDataModel Model)
        //{
        //    var urldt = new System.Data.DataTable("tableName");
        //    // create fields
        //    urldt.Columns.Add("message", typeof(string));
        //    urldt.Columns.Add("status", typeof(int));
        //    urldt.Columns.Add("data", typeof(string));
        //    try
        //    {
        //        string strResult = _unitOfWork.AadharCardServiceRepository.GetAadharByVID(Model);
        //        if (!string.IsNullOrEmpty(strResult))
        //        {
        //            if (strResult.Contains("NO"))
        //            {
        //                urldt.Rows.Add(new Object[]{
        //                        "Failed",1,
        //                        strResult });

        //            }
        //            else
        //            {
        //                //create table
        //                urldt.Rows.Add(new Object[]{
        //                        "success",0,
        //                        strResult });
        //            }
        //        }
        //        else
        //        {
        //            //create table
        //            urldt.Rows.Add(new Object[]{
        //                        "Failed",1,
        //                        "something went wrong" });

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        var errorDesc = new ErrorDescription
        //        {
        //            Message = ex.Message,
        //            PageName = "AadharCardService",
        //            ActionName = "GetAadharByVID.SaveData",
        //            SqlExecutableQuery = ""
        //        };
        //        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //        urldt.Rows.Add(new Object[]{
        //                        "Please try again",1,
        //                       ex.Message
        //                        });
        //    }
        //    return urldt;
        //}

        //[HttpGet("eSignPDF/{PDFFileName}/{OTPTransactionID}/{DepartmentID}/{ParamID}")]
        //public async Task<DataTable> eSignPDF(string PDFFileName, string OTPTransactionID, int DepartmentID, int ParamID)
        //{
        //    var urldt = new System.Data.DataTable("tableName");
        //    // create fields
        //    urldt.Columns.Add("message", typeof(string));
        //    urldt.Columns.Add("status", typeof(int));
        //    try
        //    {
        //        string str = await Task.Run(() => _unitOfWork.AadharCardServiceRepository.eSignPDF(PDFFileName, OTPTransactionID, DepartmentID, ParamID));
        //        if (str == "Success")
        //        {
        //            urldt.Rows.Add(new Object[]{
        //                        "E-Sign Successfully",0
        //        });
        //        }
        //        else
        //        {
        //            urldt.Rows.Add(new Object[]{
        //                        "Transaction Id Invalid!",1,
        //                        });

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        urldt.Rows.Add(new Object[]{
        //                        "Please try again",1
        //                        });
        //        var errorDesc = new ErrorDescription
        //        {
        //            Message = ex.Message,
        //            PageName = "AadharCardService",
        //            ActionName = "Aadharservice.ValidateAadharOTP",
        //            SqlExecutableQuery = ""
        //        };
        //        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //    }
        //    finally
        //    {
        //        // UnitOfWork.Dispose();
        //    }
        //    return urldt;
        //}



        //[HttpPost("SendAadharOTP")]
        //public async Task<DataTable> SendAadharOTP(AadharCardServiceDataModel Model)
        //{
        //    var urldt = new System.Data.DataTable("tableName");
        //    urldt.Columns.Add("message", typeof(string));
        //    urldt.Columns.Add("status", typeof(int));
        //    urldt.Columns.Add("data", typeof(string));
        //    urldt.Columns.Add("optionMsg", typeof(string));
        //    try
        //    {
        //        var options = new RestClientOptions("https://rajkisan.rajasthan.gov.in")
        //        {
        //            MaxTimeout = -1,
        //        };
        //        var client = new RestClient(options);
        //        var request = new RestRequest("/Service/ChatBotAppService", Method.Post);
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddHeader("Access-Control-Allow-Origin", "*");

        //        DataTable dataTable_Master = new DataTable();
        //        dataTable_Master = await _unitOfWork.CommonFunctionRepository.GetAadharCardServiceMaster();


        //        var body = new
        //        {
        //            obj = new { usrnm = dataTable_Master.Rows[0]["UserName"].ToString(), psw = dataTable_Master.Rows[0]["Password"].ToString(), AppType = "ChatbotAPIs", Aadhaar = Model.AadharNo, srvnm = "ChatbotAPIs", srvmethodnm = "SendOtpByAadharNoCB" }
        //        };

        //        request.AddJsonBody(body);
        //        RestResponse response = client.Execute(request);
        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            var response1 = JsonSerializer.Deserialize<List<ResponseDataModal>>(response.Content);

        //            if (response1 != null)
        //            {

        //                urldt.Rows.Add(new Object[]{
        //                     response1.FirstOrDefault().message,0,
        //                      response1.FirstOrDefault().data,"optionMsg" });
        //            }
        //            else
        //            {
        //                urldt.Rows.Add(new Object[]{
        //                        "Please try again",1,
        //                        "Please try again","optionMsg"
        //                        });
        //            }

        //        }
        //        else
        //        {
        //            urldt.Rows.Add(new Object[]{
        //                        "Please try again",1,
        //                        "Please try again","optionMsg"
        //                        });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        urldt.Rows.Add(new Object[]{
        //                        "Please try again",1,
        //                        ex.Message,"optionMsg"
        //                        });

        //        var errorDesc = new ErrorDescription
        //        {
        //            Message = ex.Message,
        //            PageName = "AadharCardService",
        //            ActionName = "Aadharservice.SendAadharOTP",
        //            SqlExecutableQuery = ""
        //        };
        //        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //    }
        //    return urldt;
        //}

        //[HttpPost("ValidateAadharOTP")]
        //public async Task<DataTable> ValidateAadharOTP(AadharCardServiceDataModel Model)
        //{
        //    var urldt = new System.Data.DataTable("tableName");
        //    urldt.Columns.Add("message", typeof(string));
        //    urldt.Columns.Add("status", typeof(int));
        //    urldt.Columns.Add("data", typeof(string));
        //    try
        //    {
        //        var options = new RestClientOptions("https://rajkisan.rajasthan.gov.in")
        //        {
        //            MaxTimeout = -1,
        //        };
        //        var client = new RestClient(options);
        //        var request = new RestRequest("/Service/ChatBotAppService", Method.Post);
        //        request.AddHeader("Content-Type", "application/json");

        //        DataTable dataTable_Master = new DataTable();
        //        dataTable_Master = await _unitOfWork.CommonFunctionRepository.GetAadharCardServiceMaster();

        //        var body = "{\"obj\":{\"usrnm\":\""+ dataTable_Master.Rows[0]["UserName"].ToString() + "\",\"psw\":\""+ dataTable_Master.Rows[0]["Password"].ToString() + "\",\"AppType\":\"ChatbotAPIs\",\"Aadhaar\":\"" + Model.AadharNo + "\",\"otp\":\"" + Model.OTP + "\",\"txn\":\"" + Model.TransactionNo + "\",\"srvnm\":\"ChatbotAPIs\",\"srvmethodnm\":\"VerifyAadhaarOTPCB\"}}";


        //        request.AddStringBody(body, DataFormat.Json);
        //        RestResponse response = client.Execute(request);


        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            var response1 = JsonSerializer.Deserialize<List<ResponseDataModal>>(response.Content);
        //            if (response1 != null)
        //            {

        //                urldt.Rows.Add(new Object[]{
        //                     response1.FirstOrDefault().message,0,
        //                      response1.FirstOrDefault().data });
        //            }
        //            else
        //            {
        //                urldt.Rows.Add(new Object[]{
        //                        "Please try again",1,
        //                        "Please try again"
        //                        });
        //            }

        //        }
        //        else
        //        {
        //            urldt.Rows.Add(new Object[]{
        //                        "Please try again",1,
        //                        "Please try again"
        //                        });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        urldt.Rows.Add(new Object[]{
        //                        "Please try again",1,
        //                        "Please try again"
        //                        });
        //        var errorDesc = new ErrorDescription
        //        {
        //            Message = ex.Message,
        //            PageName = "AadharCardService",
        //            ActionName = "Aadharservice.ValidateAadharOTP",
        //            SqlExecutableQuery = ""
        //        };
        //        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //    }
        //    return urldt;
        //}

    }
}
