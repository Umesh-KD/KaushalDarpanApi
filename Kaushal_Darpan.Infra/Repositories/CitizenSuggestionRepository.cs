using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AssignRoleRight;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.CitizenSuggestion;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.Examiners;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CitizenSuggestionRepository: ICitizenSuggestionRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CitizenSuggestionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CitizenSuggestionRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        public async Task<DataTable> GetAllData(CitizenSuggestionSearchModel model)
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
                        command.CommandText = "Sp_GetAllCitizenSuggestion";
                        command.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@CommnID", model.CommnID);
                        command.Parameters.AddWithValue("@QueryAging", model.QueryAging);
                        command.Parameters.AddWithValue("@CitizenQueryStatus", model.CitizenQueryStatus);

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



        public async Task<DataTable> SaveData(CitizenSuggestion request)
        {
            _actionName = "SaveData(CitizenSuggestion request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable resultTable = new DataTable(); // To store the result set

                    // Create the command and set up parameters
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "Sp_AddCitizenSuggestion";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@SubjectId", request.SubjectId);
                        command.Parameters.AddWithValue("@CommnID", request.CommnID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Comment", request.Comment);
                        command.Parameters.AddWithValue("@AttchFilePath", request.AttchFilePath);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);

                        _sqlQuery = command.GetSqlExecutableQuery(); // Logging SQL query for debugging

                        // Open a connection and execute the command to fill the result table
                        using (var dataAdapter = new SqlDataAdapter(command))
                        {
                            await Task.Run(() => dataAdapter.Fill(resultTable)); // Fill the DataTable asynchronously
                        }

                        // The result set will now contain the message and the generated SR number
                        if (resultTable.Rows.Count > 0)
                        {
                            var generatedSRNo = resultTable.Rows[0]["GeneratedSRNo"].ToString();
                            if (!string.IsNullOrEmpty(generatedSRNo))
                            {
                                // Add the generated SR number to the result table
                                DataRow newRow = resultTable.NewRow();
                                newRow["GeneratedSRNo"] = generatedSRNo; // Assuming you want to store the SRNo in the result
                                resultTable.Rows.Add(newRow);
                            }
                        }
                    }

                    return resultTable;
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










        public async Task<bool> SaveReplayData(ReplayQuery request)
        {
            _actionName = "SaveReplayData(ReplayQuery request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "Sp_UpdateQueryReplay";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@Replay", request.Replay);
                        command.Parameters.AddWithValue("@Pk_ID", request.Pk_ID);
                        command.Parameters.AddWithValue("@ReplayAttachment", request.ReplayAttachment);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IsResolved", request.IsResolved);

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

        public async Task<CitizenSuggestion> GetByID(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = " select * from Tbl_CitzenSuggestion  Where Pk_ID ='" + PK_ID + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new CitizenSuggestion();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<CitizenSuggestion>(dataTable);
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


        public async Task<DataTable> GetSRNumberData(CitizenSuggestionSearchSRModel model)
        {
            _actionName = "GetSRNumberData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Sp_GetCitizenSuggestionTrack";
                        command.Parameters.AddWithValue("@SRNumber", model.SRNumber);
                        command.Parameters.AddWithValue("@action", "GetSRNumberData");

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

        public async Task<DataTable> GetSRNDataList(CitizenSuggestionSearchModel model)
        {
            _actionName = "GetSRNumberData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Sp_GetCitizenSuggestionTrack";
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@action", "GetAllData");

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


        public async Task<CitizenSuggestion> GetByMobileNo(string Mobile)
        {
            _actionName = "GetByMobileNo(int Mobile)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "select ISNULL( COUNT( Pk_ID),0) as Pk_ID  from Tbl_CitzenSuggestion where MobileNo ='" + Mobile + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new CitizenSuggestion();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<CitizenSuggestion>(dataTable);
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

        public async Task<bool> SaveUserRating(UserRatingDataModel request)
        {
            _actionName = "SaveReplayData(ReplayQuery request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "Sp_GetCitizenSuggestionTrack";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@action", "UpdateUserRating");
                        command.Parameters.AddWithValue("@Pk_ID", request.Pk_ID);
                        command.Parameters.AddWithValue("@SRNumber", request.SRNo);
                        command.Parameters.AddWithValue("@RatingRemarks", request.RatingRemarks);
                        command.Parameters.AddWithValue("@UserRating", request.UserRating);

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
