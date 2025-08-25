using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ITIApplication;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class InternalSlidingRepository : IInternalSlidingRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public InternalSlidingRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "InternalSlidingRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetInternalSliding(SearchSlidingModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_ITI_Internal_Sliding";
                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationNo);
                        command.Parameters.AddWithValue("@TradeID", body.TradeID);
                        command.Parameters.AddWithValue("@CollegeID", body.CollegeID);
                        command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
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

        public async Task<DataTable> GetGenerateAllotment(SearchSlidingModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_InternalSliding";
                        command.Parameters.AddWithValue("@collegeID", body.CollegeID);
                        command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
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
        public async Task<DataTable> GetDDLInternalSlidingUnitList(SearchSlidingModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_InternalSlidingUnit";
                        command.Parameters.AddWithValue("@ID", body.InsID);
                        //command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        command.Parameters.AddWithValue("@SeatIntakeId", body.SeatIntakeId);
                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@Action", body.action);
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

        public async Task<DataTable> SaveData(SearchSlidingModel request)
        {
            _actionName = "SaveData(CollegeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIInternalSlidingSave";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CollegeId", request.CollegeID);
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@CollegeTradeId", request.InsID);
                        command.Parameters.AddWithValue("@ShiftUnitID", request.UnitID);
                        command.Parameters.AddWithValue("@CreatedBy", request.UserId);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@AllotmentId", request.AllotmentId);
                        command.Parameters.AddWithValue("@AllotedCategory", request.AllotedCategory);
                        command.Parameters.AddWithValue("@SeatMetrixId", request.SeatMetrixId);
                        command.Parameters.AddWithValue("@Action", request.action);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

        public async Task<DataTable> SaveSawpData(SearchSlidingModel request)
        {
            _actionName = "SaveData(CollegeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIInternalSwappingSave";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CollegeId", request.CollegeID);
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@ApplicationNo", request.SwapApplicationNo);
                        command.Parameters.AddWithValue("@CollegeTradeId", request.InsID);
                        command.Parameters.AddWithValue("@ShiftUnitID", request.UnitID);
                        command.Parameters.AddWithValue("@CreatedBy", request.UserId);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@AllotmentId", request.AllotmentId);
                        command.Parameters.AddWithValue("@Action", request.action);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

    }
}