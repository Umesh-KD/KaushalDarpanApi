using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CollegeAdmissionSeatAllotment;
using Kaushal_Darpan.Models.ITIIMCAllocation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CollegeAdmissionSeatAllotmentRepository: ICollegeAdmissionSeatAllotmentRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CollegeAdmissionSeatAllotmentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BterApplicationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetApplicationDatabyID(ApplicationSearchDataModel searchRequest)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = " select * from M_StreamMaster Where StreamID='" + PK_ID + "' "; ;
                        command.CommandText = "USP_CollegeAdmissionSeatAllotment";
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);
                        command.Parameters.AddWithValue("@Action", searchRequest.Action);

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

        public async Task<DataTable> GetTradeListByCollege(SeatMatrixSearchModel body)
        {
            _actionName = "GetTradeListByCollege()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeAdmissionSeatAllotment";

                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        //command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        //command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@Action", "GetTradeListByCollege");

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

        public async Task<DataTable> GetBranchListByCollege(SeatMatrixSearchModel body)
        {
            _actionName = "GetTradeListByCollege()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeAdmissionSeatAllotment";

                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@AcadmicYearID", body.AcadmicYearID);
                        //command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        //command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@Action", "GetBranchListByCollege");

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

        public async Task<DataTable> ShiftUnitList(SeatMatrixSearchModel body)
        {
            _actionName = "ShiftUnitList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeAdmissionSeatAllotment";

                        command.Parameters.AddWithValue("@CollegeTradeID", body.CollegeTradeID);
                        //command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        //command.Parameters.AddWithValue("@TradeID", body.TradeID);
                        command.Parameters.AddWithValue("@Action", body.action);

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

        public async Task<int> UpdateAllotments(SeatAllocationDataModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveData(GroupMaster request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeAdmissionSeatAllotment";
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@TradeID", request.TradeID);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@CollegeTradeID", request.CollegeTradeID);
                        command.Parameters.AddWithValue("@ShiftUnit", request.ShiftUnit);
                        command.Parameters.AddWithValue("@AllotedCategory", request.AllotedCategory);
                        command.Parameters.AddWithValue("@SeatMetrixColumn", request.SeatMetrixColumn);
                        command.Parameters.AddWithValue("@SeatMetrixId", request.SeatMetrixId);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@DirectAdmissionType", request.DirectAdmissionType);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Action", request.action);
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
    }
}
