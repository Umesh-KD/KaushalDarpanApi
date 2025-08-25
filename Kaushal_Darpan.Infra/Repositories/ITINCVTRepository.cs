using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTEReatsDistributionsMaster;
using Kaushal_Darpan.Models.ITIIMCAllocation;
using Kaushal_Darpan.Models.ITINCVT;
using Newtonsoft.Json;
using System.Data;
using System.Drawing.Printing;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITINCVTRepository : IITINCVTRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITINCVTRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "IITINCVTRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        public async Task<DataTable> GetAllData(ITINCVTDataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_NCVT_DATA_PUSH";
                            
                        //command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", body.IPAddress);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.sortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.sortColumn);
                        command.Parameters.AddWithValue("@SearchText", body.SearchText);
                        command.Parameters.AddWithValue("@Action", body.Action);


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

        public async Task<string> UpdatePushStatus(ITINCVTDataModel body)
        {
            _actionName = "UpdatePushStatus()";
            try
            {
                return await Task.Run(async () =>
                {
                    string result = "";
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_NCVT_DATA_PUSH";

                        //command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@PushData", body.PushData);
                        command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", body.IPAddress);     
                        command.Parameters.AddWithValue("@Action", "UpdatePushStatus");


                         result = command.ExecuteScalar().ToString();// Get sql query
                        //dataTable =  command.FillAsync_DataTable();
                    }
                    return result;
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

        public async Task<DataTable> GetNCVTExamDataFormat(ITINCVTDataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_NCVT_IMPORT_EXAM_DATA";

                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@IPAddress", body.IPAddress);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.sortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.sortColumn);
                        command.Parameters.AddWithValue("@SearchText", body.SearchText);
                        command.Parameters.AddWithValue("@CollegeType", body.CollegeType);
                        command.Parameters.AddWithValue("@CollegeId", body.CollegeId);
                        command.Parameters.AddWithValue("@CollegeCode", body.CollegeCode);
                        command.Parameters.AddWithValue("@TradeId", body.TradeId);
                        command.Parameters.AddWithValue("@TradeCode", body.TradeCode);


                        command.Parameters.AddWithValue("@Action", body.Action);


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

        public async Task<DataTable> SaveExamData1(List<ITINCVTImportDataModel> model)
        {
            _actionName = "SaveNVCTExamDatalist(List<ITINCVTImportDataModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_NCVT_IMPORT_EXAM_DATA";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_addStudentEligibleForEnrollmentData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@Action", "ImportData");

                       // command.Parameters.Add("@Retval", SqlDbType.Int);// out
                       // command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        var data = await command.ExecuteNonQueryAsync();

                        return dataTable;
                    }

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

        public async Task<DataTable> SaveExamData(List<ITINCVTImportDataModel> model)
        {
            _actionName = "SaveSeatsMatrixlist(List<BTERSeatMetrixModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_NCVT_IMPORT_EXAM_DATA";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_addStudentEligibleForEnrollmentData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@Action", "ImportData");

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();


                       // _sqlQuery = command.GetSqlExecutableQuery();
                       // result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Retval"].Value);// out



                        return dataTable;
                    }

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
