using AutoMapper;
using Kaushal_Darpan.Api.Email;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationMessageModel;

//using Newtonsoft.Json;
using Kaushal_Darpan.Models.SMSConfigurationSetting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static Kaushal_Darpan.Api.Controllers.IndustryInstitutePartnershipMasterController;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]

    public class SMSMailController : BaseController
    {
        public override string PageName => "SMSMailController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        //private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataTable _dataTable_Master = new DataTable();
        private readonly SMSConfigurationSettingModel _sMSConfigurationSetting;

        //public SMSMailController(IMapper mapper, IUnitOfWork unitOfWork, IEmailService emailService)
        public SMSMailController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            //_emailService = emailService;
            _unitOfWork = unitOfWork;
            _sMSConfigurationSetting = _unitOfWork.SMSMailRepository.GetSMSConfigurationSetting().Result;
        }

        [HttpGet("SendMessage/{MobileNo}/{MessageType}/{ID=0}")]
        public async Task<ApiResult<string>> SendMessage(string MobileNo, string MessageType, int ID = 0)
        {
            ActionName = "SendMessage(string MobileNo, string MessageType, int ID = 0)";

            var result = new ApiResult<string>();
            try
            {
                // send
                string ReturnOTP = "";
                string MessageBody = "";
                string TempletID = "";
                DataTable dataTable = await _unitOfWork.SMSMailRepository.GetSMSTemplateByMessageType(MessageType);
                if (dataTable.Rows.Count > 0)
                {
                    MessageBody = dataTable.Rows[0]["MessageBody"].ToString();
                    TempletID = dataTable.Rows[0]["TemplateID"].ToString(); ;
                }
                if (MessageType == EnumMessageType.Iti_OTP.GetDescription())
                {
                    ReturnOTP = CommonFuncationHelper.SMS_GenerateNewRandom();
                    MessageBody = MessageBody.Replace("{#OTP#}", ReturnOTP);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, MobileNo, MessageBody, TempletID);
                }
                else if (MessageType == EnumMessageType.Bter_OTP.GetDescription())
                {
                    ReturnOTP = CommonFuncationHelper.SMS_GenerateNewRandom();
                    MessageBody = MessageBody.Replace("{#OTP#}", ReturnOTP);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, MobileNo, MessageBody, TempletID); 
                }
                else
                {
                    //Like Templet
                    MessageBody = MessageBody.Replace("{#OTP#}", ReturnOTP);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, MobileNo, MessageBody, TempletID);
                }
                 //await _emailService.SendEmail(MessageBody, "ramraj.malav@devitpl.com");
                result.Data = ReturnOTP;
                if (result.Data != null)
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




        [HttpPost("SendApplicationMessage")]
        public async Task<ApiResult<string>> SendApplicationMessage(ApplicationMessageDataModel request)
        {
            ActionName = "SendApplicationMessage(ApplicationMessageDataModel request)";

            var result = new ApiResult<string>();
            try
            {
                // send
                string ReturnOTP = "";
                string MessageBody = "";
                string TempletID = "";
                DataTable dataTable = await _unitOfWork.SMSMailRepository.GetSMSTemplateByMessageType(request.MessageType);
                if (dataTable.Rows.Count > 0)
                {
                    MessageBody = Convert.ToString(dataTable.Rows[0]["MessageBody"]);
                    TempletID = Convert.ToString(dataTable.Rows[0]["TemplateID"]);
                }
                if (request.MessageType == EnumMessageType.Iti_OTP.GetDescription())
                {
                    ReturnOTP = CommonFuncationHelper.SMS_GenerateNewRandom();
                    MessageBody = MessageBody.Replace("{#OTP#}", ReturnOTP);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                }
                else if (request.MessageType == EnumMessageType.Iti_FormSubmit.GetDescription()
                    || request.MessageType == EnumMessageType.Iti_FormFinalSubmit.GetDescription())
                {
                    MessageBody = MessageBody.Replace("{#ApplicationNo#}", request.ApplicationNo);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                }
                else if (request.MessageType == EnumMessageType.Bter_OTP.GetDescription())
                {
                    ReturnOTP = CommonFuncationHelper.SMS_GenerateNewRandom();
                    MessageBody = MessageBody.Replace("{#OTP#}", ReturnOTP);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                }
                else if (request.MessageType == EnumMessageType.Bter_FormSubmit.GetDescription()
                    || request.MessageType == EnumMessageType.Bter_FormFinalSubmit.GetDescription())
                {
                    MessageBody = MessageBody.Replace("{#ApplicationNo#}", request.ApplicationNo);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                }
                else if (request.MessageType == EnumMessageType.Bter_NotifyCandidateDeficiency.GetDescription())
                {
                    DataTable AppDetails = await _unitOfWork.BterApplicationRepository.GetDetailsbyApplicationNo(request.ApplicationDetails);
                    foreach (DataRow row in AppDetails.Rows)
                    {
                        MessageBody = MessageBody.Replace("{#ApplicationNo#}", Convert.ToString(row["ApplicationNo"]))
                        .Replace("{#Scheme#}", Convert.ToString(row["Scheme"]))                        
                        .Replace("{#DepartmentName#}", Convert.ToString(row["PortalName"]))
                        .Replace("{#var#}", Convert.ToString(row["MessageRemarks"]));
                        try
                        {
                            var mobile = Convert.ToString(row["MobileNo"]);
                            if(mobile != null)
                            {
                                CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, mobile, MessageBody, TempletID);//add in que
                            }                            
                        }
                        catch
                        {
                        }
                    }
                }
                else if (request.MessageType == EnumMessageType.Bter_NotifyCandidateApproveMerit.GetDescription())
                {
                    //DataTable AppDetails = await _unitOfWork.BterApplicationRepository.GetDetailsbyApplicationNo(request.ApplicationDetails);
                    
                    DataTable AppDetails = await _unitOfWork.iCorrectMeritRepository.GetApplicationDetails_ByMeritId(request.MeritId!.Value);
                    foreach (DataRow row in AppDetails.Rows)
                    {
                        MessageBody = MessageBody.Replace("{#coursetype#}", Convert.ToString(row["Scheme"]));
                        try
                        {
                            var mobile = Convert.ToString(row["MobileNo"]);
                            if (mobile != null)
                            {
                                CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, mobile, MessageBody, TempletID);//add in que
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                else if (request.MessageType == EnumMessageType.Bter_NotifyCandidateRejectMerit.GetDescription())
                {
                    //DataTable AppDetails = await _unitOfWork.BterApplicationRepository.GetDetailsbyApplicationNo(request.ApplicationDetails);

                    DataTable AppDetails = await _unitOfWork.iCorrectMeritRepository.GetApplicationDetails_ByMeritId(request.MeritId!.Value);
                    foreach (DataRow row in AppDetails.Rows)
                    {
                        MessageBody = MessageBody.Replace("{#coursetype#}", Convert.ToString(row["Scheme"]))
                        .Replace("{#portal#}", Convert.ToString(row["PortalName"]));
                        try
                        {
                            var mobile = Convert.ToString(row["MobileNo"]);
                            if (mobile != null)
                            {
                                CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, mobile, MessageBody, TempletID);//add in que
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    //Like Templet
                    MessageBody = MessageBody.Replace("{#OTP#}", ReturnOTP);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                }

                result.Data = ReturnOTP;
                if (result.Data != null)
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




        [HttpGet("SendMessage_Local")]
        public async Task<ApiResult<string>> SendMessage_Local()
        {
            ActionName = "SendMessage_Local()";

            var result = new ApiResult<string>();
            try
            {
                //
                string MessageBody = "";
                string TempletID = "1107175393865180250";
                string MobileNo = "7737348604";
                string AID = "0";

                DataTable dataTable = await _unitOfWork.SMSMailRepository.GetAllUnsendSMS();
                foreach (DataRow item in dataTable.Rows)
                {
                    AID = item["AID"].ToString();
                    MessageBody = item["SMSText"].ToString();
                    //TempletID = item["TemplateID"].ToString();
                    MobileNo = item["MobileNo"].ToString();
                    try
                    {
                        string Response = await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, MobileNo, MessageBody, TempletID);
                        var isSend = await _unitOfWork.SMSMailRepository.UpdateUnsendSMSById(AID, Response);
                        _unitOfWork.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                result.Data = "Done";
                if (result.Data != null)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "SMS Send successfully .!";
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
    }
}