using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.GuestRoomManagementModel;
using Kaushal_Darpan.Models.ITI_Inspection;
using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.Test;
using Newtonsoft.Json;
using System.Data;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class TeacherHigherEducationApplicationRepository : ITeacherHigherEducationApplicationRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public TeacherHigherEducationApplicationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "TeacherHigherEducationApplicationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        #region enrolled promoted to verify
        public async Task<DataTable> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)
        {
            _actionName = "GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEnrolledStudent_Promoted";

                        command.Parameters.AddWithValue("@action", "_getEnrolledStudent_Promoted");
                        command.Parameters.AddWithValue("@StudentName", model.StudentName);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@ApplicationNo", model.ApplicationNo);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);

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

        public async Task<int> SaveTeacherHighEduApp(TeacherHigherEducationApplicationModel model)
        {
            _actionName = "SaveTeacherHighEduApp(TeacherHigherEducationApplicationModel model)";
            return await Task.Run(async () =>
            {
                try
                {

                    //var jsonData = JsonConvert.SerializeObject(request);
                    //DataTable dataTable = new DataTable();
                    int result = 0;

                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {

                        command.CommandText = "USP_THTE_ApplicationIU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Set parameters as per stored procedure signature
                        command.Parameters.AddWithValue("@THTEAppID", model.THTEAppID); // Assuming 0 for new insert
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TeacherName", model.TeacherName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DOB", string.IsNullOrEmpty(model.DOB) ? (object)DBNull.Value : model.DOB);
                        command.Parameters.AddWithValue("@JoiningDate", string.IsNullOrEmpty(model.JoiningDate) ? (object)DBNull.Value : model.JoiningDate);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID); // Set accordingly if you have this info in model
                        command.Parameters.AddWithValue("@AppliedCourse", model.AppliedCourse);
                        command.Parameters.AddWithValue("@AppliedInstitute", model.AppliedInstitute ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PHDStatus", model.PHDStatus);
                        command.Parameters.AddWithValue("@AppliedInstituteDistance", model.AppliedInstituteDistance);
                        command.Parameters.AddWithValue("@AppliedInstituteCourseCategory", model.AppliedInstituteCourseCategory);
                        command.Parameters.AddWithValue("@AppliedInstituteSubCategory", model.AppliedInstituteSubCategory);
                        command.Parameters.AddWithValue("@Remark", model.Remark ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ActiveStatus", 1);
                        command.Parameters.AddWithValue("@DeleteStatus", 0);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy); // Set if you have this info
                        command.Parameters.AddWithValue("@UpdatedBy", model.CreatedBy); // Set if you have this info
                        command.Parameters.AddWithValue("@SessionID", model.SessionID); // Set if you have this info
                        command.Parameters.AddWithValue("@IsQualificationRecorded", model.IsQualificationRecorded);
                        command.Parameters.AddWithValue("@CollegeDetailList", JsonConvert.SerializeObject(model.CollegeDetailList)); // Set if you have this info
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress); // Set if you have this info

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

        #endregion


        public async Task<DataTable> GetCategoryOfApplyCourseInstitute(THTE_DDL body)
        {
            _actionName = "GetCategoryOfApplyCourseInstitute()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HigherStudyPermissionCategoryDDl";
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


        public async Task<DataTable> THTE_GetStaffPersonalDetailByUserID(BTER_EM_GetPersonalDetailByUserID body)
        {
            _actionName = "BTER_EM_GetPersonalDetailByUserID(BTER_EM_GetPersonalDetailByUserID body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_GetPersonalDetailByUserID";
                        command.Parameters.AddWithValue("@Action", "GetDetailStaffIDAC");
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

        public async Task<DataTable> GetTHTE_ApplicationData(THTE_ApplicationSearchModel model)
        {
            _actionName = "GetTHTE_ApplicationData(THTE_ApplicationSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_ApplicationList";
                        command.Parameters.AddWithValue("@THTEAppID", model.THTEAppID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
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

        public async Task<TeacherHigherEducationApplicationModel> GetTHTE_ApplicationByID(THTE_ApplicationSearchModel body)
        {
            _actionName = "GetTHTE_ApplicationByID(THTE_ApplicationSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    var data = new TeacherHigherEducationApplicationModel();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_ApplicationByID";
                        command.Parameters.AddWithValue("@THTEAppID", body.THTEAppID);
                        command.Parameters.AddWithValue("@StaffID", body.StaffID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }

                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {
                                data = CommonFuncationHelper.ConvertDataTable<TeacherHigherEducationApplicationModel>(dataSet.Tables[0]);
                            }
                        if (dataSet.Tables.Count > 1)
                        {
                            data.CollegeDetailList = CommonFuncationHelper.ConvertDataTable<List<CollegeDetailList>>(dataSet.Tables[1]);
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

        public async Task<bool> DeleteTHTE_ApplicationByID(THTE_ApplicationSearchModel request)
        {
            _actionName = "DeleteTHTE_ApplicationByID(THTE_ApplicationSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandText = "USP_THTE_ApplicationHistoryDelete";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@THTEAppID", request.THTEAppID);
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



        public async Task<DataTable> GetAllAppliedCoursesDDL(THTE_DDL body)
        {
            _actionName = " GetAllAppliedCoursesDDL(THTE_DDL body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_GetAllAppliedCoursesDDL";
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

        public async Task<DataTable> GetAllInstitutionalsDDL(THTE_DDL body)
        {
            _actionName = "GetAllInstitutionalsDDL(THTE_DDL body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_GetAllInstitutionalsDDL";
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

        public async Task<DataTable> THTE_GrtApplicationStatusHistory(THTE_ApplicationSearchModel body)
        {
            _actionName = "THTE_GrtApplicationStatusHistory(THTE_ApplicationSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_ApplicationStatusHistory";
                        command.Parameters.AddWithValue("@THTEAppID", body.THTEAppID);
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



        public async Task<int> CommitteeSaveData(CommitteeDataModel request)
        {
            _actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_Committee_IU";

                        command.Parameters.AddWithValue("@InspectionTeamID", request.InspectionTeamID);
                        command.Parameters.AddWithValue("@InspectionTeamName", request.InspectionTeamName);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@TeamTypeID", request.TeamTypeID);
                        command.Parameters.AddWithValue("@DeploymentDateFrom", request.DeploymentDateFrom);
                        command.Parameters.AddWithValue("@DeploymentDateTo", request.DeploymentDateTo);
                        command.Parameters.AddWithValue("@InspectionMemberDetails", JsonConvert.SerializeObject(request.InspectionMemberDetails));
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteId);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);

                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
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


        public async Task<DataTable> GetCommitteeAllData(CommitteeSearchModel body)
        {
            _actionName = "GetCommitteeAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_CommitteeList";
                        command.Parameters.AddWithValue("@Action", "GetAllData");
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DeploymentStatus", body.DeploymentStatus);
                        command.Parameters.AddWithValue("@DeploymentDate", body.DeploymentDate);
                        command.Parameters.AddWithValue("@InspectionTeamName", body.InspectionTeamName);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@LevelId", body.LevelId);
                        command.Parameters.AddWithValue("@InstituteId", body.InstituteId);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);

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


        public async Task<CommitteeDataModel> GetCommitteeById_Team(int ID, int RoleID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if(RoleID == 17)
                        {
                            command.CommandText = "USP_THTE_CommitteeList_DTE";
                        }
                        else
                        {
                            command.CommandText = "USP_THTE_CommitteeList";
                        }
                        command.Parameters.AddWithValue("@InspectionTeamID", ID);
                        command.Parameters.AddWithValue("@Action", "GetById_Team");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new CommitteeDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<CommitteeDataModel>(dataSet.Tables[0]);
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.DeploymentDateFrom = Convert.ToString(dataSet.Tables[1].Rows[0]["DeploymentDateFrom"]);
                                data.DeploymentDateTo = Convert.ToString(dataSet.Tables[1].Rows[0]["DeploymentDateTo"]);
                            }
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.InspectionMemberDetails = CommonFuncationHelper.ConvertDataTable<List<CommitteeMemberDetailsDataModel>>(dataSet.Tables[1]);
                            }
                            if (dataSet.Tables[2].Rows.Count > 0)
                            {
                                data.InspectionDeploymentDetails = CommonFuncationHelper.ConvertDataTable<List<CommitteeDeploymentDataModel>>(dataSet.Tables[2]);
                            }
                        }
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

        public async Task<DataTable> GetCommitteeDDL(THTE_DDL body)
        {
            _actionName = "GetCommitteeDDL(THTE_DDL body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_CommitteeDDL";
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);

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

        public async Task<DataTable> Bter_CommitteeStaffCheckSSOID(CommitteeStaffSSOIDSearchModel body)
        {

            _actionName = "GuestStaffProfile()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_CommitteeStaffCheckSSOID";
                        command.Parameters.AddWithValue("@DepartmentId", body.DepartmentID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
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



        public async Task<DataTable> THTE_GrtApplyInstituteList(THTE_ApplicationSearchModel body)
        {
            _actionName = "THTE_GrtApplyInstituteList(THTE_ApplicationSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_ApplyInstituteList";
           
                        command.Parameters.AddWithValue("@THTEAppID", body.THTEAppID);
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
        public async Task<int> UpdateInstitutestatus(List<CollegeDetailList> entity)
        {
            _actionName = "UpdateSaveData(List<ITITheoryMarksModel> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_UpdateApplyCollegeDetails";
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

        public async Task<DataTable> THTE_GetInstituteCommitteeList(InstituteCommitteListDataModel body)
        {
            _actionName = "THTE_GetInstituteCommitteeList(InstituteCommitteListDataModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_InstituteCommitteeList_GetData";

                        command.Parameters.AddWithValue("@action", body.action);
                        command.Parameters.AddWithValue("@CommitteeID", body.CommitteeID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
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

        public async Task<DataTable> THTE_GetDTECommitteeList(CommitteeSearchModel body)
        {
            _actionName = "THTE_GetDTECommitteeList(CommitteeSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_CommitteeList_DTE";
                        command.Parameters.AddWithValue("@Action", "GetAllData");

                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@InspectionTeamName", body.InspectionTeamName);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@LevelId", body.LevelId);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);

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

        public async Task<int> THTE_DTECommitteeSaveData(DTECommitteeDataModel request)
        {
            _actionName = "THTE_DTECommitteeSaveData(DTECommitteeDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_Committee_IU_DTE";
                        command.Parameters.AddWithValue("@DTECommitteeID", request.DTECommitteeID);
                        command.Parameters.AddWithValue("@DTECommitteeName", request.DTECommitteeName);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@DTECommitteeMemberDetails", JsonConvert.SerializeObject(request.DTECommitteeMemberDetails));
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int); // out
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

        public async Task<DTECommitteeDataModel> THTE_GetDTECommitteeById(int ID, int RoleID)
        {
            _actionName = "THTE_GetDTECommitteeById(int ID, int RoleID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_CommitteeList_DTE";
                        command.Parameters.AddWithValue("@DTECommitteeID", ID);
                        command.Parameters.AddWithValue("@Action", "GetById_Team");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new DTECommitteeDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<DTECommitteeDataModel>(dataSet.Tables[0]);
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.DTECommitteeMemberDetails = CommonFuncationHelper.ConvertDataTable<List<DTECommitteeMemberDetailsDataModel>>(dataSet.Tables[1]);
                            }
                        }
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

        public async Task<DataTable> THTE_GetDTECommitteeDDL(CommitteeSearchModel body)
        {
            _actionName = "THTE_GetDTECommitteeDDL(CommitteeSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_CommitteeList_DTE";
                        command.Parameters.AddWithValue("@Action", "DTECommitteeDDL");

                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@InspectionTeamName", body.InspectionTeamName);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@LevelId", body.LevelId);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);

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

        public async Task<int> SaveDTERecommendationInstitutes_THTE(List<CollegeDetailList> entity)
        {
            _actionName = "SaveDTERecommendationInstitutes_THTE(List<ITITheoryMarksModel> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_THTE_UpdateApplyCollegeDetails_DTE";
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

        public async Task<bool> CommitteeStatusChange_THTE(CommitteeStatusChangeDataModel request)
        {
            _actionName = "CommitteeStatusChange_THTE(CommitteeStatusChangeDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_CommitteeList_DTE";
                        command.Parameters.AddWithValue("@Action", "CommitteeStatusChange");

                        command.Parameters.AddWithValue("@CommitteeID", request.CommitteeID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@IsActive", request.IsActive);
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
    }
}


