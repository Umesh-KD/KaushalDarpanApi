using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.RevaluationDataModel;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Kaushal_Darpan.Models.UserMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class RevaluationRepository : IRevaluationRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public RevaluationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "RevaluationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetDetails(RevaluationDataModel body)
        {
            _actionName = "GetTeacherForExaminer()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentDetailsByRollNo";

                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@DOB", body.DOB);
                        command.Parameters.AddWithValue("@EnrollmentNo", body.EnrollmentNo);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);

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

        public async Task<DataTable> GetAllRevalation(StudentDetailsByRollNoModel body)
        {
            _actionName = "GetTeacherForExaminer()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPaymentRevaluationDetails";

                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@StudentExamID", body.StudentExamID);
                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@CourseType", body.CourseTypeID);
                        command.Parameters.AddWithValue("@IsKiosk", body.IsKiosk);

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


        public async Task<DataTable> GetAllRevalationReportList(RevalationReportsearchModel body)
        {
            _actionName = "getAllNodalUserdata()";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_Reval_ReportDetails";

                    command.Parameters.AddWithValue("@EnrollmentNo", body.EnrollmentNo);
                    command.Parameters.AddWithValue("@ResultDate", body.ResultDate);
                    command.Parameters.AddWithValue("@RollNo", body.RollNumber);
                    command.Parameters.AddWithValue("@SubjectCode", body.SubjectCode);
                    command.Parameters.AddWithValue("@RevaluationTxnNo", body.RevaluationTxnNo);
                    command.Parameters.AddWithValue("@RevaluationChallan", body.RevaluationChallan);
                    command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);

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

    }
}
