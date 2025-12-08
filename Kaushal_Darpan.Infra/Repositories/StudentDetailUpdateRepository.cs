using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.SSOUserDetails;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.StudentDetailUpdate;
using Kaushal_Darpan.Models.StudentMaster;
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
    public class StudentDetailUpdateRepository : IStudentDetailUpdateRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;

        public StudentDetailUpdateRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "PreExamStudentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        public async Task<DataTable> GetStudentDetailUpdate(StudentDetailUpdateModel model)
        {
            _actionName = "GetStudentDetailUpdate()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentDetailUpdate";
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@ApplicationNo", model.ApplicationNo);
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
       
        public async Task<StudentDetailUpdateModel> GetById(int PK_ID, int DepartmentID, int Eng_NonEng)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentDetail_Edit";
                        command.Parameters.AddWithValue("@StudentID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", Eng_NonEng);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new StudentDetailUpdateModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<StudentDetailUpdateModel>(dataTable);
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

        public async Task<bool> UpdateStudentData(StudentDetailUpdateModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "EditStudentData_PreExam(StudentMasterModel request)";
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_EditStudentData_Insert_Update";

                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@StudentName", request.StudentName);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@Remarks", request.Remark);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@Dis_Document", request.Dis_Document);
                        command.Parameters.AddWithValue("@Document", request.Document);
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


        #region employement details start

        public async Task<DataTable> GetAllData(StudentDetailUpdateModel model)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentDetailOperation";
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@ApplicationNo", model.ApplicationNo);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@action", "_getAllData");

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

        public async Task<DataTable> GetStudentEmployementData(StudentEmploymentDetailsModel model)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    int result = 0;
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentProfile_Employement";
                        command.Parameters.AddWithValue("@AID", model.AID);
                        command.Parameters.AddWithValue("@CompanyName", model.CompanyName);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@Action","GetAllData");
                        //command.Parameters.Add("@Return", SqlDbType.Int); // out
                        //command.Parameters["@Return"].Direction = ParameterDirection.Output; // out
                        // Output parameter
                        var returnParam = command.Parameters.Add("@Return", SqlDbType.Int);
                        returnParam.Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();


                        //result = Convert.ToInt32(command.Parameters["@Return"].Value);
                        // Read the output value
                        if (returnParam.Value != DBNull.Value)
                            result = Convert.ToInt32(returnParam.Value);                                                    
                    
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


        public async Task<bool> SaveEmployementData(StudentEmploymentDetailsModel request)
        {
            _actionName = "SaveEmployementData(CompanyMasterModels request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentProfile_Employement";

                        command.Parameters.AddWithValue("@ListEmployementDetails", JsonConvert.SerializeObject(request.ListEmployementDetails));

                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out


                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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


        public async Task<bool> Delete_StudEmployementByID(StudentEmploymentDetailsModel request)
        {
            _actionName = "Delete_StudEmployementByID(StudentEmploymentDetailsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                       
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentProfile_Employement";
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@AID", request.AID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@Action", "Delete_StudEmpDataByID");
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
                                                                    // Read the output value
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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


        #endregion employement details end

        #region  additional qualification details start

        public async Task<DataTable> GetStudentAdditionalQualiData(LateralEntryQualificationModel model)
        {
            _actionName = "GetStudentAdditionalQualiData()";
            try
            {
                return await Task.Run(async () =>
                {
                    int result = 0;
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_AdditionalQualification";
                        //command.Parameters.AddWithValue("@AID", model.AID);
                        //command.Parameters.AddWithValue("@CompanyName", model.CompanyName);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        command.Parameters.AddWithValue("@Action", "GetAllData");
                        //command.Parameters.Add("@Return", SqlDbType.Int); // out
                        //command.Parameters["@Return"].Direction = ParameterDirection.Output; // out
                        // Output parameter
                        var returnParam = command.Parameters.Add("@Return", SqlDbType.Int);
                        returnParam.Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();


                        //result = Convert.ToInt32(command.Parameters["@Return"].Value);
                        // Read the output value
                        if (returnParam.Value != DBNull.Value)
                            result = Convert.ToInt32(returnParam.Value);

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

        public async Task<bool> Delete_StudentAdditionalQualiData(LateralEntryQualificationModel request)
        {
            _actionName = "Delete_StudentAdditionalQualiData(LateralEntryQualificationModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_AdditionalQualification";
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@Action", "Delete_AdditionalQualiByID");
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
                                                                    // Read the output value
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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


        #endregion  additional qualification details end



    }

}
