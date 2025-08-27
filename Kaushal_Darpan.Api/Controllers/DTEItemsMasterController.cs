using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.GroupMaster;
using Kaushal_Darpan.Models.ItemsMaster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class DTEItemsMasterController : BaseController
    {
        public override string PageName => "DTEItemsMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DTEItemsMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] DTEItemsSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAllData(body));
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

        [HttpPost("SaveData")]
        public async Task<ApiResult<int>> SaveData([FromBody] DTEItemsModel request)
        {
            ActionName = "SaveData([FromBody] DTEItemsModel request)";
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


                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data>0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ItemId == 1)
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
                        if (request.ItemId == 0)
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
        
        [HttpPost("UpdateStatusItemsData")]
        public async Task<ApiResult<int>> UpdateStatusItemsData([FromBody] DTEItemsModel request)
        {
            ActionName = "SaveData([FromBody] DTEItemsModel request)";
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


                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.UpdateStatusItemsData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data>0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ItemId == 1)
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
                        if (request.ItemId == 0)
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

        [HttpGet("GetByID/{PK_ID}")]
        public async Task<ApiResult<DTEItemsModel>> GetByID(int PK_ID)
            {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DTEItemsModel>();
                try
                {
                    var data = await _unitOfWork.iDTEItemsMasterRepository.GetById(PK_ID);
                    result.Data = data;
                    if (data != null)
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

        [HttpPost("DeleteDataByID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int PK_ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new DTEItemsModel
                    {
                        ItemId = PK_ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.DeleteDataByID(DeleteData_Request);
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

        [HttpGet("GetDTEItemDetails/{PK_ID}")]
        public async Task<ApiResult<DTEItemsDetailsModel>> GetDTEItemDetails(int PK_ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DTEItemsDetailsModel>();
                try
                {
                    var data = await _unitOfWork.iDTEItemsMasterRepository.GetDTEItemDetails(PK_ID);
                    result.Data = data;
                    if (data != null)
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

        [HttpGet("GetAllDTEItemDetails/{PK_ID}")]
        public async Task<ApiResult<object>> GetAllDTEItemDetails(int PK_ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<object>();
                try
                {
                    var data = await _unitOfWork.iDTEItemsMasterRepository.GetAllDTEItemDetails(PK_ID);

                    // Ensure that data is a List<ItemsDetailsModel> and check its count
                    if (data != null && data.Count > 0)
                    {
                        result.Data = data;
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

        [HttpPost("UpdateDTEItemData")]
        public async Task<ApiResult<bool>> UpdateDTEItemData([FromBody] List<DTEItemsDetailsModel> request)
        {
            Console.WriteLine(JsonConvert.SerializeObject(request));
            var result = new ApiResult<bool>();

            try
            {
                var totalRowsAffected = await _unitOfWork.iDTEItemsMasterRepository.UpdateDTEItemData(request);
                _unitOfWork.SaveChanges();

                if (totalRowsAffected > 0)

                {
                    result.Data = true;
                    result.State = EnumStatus.Success;
                    result.Message = "Saved successfully .!";
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
            }

            return result;
        }

        [HttpPost("GetAllAuctionList")]
        public async Task<ApiResult<DataTable>> GetAllAuctionList([FromBody] DTEItemsSearchModel body)
        {
            ActionName = "GetAllAuctionList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAllAuctionList(body));
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

        [HttpPost("SaveAuctionData")]
        public async Task<ApiResult<int>> SaveAuctionData([FromBody] AuctionDetailsModel request)
        {
            ActionName = "SaveAuctionData([FromBody] DTEItemsModel request)";
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


                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.SaveAuctionData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ItemDetailsId == 1)
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
                        if (request.ItemDetailsId == 0)
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


        [HttpPost("EquipmentCodeDuplicate")]
        public async Task<ApiResult<int>> EquipmentCodeDuplicate([FromBody] EquipmentCodeDuplicateSearch request)
        {
            ActionName = "EquipmentCodeDuplicate([FromBody] EquipmentCodeDuplicateSearch request)";
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


                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.EquipmentCodeDuplicate(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        
                        if (result.Data == 1)
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = "Duplicate equipment code already exists.";
                        }
                        else
                        {
                            result.State = EnumStatus.Success;
                            result.Message = "Equipment code is available.";
                        }
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Record not found.";
                    }
                    else
                    {
                        
                        if (request.IsDuplicate == 0)
                        {
                            result.State = EnumStatus.Success;
                            result.ErrorMessage = "Equipment code is available.";
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
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


        [HttpPost("CheckItemAuction")]
        public async Task<ApiResult<DataTable>> CheckItemAuction([FromBody] CheckItemAuctionSearch request)
        {
            ActionName = "CheckItemAuction([FromBody] CheckItemAuctionSearch request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }
                    result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.CheckItemAuction(request));
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
            });
        }
    }
}


