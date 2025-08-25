using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.GroupCodeAllocation;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class GroupCodeAllocationRepository : IGroupCodeAllocationRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public GroupCodeAllocationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "GroupCodeAllocationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<List<GroupCodeAllocationAddEditModel>> GetAllData(GroupCodeAllocationSearchModel filterModel)
        {
            _actionName = "GetAllData(GroupCodeAllocationSearchModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<GroupCodeAllocationAddEditModel> groupCode = new List<GroupCodeAllocationAddEditModel>();
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetGroupCodeMaster";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterId);
                        command.Parameters.AddWithValue("@CommonSubjectYesNo", filterModel.CommonSubjectYesNo);
                        command.Parameters.AddWithValue("@PartitionSize", filterModel.PartitionSize);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    if (dt != null)
                    {
                        groupCode = CommonFuncationHelper.ConvertDataTable<List<GroupCodeAllocationAddEditModel>>(dt);
                    }

                    return groupCode;
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

        public async Task<int> SaveData(List<GroupCodeAllocationAddEditModel> request, int StartValue)
        {
            _actionName = "SaveData(List<GroupCodeAllocationAddEditModel> request, int StartValue)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEditGroupCodeAllocation";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditData");
                        command.Parameters.AddWithValue("@StartValue", StartValue);
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));

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

        public async Task<List<GroupCodeAddEditModel>> GetPartitionData(GroupCodeAllocationSearchModel filterModel)
        {
            _actionName = "GetPartitionData(GroupCodeAllocationSearchModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<GroupCodeAddEditModel> groupCode = new List<GroupCodeAddEditModel>();
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;//max
                        command.CommandText = "USP_GetGroupCodeMaster";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "_getPartiotionData");
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterId);
                        command.Parameters.AddWithValue("@CommonSubjectYesNo", filterModel.CommonSubjectYesNo);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@PartitionSize", filterModel.PartitionSize);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    if (dt != null)
                    {
                        groupCode = CommonFuncationHelper.ConvertDataTable<List<GroupCodeAddEditModel>>(dt);
                    }
                    return groupCode;
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

        public async Task<int> SavePartitionData(List<GroupCodeAddEditModel> request)
        {
            _actionName = "SavePartitionData(List<GroupCodeAddEditModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEditGroupCodeMaster";
                        command.CommandTimeout = 0;//max
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditPartiotionData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));

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






