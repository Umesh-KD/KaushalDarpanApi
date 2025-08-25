using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ITIRelievingExam;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidationActionFilter]
    public class ITIRelievingExamController : BaseController
    {
        public override string PageName => "ITIRelievingExamController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIRelievingExamController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //[HttpGet("GetTrade")]
        //public async Task<IActionResult> GetTrade()
        //{
        //    try
        //    {
        //        var trades = await _unitOfWork.ITIRelievingExamRepository.GetTradesAsync();
        //        return Ok(trades);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception and return an error response
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpPost("SaveRelievingExaminerData")]
        public async Task<ApiResult<DataTable>> SaveRelievingExaminerData([FromBody] ITIExaminerRelievingModel request)
        {
            ActionName = "SaveRelievingExaminerData([FromBody] ITIExaminerRelievingModel request)";
            return await Task.Run(async () =>
            {
                //var result = new ApiResult<bool>();
                var result = new ApiResult<DataTable>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed! Yaha";
                    //    return result;
                    //}

                    result.Data = await _unitOfWork.ITIRelievingExamRepository.SaveRelievingExaminerDataAsync(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data.Rows.Count > 0 && Convert.ToInt32(result.Data.Rows[0][0].ToString()) > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                        result.Data = result.Data;
                    }
                    else if (Convert.ToInt32(result.Data.Rows[0][0].ToString()) == -2)
                    {
                        result.ErrorMessage = result.Data.Rows[0][1].ToString();
                        result.State = EnumStatus.Error;
                        result.Data = result.Data;
                    }

                    //result.Data = await _unitOfWork.ITIRelievingExamRepository.SaveRelievingExaminerDataAsync(request);
                    //_unitOfWork.SaveChanges();
                    //if (result.Data)
                    //{
                    //    result.State = EnumStatus.Success;
                    //    result.Message = Constants.MSG_SAVE_SUCCESS;
                    //}
                    //else
                    //{
                    //    result.ErrorMessage = Constants.MSG_ADD_ERROR;

                    //}
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

        [HttpPost("SaveRelievingCoOrdinatorData")]
        public async Task<ApiResult<bool>> SaveRelievingCoOrdinatorData([FromBody] ITICoordinatorRelievingModel request)
        {
            ActionName = "SaveRelievingCoOrdinatorData([FromBody] ITICoordinatorRelievingModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                 


                    result.Data = await _unitOfWork.ITIRelievingExamRepository.SaveRelievingCoOrdinatorData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
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

        [HttpGet("GetRelievingExaminerById/{id}")]
        public async Task<ApiResult<DataTable>> GetRelievingExaminerById(int id)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITIRelievingExamRepository.GetRelievingExaminerByIdAsync(id);
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


        [HttpGet("GetDataBySSOId/{SSOId}")]
        public async Task<ApiResult<DataTable>> GetAllDataBySSOId(string SSOId)
        {
            ActionName = "GetAllDataBySSOId(string SSOId)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIRelievingExamRepository.GetDataBySSOId(SSOId));
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


        [HttpPost("SaveUndertakingExaminerData")]
        public async Task<ApiResult<bool>> SaveUndertakingExaminerData([FromBody] UndertakingExaminerFormModel model)
        {
            ActionName = "SaveUndertakingExaminerData([FromBody] UndertakingExaminerFormModel model)";
            var result = new ApiResult<bool>();
            try
            {
                if (!ModelState.IsValid)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "Validation failed!";
                    return result;
                }
               
                result.Data = await _unitOfWork.ITIRelievingExamRepository.SaveUndertakingExaminerData(model);
                _unitOfWork.SaveChanges();
                if (result.Data)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else
                {
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
        }




        [HttpGet("GetRelievingByExamCoordinatorId/{id}")]
        public async Task<ApiResult<DataTable>> GetRelievingByExamCoordinatorId(int id)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITIRelievingExamRepository.GetRelievingByExamCoordinatorByIdAsync(id);

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

        [HttpGet("GetUndertakingExaminerDetailsById/{id}")]
        public async Task<ApiResult<DataTable>> GetUndertakingExaminerDetailsById(int id)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITIRelievingExamRepository.GetUndertakingExaminerDetailsByIdAsync(id);
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

        [HttpGet("GetCenterListByUserid/{Userid}")]
        public async Task<ApiResult<DataTable>> GetCenterListByUserid(int id)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.ITIRelievingExamRepository.Get_CenterListByUserid(id);
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
    }
}
