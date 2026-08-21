using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Kaushal_Darpan.Api.Email;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationMessageModel;
using Kaushal_Darpan.Models.CommonModel;


//using Newtonsoft.Json;
using Kaushal_Darpan.Models.SMSConfigurationSetting;
using Kaushal_Darpan.Models.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Channels;
using System.Text.RegularExpressions;
using static Kaushal_Darpan.Api.Controllers.IndustryInstitutePartnershipMasterController;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
                await _unitOfWork.DisposeAsync();
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
            string oldmessagetype = request.MessageType;

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
                request.MessageType = oldmessagetype;

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

                else if (request.MessageType == EnumMessageType.GuestHouseCheckIn.GetDescription())
                {

                    //MessageBody = MessageBody.Replace("{#RoomNo#}", request.ApplicationNo.Replace("{#GuestHouseName#}", request.ApplicantName));
                    MessageBody = MessageBody.Replace("{#checkIn_CheckOut#}", request.CheckIn_CheckOut)
                        .Replace("{#RoomNo#}", request.ApplicationNo)
                        .Replace("{#var#}", "")
                        .Replace("{#GuestHouseName#}", request.ApplicantName);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                }
                else if (request.MessageType == EnumMessageType.GuestHouseCheckOut.GetDescription())
                {

                    MessageBody = MessageBody.Replace("{#checkIn_CheckOut#}", request.CheckIn_CheckOut)
                        .Replace("{#RoomNo#}", request.ApplicationNo)
                        .Replace("{#var#}", "")
                        .Replace("{#GuestHouseName#}", request.ApplicantName);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                }
                else if (request.MessageType == EnumMessageType.GuestHouseAdminApprove.GetDescription())
                {

                    MessageBody = MessageBody.Replace("{#room#}", request.ApplicationNo)
                        .Replace("{#GuestHouseName#}", request.ApplicantName)
                        .Replace("{#var#}", "")
                        .Replace("{#status#}", request.Status);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                }
                //Bter Placmeent SMS Service campus creatrion
                else if (request.MessageType == EnumMessageType.Bter_CampusPostCreation.GetDescription())
                {

                    MessageBody = MessageBody.Replace("{#ApplicantName#}", request.ApplicantName)
                        .Replace("{#CampusID#}", request.CampusID)
                        .Replace("{#var#}", "")
                        .Replace("{#ActionDate#}", request.ActionDate)
                        .Replace("{#CampusLocationURL#}", request.CampusLocationURL)
                        .Replace("{#NodalType#}", request.NodalType);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                }
                //Bter Placmeent SMS Service campus approval
                else if (request.MessageType == EnumMessageType.Bter_CampusApprove.GetDescription())
                {

                    MessageBody = MessageBody.Replace("{#ApplicantName#}", request.ApplicantName)
                        .Replace("{#CampusID#}", request.CampusID)
                        .Replace("{#var#}", "")
                        .Replace("{#ActionDate#}", request.ActionDate)
                        .Replace("{#ReferenceID#}", request.ReferenceID)
                        .Replace("{#CampusLocationURL#}", request.CampusLocationURL)
                        .Replace("{#NodalType#}", request.NodalType);
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                    //await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, "8003781633", MessageBody, TempletID);
                }
                //Bter Placmeent SMS Service student consent
                else if (request.MessageType == EnumMessageType.Bter_StudentConsent.GetDescription())
                {

                    MessageBody = MessageBody.Replace("{#EnrollmentNo#}", request.EnrollmentNo)
                        .Replace("{#CampusID#}", request.CampusID)
                        .Replace("{#var#}", "")
                        .Replace("{#RegNo#}", request.RegNo)
                        ;
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                    // await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, "8003781633", MessageBody, TempletID);
                }
                //Bter Placmeent SMS Service CompanyHRApprove
                else if (request.MessageType == EnumMessageType.Bter_ComapnyHRApprove.GetDescription())
                {
                    MessageBody = MessageBody.Replace("{#ApplicantName#}", request.ApplicantName)
                        .Replace("{#ReferenceID#}", request.ReferenceID)
                        ;
                    await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, request.MobileNo, MessageBody, TempletID);
                    // await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, "8003781633", MessageBody, TempletID);

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
                await _unitOfWork.DisposeAsync();
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
                string MessageBody = "Final Provisional merit list of Academic Year 2026-27 ITI Admission is published.Please check your merit at kdhte.rajasthan.gov.in/itipublicinfo -DTE,Jodhpur";
                //string MessageBody = "Final Provisional merit list of Iti Admission is published.Please check your merit at kdhte.rajasthan.gov.in -DTE,Jodhpur"
                string TempletID = "1007590954352487470";
                string MobileNo = "7737348604";
                string AID = "0";

                DataTable dataTable = await _unitOfWork.SMSMailRepository.GetAllUnsendSMS();
                foreach (DataRow item in dataTable.Rows)
                {
                    AID = item["AID"].ToString();
                    MessageBody = item["SMSText"].ToString();
                    TempletID = item["TemplateID"].ToString();
                    MobileNo = item["MobileNo"].ToString();
                    try
                    {
                        string Response = await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, MobileNo, MessageBody, TempletID);
                        var isSend = await _unitOfWork.SMSMailRepository.UpdateUnsendSMSById(AID, Response);
                        await _unitOfWork.SaveChangesAsync();
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
                await _unitOfWork.DisposeAsync();
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

        //[RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpPost("SendSMSForStudentEnrollmentData")]
        public async Task<ApiResult<bool>> SendSMSForStudentEnrollmentData([FromBody] List<ForSMSEnrollmentStudentMarkedModel> request)
        {
            ActionName = "SendSMSForStudentEnrollmentData([FromBody] List<ForSMSEnrollmentStudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    DataTable dataTable = await _unitOfWork.SMSMailRepository.GetSMSTemplateByMessageType(request[0].MessageType);
                    foreach (var item in request)
                    {
                        try
                        {
                            string ReturnOTP = "";
                            string MessageBody = "";
                            string TempletID = "";
                            string DepartmentName = "Bter";
                            string var = "";
                            if (dataTable.Rows.Count > 0)
                            {
                                MessageBody = Convert.ToString(dataTable.Rows[0]["MessageBody"]);
                                TempletID = Convert.ToString(dataTable.Rows[0]["TemplateID"]);
                            }
                            if (item.MessageType == EnumMessageType.Bter_EnrollmentForStudent.GetDescription())
                            {
                                ReturnOTP = CommonFuncationHelper.SMS_GenerateNewRandom();
                                MessageBody = MessageBody.Replace("{#ApplicationNo#}", item.ApplicationNo)
                                .Replace("{#DepartmentName#}", DepartmentName)
                                .Replace("{#var#}", var);

                                CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, item.MobileNo, MessageBody, TempletID);
                            }
                        }
                        catch { }
                    }

                    //result.Data = ReturnOTP;
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
                    await _unitOfWork.DisposeAsync();
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

            });

        }

        [HttpPost("NorifyStudent_VerifyForExamination")]
        public async Task<ApiResult<bool>> NorifyStudent_VerifyForExamination([FromBody] List<ForSMSNotifyStudentModel> request)
        {
            ActionName = "NorifyStudent_VerifyForExamination([FromBody] List<ForSMSNotifyStudentModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    DataTable dataTable = await _unitOfWork.SMSMailRepository.GetSMSTemplateByMessageType(request[0].MessageType);
                    foreach (var item in request)
                    {
                        try
                        {
                            string ReturnOTP = "";
                            string MessageBody = "";
                            string TempletID = "";
                            string DepartmentName = "Bter";
                            string var = "";
                            if (dataTable.Rows.Count > 0)
                            {
                                MessageBody = Convert.ToString(dataTable.Rows[0]["MessageBody"]);
                                TempletID = Convert.ToString(dataTable.Rows[0]["TemplateID"]);
                            }
                            if (item.MessageType == EnumMessageType.Exam_Fee_Reminder.GetDescription())
                            {
                                //MessageBody = MessageBody.Replace("{#ApplicationNo#}", Convert.ToString(item.StudentName))
                                //   .Replace("{#Scheme#}", Convert.ToString(item.StudentName))
                                //   .Replace("{#DepartmentName#}", Convert.ToString(item.StudentName))
                                //   .Replace("{#var#}", Convert.ToString(item.StudentName));
                                try
                                {
                                    var mobile = Convert.ToString(item.MobileNo);
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
                        catch { }
                    }

                    //result.Data = ReturnOTP;
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
                    await _unitOfWork.DisposeAsync();
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

            });

        }

        [HttpPost("NorifyStudent_VerifyForEnrollment")]
        public async Task<ApiResult<bool>> NorifyStudent_VerifyForEnrollment([FromBody] List<ForSMSNotifyStudentModel> request)
        {
            ActionName = "NorifyStudent_VerifyForEnrollment([FromBody] List<ForSMSNotifyStudentModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    DataTable dataTable = await _unitOfWork.SMSMailRepository.GetSMSTemplateByMessageType(request[0].MessageType);
                    foreach (var item in request)
                    {
                        try
                        {
                            string ReturnOTP = "";
                            string MessageBody = "";
                            string TempletID = "";
                            string DepartmentName = "Bter";
                            string var = "";
                            if (dataTable.Rows.Count > 0)
                            {
                                MessageBody = Convert.ToString(dataTable.Rows[0]["MessageBody"]);
                                TempletID = Convert.ToString(dataTable.Rows[0]["TemplateID"]);
                            }
                            if (item.MessageType == EnumMessageType.Exam_Fee_Reminder.GetDescription())
                            {


                                //MessageBody = MessageBody.Replace("{#ApplicationNo#}", Convert.ToString(item.StudentName))
                                //   .Replace("{#Scheme#}", Convert.ToString(item.StudentName))
                                //   .Replace("{#DepartmentName#}", Convert.ToString(item.StudentName))
                                //   .Replace("{#var#}", Convert.ToString(item.StudentName));
                                try
                                {
                                    var mobile = Convert.ToString(item.MobileNo);
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
                        catch { }
                    }

                    //result.Data = ReturnOTP;
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
                    await _unitOfWork.DisposeAsync();
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

            });

        }

        [HttpPost("NorifyStudent_PlacementShortlist")]
        public async Task<ApiResult<bool>> NorifyStudent_PlacementShortlist([FromBody] List<ForSMSNotifyStudentPlacementShorlistModel> request)
        {
            ActionName = "NorifyStudent_PlacementShortlist([FromBody] List<ForSMSNotifyStudentPlacementShorlistModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await Task.Run(() => _unitOfWork.PlacementShortListStudentRepository.SaveShortlistNotifyHistory(request));
                    DataTable dataTable = await _unitOfWork.SMSMailRepository.GetSMSTemplateByMessageType(request[0].MessageType);
                    foreach (var item in request)
                    {
                        try
                        {
                            string ReturnOTP = "";
                            string MessageBody = "";
                            string TempletID = "";
                            string DepartmentName = "Bter";
                            string var = "";
                            if (dataTable.Rows.Count > 0)
                            {
                                MessageBody = Convert.ToString(dataTable.Rows[0]["MessageBody"]);
                                TempletID = Convert.ToString(dataTable.Rows[0]["TemplateID"]);
                            }
                            if (item.MessageType == EnumMessageType.Bter_StudentShortList.GetDescription())
                            {
                                try
                                {
                                    var mobile = Convert.ToString(item.MobileNo);
                                    if (mobile != null)
                                    {
                                        MessageBody = MessageBody.Replace("{#EnrollmentNo#}", item.EnrollmentNo)
                                            .Replace("{#RoundNo#}", item.RoundNo.ToString());

                                        CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, mobile, MessageBody, TempletID);//add in que
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        catch { }
                    }

                    //result.Data = ReturnOTP;
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
                    await _unitOfWork.DisposeAsync();
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

            });

        }


        [HttpPost("NorifyStudent_PlacementSelected")]
        public async Task<ApiResult<bool>> NorifyStudent_PlacementSelected([FromBody] List<ForSMSNotifyStudentPlacementShorlistModel> request)
        {
            ActionName = "NorifyStudent_PlacementSelected([FromBody] List<ForSMSNotifyStudentPlacementShorlistModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await Task.Run(() => _unitOfWork.PlacementSelectedStudentRepository.SaveNotifyHistory(request));
                    await _unitOfWork.SaveChangesAsync();
                    DataTable dataTable = await _unitOfWork.SMSMailRepository.GetSMSTemplateByMessageType(request[0].MessageType);
                    foreach (var item in request)
                    {
                        try
                        {
                            string ReturnOTP = "";
                            string MessageBody = "";
                            string TempletID = "";
                            string DepartmentName = "Bter";
                            string var = "";
                            if (dataTable.Rows.Count > 0)
                            {
                                MessageBody = Convert.ToString(dataTable.Rows[0]["MessageBody"]);
                                TempletID = Convert.ToString(dataTable.Rows[0]["TemplateID"]);
                            }
                            if (item.MessageType == EnumMessageType.Bter_StudentShortList.GetDescription())
                            {
                                try
                                {
                                    var mobile = Convert.ToString(item.MobileNo);
                                    if (mobile != null)
                                    {
                                        MessageBody = MessageBody.Replace("{#EnrollmentNo#}", item.EnrollmentNo)
                                            .Replace("{#RoundNo#}", item.RoundNo.ToString());

                                        CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, mobile, MessageBody, TempletID);//add in que
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        catch { }
                    }

                    //result.Data = ReturnOTP;
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
                    await _unitOfWork.DisposeAsync();
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

            });

        }

        [HttpPost("SendEmail")]
        public async Task<ApiResult<bool>> SendEmail([FromBody] List<ForSMSEnrollmentStudentMarkedModel> request)
        {
            ActionName = "SendEmail([FromBody] List<ForSMSEnrollmentStudentMarkedModel> request)";

            return await Task.Run(async () =>
            {
                EmailTemplate ObjET = new EmailTemplate();
                var result = new ApiResult<bool>();
                try
                {
                    DataTable dataTable = await _unitOfWork.SMSMailRepository.GetEmailTemplateByTemplateCode(request[0].TemplateCode!);

                    try
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            if (dataTable.Rows.Count > 0)
                            {
                                ObjET.ID = Convert.ToInt16(dataTable.Rows[0]["ID"]);
                                ObjET.TemplateName = dataTable.Rows[0]["TemplateName"].ToString()!;
                                ObjET.EmailSubject = dataTable.Rows[0]["EmailSubject"].ToString()!;
                                ObjET.EmailBody = dataTable.Rows[0]["EmailBody"].ToString()!;
                                ObjET.ToQuery = dataTable.Rows[0]["ToQuery"].ToString();
                                ObjET.CcQuery = dataTable.Rows[0]["CcQuery"].ToString();
                                ObjET.BccQuery = dataTable.Rows[0]["BccQuery"].ToString();
                                ObjET.DataQuery = Convert.ToString(dataTable.Rows[0]["DataQuery"]);
                                ObjET.EmailAttachment = dataTable.Rows[0]["EmailAttachment"].ToString();
                            }
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = "Email Template Not found.!";
                        }

                        DataTable InnerdataTable = await _unitOfWork.SMSMailRepository.GetDynamicData(ObjET.DataQuery!);

                        EmailSettings OBJES = new EmailSettings();

                        foreach (DataRow DR in InnerdataTable.Rows)
                        {
                            try
                            {
                                ObjET.EmailBody = ObjET.EmailBody;

                                MailMessage message = new MailMessage(OBJES.FromEmail, DR["EmailId"].ToString()!, ObjET.EmailSubject, ObjET.EmailBody);
                                message.IsBodyHtml = true;

                                message.Attachments.Add(new Attachment("EmailAttachment"));
                                SmtpClient smtp = new SmtpClient();
                                NetworkCredential basicCredential = new NetworkCredential(OBJES.UserName, OBJES.Password);
                                smtp.Host = OBJES.Host;
                                smtp.Port = OBJES.Port;
                                smtp.EnableSsl = OBJES.EnableSsl;
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = basicCredential;
                                smtp.Send(message);
                            }
                            catch { }

                        }

                    }
                    catch { }


                    //result.Data = ReturnOTP;

                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;

            });

        }

        [HttpPost("SendEmail_New")]
        public async Task<ApiResult<bool>> SendEmail_New([FromBody] List<ForSMSEnrollmentStudentMarkedModel> request)
        {
            ActionName = "SendEmail([FromBody] List<ForSMSEnrollmentStudentMarkedModel> request)";
            var result = new ApiResult<bool>();
            long k = 0;
            try
            {
                if (request == null || request.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "Request data not found.";
                    return result;
                }

                // ============================================================
                // 1. Get Email Template
                // ============================================================

                EmailTemplate ObjET = new EmailTemplate();

                DataTable templateTable =
                    await _unitOfWork.SMSMailRepository
                        .GetEmailTemplateByTemplateCode(
                            request[0].TemplateCode!);

                if (templateTable == null || templateTable.Rows.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "Email Template Not found.!";
                    return result;
                }

                DataRow templateRow = templateTable.Rows[0];

                ObjET.ID = Convert.ToInt32(templateRow["ID"]);
                ObjET.TemplateName = Convert.ToString(templateRow["TemplateName"]) ?? "";
                ObjET.EmailSubject = Convert.ToString(templateRow["EmailSubject"]) ?? "";
                ObjET.EmailBody = Convert.ToString(templateRow["EmailBody"]) ?? "";
                ObjET.ToQuery = Convert.ToString(templateRow["ToQuery"]) ?? "";
                ObjET.CcQuery = Convert.ToString(templateRow["CcQuery"]) ?? "";
                ObjET.BccQuery = Convert.ToString(templateRow["BccQuery"]) ?? "";
                ObjET.DataQuery = Convert.ToString(templateRow["DataQuery"]) ?? "";
                ObjET.EmailAttachment = Convert.ToString(templateRow["EmailAttachment"]) ?? "";

                // ============================================================
                // 2. Get Dynamic Data From DB
                // ============================================================

                DataTable dynamicDataTable =
                    await _unitOfWork.SMSMailRepository
                        .GetDynamicData(ObjET.DataQuery);


                // ============================================================
                // 3. Email Settings
                // ============================================================

                EmailSettings OBJES = new EmailSettings
                {
                    Host = ConfigurationHelper.SMTPHost,
                    Port = ConfigurationHelper.SMTPPort,
                    UserName = ConfigurationHelper.SMTPUsername,
                    Password = ConfigurationHelper.SMTPPassword,
                    EnableSsl = ConfigurationHelper.EnableSsl,
                    FromEmail = ConfigurationHelper.SMTPFromEmail
                };


                // ============================================================
                // 4. Send Email For Each Request
                // ============================================================

                foreach (var item in request)
                {
                    try
                    {
                        // ----------------------------------------------------
                        // IMPORTANT:
                        // Always use original template body
                        // ----------------------------------------------------

                        string emailBody = ObjET.EmailBody;
                        string emailSubject = ObjET.EmailSubject;

                        // ----------------------------------------------------
                        // Replace Dynamic DB Values
                        // ----------------------------------------------------

                        if (dynamicDataTable != null && dynamicDataTable.Rows.Count > 0)
                        {
                            //DataRow dataRow = dynamicDataTable.Rows[0];
                            foreach (DataRow DR in dynamicDataTable.Rows)
                            {
                                foreach (DataColumn column in dynamicDataTable.Columns)
                                {
                                    string columnName = column.ColumnName;
                                    string value = Convert.ToString(DR[column]) ?? "";
                                    emailBody = emailBody.Replace("{{" + columnName + "}}", value);
                                    emailSubject = emailSubject.Replace("{{" + columnName + "}}", value);
                                }
                                // ====================================================
                                // 5. Get To / CC / BCC
                                // ====================================================

                                string toEmail = "";
                                string ccEmail = "";
                                string bccEmail = "";

                                if (!string.IsNullOrWhiteSpace(DR["Email"].ToString()))
                                {
                                    toEmail = Convert.ToString(DR["Email"]) ?? "";
                                }

                                if (!string.IsNullOrWhiteSpace(ObjET.CcQuery))
                                {
                                    ccEmail = Convert.ToString(ObjET.CcQuery);
                                }

                                if (!string.IsNullOrWhiteSpace(ObjET.BccQuery))
                                {
                                    bccEmail = Convert.ToString(ObjET.BccQuery);
                                }

                                // ====================================================
                                // 6. If To Email Not Found
                                // ====================================================

                                if (string.IsNullOrWhiteSpace(toEmail))
                                {
                                    continue;
                                }

                                // ====================================================
                                // 7. Create Mail
                                // ====================================================

                                using (MailMessage message = new MailMessage())
                                {
                                    message.From = new MailAddress(OBJES.FromEmail);

                                    message.Subject = emailSubject;
                                    message.Body = emailBody;
                                    message.IsBodyHtml = true;

                                    // ------------------------------------------------
                                    // TO
                                    // ------------------------------------------------

                                    AddEmails(message.To, toEmail);

                                    // ------------------------------------------------
                                    // CC
                                    // ------------------------------------------------

                                    AddEmails(message.CC, ccEmail);

                                    // ------------------------------------------------
                                    // BCC
                                    // ------------------------------------------------

                                    AddEmails(message.Bcc, bccEmail);

                                    // =================================================
                                    // 8. Attachment
                                    // =================================================

                                    if (!string.IsNullOrWhiteSpace(ObjET.EmailAttachment))
                                    {
                                        string attachmentPath = ObjET.EmailAttachment;
                                        // Check file
                                        if (System.IO.File.Exists(attachmentPath))
                                        {
                                            message.Attachments.Add(new Attachment(attachmentPath));
                                        }
                                    }
                                    else
                                    {
                                        if (dynamicDataTable.Columns.Contains("Doc") && !string.IsNullOrWhiteSpace(DR["Doc"].ToString()))
                                        {
                                            if (System.IO.File.Exists(DR["Doc"].ToString()))
                                            {
                                                message.Attachments.Add(new Attachment(DR["Doc"].ToString()!));
                                            }
                                        }
                                    }

                                    // =================================================
                                    // 9. SMTP
                                    // =================================================

                                    using (SmtpClient smtp = new SmtpClient())
                                    {
                                        smtp.Host = OBJES.Host;
                                        smtp.Port = OBJES.Port;
                                        smtp.EnableSsl = OBJES.EnableSsl;
                                        smtp.UseDefaultCredentials = false;

                                        smtp.Credentials = new NetworkCredential(OBJES.UserName, OBJES.Password);
                                        //var k= await smtp.SendMailAsync(message);

                                        try
                                        {
                                            await smtp.SendMailAsync(message);
                                            // ============================================================
                                            // 10. Success                                            
                                            k = await SaveEmailLogs(message, templateRow["TemplateName"].ToString()!, "Email is sent successfully");

                                            // ============================================================

                                            result.Data = k > 0 ? true : false;
                                            result.State = k > 0 ? EnumStatus.Success : EnumStatus.Error;
                                            result.Message = k > 0 ? "Email logged successfully." : "Something went wrong.";

                                        }
                                        catch (Exception ex)
                                        {
                                            // ============================================================
                                            // 11. Error
                                            // ============================================================
                                            k = await SaveEmailLogs(message, templateRow["TemplateName"].ToString()!, ex.Message);
                                            result.State = EnumStatus.Error;
                                            result.ErrorMessage = ex.Message;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (SmtpException ex)
                    {
                        result.Data = false;
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = ex.Message;
                    }
                    catch (Exception emailEx)
                    {
                        // Don't hide email error
                        var nex = new NewException
                        {
                            PageName = PageName,
                            ActionName = ActionName,
                            Ex = emailEx
                        };

                        await CreateErrorLog(
                            nex,
                            _unitOfWork);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(
                    nex,
                    _unitOfWork);

                return result;
            }
        }


        private async Task<long> SaveEmailLogs(MailMessage message, string TemplateName, string Status)
        {
            return await _unitOfWork.SMSMailRepository.SaveEmailLog(new EmailLog
            {
                TemplateCode = TemplateName,
                ToEmail = string.Join(",", message.To.Select(x => x.Address)),
                CcEmail = string.Join(",", message.CC.Select(x => x.Address)),
                BccEmail = string.Join(",", message.Bcc.Select(x => x.Address)),
                EmailSubject = message.Subject,
                EmailBody = message.Body,
                EmailAttachment = string.Join(",", message.Attachments.Select(x => x.Name)),
                EmailStatus = Status,
                ErrorMessage = null,
                ReferenceID = 0,//referenceId,
                SentDate = DateTime.Now
            });

        }


        private string ReplaceVariables(string text, Dictionary<string, object> data)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return Regex.Replace(
                text,
                @"\{\{(.*?)\}\}",
                match =>
                {
                    string key = match.Groups[1]
                        .Value
                        .Trim();

                    return data.TryGetValue(key, out var value)
                        ? value?.ToString() ?? ""
                        : match.Value;
                });
        }

        private void AddEmails(MailAddressCollection collection, string? emails)
        {
            if (string.IsNullOrWhiteSpace(emails))
                return;

            string[] emailList = emails
                .Split(
                    new[] { ';', ',' },
                    StringSplitOptions.RemoveEmptyEntries);

            foreach (string email in emailList)
            {
                string value = email.Trim();

                if (!string.IsNullOrWhiteSpace(value))
                {
                    collection.Add(
                        new MailAddress(value));
                }
            }
        }

    }
}