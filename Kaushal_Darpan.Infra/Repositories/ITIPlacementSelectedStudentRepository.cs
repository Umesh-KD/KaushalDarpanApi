using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITIPlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.ITIPlacementShortListStudentMaster;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIPlacementSelectedStudentRepository : I_ITIPlacementSelectedStudentRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;  
        public ITIPlacementSelectedStudentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "PlacementSelectedStudentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<List<ITIPlacementSelectedStudentResponseModel>> GetAllData(ITIPlacementSelectedStudentSearchModel searchModel)
        {
            _actionName = "GetAllData(ITIPlacementSelectedStudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIPlacementSelectedStudents";
                        //command.Parameters.AddWithValue("@DepartmentID", 1);
                        //command.Parameters.AddWithValue("@Eng_NonEng", searchModel.Eng_NonEng);

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "_getAllData");
                       // command.Parameters.AddWithValue("@RoleId", searchModel.RoleId);
                        //command.Parameters.AddWithValue("@UserId", searchModel.UserId);
                        command.Parameters.AddWithValue("@BranchID", searchModel.BranchID);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", searchModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@CampusPostID", searchModel.CampusPostID);
                        command.Parameters.AddWithValue("@HiringRoleID", searchModel.HiringRoleID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<ITIPlacementSelectedStudentResponseModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<ITIPlacementSelectedStudentResponseModel>>(dataTable);
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


        public async Task<int> SaveAllData(List<ITIPlacementSelectedStudentResponseModel> entity)
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
                        command.CommandText = "USP_AddITISelectedData";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditAllData");
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








