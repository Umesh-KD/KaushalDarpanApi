using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.IssueTrackerMasters;
using Kaushal_Darpan.Models.ITICenterAllocaqtion;
using Kaushal_Darpan.Models.ScholarshipMaster;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class IssueTrackerRepository : IIssueTrackerRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public IssueTrackerRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "IssueTrackerRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SaveData(IssueSaveData request)
        {
            _actionName = "SaveData(IssueTrackerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_InsertIssueTracker";

                        // Add all input parameters

                        command.Parameters.AddWithValue("@IssueID", request.IssueID);
                        command.Parameters.AddWithValue("@ProjectName", request.ProjectName);

                        command.Parameters.AddWithValue("@Description", request.Discription);
                        command.Parameters.AddWithValue("@PriorityID", request.PriorityID);
                        command.Parameters.AddWithValue("@StatusID", request.StatusTypeID);
                        command.Parameters.AddWithValue("@IssueFileName", request.Document);
                        command.Parameters.AddWithValue("@IssueDisFileName", request.Dis_DocName);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);

              
                        command.Parameters.AddWithValue("@IssueName", request.IssueName);
                        command.Parameters.AddWithValue("@RegFileName", request.RegFileName);
                        command.Parameters.AddWithValue("@RegDisFileName", request.RegDisFileName);
                        command.Parameters.AddWithValue("@json", JsonConvert.SerializeObject(request.IssueFile));
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@IssueTypeID", request.IssueTypeID);
                        command.Parameters.AddWithValue("@IssueTypeDescriptionID", request.IssueTypeDescriptionID);
                        
                        // Output parameter
                 
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

        public async Task<DataTable> GetAllData(IssueTrackerListSearchModel model)
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
                        command.CommandText = "USP_BTER_IssueTracker";
                        //command.Parameters.AddWithValue("@DepartmentID", model.IssueID);
                        command.Parameters.AddWithValue("@IssueName", model.Issue);
                        command.Parameters.AddWithValue("@ActionType", "GetAllData");
                        command.Parameters.AddWithValue("@IssueDate", model.IssueDate);
                        //command.Parameters.AddWithValue("@StreamID", model.IssuePriorityDate);
                        //command.Parameters.AddWithValue("@SemesterID", model.IssueresolvedDate);
                        command.Parameters.AddWithValue("@Priority", model.PriorityID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@IssueTypeID", model.IssueTypeID);
                        command.Parameters.AddWithValue("@IssueTypeDescriptionID", model.IssueTypeDescriptionID);

                        

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
       
        public async Task<DataTable> GetUserList(int RoleID)
        {
            _actionName = "GetUserList()";
            DataTable dataTable = new DataTable();

            try
            {
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetUserListByRoleID";
                    command.Parameters.AddWithValue("@RoleID", RoleID);
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
        }



        //GetRoleList

        public async Task<DataTable> GetRoleList()
        {
            _actionName = "GetRoleList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetRoleList";
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

        public async Task<IssueTrackerMaster> GetById(int IssueID)
        {
            _actionName = "GetById(int IssueID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_IssueTracker";

                        // Pass required parameters
                        command.Parameters.AddWithValue("@ActionType", "GetByID"); // Adjust value if needed
                        command.Parameters.AddWithValue("@IssueID", IssueID); // Fix variable name too

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    var data = new IssueTrackerMaster();
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<IssueTrackerMaster>(dataTable);
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

        public async Task<bool> DeleteDataByID(IssueTrackerMaster request)
        {
            //Change Action Name
            _actionName = "DeleteDataByID(SubjectMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = " select * from M_StreamMaster Where StreamID='" + PK_ID + "' "; ;
                        //Change SP name
                        command.CommandText = "USP_BTER_IssueTracker";
                        command.Parameters.AddWithValue("@IssueID", request.IssueID);
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


        public async Task<int> AssignIssure(List<IssueTrackerListSearchModel> entity)
        {
            _actionName = "SaveAllData(List<CreateTpoAddEditModel> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIAssignIssue";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling

  
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
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


