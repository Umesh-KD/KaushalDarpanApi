using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.RoleMaster;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.ScholarshipMaster;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class HiringRoleMasterController : BaseController
    {
        public override string PageName => "HiringRoleMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HiringRoleMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData()
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HiringRoleMasterRepository.GetAllData());
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

        [HttpGet("GetAllSanction")]
        public async Task<ApiResult<DataTable>> GetAllSanction()
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HiringRoleMasterRepository.GetAllSanction());
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


        [HttpGet("GetByID/{PK_ID}")]
        public async Task<ApiResult<HiringRoleMasterModel>> GetByID(int PK_ID)
        {

            ActionName = " GetByID(int PK_ID)";
            var result = new ApiResult<HiringRoleMasterModel>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HiringRoleMasterRepository.GetById(PK_ID));
                result.State = EnumStatus.Success;
                if (result.Data == null)
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



        [HttpGet("GetByIDSanction/{PK_ID}")]
        public async Task<ApiResult<SanctionOrderMasterModel>> GetByIDSanction(int PK_ID)
        {

            ActionName = " GetByID(int PK_ID)";
            var result = new ApiResult<SanctionOrderMasterModel>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HiringRoleMasterRepository.GetByIDSanction(PK_ID));
                result.State = EnumStatus.Success;
                if (result.Data == null)
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


        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] HiringRoleMasterModel request)
        {
            ActionName = "SaveData([FromBody] HiringRoleMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.HiringRoleMasterRepository.SaveData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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



        [HttpPost("SaveDataSanction")]
        public async Task<ApiResult<bool>> SaveDataSanction([FromBody] SanctionOrderMasterModel request)
        {
            ActionName = "SaveDataSanction([FromBody] SanctionOrderMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.HiringRoleMasterRepository.SaveDataSanction(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.SanctionID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.SanctionID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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


        [HttpPost("SaveSanctionOrder")]
        public async Task<ApiResult<int>> SaveSanctionOrder([FromBody] OrderDetailsList request)
        {
            ActionName = "SaveDataSanction([FromBody] SanctionOrderMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.HiringRoleMasterRepository.SaveSanctionOrder(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.SanctionID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else if(result.Data == -1)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Duplicate Order Name!";
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Duplicate Sanction Order Number!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.SanctionID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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

        /*put is used to full update the existing record*/
        [HttpPut("UpdateData")]
        public async Task<ApiResult<bool>> UpdateData(HiringRoleMasterModel request)
        {
            ActionName = "UpdateData(HiringRoleMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.HiringRoleMasterRepository.UpdateData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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

        /*delete is used to remove the existing record*/
        [HttpPost("DeleteDataByID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int PK_ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new HiringRoleMasterModel
                    {
                        ID = PK_ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.HiringRoleMasterRepository.DeleteDataByID(DeleteData_Request);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Deleted successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error deleting data.!";
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

        [HttpPost("DeleteDataBySanctionID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataBySanctionID(int PK_ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new HiringRoleMasterModel
                    {
                        ID = PK_ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.HiringRoleMasterRepository.DeleteDataBySanctionID(DeleteData_Request);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Deleted successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error deleting data.!";
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

        [HttpPost("GetsanctionOrder")]
        public async Task<ApiResult<DataTable>> GetsanctionOrder([FromBody] OrderDetailsList model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.HiringRoleMasterRepository.GetsanctionOrder(model);
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

        [HttpPost("GetOrderDetailsList_ByDate")]
        public async Task<ApiResult<DataTable>> GetOrderDetailsList_ByDate([FromBody] OrderDetailsList model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.HiringRoleMasterRepository.GetOrderDetailsList_ByDate(model);
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


        [HttpPost("GetsanctionOrderNotAssign")]
        public async Task<ApiResult<DataTable>> GetsanctionOrderNotAssign([FromBody] OrderDetailsList model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.HiringRoleMasterRepository.GetsanctionOrderNotAssign(model);
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



        [HttpGet("GetByIDSanctionOrder/{PK_ID}")]
        public async Task<ApiResult<OrderDetailsList>> GetByIDSanctionOrder(int PK_ID)
        {

            ActionName = " GetByID(int PK_ID)";
            var result = new ApiResult<OrderDetailsList>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HiringRoleMasterRepository.GetByIDSanctionOrder(PK_ID));
                result.State = EnumStatus.Success;
                if (result.Data == null)
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
        [HttpPost("DeleteSanctionOrder/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteSanctionOrder(int PK_ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new HiringRoleMasterModel
                    {
                        ID = PK_ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.HiringRoleMasterRepository.DeleteSanctionOrder(DeleteData_Request);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Deleted successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error deleting data.!";
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

        #region AnnouncementTypesMaster

        [HttpPost("GetAllAnnouncementTypes")]
        public async Task<ApiResult<DataTable>> GetAllAnnouncementTypes([FromBody] AnnouncementTypeMasterModel request)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.HiringRoleMasterRepository.GetAllAnnouncementTypes(request);

                result.Data = data;

                if (data.Rows.Count > 0)
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
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                await _unitOfWork.DisposeAsync();
            }

            return result;
        }

        [HttpGet("GetAnnouncementTypeByID/{id}")]
        public async Task<ApiResult<AnnouncementTypeMasterModel>> GetAnnouncementTypeByID(int id)
        {
            var result = new ApiResult<AnnouncementTypeMasterModel>();

            try
            {
                var data = await _unitOfWork.HiringRoleMasterRepository.GetAnnouncementTypeByID(id);

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
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                await _unitOfWork.DisposeAsync();
            }

            return result;
        }

        [HttpPost("SaveAnnouncementType")]
        public async Task<ApiResult<bool>> SaveAnnouncementType([FromBody] AnnouncementTypeMasterModel request)
        {
            var result = new ApiResult<bool>();

            try
            {
                result.Data = await _unitOfWork.HiringRoleMasterRepository.SaveAnnouncementType(request);

                await _unitOfWork.SaveChangesAsync();

                if (result.Data)
                {
                    result.State = EnumStatus.Success;
                    result.Message = request.ID == 0
                        ? "Saved successfully!"
                        : "Updated successfully!";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = request.ID == 0
                        ? "Error saving data!"
                        : "Error updating data!";
                }
            }
            catch (Exception ex)
            {
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = "SaveAnnouncementType",
                    Ex = ex
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
        }

        [HttpPost("DeleteAnnouncementTypeByID/{id}/{updatedBy}")]
        public async Task<ApiResult<bool>> DeleteAnnouncementTypeByID(int id, int updatedBy)
        {
            var result = new ApiResult<bool>();

            try
            {
                var request = new AnnouncementTypeMasterModel
                {
                    ID = id,
                    UpdatedBy = updatedBy
                };

                result.Data = await _unitOfWork.HiringRoleMasterRepository.DeleteAnnouncementTypeByID(request);

                await _unitOfWork.SaveChangesAsync();

                if (result.Data)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Deleted successfully!";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "Delete failed!";
                }
            }
            catch (Exception ex)
            {
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = "DeleteAnnouncementTypeByID",
                    Ex = ex
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
        }

        #endregion
    }

}
