using AutoMapper;
using Azure;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.EMMA;
using EmitraEmitraEncrytDecryptClient;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.EmitraPayment;
using Kaushal_Darpan.Models.RPPPayment;
using Kaushal_Darpan.Models.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Ocsp;
using RestSharp;
using System.Buffers;
using System.Data;
using System.Globalization;
using System.Net;
using System.Text;
using System.Transactions;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]

    public class EmitraPaymentServiceController : BaseController
    {
        public override string PageName => "EmitraPaymentServiceController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmitraPaymentServiceController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("EmitraPaymentITI")]
        public async Task<ApiResult<EmitraRequestDetailsModel>> EmitraPaymentITI(EmitraRequestDetailsModel Model)
        {
            ActionName = "EmitraPayment(EmitraRequestDetailsModel Model)";

            var requestDetailsModel = new ApiResult<EmitraRequestDetailsModel>();
            try
            {
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);

                if (EmitraServiceDetail == null)
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
                objEmitra.StudentID = Model.StudentID;
                objEmitra.SemesterID = Model.SemesterID;
                objEmitra.ExamStudentStatus = Model.ExamStudentStatus;
                objEmitra.StudentFeesTransactionItems = Model.StudentFeesTransactionItems;
                objEmitra.SSOID = Model.SsoID;
                objEmitra.IsEmitra = Model.IsKiosk;
                objEmitra.DepartmentID = Model.DepartmentID;
                objEmitra.UniqueServiceID = Model.ID;
                objEmitra.FeeFor = Model.FeeFor;
                if (Model.TransactionApplicationIDs != null)
                {
                    if (Model.TransactionApplicationIDs.Length > 0)
                    {
                        objEmitra.TransactionApplicationID = string.Join(',', Model.TransactionApplicationIDs);
                    }
                }
                var result = await _unitOfWork.CommonFunctionRepository.CreateEmitraTransationITI(objEmitra);
                _unitOfWork.SaveChanges();
                if (result.TransactionId > 0)
                {
                    PGRequestModel data = new PGRequestModel();
                    data.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;
                    Random rnd = new Random();
                    data.PRN = "KD" + rnd.Next(100000, 999999) + rnd.Next(100000, 999999);

                    data.REQTIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    data.AMOUNT = Convert.ToString(Model.Amount);
                    data.SUCCESSURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&SERVICEID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("NO") + "&UniqueServiceID=" + Model.ID.ToString();
                    data.FAILUREURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&SERVICEID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("YES") + "&UniqueServiceID=" + Model.ID.ToString();
                    data.USERNAME = Model.UserName;
                    data.USERMOBILE = Model.MobileNo;
                    data.COMMTYPE = EmitraServiceDetail.COMMTYPE;
                    data.OFFICECODE = EmitraServiceDetail.OFFICECODE;

                    data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("##", Model.Amount.ToString());

                    data.SERVICEID = EmitraServiceDetail.SERVICEID;
                    data.UDF1 = Convert.ToString(Model.ExamStudentStatus);

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
                            var UpdateStatus = await _unitOfWork.CommonFunctionRepository.CreateEmitraTransationITI(objEmitra);
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

        [HttpPost("EmitraExaminationBackToBackITI")]
        public async Task<ApiResult<EmitraRequestDetailsModel>> EmitraExaminationBackToBackITI(EmitraRequestDetailsModel Model)
        {
            ActionName = "EmitraApplicationPayment(EmitraRequestDetailsModel Model)";
            var requestDetailsModel = new ApiResult<EmitraRequestDetailsModel>();
            try
            {
                Model.IsKiosk = true;
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);
                if (EmitraServiceDetail == null)
                {
                    requestDetailsModel.Data = Model;
                    requestDetailsModel.State = EnumStatus.Error;
                    requestDetailsModel.ErrorMessage = "Service Id Not Mapped";
                    return requestDetailsModel;
                }
                Random rnd = new Random();
                string prnNo = "ITI" + rnd.Next(100000, 999999) + rnd.Next(100000, 999999);
                EmitraTransactionsModel objEmitra = new EmitraTransactionsModel();
                objEmitra.key = "_InsertDetails";
                objEmitra.ApplicationIdEnc = Model.ApplicationIdEnc;
                //objEmitra.Amount = Model.Amount;
                objEmitra.Amount = Model.Amount + Model.ProcessingFee + Model.FormCommision;
                objEmitra.StudentID = Model.StudentID;
                objEmitra.SemesterID = Model.SemesterID;
                objEmitra.SSOID = Model.SsoID;
                objEmitra.IsEmitra = Model.IsKiosk;
                objEmitra.FeeFor = Model.FeeFor;
                objEmitra.DepartmentID = Model.DepartmentID;
                objEmitra.PRN = prnNo;
                objEmitra.ServiceID = Model.ServiceID;
                objEmitra.KioskID = Model.KIOSKCODE;
                objEmitra.UniqueServiceID = Model.ID;
                objEmitra.ExamStudentStatus = Model.ExamStudentStatus;
                objEmitra.StudentFeesTransactionItems = Model.StudentFeesTransactionItems ?? objEmitra.StudentFeesTransactionItems;
                var result = await _unitOfWork.CommonFunctionRepository.CreateEmitraTransationITI(objEmitra);
                _unitOfWork.SaveChanges();
                if (result.TransactionId > 0)
                {
                    PGRequestModel data = new PGRequestModel();
                    data.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;

                    data.PRN = prnNo;
                    data.REQUESTID = Convert.ToString(result.TransactionId);
                    data.REQTIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    data.AMOUNT = Convert.ToString(objEmitra.Amount);
                    data.USERNAME = Model.UserName;
                    data.USERMOBILE = Model.MobileNo;
                    data.COMMTYPE = EmitraServiceDetail.COMMTYPE;
                    data.OFFICECODE = EmitraServiceDetail.OFFICECODE;
                    data.SUBSERVICEID = "";
                    data.CONSUMERKEY = prnNo;
                    data.CONSUMERNAME = Model.UserName;
                    data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("##", Model.Amount.ToString());
                    //data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("{H1}", Model.Amount.ToString()).Replace("{H2}", Model.ProcessingFee.ToString());

                    //data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("{application_fee}", Model.Amount.ToString())
                    //    .Replace("{processing_fee}", Model.ProcessingFee.ToString()).Replace("{form_commission}", Convert.ToString(Model.FormCommision));


                    data.SERVICEID = EmitraServiceDetail.SERVICEID;
                    data.UDF1 = "";
                    data.UDF2 = Model.SsoID;
                    data.USEREMAIL = Model.USEREMAIL;
                    data.SSOTOKEN = Model.SSoToken;
                    data.SSOID = Model.SsoID;

                    // create checksum
                    var dRequestChecksum = new DRequestChecksum
                    {
                        SSOID = data.SSOID,
                        REQUESTID = data.REQUESTID,
                        REQTIMESTAMP = data.REQTIMESTAMP,
                        SSOTOKEN = data.SSOTOKEN,
                    };
                    data.CHECKSUM = CommonFuncationHelper.CreateMD5(JsonConvert.SerializeObject(dRequestChecksum));
                    string retVal = ThirdPartyServiceHelper.MakeEmitraTransactionsEncrypted(EmitraServiceDetail.ServiceURL, JsonConvert.SerializeObject(data), EmitraServiceDetail.EncryptionKey);
                    string decVal = EmitraHelper.Decrypt(retVal, EmitraServiceDetail.EncryptionKey);
                    DResponse resp = JsonConvert.DeserializeObject<DResponse>(decVal);
                    if (resp != null)
                    {
                        try
                        {
                            objEmitra.key = "_UpdateEmitraPaymentStatus";
                            objEmitra.ResponseString = JsonConvert.SerializeObject(resp);
                            objEmitra.RequestString = JsonConvert.SerializeObject(data);
                            objEmitra.TransactionId = Convert.ToInt32(result.TransactionId);
                            objEmitra.ServiceID = EmitraServiceDetail.SERVICEID;
                            objEmitra.PRN = data.PRN;
                            objEmitra.TransactionNo = Convert.ToString(resp.TRANSACTIONID);
                            objEmitra.PaidAmount = Convert.ToDecimal(resp.TRANSAMT);
                            objEmitra.RequestStatus = Convert.ToString(resp.TRANSACTIONSTATUS);
                            objEmitra.StatusMsg = Convert.ToString(resp.MSG);
                            objEmitra.ReceiptNo = Convert.ToString(resp.RECEIPTNO);
                            var UpdateStatus = await _unitOfWork.CommonFunctionRepository.CreateEmitraTransationITI(objEmitra);
                            _unitOfWork.SaveChanges();
                            if (resp.TRANSACTIONSTATUS.Contains("SUCCESS"))
                            {
                                requestDetailsModel.State = EnumStatus.Success;
                                requestDetailsModel.Message = resp.MSG;
                                requestDetailsModel.PDFURL = resp.RECEIPT_URL;
                            }
                            else
                            {
                                requestDetailsModel.State = EnumStatus.Error;
                                requestDetailsModel.Message = resp.MSG;
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
                    }
                    else
                    {
                        requestDetailsModel.State = EnumStatus.Error;
                        requestDetailsModel.Message = "something went wrong!";
                    }

                }
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                requestDetailsModel.State = EnumStatus.Error;
                requestDetailsModel.Message = "Something went wrong";
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




        #region "Emitra PAYMENT INTEGRAION"
        [HttpPost("EmitraPayment")]
        public async Task<ApiResult<EmitraRequestDetailsModel>> EmitraPayment(EmitraRequestDetailsModel Model)
        {
            ActionName = "EmitraPayment(EmitraRequestDetailsModel Model)";

            var requestDetailsModel = new ApiResult<EmitraRequestDetailsModel>();
            try
            {
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);

                if (EmitraServiceDetail == null)
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
                objEmitra.EnrollFeeAmount = (Model.EnrollFeeAmount ?? 0);
                objEmitra.StudentID = Model.StudentID;
                objEmitra.SemesterID = Model.SemesterID;
                objEmitra.ExamStudentStatus = Model.ExamStudentStatus;
                objEmitra.StudentFeesTransactionItems = Model.StudentFeesTransactionItems;
                objEmitra.SSOID = Model.SsoID;
                objEmitra.IsEmitra = Model.IsKiosk;
                objEmitra.DepartmentID = Model.DepartmentID;
                objEmitra.UniqueServiceID = Model.ID;
                objEmitra.FeeFor = Model.FeeFor;
                if (Model.TransactionApplicationIDs != null)
                {
                    if (Model.TransactionApplicationIDs.Length > 0)
                    {
                        objEmitra.TransactionApplicationID = string.Join(',', Model.TransactionApplicationIDs);
                    }
                }
                var result = await _unitOfWork.CommonFunctionRepository.CreateEmitraTransation(objEmitra);
                _unitOfWork.SaveChanges();

                if (result.TransactionId > 0)
                {
                    PGRequestModel data = new PGRequestModel();
                    data.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;
                    Random rnd = new Random();
                    data.PRN = "KD" + rnd.Next(100000, 999999) + rnd.Next(100000, 999999);

                    data.REQTIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    data.AMOUNT = Convert.ToString(Model.Amount);

                    if (Model.FeeFor== "EnrollmentFee")
                    {
                        data.ExamFeeAmount = Convert.ToString(Model.Amount);
                        data.EnrollFeeAmount = Convert.ToString(Model.EnrollFeeAmount);
                        decimal totalAmount = (decimal)(Model.Amount) + (decimal)(Model.EnrollFeeAmount ?? 0);

                        data.AMOUNT = totalAmount.ToString();
                        data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("##", data.AMOUNT.ToString());
                        data.CHECKSUM = CommonFuncationHelper.CreateMD5(data.PRN + "|" + data.AMOUNT.ToString() + "|" + EmitraServiceDetail.CHECKSUMKEY);
                    }
                    else
                    {
                        data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("##", Model.Amount.ToString());
                    }

                    data.CHECKSUM = CommonFuncationHelper.CreateMD5(data.PRN + "|" + data.AMOUNT.ToString() + "|" + EmitraServiceDetail.CHECKSUMKEY);

                    data.SUCCESSURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&SERVICEID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("NO") + "&UniqueServiceID=" + Model.ID.ToString();
                    data.FAILUREURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&SERVICEID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("YES") + "&UniqueServiceID=" + Model.ID.ToString();
                    data.USERNAME = Model.UserName;
                    data.USERMOBILE = Model.MobileNo;
                    data.COMMTYPE = EmitraServiceDetail.COMMTYPE;
                    data.OFFICECODE = EmitraServiceDetail.OFFICECODE;
                    
                    data.SERVICEID = EmitraServiceDetail.SERVICEID;
                    data.UDF1 = Convert.ToString(Model.ExamStudentStatus);

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





        [HttpPost("GetTransactionStatus")]
        public async Task<ApiResult<EmitraResponseParametersModel>> GetTransactionStatus(RPPTransactionStatusDataModel Model)
        {
            ActionName = "GetTransactionStatus(RPPTransactionStatusDataModel Model)";

            var result = new ApiResult<EmitraResponseParametersModel>();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //get payment details form database
                EmitraRequestDetailsModel dataModel = new EmitraRequestDetailsModel();
                dataModel.ServiceID = Model.ServiceID;
                dataModel.DepartmentID = Model.DepartmentID;
                dataModel.ID = Model.ID;
                var data = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(dataModel);
                if (data == null)
                {
                    result.State = EnumStatus.Error;
                    result.Data = new EmitraResponseParametersModel();
                    result.Message = "Payment Integrations Details Not Found.!";
                    return result;
                }

                var d = data.VerifyURL + "?MERCHANTCODE=" + data.MERCHANTCODE + "&SERVICEID=" + data.SERVICEID + "&PRN=" + Model.PRN + "";
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(d);
                webrequest.Method = "POST";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.ContentLength = 0;

                Stream stream = webrequest.GetRequestStream();
                stream.Close();
                string RESPONSEJSON;
                using (WebResponse response = webrequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        RESPONSEJSON = reader.ReadToEnd();
                        EmitraResponseParametersModel RESPONSEPARAMS = JsonConvert.DeserializeObject<EmitraResponseParametersModel>(RESPONSEJSON);
                        dynamic parsedResponse = JsonConvert.DeserializeObject(RESPONSEJSON);
                        if (parsedResponse != null)
                        {
                            string STATUS = parsedResponse.STATUS;
                            string RESPONSECODE = parsedResponse.RESPONSECODE;

                            if (STATUS == "SUCCESS" || STATUS == "FAILED" || STATUS == "PENDING")
                            {
                                result.State = EnumStatus.Success;
                                result.Data = RESPONSEPARAMS;
                                result.Message = RESPONSEPARAMS.RESPONSEMESSAGE;
                                //Update Database
                                RESPONSEPARAMS.TransactionNo = RESPONSEPARAMS.TRANSACTIONID;
                                RESPONSEPARAMS.ApplicationIdEnc = Model.ApplicationID;
                                RESPONSEPARAMS.TRANSACTIONID = Model.TransactionID;
                                RESPONSEPARAMS.ExamStudentStatus = Convert.ToString(Model.ExamStudentStatus);
                                await _unitOfWork.CommonFunctionRepository.UpdateEmitraPaymentStatus(RESPONSEPARAMS);
                                _unitOfWork.SaveChanges();
                            }
                            else
                            {
                                result.State = EnumStatus.Error;
                                result.Data = RESPONSEPARAMS;
                                result.Message = RESPONSEPARAMS.RESPONSEMESSAGE;
                                ////Update Database 
                                //no need this code
                                RESPONSEPARAMS.TransactionNo = RESPONSEPARAMS.TRANSACTIONID;
                                RESPONSEPARAMS.ApplicationIdEnc = Model.ApplicationID;
                                RESPONSEPARAMS.TRANSACTIONID = Model.TransactionID;
                                RESPONSEPARAMS.ExamStudentStatus = Convert.ToString(Model.ExamStudentStatus);
                                await _unitOfWork.CommonFunctionRepository.UpdateEmitraPaymentStatus(RESPONSEPARAMS);
                                _unitOfWork.SaveChanges();
                            }
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Message = "Invalid response received.";
                        }

                    }
                }
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.Message = ex.Message;
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



        [HttpPost("EmitraCheckPaymentStatus")]
        public async Task<ApiResult<EmitraRequestDetailsModel>> EmitraCheckPaymentStatus(EmitraRequestDetailsModel Model)
        {
            ActionName = "EmitraCheckPaymentStatus(EmitraRequestDetailsModel Model)";
            EmitraResponseParametersModel objRespose = new EmitraResponseParametersModel();
            var requestDetailsModel = new ApiResult<EmitraRequestDetailsModel>();
            try

            {
                Model.ServiceID = "5442";
                Model.PRN = "KD860485539641";
                Model.TransactionID = "24";
                Model.Amount = Convert.ToDecimal("5000");

                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);
                if (EmitraServiceDetail.REVENUEHEAD == null)
                {
                    requestDetailsModel.Data = Model;
                    requestDetailsModel.State = EnumStatus.Error;
                    requestDetailsModel.ErrorMessage = "Service Id Not Mapped";
                    return requestDetailsModel;
                }

                if (!string.IsNullOrEmpty(EmitraServiceDetail.MERCHANTCODE) && Model.UserType == "C")
                {
                    string pgverifyurl = EmitraServiceDetail.VerifyURL + "?MERCHANTCODE=" + EmitraServiceDetail.MERCHANTCODE + "&SERVICEID=" + EmitraServiceDetail.SERVICEID + "&PRN=" + Model.PRN + "";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pgverifyurl);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Method = "POST";
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Close();
                    HttpWebResponse response;
                    response = (HttpWebResponse)request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    string retVal = new StreamReader(responseStream).ReadToEnd();
                    objRespose = JsonConvert.DeserializeObject<EmitraResponseParametersModel>(retVal);
                }
                else
                {
                    EmitraVerifyRequest req = new EmitraVerifyRequest();
                    req.SSOTOKEN = "0";
                    req.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;
                    req.REQUESTID = Model.TransactionID;
                    req.SERVICEID = EmitraServiceDetail.SERVICEID;
                    req.CHECKSUM = CommonFuncationHelper.CreateMD5(Model.PRN + "|" + Model.Amount + "|" + EmitraServiceDetail.CHECKSUMKEY);
                    object retVal = MakeTransactionsEncrypted(EmitraServiceDetail.VerifyURL, JsonConvert.SerializeObject(req), EmitraServiceDetail.EncryptionKey, EmitraServiceDetail.WebServiceURL);
                    //Decript String


                    EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();
                    EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, EmitraServiceDetail.WebServiceURL);
                    EmitraDecriptStringResponse response = await emitraencsev.EmitraDecriptStringAsync(EmitraServiceDetail.EncryptionKey, Convert.ToString(retVal));
                    objRespose = JsonConvert.DeserializeObject<EmitraResponseParametersModel>(response.Body.EmitraDecriptStringResult);
                }

                if (objRespose != null)
                {
                    EmitraTransactionsModel objEmitra = new EmitraTransactionsModel();
                    try

                    {
                        objEmitra.key = "_EmitraCheckPaymentStatus";
                        objEmitra.ResponseString = JsonConvert.SerializeObject(objRespose);
                        objEmitra.TransactionId = string.IsNullOrEmpty(Model.TransactionID) == true ? 0 : Convert.ToInt32(Model.TransactionID);
                        objEmitra.ServiceID = EmitraServiceDetail.SERVICEID;
                        objEmitra.PRN = Model.PRN;
                        var UpdateStatus = await _unitOfWork.CommonFunctionRepository.CreateEmitraTransation(objEmitra);
                        _unitOfWork.SaveChanges();
                    }
                    catch (System.Exception ex)
                    {
                        requestDetailsModel.Data = Model;
                        requestDetailsModel.State = EnumStatus.Error;
                        requestDetailsModel.ErrorMessage = ex.Message;
                    }
                }


                requestDetailsModel.Data = Model;
                requestDetailsModel.State = EnumStatus.Success;
                requestDetailsModel.Message = "successfully .!";

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

        [HttpGet("GetPreviewPaymentDetails/{CollegeID}")]
        public async Task<ApiResult<List<RPPResponseParametersModel>>> GetPreviewPaymentDetails(int CollegeID)
        {
            var result = new ApiResult<List<RPPResponseParametersModel>>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetPreviewPaymentDetails(CollegeID));
                result.State = EnumStatus.Success;
                if (result.Data.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
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

        [HttpPost("EmitraPaymentResponse")] //IActionResult
        public async Task<IActionResult> EmitraPaymentResponse(string UniquerequestId = "", string ApplicationIdEnc = "", string ServiceID = "", string IsFailed = "", string UniqueServiceID = "")
        {
            var RetrunUrL = "";
            try
            {
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');

                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');
                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');
                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');

                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');

                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');

                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');

                EmitraRequestDetailsModel Model = new EmitraRequestDetailsModel();
                Model.ServiceID = ServiceID;
                Model.ID = string.IsNullOrEmpty(UniqueServiceID) == true ? 0 : Convert.ToInt32(UniqueServiceID);
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);

                var data = Convert.ToString(Request.Form["encData"]);

                var vIsFailed = CommonFuncationHelper.EmitraDecrypt(IsFailed);

                EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();

                EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, EmitraServiceDetail.WebServiceURL);
                EmitraDecriptStringResponse response = await emitraencsev.EmitraDecriptStringAsync(EmitraServiceDetail.EncryptionKey, data);
                var EmitraResponseData = JsonConvert.DeserializeObject<EmitraResponseParametersModel>(response.Body.EmitraDecriptStringResult);

                if (EmitraResponseData != null)
                {
                    EmitraResponseData.ApplicationIdEnc = CommonFuncationHelper.EmitraDecrypt(ApplicationIdEnc);
                    EmitraResponseData.TRANSACTIONID = CommonFuncationHelper.EmitraDecrypt(UniquerequestId);
                    EmitraResponseData.ExamStudentStatus = EmitraResponseData.UDF1;
                    await _unitOfWork.CommonFunctionRepository.UpdateEmitraPaymentStatus(EmitraResponseData);
                    _unitOfWork.SaveChanges();
                }

                if (vIsFailed == "NO")
                {
                    RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData.PRN);
                }
                else
                {
                    RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData.PRN);
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

            //return response;
            return new RedirectResult(RetrunUrL);
        }

        [HttpGet("GetEmitraTransactionDetails/{TransID}")]
        public async Task<ApiResult<DataTable>> GetEmitraTransactionDetails(string TransID)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetEmitraTransactionDetails(TransID));
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


        [HttpGet("GetEmitraITITransactionDetails/{TransID}")]
        public async Task<ApiResult<DataTable>> GetEmitraITITransactionDetails(string TransID)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetEmitraITITransactionDetails(TransID));
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





        [HttpGet("GetEmitraApplicationTransactionDetails/{TransID}")]
        public async Task<ApiResult<DataTable>> GetEmitraApplicationTransactionDetails(string TransID)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetEmitraApplicationTransactionDetails(TransID));
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

        [HttpPost("GetEmitraTransactionStatus")]
        public async Task<ApiResult<RPPResponseParametersModel>> GetEmitraTransactionStatus(RPPTransactionStatusDataModel Model)
        {
            ActionName = "GetTransactionStatus(RPPTransactionStatusDataModel Model)";

            var result = new ApiResult<RPPResponseParametersModel>();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //get payment details form database
                RPPPaymentGatewayDataModel dataModel = new RPPPaymentGatewayDataModel();
                dataModel.PaymentGateway = (int)EnmPaymentGatway.RPP;
                dataModel.DepartmentID = Model.DepartmentID;
                var data = await _unitOfWork.CommonFunctionRepository.RPPGetpaymentGatewayDetails(dataModel);
                data.VerificationURL = "http://emitrauat.rajasthan.gov.in/webServicesRepositoryUat/newAggrTransVerify";
                if (string.IsNullOrWhiteSpace(data.MerchantCode))
                {
                    result.State = EnumStatus.Error;
                    result.Data = new RPPResponseParametersModel();
                    result.ErrorMessage = "Payment Integrations Details Not Found.!";
                    return result;
                }

                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(data.VerificationURL + "?MERCHANTCODE=" + data.MerchantCode + "&&PRN=" + Model.PRN + "&&AMOUNT=" + Model.AMOUNT);
                webrequest.Method = "POST";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.ContentLength = 0;

                Stream stream = webrequest.GetRequestStream();
                stream.Close();
                string RESPONSEJSON;
                using (WebResponse response = webrequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        RESPONSEJSON = reader.ReadToEnd();
                        RPPResponseParametersModel RESPONSEPARAMS = JsonConvert.DeserializeObject<RPPResponseParametersModel>(RESPONSEJSON);
                        dynamic stuff = JsonConvert.DeserializeObject(RESPONSEJSON);
                        string STATUS = stuff.STATUS;
                        string RESPONSECODE = stuff.RESPONSECODE;
                        if (STATUS == "SUCCESS")
                        {
                            result.State = EnumStatus.Success;
                            result.Data = RESPONSEPARAMS;
                            result.Message = RESPONSEPARAMS.RESPONSEMESSAGE;

                            //Update Database
                            RPPPaymentResponseModel objresponse = new RPPPaymentResponseModel();
                            objresponse.STATUS = RESPONSEPARAMS.STATUS;
                            objresponse.RESPONSEJSON = RESPONSEJSON;
                            objresponse.RESPONSEPARAMETERS = RESPONSEPARAMS;
                            await _unitOfWork.CommonFunctionRepository.RPPSaveData(objresponse);
                            _unitOfWork.SaveChanges();
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Data = RESPONSEPARAMS;
                            result.ErrorMessage = RESPONSEPARAMS.RESPONSEMESSAGE;


                            //Update Database
                            RPPPaymentResponseModel objresponse = new RPPPaymentResponseModel();
                            objresponse.STATUS = RESPONSEPARAMS.STATUS;
                            objresponse.RESPONSEJSON = RESPONSEJSON;
                            objresponse.RESPONSEPARAMETERS = RESPONSEPARAMS;
                            await _unitOfWork.CommonFunctionRepository.RPPSaveData(objresponse);
                            _unitOfWork.SaveChanges();
                        }
                    }
                }

            }
            catch (System.Exception ex)
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


        [HttpPost("GetTransactionDetailsActionWise")]
        public async Task<ApiResult<DataTable>> GetTransactionDetailsActionWise(StudentSearchModel model)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetTransactionDetailsActionWise(model));
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
        #endregion


        #region "ITI Student Payment Response "

        [HttpPost("ITIStudentPaymentResponse")] //IActionResult
        public async Task<IActionResult> ITIStudentPaymentResponse(string UniquerequestId = "", string ApplicationIdEnc = "", string ServiceID = "", string IsFailed = "", string UniqueServiceID = "")
        {
            var RetrunUrL = "";
            try
            {
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');

                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');
                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');
                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');

                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');

                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');


                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');




                EmitraRequestDetailsModel Model = new EmitraRequestDetailsModel();
                Model.ServiceID = ServiceID;
                Model.ID = string.IsNullOrEmpty(UniqueServiceID) == true ? 0 : Convert.ToInt32(UniqueServiceID);
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);

                var data = Convert.ToString(Request.Form["encData"]);

                var vIsFailed = CommonFuncationHelper.EmitraDecrypt(IsFailed);

                EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();

                EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, EmitraServiceDetail.WebServiceURL);
                EmitraDecriptStringResponse response = await emitraencsev.EmitraDecriptStringAsync(EmitraServiceDetail.EncryptionKey, data);
                var EmitraResponseData = JsonConvert.DeserializeObject<EmitraResponseParametersModel>(response.Body.EmitraDecriptStringResult);

                if (EmitraResponseData != null)
                {
                    EmitraResponseData.ApplicationIdEnc = CommonFuncationHelper.EmitraDecrypt(ApplicationIdEnc);
                    EmitraResponseData.TRANSACTIONID = CommonFuncationHelper.EmitraDecrypt(UniquerequestId);
                    EmitraResponseData.ExamStudentStatus = EmitraResponseData.UDF1;
                    await _unitOfWork.CommonFunctionRepository.UpdateITIEmitraPaymentStatus(EmitraResponseData);
                    _unitOfWork.SaveChanges();
                }

                if (vIsFailed == "NO")
                {
                    RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData?.PRN);
                }
                else
                {
                    RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData?.PRN);
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

            //return response;
            return new RedirectResult(RetrunUrL);
        }


        #endregion

        #region "Emitra PAYMENT INTEGRAION APPLICATION"
        [HttpPost("EmitraApplicationPayment")]
        public async Task<ApiResult<EmitraRequestDetailsModel>> EmitraApplicationPayment(EmitraRequestDetailsModel Model)
        {
            ActionName = "EmitraApplicationPayment(EmitraRequestDetailsModel Model)";
            var requestDetailsModel = new ApiResult<EmitraRequestDetailsModel>();
            try
            {
                Model.IsKiosk = true;
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);
                if (EmitraServiceDetail == null)
                {
                    requestDetailsModel.Data = Model;
                    requestDetailsModel.State = EnumStatus.Error;
                    requestDetailsModel.ErrorMessage = "Service Id Not Mapped";
                    return requestDetailsModel;
                }
                Random rnd = new Random();
                string prnNo = "KD" + rnd.Next(100000, 999999) + rnd.Next(100000, 999999);
                EmitraTransactionsModel objEmitra = new EmitraTransactionsModel();
                objEmitra.key = "_InsertDetails";
                objEmitra.ApplicationIdEnc = Model.ApplicationIdEnc;
                //objEmitra.Amount = Model.Amount;
                objEmitra.Amount = Model.Amount + Model.ProcessingFee + Model.FormCommision;
                objEmitra.StudentID = Model.StudentID;
                objEmitra.SemesterID = Model.SemesterID;
                objEmitra.SSOID = Model.SsoID;
                objEmitra.IsEmitra = Model.IsKiosk;
                objEmitra.FeeFor = Model.FeeFor;
                objEmitra.DepartmentID = Model.DepartmentID;
                objEmitra.PRN = prnNo;
                objEmitra.ServiceID = Model.ServiceID;
                objEmitra.KioskID = Model.KIOSKCODE;
                var result = await _unitOfWork.CommonFunctionRepository.CreateEmitraApplicationTransation(objEmitra);
                _unitOfWork.SaveChanges();
                if (result.TransactionId > 0)
                {
                    PGRequestModel data = new PGRequestModel();
                    data.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;

                    data.PRN = prnNo;
                    data.REQUESTID = Convert.ToString(result.TransactionId);
                    data.REQTIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    data.AMOUNT = Convert.ToString(objEmitra.Amount);
                    //data.SUCCESSURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&ServiceID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("NO") + "&UniqueServiceID=" + Model.ID.ToString();
                    // data.FAILUREURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&ServiceID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("YES") + "&UniqueServiceID=" + Model.ID.ToString();
                    data.USERNAME = Model.UserName;
                    data.USERMOBILE = Model.MobileNo;
                    data.COMMTYPE = EmitraServiceDetail.COMMTYPE;
                    data.OFFICECODE = EmitraServiceDetail.OFFICECODE;
                    data.SUBSERVICEID = "";
                    data.CONSUMERKEY = prnNo;
                    data.CONSUMERNAME = Model.UserName;
                    // data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("##", Model.Amount.ToString());
                    //data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("{H1}", Model.Amount.ToString()).Replace("{H2}", Model.ProcessingFee.ToString());

                    data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("{application_fee}", Model.Amount.ToString())
                        .Replace("{processing_fee}", Model.ProcessingFee.ToString()).Replace("{form_commission}", Convert.ToString(Model.FormCommision));


                    data.SERVICEID = EmitraServiceDetail.SERVICEID;
                    data.UDF1 = "";
                    data.UDF2 = Model.SsoID;
                    data.USEREMAIL = Model.USEREMAIL;
                    data.SSOTOKEN = Model.SSoToken;
                    data.SSOID = Model.SsoID;

                    // create checksum
                    var dRequestChecksum = new DRequestChecksum
                    {
                        SSOID = data.SSOID,
                        REQUESTID = data.REQUESTID,
                        REQTIMESTAMP = data.REQTIMESTAMP,
                        SSOTOKEN = data.SSOTOKEN,
                    };
                    data.CHECKSUM = CommonFuncationHelper.CreateMD5(JsonConvert.SerializeObject(dRequestChecksum));




                    // data.CHECKSUM = CommonFuncationHelper.CreateMD5(data.PRN + "|" + data.AMOUNT + "|" + EmitraServiceDetail.CHECKSUMKEY);

                    //EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();

                    //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                    //EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, EmitraServiceDetail.WebServiceURL);
                    //EmitraEncryptStringResponse response = await emitraencsev.EmitraEncryptStringAsync(EmitraServiceDetail.EncryptionKey, JsonConvert.SerializeObject(data));


                    string retVal = ThirdPartyServiceHelper.MakeEmitraTransactionsEncrypted(EmitraServiceDetail.ServiceURL, JsonConvert.SerializeObject(data), EmitraServiceDetail.EncryptionKey);

                    string decVal = EmitraHelper.Decrypt(retVal, EmitraServiceDetail.EncryptionKey);
                    DResponse resp = JsonConvert.DeserializeObject<DResponse>(decVal);

                    if (resp != null)
                    {
                        try
                        {

                            //{ "REQUESTID":"64","TRANSACTIONSTATUSCODE":"200","RECEIPTNO":"25677939790","TRANSACTIONID":"250696960263",
                            //        "TRANSAMT":"80.0000",
                            //        "REMAININGWALLET":"867219.30","EMITRATIMESTAMP":"20250513232840000",
                            //        "TRANSACTIONSTATUS":"SUCCESS","MSG":"Transaction Successfully Done",
                            //        "RECEIPT_URL":"https://emitraapp.rajasthan.gov.in/emitrashared1/document/RECEIPT_ONLY/13-05-2025/RECEIPTNO_25677939790.pdf",
                            //        "CHECKSUM":"6c139b18094fa60b84bc28be31cc627b"}

                            objEmitra.key = "_UpdateEmitraPaymentStatus";
                            objEmitra.ResponseString = JsonConvert.SerializeObject(resp);
                            objEmitra.RequestString = JsonConvert.SerializeObject(data);
                            objEmitra.TransactionId = Convert.ToInt32(resp.REQUESTID);
                            objEmitra.ServiceID = EmitraServiceDetail.SERVICEID;
                            objEmitra.PRN = data.PRN;
                            objEmitra.TransactionNo = Convert.ToString(resp.TRANSACTIONID);
                            objEmitra.PaidAmount = Convert.ToDecimal(resp.TRANSAMT);
                            objEmitra.RequestStatus = Convert.ToString(resp.TRANSACTIONSTATUS);
                            objEmitra.StatusMsg = Convert.ToString(resp.MSG);
                            objEmitra.ReceiptNo = Convert.ToString(resp.RECEIPTNO);


                            var UpdateStatus = await _unitOfWork.CommonFunctionRepository.CreateEmitraApplicationTransation(objEmitra);
                            _unitOfWork.SaveChanges();

                            if (resp.TRANSACTIONSTATUS.Contains("SUCCESS"))
                            {
                                requestDetailsModel.State = EnumStatus.Success;
                                requestDetailsModel.Message = resp.MSG;
                                requestDetailsModel.PDFURL = resp.RECEIPT_URL;
                            }
                            else
                            {
                                requestDetailsModel.State = EnumStatus.Error;
                                requestDetailsModel.Message = resp.MSG;
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
                    }
                    else
                    {
                        requestDetailsModel.State = EnumStatus.Error;
                        requestDetailsModel.Message = "something went wrong!";


                    }

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

        [HttpPost("EmitraApplicationPaymentResponse")] //IActionResult
        public async Task<IActionResult> EmitraApplicationPaymentResponse(string UniquerequestId = "", string ApplicationIdEnc = "", string ServiceID = "", string IsFailed = "", string UniqueServiceID = "")
        {
            var RetrunUrL = "";
            try
            {
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');

                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');
                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');
                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');

                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');

                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');

                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');

                EmitraRequestDetailsModel Model = new EmitraRequestDetailsModel();
                Model.ServiceID = ServiceID;
                Model.ID = string.IsNullOrEmpty(UniqueServiceID) == true ? 0 : Convert.ToInt32(UniqueServiceID);
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);

                var data = Convert.ToString(Request.Form["encData"]);

                var vIsFailed = CommonFuncationHelper.EmitraDecrypt(IsFailed);

                EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();

                EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, EmitraServiceDetail.WebServiceURL);
                EmitraDecriptStringResponse response = await emitraencsev.EmitraDecriptStringAsync(EmitraServiceDetail.EncryptionKey, data);
                var EmitraResponseData = JsonConvert.DeserializeObject<EmitraResponseParametersModel>(response.Body.EmitraDecriptStringResult);

                if (EmitraResponseData != null)
                {
                    EmitraResponseData.ApplicationIdEnc = CommonFuncationHelper.EmitraDecrypt(ApplicationIdEnc);
                    EmitraResponseData.TRANSACTIONID = CommonFuncationHelper.EmitraDecrypt(UniquerequestId);
                    EmitraResponseData.ExamStudentStatus = EmitraResponseData.UDF1;
                    await _unitOfWork.CommonFunctionRepository.UpdateEmitraApplicationPaymentStatus(EmitraResponseData);
                    _unitOfWork.SaveChanges();
                }

                // EmitraServiceDetail.SuccessFailedURL = "http://localhost:4200/ApplicationPaymentStatus";


                if (vIsFailed == "NO")
                {
                    RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData.PRN);
                }
                else
                {
                    RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData.PRN);
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

            //return response;
            return new RedirectResult(RetrunUrL);
        }


        [HttpPost("EmitraApplicationPaymentNew")]
        public async Task<ApiResult<EmitraRequestDetailsModel>> EmitraApplicationPaymentNew(EmitraRequestDetailsModel Model)
        {
            ActionName = "EmitraApplicationPaymentNew(EmitraRequestDetailsModel Model)";
            var requestDetailsModel = new ApiResult<EmitraRequestDetailsModel>();
            try
            {

                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);
                if (EmitraServiceDetail == null)
                {
                    requestDetailsModel.Data = Model;
                    requestDetailsModel.State = EnumStatus.Error;
                    requestDetailsModel.ErrorMessage = "Service Id Not Mapped";
                    return requestDetailsModel;
                }
                EmitraTransactionsModel objEmitra = new EmitraTransactionsModel();
                objEmitra.key = "_InsertDetails";
                objEmitra.ApplicationIdEnc = Model.ApplicationIdEnc;
                objEmitra.Amount = Model.Amount + Model.ProcessingFee + Model.FormCommision;
                objEmitra.StudentID = Model.StudentID;
                objEmitra.SemesterID = Model.SemesterID;
                objEmitra.SSOID = Model.SsoID;
                objEmitra.IsEmitra = Model.IsKiosk;
                objEmitra.FeeFor = Model.FeeFor;
                objEmitra.DepartmentID = Model.DepartmentID;
                objEmitra.KioskID = Model.KIOSKCODE;
                var result = await _unitOfWork.CommonFunctionRepository.CreateEmitraApplicationTransation(objEmitra);
                _unitOfWork.SaveChanges();
                if (result.TransactionId > 0)
                {
                    PGRequestModel data = new PGRequestModel();
                    data.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;
                    Random rnd = new Random();
                    data.PRN = "KD" + rnd.Next(100000, 999999) + rnd.Next(100000, 999999);
                    data.REQTIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    data.AMOUNT = Convert.ToString(objEmitra.Amount);

                    data.SUCCESSURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&ServiceID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("NO") + "&UniqueServiceID=" + Model.ID.ToString();
                    data.FAILUREURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&ApplicationIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.ApplicationIdEnc) + "&ServiceID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("YES") + "&UniqueServiceID=" + Model.ID.ToString();

                    data.USERNAME = Model.UserName.Trim();
                    data.USERMOBILE = Model.MobileNo;
                    data.COMMTYPE = EmitraServiceDetail.COMMTYPE;
                    data.OFFICECODE = EmitraServiceDetail.OFFICECODE;
                    // data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("##", Model.Amount.ToString());
                    data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("{application_fee}", Model.Amount.ToString())
                        .Replace("{processing_fee}", Model.ProcessingFee.ToString()).Replace("{form_commission}", Convert.ToString(Model.FormCommision));

                    data.SERVICEID = EmitraServiceDetail.SERVICEID;
                    data.UDF1 = Convert.ToString(result.TransactionId);
                    data.UDF2 = Convert.ToString(Model.DirectAdmission);
                    data.USEREMAIL = Model.USEREMAIL.Trim();
                    data.CONSUMERKEY = data.PRN + "-" + Model.ApplicationIdEnc;
                    data.CHANNEL = "ONLINE";
                    data.LOOKUPID = "";
                    data.SSOTOKEN = Model.SSoToken;

                    // Create checksum input string
                    string checksumRaw = data.MERCHANTCODE + data.SERVICEID + data.PRN + "ONLINE" +
                    data.REQTIMESTAMP + data.AMOUNT + data.SUCCESSURL +
                    data.FAILUREURL + data.USERNAME + data.USERMOBILE +
                    data.USEREMAIL + data.CONSUMERKEY + data.OFFICECODE +
                    data.REVENUEHEAD + data.UDF1 + data.UDF2 + data.LOOKUPID +
                    data.COMMTYPE + EmitraServiceDetail.CHECKSUMKEY + data.SSOTOKEN;
                    // Generate base64 SHA256 checksum
                    data.CHECKSUM = EmitraHelper.GenerateSha256HashNew(checksumRaw);
                    // Prepare encryption string with correct URLs
                    string dataStart = "PRN=" + data.PRN +
                                       "::CHANNEL=ONLINE" +
                                       "::REQTIMESTAMP=" + data.REQTIMESTAMP +
                                       "::AMOUNT=" + data.AMOUNT +
                                       "::SUCCESSURL=" + data.SUCCESSURL +
                                       "::FAILUREURL=" + data.FAILUREURL +
                                       "::USERNAME=" + data.USERNAME +
                                       "::USERMOBILE=" + data.USERMOBILE +
                                       "::USEREMAIL=" + data.USEREMAIL +
                                       "::CONSUMERKEY=" + data.CONSUMERKEY +
                                       "::OFFICECODE=" + data.OFFICECODE +
                                       "::REVENUEHEAD=" + data.REVENUEHEAD +
                                       "::UDF1=" + data.UDF1 +
                                       "::UDF2=" + data.UDF2 +
                                       "::LOOKUPID=" + data.LOOKUPID +
                                       "::COMMTYPE=" + data.COMMTYPE +
                                       "::CHECKSUM=" + data.CHECKSUM +
                                       "::SSOTOKEN=" + data.SSOTOKEN

                                       ;

                    string ENC = EmitraHelper.AESEncrypt(dataStart, EmitraServiceDetail.EncryptionKey);
                    if (data != null)
                    {
                        try
                        {
                            objEmitra.key = "_UpdateDetails";
                            objEmitra.RequestString = JsonConvert.SerializeObject(data);
                            objEmitra.TransactionId = result.TransactionId;
                            objEmitra.ServiceID = EmitraServiceDetail.SERVICEID;
                            objEmitra.PRN = data.PRN;
                            var UpdateStatus = await _unitOfWork.CommonFunctionRepository.CreateEmitraApplicationTransation(objEmitra);
                            _unitOfWork.SaveChanges();
                        }
                        catch (System.Exception ex)
                        {
                            throw ex;
                        }
                    }
                    Model.ENCDATA = ENC;
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


        [HttpPost("EmitraApplicationPaymentResponseNew")] //IActionResult
        public async Task<IActionResult> EmitraApplicationPaymentResponseNew(string UniquerequestId = "", string ApplicationIdEnc = "", string ServiceID = "", string IsFailed = "", string UniqueServiceID = "")
        {
            var RetrunUrL = "";
            try
            {
                Int64 applicationNo = 0;
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');

                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');
                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');
                ApplicationIdEnc = ApplicationIdEnc.Replace(' ', '+');

                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');

                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');

                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');

                EmitraRequestDetailsModel Model = new EmitraRequestDetailsModel();
                Model.ServiceID = ServiceID;
                Model.ID = string.IsNullOrEmpty(UniqueServiceID) == true ? 0 : Convert.ToInt32(UniqueServiceID);
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);

                string PRN = Convert.ToString(Request.Form["PRN"]);
                string STATUS = Convert.ToString(Request.Form["STATUS"]);
                var data = Convert.ToString(Request.Form["ENCDATA"]);

                string DNC2 = EmitraHelper.AESDecrypt(data, EmitraServiceDetail.EncryptionKey);

                var dict = DNC2.Split(new[] { "::" }, StringSplitOptions.None)
                .Select(part => part.Split(new[] { '=' }, 2))
                .ToDictionary(pair => pair[0], pair => pair.Length > 1 ? pair[1] : "");
                string json = JsonConvert.SerializeObject(dict);
                EmitraResponseParametersModel EmitraResponseData = new EmitraResponseParametersModel();
                EmitraResponseData = JsonConvert.DeserializeObject<EmitraResponseParametersModel>(json);




                EmitraResponseData.STATUS = STATUS;
                var vIsFailed = CommonFuncationHelper.EmitraDecrypt(IsFailed);
                if (EmitraResponseData != null)
                {
                    EmitraResponseData.ApplicationIdEnc = CommonFuncationHelper.EmitraDecrypt(ApplicationIdEnc);
                    EmitraResponseData.TRANSACTIONID = CommonFuncationHelper.EmitraDecrypt(UniquerequestId);
                    EmitraResponseData.ExamStudentStatus = EmitraResponseData.UDF1;
                    applicationNo = await _unitOfWork.CommonFunctionRepository.UpdateEmitraApplicationPaymentStatus(EmitraResponseData);
                    _unitOfWork.SaveChanges();

                }
                // EmitraServiceDetail.SuccessFailedURL = "http://localhost:4200/ApplicationPaymentStatus";


                if (Convert.ToString(EmitraResponseData.UDF2) == "1") //for direct admission
                {
                    if (EmitraResponseData.STATUS.ToLower() == "success")
                    {
                        RetrunUrL = string.Format("https://kdhte.rajasthan.gov.in/dashboard");
                    }
                    else
                    {
                        RetrunUrL = string.Format("{0}?TransID={1}", "https://kdhte.rajasthan.gov.in/ApplicationPaymentStatus", EmitraResponseData.PRN);
                    }
                }
                else //for all cases
                {
                    if (vIsFailed == "NO")
                    {
                        RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData.PRN);
                    }
                    else
                    {
                        RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData.PRN);
                    }
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

            //return response;
            return new RedirectResult(RetrunUrL);
        }




        [HttpPost("EmitraApplicationVerifyPaymentStatus")]
        public async Task<ApiResult<EmitraResponseParametersModel>> EmitraApplicationVerifyPaymentStatus(RPPTransactionStatusDataModel Model)
        {
            ActionName = "GetTransactionStatus(RPPTransactionStatusDataModel Model)";
            var result = new ApiResult<EmitraResponseParametersModel>();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //get payment details form database
                EmitraRequestDetailsModel dataModel = new EmitraRequestDetailsModel();
                dataModel.ServiceID = Model.ServiceID;
                dataModel.DepartmentID = Model.DepartmentID;
                dataModel.ID = Model.ID;
                var data = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(dataModel);
                if (data == null)
                {
                    result.State = EnumStatus.Error;
                    result.Data = new EmitraResponseParametersModel();
                    result.ErrorMessage = "Payment Integrations Details Not Found.!";
                    return result;
                }

                var d = data.VerifyURL + "?MERCHANTCODE=" + data.MERCHANTCODE + "&SERVICEID=" + data.SERVICEID + "&PRN=" + Model.PRN + "";
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(d);
                webrequest.Method = "POST";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.ContentLength = 0;
                Stream stream = webrequest.GetRequestStream();
                stream.Close();
                string RESPONSEJSON;
                using (WebResponse response = webrequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        RESPONSEJSON = reader.ReadToEnd();
                        EmitraResponseParametersModel RESPONSEPARAMS = JsonConvert.DeserializeObject<EmitraResponseParametersModel>(RESPONSEJSON);
                        dynamic stuff = JsonConvert.DeserializeObject(RESPONSEJSON);
                        string STATUS = stuff.STATUS;
                        string RESPONSECODE = stuff.RESPONSECODE;
                        if (STATUS == "SUCCESS")
                        {
                            result.State = EnumStatus.Success;
                            result.Data = RESPONSEPARAMS;
                            result.Message = RESPONSEPARAMS.RESPONSEMESSAGE;
                            //Update Database
                            RESPONSEPARAMS.TransactionNo = RESPONSEPARAMS.TRANSACTIONID;
                            RESPONSEPARAMS.ApplicationIdEnc = Model.ApplicationID;
                            RESPONSEPARAMS.TRANSACTIONID = Model.TransactionID;
                            RESPONSEPARAMS.ExamStudentStatus = Convert.ToString(Model.ExamStudentStatus);
                            await _unitOfWork.CommonFunctionRepository.UpdateEmitraApplicationPaymentStatus(RESPONSEPARAMS);
                            _unitOfWork.SaveChanges();
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Data = RESPONSEPARAMS;
                            result.ErrorMessage = RESPONSEPARAMS.RESPONSEMESSAGE;
                            //Update Database
                            RESPONSEPARAMS.TransactionNo = RESPONSEPARAMS.TRANSACTIONID;
                            RESPONSEPARAMS.ApplicationIdEnc = Model.ApplicationID;
                            RESPONSEPARAMS.TRANSACTIONID = Model.TransactionID;
                            RESPONSEPARAMS.ExamStudentStatus = Convert.ToString(Model.ExamStudentStatus);
                            await _unitOfWork.CommonFunctionRepository.UpdateEmitraApplicationPaymentStatus(RESPONSEPARAMS);
                            _unitOfWork.SaveChanges();
                        }
                    }
                }

            }
            catch (System.Exception ex)
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



        //check status verify
        [HttpPost("EmitraApplicationVerifyPaymentStatusNew")]
        public async Task<ApiResult<EmitraResponseParametersModel>> EmitraApplicationVerifyPaymentStatusNew(RPPTransactionStatusDataModel Model)
        {
            ActionName = "GetTransactionStatus(RPPTransactionStatusDataModel Model)";
            var result = new ApiResult<EmitraResponseParametersModel>();
            try
            {

                MobilaAppCancelMerchanttokenResponse _MobilaAppCancelMerchanttokenResponse = new MobilaAppCancelMerchanttokenResponse();
                VerifywallettransactionsResponse _VerifywallettransactionsResponse = new VerifywallettransactionsResponse();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //get payment details form database
                EmitraRequestDetailsModel dataModel = new EmitraRequestDetailsModel();
                dataModel.ServiceID = Model.ServiceID;
                dataModel.DepartmentID = Model.DepartmentID;
                dataModel.ID = Model.ID;
                var data = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(dataModel);
                if (data == null)
                {
                    result.State = EnumStatus.Error;
                    result.Data = new EmitraResponseParametersModel();
                    result.ErrorMessage = "Payment Integrations Details Not Found.!";
                    return result;
                }

                //new services Changes
                string requestBody = JsonConvert.SerializeObject(new
                {
                    cleintId = data.CleintID,
                    clientSecret = data.ClientSecret
                });
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(data.TokenURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Access-Control-Allow-Origin", "*");
                webRequest.Timeout = 30000;
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamWriter.Write(requestBody);
                }

                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    _MobilaAppCancelMerchanttokenResponse = JsonConvert.DeserializeObject<MobilaAppCancelMerchanttokenResponse>(responseText);

                    if (_MobilaAppCancelMerchanttokenResponse?.statusCode == 200 && _MobilaAppCancelMerchanttokenResponse.data != null)

                    {

                        string token = _MobilaAppCancelMerchanttokenResponse.data.access_token;
                        string requestStatusBody = JsonConvert.SerializeObject(new
                        {
                            MERCHANTCODE = data.MERCHANTCODE,
                            SERVICEID = data.SERVICEID,
                            PRN = data.PRN,
                            AMOUNT = data.AMOUNT
                        });

                        HttpWebRequest statusRequest = (HttpWebRequest)WebRequest.Create(data.VerifyURL);
                        statusRequest.Method = "POST";
                        statusRequest.ContentType = "application/json";
                        statusRequest.Headers.Add("Authorization", "Bearer " + token);
                        statusRequest.Headers.Add("X-Api-Name", "PAYMENT_STATUS");
                        statusRequest.Headers.Add("Access-Control-Allow-Origin", "*");
                        statusRequest.Timeout = 30000;
                        using (var streamWriter = new StreamWriter(statusRequest.GetRequestStream()))
                        {
                            streamWriter.Write(requestStatusBody);
                        }
                        using (HttpWebResponse statusResponse = (HttpWebResponse)statusRequest.GetResponse())
                        {
                            using (StreamReader readerCHeck = new StreamReader(response.GetResponseStream()))
                            {
                                string responseTextCheck = readerCHeck.ReadToEnd();
                                _VerifywallettransactionsResponse = JsonConvert.DeserializeObject<VerifywallettransactionsResponse>(responseTextCheck);

                                dynamic stuff = JsonConvert.DeserializeObject(responseTextCheck);
                                string STATUS = stuff.STATUS;
                                string RESPONSECODE = stuff.RESPONSECODE;

                                if (_VerifywallettransactionsResponse?.statusCode == 200 && _VerifywallettransactionsResponse.data != null)
                                {
                                    result.State = EnumStatus.Success;
                                    result.Data = _VerifywallettransactionsResponse.data;
                                    result.Message = _VerifywallettransactionsResponse.message;
                                    //Update Database
                                    _VerifywallettransactionsResponse.data.TransactionNo = _VerifywallettransactionsResponse.data.TRANSACTIONID;
                                    _VerifywallettransactionsResponse.data.ApplicationIdEnc = Model.ApplicationID;
                                    _VerifywallettransactionsResponse.data.TRANSACTIONID = Model.TransactionID;
                                    _VerifywallettransactionsResponse.data.ExamStudentStatus = Convert.ToString(Model.ExamStudentStatus);

                                    await _unitOfWork.CommonFunctionRepository.UpdateEmitraApplicationPaymentStatus(_VerifywallettransactionsResponse.data);
                                    _unitOfWork.SaveChanges();
                                }
                                else
                                {
                                    result.State = EnumStatus.Error;
                                    result.Data = _VerifywallettransactionsResponse.data; ;
                                    result.ErrorMessage = _VerifywallettransactionsResponse.message;
                                    //Update Database
                                    _VerifywallettransactionsResponse.data.TransactionNo = _VerifywallettransactionsResponse.data.TRANSACTIONID;
                                    _VerifywallettransactionsResponse.data.ApplicationIdEnc = Model.ApplicationID;
                                    _VerifywallettransactionsResponse.data.TRANSACTIONID = Model.TransactionID;
                                    _VerifywallettransactionsResponse.data.ExamStudentStatus = Convert.ToString(Model.ExamStudentStatus);
                                    await _unitOfWork.CommonFunctionRepository.UpdateEmitraApplicationPaymentStatus(_VerifywallettransactionsResponse.data);
                                    _unitOfWork.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
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

        [HttpPost("GetEmitraServiceAndFeeData")]
        public async Task<ApiResult<EmitraServiceAndFeeModel>> GetEmitraServiceAndFeeData(EmitraServiceAndFeeRequestModel model)
        {
            ActionName = "GetEmitraServiceAndFeeData(EmitraServiceAndFeeRequestModel model)";
            var result = new ApiResult<EmitraServiceAndFeeModel>();
            try
            {
                //get
                var data = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceAndFeeData(model);
                if (data != null)
                {
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (System.Exception ex)
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

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


        #region Payment integration for college
        [HttpPost("EmitraCollegePayment")]
        public async Task<ApiResult<EmitraRequestDetailsModel>> EmitraCollegePayment(EmitraRequestDetailsModel Model)
        {
            ActionName = "EmitraCollegePayment(EmitraRequestDetailsModel Model)";
            var requestDetailsModel = new ApiResult<EmitraRequestDetailsModel>();
            try
            {
                //get
                Model.IsKiosk = true;
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);
                if (EmitraServiceDetail == null)
                {
                    requestDetailsModel.Data = Model;
                    requestDetailsModel.State = EnumStatus.Error;
                    requestDetailsModel.ErrorMessage = "Service Id Not Mapped";
                    return requestDetailsModel;
                }
                Random rnd = new Random();
                string prnNo = "KD" + rnd.Next(100000, 999999) + rnd.Next(100000, 999999);
                EmitraCollegeTransactionsModel objEmitra = new EmitraCollegeTransactionsModel();
                objEmitra.key = "_InsertDetails";
                objEmitra.CollegeIdEnc = Model.InstituteIDEnc ?? "";
                objEmitra.Amount = Model.Amount + Model.ProcessingFee + Model.FormCommision;
                objEmitra.SSOID = Model.SsoID;
                objEmitra.IsEmitra = Model.IsKiosk;
                objEmitra.FeeFor = Model.FeeFor;
                objEmitra.DepartmentID = Model.DepartmentID;
                objEmitra.PRN = prnNo;
                objEmitra.ServiceID = Model.ServiceID;
                objEmitra.KioskID = Model.KIOSKCODE;

                // save
                var result = await _unitOfWork.CommonFunctionRepository.SaveEmitraCollegeTransation(objEmitra);
                _unitOfWork.SaveChanges();

                if (result.TransactionId > 0)
                {
                    PGRequestModel data = new PGRequestModel();
                    data.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;

                    data.PRN = prnNo;
                    data.REQUESTID = Convert.ToString(result.TransactionId);
                    data.REQTIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    data.AMOUNT = Convert.ToString(objEmitra.Amount);
                    data.USERNAME = Model.UserName;
                    data.USERMOBILE = Model.MobileNo;
                    data.COMMTYPE = EmitraServiceDetail.COMMTYPE;
                    data.OFFICECODE = EmitraServiceDetail.OFFICECODE;
                    data.SUBSERVICEID = "";
                    data.CONSUMERKEY = prnNo;
                    data.CONSUMERNAME = Model.UserName;

                    data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("{application_fee}", Model.Amount.ToString())
                        .Replace("{processing_fee}", Model.ProcessingFee.ToString()).Replace("{form_commission}", Convert.ToString(Model.FormCommision));


                    data.SERVICEID = EmitraServiceDetail.SERVICEID;
                    data.UDF1 = "";
                    data.UDF2 = Model.SsoID;
                    data.USEREMAIL = Model.USEREMAIL;
                    data.SSOTOKEN = Model.SSoToken;
                    data.SSOID = Model.SsoID;

                    // create checksum
                    var dRequestChecksum = new DRequestChecksum
                    {
                        SSOID = data.SSOID,
                        REQUESTID = data.REQUESTID,
                        REQTIMESTAMP = data.REQTIMESTAMP,
                        SSOTOKEN = data.SSOTOKEN,
                    };
                    data.CHECKSUM = CommonFuncationHelper.CreateMD5(JsonConvert.SerializeObject(dRequestChecksum));

                    string retVal = ThirdPartyServiceHelper.MakeEmitraTransactionsEncrypted(EmitraServiceDetail.ServiceURL, JsonConvert.SerializeObject(data), EmitraServiceDetail.EncryptionKey);

                    string decVal = EmitraHelper.Decrypt(retVal, EmitraServiceDetail.EncryptionKey);
                    DResponse resp = JsonConvert.DeserializeObject<DResponse>(decVal);

                    if (resp != null)
                    {
                        try
                        {

                            objEmitra.key = "_UpdateEmitraPaymentStatus";
                            objEmitra.ResponseString = JsonConvert.SerializeObject(resp);
                            objEmitra.RequestString = JsonConvert.SerializeObject(data);
                            objEmitra.TransactionId = Convert.ToInt32(resp.REQUESTID);
                            objEmitra.ServiceID = EmitraServiceDetail.SERVICEID;
                            objEmitra.PRN = data.PRN;
                            objEmitra.TransactionNo = Convert.ToString(resp.TRANSACTIONID);
                            objEmitra.PaidAmount = Convert.ToDecimal(resp.TRANSAMT);
                            objEmitra.RequestStatus = Convert.ToString(resp.TRANSACTIONSTATUS);
                            objEmitra.StatusMsg = Convert.ToString(resp.MSG);
                            objEmitra.ReceiptNo = Convert.ToString(resp.RECEIPTNO);

                            //save
                            var UpdateStatus = await _unitOfWork.CommonFunctionRepository.SaveEmitraCollegeTransation(objEmitra);
                            _unitOfWork.SaveChanges();

                            if (resp.TRANSACTIONSTATUS.Contains("SUCCESS"))
                            {
                                requestDetailsModel.State = EnumStatus.Success;
                                requestDetailsModel.Message = resp.MSG;
                                requestDetailsModel.PDFURL = resp.RECEIPT_URL;
                            }
                            else
                            {
                                requestDetailsModel.State = EnumStatus.Error;
                                requestDetailsModel.Message = resp.MSG;
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
                    }
                    else
                    {
                        requestDetailsModel.State = EnumStatus.Error;
                        requestDetailsModel.Message = "something went wrong!";


                    }

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

        [HttpPost("EmitraCollegePaymentNew")]
        public async Task<ApiResult<EmitraRequestDetailsModel>> EmitraCollegePaymentNew(EmitraRequestDetailsModel Model)
        {
            ActionName = "EmitraCollegePaymentNew(EmitraRequestDetailsModel Model)";
            var requestDetailsModel = new ApiResult<EmitraRequestDetailsModel>();
            try
            {
                //get
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);
                if (EmitraServiceDetail == null)
                {
                    requestDetailsModel.Data = Model;
                    requestDetailsModel.State = EnumStatus.Error;
                    requestDetailsModel.ErrorMessage = "Service Id Not Mapped";
                    return requestDetailsModel;
                }
                EmitraCollegeTransactionsModel objEmitra = new EmitraCollegeTransactionsModel();
                objEmitra.key = "_InsertDetails";
                objEmitra.CollegeIdEnc = Model.InstituteIDEnc ?? "";
                objEmitra.Amount = Model.Amount + Model.ProcessingFee + Model.FormCommision;
                objEmitra.SSOID = Model.SsoID;
                objEmitra.IsEmitra = Model.IsKiosk;
                objEmitra.FeeFor = Model.FeeFor;
                objEmitra.DepartmentID = Model.DepartmentID;
                objEmitra.KioskID = Model.KIOSKCODE;

                //save
                var result = await _unitOfWork.CommonFunctionRepository.SaveEmitraCollegeTransation(objEmitra);
                _unitOfWork.SaveChanges();
                if (result.TransactionId > 0)
                {
                    PGRequestModel data = new PGRequestModel();
                    data.MERCHANTCODE = EmitraServiceDetail.MERCHANTCODE;
                    Random rnd = new Random();
                    data.PRN = "KD" + rnd.Next(100000, 999999) + rnd.Next(100000, 999999);
                    data.REQTIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    data.AMOUNT = Convert.ToString(objEmitra.Amount);

                    //url
                    data.SUCCESSURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&CollegeIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.InstituteIDEnc) + "&ServiceID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("NO") + "&UniqueServiceID=" + Model.ID.ToString();

                    //url
                    data.FAILUREURL = EmitraServiceDetail.REDIRECTURL + "?UniquerequestId=" + CommonFuncationHelper.EmitraEncrypt(Convert.ToString(result.TransactionId)) + "&CollegeIdEnc=" + CommonFuncationHelper.EmitraEncrypt(Model.InstituteIDEnc) + "&ServiceID=" + Model.ServiceID.ToString() + "&IsFailed=" + CommonFuncationHelper.EmitraEncrypt("YES") + "&UniqueServiceID=" + Model.ID.ToString();

                    data.USERNAME = Model.UserName.Trim();
                    data.USERMOBILE = Model?.MobileNo ?? "";
                    data.COMMTYPE = EmitraServiceDetail.COMMTYPE;
                    data.OFFICECODE = EmitraServiceDetail.OFFICECODE;
                    // data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("##", Model.Amount.ToString());
                    data.REVENUEHEAD = EmitraServiceDetail.REVENUEHEAD.Replace("{application_fee}", Model.Amount.ToString())
                        .Replace("{processing_fee}", Model.ProcessingFee.ToString()).Replace("{form_commission}", Convert.ToString(Model.FormCommision));

                    data.SERVICEID = EmitraServiceDetail.SERVICEID;
                    data.UDF1 = Convert.ToString(result.TransactionId);
                    data.UDF2 = "";
                    data.USEREMAIL = Model.USEREMAIL.Trim();
                    data.CONSUMERKEY = data.PRN + "-" + Model.InstituteIDEnc;
                    data.CHANNEL = "ONLINE";
                    data.LOOKUPID = "";
                    data.SSOTOKEN = Model.SSoToken;

                    // Create checksum input string
                    string checksumRaw = data.MERCHANTCODE + data.SERVICEID + data.PRN + "ONLINE" +
                    data.REQTIMESTAMP + data.AMOUNT + data.SUCCESSURL +
                    data.FAILUREURL + data.USERNAME + data.USERMOBILE +
                    data.USEREMAIL + data.CONSUMERKEY + data.OFFICECODE +
                    data.REVENUEHEAD + data.UDF1 + data.UDF2 + data.LOOKUPID +
                    data.COMMTYPE + EmitraServiceDetail.CHECKSUMKEY + data.SSOTOKEN;
                    // Generate base64 SHA256 checksum
                    data.CHECKSUM = EmitraHelper.GenerateSha256HashNew(checksumRaw);
                    // Prepare encryption string with correct URLs
                    string dataStart = "PRN=" + data.PRN +
                                       "::CHANNEL=ONLINE" +
                                       "::REQTIMESTAMP=" + data.REQTIMESTAMP +
                                       "::AMOUNT=" + data.AMOUNT +
                                       "::SUCCESSURL=" + data.SUCCESSURL +
                                       "::FAILUREURL=" + data.FAILUREURL +
                                       "::USERNAME=" + data.USERNAME +
                                       "::USERMOBILE=" + data.USERMOBILE +
                                       "::USEREMAIL=" + data.USEREMAIL +
                                       "::CONSUMERKEY=" + data.CONSUMERKEY +
                                       "::OFFICECODE=" + data.OFFICECODE +
                                       "::REVENUEHEAD=" + data.REVENUEHEAD +
                                       "::UDF1=" + data.UDF1 +
                                       "::UDF2=" + data.UDF2 +
                                       "::LOOKUPID=" + data.LOOKUPID +
                                       "::COMMTYPE=" + data.COMMTYPE +
                                       "::CHECKSUM=" + data.CHECKSUM +
                                       "::SSOTOKEN=" + data.SSOTOKEN

                                       ;

                    string ENC = EmitraHelper.AESEncrypt(dataStart, EmitraServiceDetail.EncryptionKey);
                    if (data != null)
                    {
                        try
                        {
                            objEmitra.key = "_UpdateDetails";
                            objEmitra.RequestString = JsonConvert.SerializeObject(data);
                            objEmitra.TransactionId = result.TransactionId;
                            objEmitra.ServiceID = EmitraServiceDetail.SERVICEID;
                            objEmitra.PRN = data.PRN;

                            //save
                            var UpdateStatus = await _unitOfWork.CommonFunctionRepository.SaveEmitraCollegeTransation(objEmitra);
                            _unitOfWork.SaveChanges();
                        }
                        catch (System.Exception ex)
                        {
                            throw ex;
                        }
                    }
                    Model.ENCDATA = ENC;
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

        [HttpPost("EmitraCollegePaymentResponseNew")] //IActionResult
        public async Task<IActionResult> EmitraCollegePaymentResponseNew(string UniquerequestId = "", string CollegeIdEnc = "", string ServiceID = "", string IsFailed = "", string UniqueServiceID = "")
        {
            var RetrunUrL = "";
            try
            {
                Int64 applicationNo = 0;
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');
                UniquerequestId = UniquerequestId.Replace(' ', '+');

                CollegeIdEnc = CollegeIdEnc.Replace(' ', '+');
                CollegeIdEnc = CollegeIdEnc.Replace(' ', '+');
                CollegeIdEnc = CollegeIdEnc.Replace(' ', '+');

                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');
                IsFailed = IsFailed.Replace(' ', '+');

                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');
                ServiceID = ServiceID.Replace(' ', '+');

                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');
                UniqueServiceID = UniqueServiceID.Replace(' ', '+');

                EmitraRequestDetailsModel Model = new EmitraRequestDetailsModel();
                Model.ServiceID = ServiceID;
                Model.ID = string.IsNullOrEmpty(UniqueServiceID) == true ? 0 : Convert.ToInt32(UniqueServiceID);
                var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);

                string PRN = Convert.ToString(Request.Form["PRN"]);
                string STATUS = Convert.ToString(Request.Form["STATUS"]);
                var data = Convert.ToString(Request.Form["ENCDATA"]);

                string DNC2 = EmitraHelper.AESDecrypt(data, EmitraServiceDetail.EncryptionKey);

                var dict = DNC2.Split(new[] { "::" }, StringSplitOptions.None)
          .Select(part => part.Split(new[] { '=' }, 2))
          .ToDictionary(pair => pair[0], pair => pair.Length > 1 ? pair[1] : "");
                string json = JsonConvert.SerializeObject(dict);
                EmitraCollegeTransactionsModel EmitraResponseData = new EmitraCollegeTransactionsModel();
                EmitraResponseData = JsonConvert.DeserializeObject<EmitraCollegeTransactionsModel>(json);

                EmitraResponseData.STATUS = STATUS;
                var vIsFailed = CommonFuncationHelper.EmitraDecrypt(IsFailed);
                if (EmitraResponseData != null)
                {
                    EmitraResponseData.CollegeIdEnc = CommonFuncationHelper.EmitraDecrypt(CollegeIdEnc);
                    EmitraResponseData.TRANSACTIONID = CommonFuncationHelper.EmitraDecrypt(UniquerequestId);
                    applicationNo = await _unitOfWork.CommonFunctionRepository.UpdateEmitraCollegePaymentStatus(EmitraResponseData);
                    _unitOfWork.SaveChanges();

                }
                // EmitraServiceDetail.SuccessFailedURL = "http://localhost:4200/ApplicationPaymentStatus";


                if (vIsFailed == "NO")
                {
                    RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData.PRN);
                }
                else
                {
                    RetrunUrL = string.Format("{0}?TransID={1}", EmitraServiceDetail.SuccessFailedURL, EmitraResponseData.PRN);
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

            //return response;
            return new RedirectResult(RetrunUrL);
        }

        [HttpGet("GetEmitraCollegeTransactionDetails/{TransID}")]
        public async Task<ApiResult<DataTable>> GetEmitraCollegeTransactionDetails(string TransID)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetEmitraCollegeTransactionDetails(TransID));
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
        #endregion


        #region Private

        #region Emitra TransactionEntripted
        private static async Task<string> MakeTransactionsEncrypted(string URL, string data, string encryptionKey, string WebServiceURL)
        {
            try
            {
                //LogErrorToLogFile error = new LogErrorToLogFile();

                //error.LogEmitra("Kiosk Verify " + data, "TestApp1");

                EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration endpointConfiguration = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient.EndpointConfiguration();

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient emitraencsev = new EmitraEmitraEncrytDecryptClient.EmitraEncrytDecryptSoapClient(endpointConfiguration, WebServiceURL);
                EmitraEncryptStringResponse response = await emitraencsev.EmitraEncryptStringAsync(encryptionKey, JsonConvert.SerializeObject(data));


                string encData = response.Body.EmitraEncryptStringResult;
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
                var Emitraresponse = http.GetResponse();
                var stream = Emitraresponse.GetResponseStream();
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

        #endregion
    }

}
