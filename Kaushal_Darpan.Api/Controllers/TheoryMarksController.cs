using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.GroupMaster;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.TheoryMarks;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    public class TheoryMarksController : BaseController
    {
        public override string PageName => "TheoryMarksController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TheoryMarksController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("GetTheoryMarksDetailList")]
        public async Task<ApiResult<DataTable>> GetTheoryMarksDetailList([FromBody] TheorySearchModel body)
        {
            ActionName = "GetTheoryMarksDetailList([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TheoryMarksRepository.GetTheoryMarksDetailList(body);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
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

        [HttpPost("UpdateSaveData")]
        public async Task<ApiResult<bool>> UpdateSaveData([FromBody] List<TheoryMarksModel> request)
        {
            ActionName = "UpdateSaveData([FromBody] List<TheoryMarksModel> request)";
            var result = new ApiResult<bool>();
            try
            {
                request.ForEach(x =>
                {
                    x.IPAddress = CommonFuncationHelper.GetIpAddress();
                });
                // Pass the list to the repository for batch update
                var isSave = await Task.Run(() => _unitOfWork.TheoryMarksRepository.UpdateSaveData(request));
                await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                if (isSave > 0)
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
        }

        [HttpPost("GetTheoryMarksRptData")]
        public async Task<ApiResult<DataTable>> GetTheoryMarksRptData([FromBody] TheorySearchModel body)
        {
            ActionName = "GetTheoryMarksRptData([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TheoryMarksRepository.GetTheoryMarksRptData(body));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
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

        [HttpPost("FeedbackSubmit")]
        public async Task<ApiResult<bool>> FeedbackSubmit([FromBody] ExaminerFeedbackDataModel request)
        {
            ActionName = "FeedbackSubmit([FromBody] ExaminerFeedbackDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.TheoryMarksRepository.FeedbackSubmit(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave > 0)
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

        [HttpPost("GetTheoryMarks_Admin")]
        public async Task<ApiResult<DataTable>> GetTheoryMarks_Admin([FromBody] TheorySearchModel body)
        {
            ActionName = "GetTheoryMarks_Admin([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TheoryMarksRepository.GetTheoryMarks_Admin(body));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
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

        [HttpPost("UpdateTheoryMarks_Admin")]
        public async Task<ApiResult<bool>> UpdateTheoryMarks_Admin([FromBody] List<TheoryMarksModel> request)
        {
            ActionName = "UpdateTheoryMarks_Admin([FromBody] List<TheoryMarksModel> request)";
            var result = new ApiResult<bool>();
            try
            {
                request.ForEach(x =>
                {
                    x.IPAddress = CommonFuncationHelper.GetIpAddress();
                });
                // Pass the list to the repository for batch update
                var isSave = await Task.Run(() => _unitOfWork.TheoryMarksRepository.UpdateTheoryMarks_Admin(request));
                await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                if (isSave > 0)
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
        }

        [HttpPost("GetUFMStudentExtraInfo")]
        public async Task<ApiResult<UFMStudentExtraInfoSaveModel>> GetUFMStudentExtraInfo([FromBody] UFMStudentExtraInfoGetModel body)
        {
            ActionName = "GetUFMStudentExtraInfo([FromBody] UFMStudentExtraInfoGetModel body)";
            var result = new ApiResult<UFMStudentExtraInfoSaveModel>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TheoryMarksRepository.GetUFMStudentExtraInfo(body));
                if (result.Data == null)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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

        [HttpPost("SaveUFMStudentExtraInfo")]
        public async Task<ApiResult<bool>> SaveUFMStudentExtraInfo([FromBody] UFMStudentExtraInfoSaveModel model)
        {
            ActionName = "SaveUFMStudentExtraInfo([FromBody] UFMStudentExtraInfoSaveModel model)";
            var result = new ApiResult<bool>();
            try
            {
                if (model == null || model.StudentID == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_VALIDATION_FAILED;
                    return result;
                }

                model.IPAddress = CommonFuncationHelper.GetIpAddress();

                // Pass the list to the repository for batch update
                var isSave = await Task.Run(() => _unitOfWork.TheoryMarksRepository.SaveUFMStudentExtraInfo(model));
                await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                if (isSave > 0)
                {
                    result.Data = true;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_ADD_ERROR;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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
        }

        [HttpPost("SaveUFMExtraInfo")]
        public async Task<ApiResult<bool>> SaveUFMExtraInfo([FromBody] UFMExtraInfoSaveModel model)
        {
            ActionName = "SaveUFMExtraInfo([FromBody] UFMExtraInfoSaveModel model)";
            var result = new ApiResult<bool>();
            try
            {
                if (model == null)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_VALIDATION_FAILED;
                    return result;
                }

                model.IPAddress = CommonFuncationHelper.GetIpAddress();

                // Pass the list to the repository for batch update
                var isSave = await Task.Run(() => _unitOfWork.TheoryMarksRepository.SaveUFMExtraInfo(model));
                await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                if (isSave > 0)
                {
                    result.Data = true;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_ADD_ERROR;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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
        }


        [HttpPost("UpdateUFMCategory")]
        public async Task<ApiResult<bool>> UpdateUFMCategory([FromBody] UFMCategoryUpdateModel model)
        {
            ActionName = "UpdateUFMCategory([FromBody] UFMCategoryUpdateModel model)";
            var result = new ApiResult<bool>();
            try
            {
                if (model == null)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_VALIDATION_FAILED;
                    return result;
                }

                model.IPAddress = CommonFuncationHelper.GetIpAddress();

                // Pass the list to the repository for batch update
                var isSave = await Task.Run(() => _unitOfWork.TheoryMarksRepository.UpdateUFMCategory(model));
                await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                if (isSave > 0)
                {
                    result.Data = true;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_UPDATE_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_ADD_ERROR;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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
        }

        [HttpPost("GetAllExaminerReport")]
        public async Task<ApiResult<DataTable>> GetAllExaminerReport([FromBody] ExaminerReportSearchModel body)
        {
            ActionName = "GetAllExaminerReport([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TheoryMarksRepository.GetAllExaminerReport(body));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
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
    }
}
