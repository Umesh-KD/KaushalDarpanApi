
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CheckListModel;
using Kaushal_Darpan.Models.ITI_Apprenticeship;
using Kaushal_Darpan.Models.ITIAdminDashboard;
using Kaushal_Darpan.Models.ITICenterObserver;
using Kaushal_Darpan.Models.ITIFlyingSquad;
using Microsoft.Data.SqlClient;
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
    public class ITI_ApprenticeshipRepository : IITIApprenticeshipRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITI_ApprenticeshipRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITI_ApprenticeshipRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ITI_ApprenticeshipSearchModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@Action", "GetAllData");
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DeploymentStatus", body.DeploymentStatus);
                        command.Parameters.AddWithValue("@DeploymentDate", body.DeploymentDate);
                        command.Parameters.AddWithValue("@ApprenticeshipTeamName", body.ApprenticeshipTeamName);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@LevelId", body.LevelId);

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


        public async Task<DataTable> GetAllInspectedData(ITI_ApprenticeshipSearchModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_ApprenticeshipReport";
                        command.Parameters.AddWithValue("@Action", "GetAllData");
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DeploymentStatus", body.DeploymentStatus);
                        command.Parameters.AddWithValue("@DeploymentDate", body.DeploymentDate);
                        command.Parameters.AddWithValue("@ApprenticeshipTeamName", body.ApprenticeshipTeamName);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@LevelId", body.LevelId);

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

        public async Task<DataTable> GetITIApprenticeshipDropdown(ITI_ApprenticeshipDropdownModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship_Dropdowns";
                        command.Parameters.AddWithValue("@action", body.action);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@ManagementTypeID", body.ManagementTypeID);
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

        public async Task<int> SaveData(ITI_ApprenticeshipDataModel request)
        {
            _actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship_IU";
                        command.Parameters.AddWithValue("@ApprenticeshipTeamID", request.ApprenticeshipTeamID);
                        command.Parameters.AddWithValue("@ApprenticeshipTeamName", request.ApprenticeshipTeamName);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        //command.Parameters.AddWithValue("@TeamTypeID", request.TeamTypeID);
                        command.Parameters.AddWithValue("@DeploymentDateFrom", request.DeploymentDateFrom);
                        //command.Parameters.AddWithValue("@DeploymentDateTo", request.DeploymentDateTo);
                        command.Parameters.AddWithValue("@ApprenticeshipMemberDetails", JsonConvert.SerializeObject(request.ApprenticeshipMemberDetails));

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


        public async Task<int> SaveApprenticeshipDeploymentData(List<ApprenticeshipDeploymentDataModel> request)
        {
            _actionName = "SaveApprenticeshipDeploymentData(ApprenticeshipDeploymentDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ApprenticeshipDeployment_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
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

        public async Task<ITI_ApprenticeshipDataModel> GetById_Team(int ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@ApprenticeshipTeamID", ID);
                        command.Parameters.AddWithValue("@Action", "GetById_Team");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITI_ApprenticeshipDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITI_ApprenticeshipDataModel>(dataSet.Tables[0]);
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.DeploymentDateFrom = Convert.ToString(dataSet.Tables[1].Rows[0]["DeploymentDateFrom"]);
                                data.DeploymentDateTo = Convert.ToString(dataSet.Tables[1].Rows[0]["DeploymentDateTo"]);
                            }
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.ApprenticeshipMemberDetails = CommonFuncationHelper.ConvertDataTable<List<ApprenticeshipMemberDetailsDataModel>>(dataSet.Tables[1]);
                            }
                            if (dataSet.Tables[2].Rows.Count > 0)
                            {
                                data.ApprenticeshipDeploymentDetails = CommonFuncationHelper.ConvertDataTable<List<ApprenticeshipDeploymentDataModel>>(dataSet.Tables[2]);
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

        public async Task<DataTable> GetById_Deployment(int ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@ApprenticeshipTeamID", ID);
                        command.Parameters.AddWithValue("@Action", "GetById_Deployment");

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

        public async Task<DataTable> GetApprenticeshipDataByID_Status(ITI_ApprenticeshipSearchModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@Action", "GetApprenticeshipDataByID_Status");
                        command.Parameters.AddWithValue("@StaffID", body.StaffID);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@DeploymentStatus", body.DeploymentStatus);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
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

        public async Task<DataTable> GetAllData_GenerateOrder(ITI_ApprenticeshipSearchModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship_GetData";
                        command.Parameters.AddWithValue("@Action", "GetAllData_GenerateOrder");
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DeploymentStatus", body.DeploymentStatus);

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

        public async Task<DataSet> GenerateApprenticeshipDutyOrder(List<CODeploymentDataModel> model)
        {
            _actionName = "GenerateApprenticeshipDutyOrder(GenerateDutyOrder model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship_GenerateDutyOrder";
                        //command.Parameters.AddWithValue("@DeploymentID", model.DeploymentID);
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
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

        public async Task<Boolean> check_Engagement(ApprenticeshipMemberDetailsDataModel request)
        {
            //_actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    Boolean result = false;
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        //Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship_GetData";
                        command.Parameters.AddWithValue("@Action", "check_Engagement");
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@DeploymentDateFrom", request.DeploymentDateFrom);
                        command.Parameters.AddWithValue("@DeploymentDateTo", request.DeploymentDateTo);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();// out
                        if (dataTable != null && dataTable.Rows.Count > 0)
                        {
                            result = true;
                        }
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

        public async Task<int> SaveCheckSSODataModel(Apprenticeship_SaveCheckSSODataModel request)
        {
            //_actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship_GetData";
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@EmailID", request.EmailID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@DeploymentDateFrom", request.DeploymentDateFrom);
                        command.Parameters.AddWithValue("@DeploymentDateTo", request.DeploymentDateTo);
                        command.Parameters.AddWithValue("@Action", "SaveCheckSSODataModel");

                        // Add @Return as OUTPUT
                        var returnParam = new SqlParameter("@Return", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(returnParam);

                        _sqlQuery = command.GetSqlExecutableQuery(); // Optional: For logging/debugging

                        await command.ExecuteNonQueryAsync();

                        // Get output parameter value
                        result = Convert.ToInt32(returnParam.Value);

                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //result = await command.ExecuteNonQueryAsync();
                        //result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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

        public async Task<int> UpdateDeployment(int id)
        {
            //_actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@ApprenticeshipTeamID", id);
                        command.Parameters.AddWithValue("@Action", "UpdateDeployment");
                

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

        public async Task<DataSet> GenerateApprenticeshipDeploymentOrder(int id)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataset = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@Action", "GetApprenticeshipDeployment");
                        command.Parameters.AddWithValue("@ApprenticeshipTeamId", id);
         

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataset = await command.FillAsync();
                    }
                   
                    return dataset;
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

        public async Task<DataTable> GetITIApprenticeshipDashData(ApprenticeshipDeploymentDataModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@Action", "GetITIApprenticeshipDashData");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
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

        public async Task<DataTable> GetITIApprenticeshipMemeberTeamList(ApprenticeshipDeploymentDataModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@Action", "GetITIApprenticeshipMemeberTeamList");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
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

        public async Task<DataTable> GetITIApprenticeshipIndustryList(ApprenticeshipDeploymentDataModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@ApprenticeshipTeamID", model.ApprenticeshipTeamID);
                        command.Parameters.AddWithValue("@Action", "GetITIApprenticeshipIndustryList");
                        command.Parameters.AddWithValue("@AnswerStatus", model.AnswerStatus);
                        command.Parameters.AddWithValue("@UserID", model.UserID);


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

        public async Task<DataSet> GetITIApprenticeshipQuestionData(ApprenticeshipDeploymentDataModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataTable = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship";
                        command.Parameters.AddWithValue("@Action", "GetITIApprenticeshipQuestionData");
                        command.Parameters.AddWithValue("@ApprenticeshipTeamID", model.ApprenticeshipTeamID);
                        command.Parameters.AddWithValue("@DeploymentID", model.DeploymentID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync();
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

        public async Task<int> SaveApprenticeshipAnswersByIndustry(ITI_ApprenticeshipAnswerModel request)
        {
            _actionName = "SaveChecklistAnswers(ChecklistAnswerRequest request)";
            return await Task.Run(async () =>
            {
                //var rowJson_Main = JsonConvert.SerializeObject(request.MainData);
                //var rowJson_TradeWise = JsonConvert.SerializeObject(request.TrainersData);
                //var rowJson_Trainers = JsonConvert.SerializeObject(request.TradeWiseData);
                //var rowJson_Facility = JsonConvert.SerializeObject(request.FacilityData);

                //var param = new DynamicParameters();

                // 1. Serialize MainData directly
                string jsonMain = JsonConvert.SerializeObject(request.MainData);

                // 2. Flatten TradeWiseList from each entry in TradeWiseData
                var flatTradeWiseList = request.TradeWiseData?
                    .SelectMany(t => t.TradeWiseList)
                    .ToList();
                string jsonTradeWise = JsonConvert.SerializeObject(flatTradeWiseList);

                // 3. Flatten TrainersList
                var flatTrainersList = request.TrainersData?
                    .SelectMany(t => t.TrainersList)
                    .ToList();
                string jsonTrainers = JsonConvert.SerializeObject(flatTrainersList);

                // 4. Flatten FacilityList
                var flatFacilityList = request.FacilityData?
                    .SelectMany(t => t.FacilityList)
                    .ToList();
                string jsonFacility = JsonConvert.SerializeObject(flatFacilityList);

                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_SaveApprenticeshipAnswers_MergeBased";
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@JsonData", jsonData);
                        //command.Parameters.AddWithValue("@QuestionID", request.MainData[0].QuestionID);
                        //command.Parameters.AddWithValue("@TypeID", request.MainData[0].TypeID);
                        //command.Parameters.AddWithValue("@AnswerText", request.MainData[0].AnswerText);
                        //command.Parameters.AddWithValue("@Remarks", request.MainData[0].Remarks);
                        //command.Parameters.AddWithValue("@CreatedBy", request.MainData[0].CreatedBy);
                        //command.Parameters.AddWithValue("@ModifyBy", request.MainData[0].CreatedBy);
                        //command.Parameters.AddWithValue("@DeploymentID", request.MainData[0].DeploymentID);
                        //command.Parameters.AddWithValue("@ApprenticeshipTeamID", request.MainData[0].ApprenticeshipTeamID);


                        command.Parameters.Add("@rowJson_Main", SqlDbType.NVarChar).Value = jsonMain;
                        command.Parameters.Add("@rowJson_TradeWise", SqlDbType.NVarChar).Value = jsonTradeWise;
                        command.Parameters.Add("@rowJson_Trainers", SqlDbType.NVarChar).Value = jsonTrainers;
                        command.Parameters.Add("@rowJson_Facility", SqlDbType.NVarChar).Value = jsonFacility;

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



        public async Task<DataSet> GenerateCOAnsweredReport( int DeploymentID)
        {
            _actionName = "GenerateCOAnsweredReport(GenerateDutyOrder model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_ApprenticeshipReport";
                        command.Parameters.AddWithValue("@action", "_GetCheckList_AnswerCO");
                        //command.Parameters.AddWithValue("@ApprenticeshipTeamID", ApprenticeshipTeamID);
                        command.Parameters.AddWithValue("@DeploymentID", DeploymentID);
                        //command.Parameters.AddWithValue("@TypeID", 4);
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


        public async Task<DataTable> GetAllInspectedDataByID(ITI_ApprenticeshipSearchModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_ApprenticeshipReport";
                        command.Parameters.AddWithValue("@Action", "GetAllInspectedDataByID");
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DeploymentStatus", body.DeploymentStatus);
                        command.Parameters.AddWithValue("@DeploymentDate", body.DeploymentDate);
                        command.Parameters.AddWithValue("@ApprenticeshipTeamName", body.ApprenticeshipTeamName);
                        command.Parameters.AddWithValue("@ApprenticeshipTeamID", body.ApprenticeshipTeamID);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@LevelId", body.LevelId);

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

        public async Task<int> UpdateReport(string filename, int DeploymentID)
        {
            _actionName = "UpdateDutyOrder(List<GenerateAdmitCardModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_ApprenticeshipReport";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@Action", "UpdateReport");
                        command.Parameters.AddWithValue("@surveyfile", filename);
                        command.Parameters.AddWithValue("@DeploymentID", DeploymentID);
                        //command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        //command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        //retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
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


        public async Task<int> UpdateDutyOrder(int id)
        {
            _actionName = "UpdateDutyOrder(List<GenerateAdmitCardModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_Apprenticeship_UpdateDutyOrder";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        
                        command.Parameters.AddWithValue("@ApprenticeshipTeamID", id);
                
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


        public async Task<bool> UpdateAttendance(UpdateAttendance model)
        {
            _actionName = "UpdateAttendance(UpdateAttendance model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Apprenticeship_UpdateAttendance";
                        command.Parameters.AddWithValue("@IsPresent", model.IsPresent);
                        command.Parameters.AddWithValue("@Remark", model.Remark);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@COAttendanceID", model.COAttendanceID);
                        command.Parameters.AddWithValue("@latitude", model.latitude);
                        command.Parameters.AddWithValue("@longitude", model.longitude);
                        command.Parameters.AddWithValue("@photo", model.photo);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@TypeID", model.TypeID);
                        command.Parameters.AddWithValue("@action", "UpdateAttendance");
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


        public async Task<int> IsRequestApprenticeship(PostIsRequestCenterObserver model)
        {
            _actionName = "IsRequestCenterObserver(PostIsRequestCenterObserver model)";

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITI_ApprenticeshipRequest_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@OperationType", "UPDATE");
                        command.Parameters.AddWithValue("@DeploymentID", model.DeploymentID);
                        command.Parameters.AddWithValue("@Remark", model.Remark);
                        command.Parameters.AddWithValue("@IsRequest", model.IsRequest);

                        // Add the return parameter
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }


        public async Task<int> ApproveRequest(int DeployedID)
        {
            _actionName = "IsRequestFlyingSquad()";

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "[USP_ITI_ApprenticeshipRequest_IU]";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@OperationType", "UpdateRequestStatus");
                        command.Parameters.AddWithValue("@DeploymentID", DeployedID);
                        command.Parameters.AddWithValue("@IsRequest", 2);

                        // Add the return parameter
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }
    }
}