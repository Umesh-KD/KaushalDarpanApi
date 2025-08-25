using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.ITIPracticalExaminer;
using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.TimeTable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
  
    public class ITIInvigilatorRepository : IITIInvigilatorRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private readonly string _IPAddress;

        public ITIInvigilatorRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIInvigilatorRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(TimeTableSearchModel model)
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
                        command.CommandText = "USP_ITIinvigilatorData";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@ShiftID", model.ShiftID);
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

        public async Task<int> SaveInvigilator(ItiInvigilatorDataModel request)
        {
            _actionName = "SaveInvigilator(ItiInvigilatorDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ItiSaveInvigilator";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@InvigilatorID", request.InvigilatorID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@TimeTableID", request.TimeTableID);
                        command.Parameters.AddWithValue("@RollNoTo", request.RollNoTo);
                        command.Parameters.AddWithValue("@RollNoFrom", request.RollNoFrom);

                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
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



        public async Task<DataTable> GetAllInvigilator(ItiInvigilatorSearchModel model)
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
                        command.CommandText = "USP_ITIGetInvigilator";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);

                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);

                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@TimeTableID", model.TimeTableID);
                        command.Parameters.AddWithValue("@Action", model.Action);
                        command.Parameters.AddWithValue("@InvigilatorID", model.InvigilatorID);
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


        public async Task<DataTable> GetAllTheoryStudents(ItiTheoryStudentMaster model)
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
                        command.CommandText = "USP_ITI_GetTheoryStudentIti";
                      
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndtermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.EngNong);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SubjectName", model.SubjectName);
               
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

        public async Task<DataTable> GetInvigilatorData_UserWise(int departmentId, int semesterId, int instituteId, int endTermId, int shiftId, int engNonEng, int userId)
        {
            _actionName = "GetInvigilatorData_UserWise";
            return await Task.Run(async () =>
            {
                try
                {
                    var dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIinvigilatorData_WithRollRange_UserWise";

                        command.Parameters.AddWithValue("@DepartmentID", departmentId);
                        command.Parameters.AddWithValue("@SemesterID", semesterId);
                        command.Parameters.AddWithValue("@InstituteID", instituteId);
                        command.Parameters.AddWithValue("@EndTermID", endTermId);
                        command.Parameters.AddWithValue("@ShiftID", shiftId);
                        command.Parameters.AddWithValue("@Eng_NonEng", engNonEng);
                        command.Parameters.AddWithValue("@UserID", userId);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
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


        public async Task<DataTable> GetTheoryStudentsByRollRangeAsync(ItiInvigilatorDataModel model)
        {
            _actionName = "GetTheoryStudentsByRollRangeAsync";

            return await Task.Run(async () =>
            {
                try
                {
                    var dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetTheoryStudentIti_WithRollRange";

                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID ?? 0);
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID ?? 0);
                        command.Parameters.AddWithValue("@Action", string.Empty);
                        command.Parameters.AddWithValue("@StudentExamID", 0);
                        command.Parameters.AddWithValue("@ExamType", 1);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng ?? 0);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@SubjectID", 0);
                        command.Parameters.AddWithValue("@SubjectName", model.SubjectName);
                        command.Parameters.AddWithValue("@RollNumberFrom", model.RollNoFrom ?? string.Empty);
                        command.Parameters.AddWithValue("@RollNumberTo", model.RollNoTo ?? string.Empty);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
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


        public async Task<int> SaveIsPresentData(List<StudentExamMarksUpdateModel> entityList)
        {
            _actionName = "UpdateIsPresentData(List<StudentExamMarksUpdateModel> entityList)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Update_IsPresent_ByStudentExamPaperMarksID";

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entityList));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
             
                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();

                        await command.ExecuteNonQueryAsync();


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

        public async Task<DataTable> ITIInvigilatorDashboard(ItiInvigilatorSearchModel filterModel)
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
                        command.CommandText = "USP_ITI_InvigilatorDashboard";
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);

           
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);
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

        public async Task<DataTable> Iti_InvigilatorPaymentGenerateAndViewPdf(ITI_InvigilatorPDFViewModal filterModel)
        {
            _actionName = "Iti_InvigilatorPaymentGenerateAndViewPdf(ITI_InvigilatorPDFViewModal filterModel)";
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
                        command.Parameters.AddWithValue("@action", "_samplePDF_for_Invigilator");
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@ExaminerID", filterModel.InvigilatorID);
                        command.Parameters.AddWithValue("@GroupCodeID", filterModel.Status);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@TotalSelectedShift", filterModel.ITIInvigilatorIDs);    //comma seprated Selected Invigilator IDS  24072025

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
    


      public async Task<DataTable> Iti_InvigilatorSubmitandForwardToAdmin(ITI_InvigilatorPDFForwardModal filterModel)
        {
            _actionName = "Iti_InvigilatorSubmitandForwardToAdmin(ITI_InvigilatorPDFForwardModal filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITISaveRemunerationInvigilator";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@Action", "Insert");
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@FileName", filterModel.FileName);                      
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", filterModel.Eng_NonEng);                       
                        command.Parameters.AddWithValue("@InvigilatorIDs", filterModel.ITIInvigilatorID);                       
                    

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


        public async Task<DataTable> GetItiRemunerationInvigilatorAdminDetails(ITI_AdminInvigilatorRemunerationDetailModal filterModel)
        {
            _actionName = "GetItiRemunerationInvigilatorAdminDetails(ITI_AdminInvigilatorRemunerationDetailModal filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITISaveRemunerationInvigilator";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "GetRemunerationInvigilatorDetail_admin");
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@UserID", filterModel.Userid);

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


        public async Task<int> UpdateToApprove(ITI_AdminInvigilatorRemunerationDetailModal model)
        {
            _actionName = "UpdateToApprove(RenumerationExaminerPDFModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITISaveRemunerationInvigilator";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "UpdateToApprove");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@UserID", model.Userid);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@RemunerationId", model.RemunerationPKID);
                        command.Parameters.AddWithValue("@Remark", model.Remarks);

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



        public async Task<DataTable> GetinvigilatorDetailbyRemunerationID(int remunerationid =0)
        {
            _actionName = "GetinvigilatorDetailbyRemunerationID(RenumerationExaminerPDFModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITISaveRemunerationInvigilator";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "InvigilatorDetailbyRemunerationID");
                        command.Parameters.AddWithValue("@RemunerationId", remunerationid);

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


        public async Task<DataTable> GetRemunerationApproveList(ITI_AdminInvigilatorRemunerationDetailModal filterModel)
        {
            _actionName = "GetRemunerationApproveList(ITI_AdminInvigilatorRemunerationDetailModal filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITISaveRemunerationInvigilator";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "RemunerationApproveList");
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@UserID", filterModel.Userid);

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
    }
}
