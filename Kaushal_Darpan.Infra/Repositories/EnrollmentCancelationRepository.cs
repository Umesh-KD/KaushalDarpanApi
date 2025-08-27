using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Attendance;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMeritIInfoModel;
using Newtonsoft.Json;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class EnrollmentCancelationRepository : IEnrollmentCancelationRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public EnrollmentCancelationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "EnrollmentCancelationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> ChangeEnRollNoStatus(StudentEnrolmentCancelModel model)
        {
            _actionName = "ChangeEnRollNoStatus(List<StudentEnrolmentCancelModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ChangeEnRollNoStatusCancel";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@action", model.Action);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@remark", model.Remark);


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


        public async Task<DataTable> GetEnrollCancelationData(StudentEnrolmentCancelModel model)
        {
            _actionName = "GetEnrollCancelationData(StudentEnrolmentCancelModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStatus_By_EnrollCancelation";
                        command.Parameters.AddWithValue("@Status", model.Status);
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

        public async Task<List<StudentDetailsModel>> GetAllData(StudentSearchModel searchModel)
        {
            _actionName = "GetAllData(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentDetails_ForEnrollCancelation";

                        command.Parameters.AddWithValue("@ApplicationNo", searchModel.ApplicationNo);
                        command.Parameters.AddWithValue("@action", searchModel.Action);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<StudentDetailsModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<StudentDetailsModel>>(dataTable);
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


        public async Task<List<StudentDetailsModel>> GetEnrollmentCancelList(StudentSearchModel searchModel)
        {
            _actionName = "GetAllData(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEnrollCancelationList";
                        command.Parameters.AddWithValue("@action", searchModel.Action);
                        command.Parameters.AddWithValue("@Status", searchModel.Status);
                        command.Parameters.AddWithValue("@RoleId", searchModel.RoleID);
                        command.Parameters.AddWithValue("@StudentID", searchModel.StudentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<StudentDetailsModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<StudentDetailsModel>>(dataTable);
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

        public async Task<int> CancelEnrolment(StudentEnrolmentCancelModel filterModel)
        {
            return await Task.Run(async () =>
            {
                _actionName = "CancelEnrolment(StudentEnrolmentCancelModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentEnrollmentCancelation_IU";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@IsRequestedForEnrCancel", filterModel.IsRequestedForEnrCancel);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@NextRoleId", filterModel.NextRoleId);
                        command.Parameters.AddWithValue("@StudentName", filterModel.StudentName);
                        command.Parameters.AddWithValue("@MotherName", filterModel.MotherName);
                        command.Parameters.AddWithValue("@FatherName", filterModel.FatherName);
                        command.Parameters.AddWithValue("@InstituteName", filterModel.InstituteName);
                        command.Parameters.AddWithValue("@StreamName", filterModel.StreamName);
                        command.Parameters.AddWithValue("@DOB", filterModel.DOB);
                        command.Parameters.AddWithValue("@Dis_ENRCancelDoc", filterModel.Dis_ENRCancelDoc);
                        command.Parameters.AddWithValue("@ENRCancelDoc", filterModel.ENRCancelDoc);

                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo);
                        command.Parameters.AddWithValue("@EndTermType", filterModel.EndTermType);
                        command.Parameters.AddWithValue("@EndTermName", filterModel.EndTermName);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@ActionId", filterModel.ActionId);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@CourseType", filterModel.CourseType);
                        command.Parameters.AddWithValue("@Status", filterModel.Status);
                        command.Parameters.AddWithValue("@Remark", " ");
                        command.Parameters.AddWithValue("@ModuleID", 1);
                        command.Parameters.AddWithValue("@ModifyBy", filterModel.UserId);
                        command.Parameters.AddWithValue("@CreatedBy", filterModel.UserId);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
                        await command.ExecuteNonQueryAsync();
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

    }
}
