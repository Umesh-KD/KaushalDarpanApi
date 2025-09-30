using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.DocumentDetails;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class CounsellingApplicationFormController : BaseController
    {
        public override string PageName => "CounsellingApplicationFormController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CounsellingApplicationFormController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetApplicationDataByID_Counselling")]
        public async Task<ApiResult<CounsellingApplicationFormDataModel>> GetApplicationDataByID_Counselling(CounsellingApplicationSearchModel searchRequest)
        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<CounsellingApplicationFormDataModel>();
                try
                {
                    var data = await _unitOfWork.CounsellingApplicationFormRepository.GetApplicationDataByID_Counselling(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<CounsellingApplicationFormDataModel>(data);
                        result.Data = mappedData;
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
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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

        [HttpPost("SavePersonalDetails")]
        public async Task<ApiResult<int>> SavePersonalDetails([FromBody] CounsellingApplicationFormDataModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.CounsellingApplicationFormRepository.SavePersonalDetails(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.CandidateID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.CandidateID == 0)
                        {
                            result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        }
                        else
                        {
                            result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        }
                    }
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
            });
        }

        [HttpPost("Counselling_SaveOption")]
        public async Task<ApiResult<bool>> Counselling_SaveOption([FromBody] CounsellingOptionFormDataModel request)
        {
            ActionName = "Counselling_SaveOption([FromBody] List<CounsellingOptionFormDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.CounsellingApplicationFormRepository.Counselling_SaveOption(request);
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

        //[HttpPost("Counselling_GetOptionDetailsByID")]
        //public async Task<ApiResult<DataTable>> Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel model)
        //{
        //    ActionName = "Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel model)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<DataTable>();
        //        try
        //        {
        //            var data = await _unitOfWork.CounsellingApplicationFormRepository.Counselling_GetOptionDetailsByID(model);
        //            if (data != null)
        //            {
        //                var mappedData = _mapper.Map<DataTable>(data);
        //                result.Data = mappedData;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            // Write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        return result;
        //    });
        //}

        [HttpPost("Counselling_GetOptionDetailsByID")]
        public async Task<ApiResult<List<CounsellingOptionFormDataModel>>> Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel searchRequest)
        {
            ActionName = "Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel searchRequest)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CounsellingOptionFormDataModel>>();
                try
                {
                    var data = await _unitOfWork.CounsellingApplicationFormRepository.Counselling_GetOptionDetailsByID(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<List<CounsellingOptionFormDataModel>>(data);
                        result.Data = mappedData;
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
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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

        [HttpPost("Counselling_GetDropdownByAction")]
        public async Task<ApiResult<DataTable>> Counselling_GetDropdownByAction(Counselling_DropdownDataModel model)
        {
            ActionName = " Counselling_GetDropdownByAction(Counselling_DropdownDataModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CounsellingApplicationFormRepository.Counselling_GetDropdownByAction(model);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<DataTable>(data);
                        result.Data = mappedData;
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
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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

        [HttpPost("DeleteOptionByID_Counselling")]
        public async Task<ApiResult<bool>> DeleteOptionByID_Counselling(CounsellingOptionFormDataModel model)
        {
            ActionName = "DeleteOptionByID_Counselling(CounsellingOptionFormDataModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.CounsellingApplicationFormRepository.DeleteOptionByID_Counselling(model);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_DELETE_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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

        [HttpPost("PriorityChange_Counselling")]
        public async Task<ApiResult<bool>> PriorityChange_Counselling(CounsellingOptionFormDataModel model)
        {
            ActionName = "PriorityChange(CounsellingOptionFormDataModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.CounsellingApplicationFormRepository.PriorityChange_Counselling(model);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
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
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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

        [HttpPost("GetDocumentDatabyID_Counselling")]
        public async Task<ApiResult<Counselling_DocumentDataModel>> GetDocumentDatabyID_Counselling(CounsellingApplicationSearchModel searchRequest)
        {
            ActionName = "GetDocumentDatabyID_Counselling(CounsellingApplicationSearchModel searchRequest)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<Counselling_DocumentDataModel>();
                try
                {
                    var data = await _unitOfWork.CounsellingApplicationFormRepository.GetDocumentDatabyID_Counselling(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<Counselling_DocumentDataModel>(data);
                        result.Data = mappedData;
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
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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

        [HttpPost("MapCandidateSSO")]
        public async Task<ApiResult<DataTable>> MapCandidateSSO(CounsellingApplicationSearchModel model)
        {
            ActionName = "MapCandidateSSO(CounsellingApplicationSearchModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CounsellingApplicationFormRepository.MapCandidateSSO(model);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<DataTable>(data);
                        result.Data = mappedData;
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
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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


        [HttpPost("UpdateCandidateSsoMapping")]
        public async Task<ApiResult<int>> UpdateCandidateSsoMapping([FromBody] CounsellingApplicationSearchModel model)
        {
            ActionName = "UpdateCandidateSsoMapping([FromBody] CounsellingApplicationSearchModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    var data = await _unitOfWork.CounsellingApplicationFormRepository.UpdateStudentSsoMapping(model);
                    await _unitOfWork.SaveChangesAsync();
                    if (data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Data = data;
                        result.Message = "Candidate Mapped Successfully";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Something went wrong";
                        result.Data = data;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;

                    // Log the error
                    await _unitOfWork.DisposeAsync();
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

        [HttpPost("SaveDocumentData_Counselling")]
        public async Task<ApiResult<bool>> SaveDocumentData_Counselling([FromBody] List<Counselling_DocumentDetailsModel> request)
        {
            ActionName = "SaveDocumentData_Counselling([FromBody] List<Counselling_DocumentDetailsModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.CounsellingApplicationFormRepository.SaveDocumentData_Counselling(request);
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

        [HttpPost("PreviewData_ByID_Counselling")]
        public async Task<ApiResult<CounsellingApplicationPreviewDataModel>> PreviewData_ByID_Counselling(CounsellingApplicationSearchModel searchRequest)
        {
            ActionName = "PreviewData_ByID_Counselling(CounsellingApplicationSearchModel searchRequest)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<CounsellingApplicationPreviewDataModel>();
                try
                {
                    var data = await _unitOfWork.CounsellingApplicationFormRepository.PreviewData_ByID_Counselling(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<CounsellingApplicationPreviewDataModel>(data);
                        result.Data = mappedData;
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
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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

        [HttpPost("DeleteChildOptionByID_Counselling")]
        public async Task<ApiResult<bool>> DeleteChildOptionByID_Counselling(InstituteListDataModel_Coun model)
        {
            ActionName = "DeleteChildOptionByID_Counselling(InstituteListDataModel_Coun model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.CounsellingApplicationFormRepository.DeleteChildOptionByID_Counselling(model);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_DELETE_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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

        [HttpPost("ChildPriorityChange_Counselling")]
        public async Task<ApiResult<bool>> ChildPriorityChange_Counselling(InstituteListDataModel_Coun model)
        {
            ActionName = "ChildPriorityChange_Counselling(InstituteListDataModel_Coun model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.CounsellingApplicationFormRepository.ChildPriorityChange_Counselling(model);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
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
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
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
    }
}
