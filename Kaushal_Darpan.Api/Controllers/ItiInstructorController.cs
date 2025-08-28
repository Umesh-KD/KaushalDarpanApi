using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ITI_InstructorModel;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel;


//using Kaushal_Darpan.Models.CompanyMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class ItiInstructorController : BaseController
    {
        public override string PageName => "ItiInstructorController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ItiInstructorController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SaveInstructorData")]
        public async Task<ApiResult<int>> SaveInstructorData([FromBody] ITI_InstructorModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITI_InstructorRepository.SaveInstructorData(request);

                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                        //if (request.UserID == 0)
                        //{
                        //    result.Message = Constants.MSG_SAVE_SUCCESS;
                        //}
                        //else
                        //{
                        //    result.Message = Constants.MSG_UPDATE_SUCCESS;
                        //}

                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        //if (request.UserID == 0)
                        //{
                        //    result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        //}
                        //else
                        //{
                        //    result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        //}
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

        [HttpPost("GetInstructorData")]
        public async Task<ApiResult<DataTable>> GetInstructorData([FromBody] ITI_InstructorDataSearchModel request)
        {
            ActionName = "GetInstructorData([FromBody] ITI_InstructorDataSearchModel request)";
            var result = new ApiResult<DataTable>();
            try
            {
                //result.Data = await Task.Run(() => _unitOfWork.ITICollegeMarksheetDownloadRepository.GetITICollegeList(body));
                result.Data = await Task.Run(() => _unitOfWork.ITI_InstructorRepository.GetInstructorData(request));
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



        [HttpGet("GetInstructorDataByID/{id}")]
        public async Task<ApiResult<DataTable>> GetInstructorDataByID(int id)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITI_InstructorRepository.GetInstructorDataByID(id);
                if (data != null && data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    result.Data = data;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


        [HttpGet("deleteInstructorDataByID/{id}")]
        public async Task<ApiResult<int>> deleteInstructorDataByID(int id)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITI_InstructorRepository.deleteInstructorDataByID(id);

                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                        //if (request.UserID == 0)
                        //{
                        //    result.Message = Constants.MSG_SAVE_SUCCESS;
                        //}
                        //else
                        //{
                        //    result.Message = Constants.MSG_UPDATE_SUCCESS;
                        //}

                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        //if (request.UserID == 0)
                        //{
                        //    result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        //}
                        //else
                        //{
                        //    result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        //}
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


        [HttpPost("GetGridInstructorData")]
        public async Task<ApiResult<DataTable>> GetGridInstructorData(ITI_InstructorApplicationNoDataSearchModel model)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITI_InstructorRepository.GetGridInstructorData(model);
                if (data != null && data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    result.Data = data;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


        [HttpPost("GetGridBindInstructorData")]
        public async Task<ApiResult<DataTable>> GetGridBindInstructorData(ITI_InstructorBindDataSearchModel model)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITI_InstructorRepository.GetGridBindInstructorData(model);
                if (data != null && data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    result.Data = data;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


        [HttpPost("GetInstructorDataBySsoid/{SSOID}")]
        public async Task<ApiResult<DataSet>> GetInstructorDataBySsoid(string SSOID)
        {
            ActionName = "GetAllDataPhoneVerify()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InstructorRepository.GetInstructorDataBySsoid(SSOID));
                result.State = EnumStatus.Success;
                if (result.Data.Tables.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";
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


    }
}


