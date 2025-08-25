using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITIBUDGET;
using Kaushal_Darpan.Models.NodalApperentship;
using Kaushal_Darpan.Models.UserMaster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class BudgetHeadManagementRepository : IBudgetHeadManagementRepository
    {



        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public BudgetHeadManagementRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BudgetHeadManagementRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetAllData(BudgetHeadSearchFilter model)
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
                        command.CommandText = "USP_ITI_BGT_CollegeBudgetAlloted_GET";
                        command.Parameters.AddWithValue("@FinYearID", model.FinYearID);
                        command.Parameters.AddWithValue("@CollegeID", model.CollegeID);
                        command.Parameters.AddWithValue("@ActionType", model.ActionName);
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

        public async Task<DataTable> GetAllBudgetManagementData(BudgetHeadSearchFilter model)
        {
            _actionName = "GetAllBudgetManagementData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_ITI_BGT_BudgetManagementList";
                        command.Parameters.AddWithValue("@CollegeID", model.CollegeID);
                        command.Parameters.AddWithValue("@DistributedType", model.DistributedType);
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

        public async Task<int> Save_CollegeBudgetAlloted(CollegeBudgetAllotedModel request)
        {
            _actionName = "Save_CollegeBudgetAlloted(CollegeBudgetAllotedModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {


                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_BGT_CollegeBudgetAlloted";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DistributedID", request.DistributedID);
                        command.Parameters.AddWithValue("@ActionType", request.ActionType);
                        command.Parameters.AddWithValue("@CollegeID", request.CollegeID);
                        command.Parameters.AddWithValue("@FinYearID", request.FinYearID);
                        command.Parameters.AddWithValue("@DistributedType", request.DistributedType);
                        command.Parameters.AddWithValue("@DistributedAmount", request.DistributedAmount);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);
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

        public async Task<int> Save_CollegeBudgetRequest(BudgetRequestModel request)
        {
            _actionName = "Save_CollegeBudgetAlloted(CollegeBudgetAllotedModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_BGT_CollegeBudgetRequests";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RequestID", request.RequestID);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@CollegeID", request.CollegeId);
                        command.Parameters.AddWithValue("@FinYearID", request.FinYearID);
                        command.Parameters.AddWithValue("@ApprovedAmount", request.ApprovedAmount);
                        command.Parameters.AddWithValue("@RequestAmount", request.RequestAmount);
                        command.Parameters.AddWithValue("@StatusId", request.StatusId);
                        command.Parameters.AddWithValue("@RequestFileName", request.RequestFileName);
                        command.Parameters.AddWithValue("@CreatedBy", request.UserID);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
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


        

        public async Task<int> Save_CollegeBudgetUtilizations(List<CollegeBudgetUtilizationModel> request)
        {
            _actionName = "Save_CollegeBudgetAlloted(CollegeBudgetAllotedModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {


                        // Set the stored procedure name and type
                        command.CommandText = "USP_BGT_AddEditCollegeBudgetUtilizations";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ActionName", "InsertRecord");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
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

        public async Task<DataTable> GetUtilizationsData(BudgetHeadSearchFilter model)
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
                        command.CommandText = "USP_BGT_AddEditCollegeBudgetUtilizations_Get";
                        command.Parameters.AddWithValue("@DistributedID", model.DistributedID);
                        command.Parameters.AddWithValue("@ActionName", model.ActionName);
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

        public async Task<DataTable> GetBudgetRequestData(BudgetHeadSearchFilter model)
        {
            _actionName = "GetBudgetRequestData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_CollegeBudgetRequests_GET";
                        command.Parameters.AddWithValue("@RequestID", model.RequestID);
                        command.Parameters.AddWithValue("@Action", model.ActionName);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@CollegeID", model.CollegeID);
                        command.Parameters.AddWithValue("@FinYearID", model.FinYearID);
                        command.Parameters.AddWithValue("@Status", model.StatusId);
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



    }
}
