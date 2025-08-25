using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.ITIPrivateEstablish;
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
    public class ITIPrivateEstablishRepository : IITIPrivateEstablishRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIPrivateEstablishRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIPrivateEstablishRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetAllData(ITIPrivateEstablishSearchModel body)
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

                        if (body.DepartmentID == 1)
                        {
                            command.CommandText = "USP_GetITIPrivateEstablish";
                        }
                        else if (body.DepartmentID == 2)
                        {
                            command.CommandText = "USP_GetITIPrivateEstablish";
                        }
                        
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
        public async Task<bool> SaveData(ITIPrivateEstablishModel request)
        {
            _actionName = "SaveData(ITIPrivateEstablishModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ItiPrivateStaffMaster_IU";
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
        public async Task<int> SaveBasicData(ITIPrivateEstablish_AddStaffBasicDetailDataModel request)
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
                        command.CommandText = "USP_ITIPrivateEstablish_IU";
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
                        command.Parameters.AddWithValue("@EMTypeID", request.EMTypeID);
                        command.Parameters.AddWithValue("@multiHostelIDs", request.multiHostelIDs);
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

        public async Task<ITIPrivateEstablishModel> GetById(int PK_ID, int DepartmentID)
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
                        command.CommandText = "USP_ITI_PrivateEstablish_StaffMaster_Edit";
                        command.Parameters.AddWithValue("@StaffID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITIPrivateEstablishModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITIPrivateEstablishModel>(dataSet.Tables[0]);
                            data.EduQualificationDetailsModel = CommonFuncationHelper.ConvertDataTable<List<ITIPrivateEstablish_Staff_EduQualificationDetailsModel>>(dataSet.Tables[1]);
                        }
                        if (dataSet.Tables.Count > 2)
                        {
                            data.StaffSubjectListModel = CommonFuncationHelper.ConvertDataTable<List<ITIPrivateEstablish_StaffSubjectListModel>>(dataSet.Tables[2]);
                        }

                        if (dataSet.Tables.Count > 3)
                        {
                            data.StaffHostelListModel = CommonFuncationHelper.ConvertDataTable<List<ITIPrivateEstablish_StaffHostelListModel>>(dataSet.Tables[3]);
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

        public async Task<bool> LockandSubmit(ITIPrivateEstablishModel request)
        {
            _actionName = "LockandSubmit(ITIPrivateEstablishModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_GetITIPrivateEstablish";
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

        public async Task<bool> UnlockStaff(ITIPrivateEstablishModel request)
        {
            _actionName = "LockandSubmit(ITIPrivateEstablishModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_GetITIPrivateEstablish";
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


        public async Task<bool> ApproveStaff(ITIPrivateEstablishModel request)
        {
            _actionName = "LockandSubmit(ITIPrivateEstablishModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_GetITIPrivateEstablish";
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


        public async Task<DataTable> StaffLevelType(ITIPrivateEstablishSearchModel body)
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
                        command.CommandText = "USP_GetITIPrivateEstablish";
                        command.Parameters.AddWithValue("@action", "M_ITIPrivateEM_StaffLevelType"); // Assuming you are using the action filter
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
        public async Task<DataTable> StaffLevelChild(ITIPrivateEstablishSearchModel body)
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
                        command.CommandText = "USP_GetITIPrivateEstablish";
                        command.Parameters.AddWithValue("@action", "M_ITIPrivateEM_StaffLevelChild"); // Assuming you are using the action filter
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


        public async Task<bool> IsDownloadCertificate(ITIPrivateEstablishModel request)
        {
            _actionName = "IsDownloadCertificate(ITIPrivateEstablishModel request)";
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

        public async Task<int> ChangeWorkingInstitute(ITIPrivateEstablish_AddStaffBasicDetailDataModel request)
        {
            _actionName = "ChangeWorkingInstitute(AddStaffBasicDetailDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_GetITIPrivateEstablish";
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

        public async Task<bool> ChangeWorkingInstitute(ITIPrivateEstablishModel request)
        {
            _actionName = "ChangeWorkingInstitute(ITIPrivateEstablishModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_GetITIPrivateEstablish";
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

        public async Task<DataTable> GetCurrentWorkingInstitute_ByID(ITIPrivateEstablishSearchModel body)
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
    }


}



