using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.DesignationMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesignationMasterController : BaseController
    {
        public override string PageName => "DesignationMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DesignationMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllDesignations")]
        public async Task<ApiResult<DataTable>> GetAllData()
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.DesignationMasterRepository.GetAllData());
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data loaded successfully!";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No records found!";
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Warning;
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return result;
        }

        [HttpGet("GetByID/{designationID}")]
        public async Task<ApiResult<DesignationMasterModel>> GetByID(int designationID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DesignationMasterModel>();
                try
                {
                    var data = await _unitOfWork.DesignationMasterRepository.GetById(designationID);
                    if (data != null)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data loaded successfully!";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }

        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] DesignationMasterModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.DesignationMasterRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.DesignationID == 0)
                        {
                            result.Message = "Saved successfully!";
                        }
                        else
                        {
                            result.Message = "Updated successfully!";
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.DesignationID == 0)
                            result.ErrorMessage = "Error adding data!";
                        else
                            result.ErrorMessage = "Error updating data!";
                    }
                }
                catch (Exception ex)
                {
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = "SaveData",
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }

        [HttpPut("UpdateData")]
        public async Task<ApiResult<bool>> UpdateData([FromBody] DesignationMasterModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.DesignationMasterRepository.UpdateData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Updated successfully!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Error updating data!";
                    }
                }
                catch (Exception ex)
                {
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = "UpdateData",
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }

        [HttpDelete("DeleteDataByID/{designationID}/{modifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int designationID, int modifyBy)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var deleteRequest = new DesignationMasterModel
                    {
                        DesignationID = designationID,
                        ModifyBy = modifyBy,
                    };
                    result.Data = await _unitOfWork.DesignationMasterRepository.DeleteDataById(deleteRequest);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Deleted successfully!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Error deleting data!";
                    }
                }
                catch (Exception ex)
                {
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = "DeleteDataByID",
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }
    }
}
