using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTERIMCAllocationModel;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.DispatchMaster;
using Kaushal_Darpan.Models.ITINodalOfficerExminerReport;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.SubjectMaster;
using Kaushal_Darpan.Models.ViewPlacedStudents;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Zlib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIGovtEMStaffMasterRepository : IITIGovtEMStaffMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIGovtEMStaffMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIGovtEMStaffMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetAllData(ITIGovtEMStaffMasterSearchModel body)
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
                        command.CommandText = "USP_ITI_Govt_GetStaffMasterData";
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
        public async Task<bool> SaveData(ITIGovtEMStaffMasterModel request)
        {
            _actionName = "SaveData(StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_StaffMaster_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@StaffTypeID", request.StaffTypeID);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@DateOfBirth", request.DateOfBirth);
                        command.Parameters.AddWithValue("@Pincode", request.Pincode);
                        command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@CourseID", request.CourseID);
                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@Experience", request.Experience);
                        command.Parameters.AddWithValue("@AdharCardNumber", CryptoHelper.DecryptData(request.AdharCardNumber));
                        command.Parameters.AddWithValue("@PanCardNumber", CryptoHelper.DecryptData(request.PanCardNumber));
                        command.Parameters.AddWithValue("@PFDeduction", request.PFDeduction);
                        command.Parameters.AddWithValue("@AnnualSalary", request.AnnualSalary);
                        command.Parameters.AddWithValue("@ResearchGuide", request.ResearchGuide);
                        command.Parameters.AddWithValue("@AdharCardPhoto", request.AdharCardPhoto);
                        command.Parameters.AddWithValue("@Dis_AdharCardNumber", request.Dis_AdharCardNumber);
                        command.Parameters.AddWithValue("@DateOfAppointment", request.DateOfAppointment);
                        command.Parameters.AddWithValue("@DateOfJoining", request.DateOfJoining);
                        command.Parameters.AddWithValue("@Dis_Certificate", request.Dis_Certificate);
                        command.Parameters.AddWithValue("@Certificate", request.Certificate);
                        command.Parameters.AddWithValue("@Dis_ProfileName", request.Dis_ProfileName);
                        command.Parameters.AddWithValue("@ProfilePhoto", request.ProfilePhoto);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@Address", request.Address);
                        command.Parameters.AddWithValue("@PanCardPhoto", request.PanCardPhoto);
                        command.Parameters.AddWithValue("@Dis_PanCardNumber", request.Dis_PanCardNumber);
                        command.Parameters.AddWithValue("@StaffStatus", request.StaffStatus);
                        command.Parameters.AddWithValue("@HigherQualificationID", request.HigherQualificationID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@SpecializationSubjectID", request.SpecializationSubjectID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@StatusOfStaff", request.StatusOfStaff);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@UGQualificationID", request.UGQualificationID);
                        command.Parameters.AddWithValue("@PGQualificationID", request.PGQualificationID);
                        command.Parameters.AddWithValue("@PHDQualification", request.PHDQualification);

                        //bank details Save
                        command.Parameters.AddWithValue("@BankName", request.BankName);
                        command.Parameters.AddWithValue("@BankAccountName", request.BankAccountName);
                        command.Parameters.AddWithValue("@BankAccountNo", request.BankAccountNo);
                        command.Parameters.AddWithValue("@IFSCCode", request.IFSCCode);

                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IsExaminer", request.IsExaminer);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@EduQualificationDetails", JsonConvert.SerializeObject(request.EduQualificationDetailsModel));
                        command.Parameters.AddWithValue("@StaffSubjectListModel", JsonConvert.SerializeObject(request.StaffSubjectListModel));


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
        public async Task<int> SaveBasicData(ITIGovtEMAddStaffBasicDetailDataModel request)
        {
            _actionName = "SaveData(AddStaffBasicDetailDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_Govt_SaveStaffBasicDetails";
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
                        command.Parameters.AddWithValue("@OfficeID", request.OfficeID);
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

        public async Task<ITIGovtEMStaffMasterModel> GetById(int PK_ID, int DepartmentID)
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
                        command.CommandText = "USP_ITI_Govt_StaffMaster_Edit";
                        command.Parameters.AddWithValue("@StaffID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITIGovtEMStaffMasterModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITIGovtEMStaffMasterModel>(dataSet.Tables[0]);
                            data.EduQualificationDetailsModel = CommonFuncationHelper.ConvertDataTable<List<ITIGovtEMStaff_EduQualificationDetailsModel>>(dataSet.Tables[1]);
                        }
                        if (dataSet.Tables.Count > 2)
                        {
                            data.StaffSubjectListModel = CommonFuncationHelper.ConvertDataTable<List<ITIGovtEMStaffSubjectListModel>>(dataSet.Tables[2]);
                        }

                        if (dataSet.Tables.Count > 3)
                        {
                            data.StaffHostelListModel = CommonFuncationHelper.ConvertDataTable<List<ITIGovtEMStaffHostelListModel>>(dataSet.Tables[3]);
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

        public async Task<bool> LockandSubmit(ITIGovtEMStaffMasterModel request)
        {
            _actionName = "LockandSubmit(StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_GetStaffMasterData";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@StatusOfStaff", request.StatusOfStaff);


                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@action", "_LockandSubmit");


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

        public async Task<bool> UnlockStaff(ITIGovtEMStaffMasterModel request)
        {
            _actionName = "LockandSubmit(StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_GetStaffMasterData";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@StatusOfStaff", request.StatusOfStaff);


                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@action", "_UnlockStaff");


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


        public async Task<bool> ApproveStaff(ITIGovtEMStaffMasterModel request)
        {
            _actionName = "LockandSubmit(StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_GetStaffMasterData";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@StatusOfStaff", request.StatusOfStaff);

                        command.Parameters.AddWithValue("@action", "_ApproveStaff");


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


        public async Task<DataTable> StaffLevelType(ITIGovtEMStaffMasterSearchModel body)
        {
            _actionName = "StaffLevelType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStaffMasterData";
                        command.Parameters.AddWithValue("@action", "ITIGovtEM_StaffLevelType"); // Assuming you are using the action filter
                        command.Parameters.AddWithValue("@StaffTypeID", body.StaffTypeID);


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
        public async Task<DataTable> StaffLevelChild(ITIGovtEMStaffMasterSearchModel body)
        {
            _actionName = "StaffLevelType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStaffMasterData";
                        command.Parameters.AddWithValue("@action", "ITIGovtEM_StaffLevelChild"); // Assuming you are using the action filter
                        command.Parameters.AddWithValue("@StaffLevelID", body.StaffLevelID);


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


        public async Task<bool> IsDownloadCertificate(ITIGovtEMStaffMasterModel request)
        {
            _actionName = "IsDownloadCertificate(StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_StaffMasterIsDownloadCertificate";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@action", "IsDownloadCertificate");
                        command.Parameters.AddWithValue("@StaffIDs", request.StaffIDs);
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


        public async Task<int> IsDeleteHostelWarden(string SSOID)
        {
            _actionName = "IsDeleteHostelWarden(SSOID request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_HostelWardenIsDelete";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SSOID", SSOID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

        public async Task<int> ChangeWorkingInstitute(ITIGovtEMAddStaffBasicDetailDataModel request)
        {
            _actionName = "ChangeWorkingInstitute(AddStaffBasicDetailDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_GetStaffMasterData";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@action", "_changeWorkingInstitute");

                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);

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

        public async Task<bool> ChangeWorkingInstitute(ITIGovtEMStaffMasterModel request)
        {
            _actionName = "ChangeWorkingInstitute(StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_GetStaffMasterData";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);

                        command.Parameters.AddWithValue("@action", "_changeWorkingInstitute");

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

        public async Task<DataTable> GetCurrentWorkingInstitute_ByID(ITIGovtEMStaffMasterSearchModel body)
        {
            _actionName = "StaffLevelType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StaffMaster_GetCurrentWorkingInstitute";
                        command.Parameters.AddWithValue("@StaffID", body.StaffID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);


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

        public async Task<int> UpdateSSOIDByPriciple(UpdateSSOIDByPricipleModel request)
        {
            _actionName = "UpdateSSOIDByPriciple(UpdateSSOIDByPricipleModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_Govt_UpdateSSOIDByPriciple";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with default values or leave them as-is

                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);

                        command.Parameters.AddWithValue("@SSOID", request.SSOID);

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

        public async Task<DataTable> GetPrincipleList(ITIGovtStudentSearchModel body)
        {
            _actionName = "GetPrincipleList(int CourseType, int DepartmentID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiGovtPrincipleList";
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", body.RoleId);
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

        public async Task<DataTable> GetAllITI_Govt_EM_OFFICERS(ITI_Govt_EM_OFFICERSSearchDataModel request)
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
                        command.CommandText = "USP_ITI_Govt_EM_OFFICERS";
                        command.Parameters.AddWithValue("@Action", "ITI_Govt_EM_OFFICERS_ByDevisionID");
                        command.Parameters.AddWithValue("@DivisionID", request.DivisionID);
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


        public async Task<int> ZonalOfficeCreateSSOID(UpdateSSOIDByPricipleModel request)
        {
            _actionName = "UpdateSSOIDByPriciple(UpdateSSOIDByPricipleModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_Govt_UpdateSSOIDByPriciple";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with default values or leave them as-is

                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);

                        command.Parameters.AddWithValue("@SSOID", request.SSOID);

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


        public async Task<DataTable> ITIGovtEM_OfficeGetAllData(ITIGovtEM_OfficeSearchModel body)
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
                        command.CommandText = "USP_Get_M_ITIGovtEM_Office";
                        command.Parameters.AddWithValue("@action", "GetData");
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
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

        public async Task<int> ITIGovtEM_OfficeSaveData(ITIGovtEM_OfficeSaveDataModel request)
        {
            _actionName = "ITIGovtEM_OfficeSaveData(ITIGovtEM_OfficeSaveDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_M_ITIGovtEM_Office_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@action", "SaveData");
                        // Add parameters with default values or leave them as-is
                        command.Parameters.AddWithValue("@OfficeID", request.OfficeID);
                        command.Parameters.AddWithValue("@OfficeName", request.OfficeName);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@LevelID", request.LevelID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermId);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

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


        public async Task<ITIGovtEM_OfficeSearchModel> ITIGovtEM_OfficeGetByID(int PK_Id)
        {
            _actionName = "ITIGovtEM_OfficeGetByID(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_M_ITIGovtEM_Office";
                        command.Parameters.AddWithValue("@action", "GetDataById");
                        command.Parameters.AddWithValue("@OfficeId", PK_Id);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITIGovtEM_OfficeSearchModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITIGovtEM_OfficeSearchModel>(dataSet.Tables[0]);




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


        public async Task<bool> ITIGovtEM_OfficeDeleteById(int ID, int userId)
        {
            _actionName = "ITIGovtEM_OfficeDeleteById(request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update [M_ITIGovtEM_Office]  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{userId} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where OfficeID = {ID}";
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


        public async Task<DataTable> ITIGovtEM_PostGetAllData(ITIGovtEM_PostSearchModel body)
        {
            _actionName = "ITIGovtEM_PostGetAllData(ITIGovtEM_PostSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EM_Post";
                        command.Parameters.AddWithValue("@action", "GetData");
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
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

        public async Task<int> ITIGovtEM_PostSaveData(ITIGovtEM_PostSaveDataModel request)
        {
            _actionName = "ITIGovtEM_PostSaveData(ITIGovtEM_PostSaveDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_Govt_EM_Post_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@action", "SaveData");
                        command.Parameters.AddWithValue("@ID", request.ID);
                        // Add parameters with default values or leave them as-is
                        command.Parameters.AddWithValue("@OfficeID", request.OfficeID);
                        command.Parameters.AddWithValue("@PostName", request.PostName);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermId);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
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


        public async Task<ITIGovtEM_PostSearchModel> ITIGovtEM_PostGetByID(int PK_Id)
        {
            _actionName = "ITIGovtEM_PostGetByID(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EM_Post";
                        command.Parameters.AddWithValue("@action", "GetDataById");
                        command.Parameters.AddWithValue("@ID", PK_Id);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITIGovtEM_PostSearchModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITIGovtEM_PostSearchModel>(dataSet.Tables[0]);




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


        public async Task<bool> ITIGovtEM_PostDeleteById(int ID, int userId)
        {
            _actionName = "ITIGovtEM_PostDeleteById(request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update [ITI_Govt_EM_Post]  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{userId} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ID = {ID}";
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

        public async Task<DataTable> ITIGovtEM_Govt_AdminT2Zonal_Save(List<ITI_Govt_EM_ZonalOFFICERSDataModel> request)
        {
            _actionName = "SaveData(ITIGovtEM_Govt_AdminT2Zonal_Save request)";
            return await Task.Run(async () =>
            {
                try
                {

                    var jsonData = JsonConvert.SerializeObject(request);
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_ITI_Govt_AdminT2Zonal_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "AdminT2Zonal_IU");
                        command.Parameters.AddWithValue("@AdminT2ZonalDataJson", jsonData);
                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //dataTable = await command.FillAsync_DataTable();
                        // Execute the command

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


        public async Task<ITI_Govt_EM_ZonalOFFICERSDataModel> ITIGovtEM_SSOIDCheck(string SSOID)
        {
            _actionName = "ITIGovtEM_SSOIDCheck(string SSOID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_AdminT2Zonal_IU";
                        command.Parameters.AddWithValue("@action", "SSOIdCheck");
                        command.Parameters.AddWithValue("@SSOIDs", SSOID);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITI_Govt_EM_ZonalOFFICERSDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITI_Govt_EM_ZonalOFFICERSDataModel>(dataSet.Tables[0]);




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


        public async Task<DataTable> ITIGovtEM_Govt_AdminT2Zonal_GetAllData(ITI_Govt_EM_ZonalOFFICERSSearchDataModel body)
        {
            _actionName = "ITIGovtEM_Govt_AdminT2Zonal_GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_GetAdminT2Zonal";
                        command.Parameters.AddWithValue("@action", "GetData");
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@LevelID", body.LevelID);
                        command.Parameters.AddWithValue("@OfficeID", body.OfficeID);
                        command.Parameters.AddWithValue("@StaffTypeID", body.StaffTypeID);
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


        public async Task<int> ITIGovtEM_Govt_StaffProfileQualification(List<ITIGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel> body)
        {
            _actionName = "ITIGovtEM_Govt_StaffProfileQualification()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    var jsonData = JsonConvert.SerializeObject(body);

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EM_EducationalQualification_IU";
                        command.Parameters.AddWithValue("@Action", "ITI_Govt_EM_EducationalQualification_IU");
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

        public async Task<int> ITIGovtEM_Govt_StaffProfileStaffPosting(List<StaffPostingData> body)
        {
            _actionName = "ITIGovtEM_Govt_StaffProfileStaffPosting(List<StaffPostingData> body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    var jsonData = JsonConvert.SerializeObject(body);

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EM_ServiceDetailsOfPersonnel_IU";
                        command.Parameters.AddWithValue("@Action", "ITI_Govt_EM_ServiceDetailsOfPersonnel_IU");
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


        public async Task<int> ITIGovtEM_Govt_StaffProfileUpdate(ITIGovtEMStaff_PersonalDetailsModel body)
        {
            _actionName = "ITIGovtEM_Govt_StaffProfileUpdate(ITIGovtEMStaff_PersonalDetailsModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    var jsonData = JsonConvert.SerializeObject(body.PromotionList);

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIGovtStaffProfile_IU";
                        command.Parameters.AddWithValue("@StaffID",body.StaffID);
                      
                        command.Parameters.AddWithValue("@RoleID", 0);
                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@EmployeeID", body.EmployeeID);
                        command.Parameters.AddWithValue("@CurrentBasicDesignationID", body.CurrentBasicDesignationID);
                        command.Parameters.AddWithValue("@CoreBusiness", body.CoreBusiness); 
                        command.Parameters.AddWithValue("@CurrentPostingEmp", body.CurrentPostingEmp);
                        command.Parameters.AddWithValue("@DateofPostingEmp", body.DateofPostingEmp ?? null);
                        command.Parameters.AddWithValue("@GenderID", body.GenderID);
                        command.Parameters.AddWithValue("@PanCardNumber", body.PanCardNumber);
                        command.Parameters.AddWithValue("@BloodGroupID", body.BloodGroupID);
                        command.Parameters.AddWithValue("@FatherName", body.FatherName);
                        command.Parameters.AddWithValue("@DateOfBirth", body.DateOfBirth);
                        command.Parameters.AddWithValue("@MaritalStatusID", body.MaritalStatusID);
                        command.Parameters.AddWithValue("@Husband_WifeName", body.Husband_WifeName);
                        command.Parameters.AddWithValue("@ServiceTypeHWID", body.ServiceTypeHWID);
                        command.Parameters.AddWithValue("@EmployeeIDOfHusband_Wife", body.EmployeeIDOfHusband_Wife);
                        command.Parameters.AddWithValue("@CastID", body.CastID);
                        command.Parameters.AddWithValue("@ReligionID", body.ReligionID);
                        command.Parameters.AddWithValue("@DivyangID", body.DivyangID);
                        command.Parameters.AddWithValue("@BeforeChildren", body.BeforeChildren);
                        command.Parameters.AddWithValue("@AfterChildren", body.AfterChildren);
                        command.Parameters.AddWithValue("@TotalChildren", body.TotalChildren);
                        command.Parameters.AddWithValue("@Address", body.Address);
                        command.Parameters.AddWithValue("@Pincode", body.Pincode);
                        command.Parameters.AddWithValue("@StateID", body.StateID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@StateHomeStateID", body.StateHomeStateID);
                        command.Parameters.AddWithValue("@Email", body.Email);
                        command.Parameters.AddWithValue("@MobileNumber", body.MobileNumber);
                        command.Parameters.AddWithValue("@AdharCardNumber", body.AdharCardNumber);
                        command.Parameters.AddWithValue("@BhamashahNo", body.BhamashahNo);
                        command.Parameters.AddWithValue("@PassportNo", body.PassportNo);
                        command.Parameters.AddWithValue("@CITSPassedYears", body.CITSPassedYears);
                        command.Parameters.AddWithValue("@DateOfJoiningGvernmentOfEmp", body.DateOfJoiningGvernmentOfEmp);
                        command.Parameters.AddWithValue("@FirstPostJoiningDateEmp", body.FirstPostJoiningDateEmp);
                        command.Parameters.AddWithValue("@JudicialCasePendingID", body.JudicialCasePendingID);
                        command.Parameters.AddWithValue("@SpecialAbilityID", body.SpecialAbilityID);
                        command.Parameters.AddWithValue("@DepartmentalEnquiryPendingID", body.DepartmentalEnquiryPendingID);
                        command.Parameters.AddWithValue("@PunishedInDepartmentalInquiryID", body.PunishedInDepartmentalInquiryID);
                        command.Parameters.AddWithValue("@DateOfPunishment", body.DateOfPunishment);
                        command.Parameters.AddWithValue("@DistrictYear", body.DistrictYear);
                        command.Parameters.AddWithValue("@DistrictCommak", body.DistrictCommak);
                        command.Parameters.AddWithValue("@DivisionLevelYear", body.DivisionLevelYear);
                        command.Parameters.AddWithValue("@DivisionLevelCommak", body.DivisionLevelCommak);
                        command.Parameters.AddWithValue("@StateYear", body.StateYear);
                        command.Parameters.AddWithValue("@StateCommak", body.StateCommak);
                        command.Parameters.AddWithValue("@isSeniorInstructor", body.isSeniorInstructor);
                        command.Parameters.AddWithValue("@isRenounced", body.isRenounced);
                        command.Parameters.AddWithValue("@isDepartmentalMixed", body.isDepartmentalMixed);
              
                        command.Parameters.AddWithValue("@jsonData", jsonData);
                        //command.Parameters.Add("@Return", SqlDbType.Int);
                        //command.Parameters["@Return"].Direction = ParameterDirection.Output;
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        //result = Convert.ToInt32(command.Parameters["@Return"].Value);
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


        public async Task<bool> ITI_GOVT_EM_ApproveRejectStaff(RequestUpdateStatus request)
        {
            _actionName = "LockandSubmit(StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITI_GOVT_EM_StaffApproveRejectStaff";
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

        public async Task<DataTable> ITIGovtEM_Govt_PersonnelDetailsInstitutionsAccordingBudget_Save(List<ITI_Govt_EM_SanctionedPostBasedInstituteModel> request)
        {
            _actionName = "ITIGovtEM_Govt_PersonnelDetailsInstitutionsAccordingBudget_Save(List<ITI_Govt_EM_SanctionedPostBasedInstituteModel> request)";
            return await Task.Run(async () =>
            {
                try
                {

                    var jsonData = JsonConvert.SerializeObject(request);
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_ITI_Govt_PersonnelDetailsInstitutionsAccordingBudget_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "PersonnelDetailsInstitutionsAccordingBudget_IU");
                        command.Parameters.AddWithValue("@DataJson", jsonData);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //dataTable = await command.FillAsync_DataTable();
                        // Execute the command

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

        public async Task<DataTable> ITIGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData(ITI_Govt_EM_ZonalOFFICERSSearchDataModel body)
        {
            _actionName = "ITIGovtEM_Govt_AdminT2Zonal_GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SanctionedPostInstitutePersonnelBudget";
                        command.Parameters.AddWithValue("@action", "GetData");
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
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


        public async Task<DataTable> ITIGovtEM_Govt_RoleOfficeMapping_GetAllData(ITI_Govt_EM_RoleOfficeMappingSearchDataModel body)
        {
            _actionName = "ITIGovtEM_Govt_RoleOfficeMapping_GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_RoleOfficeMapping";                       
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
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

        public async Task<PersonalDetailByUserIDModel> ITIGovtEM_ITI_Govt_Em_PersonalDetailByUserID(PersonalDetailByUserIDSearchModel body)
        {
            _actionName = "ITIGovtEM_ITI_Govt_Em_PersonalDetailByUserID(PersonalDetailByUserIDSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    var data = new PersonalDetailByUserIDModel();

                    if (body.Action== "StaffDetails")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_ITI_Govt_Em_PersonalDetailByUserID";
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
                                data.iTIGovtEMStaffPersonalDetails = CommonFuncationHelper.ConvertDataTable<ITIGovtEMStaff_PersonalDetailsModel>(dataSet.Tables[0]);
                            }
                            if (dataSet.Tables.Count > 0)
                            {
                                data.PromotionList = CommonFuncationHelper.ConvertDataTable<List<ITIGovtEMStaff_PromotionIsRenouncedModel>>(dataSet.Tables[1]);
                            }
                                
                        }
                    }
                    if (body.Action == "UserEducationalQualification")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_ITI_Govt_Em_PersonalDetailByUserID";
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
                                data.EducationalList = CommonFuncationHelper.ConvertDataTable<List<ITIGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel>>(dataSet.Tables[0]);
                            }
                        }
                    }
                    if (body.Action == "ServiceDetailsOfPersonnel")
                    {
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_ITI_Govt_Em_PersonalDetailByUserID";
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


        //public async Task<bool> ITIGovtEM_DeleteByIdStaffPromotionHistory(int ID, int ModifyBy)
        //{
        //    _actionName = "ITIGovtEM_DeleteByIdStaffPromotionHistory(ID ,ModifyBy)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.CommandText = "USP_ITI_Govt_EM_StaffDeleteByID";
        //                command.Parameters.AddWithValue("@ID", ID);
        //                command.Parameters.AddWithValue("@ModifyBy", ModifyBy);
        //                command.Parameters.AddWithValue("@Action", "DeleteStaffPromotionHistory");

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                result = await command.ExecuteNonQueryAsync();
        //            }
        //            if (result > 0)
        //                return true;
        //            else
        //                return false;
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



        public async Task<int> ITIGovtEM_DeleteByIdStaffPromotionHistory(ITIGovtEM_DeleteByIdStaffPromotionHistoryDelete body)
        {
            _actionName = "ITIGovtEM_DeleteByIdStaffPromotionHistory(ITIGovtEM_DeleteByIdStaffPromotionHistoryDelete body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EM_StaffDeleteByID";
                        command.Parameters.AddWithValue("@ID", body.StaffID);
                        command.Parameters.AddWithValue("@ModifyBy", body.UserID);
                        command.Parameters.AddWithValue("@Action", "DeleteStaffPromotionHistory");
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






        public async Task<DataTable> ITIGovtEM_ITI_Govt_EM_GetUserLevel(int ID)
        {
            _actionName = "ITIGovtEM_ITI_Govt_EM_GetUserLevel(int ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EM_GetUserLevel";
                        command.Parameters.AddWithValue("@UserID", ID);
                       
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


        //public async Task<bool> ITI_Govt_EM_EducationalQualificationDeleteByID(int ID, int ModifyBy)
        //{
        //    _actionName = "ITI_Govt_EM_EducationalQualificationDeleteByID(ID ,ModifyBy)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.CommandText = "USP_ITI_Govt_EM_EducationalQualificationDeleteByID";
        //                command.Parameters.AddWithValue("@ID", ID);
        //                command.Parameters.AddWithValue("@ModifyBy", ModifyBy);
        //                command.Parameters.AddWithValue("@Action", "ITI_Govt_EM_EducationalQualificationDeleteByID");

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                result = await command.ExecuteNonQueryAsync();
        //            }
        //            if (result > 0)
        //                return true;
        //            else
        //                return false;
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


        public async Task<int> ITI_Govt_EM_EducationalQualificationDeleteByID(ITIGovtEM_DeleteByIdStaffEducationDelete body)
        {
            _actionName = "ITI_Govt_EM_EducationalQualificationDeleteByID(ITIGovtEM_DeleteByIdStaffEducationDelete body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EM_EducationalQualificationDeleteByID";
                        command.Parameters.AddWithValue("@ID", body.StaffID);
                        command.Parameters.AddWithValue("@ModifyBy", body.UserID);
                        command.Parameters.AddWithValue("@Action", "ITI_Govt_EM_EducationalQualificationDeleteByID");
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



        //public async Task<bool> ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(int ID, int ModifyBy)
        //{
        //    _actionName = "ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(ID ,ModifyBy)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.CommandText = "USP_ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID";
        //                command.Parameters.AddWithValue("@ID", ID);
        //                command.Parameters.AddWithValue("@ModifyBy", ModifyBy);
        //                command.Parameters.AddWithValue("@Action", "ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID");

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                result = await command.ExecuteNonQueryAsync();
        //            }
        //            if (result > 0)
        //                return true;
        //            else
        //                return false;
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

        public async Task<int> ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(ITIGovtEM_DeleteByIdStaffServiceDelete body)
        {
            _actionName = "ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(ITIGovtEM_DeleteByIdStaffServiceDelete body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID";
                        command.Parameters.AddWithValue("@ID", body.StaffID);
                        command.Parameters.AddWithValue("@ModifyBy", body.UserID);
                        command.Parameters.AddWithValue("@Action", "ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID");
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

        public async Task<DataTable> GetITI_Govt_EM_GetUserProfileStatus(int ID)
        {
            _actionName = "GetITI_Govt_EM_GetUserProfileStatus(int ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EM_GetUserProfileStatus";
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
                        command.CommandText = "USP_ITI_Govt_EM_UpdateStaffProfileStatus";
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



        public async Task<ITI_Govt_EM_ZonalOFFICERSDataModel> ITIGovtEM_GetSSOID(int StaffId)
        {
            _actionName = "ITIGovtEM_GetSSOID(int StaffId)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_AdminT2Zonal_IU";
                        command.Parameters.AddWithValue("@action", "GetSSOID");
                        command.Parameters.AddWithValue("@StaffID", StaffId);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITI_Govt_EM_ZonalOFFICERSDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITI_Govt_EM_ZonalOFFICERSDataModel>(dataSet.Tables[0]);

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


        #region GetJoiningLetter
        public async Task<DataSet> GetJoiningLetter(JoiningLetterSearchModel model)
        {
            _actionName = "GetJoiningLetter(JoiningLetterSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_JoiningAndRelievingLetter";
                        command.Parameters.AddWithValue("@Action", "JoiningLetter");
                        command.Parameters.AddWithValue("@UserId", model.UserID);
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


        #region GetRelievingLetter
        public async Task<DataSet> GetRelievingLetter(RelievingLetterSearchModel model)
        {
            _actionName = "GetJRelievingLetter(RelievingLetterSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_JoiningAndRelievingLetter";
                        command.Parameters.AddWithValue("@Action", "RelievingLetter");
                        command.Parameters.AddWithValue("@UserId", model.UserID);
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



        #region
        public async Task<DataTable> GetITI_Govt_CheckDistrictNodalOffice(CheckDistrictNodalOfficeSearchModel model)
        {
            _actionName = "GetITI_Govt_CheckDistrictNodalOffice(CheckDistrictNodalOfficeSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_CheckDistrictNodalOffice";
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@LevelID", model.LevelID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
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
        //public async Task<bool> ITIGovtEM_OfficeDelete(int ID, int ModifyBy)
        //{
        //    _actionName = "ITIGovtEM_OfficeDelete(ID ,ModifyBy)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.CommandText = "USP_ITI_Govt_EMOfficeDelete";
        //                command.Parameters.AddWithValue("@UserID", ID);
        //                command.Parameters.AddWithValue("@ModifyBy", ModifyBy);
        //                command.Parameters.AddWithValue("@Action", "ITI_Govt_EMOfficeDelete");
        //                command.Parameters.Add("@Return", SqlDbType.Int);// out
        //                command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                result = await command.ExecuteNonQueryAsync();
        //                result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
        //            }
        //            if (result > 0)
        //                return true;
        //            else
        //                return false;
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


        public async Task<int> ITIGovtEM_OfficeDelete(ITIGovtEM_OfficeDeleteModel body)
        {
            _actionName = "ITIGovtEM_OfficeDelete(ITIGovtEM_OfficeDeleteModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Govt_EMOfficeDelete";
                        command.Parameters.AddWithValue("@Action", "ITI_Govt_EMOfficeDelete");
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

        #endregion


        #region
        public async Task<DataTable> GetITI_Govt_EM_UserProfileStatusHt(ITI_Govt_EM_UserRequestHistoryListSearchDataModel Model)
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
                        command.CommandText = "USP_ITI_Govt_EM_UserProfileStatusHt";
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

    }


}



