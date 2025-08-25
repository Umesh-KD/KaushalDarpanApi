using AspNetCore.Reporting;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using ExcelDataReader;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.ITIPracticalExaminer;
using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.TimeTable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class ITIInvigilatorController : BaseController
    {
        public override string PageName => "ITIInvigilatorController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DBContext _dbContext;
        private string _sqlQuery;
        public ITIInvigilatorController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] TimeTableSearchModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIInvigilatorRepository.GetAllData(model);
                if (result.Data.Rows.Count > 0)
                {
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

        [HttpPost("SaveInvigilator")]
        public async Task<ApiResult<bool>> SaveInvigilator([FromBody] ItiInvigilatorDataModel request)
        {
            ActionName = "SaveInvigilator([FromBody] ItiInvigilatorDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    var isSave = await _unitOfWork.ITIInvigilatorRepository.SaveInvigilator(request);
                    _unitOfWork.SaveChanges();

                    if (isSave == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    if (isSave == -6)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_AlreadyAssigned;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
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

        [HttpPost("GetAllInvigilator")]
        public async Task<ApiResult<DataTable>> GetAllInvigilator([FromBody] ItiInvigilatorSearchModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIInvigilatorRepository.GetAllInvigilator(model);
                if (result.Data.Rows.Count > 0)
                {
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



        [HttpPost("GetAllTheoryStudents")]
        public async Task<ApiResult<DataTable>> GetAllTheoryStudents([FromBody] ItiTheoryStudentMaster model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIInvigilatorRepository.GetAllTheoryStudents(model);
                if (result.Data.Rows.Count > 0)
                {
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


        [HttpPost("GetInvigilatorData_UserWise")]
        public async Task<ApiResult<DataTable>> GetInvigilatorData_UserWise([FromBody] ItiInvigilatorDataModel model)
        {
            ActionName = "GetInvigilatorData_UserWise()";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ITIInvigilatorRepository.GetInvigilatorData_UserWise(model.DepartmentID ?? 0, model.SemesterID,model.InstituteID, model.EndTermID ?? 0,model.ShiftID,model.Eng_NonEng ?? 0,model.UserID);

                if (result.Data != null && result.Data.Rows.Count > 0)
                {
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
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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

        [HttpPost("GetTheoryStudentsByRollRange")]
        public async Task<ApiResult<DataTable>> GetTheoryStudentsByRollRange([FromBody] ItiInvigilatorDataModel model)
        {
            ActionName = "GetTheoryStudentsByRollRange()";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ITIInvigilatorRepository.GetTheoryStudentsByRollRangeAsync(model);

                if (result.Data != null && result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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



        [HttpPost("SaveIsPresentData")]
        public async Task<ApiResult<int>> SaveIsPresentData([FromBody] List<StudentExamMarksUpdateModel> entityList)
        {
            ActionName = "SaveIsPresentData(List<StudentExamMarksUpdateModel>)";
            var result = new ApiResult<int>();

            try
            {
                result.Data = await _unitOfWork.ITIInvigilatorRepository.SaveIsPresentData(entityList);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_UPDATE_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_UPDATE_ERROR;
                }
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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


        [HttpPost("ITIInvigilatorDashboard")]
        public async Task<ApiResult<DataTable>> ITIInvigilatorDashboard([FromBody] ItiInvigilatorSearchModel filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIInvigilatorRepository.ITIInvigilatorDashboard(filterModel);
                if (result.Data.Rows.Count > 0)
                {
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


        [HttpPost("Iti_InvigilatorPaymentGenerateAndViewPdf")]
        //[RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<IActionResult> Iti_InvigilatorPaymentGenerateAndViewPdf([FromBody] ITI_InvigilatorPDFViewModal filterModel)
        {
            ActionName = "GenerateAndViewPdf([FromBody] RenumerationExaminerRequestModel filterModel)";

            try
            {
                var data = await _unitOfWork.ITIInvigilatorRepository.Iti_InvigilatorPaymentGenerateAndViewPdf(filterModel);
                if (data?.Rows?.Count > 0)
                {
                    //rdlc
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderITI, "ITIRemunerationInvigilator.rdlc");

                    var newFileName = $"RemunerationInvigilator_{DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}.pdf";
                    string folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder_ITI}{Constants.RemunerationFolder}";

                    //rpt
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcPath);
                    localReport.AddDataSource("Remuneration", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save
                    System.IO.File.WriteAllBytes(Path.Combine(folderPath, newFileName), reportResult.MainStream);

                    //file stream
                    return File(reportResult.MainStream, "application/pdf", newFileName);
                }
                else
                {
                    return Content("No data available to generate the PDF.");
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //
                return Content("An error occurred while generating the PDF.");
            }
        }



        [HttpPost("Iti_InvigilatorSubmitandForwardToAdmin")]
        public async Task<ApiResult<DataTable>> Iti_InvigilatorSubmitandForwardToAdmin([FromBody] ITI_InvigilatorPDFForwardModal filterModel)
        {
            ActionName = "Iti_InvigilatorSubmitandForwardToAdmin()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIInvigilatorRepository.Iti_InvigilatorSubmitandForwardToAdmin(filterModel);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                _unitOfWork.Dispose();
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

        [HttpPost("GetItiRemunerationInvigilatorAdminDetails")]
        public async Task<ApiResult<DataTable>> GetItiRemunerationInvigilatorAdminDetails([FromBody] ITI_AdminInvigilatorRemunerationDetailModal filterModel)
        {
            ActionName = "Iti_InvigilatorSubmitandForwardToAdmin()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIInvigilatorRepository.GetItiRemunerationInvigilatorAdminDetails(filterModel);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                _unitOfWork.Dispose();
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


        [HttpPost("UpdateToApprove")]
         public async Task<ApiResult<bool>> UpdateToApprove([FromBody] ITI_AdminInvigilatorRemunerationDetailModal filterModel)
        {
            ActionName = "UpdateToApprove( int RemunerationID)";
            var result = new ApiResult<bool>();
            try
            {
                var data = await _unitOfWork.ITIInvigilatorRepository.UpdateToApprove(filterModel);
                _unitOfWork.SaveChanges();
                //var objData = CommonFuncationHelper.ConvertDataTable<RenumerationExaminerPDFModel>(data);
                if (data == 2)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Update Successfully";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
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



        [HttpPost("GetinvigilatorDetailbyRemunerationID/{RemunerationID}")]
        public async Task<ApiResult<DataTable>> GetinvigilatorDetailbyRemunerationID( int RemunerationID =0 )
        {
            ActionName = "GetinvigilatorDetailbyRemunerationID( RemunerationID)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIInvigilatorRepository.GetinvigilatorDetailbyRemunerationID(RemunerationID);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                _unitOfWork.Dispose();
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



        [HttpPost("GetRemunerationApproveList")]
        public async Task<ApiResult<DataTable>> GetRemunerationApproveList([FromBody] ITI_AdminInvigilatorRemunerationDetailModal filterModel)
        {
            ActionName = "Iti_InvigilatorSubmitandForwardToAdmin()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIInvigilatorRepository.GetRemunerationApproveList(filterModel);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                _unitOfWork.Dispose();
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
