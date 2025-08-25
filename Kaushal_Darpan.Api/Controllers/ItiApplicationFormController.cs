using AutoMapper;
using iTextSharp.text.pdf.parser;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.ITIAdminDashboard;
using Kaushal_Darpan.Models.ITIApplication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;
using static Kaushal_Darpan.Models.ITIApplication.ItiApplicationPreviewDataModel;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class ItiApplicationFormController : BaseController
    {
        public override string PageName => "ItiApplicationFormController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ItiApplicationFormController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SavePersonalDetailsData")]
        public async Task<ApiResult<int>> SavePersonalDetailsData([FromBody] PersonalDetailsDataModel request)
        {
            ActionName = " SavePersonalDetailsData([FromBody] PersonalDetailsDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ItiApplicationFormRepository.SavePersonalDetailsData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ApplicationID == 0)
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
                        if (request.ApplicationID == 0)
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
            });
        }

        [HttpPost("GetApplicationDatabyID")]
        public async Task<ApiResult<PersonalDetailsDataModel>> GetApplicationDatabyID(ItiApplicationSearchModel searchRequest)
        {
            ActionName = "GetApplicationDatabyID(int ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<PersonalDetailsDataModel>();
                try
                {
                    var data = await _unitOfWork.ItiApplicationFormRepository.GetApplicationDatabyID(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<PersonalDetailsDataModel>(data);
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
                    _unitOfWork.Dispose();
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

        [HttpPost("SaveOptionDetailsData")]
        public async Task<ApiResult<bool>> SaveOptionDetailsData([FromBody] List<OptionDetailsDataModel> request)
        {
            ActionName = "SaveOptionDetailsData([FromBody] List<OptionDetailsDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.ItiApplicationFormRepository.SaveOptionDetailsData(request);
                    _unitOfWork.SaveChanges();

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
            });
        }

        [HttpPost("SaveQualificationDetailsData")]
        public async Task<ApiResult<bool>> SaveQualificationDetailsData([FromBody] List<QualificationDetailsDataModel> request)
        {
            ActionName = "SaveQualificationDetailsData([FromBody] List<QualificationDetailsDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.ItiApplicationFormRepository.SaveQualificationDetailsData(request);
                    _unitOfWork.SaveChanges();

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
            });
        }

        [HttpPost("SaveDocumentDetailsData")]
        public async Task<ApiResult<bool>> SaveDocumentDetailsData([FromBody] List<DocumentDetailsModel> request)
        {
            ActionName = "SaveDocumentDetailsData([FromBody] DocumentDetailsDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.ItiApplicationFormRepository.SaveDocumentDetailsData(request);
                    _unitOfWork.SaveChanges();

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
            });
        }

        [HttpPost("SaveAddressDetailsData")]
        public async Task<ApiResult<int>> SaveAddressDetailsData([FromBody] AddressDetailsDataModel request)
        {
            ActionName = "SaveAddressDetailsData([FromBody] AddressDetailsDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ItiApplicationFormRepository.SaveAddressDetailsData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ApplicationID == 0)
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
                        if (request.ApplicationID == 0)
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
            });
        }

        [HttpPost("GetQualificationDatabyID")]
        public async Task<ApiResult<List<QualificationDetailsDataModel>>> GetQualificationDatabyID(ItiApplicationSearchModel request)
        {
            ActionName = "GetApplicationDatabyID(int ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<QualificationDetailsDataModel>>();
                try
                {
                    var data = await _unitOfWork.ItiApplicationFormRepository.GetQualificationDatabyID(request);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<List<QualificationDetailsDataModel>>(data);
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
                    _unitOfWork.Dispose();
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

        [HttpPost("GetAddressDetailsbyID")]
        public async Task<ApiResult<AddressDetailsDataModel>> GetAddressDetailsbyID(ItiApplicationSearchModel request)
        {
            ActionName = "GetAddressDetailsbyID(ItiApplicationSearchModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<AddressDetailsDataModel>();
                try
                {
                    var data = await _unitOfWork.ItiApplicationFormRepository.GetAddressDetailsbyID(request);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<AddressDetailsDataModel>(data);
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
                    _unitOfWork.Dispose();
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

        [HttpPost("GetOptionDetailsbyID")]
        public async Task<ApiResult<List<OptionDetailsDataModel>>> GetOptionDetailsbyID(ItiApplicationSearchModel request)
        {
            ActionName = "GetOptionDetailsbyID(int ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<OptionDetailsDataModel>>();
                try
                {
                    var data = await _unitOfWork.ItiApplicationFormRepository.GetOptionDetailsbyID(request);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<List<OptionDetailsDataModel>>(data);
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
                    _unitOfWork.Dispose();
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

        [HttpPost("GetApplicationPreviewbyID")]
        public async Task<ApiResult<ItiApplicationPreviewModel>> GetApplicationPreviewbyID(ItiApplicationSearchModel searchRequest)
        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ItiApplicationPreviewModel>();
                try
                {
                    var data = await _unitOfWork.ItiApplicationFormRepository.GetApplicationPreviewbyID(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ItiApplicationPreviewModel>(data);
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
                    _unitOfWork.Dispose();
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

        [HttpPost("GetDocumentDatabyID")]
        public async Task<ApiResult<DocumentDetailsDataModel>> GetDocumentDatabyID(ItiApplicationSearchModel searchRequest)
        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DocumentDetailsDataModel>();
                try
                {
                    var data = await _unitOfWork.ItiApplicationFormRepository.GetDocumentDatabyID(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<DocumentDetailsDataModel>(data);
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
                    _unitOfWork.Dispose();
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

        [HttpPost("FinalSubmit/{ApplicationID}/{Status}")]
        public async Task<ApiResult<int>> FinalSubmit(int ApplicationID, int Status)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ItiApplicationFormRepository.FinalSubmit(ApplicationID, Status);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (ApplicationID == 0)
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
                        if (ApplicationID == 0)
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
            });
        }



        [HttpPost("GetITIStudentProfileDownload")]
        public async Task<ApiResult<DataTable>> GetITIStudentProfileDownload(ItiApplicationSearchModel searchRequest)
        {
            ActionName = "GetITIStudentProfileDownload(int EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.ItiApplicationFormRepository.GetITIStudentProfileDownload(searchRequest);
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
                    _unitOfWork.Dispose();
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

        [HttpPost("DeleteOptionByID")]
        public async Task<ApiResult<bool>> DeleteOptionByID(OptionDetailsDataModel model)
        {
            ActionName = "DeleteOptionByID(OptionDetailsDataModel productDetails)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ItiApplicationFormRepository.DeleteOptionByID(model);
                    _unitOfWork.SaveChanges();

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
                    _unitOfWork.Dispose();
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

        [HttpPost("PriorityChange")]
        public async Task<ApiResult<bool>> PriorityChange(OptionDetailsDataModel model)
        {
            ActionName = "DeleteOptionByID(OptionDetailsDataModel productDetails)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ItiApplicationFormRepository.PriorityChange(model);
                    _unitOfWork.SaveChanges();

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
                    _unitOfWork.Dispose();
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

        [HttpPost("GetItiApplicationData")]
        public async Task<ApiResult<DataTable>> GetItiApplicationData([FromBody] ItiAdminDashApplicationSearchModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ItiApplicationFormRepository.GetItiApplicationData(model);
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

        [HttpPost("UnlockApplication")]
        public async Task<ApiResult<bool>> UnlockApplication(ItiApplicationUnlockDataModel model)
        {
            ActionName = "DeleteOptionByID(OptionDetailsDataModel productDetails)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ItiApplicationFormRepository.UnlockApplication(model);
                    _unitOfWork.SaveChanges();

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
                    _unitOfWork.Dispose();
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

        [HttpPost("ITI_DirectAdmissionApply")]
        public async Task<ApiResult<int>> ITI_DirectAdmissionApply([FromBody] ITI_DirectAdmissionApplyDataModel request)
        {
            ActionName = "SaveOptionDetailsData([FromBody] List<OptionDetailsDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var isSave = await _unitOfWork.ItiApplicationFormRepository.ITI_DirectAdmissionApply(request);
                    _unitOfWork.SaveChanges();

                    if (isSave == 2)
                    {
                        //result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = "FOLLOWING CANDIDATE ALREADY APPLIED FOR DIRECT ADMISSION";
                    }
                    else if (isSave > 2)
                    {
                        result.Data = isSave;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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
            });
        }
    }
}
