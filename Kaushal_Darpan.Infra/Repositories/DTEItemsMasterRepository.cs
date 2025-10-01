using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.EquipmentsMaster;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using Kaushal_Darpan.Models.ItemsMaster;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class DTEItemsMasterRepository : IDTEItemsMasterRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public DTEItemsMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "DTEItemsMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllDTEItems";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@CollegeId", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@StatusID", SearchReq.StatusID);
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
        public async Task<DTEItemsModel> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandText = "select * from M_DteItemsMaster Where ItemId ='" + PK_ID + "' ";

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
        public async Task<int> SaveData(DTEItemsModel request)
        {
            _actionName = "SaveData(DTEItemsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_DTEItems_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ItemId", request.ItemId);
                        command.Parameters.AddWithValue("@TradeId", request.TradeId);
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
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsConsume", request.IsConsume);
                        command.Parameters.AddWithValue("@voucherdate", request.voucherdate);
                        command.Parameters.AddWithValue("@unitId", request.unitId);
                        command.Parameters.AddWithValue("@abbreviation", request.abbreviation);
                        command.Parameters.AddWithValue("@batchId", request.batchId);

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
        public async Task<int> UpdateStatusItemsData(DTEItemsModel request)
        {
            _actionName = "SaveData(DTEItemsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_DTEUpdateStatusItems_IU";
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
        public async Task<bool> DeleteDataByID(DTEItemsModel request)
        {
            _actionName = "DeleteDataByID(DTEItemsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandText = $"update M_DteItemsMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ItemId = {request.ItemId}";

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


        public async Task<DTEItemsDetailsModel> GetDTEItemDetails(int PK_ID)
        {
            _actionName = "GetDTEItemDetails(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandText = "USP_BTER_Get_DteEquipmentsItemDetails_ByID";
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

        public async Task<List<DTEItemsDetailsModel>> GetAllDTEItemDetails(int PK_ID)
        {
            _actionName = "GetItemDetails(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandText = "USP_BTER_Get_DteEquipmentsItemDetails_ByID";  // Stored Procedure
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
                                isOption = row["isOption"] != DBNull.Value && Convert.ToBoolean(row["isOption"]),
                                AuctionStatus = row["AuctionStatus"].ToString(),
                                ItemId = Convert.ToInt32(row["ItemId"].ToString()),
                                IsSerialNo = Convert.ToInt32(row["IsSerialNo"].ToString())
                               
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

        public async Task<int> UpdateDTEItemData(List<DTEItemsDetailsModel> itemsDetails)
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

                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandText = "USP_UpdateDteItemData_IU";
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

        public async Task<DataTable> GetAllAuctionList(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAll_AuctionDetail";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.CollegeId);
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

        public async Task<int> SaveAuctionData(AuctionDetailsModel request)
        {
            _actionName = "SaveData(DTEItemsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTER_Auction_Doc_Update";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ItemDetailsId", request.ItemDetailsId);
                        command.Parameters.AddWithValue("@AuctionDate", request.AuctionDate);
                        command.Parameters.AddWithValue("@Dis_AuctionDoc", request.Dis_AuctionDoc);
                        command.Parameters.AddWithValue("@AuctionDoc", request.AuctionDoc);
                        command.Parameters.AddWithValue("@AuctionQuantity", request.AuctionQuantity);
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


        public async Task<int> EquipmentCodeDuplicate(EquipmentCodeDuplicateSearch request)
        {
            _actionName = "EquipmentCodeDuplicate(EquipmentCodeDuplicateSearch request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Bter_EquipmentCodeDuplicate";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ItemCategoryName", request.ItemCategoryName);
                        command.Parameters.AddWithValue("@EquipmentsCode", request.EquipmentsCode);
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


        public async Task<DataTable> CheckItemAuction(CheckItemAuctionSearch request)
        {
            _actionName = "CheckItemAuction(CheckItemAuctionSearch request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CheckItemAuction";
                        command.Parameters.AddWithValue("@Item", request.ItemId);
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


        public async Task<int> UpdateStatusRevert(DTEItemsModel request)
        {
            _actionName = "SaveData(DTEItemsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_DTEUpdateRevertStatusItems";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ItemId", request.ItemId);                        
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
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


        public async Task<DataTable> GetAll_INV_GetCommonIssueDDL(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_GetCommonIssueDDL";
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


        public async Task<DataTable> GetConsumeItemList(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_ConsumeItemList";
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
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_INV_StaffIssueReturnItemsBter";
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

        public async Task<DataTable> GetInventoryIssueItemList(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "GetInventoryIssueItemList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_GetIssueItemList";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffID);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.ItemID);
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

        public async Task<DataTable> GetAll_INV_returnItem(ItemsIssueReturnModels SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_INV_StaffIssueReturnItemsBter";

                        command.Parameters.AddWithValue("@Type", "ReturnItemUpdate");
                        command.Parameters.AddWithValue("@Remarks", SearchReq.Remarks);
                        command.Parameters.AddWithValue("@ItemCategoryId", SearchReq.ItemCategoryId);
                        command.Parameters.AddWithValue("@ReturnDate", SearchReq.ReturnDate);
                        command.Parameters.AddWithValue("@ConditionAtReturn", SearchReq.ConditionAtReturn);

                        command.Parameters.AddWithValue("@ItemList", JsonConvert.SerializeObject(SearchReq.ItemList));


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

        public async Task<DataTable> GetAllinventoryIssueHistory(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_GetAllInventoryIssueHistory";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.ItemID);
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


        public async Task<DataTable> GetItemListType(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_ItemListType";

                        command.Parameters.AddWithValue("@InstituteID", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@ItemType", SearchReq.IsConsumable);
                        command.Parameters.AddWithValue("@ActionType", "GetItemListType");

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

        public async Task<DataTable> GetAllItemList(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_ItemListType";

                        command.Parameters.AddWithValue("@InstituteID", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@ItemType", SearchReq.IsConsumable);
                        command.Parameters.AddWithValue("@ActionType", "GetItemList");

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


        public async Task<int> SaveIssueItemsList(List<ItemsIssueReturnModels> request)
        {
            _actionName = "SaveIssueItems(List<DTEItemsSaveModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; 
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "SP_SaveDTEIssuedItems";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);
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
    }
}








