using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CheckListModel;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.ITI_Inspection;
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
    public class ITI_InspectionRepository: I_ITI_InspectionRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITI_InspectionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITI_InspectionRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ITI_InspectionSearchModel body)
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
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@Action", "GetAllData");
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DeploymentStatus", body.DeploymentStatus);
                        command.Parameters.AddWithValue("@DeploymentDate", body.DeploymentDate);
                        command.Parameters.AddWithValue("@InspectionTeamName", body.InspectionTeamName);
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


        public async Task<DataTable> GetAllInspectedData(ITI_InspectionSearchModel body)
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
                        command.CommandText = "USP_ITI_InspectionReport";
                        command.Parameters.AddWithValue("@Action", "GetAllData");
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DeploymentStatus", body.DeploymentStatus);
                        command.Parameters.AddWithValue("@DeploymentDate", body.DeploymentDate);
                        command.Parameters.AddWithValue("@InspectionTeamName", body.InspectionTeamName);
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

        public async Task<DataTable> GetITIInspectionDropdown(ITI_InspectionDropdownModel body)
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
                        command.CommandText = "USP_ITI_Inspection_Dropdowns";
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

        public async Task<int> SaveData(ITI_InspectionDataModel request)
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
                        command.CommandText = "USP_ITI_Inspection_IU";
                        command.Parameters.AddWithValue("@InspectionTeamID", request.InspectionTeamID);
                        command.Parameters.AddWithValue("@InspectionTeamName", request.InspectionTeamName);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@TeamTypeID", request.TeamTypeID);
                        command.Parameters.AddWithValue("@DeploymentDateFrom", request.DeploymentDateFrom);
                        command.Parameters.AddWithValue("@DeploymentDateTo", request.DeploymentDateTo);
                        command.Parameters.AddWithValue("@InspectionMemberDetails", JsonConvert.SerializeObject(request.InspectionMemberDetails));

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


        public async Task<int> SaveInspectionDeploymentData(List<InspectionDeploymentDataModel> request)
        {
            _actionName = "SaveInspectionDeploymentData(InspectionDeploymentDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_InspectionDeployment_IU";
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

        public async Task<ITI_InspectionDataModel> GetById_Team(int ID)
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
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@InspectionTeamID", ID);
                        command.Parameters.AddWithValue("@Action", "GetById_Team");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITI_InspectionDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITI_InspectionDataModel>(dataSet.Tables[0]);
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.DeploymentDateFrom = Convert.ToString(dataSet.Tables[1].Rows[0]["DeploymentDateFrom"]);
                                data.DeploymentDateTo = Convert.ToString(dataSet.Tables[1].Rows[0]["DeploymentDateTo"]);
                            }
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.InspectionMemberDetails = CommonFuncationHelper.ConvertDataTable<List<InspectionMemberDetailsDataModel>>(dataSet.Tables[1]);
                            }
                            if (dataSet.Tables[2].Rows.Count > 0)
                            {
                                data.InspectionDeploymentDetails = CommonFuncationHelper.ConvertDataTable<List<InspectionDeploymentDataModel>>(dataSet.Tables[2]);
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
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@InspectionTeamID", ID);
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

        public async Task<DataTable> GetInspectionDataByID_Status(ITI_InspectionSearchModel body)
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
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@Action", "GetInspectionDataByID_Status");
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

        public async Task<DataTable> GetAllData_GenerateOrder(ITI_InspectionSearchModel body)
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
                        command.CommandText = "USP_ITI_Inspection_GetData";
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

        public async Task<DataSet> GenerateInspectionDutyOrder(List<CODeploymentDataModel> model)
        {
            _actionName = "GenerateInspectionDutyOrder(GenerateDutyOrder model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Inspection_GenerateDutyOrder";
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

        public async Task<Boolean> check_Engagement(InspectionMemberDetailsDataModel request)
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
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Inspection_GetData";
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

        public async Task<int> SaveCheckSSODataModel(SaveCheckSSODataModel request)
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
                        command.CommandText = "USP_ITI_Inspection_GetData";
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
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@InspectionTeamID", id);
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

        public async Task<DataSet> GenerateInspectionDeploymentOrder(int id)
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
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@Action", "GetInspectionDeployment");
                        command.Parameters.AddWithValue("@InspectionTeamId", id);
         

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

        public async Task<DataTable> GetITIInspectionDashData(InspectionDeploymentDataModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@Action", "GetITIInspectionDashData");
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

        public async Task<DataTable> GetITIInspectionMemeberTeamList(InspectionDeploymentDataModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@Action", "GetITIInspectionMemeberTeamList");
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

        public async Task<DataTable> GetITIInspectionInstituteList(InspectionDeploymentDataModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@InspectionTeamID", model.InspectionTeamID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@Action", "GetITIInspectionInstituteList");
                        command.Parameters.AddWithValue("@AnswerStatus", model.AnswerStatus);


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

        public async Task<DataTable> GetITIInspectionQuestionData(InspectionDeploymentDataModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Inspection";
                        command.Parameters.AddWithValue("@Action", "GetITIInspectionQuestionData");
                        command.Parameters.AddWithValue("@InspectionTeamID", model.InspectionTeamID);
                        command.Parameters.AddWithValue("@DeploymentID", model.DeploymentID);
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

        public async Task<int> SaveInspectionAnswersByInstitute(ITI_InspectionAnswerModel request)
        {
            _actionName = "SaveChecklistAnswers(ChecklistAnswerRequest request)";
            return await Task.Run(async () =>
            {
                var jsonData = JsonConvert.SerializeObject(request);
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "trn_SaveInspection_Answers";
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@JsonData", jsonData);
                        command.Parameters.Add("@JsonData", SqlDbType.NVarChar).Value = jsonData;
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



        public async Task<DataSet> GenerateCOAnsweredReport(int id)
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
                        command.CommandText = "USP_ITI_InspectionReport";
                        command.Parameters.AddWithValue("@action", "_GetCheckList_AnswerCO");
                        command.Parameters.AddWithValue("@DeploymentID", id);
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


        public async Task<int> UpdateDutyOrder(CODeploymentDataModel model)
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
                        command.CommandText = "USP_ITI_Inspection_UpdateDutyOrder";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@DeploymentID", model.DeploymentID);
                        command.Parameters.AddWithValue("@InspectionTeamID", model.InspectionTeamID);
                        command.Parameters.AddWithValue("@DutyOrder", model.DutyOrder);
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
                        command.CommandText = "USP_ITI_Inspection_UpdateAttendance";
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
       

        public async Task<int> IsRequestInspection(PostIsRequestCenterObserver model)
        {
            _actionName = "IsRequestCenterObserver(PostIsRequestCenterObserver model)";

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITI_InspectionRequest_IU";
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
                        command.CommandText = "[USP_ITI_InspectionRequest_IU]";
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

        public async Task<List<CommonDDLModel>> GetDistrictMaster(ITI_InspectionSearchModel body)
        {
            _actionName = "GetDistrictMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Inspection";

                        command.Parameters.AddWithValue("@Action", "GetDistrictMaster");
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@LevelId", body.LevelId);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    List<CommonDDLModel> dataModels = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        dataModels = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return dataModels;
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