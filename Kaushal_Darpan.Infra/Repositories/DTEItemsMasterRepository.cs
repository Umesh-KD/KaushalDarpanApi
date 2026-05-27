using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.EquipmentsMaster;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using Kaushal_Darpan.Models.ItemsMaster;
using Kaushal_Darpan.Models.StaffMaster;
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
            _actionName = "GetAllData(DTEItemsSearchModel SearchReq)";
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
                        command.Parameters.AddWithValue("@IsConsumable", SearchReq.ItemType);
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



        public async Task<DataTable> GetAllDataHistory(DTEItemsSearchModel SearchReq)
        {
            _actionName = "GetAllData(DTEItemsSearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllDTEItemsHistory";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@CollegeId", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@StatusID", SearchReq.StatusID);
                        command.Parameters.AddWithValue("@IsConsumable", SearchReq.ItemType);
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
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_INV_GetStockItemById";

                        command.Parameters.AddWithValue("@ItemId", PK_ID);

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
                        command.Parameters.AddWithValue("@ReceiptBookFolio", request.receiptbookfolio);
                        command.Parameters.AddWithValue("@IssueDate", request.issuedate);
                        command.Parameters.AddWithValue("@IndentNo", request.IndentNo);
                        command.Parameters.AddWithValue("@IssueBookFolioDate", request.issuebookfoliodate);
                        command.Parameters.AddWithValue("@QuantityIssued", request.QuantityIssued); 
                        command.Parameters.AddWithValue("@QuantityBalance", request.QuantityBalance);
                        command.Parameters.AddWithValue("@BillFileName", request.BillFileName);
                        command.Parameters.AddWithValue("@BillFilePath", request.BillFilePath);
                        command.Parameters.AddWithValue("@Specification", request.Specification);
                        command.Parameters.AddWithValue("@MappingId", request.MappingId);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
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
                                IsSerialNo = row.Field<int?>("IsSerialNo") ?? 0

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
                        command.Parameters.AddWithValue("@Authority", request.Authority_forAuctionOrder);
                        command.Parameters.AddWithValue("@ModeOfDisposal", request.ModeOfDisposal);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);
                        command.Parameters.AddWithValue("@ApproximateCost", request.ApproximateCost);
                        command.Parameters.AddWithValue("@ItemDetails", JsonConvert.SerializeObject(request.ItemDetails));

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
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
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
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StaffIssueReturnItems";
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
            _actionName = "GetAllinventoryIssueHistory(inventoryIssueHistorySearchModel SearchReq)";
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
                        command.Parameters.AddWithValue("@UserID", SearchReq.UserID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@status", SearchReq.status);
                        command.Parameters.AddWithValue("@IsStaff", SearchReq.IsStaff);
                        command.Parameters.AddWithValue("@IssueStatus", SearchReq.IssueStatus);
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



        public async Task<DataTable> GetAllinventoryIssueHistoryTrail(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "GetAllinventoryIssueHistory(inventoryIssueHistorySearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_GetAllInventoryIssueHistoryTrail";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.ItemID);
                        command.Parameters.AddWithValue("@UserID", SearchReq.UserID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@status", SearchReq.status);
                        command.Parameters.AddWithValue("@IsStaff", SearchReq.IsStaff);
                        command.Parameters.AddWithValue("@IssueStatus", SearchReq.IssueStatus);
                        command.Parameters.AddWithValue("@ItemDetailsId", SearchReq.ItemDetailsId);
                        command.Parameters.AddWithValue("@IssuedId", SearchReq.IssuedId);
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

        public async Task<DataTable> GetAllInventoryIssueReturnItemList(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "GetAllInventoryIssueReturnItemList(inventoryIssueHistorySearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_GetAllInventoryIssueReturnItemList";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.ItemID);
                        command.Parameters.AddWithValue("@ActionName", SearchReq.actionName);
                        command.Parameters.AddWithValue("@ReturnStatus", SearchReq.ReturnStatus);
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
            _actionName = "GetItemListType(DTEItemsSearchModel SearchReq)";
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
                        command.Parameters.AddWithValue("@ItemType", SearchReq.ItemType);
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
            _actionName = "GetAllItemList(DTEItemsSearchModel SearchReq)";
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
                        command.Parameters.AddWithValue("@ItemType", SearchReq.ItemType);
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

        public async Task<DataTable> GetAllinventoryIssueReport(ItemsIssueReturnModels SearchReq)
        {
            _actionName = "GetAllinventoryIssueReport(ItemsIssueReturnModels SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_ItemReportList";
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

        public async Task<DataTable> GetIssueItemList(ItemsIssueReturnModels SearchReq)
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
                        command.CommandText = "USP_Bter_GetIssueItemList";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffId);
                        command.Parameters.AddWithValue("@UserID", SearchReq.UserID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
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

        public async Task<DataTable> GetInventoryIssueHistoryList(InventoryIssueHistoryListModels SearchReq)
        {
            _actionName = "GetInventoryIssueHistoryList(InventoryIssueHistoryListModels SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetInventoryIssueHistoryList";

                        //command.Parameters.AddWithValue("@IssuedId", SearchReq.IssuedId);
                        command.Parameters.AddWithValue("@staffId", SearchReq.staffId);

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
        public async Task<DataTable> GetDTEIssueItemListPermanent(int EquipmentsId, int ItemCategoryId,int InstituteID)
        {
            _actionName = "GetAllStoksNew()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DTE_GetIssueItemList_Permanent";
                        command.Parameters.AddWithValue("@EquipmentsId", EquipmentsId);
                        command.Parameters.AddWithValue("@ItemCategoryId", ItemCategoryId);
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);
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
        public async Task<DataTable> GetDTEIssueSubmitPermanent(ItemsIssueReturnModels SearchReq)
        {
            _actionName = "GetIssueSubmitPermanent()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_SaveDTEIssuedItemsPermanent";
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(SearchReq.ItemList));
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffId);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@TradeId", SearchReq.TradeId);
                        command.Parameters.AddWithValue("@FileName", SearchReq.FileName);
                        command.Parameters.AddWithValue("@StreamID", SearchReq.StreamID);
                        command.Parameters.AddWithValue("@LabID", SearchReq.LabID);
                        command.Parameters.AddWithValue("@IndentNo", SearchReq.IndentNo);
                        command.Parameters.AddWithValue("@UserId", SearchReq.UserId);
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
        public async Task<DataTable> GetDTEGetSetLabMaster(DTELabMasterModel SearchReq)
        {
            _actionName = "GetDTEGetSetLabMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DTELabMaster_Operation";
                        command.Parameters.AddWithValue("@ActionName", SearchReq.ActionName);
                        command.Parameters.AddWithValue("@Lab_Id", SearchReq.Lab_Id);
                        command.Parameters.AddWithValue("@Lab_Name", SearchReq.Lab_Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Lab_DepartmentId", SearchReq.Lab_DepartmentId);
                        command.Parameters.AddWithValue("@Lab_BranchId", SearchReq.Lab_BranchId);
                        command.Parameters.AddWithValue("@Lab_CollegeId", SearchReq.Lab_CollegeId);
                        command.Parameters.AddWithValue("@Lab_TechnicianId", SearchReq.Lab_TechnicianId);
                        command.Parameters.AddWithValue("@Lab_ActiveStatus", SearchReq.Lab_ActiveStatus);
                        command.Parameters.AddWithValue("@Lab_DeleteStatus", SearchReq.Lab_DeleteStatus);
                        //command.Parameters.AddWithValue("@Lab_RTS", SearchReq.Lab_RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Lab_CreatedBy", SearchReq.Lab_CreatedBy);
                        command.Parameters.AddWithValue("@Lab_ModifyBy", SearchReq.Lab_ModifyBy);
                        //command.Parameters.AddWithValue("@Lab_ModifyDate", SearchReq.Lab_ModifyDate ?? (object)DBNull.Value); 
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

        public async Task<DataTable> DTE_INV_SaveLabItemReturn(ItemsIssueReturnModels SearchReq)
        {
            _actionName = "GetAll_INV_returnItem()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DTE_INV_SaveLabItemReturn";
                        command.Parameters.AddWithValue("@ItemList", JsonConvert.SerializeObject(SearchReq.ItemList));
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);


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

        public async Task<int> MarkForAuctionSR6(List<ItemsIssueReturnModels> request)
        {
            _actionName = "SaveIssueItemsList(List<DTEItemsSaveModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_INV_MarkForAuctionSR6";
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

        public async Task<DataTable> Get_SR5_ReportData(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "Get_SR5_ReportData(inventoryIssueHistorySearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_SR5_ReportData";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.ItemID);
                        command.Parameters.AddWithValue("@UserID", SearchReq.UserID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@status", SearchReq.status);
                        command.Parameters.AddWithValue("@IsStaff", SearchReq.IsStaff);
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

        public async Task<DataTable> Get_SR6_ReportData(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "Get_SR6_ReportData(inventoryIssueHistorySearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_SR6_ReportData";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.ItemID);
                        command.Parameters.AddWithValue("@UserID", SearchReq.UserID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@status", SearchReq.status);
                        command.Parameters.AddWithValue("@IsStaff", SearchReq.IsStaff);
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

        public async Task<DataSet> DownloadSR6ReportData_pdf_BTER(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "DownloadSR6ReportData_pdf_BTER(inventoryIssueHistorySearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_SR6_ReportData_pdf";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.ItemID);
                        //command.Parameters.AddWithValue("@ReturnStatus", SearchReq.ReturnStatus);
                        command.Parameters.AddWithValue("@IsStaff", SearchReq.IsStaff);
                        command.Parameters.AddWithValue("@UserID", SearchReq.UserID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
                    }
                    return ds;
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

        public async Task<DataSet> Download_SR5ReportData_pdf_BTER(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "Download_SR5ReportData_pdf_BTER(inventoryIssueHistorySearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_SR5_ReportData_pdf";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.ItemID);
                        //command.Parameters.AddWithValue("@ReturnStatus", SearchReq.ReturnStatus);
                        command.Parameters.AddWithValue("@IsStaff", SearchReq.IsStaff);
                        command.Parameters.AddWithValue("@UserID", SearchReq.UserID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
                    }
                    return ds;
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

        public async Task<DataTable> GetIssueItemsForApprove(inventoryIssueHistorySearchModel SearchReq)
        {
            _actionName = "GetIssueItemsForApprove(inventoryIssueHistorySearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_INV_GetIssueItemsForApprove";
                        command.Parameters.AddWithValue("@StaffID", SearchReq.StaffID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@ItemID", SearchReq.ItemID);
                        command.Parameters.AddWithValue("@UserID", SearchReq.UserID);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@status", SearchReq.status);
                        command.Parameters.AddWithValue("@IsStaff", SearchReq.IsStaff);

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

        public async Task<int> ApproveIssuedItems(List<ApproveIssuedItemsDataModel> request)
        {
            _actionName = "ApproveIssuedItems(ApproveIssuedItemsDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandText = "USP_Bter_INV_ApproveIssuedItems";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ItemList", JsonConvert.SerializeObject(request));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; 

                        _sqlQuery = command.GetSqlExecutableQuery();
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
        public async Task<int> ApproveSR5Items(List<ApproveIssuedItemsDataModel> request)
        {
            _actionName = "ApproveSR5Items(ApproveIssuedItemsDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandText = "USP_Bter_INV_SR5ApprovalOfItems";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ItemList", JsonConvert.SerializeObject(request));
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                    command.Parameters.Add("@Return", SqlDbType.Int);
                    command.Parameters["@Return"].Direction = ParameterDirection.Output;

                    _sqlQuery = command.GetSqlExecutableQuery();
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
        }
        public async Task<DataTable> GetAllData4LabIncharge(DTEItemsSearchModel4Lab SearchReq)
        {
            _actionName = "GetAllData4LabIncharge(DTEItemsSearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllDTEItems4LabIncharge";
                        command.Parameters.AddWithValue("@EquipmentsId", SearchReq.EquipmentsId);
                        command.Parameters.AddWithValue("@CollegeId", SearchReq.CollegeId);
                        command.Parameters.AddWithValue("@RoleID", SearchReq.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", SearchReq.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@StatusID", SearchReq.StatusID);
                        command.Parameters.AddWithValue("@IsConsumable", SearchReq.ItemType);
                        command.Parameters.AddWithValue("@UserId", SearchReq.UserId);
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

        public async Task<int> MarkAuctionedItems_SR6(AuctionDetailsModel request)
        {
            _actionName = "MarkAuctionedItems_SR6(AuctionDetailsModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    // Set the stored procedure name and type
                    command.CommandText = "USP_BTER_INV_SaveAuctionData_SR6";
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters with appropriate null handling
                    command.Parameters.AddWithValue("@ItemDetailsId", request.ItemDetailsId);
                    command.Parameters.AddWithValue("@AuctionDate", request.AuctionDate);
                    command.Parameters.AddWithValue("@Dis_AuctionDoc", request.Dis_AuctionDoc);
                    command.Parameters.AddWithValue("@AuctionDoc", request.AuctionDoc);
                    command.Parameters.AddWithValue("@AuctionQuantity", request.AuctionQuantity);
                    command.Parameters.AddWithValue("@Authority", request.Authority_forAuctionOrder);
                    command.Parameters.AddWithValue("@ModeOfDisposal", request.ModeOfDisposal);
                    command.Parameters.AddWithValue("@Remarks", request.Remarks);
                    command.Parameters.AddWithValue("@ApproximateCost", request.ApproximateCost);
                    command.Parameters.AddWithValue("@ItemDetails", JsonConvert.SerializeObject(request.ItemDetails));

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
        }
    }
}








