using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.HostelManagement;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class HostelRoomDetailsRepository : IHostelRoomDetailsRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public HostelRoomDetailsRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "HostelRoomDetailsRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        public async Task<int> SaveData(RoomDetailsModel request)
        {
            _actionName = "SaveData(RoomDetailsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_HostelRoomDetails";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@HSRoomID", request.HSRoomID);
                        command.Parameters.AddWithValue("@HostelID", request.HostelID);
                        command.Parameters.AddWithValue("@RoomTypeID", request.RoomTypeID);
                        command.Parameters.AddWithValue("@RoomNo", request.RoomNo);
                        command.Parameters.AddWithValue("@StudyTableFacilities", request.StudyTableFacilities);
                        command.Parameters.AddWithValue("@AttachedBathFacilities", request.AttachedBathFacilities);
                        command.Parameters.AddWithValue("@FanFacilities", request.FanFacilities);
                        command.Parameters.AddWithValue("@CoolingFacilities", request.CoolingFacilities);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@Action", "INSERT");
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
        public async Task<int> SaveExcelData(List<RoomExcelDetailsModel> request)
        {
            _actionName = "SaveData(RoomDetailsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveExcelRoomData";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
                    }
                    return retval;
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

        public async Task<DataTable> GetAllData(int HostelID=0, int RoomTypeID = 0)
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
                        command.CommandText = "USP_HostelRoomDetails";
                        command.Parameters.AddWithValue("@HostelID", HostelID);
                        command.Parameters.AddWithValue("@RoomTypeID", RoomTypeID);
                        command.Parameters.AddWithValue("@Action", "List");
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
        
        public async Task<DataTable> GetRoomDDLList(int HostelID=0, int RoomTypeID = 0, int EndTermID=0)
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
                        command.CommandText = "USP_HostelRoomDetails";
                        command.Parameters.AddWithValue("@HostelID", HostelID);
                        command.Parameters.AddWithValue("@RoomTypeID", RoomTypeID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);
                        command.Parameters.AddWithValue("@Action", "RoomDDLList");
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


        public async Task<bool> DeleteDataByID(StatusChangeModelNew request)
        {
            _actionName = "DeleteDataByID(ItemCategoryModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        //command.CommandText = $"update M_ItemCategoryMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ItemCategoryID = {request.ItemCategoryID}";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelRoomDetails";
                        command.Parameters.AddWithValue("@Action", "Delete");
                        command.Parameters.AddWithValue("@HSRoomID", request.PK_ID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
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


        public async Task<RoomDetailsModel> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        //command.CommandText = "select * from M_ItemCategoryMaster Where ItemCategoryID ='" + PK_ID + "' ";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelRoomDetails";
                        command.Parameters.AddWithValue("@Action", "GetById");
                        command.Parameters.AddWithValue("@HSRoomID", PK_ID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new RoomDetailsModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<RoomDetailsModel>(dataTable);
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


    }
}
