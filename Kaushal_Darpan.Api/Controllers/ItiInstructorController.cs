using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ITI_InstructorModel;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel;
using Kaushal_Darpan.Models.ITIPlanning;



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

                    await _unitOfWork.SaveChangesAsync();
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

                    await _unitOfWork.SaveChangesAsync();
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


        [HttpPost("GetAllTechCITSDetails")]
        public async Task<ApiResult<DataSet>> GetAllTechCITSDetails(ITI_Instructor_TechCITSDetailsSearchModel model)
        {
            ActionName = "GetAllDataPhoneVerify()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InstructorRepository.GetAllTechCITSDetails(model));
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



        [HttpPost("UpdateInstructorData")]
        public async Task<ApiResult<int>> UpdateInstructorData([FromBody] ITI_InstructorModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITI_InstructorRepository.UpdateInstructorDataAsync(request);

                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
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


        [HttpPost("GetInstructorListIsAssign")]
        public async Task<ApiResult<DataTable>> GetInstructorListIsAssign(ITI_InstructorDataAssign model)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITI_InstructorRepository.GetInstructorListIsAssign(model);
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



        [HttpPost("GetverificationStatus")]
        public async Task<ApiResult<DataTable>> GetverificationStatus(Iti_InstructorVerification model)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITI_InstructorRepository.GetverificationStatus(model);
                await _unitOfWork.SaveChangesAsync();
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





        [HttpPost("GetInstructorListIsAssignStatus/{Uid}")]
        public async Task<ApiResult<DataTable>> GetInstructorListIsAssignStatus(string Uid)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITI_InstructorRepository.ToggleAssignStatusAsync(Uid);

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
                result.Message = Constants.MSG_ADD_ERROR;
                result.ErrorMessage = ex.Message; 
            }

            return result;
        }


        [HttpPost("SaveItiworkflow")]
        public async Task<ApiResult<bool>> SaveItiworkflow([FromBody] Iti_InstructorVerification request)
        {
            ActionName = "SaveData([FromBody] ItiReportDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed!";
                    //    return result;
                    //}


                    result.Data = await _unitOfWork.ITI_InstructorRepository.SaveItiworkflow(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.VerificationID == 0)

                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.VerificationID == 0)
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

        [HttpPost("SaveOptionDetailsData")]
        public async Task<ApiResult<bool>> SaveOptionDetailsData([FromBody] List<InstructorChoiceFillingModel> request)
        {
            ActionName = "SaveOptionDetailsData([FromBody] List<OptionDetailsDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.ITI_InstructorRepository.SaveOptionDetailsData(request);
                    await _unitOfWork.SaveChangesAsync();

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



        [HttpPost("PriorityChange")]
        public async Task<ApiResult<bool>> PriorityChange(InstructorChoiceFillingModel model)
        {
            ActionName = "DeleteOptionByID(OptionDetailsDataModel productDetails)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ITI_InstructorRepository.PriorityChange(model);
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


        [HttpPost("Onfinaljoin")]
        public async Task<ApiResult<int>> Onfinaljoin([FromBody] Iti_InstructorVerification request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITI_InstructorRepository.Onfinaljoin(request);

                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    } 
                    else if (result.Data==-3)
                    {
                        result.State=EnumStatus.Error;
                        result.ErrorMessage="No More Vacancy";
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

    }
}


