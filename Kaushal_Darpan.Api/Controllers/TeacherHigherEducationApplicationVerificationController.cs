using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Code.Helper;
using Kaushal_Darpan.Api.HtmlTempleteFile;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.PlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Report;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.Test;
using Kaushal_Darpan.Models.TimeTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Text;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class TeacherHigherEducationApplicationVerificationController : BaseController
    {
        public override string PageName => "TeacherHigherEducationApplicationVerificationController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConverter _converter;
        private readonly IPrintHtmlFile _printHtmlFile;

        public TeacherHigherEducationApplicationVerificationController(IMapper mapper, 
            IUnitOfWork unitOfWork,
            IConverter converter, 
            IPrintHtmlFile printHtmlFile)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _converter = converter;
            _printHtmlFile = printHtmlFile;
        }

        [RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpPost("GetEnrolledStudent_Promoted")]
        public async Task<ApiResult<DataTable>> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)
        {
            ActionName = "GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.GetEnrolledStudent_Promoted(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        [RoleActionFilter(EnumRole.ExaminationIncharge, EnumRole.ExaminationIncharge_NonEng)]
        [HttpPost("SaveEnrolledStudentVerify_ReturnbyExamIncharge")]
        public async Task<ApiResult<bool>> SaveEnrolledStudentVerify_ReturnbyExamIncharge([FromBody] List<EnrolledPromotedStudentSaveModel> request)
        {
            ActionName = "SaveEnrolledStudentVerify_ReturnbyExamIncharge([FromBody] List<EnrolledPromotedStudentSaveModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // regular subject
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.SaveEnrolledStudentVerify_ReturnbyExamIncharge(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;

                    // Log the error
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

        [RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        [HttpPost("ApplicationList_ForPrinciple_THTE")]
        public async Task<ApiResult<DataTable>> ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)
        {
            ActionName = "ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.ApplicationList_ForPrinciple_THTE(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        [RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        [HttpPost("UpdateApplicationStatus_Principle_THTE")]
        public async Task<ApiResult<bool>> UpdateApplicationStatus_Principle_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)
        {
            ActionName = "UpdateApplicationStatus_Principle_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.UpdateApplicationStatus_Principle_THTE(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;

                    // Log the error
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



        [RoleActionFilter(EnumRole.DTE_Eng, EnumRole.DTE_NonEng, EnumRole.CommitteeInchargeDTE)]
        [HttpPost("ApplicationList_ForDTE_THTE")]
        public async Task<ApiResult<DataTable>> ApplicationList_ForDTE_THTE(PrincipleApplicationListSearchModel model)
        {
            ActionName = "ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.ApplicationList_ForDTE_THTE(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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


        [RoleActionFilter(EnumRole.DTE_Eng, EnumRole.DTE_NonEng, EnumRole.CommitteeInchargeDTE)]
        [HttpPost("UpdateApplicationStatus_DTE_THTE")]
        public async Task<ApiResult<bool>> UpdateApplicationStatus_DTE_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)
        {
            ActionName = "UpdateApplicationStatus_Principle_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.UpdateApplicationStatus_DTE_THTE(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;

                    // Log the error
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





        [HttpPost("GetApplication_GenrateOrder_Dte_THTE")]
        public async Task<ApiResult<string>> GetApplication_GenrateOrder_Dte_THTE([FromBody] ApplicationGenrateOrderByDteListSearchModel model)
        {
            string ActionName = "GetApplication_GenrateOrder_Dte_THTE([FromBody] ApplicationGenrateOrderByDteListSearchModel model)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    // data
                    var ds = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.GetApplication_GenrateOrder_Dte_THTE(model);

                    if (ds == null || ds.Tables.Count < 2 || ds.Tables[0].Rows.Count == 0)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                        return result;
                    }

                    // html
                    var sb = _printHtmlFile.GetHtmlOfApplicationGenrateOrderDteTHTE(ds);
                    var htmlContent = sb?.ToString();

                    // pdf
                    var doc = new HtmlToPdfDocument
                    {
                        GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                        Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = htmlContent,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            //HeaderSettings = new HeaderSettings
                            //{
                            //    HtmUrl = headerFilePath,
                            //    Spacing = 3
                            //},

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                    };

                    byte[] arrbyte = await Task.Run(() => _converter.Convert(doc));
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    result.Data = Convert.ToBase64String(arrbyte);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();

                    // Log error
                    var nex = new NewException
                    {
                        PageName = "GenerateOrderPDF",
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);

                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }

        [HttpPost("GenerateOrderInWord_THTE")]
        public async Task<ApiResult<string>> GenerateOrderInWord_THTE(ApplicationGenrateOrderByDteListSearchModel model)
        {
            ActionName = "GenerateOrderInWord_THTE(ApplicationGenrateOrderByDteListSearchModel model)";
            var result = new ApiResult<string>();
            try
            {
                StringBuilder sb = new StringBuilder();

                // get details
                var data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.GetApplication_GenrateOrder_Dte_THTE(model);


                var _sb = _printHtmlFile.GetHtmlOfApplicationGenrateOrderDteTHTE(data);
               
                sb.Append(_sb);

                // add end html
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                string htmlContent = sb.ToString();
                // convert in word
                var ms = new MemoryStream();

                using (var wordDoc = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Create(
                    ms,
                    DocumentFormat.OpenXml.WordprocessingDocumentType.Document,
                    true))
                {
                    var mainPart = wordDoc.AddMainDocumentPart();
                    mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document(
                        new DocumentFormat.OpenXml.Wordprocessing.Body()
                    );

                    var converter = new HtmlToOpenXml.HtmlConverter(mainPart);


                    // html utf-8
                    await converter.ParseHtml(htmlContent);

                    // Force Hindi font if needed
                    wordDoc.ForceHindiFont();
                }

                ms.Position = 0;


                byte[] pdfBytes = ms.ToArray();

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                //return File(
                //    ms,
                //    "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                //    "timetable.docx"
                //);

            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        //[RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        [HttpPost("ApplicationList_ForCommitteeAfterPrinciple_THTE")]
        public async Task<ApiResult<DataTable>> ApplicationList_ForCommitteeAfterPrinciple_THTE(PrincipleApplicationListSearchModel model)
        {
            ActionName = "ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.ApplicationList_ForCommitteeAfterPrinciple_THTE(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        //[RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        [HttpPost("UpdateApplicationStatus_CommitteeAfterPrinciple_THTE")]
        public async Task<ApiResult<bool>> UpdateApplicationStatus_CommitteeAfterPrinciple_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)
        {
            ActionName = "UpdateApplicationStatus_Principle_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.UpdateApplicationStatus_CommitteeAfterPrinciple_THTE(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;

                    // Log the error
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

        [HttpPost("ApplicationList_ForCommittee_THTE")]
        public async Task<ApiResult<DataTable>> ApplicationList_ForCommittee_THTE(PrincipleApplicationListSearchModel model)
        {
            ActionName = "ApplicationList_ForCommittee_THTE(PrincipleApplicationListSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.ApplicationList_ForCommittee_THTE(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        //[RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        [HttpPost("UpdateApplicationStatus_Committee_THTE")]
        public async Task<ApiResult<bool>> UpdateApplicationStatus_Committee_THTE([FromBody] UpdateApplicationStatusDataModel_Committee request)
        {
            ActionName = "UpdateApplicationStatus_Principle_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.UpdateApplicationStatus_Committee_THTE(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;

                    // Log the error
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

        [RoleActionFilter(EnumRole.DTE_Eng, EnumRole.DTE_NonEng, EnumRole.CommitteeInchargeDTE)]
        [HttpPost("DTECommitteeAssign_THTE")]
        public async Task<ApiResult<bool>> DTECommitteeAssign_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)
        {
            ActionName = "DTECommitteeAssign_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.DTECommitteeAssign_THTE(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;

                    // Log the error
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

        [HttpPost("StaffDetailsPreview_THTE")]
        public async Task<ApiResult<DataTable>> StaffDetailsPreview_THTE(StaffDetailsPreviewDataModel model)
        {
            ActionName = "StaffDetailsPreview_THTE(StaffDetailsPreviewDataModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.StaffDetailsPreview_THTE(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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
