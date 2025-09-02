using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.BterApplication;
using Kaushal_Darpan.Models.BterCertificateReport;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.CommonModel;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.FlyingSquad;
using Kaushal_Darpan.Models.GenerateAdmitCard;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.ItiStudentActivities;
using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.NodalApperentship;
using Kaushal_Darpan.Models.OptionalFormatReport;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Report;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.TheoryMarks;
using Kaushal_Darpan.Models.TimeTable;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Transactions;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;
using static Kaushal_Darpan.Models.ITIApplication.ItiApplicationPreviewDataModel;
using static QRCoder.PayloadGenerator;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ReportRepository : IReportRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private readonly string _IPAddress;

        public ReportRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ReportRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();

        }

        #region GetAllDataRpt
        public async Task<DataTable> GetAllDataRpt(TheorySearchModel body)
        {
            _actionName = "GetAllDataRpt(TheorySearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (body.InternalPracticalID == 1)
                        {
                            command.CommandText = "USP_GetInternalPracticalStudentRPT";
                        }
                        else if (body.InternalPracticalID == 2)
                        {
                            command.CommandText = "USP_GetInternalAssismentStudentRPT";
                        }
                        else
                        {
                            throw new Exception(Constants.MSG_INVALID_REQUEST);
                        }

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@MarkEnter", body.MarkEnter);
                        command.Parameters.AddWithValue("@InternalPracticalID", body.InternalPracticalID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);

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
        #endregion

        #region Admit Card
        public async Task<DataSet> GetStudentAdmitCard(GenerateAdmitCardSearchModel model)
        {
            _actionName = "GetStudentAdmitCard(GenerateAdmitCardModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentAdmitCardBulk";
                        command.Parameters.AddWithValue("@action", "_getStudentAdmitCardBulk");
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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
        #region GetTestRDLC
        public async Task<DataSet> GetTestRDLC(GenerateAdmitCardSearchModel model)
        {
            _actionName = "GetStudentAdmitCard(GenerateAdmitCardModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "RDLCTEST";
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetStudentAdmitCardBulk(int StudentExamID, int DepartmentID)
        {
            _actionName = "GetStudentAdmitCardBulk(GenerateAdmitCardModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentAdmitCardBulk";
                        command.Parameters.AddWithValue("@action", "_getStudentAdmitCardBulk");
                        command.Parameters.AddWithValue("@StudentExamID", StudentExamID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Fee Receipt
        public async Task<DataSet> GetStudentFeeReceipt(string EnrollmentNo)
        {
            _actionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentFee";
                        command.Parameters.AddWithValue("@action", "_getStudentEnrolledFee");
                        command.Parameters.AddWithValue("@EnrollmentNo", EnrollmentNo);
                        command.Parameters.AddWithValue("@TransactionId", EnrollmentNo);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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
        #region Student Fee Receipt
        public async Task<DataSet> GetStudentApplicationFeeReceipt(string EnrollmentNo)
        {
            _actionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentFee";
                        command.Parameters.AddWithValue("@action", "_getStudentFeesReceipt");
                        //command.Parameters.AddWithValue("@EnrollmentNo", EnrollmentNo);
                        command.Parameters.AddWithValue("@TransactionId", EnrollmentNo);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Fee Receipt
        public async Task<DataSet> GetStudentAllotmentFeeReceipt(string EnrollmentNo)
        {
            _actionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentFee";
                        command.Parameters.AddWithValue("@action", "_getStudentAllotmentFeesReceipt");
                        //command.Parameters.AddWithValue("@EnrollmentNo", EnrollmentNo);
                        command.Parameters.AddWithValue("@TransactionId", EnrollmentNo);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Passout Student Report
        public async Task<DataSet> GetPassoutStudentReport(PassoutStudentReport model)
        {
            _actionName = "GetPassoutStudentReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPassoutStudentList";
                        //command.Parameters.AddWithValue("@action", "_getStudentAllotmentFeesReceipt");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndtermID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Challan Receipt
        public async Task<DataSet> GetStudentApplicationChallanReceipt(int ApplicationID)
        {
            _actionName = "GetStudentApplicationChallanReceipt(int ApplicationID, int ChallanNo)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentFeeChallanReceipt";
                        //command.Parameters.AddWithValue("@ChallanNo", ChallanNo);
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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
        #region Student Allotment Receipt
        public async Task<DataSet> GetStudentAllotmentReceipt(int ApplicationID)
        {
            _actionName = "GetStudentAllotmentReceipt(int ApplicationID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_AllotmentLetter";
                        //command.Parameters.AddWithValue("@ChallanNo", AllotmentId);
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Reporting Certificate
        public async Task<DataSet> GetStudentReportingCertificate(int ApplicationID)
        {
            _actionName = "GetStudentAllotmentReceipt(int ApplicationID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_AllotmentLetter";
                        //command.Parameters.AddWithValue("@ChallanNo", AllotmentId);
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region College Nodal Wise Reports       

        public async Task<DataTable> GetCollegeNodalReportsData(DTEApplicationDashboardModel filterModel)
        {
            _actionName = "GetCollegeNodalReportsData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeNodalDashboardReports";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@Menu", filterModel.Menu);
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
        #endregion

        #region Student Enrollment Reports 
        public async Task<DataTable> GetStudentEnrollmentReports(DTEApplicationDashboardModel filterModel)
        {
            _actionName = "GetStudentEnrollmentReports()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_AdminDashboardReports";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@ApplicationID", filterModel.ApplicationID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@Menu", filterModel.Menu);
                        command.Parameters.AddWithValue("@Status", filterModel.Status);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
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
        #endregion

        #region Group Center Mapping Reports 
        public async Task<DataTable> GetGroupCenterMappingReports(GroupCenterMappingModel filterModel)
        {
            _actionName = "GetStudentEnrollmentReports()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (filterModel.Type == "group-center-mapping-reports")
                        {
                            command.CommandText = "USP_GroupCenterMappingReports";
                            command.Parameters.AddWithValue("@CCCode", filterModel.CCCode);
                            command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                            command.Parameters.AddWithValue("@GroupCode", filterModel.GroupCode);
                            command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                            command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                            command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                            command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        }
                        else if (filterModel.Type == "group-center-examiner-reports")
                        {
                            command.CommandText = "USP_GroupCenterExaminerReports";
                            command.Parameters.AddWithValue("@CCCode", filterModel.CCCode);
                            command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                            command.Parameters.AddWithValue("@GroupCode", filterModel.GroupCode);
                            //command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                            //command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                            command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                            command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                            command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                            command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        }
                        else if (filterModel.Type == "sub-wise-examiner-work-distribution-report")
                        {
                            command.CommandText = "USP_SubExaminerWorkDistributionReport";
                            command.Parameters.AddWithValue("@CCCode", filterModel.CCCode);
                            command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                            command.Parameters.AddWithValue("@GroupCode", filterModel.GroupCode);
                            command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                            command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                            command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                            command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        }
                        else if (filterModel.Type == "answer-book-read-institute-subject-code")
                        {
                            command.CommandText = "USP_AnswerBookReadInstituteSubjectCodeReport";
                            command.Parameters.AddWithValue("@CCCode", filterModel.CCCode);
                            command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                            command.Parameters.AddWithValue("@GroupCode", filterModel.GroupCode);
                            command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                            command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                            command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                            command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        }
                        else if (filterModel.Type == "answer-book-read-subject-code")
                        {
                            command.CommandText = "USP_AnswerBookReadSubjectCodeReport";
                            command.Parameters.AddWithValue("@CCCode", filterModel.CCCode);
                            command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                            command.Parameters.AddWithValue("@GroupCode", filterModel.GroupCode);
                            command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                            command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                            command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                            command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        }


                        // Add parameters to the stored procedure from the model

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
        #endregion

        #region Center Daily Reports 
        public async Task<DataTable> GetCenterDailyReports(GroupCenterMappingModel filterModel)
        {
            _actionName = "GetCenterDailyReports()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCenterDailyReports";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
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

        public async Task<DataTable> GetCenterDailyReport(GroupCenterMappingModel filterModel)
        {
            _actionName = "GetCenterDailyReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (filterModel.Type == "5th-sem-back-paper-report")
                        {
                            command.CommandText = "USP_Report_5thSemBackPaper";
                        }
                        else
                        {
                            command.CommandText = "USP_GetCenterDailyReport";
                        }


                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@action", filterModel.Type);
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
        public async Task<DataTable> ExaminationsReportsMenuWise(ExaminationsReportsMenuWiseModel filterModel)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = filterModel.SP;

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@action", filterModel.Action);
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
        public async Task<DataTable> DownloadStudentEnrollmentDetails(DownloadStudentEnrollmentDetailsModel filterModel)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_DownloadStudentEnrollmentDetails";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@ApplicationNo", filterModel.ApplicationNo);
                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                        command.Parameters.AddWithValue("@Name", filterModel.Name);
                        command.Parameters.AddWithValue("@MobileNo", filterModel.MobileNo);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndtermID);
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
        public async Task<DataTable> DownloadStudentChangeEnrollmentDetails(DownloadStudentChangeEnrollmentDetailsModel filterModel)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_DownloadStudentChangeEnrollmentDetails";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@InstituteCode", filterModel.InstituteCode);
                        command.Parameters.AddWithValue("@OldEnrollmentNo", filterModel.OldEnrollmentNo);
                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                        command.Parameters.AddWithValue("@BranchCode", filterModel.BranchCode);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndtermID);
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

        public async Task<DataTable> DownloadOptionalFormatReport(OptionalFormatReportModel filterModel)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_DownloadOptionalFormatReport";
                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                        command.Parameters.AddWithValue("@InstituteCode", filterModel.InstituteCode);
                        command.Parameters.AddWithValue("@RollNo", filterModel.RollNo);
                        command.Parameters.AddWithValue("@PaperCode", filterModel.PaperCode);
                        command.Parameters.AddWithValue("@BranchCode", filterModel.BranchCode);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndtermID);
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

        public async Task<DataTable> DateWiseAttendanceReport(DateWiseAttendanceReport filterModel)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_CenterWiseExam_Attendance_Report";
                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", filterModel.StreamID);
                        command.Parameters.AddWithValue("@CenterID", filterModel.CenterID);
                        command.Parameters.AddWithValue("@ToExamDate", filterModel.ToExamDate);
                        command.Parameters.AddWithValue("@FromExamDate", filterModel.FromExamDate);
                        command.Parameters.AddWithValue("@ShiftID", filterModel.ShiftID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeId", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndtermID);
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

        #endregion

        #region Download Center Daily Reports PDF
        public async Task<DataTable> GetDownloadCenterDailyReports(GroupCenterMappingModel filterModel)
        {
            _actionName = "GetDownloadCenterDailyReports()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetDownloadCenterDailyReports";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@StreamID", filterModel.StreamID);
                        command.Parameters.AddWithValue("@CenterID", filterModel.CenterID);
                        command.Parameters.AddWithValue("@SubjectID", filterModel.SubjectID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
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
        #endregion

        #region Get Statics Report Provide By Examiner
        public async Task<DataTable> GetStaticsReportProvideByExaminer(GroupCenterMappingModel filterModel)
        {
            _actionName = "GetStaticsReportProvideByExaminer()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StaticsReportProvideByExaminer";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                        command.Parameters.AddWithValue("@GroupCode", filterModel.GroupCode);
                        command.Parameters.AddWithValue("@SubjectCode", filterModel.SubjectCode);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@Action", "StaticsReportProvideByExaminer");
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
        #endregion

        #region Get Online Marking Report Provide By Examiner
        public async Task<DataTable> GetOnlineReportProvideByExaminer(OnlineMarkingSearchModel filterModel)
        {
            _actionName = "GetOnlineReportProvideByExaminer()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_OnlineReportProvideByExaminer";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@GroupCode", filterModel.GroupCode);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@IsPresentTheory", filterModel.IsPresentTheory);
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
        #endregion


        #region Get Statics Report Provide By Examiner
        public async Task<DataTable> GetExaminerReportAndMarksTracking(GroupCenterMappingModel filterModel)
        {
            _actionName = "GetExaminerReportAndMarksTracking()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExaminerReportAndMarksTracking";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
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
        #endregion

        #region Get Statics Report Provide By Examiner
        public async Task<DataTable> GetExaminerReportAndMarksTrackingStudent(GroupCenterMappingModel filterModel)
        {
            _actionName = "GetExaminerReportAndMarksTracking()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExaminerReportAndMarksTrackingStudent";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@Marks", filterModel.Marks);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@IsPresent", filterModel.IsPresent);
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
        public async Task<DataTable> GetExaminerReportAndPresentTrackingStudent(GroupCenterMappingModel filterModel)
        {
            _actionName = "GetExaminerReportAndMarksTracking()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExaminerReportAndPresentAbsentTrackingStudent";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@IsPresent", filterModel.IsPresent);
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

        public async Task<DataSet> GetExaminerReportAndMarksDownload(GroupCenterMappingModel filterModel)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExaminerReportAndMarksDownload";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@Marks", filterModel.Marks);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@IsPresent", filterModel.IsPresent);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Enrollment Reports 
        public async Task<DataTable> GetPrincipleDashboardReport(DTEApplicationDashboardModel filterModel)
        {
            _actionName = "GetPrincipleDashboardReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_PrincipleDashboardReport";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@Menu", filterModel.Menu);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
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
        #endregion

        #region Colleges Wise Reports       

        public async Task<DataTable> GetCollegesWiseReports()
        {
            _actionName = "GetCollegesWiseReports()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCollegesWiseReports ";
                        command.Parameters.AddWithValue("@action", "_getCollegesWiseReports");
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
        #endregion


        #region 
        public async Task<DataSet> GetExaminationForm(ReportBaseModel model)
        {
            _actionName = "GetExaminationForm(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentExaminationForm";
                        command.Parameters.AddWithValue("@action", "_getStudentExaminationForm");
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region "Enrolled GetStudentEnrolledForm"
        public async Task<DataSet> GetStudentEnrolledForm(ReportBaseModel model)
        {
            _actionName = "GetStudentEnrolledForm(ReportBaseModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentEnrolledForm";
                        command.Parameters.AddWithValue("@action", "_getStudentEnrolledForm");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region "Donwload Roll List"
        public async Task<DataTable> GetStudentRollNoList(DownloadnRollNoModel model)
        {
            _actionName = "GetStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentRollList";
                        command.Parameters.AddWithValue("@action", "_getStudentRollList");
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StudentTypeID", model.StudentTypeID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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

        #region "SaveRollNumbePDFData"
        public async Task<int> SaveRollNumbePDFData(DownloadnRollNoModel request)
        {
            _actionName = "SaveRollNumbePDFData(DownloadnRollNoModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_RollNumberPDFData_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RollListID", request.RollListID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@PDFType", request.PDFType);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@TotalStudent", request.TotalStudent);
                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
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

        public async Task<int> ITISaveRollNumbePDFData(DownloadnRollNoModel request)
        {
            _actionName = "SaveRollNumbePDFData(DownloadnRollNoModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ItiRollNumberPDFData_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RollListID", request.RollListID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@PDFType", request.PDFType);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@TotalStudent", request.TotalStudent);
                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
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

        #region ITI Application Form Preview
        public async Task<DataSet> GetITIApplicationFormPreview(ItiApplicationSearchModel model)
        {
            _actionName = "GetApplicationFormPreview(PreviewApplicationModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIApplication_GetPreview_ByID";
                        command.Parameters.AddWithValue("@ApplicationId", model.ApplicationID);
                        command.Parameters.AddWithValue("@DepartmentId", model.DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Application Form Preview
        public async Task<DataSet> GetApplicationFormPreview(BterSearchModel model)
        {
            _actionName = "GetApplicationFormPreview(PreviewApplicationModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetbterpreviewData_ByID";
                        command.Parameters.AddWithValue("@ApplicationId", model.ApplicationId);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Staff Details Receipt
        public async Task<DataSet> GetExaminerDetails(int StaffID, int DepartmentID)
        {
            _actionName = "GetExaminerDetails(string StaffID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_ExaminerAppointLetter";
                        command.Parameters.AddWithValue("@StaffID", StaffID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Staff Details Receipt
        public async Task<DataSet> GetAbsentReport()
        {
            _actionName = "GetAbsentReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_ExaminerAppointLetter";
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Colleges Wise Reports       

        public async Task<DataTable> GetCollegesWiseExaminationReports()
        {
            _actionName = "GetCollegesWiseExaminationReports()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCollegesWiseReports ";
                        command.Parameters.AddWithValue("@action", "_getCollegesWiseExaminationReports");
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
        #endregion

        #region "Download Student Profile Details"
        public async Task<DataTable> DownloadStudentProfileDetails(ReportBaseModel model)
        {
            _actionName = "DownloadStudentProfileDetails(ReportBaseModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentProfileDetails";
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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
        #endregion "Download Student Profile Details"

        #region "Download Time Table"
        public async Task<DataSet> DownloadTimeTable(ReportBaseModel model)
        {
            _actionName = "DownloadTimeTable(ReportBaseModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetTimeTableList";
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Action", model.Action);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", model.ExamType);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync();
                    }
                    return dt;
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



        public async Task<DataSet> ItiDownloadTimeTable(ReportBaseModel model)
        {
            _actionName = "ItiDownloadTimeTable(ReportBaseModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_ITI_GetTimeTableList";
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Action", model.Action);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", model.ExamType);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync();
                    }
                    return dt;
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




        public async Task<DataTable> DownloadTimeTable_Header(ReportBaseModel model)
        {
            _actionName = "DownloadTimeTable(ReportBaseModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetTimeTableList";
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Action", "_GetTimeTableHeader");
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", model.ExamType);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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

        #region Student Fee Receipt
        public async Task<DataSet> GetITIStudentFeeReceipt(string EnrollmentNo)
        {
            _actionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetITIFeeReceipt";
                        command.Parameters.AddWithValue("@action", "_getStudentEnrolledFee");
                        command.Parameters.AddWithValue("@EnrollmentNo", EnrollmentNo);
                        command.Parameters.AddWithValue("@TransactionId", EnrollmentNo);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region ITI Application Form 
        public async Task<DataSet> GetITIApplicationForm(ItiApplicationSearchModel model)
        {
            _actionName = "GetApplicationFormPreview(PreviewApplicationModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_ITIApplication_GetPreview_ByID";
                        command.Parameters.AddWithValue("@ApplicationId", model.ApplicationID);
                        command.Parameters.AddWithValue("@DepartmentId", model.DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region ITI Admit Card
        public async Task<DataSet> GetITIStudentAdmitCard(GenerateAdmitCardModel model)
        {
            _actionName = "GetITIStudentAdmitCard(GenerateAdmitCardModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetITIStudentAdmitCard";
                        command.Parameters.AddWithValue("@action", "_getITIStudentAdmitCard");
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetITIStudentAdmitCardBulk(int StudentExamID, int DepartmentID)
        {
            _actionName = "GetStudentAdmitCardBulk(GenerateAdmitCardModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetITIStudentAdmitCardBulk";
                        command.Parameters.AddWithValue("@action", "_getITIStudentAdmitCardBulk");
                        command.Parameters.AddWithValue("@StudentExamID", StudentExamID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region "Download Enrollment List"
        public async Task<DataTable> GetEnrollmentList(ReportBaseModel model)
        {
            _actionName = "DownloadTimeTable(ReportBaseModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetEnrollmentList";
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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

        #region "Donwload Roll List"
        public async Task<DataTable> GetITIStudentRollNoList(DownloadnRollNoModel model)
        {
            _actionName = "GetITIStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetITIStudentRollList";
                        command.Parameters.AddWithValue("@action", "_getStudentRollList");
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StudentTypeID", model.StudentTypeID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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


        public async Task<DataTable> GetITIStudentRollNoList_collegewise(DownloadnRollNoModel model)
        {
            _actionName = "GetITIStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetITIStudentRollList_CollegeWise";
                        command.Parameters.AddWithValue("@action", "_getStudentRollList");
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StudentTypeID", model.StudentTypeID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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

        #region "Student Customize Report List Columns"
        public async Task<DataTable> GetStudentCustomizetReportsColumns()
        {


            _actionName = "GetStudentCustomizetReportsColumns()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CustomizeStudentMaster ";
                        command.Parameters.AddWithValue("@Action", "GetOnlyColumns");
                        command.Parameters.AddWithValue("@InstituteID", 0);
                        command.Parameters.AddWithValue("@StateID", 0);
                        command.Parameters.AddWithValue("@StudentTypeID", 0);
                        command.Parameters.AddWithValue("@SemesterID", 0);
                        command.Parameters.AddWithValue("@DistrictID", 0);
                        command.Parameters.AddWithValue("@BlockID", 0);
                        command.Parameters.AddWithValue("@CourseTypeID", 0);
                        command.Parameters.AddWithValue("@EndTermID", 0);
                        command.Parameters.AddWithValue("@StreamID", 0);
                        command.Parameters.AddWithValue("@GenderID", "");
                        command.Parameters.AddWithValue("@CasteCategory", "");
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
        #endregion

        #region "Student Customize Report List"
        public async Task<DataTable> GetStudentCustomizetReports(ReportCustomizeBaseModel model)
        {


            _actionName = "GetStudentCustomizeList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CustomizeStudentMaster ";
                        command.Parameters.AddWithValue("@Action", "GetDataFillterAndWithOutFillter");
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@StateID", model.StateID);
                        command.Parameters.AddWithValue("@StudentTypeID", model.StudentTypeID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@BlockID", model.BlockID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@GenderID", model.GenderID);
                        command.Parameters.AddWithValue("@CasteCategory", model.CategaryCast);
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
        #endregion

        #region "Student Customize Report Use DDL"
        public async Task<List<CommonDDLModel>> GetGender()
        {
            _actionName = "GetGender()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentCustomiseReportDDL";
                        command.Parameters.AddWithValue("@Action", "DDLGender");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        studentMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return studentMaster;
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
        public async Task<List<CommonDDLModel>> GetBlock()
        {
            _actionName = "GetBlock()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentCustomiseReportDDL";
                        command.Parameters.AddWithValue("@Action", "DDLBlock");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        studentMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return studentMaster;
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
        public async Task<List<CommonDDLModel>> GetCourseType(int DepartmentID = 0)
        {
            _actionName = "GetCourseType()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentCustomiseReportDDL";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@Action", "DDLCourseType");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        studentMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return studentMaster;
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
        public async Task<List<CommonDDLModel>> GetInstitute()
        {
            _actionName = "GetInstitute()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentCustomiseReportDDL";
                        command.Parameters.AddWithValue("@Action", "DDLInstitute");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        studentMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return studentMaster;
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
        public async Task<List<CommonDDLModel>> GetEndTerm()
        {
            _actionName = "GetEndTerm()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentCustomiseReportDDL";
                        command.Parameters.AddWithValue("@Action", "DDLEndTerm");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        studentMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return studentMaster;
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

        #region Allotment Receipt
        public async Task<DataSet> GetAllotmentReceipt(string AllotmentId)
        {
            _actionName = "GetAllotmentReceipt(string AllotmentId)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetAllotmentReceipt";
                        command.Parameters.AddWithValue("@action", "_getAllotmentReceipt");
                        command.Parameters.AddWithValue("@AllotmentId", AllotmentId);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Iti Student Reports       

        public async Task<DataTable> GetItiStudentEnrollmentReports(DTEApplicationDashboardModel filterModel)
        {
            _actionName = "GetItiStudentEnrollmentReports()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiAdminDashboardReports";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@ApplicationID", filterModel.ApplicationID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@Menu", "Enroll");
                        command.Parameters.AddWithValue("@action", _actionName);
                        command.Parameters.AddWithValue("@Status", filterModel.Status);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);
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

        public async Task<DataTable> GetIitStudentExamReports(DTEApplicationDashboardModel filterModel)
        {
            _actionName = "GetIitStudentExamReports";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiAdminDashboardReports";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@ApplicationID", filterModel.ApplicationID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@Menu", "Exam");
                        command.Parameters.AddWithValue("@action", _actionName);
                        command.Parameters.AddWithValue("@Status", filterModel.Status);
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

        #endregion

        #region Student Marksheet
        public async Task<DataSet> GetStudentMarksheet(MarksheetDownloadSearchModel model)
        {
            _actionName = "GetStudentMarksheet(MarksheetDownloadSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentMarksheet";
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEngID);
                        command.Parameters.AddWithValue("@IsRevised", model.IsRevised);
                        command.Parameters.AddWithValue("@ResultTypeID", model.ResultTypeID);
                        command.Parameters.AddWithValue("@IsReval", model.IsReval);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Hostel Form
        public async Task<DataSet> GetStudentHostelallotment(MarksheetDownloadSearchModel model)
        {
            _actionName = "GetStudentHostelallotment(MarksheetDownloadSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentHostelallotment";
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEngID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region "Donwload Appeared Passed"
        public async Task<DataTable> DownloadAppearedPassed(DownloadAppearedPassed model)
        {
            _actionName = "GetStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ResultRpt_AppearedPassedStatistics";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@ResultType", model.ResultType);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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

        #region "Donwload Appeared Passed Institute Wise"
        public async Task<DataTable> DownloadAppearedPassedInstitutewise(DownloadAppearedPassed model)
        {
            _actionName = "GetStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ResultRpt_AppearedPassedStatistics_InstituteWise";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@ResultType", model.ResultType);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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

        #region "Donwload Branch Wise Statistics"
        public async Task<DataTable> DownloadBranchWiseStatistics(DownloadAppearedPassed model)
        {
            _actionName = "GetStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ResultRpt_AppearedPassedStatistics";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@ResultType", model.ResultType);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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

        #region "Donwload Institute Branch Wise Statistics"
        public async Task<DataTable> DownloadInstituteBranchWiseStatisticsReport(DownloadAppearedPassed model)
        {
            _actionName = "GetStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ResultRpt_AppearedPassedStatistics";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@ResultType", model.ResultType);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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

        #region "Download Blank Report"
        public async Task<DataTable> GetBlankReport(BlankReportModel model)
        {
            _actionName = "GetBlankReport(ReportBaseModel model)";
            return await Task.Run(async () =>
            {
                try
                {

                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandTimeout = 0;

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetBlankReport";
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@SubjectCode", model.SubjectCode);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@BranchID", model.BranchID);
                        command.Parameters.AddWithValue("@ExamDate", model.ExamDate);
                        command.Parameters.AddWithValue("@ShiftID", model.ShiftID);
                        command.Parameters.AddWithValue("@ExamCategoryID", model.ExamCategoryID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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


        #region "Paper-Count-Customize-Report-Columns-&-List"
        public async Task<DataTable> PaperCountCustomizeReportColumnsAndList(ReportCustomizeBaseModel model)
        {
            string GetAction = "";

            // if (model.ReportFlagID == 1)
            // {
            //     GetAction = "_Student_Count_College_Branch_Wise";
            // }
            //else if (model.ReportFlagID == 2)
            // {
            //     GetAction = "_Subject_Wise_Student_Count";
            // }
            // else if(model.ReportFlagID == 3)
            // {
            //     GetAction = "_Institue_Branch_Subject_Student_Count_Sem_Wise";
            // }
            // else
            // {
            //     GetAction = "";
            // }

            GetAction = "_Institue_Branch_Subject_Student_Count_Sem_Wise";

            _actionName = "PaperCountCustomizeReportColumnsAndList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (model.Type >= 7)
                        {
                            command.CommandText = "USP_Get_SemesterSubjectWiseStudentCount ";
                            //command.Parameters.AddWithValue("@Action", GetAction);
                            command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                            command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                            command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                            command.Parameters.AddWithValue("@Type", model.Type);
                        }
                        else
                        {
                            command.CommandText = "USP_ReportsBuilder ";
                            //command.Parameters.AddWithValue("@Action", GetAction);
                            command.Parameters.AddWithValue("@StreamID", model.StreamID);
                            command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                            command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                            command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                            command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                            command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                            command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                            command.Parameters.AddWithValue("@CasteCategoryID", model.CasteCategoryID);
                            command.Parameters.AddWithValue("@Type", model.Type);
                            command.Parameters.AddWithValue("@Action", model.action);
                        }
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

        #endregion


        #region "Paper-Count-Customize-Report-List"
        public async Task<DataTable> PaperCountCustomizeReportList(ReportCustomizeBaseModel model)
        {


            _actionName = "PaperCountCustomizeReportList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ReportsBuilder ";
                        command.Parameters.AddWithValue("@Action", "_Student_Count_College_Branch_Wise");
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@CasteCategoryID", model.CasteCategoryID);
                        command.Parameters.AddWithValue("@Type", model.Type);
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

        #endregion


        #region "Center-Wise-Subject-Count-Report-ColumnsAndList"
        public async Task<DataTable> GetCenterWiseSubjectCountReportColumnsAndList(ReportCustomizeBaseModel model)
        {


            _actionName = "GetCenterWiseSubjectCountReportColumnsAndList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetCenterWiseSubjectCount ";
                        command.Parameters.AddWithValue("@action", "_getCenterWiseSubjectCount");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.ReportFlagID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StudentExamType", model.Type);
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
        #endregion

        #region 

        public async Task<DataSet> GetITIExaminationForm(ReportBaseModel model)

        {

            _actionName = "GetExaminationForm(string EnrollmentNo)";

            return await Task.Run(async () =>

            {

                try

                {

                    var ds = new DataSet();

                    using (var command = _dbContext.CreateCommand())

                    {

                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "USP_Rpt_GetITIStudentExaminationForm";

                        command.Parameters.AddWithValue("@action", "_getStudentExaminationForm");

                        command.Parameters.AddWithValue("@StudentID", model.StudentID);

                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);

                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);

                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();

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


        #region "Optional-Format-Report"
        public async Task<DataTable> GetOptionalFormatReportData(OptionalFromatReportSearchModel model)
        {
            _actionName = "GetCenterWiseSubjectCountReportColumnsAndList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetOptionalFormatReportData";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
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

        #endregion

        #region "Non-Elective-Form-Filling-Report"
        public async Task<DataTable> GetNonElectiveFormFillingReportData(NonElectiveFormFillingReportSearchModel model)
        {
            _actionName = "GetNonElectiveFormFillingReportData(NonElectiveFormFillingReportSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetNonElectiveFormFillingReportData";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
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

        #endregion

        #region BTER

        public async Task<DataSet> GetFlyingSquadDutyOrder(GetFlyingSquadDutyOrder model)
        {
            _actionName = "GetFlyingSquadDutyOrder()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetFlyingSquadOrder";
                        command.Parameters.AddWithValue("@TeamID", model.TeamID);
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_FlyingSquadDeployment_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@OperationType", "UPDATE");
                        command.Parameters.AddWithValue("@TeamID", model.TeamID);
                        command.Parameters.AddWithValue("@ID", model.ID);
                        if (model.Status != 5)
                        {
                            command.Parameters.AddWithValue("@Status", 4);
                        }
                        if (model.Status == 5)
                        {
                            command.Parameters.AddWithValue("@Status", 5);
                        }

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute
                        await command.ExecuteNonQueryAsync();
                        var finalResult = Convert.ToInt32(command.Parameters["@Return"].Value);
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


        public async Task<DataSet> GetFlyingSquadReports(GetFlyingSquadDutyOrder model)
        {
            _actionName = "USP_GetCheckList_AnswerDetails()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCheckList_AnswerDetails";
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@TypeID", 2);
                        command.Parameters.AddWithValue("@action", "_GetCheckList_AnswerFlying");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataTable> GetFlyingSquad(GetFlyingSquadModal model)
        {
            _actionName = "GetFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetFlyingSquad";
                        command.Parameters.AddWithValue("@TeamID", model.TeamID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@Status", model.Status);

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

        public async Task<DataTable> GetFlyingSquadReport(GetFlyingSquadModal model)
        {
            _actionName = "GetFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetFlyingSquadReport";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);

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

        public async Task<DataTable> GetFlyingSquadTeamReports(GetFlyingSquadModal model)
        {
            _actionName = "GetFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetFlyingSquadTeamReport";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);

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

        public async Task<DataTable> GetITIFlyingSquadTeamReports(GetFlyingSquadModal model)
        {
            _actionName = "GetITIFlyingSquadTeamReports()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIFlyingSquadTeamReport";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);

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


        #endregion

        #region ITI

        public async Task<DataSet> GetITIFlyingSquadDutyOrder(GetFlyingSquadDutyOrder model)
        {
            _actionName = "GetITIFlyingSquadDutyOrder()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIFlyingSquadOrder";
                        command.Parameters.AddWithValue("@TeamID", model.TeamID);
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITIFlyingSquadDeployment_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@OperationType", "UPDATE");
                        command.Parameters.AddWithValue("@TeamID", model.TeamID);
                        command.Parameters.AddWithValue("@ID", model.ID);
                        if (model.Status != 5)
                        {
                            command.Parameters.AddWithValue("@Status", 4);
                        }
                        if (model.Status == 5)
                        {
                            command.Parameters.AddWithValue("@Status", 5);
                        }

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute
                        await command.ExecuteNonQueryAsync();
                        var finalResult = Convert.ToInt32(command.Parameters["@Return"].Value);
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


        public async Task<DataSet> GetITIFlyingSquadReports(GetFlyingSquadDutyOrder model)
        {
            _actionName = "USP_GetCheckList_AnswerDetails()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCheckList_AnswerDetails";
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@TypeID", 2);
                        command.Parameters.AddWithValue("@action", "_GetCheckList_AnswerFlying");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataTable> GetITIFlyingSquad(GetFlyingSquadModal model)
        {
            _actionName = "GetITIFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIFlyingSquad";
                        command.Parameters.AddWithValue("@TeamID", model.TeamID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@Status", model.Status);

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

        public async Task<DataTable> GetITIFlyingSquadReport(GetFlyingSquadModal model)
        {
            _actionName = "GetITIFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIFlyingSquadReport";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);

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


        #endregion


        #region DispatchGroup Details Receipt
        public async Task<DataSet> GetDispatchGroupDetails(int ID, int EndTermID, int CourseTypeID)
        {
            _actionName = "GetDispatchGroupDetails(int ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchGroup";
                        command.Parameters.AddWithValue("@DepartmentID", 1);
                        command.Parameters.AddWithValue("@InstituteID", ID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);
                        command.Parameters.AddWithValue("@Action", "_getrptGroupList");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        #region DispatchGroup Details Certificate
        public async Task<DataSet> DownloadDispatchGroupCertificate(int ID, int StaffID, int DepartmentID)
        {
            _actionName = "DownloadDispatchGroupCertificate(int StaffID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_DispatchGroupCertificateReport";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@StaffID", StaffID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region AttendanceReport13B
        public async Task<DataTable> AttendanceReport13B(AttendanceReport13BDataModel model)
        {
            _actionName = "_AttendanceReport13B";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_AttendanceReport13B";
                        command.Parameters.AddWithValue("@action", _actionName);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@ExamDate", model.ExamDate);
                        command.Parameters.AddWithValue("@ShiftID", model.ShiftID);
                        command.Parameters.AddWithValue("@StudentExamType", model.StudentExamType);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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

        #region Report33
        public async Task<DataSet> Report33(AttendanceReport13BDataModel model)
        {
            _actionName = "_Rpt_33";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_33";
                        command.Parameters.AddWithValue("@action", _actionName);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@ExamDate", model.ExamDate);
                        command.Parameters.AddWithValue("@ShiftID", model.ShiftID);
                        command.Parameters.AddWithValue("@StudentExamType", model.StudentExamType);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SubjectCode", model.SubjectCode);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region DailyReport_BhandarForm1
        public async Task<DataSet> DailyReport_BhandarForm1(AttendanceReport13BDataModel model)
        {
            _actionName = "DailyReport_BhandarForm1";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DailyReport_BhandarForm1";

                        command.Parameters.AddWithValue("@action", _actionName);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@ExamDate", model.ExamDate);
                        command.Parameters.AddWithValue("@ShiftID", model.ShiftID);
                        command.Parameters.AddWithValue("@StudentExamType", 78);
                        command.Parameters.AddWithValue("@ExamCategoryID", model.ExamCategoryID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        #region Dispatch Principal Group Code Details Receipt
        public async Task<DataSet> GetDispatchPrincipalGroupCodeDetails(int ID, int DepartmentID)
        {
            _actionName = "GetDispatchPrincipalGroupCodeDetails(int ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_DispatchPrincipalGroupCodeReport";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Get Dispatch Superintendent Rpt Report
        public async Task<DataSet> GetDispatchSuperintendentRptReport(int ID, int DepartmentID)
        {
            _actionName = "GetDispatchPrincipalGroupCodeDetails(int ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchMasterViewByIdData";
                        command.Parameters.AddWithValue("@Action", "ViewByIdDispatchSuperintendentRptReport");
                        command.Parameters.AddWithValue("@DispatchID", ID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Get Dispatch Superintendent Rpt Report
        public async Task<DataSet> GetDispatchSuperintendentRptReport1(int ID, int DepartmentID)
        {
            _actionName = "GetDispatchPrincipalGroupCodeDetails(int ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchMasterViewByIdData";
                        command.Parameters.AddWithValue("@Action", "ViewByIdDispatchSuperintendentRptReport");
                        command.Parameters.AddWithValue("@DispatchID", ID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        #region "Get Center Wise Subject Count Report New"
        public async Task<DataTable> GetCenterWiseSubjectCountReportNew(ReportCustomizeBaseModel model)
        {


            _actionName = "GetCenterWiseSubjectCountReportNew()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetCenterWiseSubjectCount_New";
                        command.Parameters.AddWithValue("@action", "_getCenterWiseSubjectCount");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.ReportFlagID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StudentExamType", model.Type);
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
        #endregion

        public async Task<DataTable> GetRport33Data(Report33DataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_33_GetDataList";
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@ExamDate", body.ExamDate);
                        command.Parameters.AddWithValue("@ShiftID", body.ShiftID);
                        command.Parameters.AddWithValue("@ExamCategoryID", body.ExamCategoryID);
                        command.Parameters.AddWithValue("@StudentExamType", body.StudentExamType);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectCode", body.SubjectCode);
                        command.Parameters.AddWithValue("@RollNumber", body.RollNumber);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
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

        public async Task<DataTable> DailyReportBhandarForm(Report33DataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_daily-report-bhandar-form1";

                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@ExamDate", body.ExamDate);
                        command.Parameters.AddWithValue("@ShiftID", body.ShiftID);
                        command.Parameters.AddWithValue("@ExamCategoryID", body.ExamCategoryID);
                        command.Parameters.AddWithValue("@StudentExamType", body.StudentExamType);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectCode", body.SubjectCode);
                        command.Parameters.AddWithValue("@RollNumber", body.RollNumber);
                        command.Parameters.AddWithValue("@UserId", body.UserID);

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




        public async Task<DataSet> TheoryMarkListReport(ReportCustomizeBaseModel model)
        {
            _actionName = "GetAbsentReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rep_TheoryCenterList";
                        command.Parameters.AddWithValue("@action", "TheoryMarkAbsent");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region ITI Student Challan Receipt
        public async Task<DataSet> GetITIStudentApplicationChallanReceipt(int ApplicationID)
        {
            _actionName = "GetITIStudentApplicationChallanReceipt(int ApplicationID, int ChallanNo)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_RPT_Challan";
                        //command.Parameters.AddWithValue("@ChallanNo", ChallanNo);
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Fee Receipt
        public async Task<DataSet> GetITIStudentApplicationFeeReceipt(string EnrollmentNo)
        {
            _actionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetITIFeeReceipt";
                        command.Parameters.AddWithValue("@action", "_getStudentApplicationFee");
                        command.Parameters.AddWithValue("@EnrollmentNo", EnrollmentNo);
                        command.Parameters.AddWithValue("@TransactionId", EnrollmentNo);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Fee Receipt
        public async Task<DataSet> GetITICollegeProfile(int CollegeId)
        {
            _actionName = "GetITICollegeProfile(int CollegeId)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_ITICollegeProfile";
                        command.Parameters.AddWithValue("@CollegeId", CollegeId);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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



        public async Task<DataTable> ScaReportAdmin(StudentCenteredActivitesMasterSearchModel body)
        {
            _actionName = "GetAllData(StudentCenteredActivitesMasterSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ReportStudentCenteredActivityStudent";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@MarkEnter", body.MarkEnter);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@InstituteName", body.InstituteName);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
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


        #region Examiner Appoint Letter
        public async Task<DataSet> GetExaminerAppointLetter(int ExaminerID, int DepartmentID, int InstituteID, int EndTermID)
        {
            _actionName = "GetExaminerAppointLetter(string StaffID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetExaminerAppointLetter";
                        command.Parameters.AddWithValue("@ExaminerID", ExaminerID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Institute Master Report
        public async Task<DataSet> InstituteMasterReport()
        {
            _actionName = "InstituteMasterReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InstituteMasterReport";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Teacher Wise Report Pdf
        public async Task<DataSet> TeacherWiseReportPdf()
        {
            _actionName = "TeacherWiseReportPdf()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_Teacher_Wise_Report";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Subject Wise Report Pdf
        public async Task<DataSet> SubjectWiseReportPdf()
        {
            _actionName = "SubjectWiseReportPdf()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_Subject_Wise_Report";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region CenterSuperintendentStudentReport
        public async Task<DataTable> GetCenterSuperintendentStudentReport(DTEApplicationDashboardModel filterModel)
        {
            _actionName = "GetCenterSuperintendentStudentReport(DTEApplicationDashboardModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CenterSuperintendentStudentReport";

                        command.Parameters.AddWithValue("@RoleId", filterModel.RoleID);
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
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
        #endregion

        #region Statistics Information Report Pdf
        public async Task<DataSet> StatisticsInformationReportPdf(GroupCenterMappingModel body)
        {
            _actionName = "StatisticsInformationReportPdf()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StaticsReportProvideByExaminer";
                        command.Parameters.AddWithValue("@Action", body.Action);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@SubjectCode", body.SubjectCode);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@GroupCode", body.GroupCode);
                        command.Parameters.AddWithValue("@CenterCode", body.CenterCode);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Theory Marks Report Pdf
        public async Task<DataSet> TheorymarksReportPdf(TheorySearchModel body)
        {
            _actionName = "TheorymarksReportPdf()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_TheoryMasterListRdlcRpt";


                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);

                        command.Parameters.AddWithValue("@RollNo", body.RollNo);

                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);

                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@SubjectType", body.SubjectType);
                        //command.Parameters.AddWithValue("@IsConfirmed", body.IsConfirmed);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Theory Marks Report Pdf BTER
        public async Task<DataSet> TheorymarksReportPdf_BTER(TheorySearchModel body)
        {
            _actionName = "TheorymarksReportPdf()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_TheoryMasterList";

                        command.Parameters.AddWithValue("@action", "TheorymarksReportPdf");
                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@MarkEnter", body.MarkEnter);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@GroupCodeID", body.GroupCodeID);
                        command.Parameters.AddWithValue("@ExaminerCode", body.ExaminerCode);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> TheoryMarkListPDFReport(ReportCustomizeBaseModel model)
        {
            _actionName = "GetAbsentReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rep_TheoryCenterList";
                        command.Parameters.AddWithValue("@action", "TheoryMarkAbsentPDFReport");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Get ITI Search Report
        public async Task<DataTable> GetITISearchRepot(ITISearchDataModel filterModel)
        {
            _actionName = "GetITISearchRepot()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITICollegeReportDetails";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@Code", filterModel.Code);
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
        #endregion

        #region Report23
        public async Task<DataSet> Report23(AttendanceReport23DataModel model)
        {
            _actionName = "_Rpt_33";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Report_23";
                        command.Parameters.AddWithValue("@action", "Rpt_23");
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@ExamDate", model.ExamDate);
                        command.Parameters.AddWithValue("@ShiftID", model.ShiftID);
                        command.Parameters.AddWithValue("@StudentExamType", model.StudentExamType);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SubjectCode", model.SubjectCode);
                        command.Parameters.AddWithValue("@BranchID", model.StreamID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetExaminerReportAndPresentDownload(GroupCenterMappingModel filterModel)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExaminerReportAndPresentAbsentDownload";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@IsPresent", filterModel.IsPresent);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetExaminerReportOfPresentAndAbsentDownload(GroupCenterMappingModel filterModel)
        {
            _actionName = "GetExaminerReportOfPresentAndAbsentDownload(GroupCenterMappingModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExaminerReportAndMarksDownload";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@Marks", filterModel.Marks);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@IsPresent", filterModel.IsPresent);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Get ITI Dispatch Superintendent Rpt Report
        public async Task<DataSet> GetITIDispatchSuperintendentRptReport1(int ID, int DepartmentID)
        {
            _actionName = "GetITIDispatchSuperintendentRptReport1(int ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_DispatchMasterViewByIdData";
                        command.Parameters.AddWithValue("@Action", "ViewByIdDispatchSuperintendentRptReport");
                        command.Parameters.AddWithValue("@DispatchID", ID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region GetITI_Dispatch_ShowbundleByExaminerToAdminData
        public async Task<DataSet> GetITI_Dispatch_ShowbundleByExaminerToAdminData(ITI_DispatchAdmin_ByExaminer_RptSearchModel model)
        {
            _actionName = "GetITI_Dispatch_ShowbundleByExaminerToAdminData(int StaffID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_DispatchAdmin_ByExaminer_Rpt";
                        //command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@Action", "_getall");
                        command.Parameters.AddWithValue("@ExaminerID", model.ExaminerID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region College Payment Fee Receipt
        public async Task<DataSet> GetCollegePaymentFeeReceipt(string TransactionId)
        {
            _actionName = "GetCollegePaymentFeeReceipt(string TransactionId)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetCollegePaymentFee";

                        command.Parameters.AddWithValue("@action", "_getStudentFeesReceipt");
                        command.Parameters.AddWithValue("@TransactionId", TransactionId);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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



        public async Task<DataSet> ITIStateTradeCertificateReport(ITIStateTradeCertificateModel model)
        {
            _actionName = "ITIStateTradeCertificateReport(ITIStateTradeCertificateModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIStudentCertificateReports";
                        command.Parameters.AddWithValue("@Action", "STATE_TRADE_CERTIFICATE");
                        command.Parameters.AddWithValue("@RollNo", model.RollNo);
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetITIStudent_Marksheet(StudentMarksheetSearchModel model)
        {
            _actionName = " GetITIStudent_Marksheet(StudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Marksheet";
                        command.Parameters.AddWithValue("@Action", "DownloadITI_Marksheet");
                        command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
                        command.Parameters.AddWithValue("@RollNo", model.RollNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetITIStudent_MarksheetList(StudentMarksheetSearchModel model)
        {
            _actionName = " GetITIStudent_MarksheetList(StudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Marksheet";
                        command.Parameters.AddWithValue("@Action", "ITI_MarksheetList");
                        command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
                        command.Parameters.AddWithValue("@RollNo", model.RollNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        command.Parameters.AddWithValue("@DOB", model.DOB);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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



        public async Task<DataSet> GetITIStudent_PassList(StudentMarksheetSearchModel model)
        {
            _actionName = " GetITIStudent_MarksheetList(StudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Marksheet";
                        command.Parameters.AddWithValue("@Action", "PassReport");
                        command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
                        command.Parameters.AddWithValue("@PassFailID", model.PassFailID);
                        command.Parameters.AddWithValue("@RollNo", model.RollNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataTable> StateTradeCertificateDetails(ITIStateTradeCertificateModel body)
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
                        command.CommandText = "USP_ITIStudentCertificateReports";
                        command.Parameters.AddWithValue("@Action", "StudentDetails");
                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@EnrollmentNo", body.EnrollmentNo);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@ExamYearID", body.ExamYearID);
                        command.Parameters.AddWithValue("@TradeScheme", body.TradeScheme);
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

        public async Task<DataSet> ITIMarksheetConsolidated(ITIStateTradeCertificateModel model)
        {
            _actionName = "ITIMarksheetConsolidated(ITIStateTradeCertificateModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Print_Consolidated_Marksheet";
                        command.Parameters.AddWithValue("@Enrollment", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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




        #region "Examiner Report"

        public async Task<DataSet> GetPracticalExaminerMark(BlankReportModel model)
        {
            _actionName = "GetPracticalExaminerMark(string TransactionId)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (model.Eng_NonEng==1)
                        {
                            command.CommandText = "usp_Get_NcvtPracticalStudentPhotoReport";
                        }
                        else {
                            command.CommandText = "usp_Get_PracticalStudentPhotoReport";
                        }
              
                        command.Parameters.AddWithValue("@CenterID", model.CenterID);
                        command.Parameters.AddWithValue("@SubjectCode", model.SubjectCode);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> GetITIStudent_PassDataList(StudentMarksheetSearchModel model)
        {
            _actionName = " GetITIStudent_PassDataList(StudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Marksheet";
                        command.Parameters.AddWithValue("@Action", "PassReportListPage");
                        command.Parameters.AddWithValue("@PassFailID", model.PassFailID);
                        command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
                        command.Parameters.AddWithValue("@RollNo", model.RollNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        #region "Examiner Attendence Report"

        public async Task<DataSet> GetPracticalExaminerAttendence(BlankReportModel model)
        {
            _actionName = "GetPracticalExaminerAttendence(string TransactionId)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Get_PracticalStudentAttendenceReport";
                        command.Parameters.AddWithValue("@CenterID", model.CenterID);
                        command.Parameters.AddWithValue("@SubjectCode", model.SubjectCode);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region "Examiner Marks Report"

        public async Task<DataSet> GetPracticalExaminerMarksReport(BlankReportModel model)
        {
            _actionName = "GetPracticalExaminerMark(string TransactionId)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Get_PracticalStudentMarksReport";
                        command.Parameters.AddWithValue("@CenterID", model.CenterID);
                        command.Parameters.AddWithValue("@SubjectCode", model.SubjectCode);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        #region "Theory Attendence Report"

        public async Task<DataSet> DownloadTheoryStudentITI(ItiTheoryStudentMaster model)
        {
            _actionName = "GetPracticalExaminerMark(string TransactionId)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetTheoryStudentItiReport";

                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndtermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.EngNong);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SubjectName", model.SubjectName);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> ITITradeWiseResult(ITIStateTradeCertificateModel model)
        {
            _actionName = "ITITradeWiseResult(ITIStateTradeCertificateModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIStudentCertificateReports";
                        command.Parameters.AddWithValue("@Action", "TradeWiseResult");
                        command.Parameters.AddWithValue("@RollNo", "");
                        command.Parameters.AddWithValue("@EnrollmentNo", "");
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> GetITITradeWiseResultDataList(ITIStateTradeCertificateModel model)
        {
            _actionName = " GetITITradeWiseResultDataList(StudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIStudentCertificateReports";
                        command.Parameters.AddWithValue("@Action", "TradeWiseResult");
                        command.Parameters.AddWithValue("@RollNo", "");
                        command.Parameters.AddWithValue("@EnrollmentNo", "");
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetITIAddmissionStatisticsDataList(ITIAddmissionReportSearchModel model)
        {
            _actionName = " GetITIAddmissionStatisticsDataList(ITIAddmissionReportSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_ITI_AdmissionStatisticsReport";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@DivisionID", model.DivisionID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@TradeLevelId", model.TradeLevelID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> GetITISeatUtilizationStatusDataList(ITIAddmissionReportSearchModel model)
        {
            _actionName = " GetITISeatUtilizationStatusDataList(ITIAddmissionReportSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_SeatUtilizationStatus";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@DivisionID", model.DivisionID);
                        command.Parameters.AddWithValue("@id", model.TradeLevelID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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



        public async Task<DataSet> GetZoneDistrictSeatUtilization(ZoneDistrictSeatUtilizationRequestModel model)
        {
            _actionName = "GetZoneDistrictSeatUtilization(ZoneDistrictSeatUtilizationRequestModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetZoneDistrictSeatUtilization";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@DivisionID", model.DivisionID);
                        command.Parameters.AddWithValue("@id", model.id);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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



        public async Task<DataSet> GetZoneDistrictSeatUtilization_ByGender(ZoneDistrictSeatUtilizationByGenderRequestModel model)
        {
            _actionName = "GetZoneDistrictSeatUtilization_ByGender(ZoneDistrictSeatUtilizationRequestModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetZoneDistrictSeatUtilization_ByGender";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@DivisionID", model.DivisionID);
                        command.Parameters.AddWithValue("@Id", model.Id);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> GetFinalAdmissionGenderWise(FinalAdmissionGenderWiseRequestModel model)
        {
            _actionName = "GetFinalAdmissionGenderWise(FinalAdmissionGenderWiseRequestModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_FinalAdmissionGenderWise";

                        command.Parameters.AddWithValue("@TradeLevelId", model.TradeLevelId);
                        command.Parameters.AddWithValue("@AllotmentStatus", model.AllotmentStatus);
                        command.Parameters.AddWithValue("@EndTerms", model.EndTerms);
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> GetVacantSeatReport(VacantSeatReportRequestModel model)
        {
            _actionName = "GetVacantSeatReport(VacantSeatReportRequestModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetVacantSeatReport";

                      
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@DivisionID", model.DivisionID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@ITICode", model.ITICode ?? string.Empty);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetAllottedAndReportedCountByITI(AllottedReportedRequestModel model)
        {
            _actionName = "GetAllottedAndReportedCountsByITI(AllottedReportedRequestModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllottedAndReportedCountsByITI";

                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@DivisionID", model.DivisionID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@ITICode", model.ITICode ?? string.Empty);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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


        public async Task<DataSet> GetITIAdmissionsInWomenWingDataList(ITIAddmissionWomenReportSearchModel model)
        {
            _actionName = " GetITIAdmissionsInWomenWingDataList(ITIAddmissionWomenReportSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_ITI_AdmissionsInWomenWing";
                        command.Parameters.AddWithValue("@Action", "AdmissionsInWomenWing");
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelId", model.TradeLevelID);
                        command.Parameters.AddWithValue("@DivisionID", model.DivisionID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@ITICode", model.ITICode);
                        command.Parameters.AddWithValue("@CollegeID", model.ITICollegeID);
                        command.Parameters.AddWithValue("@TradeCode", model.TradeCode);
                        command.Parameters.AddWithValue("@TradeID", model.ITITradeID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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
        public async Task<DataSet> GetITITradeWiseAdmissionStatusDataList(ITIAddmissionWomenReportSearchModel model)
        {
            _actionName = " GetITITradeWiseAdmissionStatusDataList(ITIAddmissionWomenReportSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UspTradeWiseAdmissionStatusReport";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelId", model.TradeLevelID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@TradeID", model.ITITradeID);
                        command.Parameters.AddWithValue("@TradeCode", model.TradeCode);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();

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
        public async Task<DataSet> GetITIPlaningDetailsDataList(ITIAddmissionWomenReportSearchModel model)
        {
            _actionName = " GetITIPlaningDetailsDataList(ITIAddmissionWomenReportSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_ITI_PlanningDetailsReport";
                        command.Parameters.AddWithValue("@CollegeID", model.ITICollegeID);
                        command.Parameters.AddWithValue("@StatusID", model.StatusID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();

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


        public async Task<DataTable> CenterWiseTradeStudentCount(CenterStudentSearchModel body)
        {
            _actionName = "GetAllDataRpt(TheorySearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;




                        command.CommandText = "USP_ITI_GetCenterwiseTradeStudents";


                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);


                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);

                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);

                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@CenterID", body.CenterID);

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

        public async Task<DataSet> GetITICategoryWiseSeatUtilizationDataList(ITIAddmissionReportSearchModel model)
        {
            _actionName = " GetITICategoryWiseSeatUtilizationDataList(ITIAddmissionReportSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_Category_Wise_Seat_Utilization";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@DivisionID", model.DivisionID);
                        command.Parameters.AddWithValue("@TradeLevelId", model.TradeLevelID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Age Between 15 And 29
        public async Task<DataSet> GetStudentDataAgeBetween15And29(StudentDataAgeBetween15And29RequestModel model)
        {
            _actionName = "GetStudentDataAgeBetween15And29(AllottedReportedRequestModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentDataAgeBetween15And29";

                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetAllotmentReportCollege(AllotmentReportCollegeRequestModel model)
        {
            _actionName = "GetAllotmentReportCollege(AllotmentReportCollegeRequestModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentSeatAllotment";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", model.TradeId);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);
                        

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetAllotmentReportCollegeForAdmin(AllotmentReportCollegeForAdminRequestModel model)
        {
            _actionName = "GetAllotmentReportCollegeForAdmin(AllotmentReportCollegeForAdminRequestModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllotmentReportCollegeforAdmin";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", model.TradeId);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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

        public async Task<DataTable> GetBterCertificateReport(BterCertificateReportDataModel filterModel)
        {
            _actionName = "GetBterCertificateReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        // Add parameters to the stored procedure from the model
                        if (filterModel.Action == "provisional-certificate")
                        {
                            command.CommandText = "Usp_Bter_ProvisionalCertificate_Report";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                            command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                            command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                            command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                            command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID); 
                            command.Parameters.AddWithValue("@RWHEffectedEndTermID", filterModel.RWHEffectedEndTerm);
                        }
                        else if (filterModel.Action == "migration-certificate")
                        {
                            command.CommandText = "Usp_Bter_MigrationCertificate_Report";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                            command.Parameters.AddWithValue("@MigrationType", filterModel.MigrationType);
                            command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                            command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                            command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                            command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                            command.Parameters.AddWithValue("@RWHEffectedEndTermID", filterModel.RWHEffectedEndTerm);
                        }

                        else if (filterModel.Action == "diploma-report")
                        {
                            //command.CommandText = "Usp_Bter_Diploma_Report";
                            command.CommandText = "Usp_Bter_Diploma_Report1";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                            command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                            command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                            command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                            command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        }
                        else if (filterModel.Action == "diploma-date-for-digiLocker")
                        {
                            command.CommandText = "Usp_Bter_DiplomaDateForDigiLockerReport";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                        }
                        else if (filterModel.Action == "diploma-certificate")
                        {
                            command.CommandText = "Usp_Bter_Diploma_Report";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                            command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                            command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                            command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                            command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        }

                        else if (filterModel.Action == "certificate-letter")
                        {
                            command.CommandText = "Usp_Bter_CertificateLetter_Report";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                            command.Parameters.AddWithValue("@CertificateType", filterModel.CertificateType);
                            command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);
                        }

                        else if (filterModel.Action == "Appeared-Passed-Statistics")
                        {
                            command.CommandText = "USP_ResultRpt_AppearedPassedStatistics";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                            command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                            command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                            command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                            command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterType);
                        }
                        else if (filterModel.Action == "duplicate-marksheet")
                        {
                            command.CommandText = "USP_ResultRpt_AppearedPassedStatistics";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                            command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                            command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                            command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                            command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterType);
                        }
                        else if (filterModel.Action == "duplicate-provisional-certificate")
                        {
                            command.CommandText = "USP_ResultRpt_AppearedPassedStatistics";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                            command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                            command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                            command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                            command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterType);
                        }

                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                

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

        public async Task<DataSet> BterCertificateReportDownload(BterCertificateReportDataModel filterModel)
        {
            _actionName = "BterCertificateReportDownload(BterCertificateReportDataModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (filterModel.Action == "provisional-certificate-download")
                        {
                            command.CommandText = "Usp_Bter_ProvisionalCertificate_Report";
                            command.Parameters.AddWithValue("@Action", filterModel.Action);

                        }
                        else if (filterModel.Action == "migration-certificate-download")
                        {
                            command.CommandText = "Usp_Bter_MigrationCertificate_Report";
                            command.Parameters.AddWithValue("@MigrationType", filterModel.MigrationType);
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                        }

                        else if (filterModel.Action == "Cancel-Enrollment-migration-certificate-download")
                        {
                            command.CommandText = "Usp_Bter_MigrationCertificate_Report";
                            command.Parameters.AddWithValue("@MigrationType", filterModel.MigrationType);
                            command.Parameters.AddWithValue("@Action", filterModel.Action);
                        }
                        else if (filterModel.Action == "certificate-letter")
                        {
                            command.CommandText = "Usp_Bter_ProvisionalCertificate_Report";
                            command.Parameters.AddWithValue("@Action", "provisional-certificate-download");
                        }

                        // Add parameters to the stored procedure from the model
                        
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                        command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                        command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        //public async Task<DataSet> BterDiplomaReportDownload(BterCertificateReportDataModel filterModel)
        //{
        //    _actionName = "BterDiplomaReportDownload(BterCertificateReportDataModel filterModel)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            var ds = new DataSet();
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "Usp_Bter_DiplomaDateForDigiLockerReport";
        //                command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
        //                command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
        //                command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
        //                command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
        //                command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                ds = await command.FillAsync();
        //            }
        //            return ds;
        //        }
        //        catch (Exception ex)
        //        {
        //            var errorDesc = new ErrorDescription
        //            {
        //                Message = ex.Message,
        //                PageName = _pageName,
        //                ActionName = _actionName,
        //                SqlExecutableQuery = _sqlQuery
        //            };
        //            var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //            throw new Exception(errordetails, ex);
        //        }
        //    });
        //}


        public async Task<DataSet> BterDiplomaReportDownload(BterCertificateReportDataModel filterModel)
        {
            _actionName = "BterDiplomaReportDownload(BterCertificateReportDataModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_Bter_Diploma_Report1";
                        command.Parameters.AddWithValue("@Action", filterModel.Action);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                        command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                        command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> AppearedPassedStatisticsReportDownload(BterCertificateReportDataModel filterModel)
        {
            _actionName = "AppearedPassedStatisticsReportDownload(BterCertificateReportDataModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ResultRpt_AppearedPassedStatistics";
                        command.Parameters.AddWithValue("@Action", filterModel.Action);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                        command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                        command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> AppearedPassedInstituteWiseDownload(BterCertificateReportDataModel filterModel)
        {
            _actionName = "AppearedPassedInstituteWiseDownload(BterCertificateReportDataModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ResultRpt_AppearedPassedStatistics_InstituteWise";
                        command.Parameters.AddWithValue("@Action", filterModel.Action);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                        command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                        command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Direct Admission Report
        public async Task<DataSet> GetDirectAdmissionReport(DirectAdmissionReportRequestModel model)
        {
            _actionName = "GetDirectAdmissionReport(DirectAdmissionReportRequestModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DirectAdmissionReport";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", model.TradeId);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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

        #region IMC Allotment Report
        public async Task<DataSet> GetIMCAllotmentReport(IMCAllotmnentReportRequestModel model)
        {
            _actionName = "GetIMCAllotmentReport(IMCAllotmnentReportRequestModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_IMCAllotmentReport";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", model.TradeId);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);
                        command.Parameters.AddWithValue("@StatusID", model.StatusID);
                        command.Parameters.AddWithValue("@CollegeName", model.CollegeName);


                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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

        public async Task<DataTable> GetStudentjanaadharDetailReport(StudentItiSearchModel model)
        {
            _actionName = "GetStudentjanaadharDetailReport()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetStudentjanaadharDetailReport";

                        command.Parameters.AddWithValue("@studentname", model.StudentName);
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@Code", model.Code);
                        command.Parameters.AddWithValue("@CollegeID", model.CollegeID);
                        command.Parameters.AddWithValue("@TradeID", model.TradeID);
                        command.Parameters.AddWithValue("@Action", "StudentJanaadharDetails");

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

        public async Task<DataTable> GetInstitutejanaadharDetailReport()
        {
            _actionName = "GetStudentjanaadharDetailReport()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetStudentjanaadharDetailReport";
                        command.Parameters.AddWithValue("@JanaadharNo", "");
                        command.Parameters.AddWithValue("@studentname", "");
                        command.Parameters.AddWithValue("@Action", "InstituteJanaadharDetails");

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

        public async Task<DataTable> GetDropOutStudentListbyinstituteID(int InstituteID)
        {
            _actionName = "GetDropOutStudentListbyinstituteID()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetStudentjanaadharDetailReport";
                        command.Parameters.AddWithValue("@JanaadharNo", "");
                        command.Parameters.AddWithValue("@studentname", "");
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);
                        command.Parameters.AddWithValue("@Action", "GetDropOutStudentList");

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

        #region Internal Sliding For Admin Report

        public async Task<DataSet> GetInternalSlidingForAdminReport(InternalSlidingForAdminReport model)
        {
            _actionName = "GetInternalSlidingForAdminReport(InternalSlidingForAdminReport model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InternalSlidingForAdminReport";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", model.TradeId);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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

        #region Swapping For Admin Report
        public async Task<DataSet> GetSwappingForAdminReport(SwappingForAdminReport model)
        {
            _actionName = "GetSwappingForAdminReport(SwappingForAdminReport model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SwappingForAdminReport";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", model.TradeId);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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

        public async Task<DataTable> GetEstablishManagementStaffReport(BTER_EstablishManagementReportSearchModel model)
        {
            _actionName = "GetEstablishManagementStaffReport(BTER_EstablishManagementReportSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_EstablishManagementReport";
                        command.Parameters.AddWithValue("@Action", model.Action);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@StaffUserID", model.StaffUserID);
                        command.Parameters.AddWithValue("@StaffType", model.StaffType);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@Name", model.Name);
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
        public async Task<DataTable> GetBterStatisticsReport(BterStatisticsReportDataModel model)
        {
            _actionName = "GetBterStatisticsReport(BterStatisticsReportDataModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_Bter_Statistics_Report";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@StreamTypeId", model.Eng_NonEng);
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
        public async Task<DataTable> GetMassCoppingReport(BterStatisticsReportDataModel model)
        {
            _actionName = "GetMassCoppingReport(BterStatisticsReportDataModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_Bter_MassCopping_Report";
                        command.Parameters.AddWithValue("@FinancialYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@CourseType", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
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

        public async Task<DataSet> GetBterBridgeCourseReport(BterStatisticsReportDataModel filterModel)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_Bridge_Course_Report";
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.AcademicYearID);
                        command.Parameters.AddWithValue("@CourseType", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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
        public async Task<DataSet> GetBterBranchWiseStatisticalReport(BterStatisticsReportDataModel filterModel)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_Bter_BranchWiseStatisticalReport";
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.AcademicYearID);
                        command.Parameters.AddWithValue("@CourseType", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        #region "ResultStatisticsBridgeCourseReport"
        public async Task<DataTable> ResultStatisticsBridgeCourseReport(StatisticsBridgeCourseModel model)
        {
            _actionName = "GetStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ResultStatisticsBridgeCourseReport";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@ResultType", model.ResultType);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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
        public async Task<DataTable> DownloadResultStatisticsReport(StatisticsBridgeCourseModel model)
        {
            _actionName = "GetStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ResultStatisticsReport";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@ResultType", model.ResultType);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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
        public async Task<DataTable> DownloadResultStatisticsBridgeCourseStreamWiseReport(StatisticsBridgeCourseModel model)
        {
            _actionName = "GetStudentRollNoList(DownloadnRollNoModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ResultStatisticsBridgeCourseStreamWise";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@ResultType", model.ResultType);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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


        #region College Information Report
        public async Task<DataTable> GetCollegeInformationReport(CollegeInformationReportSearchModel model)
        {
            _actionName = "GetCollegeInformationReport(CollegeInformationReportSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_CollegeInformationReports";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);

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

        #endregion


        #region EWS Report

        public async Task<DataTable> GetEwsReport(EWSReportSearchModel model)
        {
            _actionName = "GetEwsReport(EWSReportSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEWSReport";

                        // Add your parameters here
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);

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

        #endregion


        #region UFM Student Report

        public async Task<DataTable> GetUFMStudentReport(UFMStudentReportSearchModel model)
        {
            _actionName = "GetUFMStudentReport(EWSReportSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetUFMStudentExamList";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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

        #endregion


        #region Sessional Fail Student Report

        public async Task<DataTable> GetSessionalFailStudentReport(GetSessionalFailStudentReport model)
        {
            _actionName = "GetSessionalFailStudentReport(GetSessionalFailStudentReport model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentSessionalTheoryMarksWithSubjectCode";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);


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

        #endregion


        public async Task<DataTable> GetInstituteStudentReport(InstituteStudentReport model)
        {
            _actionName = "GetInstituteStudentReport(InstituteStudentReport model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetInstituteAtmMarksList";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndtermID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
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

        #region RMI Fail Student Report

        public async Task<DataTable> GetRMIFailStudentReport(RMIFailStudentReport model)
        {
            _actionName = "GetSessionalFailStudentReport(RMIFailStudentReport model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentTheoryMarksWithSubjectCode";

                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);


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

        #endregion

        public async Task<DataSet> RelievingLetterReport(RelievingLetterSearchModel model)
        {
            _actionName = "RelievingLetterReport(RelievingLetterSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_EM_JoiningAndRelievingLetter";
                        command.Parameters.AddWithValue("@Action", "RelievingLetter");
                        command.Parameters.AddWithValue("@UserId", model.UserID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        ds = await command.FillAsync();
                    }
                    return ds;

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

        #region Apprenticeship  registratuion Fresher Report
        public async Task<DataSet> ApprenticeshipFresherReport(ApprenticeshipRegistrationSearchModal model)
        {
            _actionName = "ApprenticeshipFresherReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiFresherRegDetail";
                        command.Parameters.AddWithValue("@Action", "GetPassingReportData");
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CreateBy", model.Createdby);
                        command.Parameters.AddWithValue("@PKID", model.PKID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();

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

        #region Apprenticeship  registratuion Passout Report
        public async Task<DataSet> ApprenticeshipPassoutReport(ApprenticeshipRegistrationSearchModal model)
        {
            _actionName = "ApprenticeshipFresherReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiPassoutRegDetail";
                        command.Parameters.AddWithValue("@Action", "GetPassingReportData");
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CreateBy", model.Createdby);
                        command.Parameters.AddWithValue("@PKID", model.PKID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();

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

        #region Apprenticeship  registratuion List Report
        public async Task<DataSet> ApprenticeshipReport(ApprenticeshipRegistrationSearchModal model)
        {
            _actionName = "ApprenticeshipReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Save_Apprenticeship_Registration_Report";
                        command.Parameters.AddWithValue("@Action", "GetApprenticeshipReportData");
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CreateBy", model.Createdby);
                        command.Parameters.AddWithValue("@PKID", model.PKID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();

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

        #region Workshop Progress Report
        public async Task<DataSet> WorkshopProgressReport(WorkshopProgressRPTSearchModal model)
        {
            _actionName = "WorkshopProgressReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Save_WorkshopProgressReport";
                        command.Parameters.AddWithValue("@Action", "GetWorkshopProgressReportData");
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", model.Createdby);
                        command.Parameters.AddWithValue("@PKID", model.PKID);
                        command.Parameters.AddWithValue("@OrganisedDistrictID", model.SearchDistrictID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();

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


        #region PMNAM Mela Report
        public async Task<DataSet> PmnamMelaReport(ITIPMNAM_Report_SearchModal model)
        {
            _actionName = "PmnamMelaReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Save_PMNAM_MelaReport";
                        command.Parameters.AddWithValue("@Action", "GetReportData");
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", model.Createdby);
                        //command.Parameters.AddWithValue("@DistrictID", DistrictID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();

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

        public async Task<DataSet> PmnamMelaReportnodelOfficer(ITIPMNAM_Report_SearchModal model)
        {
            _actionName = "PmnamMelaReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Save_PMNAM_MelaReport";
                        command.Parameters.AddWithValue("@Action", "GetReportData");
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", model.Createdby);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();

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

        #region Mela Report
        public async Task<DataSet> MelaReport(ITIPMNAM_Report_SearchModal model)
        {
            _actionName = "MelaReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Save_PMNAM_MelaReportBeforeAfter";
                        command.Parameters.AddWithValue("@Action", "GetReportData");
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", model.Createdby);
                        command.Parameters.AddWithValue("@PKID", model.PKID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();

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

        #region Reval Dispatch Group Details Receipt
        public async Task<DataSet> GetRevalDispatchGroupDetails(int ID, int EndTermID, int CourseTypeID)
        {
            _actionName = "GetRevalDispatchGroupDetails(int ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_B_RevalDispatchGroup";
                        command.Parameters.AddWithValue("@DepartmentID", 1);
                        command.Parameters.AddWithValue("@InstituteID", ID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);
                        command.Parameters.AddWithValue("@Action", "_getrptGroupList");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        #region Reval Dispatch Group Details Certificate
        public async Task<DataSet> DownloadRevalDispatchGroupCertificate(int ID, int StaffID, int DepartmentID)
        {
            _actionName = "DownloadRevalDispatchGroupCertificate(int StaffID)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_B_RevalDispatchGroupCertificateReport";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@StaffID", StaffID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Theory Fail Student Report
        public async Task<DataTable> GetTheoryFailStudentReport(TheoryFailStudentReport model)
        {
            _actionName = "GetTheoryFailStudentReport(TheoryFailStudentReport model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_TheoryPaperFailStudentReport";

                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);


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
        #endregion

        #region ITI Allotment Report
        public async Task<DataSet> GetITIAllotmentReport(IMCAllotmnentReportRequestModel model)
        {
            _actionName = "GetITIAllotmentReport(IMCAllotmnentReportRequestModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIAllotmentReport";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", model.TradeId);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);
                        command.Parameters.AddWithValue("@StatusID", model.StatusID);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        ds = await command.FillAsync();
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

        #region  Student Revaluation Details Report BTER
        public async Task<DataTable> GetRevaluationStudentDetailsReport(RevaluationStudentDetailReport model)
        {
            _actionName = "GetRvaluationStudentDetailsReport(RevaluationStudentDetailReport model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentRevaluationDetailsReport";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);


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
        #endregion

        #region Center Superinstendent Attendance Report
        public async Task<DataTable> GetCenterSuperinstendentAttendanceReport(searchCenterSuperintendentAttendance model)
        {
            _actionName = "GetRvaluationStudentDetailsReport(RevaluationStudentDetailReport model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCenterSuperinstendentAttendanceReport";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@ExamDate", model.ExamDate);
                        command.Parameters.AddWithValue("@SemesterId", model.SemesterID);
                        command.Parameters.AddWithValue("@CenterAssignedID", model.CenterAssignedID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
           
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
        #endregion


        #region  Student Examiner Details Report BTER
        public async Task<DataTable> GetStudentExaminerDetailsReport(StudentExaminerDetailReport model)
        {
            _actionName = "GetStudentExaminerDetailsReport(StudentExaminerDetailReport model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentExaminerDetails";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);

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
        #endregion

        //iti-StudentSeatAllotment-report
        public async Task<DataSet> GetITIStudentSeatAllotmentDataList(ITIAddmissionWomenReportSearchModel model)
        {
            _actionName = "GetITIStudentSeatAllotmentDataList(ITIAddmissionWomenReportSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_StudentSeatAllotmentReport";
                        //command.Parameters.AddWithValue("@Action", "AdmissionsInWomenWing");
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelId", model.TradeLevelID);
                        //command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@ITICode", model.ITICode);
                        command.Parameters.AddWithValue("@TradeCode", model.TradeCode);
                        command.Parameters.AddWithValue("@ITITradeID", model.ITITradeID);
                        command.Parameters.AddWithValue("@ITICollegeID", model.ITICollegeID);
                        //command.Parameters.AddWithValue("@StatusID", model.StatusID);
                        //command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        //command.Parameters.AddWithValue("@ITICode", model.ITICode);
                        
                        //command.Parameters.AddWithValue("@TradeCode", model.TradeCode);
                     
                        //command.Parameters.AddWithValue("@AllotedCategory", model.AllotedCategory);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetITIStudentSeatWithdrawDataList(ITIAddmissionWomenReportSearchModel model)
        {
            _actionName = "GetITIStudentSeatWithdrawDataList(ITIAddmissionWomenReportSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_StudentSeatWithdrawReport";
                        //command.Parameters.AddWithValue("@Action", "AdmissionsInWomenWing");
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelId", model.TradeLevelID);
                        //command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@ITICode", model.ITICode);
                        command.Parameters.AddWithValue("@TradeCode", model.TradeCode);
                        command.Parameters.AddWithValue("@ITITradeID", model.ITITradeID);
                        command.Parameters.AddWithValue("@ITICollegeID", model.ITICollegeID);
                        //command.Parameters.AddWithValue("@StatusID", model.StatusID);
                        //command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        //command.Parameters.AddWithValue("@ITICode", model.ITICode);

                        //command.Parameters.AddWithValue("@TradeCode", model.TradeCode);

                        //command.Parameters.AddWithValue("@AllotedCategory", model.AllotedCategory);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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


        public async Task<DataSet> GetCentarlSupridententDistrictReportDataListReport(CentarlSupridententDistrictRequestModel model)
        {
            _actionName = "GetCentarlSupridententDistrictReportDataListReport(CentarlSupridententDistrictRequestModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiCenterSuperintendentDistrictAllocationReport";

                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@CenterCode", model.CenterCode);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataTable> GetITIEstablishManagementStaffReport(BTER_EstablishManagementReportSearchModel model)
        {
            _actionName = "GetITIEstablishManagementStaffReport(BTER_EstablishManagementReportSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_EstablishManagementReport";
                        command.Parameters.AddWithValue("@Action", model.Action);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@StaffUserID", model.StaffUserID);
                        command.Parameters.AddWithValue("@StaffType", model.StaffType);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@Name", model.Name);
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

        public async Task<DataTable> GetBterDuplicateCertificateReport(BterCertificateReportDataModel filterModel)
        {
            _actionName = "GetBterCertificateReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        // Add parameters to the stored procedure from the model
                        if (filterModel.Action == "duplicate-marksheet")
                        {
                            command.CommandText = "USP_StudentListForDuplicateMarksheet";
                            command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                            command.Parameters.AddWithValue("@ResultTypeID", filterModel.ResultType);
                            command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterType);

                            command.Parameters.AddWithValue("@IsBridge", filterModel.IsBridge);
                            command.Parameters.AddWithValue("@RollNo", filterModel.RollNo);
                        }
                        else if (filterModel.Action == "duplicate-provisional-certificate")
                        {
                            command.CommandText = "Usp_Bter_ProvisionalCertificate_DuplicateReport";
                            command.Parameters.AddWithValue("@Action", "duplicate-provisional-certificate");
                            command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                            command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                            command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                            command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterType);
                        }

                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);

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

        #region Student Duplicate Marksheet
        public async Task<DataSet> GetStudentDuplicateMarksheet(MarksheetDownloadSearchModel model)
        {
            _actionName = " GetStudentDuplicateMarksheet(MarksheetDownloadSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetStudentDuplicateMarksheet";
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEngID);
                        command.Parameters.AddWithValue("@IsRevised", model.IsRevised);
                        command.Parameters.AddWithValue("@ResultTypeID", model.ResultTypeID);
                        command.Parameters.AddWithValue("@IsReval", model.IsReval);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        #region Student Duplicate Provisional Certificate
        public async Task<DataSet> BterDuplicateProvisionalCertificateDownload(BterCertificateReportDataModel filterModel)
        {
            _actionName = "BterDuplicateProvisionalCertificateDownload(BterCertificateReportDataModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (filterModel.Action == "provisional-certificate-download")
                        {
                            command.CommandText = "Usp_Bter_ProvisionalCertificate_DuplicateReport";
                            command.Parameters.AddWithValue("@Action", "duplicate-provisional-certificate-download");
                        }
                        
                        // Add parameters to the stored procedure from the model

                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                        command.Parameters.AddWithValue("@RevisedType", filterModel.RevisedType);
                        command.Parameters.AddWithValue("@ResultType", filterModel.ResultType);
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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

        public async Task<DataSet> GetStudentWithdranSeat(AllotmentReportCollegeRequestModel model)
        {
            _actionName = "GetAllotmentReportCollege(AllotmentReportCollegeRequestModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentWithdranSeat";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", model.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", model.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", model.TradeId);
                        command.Parameters.AddWithValue("@CollegeId", model.CollegeId);
                        command.Parameters.AddWithValue("@AllotmentStatus", model.AllotmentStatus);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
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
    }
}