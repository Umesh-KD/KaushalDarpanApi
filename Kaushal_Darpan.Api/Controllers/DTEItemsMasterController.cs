using AspNetCore.Reporting;
using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.GroupMaster;
using Kaushal_Darpan.Models.ItemsMaster;
using Kaushal_Darpan.Models.ITI_Inspection;
using Kaushal_Darpan.Models.StaffMaster;
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


        [HttpPost("GetAllDataHistory")]
        public async Task<ApiResult<DataTable>> GetAllDataHistory([FromBody] DTEItemsSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAllDataHistory(body));
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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.SaveChangesAsync();

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

        [HttpPost("UpdateDTEItemData")]
        public async Task<ApiResult<bool>> UpdateDTEItemData([FromBody] List<DTEItemsDetailsModel> request)
        {
            Console.WriteLine(JsonConvert.SerializeObject(request));
            var result = new ApiResult<bool>();

            try
            {
                var totalRowsAffected = await _unitOfWork.iDTEItemsMasterRepository.UpdateDTEItemData(request);
                await _unitOfWork.SaveChangesAsync();

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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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


        [HttpPost("UpdateStatusRevertData")]
        public async Task<ApiResult<int>> UpdateStatusRevertData([FromBody] DTEItemsModel request)
        {
            ActionName = "UpdateStatusRevertData([FromBody] DTEItemsModel request)";
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


                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.UpdateStatusRevert(request);
                    await _unitOfWork.SaveChangesAsync();
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

        [HttpPost("GetAll_INV_GetCommonIssueDDL")]
        public async Task<ApiResult<DataTable>> GetAll_INV_GetCommonIssueDDL([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAll_INV_GetCommonIssueDDL(body));
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

        [HttpPost("GetConsumeItemList")]
        public async Task<ApiResult<DataTable>> GetConsumeItemList(DTEItemsSearchModel SearchReq)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.iDTEItemsMasterRepository.GetConsumeItemList(SearchReq);
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

        [HttpPost("SaveIssueItems")]
        public async Task<ApiResult<bool>> SaveIssueItems([FromBody] ItemsIssueReturnModels request)
        {
            ActionName = "SaveRequestData([FromBody] DTETradeEquipmentsMapping request)";
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


                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.SaveIssueItems(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
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


        [HttpPost("GetInventoryIssueItemList")]
        public async Task<ApiResult<DataTable>> GetInventoryIssueItemList([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "GetInventoryIssueItemList([FromBody] inventoryIssueHistorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetInventoryIssueItemList(body));
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

        [HttpPost("GetAll_INV_returnItem")]
        public async Task<ApiResult<DataTable>> GetAll_INV_returnItem([FromBody] ItemsIssueReturnModels body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAll_INV_returnItem(body));
                await _unitOfWork.SaveChangesAsync();
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

        [HttpPost("GetAllinventoryIssueHistory")]
        public async Task<ApiResult<DataTable>> GetAllinventoryIssueHistory([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "GetAllinventoryIssueHistory([FromBody] inventoryIssueHistorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAllinventoryIssueHistory(body));
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


        [HttpPost("GetAllinventoryIssueHistoryTrail")]
        public async Task<ApiResult<DataTable>> GetAllinventoryIssueHistoryTrail([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "GetAllinventoryIssueHistoryTrail([FromBody] inventoryIssueHistorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAllinventoryIssueHistoryTrail(body));
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


        [HttpPost("GetAllInventoryIssueReturnItemList")]
        public async Task<ApiResult<DataTable>> GetAllInventoryIssueReturnItemList([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "GetAllInventoryIssueReturnItemList([FromBody] inventoryIssueHistorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAllInventoryIssueReturnItemList(body));
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
        [HttpPost("GetItemListType")]
        public async Task<ApiResult<DataTable>> GetItemListType(DTEItemsSearchModel SearchReq)
        {
            ActionName = "GetItemListType()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.iDTEItemsMasterRepository.GetItemListType(SearchReq);
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
        [HttpPost("GetAllItemList")]
        public async Task<ApiResult<DataTable>> GetAllItemList(DTEItemsSearchModel SearchReq)
        {
            ActionName = "GetAllItemList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.iDTEItemsMasterRepository.GetAllItemList(SearchReq);
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


        [HttpPost("SaveInspectionDeploymentData")]
        public async Task<ApiResult<bool>> SaveInspectionDeploymentData([FromBody] List<InspectionDeploymentDataModel> request)
        {
            ActionName = "SaveInspectionDeploymentData([FromBody] List<InspectionDeploymentDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //request.ForEach(x =>
                    //{
                    //    x.IPAddress = CommonFuncationHelper.GetIpAddress();

                    //});
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ITI_InspectionRepository.SaveInspectionDeploymentData(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
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


        [HttpPost("SaveIssueItemsList")]
        public async Task<ApiResult<bool>> SaveIssueItemsList([FromBody] List<ItemsIssueReturnModels> request)
        {
            ActionName = "SaveIssueItems([FromBody] List<ItemsIssueReturnModels> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.iDTEItemsMasterRepository.SaveIssueItemsList(request);
                    await _unitOfWork.SaveChangesAsync();

                    if (isSave == -2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
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

        [HttpPost("GetAllinventoryIssueReport")]
        public async Task<ApiResult<DataTable>> GetAllinventoryIssueReport([FromBody] ItemsIssueReturnModels body)
        {
            ActionName = "GetAllinventoryIssueReport([FromBody] ItemsIssueReturnModels body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAllinventoryIssueReport(body));
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
       


        [HttpPost("GetIssueItemList")]
        public async Task<ApiResult<DataTable>> GetIssueItemList([FromBody] ItemsIssueReturnModels body)
        {
            ActionName = "GetIssueItemList([FromBody] ItemsIssueReturnModels body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetIssueItemList(body));
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

        [HttpPost("GetInventoryIssueHistoryList")]
        public async Task<ApiResult<DataTable>> GetInventoryIssueHistoryList([FromBody] InventoryIssueHistoryListModels body)
        {
            ActionName = "GetInventoryIssueHistoryList([FromBody] ItemsIssueReturnModels body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetInventoryIssueHistoryList(body));
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
        [HttpGet("GetDTEIssueItemListPermanent/{EquipmentsId}/{ItemCategoryId}/{InstituteID}")]
        public async Task<ApiResult<DataTable>> GetDTEIssueItemListPermanent(int EquipmentsId, int ItemCategoryId, int InstituteID)
        {
            ActionName = "GetIssueItemListPermanent(int itemId)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetDTEIssueItemListPermanent(EquipmentsId, ItemCategoryId, InstituteID));
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
        [HttpPost("GetDTEIssueSubmitPermanent")]
        public async Task<ApiResult<DataTable>> GetDTEIssueSubmitPermanent([FromBody] ItemsIssueReturnModels body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetDTEIssueSubmitPermanent(body));
                await _unitOfWork.SaveChangesAsync();
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
        [HttpPost("GetDTEGetSetLabMaster")]
        public async Task<ApiResult<DataTable>> GetDTEGetSetLabMaster([FromBody] DTELabMasterModel body)
        {
            ActionName = "GetDTEGetSetLabMaster()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetDTEGetSetLabMaster(body));
                await _unitOfWork.SaveChangesAsync();
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

        [HttpPost("DTE_INV_SaveLabItemReturn")]
        public async Task<ApiResult<DataTable>> DTE_INV_SaveLabItemReturn([FromBody] ItemsIssueReturnModels body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.DTE_INV_SaveLabItemReturn(body));
                await _unitOfWork.SaveChangesAsync();
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

        [HttpPost("MarkForAuctionSR6")]
        public async Task<ApiResult<bool>> MarkForAuctionSR6([FromBody] List<ItemsIssueReturnModels> request)
        {
            ActionName = "SaveIssueItems([FromBody] List<ItemsIssueReturnModels> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.iDTEItemsMasterRepository.MarkForAuctionSR6(request);
                    await _unitOfWork.SaveChangesAsync();

                    if (isSave == -2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
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

        [HttpPost("Get_SR5_ReportData")]
        public async Task<ApiResult<DataTable>> Get_SR5_ReportData([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "Get_SR5_ReportData([FromBody] inventoryIssueHistorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.Get_SR5_ReportData(body));
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

        [HttpPost("Get_SR6_ReportData")]
        public async Task<ApiResult<DataTable>> Get_SR6_ReportData([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "Get_SR6_ReportData([FromBody] inventoryIssueHistorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.Get_SR6_ReportData(body));
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

        [HttpPost("DownloadSR6ReportData_pdf_BTER")]
        public async Task<ApiResult<string>> DownloadSR6ReportData_pdf_BTER([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "DownloadSR6ReportData_pdf_BTER([FromBody] inventoryIssueHistorySearchModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.iDTEItemsMasterRepository.DownloadSR6ReportData_pdf_BTER(body);

                    if (data?.Tables?.Count >= 1)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string guid = Guid.NewGuid().ToString().ToUpper();
                        var fileName = $"SR5-Report_{guid}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/INV_SR6_Report.rdlc";

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("ITI_INV_SR6_ReportDataHeader", data.Tables[0]);
                        localReport.AddDataSource("ITI_INV_SR6_ReportDataTable", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("Download_SR5ReportData_pdf_BTER")]
        public async Task<ApiResult<string>> Download_SR5ReportData_pdf_BTER([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "Download_SR5ReportData_pdf_BTER([FromBody] inventoryIssueHistorySearchModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.iDTEItemsMasterRepository.Download_SR5ReportData_pdf_BTER(body);

                    if (data?.Tables?.Count >= 1)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string guid = Guid.NewGuid().ToString().ToUpper();
                        var fileName = $"SR5-Report_{guid}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/INV_SR5_Report.rdlc";

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("ITI_INV_SR5_ReportDataHeader", data.Tables[0]);
                        localReport.AddDataSource("ITI_INV_SR5_ReportDataTable", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("GetIssueItemsForApprove")]
        public async Task<ApiResult<DataTable>> GetIssueItemsForApprove([FromBody] inventoryIssueHistorySearchModel body)
        {
            ActionName = "GetIssueItemsForApprove([FromBody] inventoryIssueHistorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetIssueItemsForApprove(body));
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

        [HttpPost("ApproveIssuedItems")]
        public async Task<ApiResult<int>> ApproveIssuedItems([FromBody] List<ApproveIssuedItemsDataModel> request)
        {
            ActionName = "ApproveIssuedItems([FromBody] ApproveIssuedItemsDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.ApproveIssuedItems(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
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
        [HttpPost("ApproveSR5Items")]
        public async Task<ApiResult<int>> ApproveSR5Items([FromBody] List<ApproveIssuedItemsDataModel> request)
        {
            ActionName = "ApproveSR5Items([FromBody] ApproveIssuedItemsDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.ApproveSR5Items(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
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
        [HttpPost("GetAllData4LabIncharge")]
        public async Task<ApiResult<DataTable>> GetAllData4LabIncharge([FromBody] DTEItemsSearchModel4Lab body)
        {
            ActionName = "GetAllData4LabIncharge()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAllData4LabIncharge(body));
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

        [HttpPost("MarkAuctionedItems_SR6")]
        public async Task<ApiResult<int>> MarkAuctionedItems_SR6([FromBody] AuctionDetailsModel request)
        {
            ActionName = "MarkAuctionedItems_SR6([FromBody] AuctionDetailsModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.iDTEItemsMasterRepository.MarkAuctionedItems_SR6(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
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

        [HttpPost("GetAllNotWorkingItemReport")]
        public async Task<ApiResult<DataTable>> GetAllNotWorkingItemReport([FromBody] NotWorkingItemReportSearchModel body)
        {
            ActionName = "GetAllNotWorkingItemReport([FromBody] NotWorkingItemReportSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iDTEItemsMasterRepository.GetAllNotWorkingItemReport(body));
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



    }
}



