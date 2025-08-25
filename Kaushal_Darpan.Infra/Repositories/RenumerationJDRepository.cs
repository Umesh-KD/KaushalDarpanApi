using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.GroupCodeAllocation;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Kaushal_Darpan.Models.RenumerationJD;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class RenumerationJDRepository : IRenumerationJDRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public RenumerationJDRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "RenumerationJDRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<List<RenumerationJDModel>> GetAllData(RenumerationJDRequestModel filterModel)
        {
            _actionName = "GetAllData(RenumerationExaminerRequestModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<RenumerationJDModel> obj = new List<RenumerationJDModel>();
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_RenumerationJD";

                        command.Parameters.AddWithValue("@action", "_getRenumerationByStatus");

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@Status", filterModel.RenumerationExaminerStatusID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    if (dt != null)
                    {
                        obj = CommonFuncationHelper.ConvertDataTable<List<RenumerationJDModel>>(dt);
                    }

                    return obj;
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
        public async Task<int> SaveDataApprovedAndSendToAccounts(List<RenumerationJDSaveModel> request)
        {
            _actionName = "SaveDataApprovedAndSendToAccounts(RenumerationJDSaveModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "usp_SaveRenumerationExaminerGroupCode";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "_saveApprovedAndSendtoAccounts");
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






