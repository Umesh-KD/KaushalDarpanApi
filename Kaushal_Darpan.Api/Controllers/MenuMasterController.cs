using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.MenuMaster;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using System.Data;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumMenu.Admin,EnumMenu.Guest)]
    [ValidationActionFilter] 
    public class MenuMasterController : BaseController
    {
        public override string PageName => "MenuMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MenuMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] MenuMasterSerchModel request)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.MenuMasterRepository.GetAllData(request));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No record found.!";
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

        [HttpGet("GetMenuMaster/{MenuId}")]
        public async Task<ApiResult<MenuMasterModel>> GetByID(int MenuId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<MenuMasterModel>();
                try
                {
                    var data = await _unitOfWork.MenuMasterRepository.GetById(MenuId);
                    if (data != null)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully .!";

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found.!";
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
        public async Task<ApiResult<bool>> SaveData([FromBody] MenuMasterModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.MenuMasterRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.MenuId == 0)
                        {
                            result.Message = "Saved successfully .!";
                        }
                        else
                        {
                            result.Message = "Updated successfully .!";
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.MenuId == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
                    }
                }
                catch (Exception ex)
                {
                    // write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = "SaveData",
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message.ToString();

                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }

        /*put is used to full update the existing record*/
        [HttpPut("UpdateData")]
        public async Task<ApiResult<bool>> UpdateData(MenuMasterModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.MenuMasterRepository.UpdateData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error updating data.!";
                    }
                }
                catch (Exception ex)
                {
                    // write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = "UpdateData",
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message.ToString();

                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }

        /*delete is used to remove the existing record*/
        [HttpDelete("DeleteByID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int PK_ID, int ModifyBy)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new MenuMasterModel
                    {
                        MenuId = PK_ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.MenuMasterRepository.DeleteDataByID(DeleteData_Request);
                    _unitOfWork.SaveChanges();

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
                catch (Exception ex)
                {
                    // write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = "DeleteData",
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message.ToString();

                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }


        [HttpPost("MenuUserandRoleWise")]
        public async Task<ApiResult<DataTable>> MenuUserandRoleWise(MenuByUserAndRoleWiseModel model)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.MenuMasterRepository.MenuUserandRoleWise(model));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No record found.!";
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
        [HttpDelete("UpdateActiveStatusByID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> UpdateActiveStatusByID(int PK_ID, int ModifyBy)
        {
            ActionName = "UpdateActiveStatusByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new MenuMasterModel
                    {
                        MenuId = PK_ID,

                        ModifyBy = ModifyBy,

                    };
                    result.Data = await _unitOfWork.MenuMasterRepository.UpdateActiveStatusByID(DeleteData_Request);
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

        [HttpPost("SaveData_EditMenuDetails")]
        public async Task<ApiResult<bool>> SaveData_EditMenuDetails([FromBody] MenuMasterModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.MenuMasterRepository.SaveData_EditMenuDetails(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.MenuId == 0)
                        {
                            result.Message = "Saved successfully .!";
                        }
                        else
                        {
                            result.Message = "Updated successfully .!";
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.MenuId == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
                    }
                }
                catch (Exception ex)
                {
                    // write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = "SaveData",
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message.ToString();

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
