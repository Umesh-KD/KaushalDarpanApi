using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PlacementStudentMaster;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class PlacementStudentRepository : IPlacementStudentRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public PlacementStudentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "PlacementStudentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<List<PlacementStudentResponseModel>> GetAllData(PlacementStudentSearchModel searchModel)
        {
            _actionName = "GetAllData(PlacementStudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPlacementStudents";

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

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<PlacementStudentResponseModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<PlacementStudentResponseModel>>(dataTable);
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

        public async Task<DataTable> GetPlacementconsent(StudentConsentSearchmodel body)
        {
            _actionName = "CampusValidationList(int CollegeID, string Status)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPlacementconsent";
                        command.Parameters.AddWithValue("@Action", body.action);
                        command.Parameters.AddWithValue("@CompanyID", body.CompanyID);
                        command.Parameters.AddWithValue("@PostID", body.PostID);
                        command.Parameters.AddWithValue("@CollegeID", body.CollegeID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<(int Result, string RegistrationNo)> SaveData(CampusStudentConsentModel entity)
        {
            _actionName = "SaveData(CampusStudentConsentModel entity)";
            try
            {
                int result = 0;
                string registrationNo = string.Empty;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandText = "USP_CampusStudentConsent_IU";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate null handling
                    command.Parameters.AddWithValue("@PostID", entity.PostID);
                    command.Parameters.AddWithValue("@StudentID", entity.StudentID);
                    command.Parameters.AddWithValue("@SSOID", entity.SSOID);
                    command.Parameters.AddWithValue("@Remarks", entity.Remarks);
                    command.Parameters.AddWithValue("@ActiveStatus", entity.ActiveStatus);
                    command.Parameters.AddWithValue("@DeleteStatus", entity.DeleteStatus);
                    command.Parameters.AddWithValue("@CreatedBy", entity.CreatedBy);
                    command.Parameters.AddWithValue("@ModifyBy", entity.ModifyBy);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    command.Parameters.AddWithValue("@UploadedResume", entity.UploadedResume);
                    command.Parameters.AddWithValue("@Dis_UploadedResume", entity.Dis_UploadedResume);
                    command.Parameters.AddWithValue("@InterestedStatus", entity.InterestedStatus);
    

                    command.Parameters.Add("@Return", SqlDbType.Int);// out
                    command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                    command.Parameters.Add("@Return_Str", SqlDbType.VarChar, 50);// out
                    command.Parameters["@Return_Str"].Direction = ParameterDirection.Output;// out

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
                    registrationNo = (Convert.ToString(command.Parameters["@Return_Str"].Value) ?? string.Empty);// out
                }
                return (result, registrationNo);
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

        public async Task<DataTable> GetStudentConsentCount(int StudentID)
        {
            _actionName = "GetStudentConsentCount(int StudentID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CampusPostMaster";
                        command.Parameters.AddWithValue("@Action", "_getStudentconsentCount");
                        command.Parameters.AddWithValue("@StudentID", StudentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
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








