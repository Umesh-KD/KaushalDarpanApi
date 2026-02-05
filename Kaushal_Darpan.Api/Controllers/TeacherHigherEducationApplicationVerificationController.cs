using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.PlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.Test;
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

        public TeacherHigherEducationApplicationVerificationController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            string ActionName = "GetApplication_GenrateOrder_Dte_THTE";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var dt = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.GetApplication_GenrateOrder_Dte_THTE(model);

                    if (dt?.Rows.Count > 0)
                    {
                        // Optional: Remove duplicate consecutive values in rows if you want
                        // You can keep your existing logic here, but if not needed, skip

                        // Build HTML from data
                        string html = BuildGroupedApplicationOrderHtml(dt);

                        // Optional: Log the HTML for debug
                        var ex = new Exception(html);
                        var nex = new NewException
                        {
                            PageName = "THTEGenerateOrderPDF",
                            ActionName = ActionName,
                            Ex = ex,
                        };
                        await CreateErrorLog(nex, _unitOfWork);

                        // Convert to Krutidev or special font if you want (your existing logic)
                        string devFontSize = "15px";
                        var sb1 = new StringBuilder();
                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        // Log again after conversion
                        var ex1 = new Exception(sb1.ToString());
                        var nex1 = new NewException
                        {
                            PageName = "THTEGenerateOrderPDF",
                            ActionName = ActionName,
                            Ex = ex1,
                        };
                        await CreateErrorLog(nex1, _unitOfWork);

                        // Generate PDF bytes
                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "Landscape", "");

                        // Return base64 encoded pdf
                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
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


        public static string BuildGroupedApplicationOrderHtml(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            // CSS style for table
            sb.AppendLine(@"
<style>
    table {
        border-collapse: collapse;
        font-family: Arial, sans-serif;
        font-size: 12px;
        width: 100%;
    }
    th, td {
        border: 1px solid #ddd;
        padding: 6px;
    }
    th {
        background-color: #f0f0f0;
    }
</style>");

            sb.AppendLine("<table>");

            // Table header
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>SSO ID</th>");
            sb.AppendLine("<th>Teacher Name</th>");
            sb.AppendLine("<th>DOB</th>");
            sb.AppendLine("<th>Joining Date</th>");
            sb.AppendLine("<th>Applied Course Name</th>");
            sb.AppendLine("<th>Applied Institute</th>");
            sb.AppendLine("<th>Committee Name</th>");
            sb.AppendLine("<th>Status Name</th>");
            sb.AppendLine("<th>Remark</th>");
            sb.AppendLine("<th>Created Date</th>");
            sb.AppendLine("</tr>");

            // Helper local function for safe HTML encoding
            string SafeHtmlEncode(DataRow row, string columnName)
            {
                if (dt.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
                {
                    return System.Net.WebUtility.HtmlEncode(row[columnName].ToString());
                }
                return "";
            }

            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine("<tr>");

               
                string SSOID = SafeHtmlEncode(row, "SSOID");
                string TeacherName = SafeHtmlEncode(row, "TeacherName");
                string DOB = SafeHtmlEncode(row, "DOB");
                string JoiningDate = SafeHtmlEncode(row, "JoiningDate");
                string appliedCourseName = SafeHtmlEncode(row, "AppliedCourseName");
                string appliedInstitute = SafeHtmlEncode(row, "AppliedInstitute");
                string statusName = SafeHtmlEncode(row, "StatusName");
                string remark = SafeHtmlEncode(row, "Remark");
                string CommitteeName = SafeHtmlEncode(row, "CommitteeName");

                string createdDateStr = "";
                if (dt.Columns.Contains("CreatedDate") && row["CreatedDate"] != DBNull.Value)
                {
                    if (DateTime.TryParse(row["CreatedDate"].ToString(), out DateTime createdDate))
                    {
                        createdDateStr = createdDate.ToString("dd-MM-yyyy");
                    }
                }

               
                sb.AppendLine($"<td>{SSOID}</td>");
                sb.AppendLine($"<td>{TeacherName}</td>");
                sb.AppendLine($"<td>{DOB}</td>");
                sb.AppendLine($"<td>{JoiningDate}</td>");
                sb.AppendLine($"<td>{appliedCourseName}</td>");
                sb.AppendLine($"<td>{appliedInstitute}</td>");
                sb.AppendLine($"<td>{CommitteeName}</td>");
                sb.AppendLine($"<td>{statusName}</td>");
                sb.AppendLine($"<td>{remark}</td>");
                sb.AppendLine($"<td>{createdDateStr}</td>");

                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table><br/>");

            return sb.ToString();
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
    }
}
