using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.RoleMaster;
using Kaushal_Darpan.Models.SSOUserDetails;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.UserMaster;
using System.Data;
using Kaushal_Darpan.Models.RoleMaster;
using Kaushal_Darpan.Models;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class SSORepository : ISSORepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public SSORepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "SSORepository";
        }
        public async Task<SSOUserDetailsModel> GetSSOUserDetails(string SearchRecordID)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SSOProfile";
                        command.Parameters.AddWithValue("@SearchRecordID", SearchRecordID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    // class
                    var data = new SSOUserDetailsModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<SSOUserDetailsModel>(dataTable);
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
        public async Task<SSOUserDetailsModel> Login(string SSOID, string Password)
        {
            _actionName = "Login(string SSOID, string Password)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SSOLogin";
                        command.Parameters.AddWithValue("@SSOID", SSOID);
                        command.Parameters.AddWithValue("@Password", Password);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new SSOUserDetailsModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<SSOUserDetailsModel>(dataTable);
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





        public async Task<ITICollegeSSoMAP> ItiCollegeMap(string CollegeCode, string Password)
        {
            _actionName = "Login(string SSOID, string Password)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICollegeMap";
                        command.Parameters.AddWithValue("@CollegeCode", CollegeCode);
                        command.Parameters.AddWithValue("@Password", Password);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ITICollegeSSoMAP();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ITICollegeSSoMAP>(dataTable);
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

        public async Task<BTERCollegeSSoMAP> BTERCollegeMap(string CollegeCode, string Password)
        {
            _actionName = "Login(string SSOID, string Password)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERCollegeMap";
                        command.Parameters.AddWithValue("@CollegeCode", CollegeCode);
                        command.Parameters.AddWithValue("@Password", Password);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new BTERCollegeSSoMAP();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<BTERCollegeSSoMAP>(dataTable);
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


        //public async Task<SSOUserDetailsModel> Login(string SSOID, string Password)
        //{
        //    _actionName = "Login(string SSOID, string Password)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();

        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_SSOLogin";
        //                command.CommandTimeout = 600;
        //                command.Parameters.AddWithValue("@SSOID", SSOID);
        //                command.Parameters.AddWithValue("@Password", Password);

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataTable = await command.FillAsync_DataTable();
        //            }

        //            var data = new SSOUserDetailsModel();

        //            // Check if any data is returned (if LoginStatus is returned, the query is successful)
        //            if (dataTable != null && dataTable.Rows.Count > 0)
        //            {
        //                // Convert the dataTable to SSOUserDetailsModel
        //                data = CommonFuncationHelper.ConvertDataTable<SSOUserDetailsModel>(dataTable);

        //                // Handle LoginStatus (Login Success, Invalid Password, or Not Exists)
        //                if (data.LoginStatus == "Login Success")
        //                {
        //                    // Map all the necessary fields from the stored procedure results to your model
        //                    data.LoginStatus = "Login Success"; // Login is successful
        //                }
        //                else if (data.LoginStatus == "Invalid Password")
        //                {
        //                    data.LoginStatus = "Invalid Password"; // Password is incorrect
        //                }
        //                else if (data.LoginStatus == "Not Exists")
        //                {
        //                    data.LoginStatus = "Not Exists"; // SSOID does not exist
        //                }
        //            }
        //            else
        //            {
        //                // If no data returned, consider this as failed login attempt
        //                data.LoginStatus = "Invalid SSOID or Password"; // Invalid login
        //            }

        //            return data;
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


        public async Task<bool> SaveData(UserRequestModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveData(UserRequestModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_UserRequest_IU";
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@AadhaarID", request.AadhaarID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@DivisionID", request.DivisionID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@UserStatus", request.UserStatus);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
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
        public async Task<DataTable> GetUserRoleList(RoleListRequestModel request)
        {
            _actionName = "GetUserRoleList(RoleListRequestModel request)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetUserRole";
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@IsWeb", request.IsWeb);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", request.Eng_NonEng);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<DataTable> GetAcedmicYearList(int RoleID=0, int DepartmentID = 0 , int SessionTypeID =0)
        {
            _actionName = "GetUserRoleList(string SSOID, bool IsWeb)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetlstAcedmicYear";
                        command.Parameters.AddWithValue("@RoleID", RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@SessionTypeID", SessionTypeID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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
        public async Task<string> AddSSOUserProfileDetails(SSO_UserProfileDetailModel model)
        {
            _actionName = "AddSSOUserProfileDetails(SSO_UserProfileDetailModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    string result = string.Empty;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_M_AddSSOProfile";

                        command.Parameters.AddWithValue("@action", "_addssouserprofile");
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@AadhaarID", model.aadhaarId);
                        command.Parameters.AddWithValue("@BhamashahID", model.bhamashahId);
                        command.Parameters.AddWithValue("@BhamashahMemberID", model.bhamashahMemberId);
                        command.Parameters.AddWithValue("@DisplayName", model.displayName);
                        command.Parameters.AddWithValue("@DateOfBirth", model.dateOfBirth);
                        command.Parameters.AddWithValue("@Gender", model.gender);
                        command.Parameters.AddWithValue("@MobileNo", model.mobile);
                        command.Parameters.AddWithValue("@TelephoneNumber", model.telephoneNumber);
                        command.Parameters.AddWithValue("@IpPhone", model.ipPhone);
                        command.Parameters.AddWithValue("@MailPersonal", model.mailPersonal);
                        command.Parameters.AddWithValue("@PostalAddress", model.postalAddress);
                        command.Parameters.AddWithValue("@PostalCode", model.postalCode);
                        command.Parameters.AddWithValue("@City", model.l);
                        command.Parameters.AddWithValue("@State", model.st);
                        command.Parameters.AddWithValue("@Photo", model.jpegPhoto);
                        command.Parameters.AddWithValue("@Designation", model.designation);
                        command.Parameters.AddWithValue("@Department", model.department);
                        command.Parameters.AddWithValue("@DepartmentId", model.departmentId);
                        command.Parameters.AddWithValue("@MailOfficial", model.mailOfficial);
                        command.Parameters.AddWithValue("@EmployeeNumber", model.employeeNumber);
                        command.Parameters.AddWithValue("@FirstName", model.firstName);
                        command.Parameters.AddWithValue("@LastName", model.lastName);
                        command.Parameters.AddWithValue("@SldSSOIDs", ((model.oldSSOIDs?.Length ?? 0) > 0 ? model.oldSSOIDs[0] : string.Empty));
                        command.Parameters.AddWithValue("@JanaadhaarId", model.janaadhaarId);
                        command.Parameters.AddWithValue("@JanaadhaarMemberId", model.janaadhaarMemberId);
                        command.Parameters.AddWithValue("@UserType", model.userType);
                        command.Parameters.AddWithValue("@Mfa", model.mfa);
                        //new for emitra
                         command.Parameters.AddWithValue("@KIOSKCODE", model.KIOSKCODE);
                         command.Parameters.AddWithValue("@IsKiosk", model.IsKiosk);
                         command.Parameters.AddWithValue("@SERVICEID", model.SERVICEID);
                         command.Parameters.AddWithValue("@EmitraDepartmentID", model.EmitraDepartmentID);
                         command.Parameters.AddWithValue("@SSoToken", model.SSoToken);

                        command.Parameters.Add("@SearchRecordID_out", SqlDbType.NVarChar, -1);// out
                        command.Parameters["@SearchRecordID_out"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        var r = await command.ExecuteNonQueryAsync();

                        result = Convert.ToString(command.Parameters["@SearchRecordID_out"].Value);// out
                    }
                    return (result ?? string.Empty);

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

        #region UserReqList 
        public async Task<DataTable> GetUserRequestList(UserSearchModel model)
        {
            _actionName = "GetUserRequestList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_UserRequestList";
                        command.Parameters.AddWithValue("@UserStatus", model.UserStatus);

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


        #region "UpdateSSO Details"
        //public async Task<bool> UpdateStudentUserType(UpdateStudentDetailsModel request)
        //{
        //    return await Task.Run(async () =>
        //    {
        //        _actionName = "UpdateCitizenSSO(UserRequestModel request)";
        //        try
        //        {
        //            int result = 0;
        //            using (var command = _dbContext.CreateCommand(true))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_UpdateStudentUserType";
        //                command.Parameters.AddWithValue("@SSOID", request.SSOID);
        //                command.Parameters.AddWithValue("@ProfileID", request.ProfileID);
        //                command.Parameters.AddWithValue("@UserID", request.UserID);
        //                command.Parameters.Add("@Retval", SqlDbType.NVarChar, -1);// out
        //                command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

        //                _sqlQuery = command.GetSqlExecutableQuery();// sql query
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




        public async Task<int> UpdateStudentUserType(UpdateStudentDetailsModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UpdateStudentUserType(UpdateStudentDetailsModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_UpdateStudentUserType";
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@ProfileID", request.ProfileID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
                        await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
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


        public async Task<SSOUserDetailsModel> StudentLogin(string SSOID)
        {
            _actionName = "Login(string SSOID, string Password)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SSOStudentLogin";
                        command.Parameters.AddWithValue("@SSOID", SSOID);
                        //command.Parameters.AddWithValue("@Password", Password);


                        //command.Parameters.Add("@SearchRecordID_out", SqlDbType.NVarChar, -1);// out
                        //command.Parameters["@SearchRecordID_out"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new SSOUserDetailsModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<SSOUserDetailsModel>(dataTable);
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



        public async Task<SSOUserDetailsModel> MobileLogin(string SSOID,int CourseType)
        {
            _actionName = "Login(string SSOID, string Password)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DTE_MobileLogin";
                        command.Parameters.AddWithValue("@SSOID", SSOID);
                        command.Parameters.AddWithValue("@CourseType", CourseType);
                        //command.Parameters.AddWithValue("@Password", Password);


                        //command.Parameters.Add("@SearchRecordID_out", SqlDbType.NVarChar, -1);// out
                        //command.Parameters["@SearchRecordID_out"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new SSOUserDetailsModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<SSOUserDetailsModel>(dataTable);
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


        public async Task<int> CreateCollegePrincipal( CreatePrincipalModel Model)
        {
            _actionName = "CreateCollegePrincipal(CreateCollegePrincipal request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveITICollegePrincipal";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CollegeID", Model.CollegeID);
                        command.Parameters.AddWithValue("@SSOID", Model.SSOID);
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


        public async Task<int> CreateBTERCollegePrincipal(CreatePrincipalModel Model)
        {
            _actionName = "CreateBTERCollegePrincipal(CreateCollegePrincipal request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveBTERCollegePrincipal";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CollegeID", Model.CollegeID);
                        command.Parameters.AddWithValue("@SSOID", Model.SSOID);
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



        public async Task<DataTable> GetAcedmicYearListbyTypeID(RequestBaseModel model)
        {
            _actionName = "GetUserRoleList(string SSOID, bool IsWeb)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetlstAcedmicYearByType";
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@SessionTypeID", model.SessionTypeID);
                        command.Parameters.AddWithValue("@CourseType", model.Eng_NonEng);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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
