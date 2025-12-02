using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ItemUnitModel;
using Kaushal_Darpan.Models.ITIBUDGET;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ITIBudgetHeadController : BaseController
    {
        public override string PageName => "ITIBudgetHeadController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ITIBudgetHeadController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData(BudgetHeadSearchFilter model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BudgetHeadManagementRepository.GetAllData(model);
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

        [HttpPost("GetAllBudgetManagementData")]
        public async Task<ApiResult<DataTable>> GetAllBudgetManagementData(BudgetHeadSearchFilter model)
        {
            ActionName = "GetAllBudgetManagementData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BudgetHeadManagementRepository.GetAllBudgetManagementData(model);
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

        [HttpPost("Save_CollegeBudgetAlloted")]
        public async Task<ApiResult<int>> Save_CollegeBudgetAlloted([FromBody] CollegeBudgetAllotedModel request)
        {
            ActionName = "Save_CollegeBudgetAlloted([FromBody] CollegeBudgetAllotedModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    result.Data = await _unitOfWork.BudgetHeadManagementRepository.Save_CollegeBudgetAlloted(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
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
        [HttpPost("Save_CollegeBudgetUtilizations")]
        public async Task<ApiResult<int>> Save_CollegeBudgetUtilizations(List<CollegeBudgetUCDataModel> request)
        {
            ActionName = "Save_CollegeBudgetUtilizations([FromBody] CollegeBudgetUCDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    result.Data = await _unitOfWork.BudgetHeadManagementRepository.Save_CollegeBudgetUtilizations(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
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

        [HttpPost("GetBudgetUtilizationsData")]
        public async Task<ApiResult<DataTable>> GetBudgetUtilizationsData(BudgetHeadSearchFilter model)
        {
            ActionName = "GetBudgetUtilizationsData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BudgetHeadManagementRepository.GetUtilizationsData(model);
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


        [HttpPost("Save_CollegeBudgetRequest")]
        public async Task<ApiResult<int>> Save_CollegeBudgetRequest([FromBody] BudgetRequestModel request)
        {
            ActionName = "Save_CollegeBudgetAlloted([FromBody] CollegeBudgetAllotedModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    result.Data = await _unitOfWork.BudgetHeadManagementRepository.Save_CollegeBudgetRequest(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
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



        [HttpPost("GetBudgetRequestData")]
        public async Task<ApiResult<DataTable>> GetBudgetRequestData(BudgetHeadSearchFilter model)
        {
            ActionName = "GetBudgetUtilizationsData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BudgetHeadManagementRepository.GetBudgetRequestData(model);
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

        [HttpPost("SaveBudgetUtilization_Admin")]
        public async Task<ApiResult<int>> SaveBudgetUtilization_Admin(CollegeBudgetAllotedModel request)
        {
            ActionName = "SaveBudgetUtilization_Admin([FromBody] CollegeBudgetAllotedModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    result.Data = await _unitOfWork.BudgetHeadManagementRepository.SaveBudgetUtilization_Admin(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
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

        [HttpPost("GetBudget_HeadWise")]
        public async Task<ApiResult<DataTable>> GetBudget_HeadWise(BudgetHeadSearchFilter model)
        {
            ActionName = "GetBudget_HeadWise(BudgetHeadSearchFilter)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BudgetHeadManagementRepository.GetBudget_HeadWise(model);
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

        [HttpPost("Approve_CollegeBudgetAllot")]
        public async Task<ApiResult<int>> Approve_CollegeBudgetAllot([FromBody] List<CollegeBudgetAllotApproveDataModel> request)
        {
            ActionName = "Approve_CollegeBudgetAllot([FromBody] CollegeBudgetAllotApproveDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    result.Data = await _unitOfWork.BudgetHeadManagementRepository.Approve_CollegeBudgetAllot(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
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

        [HttpPost("UnlockUtilization_ITI_BGT")]
        public async Task<ApiResult<int>> UnlockUtilization_ITI_BGT(UnlockUtilizationDataModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.BudgetHeadManagementRepository.UnlockUtilization_ITI_BGT(model);

                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
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


        [HttpPost("GetAllBudgetReportData")]
        public async Task<ApiResult<DataTable>> GetAllBudgetReportData(BudgetHeadSearchFilter model)
        {
            ActionName = "GetAllBudgetReportData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BudgetHeadManagementRepository.GetAllBudgetReportData(model);
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
        [HttpPost("GetAllotedReportData")]
        public async Task<ApiResult<DataTable>> GetAllotedReportData(BudgetHeadSearchFilter model)
        {
            ActionName = "GetAllotedReportData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BudgetHeadManagementRepository.GetAllotedReportData(model);
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

        [HttpPost("GetAllUCUtilizationReportData")]
        public async Task<ApiResult<DataTable>> GetAllUCUtilizationReportData(BudgetHeadSearchFilter model)
        {
            ActionName = "GetAllUCUtilizationReportData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BudgetHeadManagementRepository.GetAllUCUtilizationReportData(model);
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
