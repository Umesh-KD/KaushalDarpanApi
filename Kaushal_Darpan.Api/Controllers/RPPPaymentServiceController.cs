using AutoMapper;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.RPPPayment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]

    public class RPPPaymentServiceController : BaseController
    {
        public override string PageName => "RPPPaymentServiceController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly M_AadharCardServiceMaster _m_AadharCardServiceMaster;

        public RPPPaymentServiceController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _m_AadharCardServiceMaster = _unitOfWork.CommonFunctionRepository.GetAadharCardServiceMaster().Result;
        }

        #region "RPP PAYMENT INTEGRAION"
        [HttpPost("PaymentRequest")]
        public async Task<ApiResult<RPPPaymentRequestModel>> PaymentRequest(RPPRequestDetailsModel request)
        {
            ActionName = "PaymentRequest(RPPRequestDetailsModel request)";

            RPPPaymentGatewayDataModel Model = new RPPPaymentGatewayDataModel();
            Model.PaymentGateway = (int)EnmPaymentGatway.RPP;
            Model.DepartmentID = request.DepartmentID;
            var data = await _unitOfWork.CommonFunctionRepository.RPPGetpaymentGatewayDetails(Model);

            var result = new ApiResult<RPPPaymentRequestModel>();
            string PRN = "TXN" + CommonFuncationHelper.GenerateTransactionNumber();
            try
            {
                if (string.IsNullOrWhiteSpace(data.MerchantCode))
                {
                    result.State = EnumStatus.Error;
                    result.Data = new RPPPaymentRequestModel();
                    result.ErrorMessage = "Payment Integraion Details Not Found.!";
                    return result;
                }

                result.Data = await Task.Run(() => ThirdPartyServiceHelper.RPPSendRequest(PRN, Convert.ToDecimal(request.AMOUNT).ToString(), request.PURPOSE, request.USERNAME, request.USERMOBILE, request.USEREMAIL, request.ApplyNocApplicationID.ToString(), data));
                if (result.Data == null)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "There was an error payment.!";
                    return result;
                }

                result.Data.CreatedBy = request.CreatedBy;
                result.Data.SSOID = request.SSOID;
                result.Data.REQUESTPARAMETERS.RequestType = (int)EnmPaymetRequest.PaymentRequest;

                bool isSuccess = await _unitOfWork.CommonFunctionRepository.RPPCreatePaymentRequest(result.Data);
                _unitOfWork.SaveChanges();
                if (!isSuccess)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "There was an error payment.!";
                    return result;
                }

                result.State = EnumStatus.Success;
                result.Message = "Payment successfully .!";
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

        [HttpPost("PaymentResponse")] //IActionResult
        public async Task<IActionResult> PaymentResponse(string DepartmentID = "0")
        {
            ActionName = "PaymentResponse(string DepartmentID = '0')";

            var result = new ApiResult<RPPPaymentResponseModel>();
            string RetrunUrL = "";
            try
            {
                //Get Department ID
                DepartmentID = DepartmentID.Replace(' ', '+');
                DepartmentID = DepartmentID.Replace(' ', '+');
                DepartmentID = DepartmentID.Replace(' ', '+');

                //Get Payment Details 
                RPPPaymentGatewayDataModel Model = new RPPPaymentGatewayDataModel();
                Model.PaymentGateway = (int)EnmPaymentGatway.RPP;
                Model.DepartmentID = Convert.ToInt32(CommonFuncationHelper.EmitraDecrypt(DepartmentID));
                var data = await _unitOfWork.CommonFunctionRepository.RPPGetpaymentGatewayDetails(Model);

                string STATUS = Request.Form["STATUS"];
                string ENCDATA = Request.Form["ENCDATA"];

                result.Data = ThirdPartyServiceHelper.RPPGetResponse(STATUS, ENCDATA, data);
                result.State = EnumStatus.Success;
                if (result.Data != null)
                {
                    await _unitOfWork.CommonFunctionRepository.RPPSaveData(result.Data);
                    _unitOfWork.SaveChanges();

                    if (result.Data.CHECKSUMVALID)
                    {
                        if (result.Data.RESPONSEPARAMETERS.STATUS.ToLower() == "Success".ToLower())
                        {
                            result.State = EnumStatus.Success;
                            result.Message = "Data load successfully .!";
                            RetrunUrL = string.Format("{0}{1}", data.RedirectURL, result.Data.RESPONSEPARAMETERS.PRN);
                        }
                        else
                        {
                            RetrunUrL = string.Format("{0}{1}", data.RedirectURL, result.Data.RESPONSEPARAMETERS.PRN);
                        }
                    }
                    else
                    {
                        RetrunUrL = string.Format("{0}{1}", data.RedirectURL, result.Data.RESPONSEPARAMETERS.PRN);
                    }
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No record found.!";
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
            return Redirect(RetrunUrL);
        }

        [HttpGet("GetTransactionDetails/{TransID}")]
        public async Task<ApiResult<List<RPPResponseParametersModel>>> GetTransactionDetails(string TransID)
        {
            ActionName = "GetTransactionDetails(string TransID)";

            var result = new ApiResult<List<RPPResponseParametersModel>>();
            try
            {
                result.Data = await _unitOfWork.CommonFunctionRepository.RPPGetPaymentListIDWise(TransID);
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

        [HttpPost("GetTransactionStatus")]
        public async Task<ApiResult<RPPResponseParametersModel>> GetTransactionStatus(RPPTransactionStatusDataModel Model)
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
                Model.PRN = "KD230563301306";
                var data = await _unitOfWork.CommonFunctionRepository.RPPGetpaymentGatewayDetails(dataModel);
                data.VerificationURL = "http://emitrauat.rajasthan.gov.in/webServicesRepositoryUat/newAggrTransVerify";

                if (string.IsNullOrWhiteSpace(data.MerchantCode))
                {
                    result.State = EnumStatus.Error;
                    result.Data = new RPPResponseParametersModel();
                    result.ErrorMessage = "Payment Integrations Details Not Found.!";
                    return result;
                }


                var d= data.VerificationURL + "?MERCHANTCODE=" + data.MerchantCode + "&SERVICEID=" + "5442" + "&PRN=" + Model.PRN + "";
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

        [HttpPost("RPPTransactionRefund")]
        public async Task<ApiResult<RPPResponseParametersModel>> RPPTransactionRefund(RPPTransactionStatusDataModel Model)
        {
            ActionName = "RPPTransactionRefund(RPPTransactionStatusDataModel Model)";

            var result = new ApiResult<RPPResponseParametersModel>();
            try
            {
                string APINAME = "TXNREFUND";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                //get payment details form database
                RPPPaymentGatewayDataModel dataModel = new RPPPaymentGatewayDataModel();
                dataModel.PaymentGateway = (int)EnmPaymentGatway.RPP;
                dataModel.DepartmentID = Model.DepartmentID;
                var data = await _unitOfWork.CommonFunctionRepository.RPPGetpaymentGatewayDetails(dataModel);

                if (string.IsNullOrWhiteSpace(data.MerchantCode))
                {
                    result.State = EnumStatus.Error;
                    result.Data = new RPPResponseParametersModel();
                    result.ErrorMessage = "Payment Integrations Details Not Found.!";
                    return result;
                }

                //Save Data in database
                RPPPaymentRequestModel paymentRequest = new RPPPaymentRequestModel();
                paymentRequest.REQUESTPARAMETERS = new RPPRequestParametersModel();//assign memory

                Model.SubOrderID = paymentRequest.REQUESTPARAMETERS.PRN;
                paymentRequest.REQUESTJSON = JsonConvert.SerializeObject(Model);
                paymentRequest.REQUESTPARAMETERS.UDF1 = Model.ApplyNocApplicationID;
                paymentRequest.REQUESTPARAMETERS.RequestType = (int)EnmPaymetRequest.RefundRequest;
                paymentRequest.REQUESTPARAMETERS.PRN = "RFD" + CommonFuncationHelper.GenerateTransactionNumber();
                paymentRequest.REQUESTPARAMETERS.AMOUNT = Model.AMOUNT;
                paymentRequest.REQUESTPARAMETERS.MERCHANTCODE = data.MerchantCode;
                paymentRequest.REQUESTPARAMETERS.RPPTXNID = Model.RPPTXNID;
                paymentRequest.SSOID = Model.SSOID;
                bool isSuccess = await _unitOfWork.CommonFunctionRepository.RPPCreatePaymentRequest(paymentRequest);
                _unitOfWork.SaveChanges();

                if (!isSuccess)
                {
                    result.State = EnumStatus.Error;
                    result.Data = new RPPResponseParametersModel();
                    result.ErrorMessage = "Something went wrong";
                    return result;
                }

                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(data.RefundURL + "?MERCHANTCODE=" + data.MerchantCode + "&PRN=" + Model.PRN + "&AMOUNT=" + Model.AMOUNT + "&RPPTXNID=" + Model.RPPTXNID + "&SUBORDERID=" + paymentRequest.REQUESTPARAMETERS.PRN + "&APINAME=" + APINAME);
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
                            result.Message = RESPONSEPARAMS.REMARKS;

                            #region "Update Payment Status Success Case"
                            RPPPaymentResponseModel obj = new RPPPaymentResponseModel();
                            obj.RESPONSEPARAMETERS = RESPONSEPARAMS;
                            obj.RESPONSEJSON = RESPONSEJSON;
                            obj.RESPONSEPARAMETERS.UDF1 = Model.ApplyNocApplicationID;
                            obj.RESPONSEPARAMETERS.PRN = paymentRequest.REQUESTPARAMETERS.PRN;
                            obj.RESPONSEPARAMETERS.REFUNDID = RESPONSEPARAMS.REFUNDID;
                            obj.RESPONSEPARAMETERS.REFUNDTIMESTAMP = RESPONSEPARAMS.REFUNDTIMESTAMP;
                            obj.RESPONSEPARAMETERS.REMARKS = RESPONSEPARAMS.REMARKS;
                            obj.RESPONSEPARAMETERS.RESPONSEMESSAGE = RESPONSEPARAMS.REFUNDSTATUS;
                            obj.RESPONSEPARAMETERS.REFUNDSTATUS = RESPONSEPARAMS.REFUNDSTATUS;
                            obj.RESPONSEPARAMETERS.RPPTXNID = Model.RPPTXNID;
                            await _unitOfWork.CommonFunctionRepository.RPPUpdateRefundStatus(obj);
                            _unitOfWork.SaveChanges();
                            #endregion
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Data = RESPONSEPARAMS;
                            result.ErrorMessage = RESPONSEPARAMS.RESPONSEMESSAGE;

                            #region "Update Payment Status Failed Case"
                            RPPPaymentResponseModel obj = new RPPPaymentResponseModel();
                            obj.RESPONSEPARAMETERS = RESPONSEPARAMS;
                            obj.RESPONSEJSON = RESPONSEJSON;
                            obj.RESPONSEPARAMETERS.UDF1 = Model.ApplyNocApplicationID;
                            obj.RESPONSEPARAMETERS.PRN = paymentRequest.REQUESTPARAMETERS.PRN;
                            obj.RESPONSEPARAMETERS.RESPONSECODE = RESPONSEPARAMS.RESPONSECODE;
                            obj.RESPONSEPARAMETERS.REFUNDSTATUS = RESPONSEPARAMS.STATUS;
                            obj.RESPONSEPARAMETERS.RPPTXNID = Model.RPPTXNID;
                            await _unitOfWork.CommonFunctionRepository.RPPUpdateRefundStatus(obj);
                            _unitOfWork.SaveChanges();
                            #endregion

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

        [HttpPost("RPPTransactionRefundStatus")]
        public async Task<ApiResult<RPPRefundTransactionDataModel>> RPPTransactionRefundStatus(RPPTransactionStatusDataModel Model)
        {
            ActionName = "RPPTransactionRefundStatus(RPPTransactionStatusDataModel Model)";

            var result = new ApiResult<RPPRefundTransactionDataModel>();
            try
            {
                string APINAME = "REFUNDSTATUS";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                //get payment details form database
                RPPPaymentGatewayDataModel dataModel = new RPPPaymentGatewayDataModel();
                dataModel.PaymentGateway = (int)EnmPaymentGatway.RPP;
                dataModel.DepartmentID = Model.DepartmentID;
                var data = await _unitOfWork.CommonFunctionRepository.RPPGetpaymentGatewayDetails(dataModel);

                if (string.IsNullOrWhiteSpace(data.MerchantCode))
                {
                    result.State = EnumStatus.Error;
                    result.Data = new RPPRefundTransactionDataModel();
                    result.ErrorMessage = "Payment Integrations Details Not Found.!";
                    return result;
                }

                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(data.RefundStatusURL + "?MERCHANTCODE=" + data.MerchantCode + "&RPPTXNID=" + Model.RPPTXNID + "&APINAME=" + APINAME);
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
                        RPPRefundTransactionDataModel RESPONSEPARAMS = JsonConvert.DeserializeObject<RPPRefundTransactionDataModel>(RESPONSEJSON);
                        dynamic stuff = JsonConvert.DeserializeObject(RESPONSEJSON);
                        string STATUS = stuff.STATUS;
                        string RESPONSECODE = stuff.RESPONSECODE;
                        if (STATUS == "SUCCESS")
                        {
                            result.State = EnumStatus.Success;
                            result.Data = RESPONSEPARAMS;
                            result.Message = RESPONSEPARAMS.STATUS;

                            RESPONSEPARAMS.ApplyNocApplicationID = Convert.ToInt32(Model.ApplyNocApplicationID);
                            //Update Status In DataBase
                            RESPONSEPARAMS.PRN = Model.PRN;
                            RESPONSEPARAMS.RESPONSEJSON = RESPONSEJSON;
                            await _unitOfWork.CommonFunctionRepository.RPPUpdateRefundTransactionStatus(RESPONSEPARAMS);
                            _unitOfWork.SaveChanges();
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Data = RESPONSEPARAMS;
                            result.ErrorMessage = RESPONSEPARAMS.STATUS;
                            //Update Status
                            RESPONSEPARAMS.ApplyNocApplicationID = Convert.ToInt32(Model.ApplyNocApplicationID);
                            RESPONSEPARAMS.PRN = Model.PRN;
                            RESPONSEPARAMS.RESPONSEJSON = RESPONSEJSON;
                            await _unitOfWork.CommonFunctionRepository.RPPUpdateRefundTransactionStatus(RESPONSEPARAMS);
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

        [HttpPost("RPPTransactionCallback")]
        public IActionResult RPPTransactionCallback()
        {
            return Content("");
        }

        [HttpPost("GetRPPTransactionList")]
        public async Task<ApiResult<DataTable>> GetRPPTransactionList(RPPTransactionSearchFilterModelModel Model)
        {
            ActionName = "GetRPPTransactionList(RPPTransactionSearchFilterModelModel Model)";

            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.CommonFunctionRepository.GetRPPTransactionList(Model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No record found.!";
                    return result;
                }

                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
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

    }
}
