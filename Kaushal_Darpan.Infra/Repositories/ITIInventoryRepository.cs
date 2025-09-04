using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CreateTpoMaster;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.ITIInventoryDashboard;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIInventoryRepository : IITIInventoryRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIInventoryRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIInventoryDashboardRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<bool> DeleteCategoryMasterByID(DTEItemCategoryModel request)
        {
            _actionName = "DeleteDataByID(DTEItemCategoryModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update M_ITI_ItemCategoryMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ItemCategoryID = {request.ItemCategoryID}";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> DeleteEquipmentsMappingByID(DTETradeEquipmentsMapping request)
        {
            _actionName = "DeleteDataByID(DTETradeEquipmentsMapping request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update M_ITI_EquipmentsMapping  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where TE_MappingId = {request.TE_MappingId}";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> DeleteEquipmentsMasterByID(DTEEquipmentsModel request)
        {
            _actionName = "DeleteDataByID(ItemCategoryModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update M_ITI_EquipmentsMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where EquipmentsId = {request.EquipmentsId}";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> DeleteIssuedItemsByID(DTEIssuedItems request)
        {
            _actionName = "DeleteDataByID(ItemCategoryModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update M_ITI_IssuedItems  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where IssuedId = {request.IssuedId}";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> DeleteItemsMasterByID(DTEItemsModel productDetails)
        {
            _actionName = "DeleteDataByID(DTEItemsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update M_ITI_ItemsMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{productDetails.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ItemId = {productDetails.ItemId}";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> DeleteItemUnitMasterByID(DTEItemUnitModel productDetails)
        {
            _actionName = "DeleteDataByID(DTEItemUnitModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update M_ITI_ItemUnitMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{productDetails.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where UnitId = {productDetails.UnitId}";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllAuctionList(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllAuctionDetail";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@OfficeID", SearchReq.OfficeID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllCategoryMaster()
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllItemCategory";
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllEquipmentsMapping(DTESearchTradeEquipmentsMapping SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllDteMappingEquipments";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentId);
                        command.Parameters.AddWithValue("@CategoryId", SearchReq.CategoryId);
                        command.Parameters.AddWithValue("@TradeId", SearchReq.TradeId);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@OfficeID", SearchReq.OfficeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllEquipmentsMaster(CommonSearchModal modal)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DepartmentID", modal.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", modal.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", modal.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", modal.InstituteID);
                        command.Parameters.AddWithValue("@OfficeID", modal.OfficeID);
                        if (modal.RoleID == 2)
                        {
                            command.Parameters.AddWithValue("@RoleID", 0);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@RoleID", modal.RoleID);
                        }

                        command.CommandText = "USP_ITI_GetAllEquipments";
                        _sqlQuery = command.GetSqlExecutableQuery();
                            dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllIssuedItems(DTEIssuedItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllIssuedItems";
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@EquipmentCode", SearchReq.EquipmentCode);
                        command.Parameters.AddWithValue("@Issuenumber", SearchReq.Issuenumber);
                        command.Parameters.AddWithValue("@IssueQuantity", SearchReq.IssueQuantity);
                        command.Parameters.AddWithValue("@TradeId", SearchReq.TradeId);
                        command.Parameters.AddWithValue("@Issuedate", SearchReq.Issuedate);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<List<DTEItemsDetailsModel>> GetAllItemDetails(int PK_ID)
        {
            _actionName = "GetItemDetails(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "USP_ITI_Get_EquipmentsItemDetails_ByID";  // Stored Procedure
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", PK_ID);
                        command.Parameters.AddWithValue("@Action", "getItemCode");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    // List to hold multiple ItemDetailsModel objects
                    var itemsList = new List<DTEItemsDetailsModel>();

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var item = new DTEItemsDetailsModel
                            {
                                ItemCode = row["ItemCode"].ToString(),
                                ItemDetailsId = row.Field<int?>("ItemDetailsId") ?? 0,
                                EquipmentCode = row["EquipmentsCode"] == DBNull.Value ? "0" : row["EquipmentsCode"].ToString(),
                                EquipmentWorking = row.Field<int?>("EquipmentWorking") ?? 0,
                                isOption = row["isOption"] != DBNull.Value && Convert.ToBoolean(row["isOption"])
                            };
                            itemsList.Add(item);
                        }
                    }


                    return itemsList;  // Returning the list of items
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllItemsMaster(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllItems";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@CollegeId", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@OfficeID", SearchReq.OfficeID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllItemUnitMaster()
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllItemUnits";
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllRequestEquipmentsMapping(DTESearchTradeEquipmentsMapping SearchReq)
        {
            _actionName = "GetAllRequestData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllRequestMappingEquipments";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentId);
                        command.Parameters.AddWithValue("@CategoryId", SearchReq.CategoryId);
                        command.Parameters.AddWithValue("@OfficeID", SearchReq.OfficeID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllRetunItem(DTEReturnItemSearchModel SearchReq)
        {
            _actionName = "GetAllRetunItem()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllReturnItems";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@CategoryId", SearchReq.CategoryId);
                        command.Parameters.AddWithValue("@Issuenumber", SearchReq.Issuenumber);

                        command.Parameters.AddWithValue("@Issuedate", SearchReq.FilterIssuedate);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllStoks(DTEStoksSearchModel SearchReq)
        {
            _actionName = "GetAllStoks()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.CommandText = "USP_ITI_GetAllStoks";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@TradeId", SearchReq.TradeId);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllStoksBalance(DTEStoksSearchModel SearchReq)
        {
            _actionName = "GetAllStoks()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllStoksBalance";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DTEEquipmentsModel> GetByIDEquipmentsMaster(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "select * from M_ITI_EquipmentsMaster Where EquipmentsId ='" + PK_ID + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new DTEEquipmentsModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<DTEEquipmentsModel>(dataTable);
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DTEItemCategoryModel> GetCategoryMasterByID(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "select * from M_ITI_ItemCategoryMaster Where ItemCategoryID ='" + PK_ID + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new DTEItemCategoryModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<DTEItemCategoryModel>(dataTable);
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DTETradeEquipmentsMapping> GetEquipmentsMappingByID(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "select * from M_ITI_EquipmentsMapping Where TE_MappingId ='" + PK_ID + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new DTETradeEquipmentsMapping();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<DTETradeEquipmentsMapping>(dataTable);
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetInventoryDashboard(ITIInventoryDashboard filterModel)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_AdminInventoryDashboard";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID );
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@ModifyBy", filterModel.ModifyBy );
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DTEIssuedItems> GetIssuedItemsByID(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "select * from M_ITI_IssuedItems  Where IssuedId  ='" + PK_ID + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new DTEIssuedItems();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<DTEIssuedItems>(dataTable);
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DTEItemsDetailsModel> GetItemDetails(int PK_ID)
        {
            _actionName = "GetDTEItemDetails(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "USP_ITI_Get_EquipmentsItemDetails_ByID";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", PK_ID);
                        command.Parameters.AddWithValue("@Action", "getDetails");
                        //command.CommandText = "select * from M_ItemsMaster Where ItemId ='" + PK_ID + "' ";
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new DTEItemsDetailsModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<DTEItemsDetailsModel>(dataTable);
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DTEItemsModel> GetItemsMasterByID(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "select * from M_ITI_ItemsMaster Where ItemId ='" + PK_ID + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new DTEItemsModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<DTEItemsModel>(dataTable);
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DTEItemUnitModel> GetItemUnitMasterByID(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "select * from M_ITI_ItemUnitMaster Where UnitId ='" + PK_ID + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new DTEItemUnitModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<DTEItemUnitModel>(dataTable);
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> HODEquipmentVerifications(DTETradeEquipmentsMapping request)
        {
            _actionName = "HOD_EquipmentVerifications(DTETradeEquipmentsMapping request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_HOD_EquipmentVerifications";
                        command.Parameters.AddWithValue("@Action", "UpdateStatuVerifications");
                        command.Parameters.AddWithValue("@TE_MappingId", request.TE_MappingId);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> SaveAuctionData(AuctionDetailsModel request)
        {
            _actionName = "SaveData(DTEItemsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_AuctionDocUpdate";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ItemDetailsId", request.ItemDetailsId);
                        command.Parameters.AddWithValue("@OfficeID", request.OfficeID);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@AuctionDate", request.AuctionDate);
                        command.Parameters.AddWithValue("@Dis_AuctionDoc", request.Dis_AuctionDoc);
                        command.Parameters.AddWithValue("@AuctionDoc", request.AuctionDoc);
                        command.Parameters.AddWithValue("@AuctionQuantity", request.AuctionQuantity);
                        command.Parameters.AddWithValue("@RowsID", request.RowsID);
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
                    }

                    return result;

                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> SaveCategoryMaster(DTEItemCategoryModel request)
        {
            _actionName = "SaveData(DTEItemCategoryModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_ItemCategory_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ItemCategoryID", request.ItemCategoryID);
                        command.Parameters.AddWithValue("@ItemCategoryName", request.ItemCategoryName);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
                    }

                    return result;

                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> SaveDataReturnItem(ReturnDteIssuedItems request)
        {
            _actionName = "SaveDataReturnDTEItem(ReturnIssuedItems request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_ReturnIssuedItems_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@IssuedId", request.IssuedId);
                        command.Parameters.AddWithValue("@ItemId", request.ItemId);
                        command.Parameters.AddWithValue("@ReturnIssueNumber", request.ReturnIssueNumber);
                        command.Parameters.AddWithValue("@ReturnQuantity", request.ReturnQuantity);
                        command.Parameters.AddWithValue("@ReturnIssueDate", request.ReturnIssueDate);
                        command.Parameters.AddWithValue("@ReturnStatus", 1);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ReturnRemark", request.ReturnRemark);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> SaveEquipmentsMapping(DTETradeEquipmentsMapping request)
        {
            _actionName = "SaveData(DTETradeEquipmentsMapping request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_MappingEquipments_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@TE_MappingId", request.TE_MappingId);
                        command.Parameters.AddWithValue("@TradeId", request.TradeId);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@CategoryId", request.CategoryId);
                        command.Parameters.AddWithValue("@EquipmentId", request.EquipmentId);
                        command.Parameters.AddWithValue("@Quantity", request.Quantity);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        
                        command.Parameters.Add(new SqlParameter("@RTS", SqlDbType.DateTime)
                        {
                            Value = request.RTS ?? (object)DBNull.Value
                        });
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.Add(new SqlParameter("@ModifyDate", SqlDbType.DateTime)
                        {
                            Value = request.ModifyDate ?? (object)DBNull.Value
                        });
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@TradeIdTypeId", request.TradeIdTypeId);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out

                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> SaveEquipmentsMappingRequestData(DTERequestTradeEquipmentsMapping request)
        {
            _actionName = "SaveData(DTETradeEquipmentsMapping request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_SaveEquipmentsMappingRequest_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@TE_MappingId", request.TE_MappingId);
                        command.Parameters.AddWithValue("@TradeId", request.TradeId);
                        command.Parameters.AddWithValue("@OfficeID", request.OfficeID);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@CategoryId", request.CategoryId);
                        command.Parameters.AddWithValue("@EquipmentId", request.EquipmentId);
                        command.Parameters.AddWithValue("@Quantity", request.Quantity);
                        command.Parameters.AddWithValue("@IdentificationMark", request.IdentificationMark);
                        command.Parameters.AddWithValue("@PricePerUnit", request.PricePerUnit);
                        command.Parameters.AddWithValue("@TotalPrice", request.TotalPrice);
                        command.Parameters.AddWithValue("@VoucherNumber", request.VoucherNumber);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@TradeIdTypeId", request.TradeIdTypeId);
                        command.Parameters.AddWithValue("@CompanyName", request.CompanyName);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out

                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> SaveEquipmentsMasterData(DTEEquipmentsModel request)
        {
            _actionName = "SaveData(ItemCategoryModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_Equipments_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@EquipmentsId", request.EquipmentsId);
                        command.Parameters.AddWithValue("@ItemCategoryId", request.ItemCategoryId);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@UnitId", request.UnitId);
                        command.Parameters.AddWithValue("@Specification", request.Specification);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (SqlException ex)
                {
                    // Check if the error message contains the "ItemCategoryName already exists"
                    if (ex.Message.Contains("ItemName already exists"))
                    {
                        // Specific handling for duplicate error
                        throw new Exception(Constants.MSG_SAVE_Duplicate);
                    }
                    else
                    {
                        // Handle other SQL exceptions
                        var errorDesc = new ErrorDescription
                        {
                            Message = ex.Message,
                            PageName = _pageName,
                            ActionName = _actionName,
                            SqlExecutableQuery = _sqlQuery
                        };
                        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                        throw new Exception(errordetails, ex);
                    }
                }
            });
        }

        public async Task<int> SaveIssuedItems(DTEIssuedItems request)
        {
            _actionName = "SaveData(DTEIssuedItems request)";
            return await Task.Run(async () =>
            {
                try
                {
                    string formattedDateTime = "";
                    if (request != null)
                    {

                        if (request?.IssueDate != null)
                        {
                            formattedDateTime = request.IssueDate.Value.ToString("yyyy/MM/dd HH:mm:ss");
                            // Use formattedDateTime as needed (e.g., send to frontend, log, etc.)
                        }

                    }

                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_IssuedItems_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@IssuedId", request.IssuedId);
                        command.Parameters.AddWithValue("@EquipmentsId", request.EquipmentsId);
                        command.Parameters.AddWithValue("@ItemId", request.ItemId);
                        command.Parameters.AddWithValue("@CategoryId", request.CategoryId);
                        command.Parameters.AddWithValue("@TradeId", request.TradeId);
                        command.Parameters.AddWithValue("@IssueNumber", request.IssueNumber);
                        command.Parameters.AddWithValue("@IssueQuantity", request.IssueQuantity);
                        command.Parameters.AddWithValue("@IssueDate", formattedDateTime);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);

                    }

                    return result;

                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> SaveItemsMaster(DTEItemsModel request)
        {
            _actionName = "SaveData(DTEItemsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_Items_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ItemId", request.ItemId);
                        command.Parameters.AddWithValue("@TradeId", request.TradeId);
                        command.Parameters.AddWithValue("@OfficeID", request.OfficeID);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@ItemCategoryId", request.ItemCategoryId);
                        command.Parameters.AddWithValue("@EquipmentsId", request.EquipmentsId);
                        command.Parameters.AddWithValue("@IdentificationMark", request.IdentificationMark);
                        command.Parameters.AddWithValue("@CampanyName", request.CampanyName);
                        command.Parameters.AddWithValue("@VoucherNumber", request.VoucherNumber);
                        command.Parameters.AddWithValue("@Quantity", request.Quantity);
                        command.Parameters.AddWithValue("@PricePerUnit", request.PricePerUnit);
                        command.Parameters.AddWithValue("@TotalPrice", request.TotalPrice);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@ItemType", request.ItemType);
                        command.Parameters.AddWithValue("@IsConsume", request.IsConsume);
          
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);


                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
                    }

                    return result;

                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> SaveItemUnitMaster(DTEItemUnitModel request)
        {
            _actionName = "SaveData(DTEItemUnitModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_ItemUnit_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@UnitId", request.UnitId);
                        command.Parameters.AddWithValue("@UnitName", request.UnitName);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> SaveRequestEquipmentsMapping(DTETEquipmentsRequestMapping request)
        {
            _actionName = "SaveRequestData(DTETradeEquipmentsMapping request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_RequestMappingEquipments_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@TE_MappingId", request.TE_MappingId);
                        command.Parameters.AddWithValue("@TradeId", request.TradeId);
                        command.Parameters.AddWithValue("@OfficeID", request.OfficeID);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@CategoryId", request.CategoryId);
                        command.Parameters.AddWithValue("@EquipmentId", request.EquipmentId);
                        command.Parameters.AddWithValue("@Quantity", request.Quantity);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@TradeIdTypeId", request.TradeIdTypeId);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> UpdateItemData(List<DTEItemsDetailsModel> itemsDetails)
        {
            int totalRowsAffected = 0;

            try
            {
                if (itemsDetails == null || itemsDetails.Count == 0)
                {
                    throw new ArgumentException("Item details cannot be null or empty.");
                }

                // Serialize all items at once
                string jsonData = JsonConvert.SerializeObject(itemsDetails);

                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandText = "USP_ITI_UpdateItemData_IU";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ItemData", jsonData);

                    var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(rowsAffectedParam);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    await command.ExecuteNonQueryAsync();

                    totalRowsAffected += (int)rowsAffectedParam.Value;
                    _sqlQuery = command.GetSqlExecutableQuery();
                }

                return totalRowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating item data", ex);
            }
        }

        public async Task<bool> UpdateStatusEquipmentsMapping(DTEUpdateStatusMapping request)
        {
            _actionName = "SaveData(DTETradeEquipmentsMapping request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_MappingUpdateEquipments_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@TE_MappingId", request.TE_MappingId);
                        command.Parameters.AddWithValue("@CategoryId", request.CategoryId);
                        command.Parameters.AddWithValue("@EquipmentId", request.EquipmentId);
                        command.Parameters.AddWithValue("@OfficeID", request.OfficeID);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out

                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> UpdateStatusItemsData(DTEItemsModel request)
        {
            _actionName = "SaveData(DTEItemsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_UpdateStatusItems_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ItemId", request.ItemId);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);


                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
                    }

                    return result;

                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetEquipment_Branch_Wise_CategoryWise(int Category)
        {
            _actionName = "GetEquipment_Branch_Wise_CategoryWise()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ITIEquipment_CategoryWise";

                        command.Parameters.AddWithValue("@ID", Category);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllDeadStockReport(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllDeadStockReport";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@CollegeId", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@OfficeID", SearchReq.OfficeID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllAuctionReport(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllAuctionReport";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@OfficeID", SearchReq.OfficeID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllinventoryIssueHistory(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAllInventoryIssueHistory";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@CollegeId", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@OfficeID", SearchReq.OfficeID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);   
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@ItemId", SearchReq.ItemId);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAll_INV_GetCommonIssueDDL(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_INV_GetCommonIssueDDL";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@TypeName", SearchReq.TypeName);
                        command.Parameters.AddWithValue("@TradeId", SearchReq.TradeId);
                        //command.Parameters.AddWithValue("@TradeId", SearchReq.TradeId);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }


        public async Task<DataTable> GetAllDDL(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_INV_GetCommonIssueDDL";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.CollegeId);
                        if (SearchReq.StatusID == 1)
                        {
                            command.Parameters.AddWithValue("@TypeName", "staffList");
                        }
                        else if (SearchReq.StatusID == 2)
                        {
                            command.Parameters.AddWithValue("@TypeName", "TradeList");
                        }
                        else if (SearchReq.StatusID == 3)
                        {
                            command.Parameters.AddWithValue("@TypeName", "ItemList");
                        }

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetConsumeItemList(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_INV_ConsumeItemList";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@ActionType", "GetConsumeItemList");
                      
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> SaveIssueItems(ItemsIssueReturnModels request)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_INV_StaffIssueReturnItems";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ItemId", request.ItemId);
                        command.Parameters.AddWithValue("@TradeId", request.TradeId);
                        command.Parameters.AddWithValue("@ItemCategoryId", request.ItemCategoryId);
                        command.Parameters.AddWithValue("@StaffId", request.StaffId);
                        command.Parameters.AddWithValue("@StaffName", request.StaffName);
                        command.Parameters.AddWithValue("@Quantity", request.Quantity);
                        command.Parameters.AddWithValue("@DueDate", request.DueDate);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@UserID", request.UserId);
                        command.Parameters.AddWithValue("@Type", "Insert");

                        command.Parameters.AddWithValue("@ItemList", JsonConvert.SerializeObject(request.ItemList));

                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
    }
}








