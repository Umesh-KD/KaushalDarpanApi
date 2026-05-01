using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.DesignationMaster;
using Kaushal_Darpan.Models.StaffMaster;
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

        [HttpPost("GetAllDesignations")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] DesignationMasterSearchModel request)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.DesignationMasterRepository.GetAllData(request));
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
                await _unitOfWork.DisposeAsync();
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
                    await _unitOfWork.DisposeAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.DisposeAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.DisposeAsync();
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
                    await _unitOfWork.SaveChangesAsync();

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
                    await _unitOfWork.DisposeAsync();
                }
                return result;
            });
        }

        [HttpPost("DesignationActiveDeActive")]
        public async Task<ApiResult<int>> DesignationActiveDeActive([FromBody] DesignationMasterSearchModel body)
        {

            ActionName = "DesignationActiveDeActive([FromBody] OfficeVacancyModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.DesignationMasterRepository.DesignationActiveDeActive(body);
                await _unitOfWork.SaveChangesAsync();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_UPDATE_SUCCESS;
                }
                else if (result.Data == -1)
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

        }
    }
}
