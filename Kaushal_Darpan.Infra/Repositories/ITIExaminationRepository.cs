using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StudentMaster;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class ITIExaminationRepository : IITIExaminationRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIExaminationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIExaminationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetPreExamStudent(PreExamStudentModel model)
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
                        command.CommandText = "USP_ITI_PreExamStudentData";

                        if (model.StudentFilterStatusId == (int)EnumExamStudentStatus.Addimited || model.StudentFilterStatusId == (int)EnumExamStudentStatus.New_Addimited)
                        {
                            command.Parameters.AddWithValue("@action", "getStudentApplicationData");
                        }
                        else if (model.StudentFilterStatusId == (int)EnumExamStudentStatus.RejectatBTER)
                        {
                            command.Parameters.AddWithValue("@action", "getStudentRejectAtBter");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@action", "getStudentExamData");
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
                        command.Parameters.AddWithValue("@EligibilityStatus", model.EligibilityStatus);

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




        public async Task<DataTable> GetPreEnrollStudent(PreExamStudentModel model)
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
                        command.CommandText = "USP_ITI_GetStudentEnrollmentData";

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
                        command.CommandText = "USP_PreExamStudentData";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
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
                        storedProcedureName = "USP_EditApplicationData_PreExam";
                    }
                    else
                    {
                        storedProcedureName = "USP_ITI_EditStudentData_PreExam";
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
                        command.Parameters.AddWithValue("@StudentExamID", request.StudentExamID);

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
                        command.Parameters.AddWithValue("@EnrollmentNo_Old", request.EnrollmentNo);
                        command.Parameters.AddWithValue("@InstituteID_New", request.InstituteID);
                        command.Parameters.AddWithValue("@StreamID_New", request.StreamID);
                        command.Parameters.AddWithValue("@OrderNo", request.OrderNo);
                        command.Parameters.AddWithValue("@OrderDate", request.OrderDate);
                        command.Parameters.AddWithValue("@UpdatedDate", request.UpdatedDate);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

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

        public async Task<bool> PreExamStudentSubject(PreExamStudentSubjectRequestModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "PreExam_UpdateEnrollmentNo(PreExamStudentSubjectRequestModel request)";
                try
                {

                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SaveStudentSubjectData";

                        //command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        //command.Parameters.AddWithValue("@EnrollmentNo_Old", request.EnrollmentNo);
                        //command.Parameters.AddWithValue("@InstituteID_New", request.InstituteID);
                        //command.Parameters.AddWithValue("@StreamID_New", request.StreamID);
                        //command.Parameters.AddWithValue("@OrderNo", request.OrderNo);
                        //command.Parameters.AddWithValue("@OrderDate", request.OrderDate);
                        //command.Parameters.AddWithValue("@UpdatedDate", request.UpdatedDate);
                        //command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@Students", JsonConvert.SerializeObject(request.Students));
                        command.Parameters.AddWithValue("@Subjects", JsonConvert.SerializeObject(request.Subjects));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out                        



                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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
        public async Task<List<Student_DataModel>> Save_Student_Exam_Status()
        {
            _actionName = "GetAllData(Student_DataModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPlacementSelectedStudents";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "_getAllData");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<Student_DataModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<Student_DataModel>>(dataTable);
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

        public async Task<int> Save_Student_Exam_Status(List<Student_DataModel> entity)
        {
            _actionName = "Save_Student_Exam_Status(List<Student_DataModel> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Insert_PreExam_Status";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditAllData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
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
        public async Task<int> Save_Student_Exam_Status_Update(List<Student_DataModel> entity)
        {
            _actionName = "Save_Student_Exam_Status(List<Student_DataModel> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_UpdateExaminationStatus";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
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
        public async Task<PreExamSubjectModel> GetStudentSubject_ByID(int PK_ID, int DepartmentID)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_SubjectStudentMaster";
                        command.Parameters.AddWithValue("@StudentID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new PreExamSubjectModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<PreExamSubjectModel>(dataSet.Tables[0]);
                            data.Subjects = CommonFuncationHelper.ConvertDataTable<List<SubjectModel>>(dataSet.Tables[1]);
                        }
                    }
                    return data;
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
                        command.CommandText = "USP_ITISaveAdmittedStudents";
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
                        command.CommandText = "USP_ItiSaveEligibleForEnrollment";
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
                        command.CommandText = "USP_ITI_SaveSelectedForExamination";
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
                        command.CommandText = "USP_ITI_SaveEligibleForExamination";
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
                        command.CommandText = "USP_SaveRejectAtBTER";
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

        public async Task<int> UpdateStudentEligibility(StudentAttendenceModel model)
        {
            _actionName = "UpdateStudentEligibility(StudentAttendenceModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_UpdateStudentEligibility";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@EligibilityStatus", model.EligibilityStatus);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        command.Parameters.AddWithValue("@FAMarks", model.FaMark);
                        command.Parameters.AddWithValue("@Remarks", model.Remarks);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
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


        public async Task<bool> RevertStatus(RevertDataModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "RevertStatus(RevertDataModel request)";
                try
                {

                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_RevertStatus";

                        command.Parameters.AddWithValue("@StudentExamID", request.StudentExamID);
                        command.Parameters.AddWithValue("@status", request.status);
                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
                    }
                    if (retval > 0)
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

        public async Task<ITIExamination_UpdateEnrollmentNoModel> GetStudentDropoutStudent(int StudentID, int StudentExamID)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "iti_getdropoutstudentdetails";
                        command.Parameters.AddWithValue("@StudentID", StudentID);
                        command.Parameters.AddWithValue("@StudentExamID", StudentExamID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITIExamination_UpdateEnrollmentNoModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITIExamination_UpdateEnrollmentNoModel>(dataSet.Tables[0]);
                            
                        }
                    }
                    return data;
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


        public async Task<int> UpdateDropout(ITIExamination_UpdateEnrollmentNoModel model)
        {
            _actionName = "UpdateDropout(StudentAttendenceModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_UpdateStudentDropout";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        command.Parameters.AddWithValue("@OrderNo", model.OrderNo);
                        command.Parameters.AddWithValue("@OrderDate", model.OrderDate);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@FileName", model.FileName);
                        command.Parameters.AddWithValue("@IsDropout", model.IsDropout);
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

        public async Task<int> ReturnToAdmitted(int StudentID)
        {
            _actionName = "UpdateDropout(StudentAttendenceModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_ReturnToAdmitted";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@StudentID",StudentID);

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

    }
}
