using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.StaffDashboard;
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
    public class BTER_EstablishManagementRepository : IBTER_EstablishManagementRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public BTER_EstablishManagementRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BTER_EstablishManagementRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> BTER_EM_AddStaffInitialDetails(BTER_EM_AddStaffInitialDetailsDataModel request)
        {
            _actionName = "BTER_EM_AddStaffInitialDetails(List<BTER_EM_AddStaffInitialDetailsDataModel> request)";
            return await Task.Run(async () =>
            {
                try
                {

                    //var jsonData = JsonConvert.SerializeObject(request);
                    //DataTable dataTable = new DataTable();
                    int result = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_BTER_EM_AddStaffInitialDetail_Admin";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "AddDetails_ByAdmin");
                        //command.Parameters.AddWithValue("@AdminT2ZonalDataJson", jsonData);
                        command.Parameters.AddWithValue("@LevelID", request.LevelID ?? 0);
                        command.Parameters.AddWithValue("@OfficeID", request.OfficeID ?? 0);
                        command.Parameters.AddWithValue("@PostID", request.PostID ?? 0);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID ?? 0);
                        command.Parameters.AddWithValue("@IsNodal", request.IsNodal ?? false);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID ?? "");
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID ?? 0);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID ?? 0);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy ?? 0);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy ?? 0);
                        command.Parameters.AddWithValue("@StaffTypeID", request.StaffTypeID ?? 0);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID ?? 0);
                        command.Parameters.AddWithValue("@Name", request.Name ?? "");
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo ?? "");
                        command.Parameters.AddWithValue("@EmailID", request.EmailID ?? "");
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID ?? 0);

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

        public async Task<DataTable> BTER_EM_GetStaffList(BTER_EM_GetStaffListDataModel body)
        {
            _actionName = " BTER_EM_GetStaffList(BTER_EM_GetStaffListDataModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_EM_GetStaffList";
                        command.Parameters.AddWithValue("@action", "GetData");
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@LevelID", body.LevelID);
                        command.Parameters.AddWithValue("@OfficeID", body.OfficeID);
                        command.Parameters.AddWithValue("@StaffTypeID", body.StaffTypeID);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
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

        public async Task<int> BTER_EM_AddStaffPrinciple(BTER_EM_AddStaffPrincipleDataModel request)
        {
            _actionName = "BTER_EM_AddStaffPrinciple(BTER_EM_AddStaffPrincipleDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTER_EM_AddStaffPrinciple";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with default values or leave them as-is
                        command.Parameters.AddWithValue("@ProfileID", request.ProfileID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@StatusOfStaff", request.StatusOfStaff);

                        command.Parameters.AddWithValue("@StaffTypeID", request.StaffTypeID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID ?? "");
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        command.Parameters.AddWithValue("@DisplayName", request.DisplayName ?? "");
                        command.Parameters.AddWithValue("@AadhaarId", request.AadhaarId ?? "");
                        command.Parameters.AddWithValue("@BhamashahId", request.BhamashahId ?? "");
                        command.Parameters.AddWithValue("@BhamashahMemberId", request.BhamashahMemberId ?? "");
                        command.Parameters.AddWithValue("@DateOfBirth", request.DateOfBirth ?? "");
                        command.Parameters.AddWithValue("@Gender", request.Gender ?? "");
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo ?? "");
                        command.Parameters.AddWithValue("@TelephoneNumber", request.TelephoneNumber ?? "");
                        command.Parameters.AddWithValue("@IpPhone", request.IpPhone ?? "");
                        command.Parameters.AddWithValue("@MailPersonal", request.MailPersonal ?? "");
                        command.Parameters.AddWithValue("@PostalAddress", request.PostalAddress ?? "");
                        command.Parameters.AddWithValue("@PostalCode", request.PostalCode ?? "");
                        command.Parameters.AddWithValue("@City", request.City ?? "");
                        command.Parameters.AddWithValue("@State", request.State ?? "");
                        command.Parameters.AddWithValue("@Photo", request.Photo ?? "");
                        command.Parameters.AddWithValue("@Designation", request.Designation ?? "");
                        command.Parameters.AddWithValue("@Department", request.Department);
                        command.Parameters.AddWithValue("@MailOfficial", request.MailOfficial ?? "");
                        command.Parameters.AddWithValue("@EmployeeNumber", request.EmployeeNumber ?? "");
                        command.Parameters.AddWithValue("@DepartmentId", request.DepartmentID);
                        command.Parameters.AddWithValue("@FirstName", request.FirstName ?? "");
                        command.Parameters.AddWithValue("@LastName", request.LastName ?? "");
                        command.Parameters.AddWithValue("@JanaadhaarId", request.JanaadhaarId ?? "");
                        command.Parameters.AddWithValue("@ManaadhaarMemberId", request.ManaadhaarMemberId ?? "");
                        command.Parameters.AddWithValue("@UserType", request.UserType ?? "");
                        command.Parameters.AddWithValue("@Mfa", request.Mfa ?? "");
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress ?? "0.0.0.0");
                        command.Parameters.AddWithValue("@CourseType", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@HostelID", request.HostelID);
                        command.Parameters.AddWithValue("@StaffLevelID", request.StaffLevelID);
                        command.Parameters.AddWithValue("@CourseID", request.BranchID);
                        command.Parameters.AddWithValue("@TechnicianID", request.TechnicianID);
                        command.Parameters.AddWithValue("@StaffLevelChildID", request.StaffLevelChildID);
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@multiHostelIDs", request.multiHostelIDs);
                        command.Parameters.AddWithValue("@EMTypeID", request.EMTypeID);
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

        public async Task<DataTable> BTER_EM_GetPrincipleStaff(BTER_EM_StaffMasterSearchModel body)
        {
            _actionName = "Task<DataTable> BTER_EM_AddStaffPrinciple(BTER_EM_StaffMasterSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_EM_GetStaffMasterData";
                        command.Parameters.AddWithValue("@action", string.IsNullOrEmpty(body.Action) == true ? "_getAllData" : body.Action); // Assuming you are using the action filter
                        command.Parameters.AddWithValue("@StaffTypeID", body.StaffTypeID);
                        command.Parameters.AddWithValue("@CourseID", body.CourseID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@StateID", body.StateID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@CourseTypeId", body.CourseTypeId);
                        command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                        command.Parameters.AddWithValue("@FilterName", body.FilterName);
                        command.Parameters.AddWithValue("@FilterSSOID", body.FilterSSOID);
                        command.Parameters.AddWithValue("@FilterStaffTypeID", body.FilterStaffTypeID);

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

        public async Task<DataTable> BTER_EM_GetPersonalDetailByUserID(BTER_EM_GetPersonalDetailByUserID body)
        {
            _actionName = " BTER_EM_GetStaffList(BTER_EM_GetStaffListDataModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_EM_GetPersonalDetailByUserID";
                        command.Parameters.AddWithValue("@action", "StaffDetails");
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
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

        public async Task<int> BTER_EM_AddStaffDetails(BTER_EM_AddStaffDetailsDataModel request)
        {
            _actionName = "BTER_EM_AddStaffDetails(BTER_EM_AddStaffDetailsDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTER_EM_AddStaffDetails";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with default values or leave them as-is
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID ?? "");
                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        command.Parameters.AddWithValue("@DateOfBirth", request.DateOfBirth ?? "");
                        command.Parameters.AddWithValue("@GenderID", request.Gender);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@DepartmentId", request.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@CourseType", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@BranchID", request.BranchID);
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@ServiceBookBranchID", request.ServiceBookBranchID);
                        command.Parameters.AddWithValue("@Remark", request.Remark);

                        // Add missing parameters from SQL
                        command.Parameters.AddWithValue("@Name", request.Name ?? "");  
                        command.Parameters.AddWithValue("@EmployeeID", request.EmployeeID ?? "");  
                        command.Parameters.AddWithValue("@CurrentDesignationID", request.CurrentDesignationID); 
                        command.Parameters.AddWithValue("@DateOfAppointment", request.DateOfAppointment ?? ""); 
                        command.Parameters.AddWithValue("@DateOfJoining", request.DateOfJoining ?? ""); 
                        command.Parameters.AddWithValue("@Experience", request.Experience); 
                        command.Parameters.AddWithValue("@QualificationAtJoining", request.QualificationAtJoining ?? ""); 
                        command.Parameters.AddWithValue("@QualificationAfterJoining", request.QualificationAfterJoining ?? ""); 
                        command.Parameters.AddWithValue("@DateOfRetirement", request.DateOfRetirement ?? "");  // Add missing DateOfRetirement
                        command.Parameters.AddWithValue("@StaffSubjectListModel", JsonConvert.SerializeObject(request.bterStaffSubjectListModel));

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

        public async Task<int> BTER_EM_DeleteStaff(ITIGovtEM_OfficeDeleteModel body)
        {
            _actionName = "BTER_EM_DeleteStaff(ITIGovtEM_OfficeDeleteModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_EM_DeleteStaff";
                        command.Parameters.AddWithValue("@Action", "BTER_EM_DeleteStaff");
                        command.Parameters.AddWithValue("@UserID", body.ID);
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
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

        public async Task<int> BTER_EM_ApproveStaffProfile(BTER_EM_ApproveStaffDataModel request)
        {
            _actionName = "BTER_EM_ApproveStaffProfile(BTER_EM_ApproveStaffDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTER_EM_CompleteAndApproveStaffProfile";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with default values or leave them as-is
                        command.Parameters.AddWithValue("@UserID", request.UserID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID ?? "");
                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DateOfBirth", request.DateOfBirth ?? "");
                        command.Parameters.AddWithValue("@Gender", request.Gender ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber ?? "");
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BranchID", request.BranchID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SanctionedPosts", request.SanctionedPosts ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsWorking", request.IsWorking ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsExtraWorking", request.IsExtraWorking ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsVacant", request.IsVacant ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@OccupiedVacant", request.OccupiedVacant ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Name", request.Name ?? "");
                        command.Parameters.AddWithValue("@EmployeeID", request.EmployeeID ?? "");
                        command.Parameters.AddWithValue("@IsEmpWorkingOnPost", request.IsEmpWorkingOnPost ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsEmpWorkingOnDeputationFromOther", request.IsEmpWorkingOnDeputationFromOther ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EmpInstituteID", request.EmpInstituteID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EmpBranchID", request.EmpBranchID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsEmpWorkingOnDeputationToOther", request.IsEmpWorkingOnDeputationToOther ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EmpDeputatedInstituteID", request.EmpDeputatedInstituteID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsSalaryDrawnFromSamePost", request.IsSalaryDrawnFromSamePost ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SalaryDrawnPostID", request.SalaryDrawnPostID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsSalaryDrawnFromOtherInstitute", request.IsSalaryDrawnFromOtherInstitute ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SalaryDrawnInstituteID", request.SalaryDrawnInstituteID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DateOfRetirement", request.DateOfRetirement ?? "");
                        command.Parameters.AddWithValue("@AnyCourtCasePending", request.AnyCourtCasePending ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@AnyDisciplinaryActionPending", request.AnyDisciplinaryActionPending ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExtraOrdinaryLeave", request.ExtraOrdinaryLeave ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SelectionCategory", request.SelectionCategory ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@HigherEduPermission", request.HigherEduPermission ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Experience", request.Experience ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@HigherEduInstitute", request.HigherEduInstitute ?? "");
                        command.Parameters.AddWithValue("@Remark", request.Remark ?? "");
                        command.Parameters.AddWithValue("@StaffID", request.StaffID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@StaffUserID", request.StaffUserID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy ?? (object)DBNull.Value);

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

        public async Task<bool> BTER_EM_UnlockProfile(BTER_EM_UnlockProfileDataModel request)
        {
            _actionName = "BTER_EM_UnlockProfile(BTER_EM_UnlockProfileDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTER_EM_GetPersonalDetailByUserID";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@StaffUserID", request.StaffUserID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);

                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@Action", "_UnlockStaff");


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

        public async Task<DataTable> BTER_EM_InstituteDDL(int DepartmentID, int InsType, int DistrictId)

        {
            _actionName = "InstituteMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_EM_GetPersonalDetailByUserID";

                        command.Parameters.AddWithValue("@Action", "InstituteDDL");

                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@InsType", InsType);
                        command.Parameters.AddWithValue("@DistrictId", DistrictId);

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


        // BTER New


        public async Task<bool> FinalSubmitUpdateStaffProfileStatus(RequestUpdateStatus productDetails)
        {
            _actionName = "FinalSubmitUpdateStaffProfileStatus(int ID,  int ModifyBy)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                       // command.CommandText = "USP_ITI_Govt_EM_UpdateStaffProfileStatus";
                        command.CommandText = "USP_BTER_Govt_EM_UpdateStaffProfileStatus";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@StaffID", productDetails.ID);
                        command.Parameters.AddWithValue("@ModifyBy", productDetails.CreatedBy);


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

        public async Task<int> BTER_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(BTERGovtEM_DeleteByIdStaffServiceDelete body)
        {
            _actionName = "BTER_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(BTERGovtEM_DeleteByIdStaffServiceDelete body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       // command.CommandText = "USP_ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID";
                        command.CommandText = "USP_BTER_Govt_EM_ServiceDetailsOfPersonnelDeleteByID";
                        command.Parameters.AddWithValue("@ID", body.StaffID);
                        command.Parameters.AddWithValue("@ModifyBy", body.UserID);
                        command.Parameters.AddWithValue("@Action", "BTER_Govt_EM_ServiceDetailsOfPersonnelDeleteByID");
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

        public async Task<DataTable> GetBTER_Govt_EM_GetUserProfileStatus(int ID)
        {
            _actionName = "GetBTER_Govt_EM_GetUserProfileStatus(int ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_Govt_EM_GetUserProfileStatus";
                        command.Parameters.AddWithValue("@StaffID", ID);

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


        public async Task<BTERPersonalDetailByUserIDSearchModel> BTERGovtEM_BTER_Govt_Em_PersonalDetailByUserID(BTERPersonalDetailByUserIDSearchModel body)
        {
            _actionName = "BTERGovtEM_BTER_Govt_Em_PersonalDetailByUserID(PersonalDetailByUserIDSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    var data = new BTERPersonalDetailByUserIDSearchModel();

                    if (body.Action == "StaffDetails")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_BTER_Govt_Em_PersonalDetailByUserID";
                            command.Parameters.AddWithValue("@Action", body.Action);
                            command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
                            command.Parameters.AddWithValue("@SSOID", body.SSOID);
                            command.Parameters.AddWithValue("@StaffID", body.StaffID);
                            _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                            dataSet = await command.FillAsync();
                        }

                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {
                                data.bTERGovtEMStaffPersonalDetails = CommonFuncationHelper.ConvertDataTable<BTERGovtEMStaff_PersonalDetailsModel>(dataSet.Tables[0]);
                            }
                            if (dataSet.Tables.Count > 0)
                            {
                                data.PromotionList = CommonFuncationHelper.ConvertDataTable<List<BTERGovtEMStaff_PromotionIsRenouncedModel>>(dataSet.Tables[1]);
                            }

                        }
                    }
                    if (body.Action == "UserEducationalQualification")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_BTER_Govt_Em_PersonalDetailByUserID";
                            command.Parameters.AddWithValue("@Action", body.Action);
                            command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
                            command.Parameters.AddWithValue("@SSOID", body.SSOID);
                            command.Parameters.AddWithValue("@StaffID", body.StaffID);
                            _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                            dataSet = await command.FillAsync();
                        }

                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {
                                data.EducationalList = CommonFuncationHelper.ConvertDataTable<List<BTERGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel>>(dataSet.Tables[0]);
                            }
                        }
                    }
                    if (body.Action == "ServiceDetailsOfPersonnel")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_BTER_Govt_Em_PersonalDetailByUserID";
                            command.Parameters.AddWithValue("@Action", body.Action);
                            command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
                            command.Parameters.AddWithValue("@SSOID", body.SSOID);
                            command.Parameters.AddWithValue("@StaffID", body.StaffID);
                            _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                            dataSet = await command.FillAsync();
                        }

                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {

                                data.PostingList = CommonFuncationHelper.ConvertDataTable<List<StaffPostingData>>(dataSet.Tables[0]);

                            }
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
        public async Task<BTERPersonalDetailByUserIDSearchModel> BTERGovtEM_BTER_Govt_Em_PersonalDetailList(BTERPersonalDetailByUserIDSearchModel body)
        {
            _actionName = "BTERGovtEM_BTER_Govt_Em_PersonalDetailByUserID(PersonalDetailByUserIDSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    var data = new BTERPersonalDetailByUserIDSearchModel();

                    if (body.Action == "StaffDetails")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_BTER_Govt_Em_PersonalDetailByUserID";
                            command.Parameters.AddWithValue("@Action", body.Action);
                            command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
                            command.Parameters.AddWithValue("@SSOID", body.SSOID);
                            command.Parameters.AddWithValue("@StaffID", body.StaffID);
                            _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                            dataSet = await command.FillAsync();
                        }

                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {
                                data.bTERGovtEMStaffPersonalDetails = CommonFuncationHelper.ConvertDataTable<BTERGovtEMStaff_PersonalDetailsModel>(dataSet.Tables[0]);
                            }
                            if (dataSet.Tables.Count > 0)
                            {
                                data.PromotionList = CommonFuncationHelper.ConvertDataTable<List<BTERGovtEMStaff_PromotionIsRenouncedModel>>(dataSet.Tables[1]);
                            }

                        }
                    }
                    if (body.Action == "UserEducationalQualification")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_BTER_Govt_Em_PersonalDetailByUserID";
                            command.Parameters.AddWithValue("@Action", body.Action);
                            command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
                            command.Parameters.AddWithValue("@SSOID", body.SSOID);
                            command.Parameters.AddWithValue("@StaffID", body.StaffID);
                            _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                            dataSet = await command.FillAsync();
                        }

                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {
                                data.EducationalList = CommonFuncationHelper.ConvertDataTable<List<BTERGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel>>(dataSet.Tables[0]);
                            }
                        }
                    } 
                    if (body.Action == "ServiceDetailsOfPersonnel")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_BTER_Govt_Em_PersonalDetailByUserID";
                            command.Parameters.AddWithValue("@Action", body.Action);
                            command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
                            command.Parameters.AddWithValue("@SSOID", body.SSOID);
                            command.Parameters.AddWithValue("@StaffID", body.StaffID);
                            _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                            dataSet = await command.FillAsync();
                        }

                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {

                                data.PostingList = CommonFuncationHelper.ConvertDataTable<List<StaffPostingData>>(dataSet.Tables[0]);

                            }
                        }
                    }


                    if (body.Action == "ServiceDetailsOfPersonnelList")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_BTER_Govt_Em_PersonalDetailByUserID";
                            command.Parameters.AddWithValue("@Action", body.Action);
                            command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
                            command.Parameters.AddWithValue("@SSOID", body.SSOID);
                            command.Parameters.AddWithValue("@StaffID", body.StaffID);
                            _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                            dataSet = await command.FillAsync();
                        }

                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {

                                data.PostingList = CommonFuncationHelper.ConvertDataTable<List<StaffPostingData>>(dataSet.Tables[0]);

                            }
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

        public async Task<int> BTERGovtEM_Govt_StaffProfileStaffPosting(List<StaffPostingData> body)
        {
            _actionName = "BTERGovtEM_Govt_StaffProfileStaffPosting(List<StaffPostingData> body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    var jsonData = JsonConvert.SerializeObject(body);

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_Govt_EM_ServiceDetailsOfPersonnel_IU";
                        command.Parameters.AddWithValue("@Action", "BTER_Govt_EM_ServiceDetailsOfPersonnel_IU");
                        command.Parameters.AddWithValue("@jsonData", jsonData);
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
            });
        }


        #region
        public async Task<DataTable> GetBter_Govt_EM_UserProfileStatusHt(Bter_Govt_EM_UserRequestHistoryListSearchDataModel Model)
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
                        command.CommandText = "USP_Bter_Govt_EM_UserProfileStatusHt";
                        // Add parameters to the stored procedure from the model                        
                        command.Parameters.AddWithValue("@UserID", Model.StaffUserID);
                        command.Parameters.AddWithValue("@StaffID", Model.StaffID);
                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        command.Parameters.AddWithValue("@Action", "UserProfileStatusHt");
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

        public async Task<bool> Bter_GOVT_EM_ApproveRejectStaff(RequestUpdateStatus request)
        {
            _actionName = "LockandSubmit(StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_Bter_GOVT_EM_StaffApproveRejectStaff";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@ModifyBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@StatusOfStaff", request.StatusIDs);
                        command.Parameters.AddWithValue("@ProfileStatus", request.ProfileStatusID);
                        command.Parameters.AddWithValue("@Remark", request.Remark);

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

        public async Task<BTER_EM_AddStaffDetailsDataModel?> BTER_EM_GetBterStaffSubjectListModelStaffID(int PK_ID, int DepartmentID)
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
                        command.CommandText = "USP_BTER_EM_GetPersonalDetailByUserID";
                        command.Parameters.AddWithValue("@StaffID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@Action", "Staff_SubjectsDetails");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new BTER_EM_AddStaffDetailsDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {

                            data.bterStaffSubjectListModel = CommonFuncationHelper.ConvertDataTable<List<BterStaffSubjectListModel>>(dataSet.Tables[0]);
                            
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

        public async Task<DataTable> GetHODDash(HODDashboardSearchModel model)
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
                        command.CommandTimeout = 0;
                        command.CommandText = "usp_Bter_HODDashboard";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);

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

        public async Task<DataTable> GetStaff_HostelIDs(StaffHostelSearchModel body)
        {
            _actionName = "GetStaff_HostelIDs(StaffHostelSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStaff_HostelIDs";
                        command.Parameters.AddWithValue("@Action", "GetStaff_HostelID");
                        command.Parameters.AddWithValue("@StaffID", body.StaffID);
                        command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
                        
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

        public async Task<bool> SaveStaff_HostelIDs(StaffHostelSearchModel body)
        {
            _actionName = "SaveStaff_HostelIDs(StaffHostelSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_GetStaff_HostelIDs";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.CommandText = "USP_GetStaff_HostelIDs";
                        command.Parameters.AddWithValue("@Action", "SaveStaff_HostelID");
                        command.Parameters.AddWithValue("@StaffID", body.StaffID);
                        command.Parameters.AddWithValue("@StaffUserID", body.StaffUserID);
                        command.Parameters.AddWithValue("@HostelIDs", body.StaffHostelIDs);

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
