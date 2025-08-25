using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITICenterObserver;
using Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel;
using Kaushal_Darpan.Models.ITIResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIResultRepository : I_ITIResultRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIResultRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIResultRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GenerateResult(ITIResultsModel model)
        {
            _actionName = "GenerateResult()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIResult";
                        command.Parameters.AddWithValue("@Action", "GenerateResult");
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
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

        public async Task<DataTable> GetResultData(ITIResultsModel model)
        {
            _actionName = "GetResultData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIResult";
                        command.Parameters.AddWithValue("@Action", "GetResultData");
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
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


        public async Task<DataTable> PublishResult(ITIResultsModel model)
        {
            _actionName = "PublishResult(ITIResultsModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIResult";
                        command.Parameters.AddWithValue("@Action", "PublishResult");
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<DataTable> GetCurrentStatusOfResult(ITIResultsModel model)
        {
            _actionName = "GetCurrentStatusOfResult(ITIResultsModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIResult";
                        command.Parameters.AddWithValue("@Action", "GetCurrentStatus");
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
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

        public async Task<DataSet> GetCFormReport(ITIResultsModel model)
        {
            _actionName = "GetCurrentStatusOfResult(ITIResultsModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataTable = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_C_FROM";
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@Action", model.Action);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync();
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

        public async Task<DataTable> GetStudentPassFailResultData(ITIStudentPassFailResultsModel model)
        {
            _actionName = "GetStudentPassFailResultData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIStudentPassFailResultData";
                        command.Parameters.AddWithValue("@Action", "GetResultData");
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@ResultStatus", model.Results);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        command.Parameters.AddWithValue("@StudentType", model.StudentType);
                        command.Parameters.AddWithValue("@is_appeared", model.is_appeared);
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


        



        public async Task<DataTable> GetITITradeList(ITIStudentPassFailResultsModel model)
        {
            _actionName = "GetITITradeList(ITIStudentPassFailResultsModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIStudentPassFailResultData";
                        command.Parameters.AddWithValue("@Action", "_getTradeList");
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@ResultStatus", model.Results);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        command.Parameters.AddWithValue("@StudentType", model.StudentType);
                        command.Parameters.AddWithValue("@is_appeared", model.is_appeared);
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

    }
}
