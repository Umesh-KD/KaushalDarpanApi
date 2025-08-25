using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PlacementVerifiedStudentTPOMaster;
using Newtonsoft.Json;
using System.Data;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class PlacementVerifiedStudentTPORepository : IPlacementVerifiedStudentTPORepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public PlacementVerifiedStudentTPORepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "PlacementVerifiedStudentTPORepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<List<PlacementVerifiedStudentTPOResponseModel>> GetAllData(PlacementVerifiedStudentTPOSearchModel searchModel)
        {
            _actionName = "GetAllData(PlacementVerifiedStudentTPOSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPlacementVerifiedStudentTPO";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@CampusPostID", searchModel.CampusPostID);
                        command.Parameters.AddWithValue("@CampusWiseHireRoleID", searchModel.CampusWiseHireRoleID);
                        command.Parameters.AddWithValue("@InstituteID", searchModel.InstituteID);
                        command.Parameters.AddWithValue("@BranchID", searchModel.BranchID);
                        command.Parameters.AddWithValue("@_10thPre", searchModel._10thPre);
                        command.Parameters.AddWithValue("@_12thPre", searchModel._12thPre);
                        command.Parameters.AddWithValue("@DiplomaPre", searchModel.DiplomaPre);
                        command.Parameters.AddWithValue("@FinancialYearID", searchModel.FinancialYearID);
                        command.Parameters.AddWithValue("@NoOfBack", searchModel.NoOfBack);
                        command.Parameters.AddWithValue("@AgeFrom", searchModel.AgeFrom);
                        command.Parameters.AddWithValue("@AgeTo", searchModel.AgeTo);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", searchModel.Eng_NonEng);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<PlacementVerifiedStudentTPOResponseModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<PlacementVerifiedStudentTPOResponseModel>>(dataTable);
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

        public async Task<int> SaveAllData(List<PlacementShortListStudentResponseModel> entity)
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
                        command.CommandText = "     ";
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








