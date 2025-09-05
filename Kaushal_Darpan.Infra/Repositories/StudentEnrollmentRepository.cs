using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Newtonsoft.Json;
using System.Data;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class StudentEnrollmentRepository : IStudentEnrollmentRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public StudentEnrollmentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "StudentEnrollmentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetPreExamStudent(PreExamStudentModel model)
        {
            _actionName = "GetPreExamStudent(PreExamStudentModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentEnrollmentData";

                        if (model.StudentFilterStatusId == (int)EnumExamStudentStatus.Addimited || model.StudentFilterStatusId == (int)EnumExamStudentStatus.New_Addimited)
                        {
                            command.Parameters.AddWithValue("@action", "getStudentApplicationData");
                        }
                        else if (model.StudentFilterStatusId == (int)EnumExamStudentStatus.RejectatBTER)
                        {
                            command.Parameters.AddWithValue("@action", "getStudentRejectAtBter");
                        }
                        else if (model.StudentFilterStatusId == (int)EnumExamStudentStatus.Dropout)
                        {
                            command.Parameters.AddWithValue("@action", "getDropoutStudent");
                        }
                        else if (model.StudentFilterStatusId == (int)EnumExamStudentStatus.ReturnByAcp)
                        {
                            command.Parameters.AddWithValue("@action", "getStudentAdmittedByStatus");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@action", "getStudentMasterData");
                        }

                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@ManagementTypeID", model.ManagementTypeID);
                        command.Parameters.AddWithValue("@FinacialYearID", model.FinacialYearID);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@BranchID", model.BranchID);
                        command.Parameters.AddWithValue("@Year_SemID", model.Year_SemID);
                        command.Parameters.AddWithValue("@StudentTypeID", model.StudentTypeID);
                        command.Parameters.AddWithValue("@StudentStatusID", model.StudentStatusID);
                        command.Parameters.AddWithValue("@StudentFilterStatusId", model.StudentFilterStatusId);
                        command.Parameters.AddWithValue("@ExamCategoryID", model.ExamCategoryID);
                        command.Parameters.AddWithValue("@OptionalSubjectStatus", model.OptionalSubjectStatus);
                        command.Parameters.AddWithValue("@BridgeCourseID", model.BridgeCourseID);
                        command.Parameters.AddWithValue("@ApplicationNo", model.ApplicationNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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

        public async Task<DataTable> GetStudentAdmitted(PreExamStudentModel model)
        {
            _actionName = "GetPreExamStudent(PreExamStudentModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAddimittedStudentData";
                        command.Parameters.AddWithValue("@action", "getStudentAdmitted");
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@ManagementTypeID", model.ManagementTypeID);
                        command.Parameters.AddWithValue("@FinacialYearID", model.FinacialYearID);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@BranchID", model.BranchID);
                        command.Parameters.AddWithValue("@Year_SemID", model.Year_SemID);
                        command.Parameters.AddWithValue("@StudentTypeID", model.StudentTypeID);
                        command.Parameters.AddWithValue("@StudentStatusID", model.StudentStatusID);
                        command.Parameters.AddWithValue("@StudentFilterStatusId", model.StudentFilterStatusId);
                        command.Parameters.AddWithValue("@ExamCategoryID", model.ExamCategoryID);
                        command.Parameters.AddWithValue("@OptionalSubjectStatus", model.OptionalSubjectStatus);
                        command.Parameters.AddWithValue("@BridgeCourseID", model.BridgeCourseID);
                        command.Parameters.AddWithValue("@ApplicationNo", model.ApplicationNo);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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

        public async Task<bool> EditStudentData_PreExam(StudentMasterModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetAnnextureListPreExamStudent(StudentMasterModel request)";
                try
                {
                    int result = 0;
                    string storedProcedureName = string.Empty;
                    if (request.StudentFilterStatusId == (int)EnumExamStudentStatus.Addimited || request.StudentFilterStatusId == (int)EnumExamStudentStatus.New_Addimited)
                    {
                        storedProcedureName = "USP_EditApplicationData_Enroll";
                    }
                    else
                    {
                        storedProcedureName = "USP_EditStudentData_Enroll";
                    }

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@StudentName", request.StudentName);
                        command.Parameters.AddWithValue("@StudentNameHindi", request.StudentNameHindi);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@FatherNameHindi", request.FatherNameHindi);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        command.Parameters.AddWithValue("@MotherNameHindi", request.MotherNameHindi);
                        //command.Parameters.AddWithValue("@StudentTypeID", request.StudentTypeID);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@ABCID", request.ABCID);
                        //command.Parameters.AddWithValue("@Papers", request.Papers);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@Status", request.status);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@CategoryA_ID", request.CategoryA_ID);
                        command.Parameters.AddWithValue("@CategoryB_ID", request.CategoryB_ID);
                        command.Parameters.AddWithValue("@DOB", request.DOB);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@AadharNo", request.AadharNo);
                        command.Parameters.AddWithValue("@BhamashahNo", request.BhamashahNo);
                        command.Parameters.AddWithValue("@Address1", request.Address1);
                        command.Parameters.AddWithValue("@StudentPhoto", request.StudentPhoto);
                        command.Parameters.AddWithValue("@StudentSign", request.StudentSign);
                        command.Parameters.AddWithValue("@BankName", request.BankName);
                        command.Parameters.AddWithValue("@IFSCCode", request.IFSCCode);
                        command.Parameters.AddWithValue("@BankAccountNo", request.BankAccountNo);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@Status_Old", request.Status_old);
                        command.Parameters.AddWithValue("@QualificationDetails_Str", JsonConvert.SerializeObject(request.QualificationDetails));
                        command.Parameters.AddWithValue("@DocumentDetails", JsonConvert.SerializeObject(request.DocumentDetails));
                        command.Parameters.AddWithValue("@SubCategoryA_ID", request.SubCategoryA_ID);
                        command.Parameters.AddWithValue("@IsVerified", request.IsVerified);

                        _sqlQuery = command.GetSqlExecutableQuery(); // Log the SQL query
                        result = await command.ExecuteNonQueryAsync();
                    }

                    // Check the result and return the appropriate response
                    return result > 0;
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

        public async Task<bool> PreExam_UpdateEnrollmentNo(PreExam_UpdateEnrollmentNoModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "PreExam_UpdateEnrollmentNo(PreExam_UpdateEnrollmentNoModel request)";
                try
                {

                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_UpdateEnrollmentCollegeAndBranch";

                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@EnrollmentNo_Old", request.EnrollmentNo);
                        command.Parameters.AddWithValue("@InstituteID_New", request.InstituteID);
                        command.Parameters.AddWithValue("@StreamID_New", request.StreamID);
                        command.Parameters.AddWithValue("@OrderNo", request.OrderNo);
                        command.Parameters.AddWithValue("@OrderDate", request.OrderDate);
                        command.Parameters.AddWithValue("@UpdatedDate", request.UpdatedDate);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@StudentExamID", request.StudentExamID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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

        public async Task<int> SaveAdmittedFinalStudentData(List<StudentMarkedModelForJoined> model)
        {
            _actionName = "SaveAdmittedFinalStudentData(List<StudentMarkedModelForJoined> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AdmitedStudentStatus";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addStudentAdmittedData");
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

        public async Task<int> SaveAdmittedStudentData(List<StudentMarkedModel> model)
        {
            _actionName = "SaveAdmittedStudentData(List<StudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveAdmittedStudents";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addStudentAdmittedData");
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

        public async Task<int> UndoRejectAtbter(List<RejectMarkModel> model)
        {
            _actionName = "SaveAdmittedStudentData(List<StudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_UndoRejectStatus";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_saveRejectAtBTER");
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

        public async Task<int> SaveEligibleForEnrollment(List<StudentMarkedModel> model)
        {
            _actionName = "SaveEligibleForEnrollment(List<StudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveEligibleForEnrollment";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addStudentEligibleForEnrollmentData");
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

        public async Task<int> SaveSelectedForExamination(List<StudentMarkedModel> model)
        {
            _actionName = "SaveSelectedForExamination(List<StudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveSelectedForExamination";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addStudentSaveSelectedForExaminationData");
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

        public async Task<int> SaveEligibleForExamination(List<StudentMarkedModel> model)
        {
            _actionName = "SaveEligibleForExamination(List<StudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveEligibleForExamination";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addStudentEligibleForExaminationData");
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

        public async Task<int> SaveRejectAtBTER(List<StudentMarkedModel> model)
        {
            _actionName = "SaveRejectAtBTER(List<StudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveRejectAtBTEREnrollment";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_saveRejectAtBTER");
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

        public async Task<int> SaveDropout(List<StudentMarkedModel> model)
        {
            _actionName = "SaveDropout(List<StudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveDropout";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_saveDropout");
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


        public async Task<int> SaveRevokeDropout(List<StudentMarkedModel> model)
        {
            _actionName = "SaveRevokeDropout(List<StudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveRevokeDropout";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_saveDropout");
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




        public async Task<DataTable> GetAnnextureListPreExamStudent(PreExamStudentModel model)
        {
            _actionName = "GetPreExamStudent()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAddimittedStudentData";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@FinacialYearID", model.FinacialYearID);
                        command.Parameters.AddWithValue("@action", "_getAnnextureListPreExamStudent");

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


        #region admitted to verify
        public async Task<DataTable> GetAdmittedStudentToVerify(StudentApplicationModel model)
        {
            _actionName = "GetAdmittedStudentToVerify(StudentApplicationModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAdmittedStudentToVerify";
                        command.Parameters.AddWithValue("@action", "getAdmittedStudentPendingToVerify");

                        command.Parameters.AddWithValue("@StudentName", model.StudentName);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@ApplicationNo", model.ApplicationNo);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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

        public async Task<int> SaveAdmittedStudentForApproveByAcp(List<StudentApplicationSaveModel> model)
        {
            _actionName = "SaveAdmittedStudentForApproveByAcp(List<StudentApplicationSaveModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveAdmittedStudentToVerify";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_saveAdmittedStudentToApprove");
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

        public async Task<int> SaveAdmittedStudentForReturnByAcp(List<StudentApplicationSaveModel> model)
        {
            _actionName = "SaveAdmittedStudentForReturnByAcp(List<StudentApplicationSaveModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveAdmittedStudentToVerify";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_saveAdmittedStudentToReturn");
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


