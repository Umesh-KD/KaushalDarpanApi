using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.GuestRoomManagementModel;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.PaperSetter;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class MarksheetDownloadRepository : IMarksheetDownloadRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public MarksheetDownloadRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "MarksheetDownloadRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetStudents(MarksheetDownloadSearchModel body)
        {
            _actionName = "GetStudents(MarksheetDownloadSearchModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_StudentListForMarksheet";
                    //command.CommandText = "USP_StudentListForMarksheet_test";
                    command.CommandTimeout = 0;

                    if (body.ResultTypeID == (int)EnumResultType.MainResult) // main and reval
                    {
                        command.Parameters.AddWithValue("@action", "_getStuListForMarksheet_main");
                    }
                    else if(body.ResultTypeID == (int)EnumResultType.RevaluationResult)
                    {
                        command.Parameters.AddWithValue("@action", "_getStuListForMarksheet_reval");
                    }
                    else if(body.ResultTypeID == (int)EnumResultType.RwhResult)
                    {
                        command.Parameters.AddWithValue("@action", "_getStuListForMarksheet_RWH");
                    }
                    else if (body.ResultTypeID == (int)EnumResultType.RwhRevalEffected)
                    {
                        command.Parameters.AddWithValue("@action", "_getRWHStuListForMarksheet_RWH_reval");
                    }
                    else if (body.ResultTypeID == (int)EnumResultType.Ufm)
                    {
                        throw new Exception("Invalid request!");
                    }
                    else
                    {
                        throw new Exception("Invalid request!");
                    }

                    command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                    command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                    command.Parameters.AddWithValue("@IsBridge", body.IsBridge);
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                    command.Parameters.AddWithValue("@ResultTypeID", body.ResultTypeID);
                    command.Parameters.AddWithValue("@RollNo", body.RollNo);
                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEngID);
                    command.Parameters.AddWithValue("@IsRevised", body.IsRevised);
                    command.Parameters.AddWithValue("@SchemeID", body.SchemeID);
                    command.Parameters.AddWithValue("@EffectiveFromEndTermId", body.EffectiveFromEndTermId);

                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<bool> SaveExaminerData(ExaminerMaster request)
        {
            _actionName = "SaveExaminerData(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Examiner_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);
                        //command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        //command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);

                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        command.Parameters.AddWithValue("@ExamID", request.ExamID);
                        command.Parameters.AddWithValue("@GroupID", request.GroupID);
                        command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExaminerCode", request.ExaminerCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsAppointed", request.IsAppointed);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

        public async Task<DataTable> GetExaminerData(TeacherForExaminerSearchModel body)
        {
            _actionName = "GetExaminerData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Examiner_GetExaminerData";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        //command.Parameters.AddWithValue("@GroupCodeID", body.GroupCodeID);
                        //command.Parameters.AddWithValue("@ExamID", body.ExamID);
                        //command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);

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

        public async Task<bool> DeleteDataByID(ExaminerMaster request)
        {
            _actionName = "DeleteDataByID(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $" update M_ExaminerMaster set ActiveStatus=0, DeleteStatus=1, IsAppointed=0,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ExaminerID={request.ExaminerID}";

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

        public async Task<DataSet> MarksheetLetterDownload(MarksheetDownloadSearchModel model)
        {
            _actionName = "MarksheetLetterDownload(MarksheetDownloadSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        if (model.ExamTypeID == (int)EnumResultType.RevaluationResult) 
                        {
                            command.CommandText = "USP_GetMarksheetLetterData_AfterReval";
                            command.Parameters.AddWithValue("@Action", "_getStuListMarksheetLetter_reval");
                        }
                        else if (model.ExamTypeID == (int)EnumResultType.RwhResult)
                        {
                            command.CommandText = "USP_GetMarksheetLetterData_RWH";
                            command.Parameters.AddWithValue("@Action", "_getStuListMarksheetLetter_rwh");
                            command.Parameters.AddWithValue("@EffectiveFromEndTermId", model.EffectiveFromEndTermId);
                        }
                        else if (model.ExamTypeID == (int)EnumResultType.RwhRevalEffected)
                        {
                            command.CommandText = "USP_GetMarksheetLetterData_RWH_Reval";
                            command.Parameters.AddWithValue("@Action", "_getStuListMarksheetLetter_rwh_reval");
                            command.Parameters.AddWithValue("@EffectiveFromEndTermId", model.EffectiveFromEndTermId);
                        }
                        else
                        {
                            command.CommandText = "USP_GetMarksheetData";
                            command.Parameters.AddWithValue("@Action", "_getStuListMarksheetLetter_main");
                        }

                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@ExamTypeID", model.ExamTypeID);

                        command.Parameters.AddWithValue("@FinancialYearID", model.AcademicYearID);
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

        public async Task<DataTable> Get5thSemBackPaperReport(BackPaperReportDataModel body)
        {
            _actionName = "Get5thSemBackPaperReport(BackPaperReportDataModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Report_5thSemBackPaper";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);

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

        public async Task<DataSet> GetStudentResult_public(StudentResultSearchModel model)
        {
            _actionName = "GetStudentResult_public(StudentResultSearchModel model)";
            try
            {
                var ds = new DataSet();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_Rpt_GetStudentResult";

                    command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    command.Parameters.AddWithValue("@RollNo", model.RollNo);
                    command.Parameters.AddWithValue("@DOB", model.DOB);
                    command.Parameters.AddWithValue("@ResultTypeID", model.ResultType);
                    command.Parameters.AddWithValue("@HasBulk", model.HasBulk);

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
        }

        public async Task<DataTable> GetResultEndTermDDLList()
        {
            _actionName = "Get5thSemBackPaperReport()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonMaster";
                        command.Parameters.AddWithValue("@action", "GetResultEndTermDDLList");

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

        public async Task<DataSet> GetStudentResultRWH_public(StudentResultSearchModel model)
        {
            _actionName = "GetStudentResultRWH_public(StudentResultSearchModel model)";
            try
            {
                var ds = new DataSet();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_Rpt_GetStudentResultRWH";

                    command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    command.Parameters.AddWithValue("@RollNo", model.RollNo);
                    command.Parameters.AddWithValue("@DOB", model.DOB);
                    command.Parameters.AddWithValue("@ResultTypeID", model.ResultType);
                    command.Parameters.AddWithValue("@EffectiveEndTermIDNew", model.EffectiveEndTermID);
                    command.Parameters.AddWithValue("@HasBulk", model.HasBulk);

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
        }

        public async Task<int> UpdateMarksheetFile(List<StudentDownloadInfo> request)
        {
            _actionName = "UpdateMarksheetFile(StudentDownloadInfo request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_UpdateStudentMarksheetFile";
                    command.Parameters.AddWithValue("@StudentList", JsonConvert.SerializeObject(request));
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);

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
        }

        public async Task<DataSet> GetStudentResultReval_public(StudentResultSearchModel model)
        {
            _actionName = "GetStudentResultReval_public(StudentResultSearchModel model)";
            try
            {
                var ds = new DataSet();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_Rpt_GetStudentResult_Reval";

                    command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    command.Parameters.AddWithValue("@RollNo", model.RollNo);
                    command.Parameters.AddWithValue("@DOB", model.DOB);
                    command.Parameters.AddWithValue("@ResultTypeID", model.ResultType);
                    command.Parameters.AddWithValue("@HasBulk", model.HasBulk);

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
        }

        public async Task<int> AddUpdateMarksheet(MarksheetSaveDataModel request)
        {
            _actionName = "AddUpdateMarksheet(MarksheetSaveDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_SaveStudentMarksheetData";
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@action", "_saveMarksheetData");
                    command.Parameters.AddWithValue("@StudentList", JsonConvert.SerializeObject(request));
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);

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
        }


        public async Task<DataTable> GetStudentsDiplomaCertificate(DiplomaCertificateDownloadSearchModel body)
        {
            _actionName = "GetStudentsDiplomaCertificate(DiplomaCertificateDownloadSearchModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_StudentListForDiplomaCertificate";
                    command.CommandTimeout = 0;

                    if (body.ResultTypeID == (int)EnumResultType.MainResult) // main and reval
                    {
                        command.Parameters.AddWithValue("@action", "_getStuListForFinalDiploma_main");
                    }
                    else if (body.ResultTypeID == (int)EnumResultType.RevaluationResult)// after reval
                    {
                        command.Parameters.AddWithValue("@action", "_getStuListForFinalDiploma_reval");
                    }
                    else if (body.ResultTypeID == (int)EnumResultType.RwhResult)
                    {
                        command.Parameters.AddWithValue("@action", "_getStuListForFinalDiploma_rwh");
                    }
                    else if (body.ResultTypeID == (int)EnumResultType.RwhRevalEffected)
                    {
                        command.Parameters.AddWithValue("@action", "_getStuListForFinalDiploma_rwh_reval");
                    }
                    else if (body.ResultTypeID == (int)EnumResultType.Ufm)
                    {
                        throw new Exception("Invalid request!");
                    }
                    else
                    {
                        throw new Exception("Invalid request!");
                    }

                    command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                    command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                    command.Parameters.AddWithValue("@IsBridge", body.IsBridge);
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                    command.Parameters.AddWithValue("@ResultTypeID", body.ResultTypeID);
                    command.Parameters.AddWithValue("@RollNo", body.RollNo);
                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEngID);
                    command.Parameters.AddWithValue("@IsRevised", body.IsRevised);
                    command.Parameters.AddWithValue("@SchemeID", body.SchemeID);
                    command.Parameters.AddWithValue("@EnrollmentNo", body.EnrollmentNo);
                    command.Parameters.AddWithValue("@EffectiveFromEndTermId", body.EffectiveFromEndTermId);

                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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


        public async Task<int> AddUpdateFinalDiplomaCertificate(FinalDiplomaCertificateSaveDataModel request)
        {
            _actionName = "AddUpdateFinalDiplomaCertificate(FinalDiplomaCertificateSaveDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_SaveStudentFinalDiplomaCertificateData";
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@action", "_SaveStudentFinalDiplomaCertificateData");

                    command.Parameters.AddWithValue("@FinalDiploma", request.FinalDiploma); // id
                    command.Parameters.AddWithValue("@enrollment", request.Enrollment);
                    command.Parameters.AddWithValue("@institute_id", request.InstituteId);
                    command.Parameters.AddWithValue("@sr_diploma", request.SrNo); // FD srno.
                    command.Parameters.AddWithValue("@result_date", request.ResultDate); // publish date
                    command.Parameters.AddWithValue("@is_locked", request.IsLocked);
                    command.Parameters.AddWithValue("@diploma_printing_date", request.DiplomaPrintingDate); // printing date
                    command.Parameters.AddWithValue("@is_rwh_result", request.IsRwhResult);
                    command.Parameters.AddWithValue("@rwh_result_id", request.RwhResultId);
                    command.Parameters.AddWithValue("@is_reval", request.IsReval);
                    command.Parameters.AddWithValue("@is_revised_issue_date", request.IsRevisedIssueDate);
                    command.Parameters.AddWithValue("@result_id", request.ResultId);// examresultid
                    command.Parameters.AddWithValue("@revised_id", request.RevisedId);
                    command.Parameters.AddWithValue("@is_block", request.IsBlock); //
                    command.Parameters.AddWithValue("@student_id", request.StudentId);
                    command.Parameters.AddWithValue("@modifed", request.ModifyBy);
                    command.Parameters.AddWithValue("@is_diploma", request.IsDiploma);
                    command.Parameters.AddWithValue("@is_duplicate", request.IsDuplicate);
                    command.Parameters.AddWithValue("@duplicate_diploma_id", request.DuplicateDiplomaId);
                    command.Parameters.AddWithValue("@request_id", request.RequestId);
                    command.Parameters.AddWithValue("@is_issued", request.IsIssued);
                    command.Parameters.AddWithValue("@ResultTypeID", request.ResultTypeID);
                    command.Parameters.AddWithValue("@EndTermID", request.EndTermID); // current end term id and rwh
                    command.Parameters.AddWithValue("@EffectiveEndTermID", request.EffectiveEndTermID); // current end term id
                    command.Parameters.AddWithValue("@IsRevised", request.IsRevised);
                    command.Parameters.AddWithValue("@FileName", request.FileName); // with file path
                    command.Parameters.AddWithValue("@Dis_FileName", request.Dis_FileName); // only file name
                    command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                    command.Parameters.AddWithValue("@IpAddress", request.IPAddress);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();
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
        }


        #region Provisional Diploma Certificate download

        public async Task<DataTable> GetStudentsProvisionalDiplomaCertificate(DiplomaCertificateDownloadSearchModel body)
        {
            _actionName = "GetStudentsProvisionalDiplomaCertificate(DiplomaCertificateDownloadSearchModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_StudentListForProvisionalCertificate";
                    command.CommandTimeout = 0;

                    //if (body.ResultTypeID == (int)EnumResultType.MainResult) // main and reval
                    //{
                    //    command.Parameters.AddWithValue("@action", "_getStuListForProvisionalDiploma");
                    //}
                    //else if (body.ResultTypeID == (int)EnumResultType.RwhResult ||
                    //            body.ResultTypeID == (int)EnumResultType.RwhRevalEffected)
                    //{
                    //    command.Parameters.AddWithValue("@action", "_getRWHStuListForProvisionalDiploma");
                    //}
                    //else if (body.ResultTypeID == (int)EnumResultType.Ufm)
                    //{
                    //    throw new Exception("Invalid request!");
                    //}
                    //else
                    //{
                    //    throw new Exception("Invalid request!");
                    //}

                    command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                    command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                    command.Parameters.AddWithValue("@IsBridge", body.IsBridge);
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                    command.Parameters.AddWithValue("@ResultTypeID", body.ResultTypeID);
                    command.Parameters.AddWithValue("@RollNo", body.RollNo);
                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEngID);
                    command.Parameters.AddWithValue("@IsRevised", body.IsRevised);
                    command.Parameters.AddWithValue("@SchemeID", body.SchemeID);
                    command.Parameters.AddWithValue("@EnrollmentNo", body.EnrollmentNo);
                    command.Parameters.AddWithValue("@EffectiveFromEndTermId", body.EffectiveFromEndTermId);

                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<int> AddUpdateProvisionalDiplomaCertificate(ProvisionalDiplomaCertificateSaveDataModel request)
        {
            _actionName = "AddUpdateProvisionalDiplomaCertificate(ProvisionalDiplomaCertificateSaveDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_SaveStudentProvisionalDiplomaCertificateData";
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@action", "_SaveStudentProvisionalDiplomaCertificateData");

                    command.Parameters.AddWithValue("@ProvisionalDiplomaID", request.ProvisionalDiplomaID); // id
                    command.Parameters.AddWithValue("@enrollment", request.Enrollment);
                    command.Parameters.AddWithValue("@institute_id", request.InstituteId);
                    command.Parameters.AddWithValue("@sr_diploma", request.SrNo); // FD srno.
                    command.Parameters.AddWithValue("@result_date", request.ResultDate); // publish date
                    command.Parameters.AddWithValue("@is_locked", request.IsLocked);
                    command.Parameters.AddWithValue("@diploma_printing_date", request.DiplomaPrintingDate); // printing date
                    command.Parameters.AddWithValue("@is_rwh_result", request.IsRwhResult);
                    command.Parameters.AddWithValue("@rwh_result_id", request.RwhResultId);
                    command.Parameters.AddWithValue("@is_reval", request.IsReval);
                    command.Parameters.AddWithValue("@is_revised_issue_date", request.IsRevisedIssueDate);
                    command.Parameters.AddWithValue("@result_id", request.ResultId);// examresultid
                    command.Parameters.AddWithValue("@revised_id", request.RevisedId);
                    command.Parameters.AddWithValue("@is_block", request.IsBlock); //
                    command.Parameters.AddWithValue("@student_id", request.StudentId);
                    command.Parameters.AddWithValue("@modifed", request.ModifyBy);
                    command.Parameters.AddWithValue("@is_diploma", request.IsDiploma);
                    command.Parameters.AddWithValue("@is_duplicate", request.IsDuplicate);
                    command.Parameters.AddWithValue("@duplicate_diploma_id", request.DuplicateDiplomaId);
                    command.Parameters.AddWithValue("@request_id", request.RequestId);
                    command.Parameters.AddWithValue("@is_issued", request.IsIssued);
                    command.Parameters.AddWithValue("@ResultTypeID", request.ResultTypeID);
                    command.Parameters.AddWithValue("@EndTermID", request.EndTermID); // current end term id and rwh
                    command.Parameters.AddWithValue("@EffectiveEndTermID", request.EffectiveEndTermID); // current end term id
                    command.Parameters.AddWithValue("@IsRevised", request.IsRevised);
                    command.Parameters.AddWithValue("@FileName", request.FileName); // with file path
                    command.Parameters.AddWithValue("@Dis_FileName", request.Dis_FileName); // only file name
                    command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                    command.Parameters.AddWithValue("@IpAddress", request.IPAddress);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();
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
        }


        #endregion


        #region Migration Diploma Certificate download

        public async Task<DataTable> GetStudentsMigrationDiplomaCertificate(DiplomaCertificateDownloadSearchModel body)
        {
            _actionName = "GetStudentsMigrationDiplomaCertificate(DiplomaCertificateDownloadSearchModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_StudentListForMigrationCertificate";
                    command.CommandTimeout = 0;

                    //if (body.ResultTypeID == (int)EnumResultType.MainResult) // main and reval
                    //{
                    //    command.Parameters.AddWithValue("@action", "_getStuListForMigrationDiploma");
                    //}
                    //else if (body.ResultTypeID == (int)EnumResultType.RwhResult ||
                    //            body.ResultTypeID == (int)EnumResultType.RwhRevalEffected)
                    //{
                    //    command.Parameters.AddWithValue("@action", "_getRWHStuListForMigrationDiploma");
                    //}
                    //else if (body.ResultTypeID == (int)EnumResultType.Ufm)
                    //{
                    //    throw new Exception("Invalid request!");
                    //}
                    //else
                    //{
                    //    throw new Exception("Invalid request!");
                    //}

                    command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                    command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                    command.Parameters.AddWithValue("@IsBridge", body.IsBridge);
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                    command.Parameters.AddWithValue("@ResultTypeID", body.ResultTypeID);
                    command.Parameters.AddWithValue("@RollNo", body.RollNo);
                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEngID);
                    command.Parameters.AddWithValue("@IsRevised", body.IsRevised);
                    command.Parameters.AddWithValue("@SchemeID", body.SchemeID);
                    command.Parameters.AddWithValue("@EnrollmentNo", body.EnrollmentNo);
                    command.Parameters.AddWithValue("@EffectiveFromEndTermId", body.EffectiveFromEndTermId);

                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<int> AddUpdateMigrationCertificate(MigrationCertificateSaveDataModel request)
        {
            _actionName = "AddUpdateMigrationCertificate(MigrationCertificateSaveDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_SaveStudentMigrationCertificateData";
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@action", "_SaveStudentMigrationCertificateData");

                    command.Parameters.AddWithValue("@MigrationID", request.MigrationID); // id
                    command.Parameters.AddWithValue("@enrollment", request.Enrollment);
                    command.Parameters.AddWithValue("@institute_id", request.InstituteId);
                    command.Parameters.AddWithValue("@sr_migration", request.SrNo); // FD srno.
                    command.Parameters.AddWithValue("@result_date", request.ResultDate); // publish date
                    command.Parameters.AddWithValue("@is_locked", request.IsLocked);
                    command.Parameters.AddWithValue("@migration_printing_date", request.MigrationPrintingDate); // printing date
                    command.Parameters.AddWithValue("@is_rwh_result", request.IsRwhResult);
                    command.Parameters.AddWithValue("@rwh_result_id", request.RwhResultId);
                    command.Parameters.AddWithValue("@is_reval", request.IsReval);
                    command.Parameters.AddWithValue("@is_revised_issue_date", request.IsRevisedIssueDate);
                    command.Parameters.AddWithValue("@result_id", request.ResultId);// examresultid
                    command.Parameters.AddWithValue("@revised_id", request.RevisedId);
                    command.Parameters.AddWithValue("@is_block", request.IsBlock); //
                    command.Parameters.AddWithValue("@student_id", request.StudentId);
                    command.Parameters.AddWithValue("@modifed", request.ModifyBy);
                    command.Parameters.AddWithValue("@is_diploma", request.IsDiploma);
                    command.Parameters.AddWithValue("@is_duplicate", request.IsDuplicate);
                    command.Parameters.AddWithValue("@duplicate_migration_id", request.DuplicateMigrationId);
                    command.Parameters.AddWithValue("@request_id", request.RequestId);
                    command.Parameters.AddWithValue("@is_issued", request.IsIssued);
                    command.Parameters.AddWithValue("@ResultTypeID", request.ResultTypeID);
                    command.Parameters.AddWithValue("@EndTermID", request.EndTermID); // current end term id and rwh
                    command.Parameters.AddWithValue("@EffectiveEndTermID", request.EffectiveEndTermID); // current end term id
                    command.Parameters.AddWithValue("@IsRevised", request.IsRevised);
                    command.Parameters.AddWithValue("@FileName", request.FileName); // with file path
                    command.Parameters.AddWithValue("@Dis_FileName", request.Dis_FileName); // only file name
                    command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                    command.Parameters.AddWithValue("@IpAddress", request.IPAddress);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();
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
        }


        #endregion



    }
}
