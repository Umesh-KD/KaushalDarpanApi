using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.InvigilatorAppointmentMaster;
using Kaushal_Darpan.Models.StaffMaster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class InvigilatorAppointmentMasterRepository: IInvigilatorAppointmentMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public InvigilatorAppointmentMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "InvigilatorAppointmentMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(InvigilatorAppointmentMasterSearchModel searchRequest)
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
                        command.CommandText = "USP_GetInvigilatorData";
                        command.Parameters.AddWithValue("@action",searchRequest.action); // Assuming you are using the action filter
                        command.Parameters.AddWithValue("@InstituteID", searchRequest.InstituteID);
                        command.Parameters.AddWithValue("@UserID", searchRequest.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", searchRequest.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", searchRequest.Eng_NonEng);
                        //command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        //command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        //command.Parameters.AddWithValue("@StateID", body.StateID);
                        //command.Parameters.AddWithValue("@DistrictID", body.DistrictID);



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

        public async Task<bool> SaveData(InvigilatorAppointmentMasterModel request)
        {
            _actionName = "SaveData(InvigilatorAppointmentMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveInvigilatorData";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@InvigilatorAppointmentID", request.InvigilatorAppointmentID);
                        command.Parameters.AddWithValue("@StreamID", request.CourseID); // Correctly mapped to @StreamID
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@ExamDate", request.Date);
                        command.Parameters.AddWithValue("@RollNo_From", request.RollNumberFrom);  // Correct mapping
                        command.Parameters.AddWithValue("@RollNo_To", request.RollNumberTo);      // Correct mapping
                        command.Parameters.AddWithValue("@Room_Number", request.RoomNumber);      // Correct mapping
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);

                        command.Parameters.AddWithValue("@JSONData", JsonConvert.SerializeObject(request.InvigilatorSSOID)); // Keep the same for @JSONData
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out


                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

        public async Task<InvigilatorAppointmentMasterModel> GetById(int PK_ID, int DepartmentID)
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
                        command.CommandText = "USP_GetInvigilatorData";
                        command.Parameters.AddWithValue("@action", "_getDataByID");
                        command.Parameters.AddWithValue("@InvigilatorAppointmentID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new InvigilatorAppointmentMasterModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<InvigilatorAppointmentMasterModel>(dataSet.Tables[0]);
                            data.InvigilatorSSOID = CommonFuncationHelper.ConvertDataTable<List<InvigilatorSSOIDModel>>(dataSet.Tables[1]);
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

        public async Task<bool> DeleteDataByID(InvigilatorAppointmentMasterModel request)
        {
            _actionName = "DeleteDataByID(HRMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DeleteInvigilatorData";
                        command.Parameters.AddWithValue("@InvigilatorAppointmentID", request.InvigilatorAppointmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<DataTable> UnlockExamAttendance_GetCSData(InvigilatorAppointmentMasterSearchModel searchRequest)
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
                        command.CommandText = "USP_UnlockExamAttendance_GetCSData";
                        command.Parameters.AddWithValue("@action", "_getCSListForAdmin"); // Assuming you are using the action filter
                        command.Parameters.AddWithValue("@InstituteID", searchRequest.InstituteID);
                        command.Parameters.AddWithValue("@UserID", searchRequest.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", searchRequest.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", searchRequest.Eng_NonEng);
                        command.Parameters.AddWithValue("@ExamDate", searchRequest.ExamDate);
                        command.Parameters.AddWithValue("@ShiftID", searchRequest.ShiftID);

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

        public async Task<bool> UnlockExamAttendance(UnlockExamAttendanceDataModel request)
        {
            _actionName = "UnlockExamAttendance(UnlockExamAttendanceDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_UnlockExamAttendance";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CenterSuperintendentID", request.CenterSuperintendentID);
                        command.Parameters.AddWithValue("@TimeTableID", request.TimeTableID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);


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
    }
}


