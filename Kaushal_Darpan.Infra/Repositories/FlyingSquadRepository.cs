using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.FlyingSquad;
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
    public class FlyingSquadRepository: IFlyingSquadRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public FlyingSquadRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "FlyingSquadRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetFlyingSquad(GetFlyingSquadModal model)
        {
            _actionName = "GetFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetFlyingSquad";
                        command.Parameters.AddWithValue("@TeamID", model.TeamID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);

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

        public async Task<int> PostFlyingSquad(PostFlyingSquadModal model)
        {
            _actionName = "PostFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEditDelete_FlyingSquad";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
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
        
        public async Task<int> PostTeamFlyingSquadForm(PostTeamFlyingSquadModal model)
        {
            _actionName = "PostTeamFlyingSquadForm()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_TeamFlyingSquad_IU";
                        command.CommandType = CommandType.StoredProcedure;                        
                        command.Parameters.AddWithValue("@OperationType", model.OperationType);
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@TeamName", model.TeamName);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@ActiveStatus", model.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", model.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
                       
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

        public async Task<DataTable> GetTeamFlyingSquad(GetTeamMemberFlyingSquadModal model)
        {
            _actionName = "GetTeamFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_TeamFlyingSquad_GET";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        //command.Parameters.AddWithValue("@TeamID", model.TeamID);

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

        public async Task<int> PostTeamDeploymentFlyingSquadForm(PostTeamFlyingSquadModal model)
        {
            _actionName = "PostTeamDeploymentFlyingSquadForm()";

            return await Task.Run(async () =>
            {
                try
                {
                    int finalResult = 0;

                    // Create a list of shifts to loop through
                    var shifts = new List<ShiftA>();
                    if (model.ShiftA != null) shifts.Add(model.ShiftA);
                    if (model.ShiftB != null) shifts.Add(model.ShiftB);

                    foreach (var shift in shifts)
                    {
                        using (var command = _dbContext.CreateCommand(true))
                        {
                            command.CommandText = "USP_FlyingSquadDeployment_IU";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@OperationType", model.OperationType);
                            command.Parameters.AddWithValue("@TeamID", model.TeamID);
                            command.Parameters.AddWithValue("@DistrictID", shift.DistrictID);
                            command.Parameters.AddWithValue("@InstituteID", shift.InstituteID);
                            command.Parameters.AddWithValue("@ShiftID", shift.ShiftID);
                            command.Parameters.AddWithValue("@DeploymentDate", (object?)shift.DeploymentDate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                            command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                            command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                            command.Parameters.AddWithValue("@ActiveStatus", model.ActiveStatus);
                            command.Parameters.AddWithValue("@DeleteStatus", model.DeleteStatus);
                            command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                            command.Parameters.AddWithValue("@Selected", model.Selected);

                            // Add the return parameter
                            command.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.Output;

                            _sqlQuery = command.GetSqlExecutableQuery();

                            // Execute
                            await command.ExecuteNonQueryAsync();
                            finalResult = Convert.ToInt32(command.Parameters["@Return"].Value);
                        }
                    }

                    return finalResult; // Return the last insert result, or customize as needed
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
        public async Task<int> IsRequestFlyingSquad(PostIsRequestFlyingSquadModal model)
        {
            _actionName = "IsRequestFlyingSquad()";

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_FlyingSquadIsRequest_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@OperationType", "UPDATE");                        
                        command.Parameters.AddWithValue("@DeploymentID", model.DeploymentID);
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

        public async Task<int> IsRequestHistoryFlyingSquad(PostIsRequestFlyingSquadModal model)
        {
            _actionName = "IsRequestFlyingSquad()";

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_FlyingSquad_History";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
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

        public async Task<int> UpdateFlyingSquad_Attendance(UpdateFlyingSquadAttendance model)
        {
            _actionName = "UpdateFlyingSquad_Attendance()";

            return await Task.Run(async () =>
            {
                try
                {
                    int finalResult = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_UpdateFlyingSquad_Attendance";
                        command.CommandType = CommandType.StoredProcedure;

                        //command.Parameters.AddWithValue("@OperationType", model.OperationType);
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@IsPresent", model.IsPresent);
                        command.Parameters.AddWithValue("@Remark", model.Remark);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@photo", model.photo);
                        command.Parameters.AddWithValue("@latitude", model.latitude);
                        command.Parameters.AddWithValue("@longitude", model.longitude);
                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute
                        await command.ExecuteNonQueryAsync();
                        finalResult = Convert.ToInt32(command.Parameters["@Return"].Value);
                    }

                    return finalResult; // Return the last insert result, or customize as needed
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
        
        public async Task<int> PostTeamDeploymentFlyingSquad(List<PostTeamFlyingSquadUpdateModal> itam)
        {
            _actionName = "PostTeamDeploymentFlyingSquad()";

            return await Task.Run(async () =>
            {
                try
                {
                    int finalResult = 0;
                    foreach(var model in itam)
                    {
                      
                            using (var command = _dbContext.CreateCommand(true))
                            {
                                command.CommandText = "USP_FlyingSquadDeployment_IU";
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@OperationType", model.OperationType);
                                command.Parameters.AddWithValue("@ID", model.ID);
                                command.Parameters.AddWithValue("@Status", model.Status);
                                command.Parameters.AddWithValue("@TeamID", model.TeamID);
                            if (model.Status == 3)
                            {
                                command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
                            }

                                // Add the return parameter
                                command.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.Output;

                                _sqlQuery = command.GetSqlExecutableQuery();

                                // Execute
                                await command.ExecuteNonQueryAsync();
                                finalResult = Convert.ToInt32(command.Parameters["@Return"].Value);
                            }
                      
                    }
                    

                    return finalResult; // Return the last insert result, or customize as needed
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


        public async Task<DataTable> GetTeamDeploymentFlyingSquad(GetTeamFlyingSquadModal model)
        {
            _actionName = "GetTeamDeploymentFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_FlyingSquadDeployment_GET";
                        command.Parameters.AddWithValue("@TeamID", model.TeamID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@ShiftID", model.ShiftID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        //command.Parameters.AddWithValue("@TeamID", model.TeamID);

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
        public async Task<DataTable> GetFlyingSquad_Attendance(GetFlyingSquadAttendance model)
        {
            _actionName = "GetFlyingSquad_Attendance()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetFlyingSquad_Attendance";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@DeploymentID", model.DeploymentID);

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


        public async Task<int> SetInchargeFlyingSquad(int ID,int TeamID, int Incharge)
        {
            _actionName = "SetInchargeFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SetInchargeFlyingSquad";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@TeamID", TeamID);
                        command.Parameters.AddWithValue("@Incharge", Incharge);
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

        //ITI

        public async Task<DataTable> GetITIFlyingSquad(GetFlyingSquadModal model)
        {
            _actionName = "GetITIFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIFlyingSquad";
                        command.Parameters.AddWithValue("@TeamID", model.TeamID);
                        command.Parameters.AddWithValue("@FlyingSquadDeploymentID", model.FlyingSquadDeploymentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);

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

        public async Task<int> PostITIFlyingSquad(PostFlyingSquadModal model)
        {
            _actionName = "PostITIFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEditDelete_ITIFlyingSquad";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
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

        public async Task<int> PostITITeamFlyingSquadForm(PostTeamFlyingSquadModal model)
        {
            _actionName = "PostITITeamFlyingSquadForm()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITITeamFlyingSquad_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OperationType", model.OperationType);
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@TeamName", model.TeamName);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@ActiveStatus", model.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", model.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);

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

        public async Task<DataTable> GetITITeamFlyingSquad(GetTeamFlyingSquadModal model)
        {
            _actionName = "GetITITeamFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITITeamFlyingSquad_GET";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        //command.Parameters.AddWithValue("@TeamID", model.TeamID);

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

        public async Task<int> PostITITeamDeploymentFlyingSquadForm(PostTeamFlyingSquadModal model)
        {
            _actionName = "PostITITeamDeploymentFlyingSquadForm()";

            return await Task.Run(async () =>
            {
                try
                {
                    int finalResult = 0;

                    // Create a list of shifts to loop through
                    var shifts = new List<ShiftA>();
                    if (model.ShiftA != null) shifts.Add(model.ShiftA);
                    if (model.ShiftB != null) shifts.Add(model.ShiftB);

                    foreach (var shift in shifts)
                    {
                        using (var command = _dbContext.CreateCommand(true))
                        {
                            command.CommandText = "USP_ITIFlyingSquadDeployment_IU";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@OperationType", model.OperationType);
                            command.Parameters.AddWithValue("@TeamID", model.TeamID);
                            command.Parameters.AddWithValue("@DistrictID", shift.DistrictID);
                            command.Parameters.AddWithValue("@InstituteID", shift.InstituteID);
                            command.Parameters.AddWithValue("@InstituteType", shift.InstituteType);
                            command.Parameters.AddWithValue("@ShiftID", shift.ShiftID);
                            command.Parameters.AddWithValue("@DeploymentDate", (object?)shift.DeploymentDate ?? DBNull.Value);
                            command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                            command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                            command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                            command.Parameters.AddWithValue("@ActiveStatus", model.ActiveStatus);
                            command.Parameters.AddWithValue("@DeleteStatus", model.DeleteStatus);
                            command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                            command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
                            command.Parameters.AddWithValue("@Selected", model.Selected);

                            // Add the return parameter
                            command.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.Output;

                            _sqlQuery = command.GetSqlExecutableQuery();

                            // Execute
                            await command.ExecuteNonQueryAsync();
                            finalResult = Convert.ToInt32(command.Parameters["@Return"].Value);
                        }
                    }

                    return finalResult; // Return the last insert result, or customize as needed
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

        public async Task<int> PostITITeamDeploymentFlyingSquad(List<PostTeamFlyingSquadUpdateModal> itam)
        {
            _actionName = "PostITITeamDeploymentFlyingSquad()";

            return await Task.Run(async () =>
            {
                try
                {
                    int finalResult = 0;
                    foreach (var model in itam)
                    {

                        using (var command = _dbContext.CreateCommand(true))
                        {
                            command.CommandText = "USP_ITIFlyingSquadDeployment_IU";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@OperationType", model.OperationType);
                            command.Parameters.AddWithValue("@ID", model.ID);
                            command.Parameters.AddWithValue("@Status", model.Status);

                            // Add the return parameter
                            command.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.Output;

                            _sqlQuery = command.GetSqlExecutableQuery();

                            // Execute
                            await command.ExecuteNonQueryAsync();
                            finalResult = Convert.ToInt32(command.Parameters["@Return"].Value);
                        }

                    }


                    return finalResult; // Return the last insert result, or customize as needed
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


        public async Task<DataTable> GetITITeamDeploymentFlyingSquad(GetTeamFlyingSquadModal model)
        {
            _actionName = "GetITITeamDeploymentFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIFlyingSquadDeployment_GET";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        //command.Parameters.AddWithValue("@TeamID", model.TeamID);

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


        public async Task<int> SetInchargeITIFlyingSquad(int ID, int TeamID, int Incharge)
        {
            _actionName = "SetInchargeITIFlyingSquad()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SetInchargeITIFlyingSquad";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@TeamID", TeamID);
                        command.Parameters.AddWithValue("@Incharge", Incharge);
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


        public async Task<int> PostFlyingSquadAttendanceForm(PostFlyingSquadAttendanceModal model)
        {
            _actionName = "PostFlyingSquadAttendanceForm()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEditDelete_FlyingSquad_Attendance";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
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
