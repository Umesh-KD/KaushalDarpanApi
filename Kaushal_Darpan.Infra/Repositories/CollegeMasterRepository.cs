using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CollegeMaster;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CollegeMasterRepository : ICollegeMasterRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CollegeMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CollegeMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetCollegeNodalDashboardData(CollageDashboardSearchModel model)
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
                        command.CommandText = "USP_CollegeNodalDashboard ";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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
        public async Task<DataTable> GetAllData(CollegeMasterSearchModel model)
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
                        if(model.RoleID==17 || model.RoleID == 18 || model.RoleID == 33 || model.RoleID == 80 || model.RoleID == 81)
                        {
                            command.CommandText = "USP_InstitutionMasterCourseWise ";
                        }
                        else
                        {
                            command.CommandText = "USP_InstitutionMaster ";
                        }
                       
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@InstituteCode", model.InstituteCode);
                        command.Parameters.AddWithValue("@InstituteName", model.InstituteName);
                        command.Parameters.AddWithValue("@ManagementType", model.ManagementType);
                        command.Parameters.AddWithValue("@DistrictId"      , model.DistrictId);
                        command.Parameters.AddWithValue("@Email"           ,  model.Email);
                        command.Parameters.AddWithValue("@SSOID"          , model.SSOID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseType", model.Eng_NonEng);

                        command.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                        command.Parameters.AddWithValue("@PageSize",   model.PageSize);

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
        public async Task<CollegeMasterModel> GetById(CollegeMasterRequestModel model)
        {
            _actionName = "GetById(CollegeMasterRequestModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCollegeMaster";

                        command.Parameters.AddWithValue("@action", "_getCollegeMaster");
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new CollegeMasterModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<CollegeMasterModel>(dataTable);
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
        public async Task<bool> SaveData(CollegeMasterModel request)
        {
            _actionName = "SaveData(CollegeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_InstituteMaster_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@InstituteCode", request.InstituteCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@InstitutionDGTCode", request.InstitutionDGTCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@InstituteNameEnglish", request.InstituteNameEnglish ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@InstituteNameHindi", request.InstituteNameHindi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CollegeTypeID", request.CollegeTypeID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", request.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@FaxNumber", request.FaxNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Website", request.Website ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LandNumber", request.LandNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LandlineSTD", request.LandlineStd);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@DivisionID", request.DivisionID);
                        command.Parameters.AddWithValue("@Address", request.Address ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PinCode", request.PinCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TehsilID", request.TehsilID);
                        command.Parameters.AddWithValue("@InstitutionManagementTypeID", request.InstitutionManagementTypeID);
                        command.Parameters.AddWithValue("@InstitutionCategoryID", request.InstitutionCategoryID);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@FinancialYearId", request.FinancialYearId);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@Capacity", request.Capacity);
                        command.Parameters.AddWithValue("@IsProfileComplete", request.IsProfileComplete);

                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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

        public async Task<DataTable> StatusChangeByID(int InstituteID, int ActiveStatus, int UserID)
        {
            _actionName = "GetTradeAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InstitutionMaster";
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);
                        command.Parameters.AddWithValue("@CreatedBy", UserID);
                        command.Parameters.AddWithValue("@ActiveStatus", ActiveStatus);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@Status", 3);
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

        public async Task<bool> DeleteDataByID(CollegeMasterModel request)
        {
            _actionName = "DeleteDataByID(CollegeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $" update M_InstituteMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where InstituteID={request.InstituteID}";

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



        public async Task<bool> UpdateActiveStatusByID(CollegeMasterModel request)
        {
            _actionName = "UpdateActiveStatusByID(CollegeMasterModel request)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandType = CommandType.Text;
                    var Query = @"
                UPDATE M_InstituteMaster
                SET 
                    ActiveStatus = CASE 
                        WHEN ActiveStatus = 1 THEN 0
                        WHEN ActiveStatus = 0 THEN 1
                    END,

                    ModifyBy = '" + request.ModifyBy + "',ModifyDate = GETDATE(),IPAddress = '" + _IPAddress + "' WHERE InstituteID ='" + request.InstituteID.ToString() + "' ";
                    command.CommandText = Query;

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
                var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errorDetails, ex);
            }
        }


        public async Task<DataTable> GetAllCollegeCourseWise(CollegeMasterSearchModel model)
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
                        command.CommandText = "USP_InstitutionMaster ";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@InstituteCode", model.InstituteCode);
                        command.Parameters.AddWithValue("@InstituteName", model.InstituteName);
                        command.Parameters.AddWithValue("@ManagementType", model.ManagementType);
                        command.Parameters.AddWithValue("@DistrictId", model.DistrictId);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", model.PageSize);

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

        public async Task<DataTable> GetInstituteProfileStatus(int InstitutId)
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
                        command.CommandText = "USP_GetInstituteProfileStatus_BTER";
                        command.Parameters.AddWithValue("@InstitutId", InstitutId);
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

        public async Task<DataTable> GetCollegeList(CollegeListSearchModel model)
        {
            _actionName = "GetCollegeList(CollegeMasterSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "USP_GetCollegeList_DTE ";

                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@InstitutionManagementTypeID", model.InstitutionManagementTypeID);
                        command.Parameters.AddWithValue("@IsProfileComplete", model.IsProfileComplete);

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








