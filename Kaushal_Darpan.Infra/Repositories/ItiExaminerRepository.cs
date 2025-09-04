using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BridgeCourse;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ItiExaminerRepository : IItiExaminerRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ItiExaminerRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ItiExaminerRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ItiExaminerSearchModel body)
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
                        command.CommandText = "USP_ITIExaminer";
                        command.Parameters.AddWithValue("@ExaminerCode", body.ExaminerCode);
                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@Email", body.Email);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@action", "_GetAllExaminer");

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


        public async Task<DataTable> GetStudentTheory(ITITeacherForExaminerSearchModel body)
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
                        command.CommandText = "USP_ITI_GetStudentTheoryBundles";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@RollTo    ", body.RollTo);
                        command.Parameters.AddWithValue("@RollFrom", body.RollFrom);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@SubjectCode", body.SubjectCode);
                        //command.Parameters.AddWithValue("@IsPractical", body.IsPractical);
                        //command.Parameters.AddWithValue("@IsTheory", body.IsTheory);
                        command.Parameters.AddWithValue("@ExaminerID", body.ExaminerID);

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



        public async Task<DataTable> GetITIExaminer(ItiExaminerSearchModel body)
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
                        command.CommandText = "USP_ITIExaminerList";


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



        public async Task<ITIExaminerModel> GetById(int PK_ID, int StaffSubjectID, int DepartmentID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        // command.CommandText = "select * from M_ExaminerMaster Where ExaminerID='" + PK_ID + "' ";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIExaminer";
                        command.Parameters.AddWithValue("@ExaminerID", PK_ID);
                        command.Parameters.AddWithValue("@action", "_GetExaminerById");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ITIExaminerModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ITIExaminerModel>(dataTable);
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
        public async Task<int> SaveData(ITIExaminerModel request)
        {
            _actionName = "SaveData(ItiExaminerModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIExaminer_IU";
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@DateOfBirth", request.DateOfBirth);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@District", request.District);
                        command.Parameters.AddWithValue("@Address", request.Address);
                        command.Parameters.AddWithValue("@AadharNumber", request.AadharNumber);
                        command.Parameters.AddWithValue("@BhamashahNumber", request.BhamashahNumber);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@EducationQualification", request.EducationQualification);
                        command.Parameters.AddWithValue("@Branch_Trade", request.Branch_Trade);
                        command.Parameters.AddWithValue("@Designation", request.Designation);
                        command.Parameters.AddWithValue("@PostingPlace", request.PostingPlace);
                        command.Parameters.AddWithValue("@BankAccountNumber", request.BankAccountNumber);
                        command.Parameters.AddWithValue("@IFSCCode", request.IFSCCode);
                        command.Parameters.AddWithValue("@BankName", request.BankName);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        //command.Parameters.AddWithValue("@RTS", request.RTS);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out





                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out

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
        public async Task<bool> DeleteDataByID(int ExaminerID = 0, int ModifyBy = 0)
        {
            _actionName = "DeleteDataByID(ItiExaminerModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        //command.CommandText = $"update M_ExaminerMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ExaminerID={request.ExaminerID}";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIExaminer";
                        command.Parameters.AddWithValue("@ExaminerID", ExaminerID);
                        command.Parameters.AddWithValue("@ModifyBy", ModifyBy);
                        command.Parameters.AddWithValue("@action", "_DeleteExaminerById");
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

        public async Task<DataTable> GetTeacherForExaminer(ITITeacherForExaminerSearchModel body)
        {
            _actionName = "GetTeacherForExaminer()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = "USP_ITI_GetStudentExaminer";
                        command.CommandText = "USP_ITI_GetStudentExaminer";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@RollTo    ", body.RollTo);
                        command.Parameters.AddWithValue("@RollFrom", body.RollFrom);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        //command.Parameters.AddWithValue("@IsPractical", body.IsPractical);
                        //command.Parameters.AddWithValue("@IsTheory", body.IsTheory);
                        command.Parameters.AddWithValue("@ExaminerID", body.ExaminerID);

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

        //public async Task<int> SaveExaminerData(ITIExaminerMaster request)
        //{
        //    _actionName = "SaveExaminerData(ExaminerMaster request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = _dbContext.CreateCommand(true))
        //            {
        //                // Set the stored procedure name and type
        //                command.CommandText = "USP_ITIExaminer_IU";
        //                command.CommandType = CommandType.StoredProcedure;

        //                // Add parameters with appropriate null handling
        //                command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);
        //                //command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
        //                //command.Parameters.AddWithValue("@StreamID", request.StreamID);
        //                command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
        //                command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
        //                command.Parameters.AddWithValue("@StaffID", request.StaffID);

        //                command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
        //                command.Parameters.AddWithValue("@ExamID", request.ExamID);
        //                command.Parameters.AddWithValue("@GroupID", request.GroupID);
        //                command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@SSOID", request.SSOID ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@ExaminerCode", request.ExaminerCode ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@IsAppointed", request.IsAppointed);
        //                command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
        //                command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
        //                command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
        //                command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
        //                command.Parameters.AddWithValue("@IPAddress", _IPAddress);
        //                command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
        //                command.Parameters.AddWithValue("@CourseType", request.CourseType);
        //                command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
        //                command.Parameters.AddWithValue("@RollFrom", request.RollFrom);
        //                command.Parameters.AddWithValue("@RollTo", request.RollTo);

        //                command.Parameters.Add("@Return", SqlDbType.Int);// out
        //                command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                // Execute the command
        //                result = await command.ExecuteNonQueryAsync();
        //                result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
        //            }

        //            return result;

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

        public async Task<int> SaveStudent(List<ItiAssignStudentExaminer> model)
        {
            _actionName = "SaveStudent(List<ItiAssignStudentExaminer> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveExaminerStudents";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_SaveSelectedForBridgeCourse");
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

        public async Task<DataTable> GetItiExaminerDashboardTiles(ITI_ExaminerDashboardModel body)
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
                        command.CommandText = "USP_ITIExaminer_Dashboard";
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);

                        command.Parameters.AddWithValue("@action", "GetExaminerDashboardTiles");

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

        public async Task<DataTable> GetItiAppointExaminerDetails(ITI_AppointExaminerDetailsModel body)
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
                        command.CommandText = "USP_ITIExaminer_Dashboard";
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@ExaminerID", body.ExaminerID);
                        command.Parameters.AddWithValue("@Status", body.Status);

                        command.Parameters.AddWithValue("@action", "AppointExaminerDetails");

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


        public async Task<DataTable> GetItiExaminerBundleDetails(ITI_AppointExaminerDetailsModel body)
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
                        command.CommandText = "USP_ITI_ExaminerBundleList";
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@ExaminerID", body.ExaminerID);
                        //command.Parameters.AddWithValue("@Status", body.Status);

                 

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



        public async Task<DataTable> GetItiRemunerationExaminerDetails(ITI_AppointExaminerDetailsModel body)
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
                        command.CommandText = "USP_ITIExaminer_Dashboard";
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        //command.Parameters.AddWithValue("@EndTermID", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //command.Parameters.AddWithValue("@EndTermID", body.RoleID);
                        //command.Parameters.AddWithValue("@ExaminerID", body.ExaminerID);
                        //command.Parameters.AddWithValue("@Status", body.Status);

                        command.Parameters.AddWithValue("@action", "RemunerationExaminerDetails");

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

        public async Task<DataTable> DeleteAssignStudentByExaminerID(int examinerId)
        {
            _actionName = "DeleteAssignStudentByExaminerID()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DeleteAssignStudentByExaminerID";
                        command.Parameters.AddWithValue("@ExaminerID", examinerId);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable(); // returns empty DataTable, but consistent with your structure
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



        public async Task<DataTable> GetTeacherForExaminerById(ITITeacherForExaminerSearchModel body)
        {
            _actionName = "GetTeacherForExaminer()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = "USP_ITI_GetStudentExaminer";
                        command.CommandText = "USP_ITI_GetStudentExaminerByID";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@RollTo    ", body.RollTo);
                        command.Parameters.AddWithValue("@RollFrom", body.RollFrom);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        //command.Parameters.AddWithValue("@IsPractical", body.IsPractical);
                        //command.Parameters.AddWithValue("@IsTheory", body.IsTheory);
                        command.Parameters.AddWithValue("@ExaminerID", body.ExaminerID);

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
        public async Task<int> SaveExaminerData(ITITheoryExaminerModel request)
        {
            _actionName = "SaveExaminerData(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_ExaminerAssign_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);
                        //command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        //command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        //command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@CenterID", request.CenterID);
                        //command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);

                        //command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        //command.Parameters.AddWithValue("@ExamID", request.ExamID);
                        command.Parameters.AddWithValue("@ExaminerCode", request.ExaminerCode);
                        // command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                        //command.Parameters.AddWithValue("@SSOID", request.SSOID ?? (object)DBNull.Value);


                        command.Parameters.AddWithValue("@ModifyBy", request.UserID);
                        command.Parameters.AddWithValue("@CreatedBy", request.UserID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        //command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        // command.Parameters.AddWithValue("@CourseType", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@RollNoFrom", request.RollNoFrom);
                        command.Parameters.AddWithValue("@RollNoTo", request.RollNoTo);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@AppointExaminerID", request.AppointExaminerID);
                        command.Parameters.AddWithValue("@BundleID", request.BundleID);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request.StudentList));

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

        public async Task<DataTable> ITIAssignedExaminerInstituteDetailbyID(int BundelID)
        {
            _actionName = "ITIAssignedExaminerInstituteDetailbyID(int BundelID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Dispatch_Showbundle";
                        command.Parameters.AddWithValue("@BundelID", BundelID);
                        command.Parameters.AddWithValue("@Action", "ITI_AssignExaminerAndInsituteDetail");
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

        public async Task<DataTable> Iti_RemunerationGenerateAndViewPdf(ITI_AppointExaminerDetailsModel filterModel)
        {
            _actionName = "GetDataForGeneratePdf(RenumerationExaminerRequestModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "[USP_ITI_RemunerationGetExaminerReport]";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "_getGroupCodeOfExaminerPDF");
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@ExaminerID", filterModel.ExaminerID);
                        command.Parameters.AddWithValue("@GroupCodeID", filterModel.Status);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);

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



        public async Task<int> SaveDataSubmitAndForwardToAdmin(ITI_AppointExaminerDetailsModel request)
        {
            _actionName = "SaveDataSubmitAndForwardToJD(RenumerationExaminerPDFModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIExaminer_Dashboard";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "_saveSubmitAndForwardToAdmin");
                        command.Parameters.AddWithValue("@AppointExaminerID", request.ExaminerID);
                        command.Parameters.AddWithValue("@filename", request.filename);

                        //command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        //command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out

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
            });
        }

        public async Task<DataTable> GetItiRemunerationAdminDetails(ITI_AppointExaminerDetailsModel body)
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
                        command.CommandText = "USP_ITIExaminer_Dashboard";
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        //command.Parameters.AddWithValue("@EndTermID", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //command.Parameters.AddWithValue("@EndTermID", body.RoleID);
                        //command.Parameters.AddWithValue("@ExaminerID", body.ExaminerID);
                        //command.Parameters.AddWithValue("@Status", body.Status);

                        command.Parameters.AddWithValue("@action", "AdminRemunerationExaminerDetails");

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


        public async Task<int> UpdateToApprove(ITI_AppointExaminerDetailsModel request)
        {
            _actionName = "SaveDataSubmitAndForwardToJD(RenumerationExaminerPDFModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIExaminer_Dashboard";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "UpdateToApprove");
                        command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);

                        //command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        //command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out

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
            });
        }

    }
}








