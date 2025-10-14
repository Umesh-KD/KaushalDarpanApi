using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.Test;
using Newtonsoft.Json;
using System.Data;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class TeacherHigherEducationApplicationRepository : ITeacherHigherEducationApplicationRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public TeacherHigherEducationApplicationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "TeacherHigherEducationApplicationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        #region enrolled promoted to verify
        public async Task<DataTable> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)
        {
            _actionName = "GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEnrolledStudent_Promoted";

                        command.Parameters.AddWithValue("@action", "_getEnrolledStudent_Promoted");
                        command.Parameters.AddWithValue("@StudentName", model.StudentName);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@ApplicationNo", model.ApplicationNo);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);

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

        public async Task<int> SaveTeacherHighEduApp(TeacherHigherEducationApplicationModel model)
        {
            _actionName = "SaveTeacherHighEduApp(TeacherHigherEducationApplicationModel model)";
            return await Task.Run(async () =>
            {
                try
                {

                    //var jsonData = JsonConvert.SerializeObject(request);
                    //DataTable dataTable = new DataTable();
                    int result = 0;

                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {

                        command.CommandText = "USP_THTE_ApplicationIU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Set parameters as per stored procedure signature
                        command.Parameters.AddWithValue("@THTEAppID", model.THTEAppID); // Assuming 0 for new insert
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TeacherName", model.TeacherName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DOB", string.IsNullOrEmpty(model.DOB) ? (object)DBNull.Value : model.DOB);
                        command.Parameters.AddWithValue("@JoiningDate", string.IsNullOrEmpty(model.JoiningDate) ? (object)DBNull.Value : model.JoiningDate);
                        command.Parameters.AddWithValue("@InstituteID", DBNull.Value); // Set accordingly if you have this info in model
                        command.Parameters.AddWithValue("@AppliedCourse", model.AppliedCourse);
                        command.Parameters.AddWithValue("@AppliedInstitute", model.AppliedInstitute ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PHDStatus", model.PHDStatus);
                        command.Parameters.AddWithValue("@AppliedInstituteDistance", model.AppliedInstituteDistance);
                        command.Parameters.AddWithValue("@AppliedInstituteCourseCategory", model.AppliedInstituteCourseCategory);
                        command.Parameters.AddWithValue("@AppliedInstituteSubCategory", model.AppliedInstituteSubCategory);
                        command.Parameters.AddWithValue("@Remark", model.Remark ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ActiveStatus", 1);
                        command.Parameters.AddWithValue("@DeleteStatus", 0);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy); // Set if you have this info
                        command.Parameters.AddWithValue("@UpdatedBy", model.CreatedBy); // Set if you have this info
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress); // Set if you have this info

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

        #endregion


        public async Task<DataTable> GetCategoryOfApplyCourseInstitute(THTE_DDL body)
        {
            _actionName = "GetCategoryOfApplyCourseInstitute()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HigherStudyPermissionCategoryDDl";
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


        public async Task<DataTable> THTE_GetStaffPersonalDetailByUserID(BTER_EM_GetPersonalDetailByUserID body)
        {
            _actionName = "BTER_EM_GetPersonalDetailByUserID(BTER_EM_GetPersonalDetailByUserID body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_GetPersonalDetailByUserID";
                        command.Parameters.AddWithValue("@Action", "GetDetailStaffIDAC");
                        command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);                        
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

        public async Task<DataTable> GetTHTE_ApplicationData(THTE_ApplicationSearchModel model)
        {
            _actionName = "GetTHTE_ApplicationData(THTE_ApplicationSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_ApplicationList";
                        command.Parameters.AddWithValue("@THTEAppID", model.THTEAppID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
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


