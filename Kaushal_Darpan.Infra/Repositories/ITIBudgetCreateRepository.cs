using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITI_BGTHeadmaster;
using Kaushal_Darpan.Models.ITIBUDGET;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIBudgetCreateRepository: I_ITIBudgetCreateRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIBudgetCreateRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIBudgetCreateRepository";
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
                    using (var command = await _dbContext.CreateCommandAsync())
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

        public async Task<DataTable> GetITIBudgetDropdown(ITIBudgetDropdownDataModel model)
        {
            _actionName = "GetITIBudgetDropdown(ITIBudgetDropdownDataModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_Dropdowns";
                        command.Parameters.AddWithValue("@Action", model.Action);
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

        public async Task<int> SaveDataBudgetCreate_Admin(ITIBudgetCreateDataModel request)
        {
            _actionName = "SaveDataBudgetCreate_Admin(ITIBudgetCreateDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {


                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BGT_BudgetCreate_IU";
                        command.Parameters.AddWithValue("@ActionName", "SaveData");

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request.BudgetHeadList));                        
                        command.Parameters.AddWithValue("@BudgetTypeID", request.BudgetTypeID);
                        command.Parameters.AddWithValue("@BudgetTypeName", request.BudgetTypeName);
                        command.Parameters.AddWithValue("@AcademicYearID", request.AcademicYearID);
                        command.Parameters.AddWithValue("@BudgetForID", request.BudgetForID);
                        command.Parameters.AddWithValue("@BudgetType_Cumulative_HeadWise", request.BudgetType_Cumulative_HeadWise);
                        command.Parameters.AddWithValue("@CumulativeAmount", request.CumulativeAmount);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@TotalAmount", request.TotalAmount);
                        command.Parameters.AddWithValue("@DistributedType", request.DistributedType);
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
    }
}
