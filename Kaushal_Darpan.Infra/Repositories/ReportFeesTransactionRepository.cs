using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ReportFeesTransactionModel;
using Newtonsoft.Json;
using System.Data;
using static Kaushal_Darpan.Models.ReportFeesTransactionModel.ReportFeesTransaction;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ReportFeesTransactionRepository : IReportFeesTransactionRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        public ReportFeesTransactionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ReportRepository";
        }

        #region StudentFeesTransactionHistory
        public async Task<DataTable> GetStudentFeesTransactionHistoryRpt(ReportFeesTransactionSearchModel model)
        {
            _actionName = "GetStudentFeesTransactionHistoryRpt(ReportFeesTransactionModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentFeesTransactionHistory";
                        command.Parameters.AddWithValue("@action", "_GetStudentFeesTransactionHistory");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@TransactionType", model.TransactionType);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@TransactionId", model.TransactionId);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", model.PageSize);
                        command.Parameters.AddWithValue("@TransctionStatus", model.TransctionStatus);
                        command.Parameters.AddWithValue("@StudentName", model.StudentName);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@AadharNo", model.AadharNo);
                        command.Parameters.AddWithValue("@DOB", model.DOB);
                        command.Parameters.AddWithValue("@FeeFor", model.FeeFor);
                        command.Parameters.AddWithValue("@TransctionDate", model.TransctionDate);
                        command.Parameters.AddWithValue("@PRN", model.PRN);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@PaymentServiceID", model.PaymentServiceID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync_DataTable();
                    }
                    return ds;
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

        public async Task<DataTable> GetApplicationFeesTransaction(GetApplicationFeesTransactionSearchModel model)
        {
            _actionName = "GetApplicationFeesTransaction(GetApplicationFeesTransactionSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetApplicationFeesTransactionDetails";

                        command.Parameters.AddWithValue("@Action", "_GetApplicationFeesTransactionDetails");

                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@TransactionType", model.TransactionType);
                        command.Parameters.AddWithValue("@TransctionStatus", model.TransctionStatus);
                        command.Parameters.AddWithValue("@TransactionId", model.TransactionId);
                        command.Parameters.AddWithValue("@ApplicationID", model.ApplicationID);
                        command.Parameters.AddWithValue("@TransactionNo", model.TransactionNo);
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@StudentName", model.StudentName);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@AadharNo", model.AadharNo);
                        command.Parameters.AddWithValue("@DOB", model.DOB);
                        command.Parameters.AddWithValue("@FeeFor", model.FeeFor);
                        command.Parameters.AddWithValue("@TransctionDate", model.TransctionDate);
                        command.Parameters.AddWithValue("@PRN", model.PRN);
                        command.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", model.PageSize);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync_DataTable();
                    }
                    return ds;
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

        public async Task<DataTable> GetEmitraFeesTransactionHistory(EmitraFeesTransactionSearchModel model)
        {
            _actionName = "GetStudentFeesTransactionHistoryRpt(ReportFeesTransactionModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentEmitraFeesTransactionHistory";
                        command.Parameters.AddWithValue("@action", "_GetStudentFeesTransactionHistory");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@TransactionType", model.TransactionType);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@TransactionId", model.TransactionId);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", model.PageSize);
                        command.Parameters.AddWithValue("@TransctionStatus", model.TransctionStatus);
                        command.Parameters.AddWithValue("@StudentName", model.StudentName);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@AadharNo", model.AadharNo);
                        command.Parameters.AddWithValue("@DOB", model.DOB);
                        command.Parameters.AddWithValue("@FeeFor", model.FeeFor);
                        command.Parameters.AddWithValue("@TransctionDate", model.TransctionDate);
                        command.Parameters.AddWithValue("@PRN", model.PRN);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync_DataTable();
                    }
                    return ds;
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







