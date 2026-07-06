using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.RoleMaster;
using Org.BouncyCastle.Utilities.Collections;

//namespace Kaushal_Darpan.Infra.Repositories
//{
//    internal class HiringRoleMasterRepository
//    {
//    }
//}
namespace Kaushal_Darpan.Infra.Repositories
{
    public class HiringRoleMasterRepository : IHiringRoleMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public HiringRoleMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "HiringRoleMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetAllData()
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HiringRoleMasterList";
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                });
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

        public async Task<DataTable> GetAllSanction()
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SanctionDetailsList";
                        command.Parameters.AddWithValue("@action","GetallData");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                });
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


        public async Task<HiringRoleMasterModel> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        _sqlQuery = $" select * from M_HiringRoleMaster Where ID='{PK_ID}'";
                        command.CommandText = _sqlQuery;
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new HiringRoleMasterModel();
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<HiringRoleMasterModel>(dataTable);
                        }
                    }
                    return data;
                });
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


        public async Task<SanctionOrderMasterModel> GetByIDSanction(int PK_ID)
        {
            _actionName = "GetByIDSanction(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        _sqlQuery = $" select * from M_SanctionMaster Where SanctionID='{PK_ID}'";
                        command.CommandText = _sqlQuery;
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new SanctionOrderMasterModel();
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<SanctionOrderMasterModel>(dataTable);
                        }
                    }
                    return data;
                });
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



        public async Task<bool> SaveData(HiringRoleMasterModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveData(HiringRoleMasterModel request)";
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HiringRoleMaster_IU";
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.UserID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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



        public async Task<bool> SaveDataSanction(SanctionOrderMasterModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveDataSanction(SanctionOrderMasterModel request)";
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SanctionDetailsList";
                        command.Parameters.AddWithValue("@SanctionID", request.SanctionID);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ParentID", request.ParentID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@action", "SaveData");
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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


        public async Task<bool> SaveSanctionOrder(OrderDetailsList request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveDataSanction(SanctionOrderMasterModel request)";
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SanctionOrderList";
                        command.Parameters.AddWithValue("@SanctionID", request.SanctionID);
                        command.Parameters.AddWithValue("@ParentID", request.ParentID);
               

                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@OrderCopy", request.OrderCopy);
                        command.Parameters.AddWithValue("@OrderDate", request.OrderDate);
                        command.Parameters.AddWithValue("@OrderNo", request.OrderNo);
                        command.Parameters.AddWithValue("@OrderType", request.OrderType);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@action", "SaveDataOrder");
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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

        public async Task<bool> UpdateData(HiringRoleMasterModel request)
        {

            return await Task.Run(async () =>
            {
                _actionName = "UpdateData(HiringRoleMasterModel request)";
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HiringRoleMaster_IU";
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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
        public async Task<bool> DeleteDataByID(HiringRoleMasterModel request)
        {

            int result = 0;
            _actionName = "DeleteDataByID(HiringRoleMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        _sqlQuery = $" update M_HiringRoleMaster set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} '," +
                        $"ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where SanctionID={request.ID}";
                        command.CommandText = _sqlQuery;
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

        public async Task<bool> DeleteSanctionOrder(HiringRoleMasterModel request)
        {

            int result = 0;
            _actionName = "DeleteSanctionOrder(HiringRoleMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandText = @"UPDATE OrderDetailsList 
                        SET ActiveStatus = 0, 
                            ModifyBy = @ModifyBy, 
                            ModifyDate = GETDATE() 
                        WHERE ID = @ID";

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ID", request.ID);

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


        public async Task<bool> DeleteDataBySanctionID(HiringRoleMasterModel request)
        {

            int result = 0;
            _actionName = "DeleteDataByID(HiringRoleMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        _sqlQuery = $" update M_SanctionMaster set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} '," +
                        $"ModifyDate=GETDATE(),IPAddress='{_IPAddress}'                         Where SanctionID={request.ID}";
                        command.CommandText = _sqlQuery;
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

        public async Task<DataTable> GetsanctionOrder(OrderDetailsList body)
        {
            _actionName = "GetAllData()";
            try
            {              
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SanctionOrderList";
                        command.Parameters.AddWithValue("@OrderType", body.OrderType);
                        command.Parameters.AddWithValue("@OrderNo", body.OrderNo);
                        command.Parameters.AddWithValue("@ParentID", body.ParentID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@action", "GetAllData");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                });
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


        public async Task<DataTable> GetOrderDetailsList_ByDate(OrderDetailsList body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SanctionOrderList";
                        command.Parameters.AddWithValue("@OrderDate", body.SelectedDate);
                        command.Parameters.AddWithValue("@TypeID", body.TypeID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@action", "_getSactionedOrder_ByDate");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                });
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


        public async Task<DataTable> GetsanctionOrderNotAssign(OrderDetailsList body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SanctionOrderList";
                         command.Parameters.AddWithValue("@OrderType", body.OrderType);
                        command.Parameters.AddWithValue("@OrderNo", body.OrderNo);
                        command.Parameters.AddWithValue("@OrderDate", body.OrderDate);
                        command.Parameters.AddWithValue("@SanctionID", body.SanctionID);
                        command.Parameters.AddWithValue("@ParentID", body.ParentID);
                        command.Parameters.AddWithValue("@action", "GetNotAssign");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                });
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



        public async Task<OrderDetailsList> GetByIDSanctionOrder(int PK_ID)
        {
            _actionName = "GetByIDSanction(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SanctionOrderList";
                        command.Parameters.AddWithValue("@action", "GetByID");
                        command.Parameters.AddWithValue("@SanctionID",PK_ID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new OrderDetailsList();
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<OrderDetailsList>(dataTable);
                        }
                    }
                    return data;
                });
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

        #region AnnouncementTypesMaster
        public async Task<DataTable> GetAllAnnouncementTypes(AnnouncementTypeMasterModel request)
        {
            _actionName = "GetAllAnnouncementTypes()";

            try
            {
                DataTable dataTable;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_AnnouncementTypeMaster_GetData";

                    command.Parameters.AddWithValue("@Action", "GetAllData");
                    command.Parameters.AddWithValue("@ID", request.ID);
                    command.Parameters.AddWithValue("@Name", request.Name ?? "");

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

                throw new Exception(CommonFuncationHelper.MakeError(errorDesc), ex);
            }
        }

        public async Task<AnnouncementTypeMasterModel> GetAnnouncementTypeByID(int id)
        {
            _actionName = "GetAnnouncementTypeByID()";

            try
            {
                DataTable dataTable;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_AnnouncementTypeMaster_GetData";

                    command.Parameters.AddWithValue("@Action", "GetByID");
                    command.Parameters.AddWithValue("@ID", id);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    dataTable = await command.FillAsync_DataTable();
                }

                var data = new AnnouncementTypeMasterModel();

                if (dataTable != null)
                {
                    data = CommonFuncationHelper.ConvertDataTable<AnnouncementTypeMasterModel>(dataTable);
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

                throw new Exception(CommonFuncationHelper.MakeError(errorDesc), ex);
            }
        }

        public async Task<bool> SaveAnnouncementType(AnnouncementTypeMasterModel request)
        {
            _actionName = "SaveAnnouncementType()";

            try
            {
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_AnnouncementTypeMaster_AddUpdate";

                    command.Parameters.AddWithValue("@ID", request.ID);
                    command.Parameters.AddWithValue("@Name", request.Name ?? "");
                    command.Parameters.AddWithValue("@IsActive", request.IsActive);
                    command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);

                    _sqlQuery = command.GetSqlExecutableQuery();

                    await command.ExecuteNonQueryAsync();
                }

                return true;
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

                throw new Exception(CommonFuncationHelper.MakeError(errorDesc), ex);
            }
        }

        public async Task<bool> DeleteAnnouncementTypeByID(AnnouncementTypeMasterModel request)
        {
            _actionName = "DeleteAnnouncementTypeByID()";

            try
            {
                int result;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandText = @"UPDATE AnnouncementType_Master
                                    SET IsDelete = 1,
                                        UpdatedBy = @UpdatedBy,
                                        UpdatedDate = GETDATE()
                                    WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", request.ID);
                    command.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy ?? 0);

                    _sqlQuery = command.GetSqlExecutableQuery();

                    result = await command.ExecuteNonQueryAsync();
                }

                return result > 0;
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

        #endregion
    }
}