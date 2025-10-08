using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Student;
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

        public async Task<int> SaveEnrolledStudentVerify_ReturnbyExamIncharge(List<EnrolledPromotedStudentSaveModel> model)
        {
            _actionName = "SaveEnrolledStudentVerify_ReturnbyExamIncharge(List<EnrolledPromotedStudentSaveModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_saveEnrolledStudent_ReturnbyExamIncharge";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_saveEnrolledStudent_ReturnbyExamIncharge");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
                    }
                    return retval;
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
    }
}


