using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.NodalOfficer;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class NodalRepository : INodalRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public NodalRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "NodalRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(SearchNodalModel filterModel)
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
                        command.CommandText = "USP_GetNodalOfficersList";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@Name", filterModel.Name);
                        command.Parameters.AddWithValue("@MobileNo", filterModel.MobileNo);//block
                        //command.Parameters.AddWithValue("@action", "_getAllData"); 

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




        //public async Task<int> SaveNodalData(List<NodalModel> entity)
        //{
        //    _actionName = "SaveNodalData(List<NodalModel> entity)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int totalRowsAffected = 0;

        //            using (var command = _dbContext.CreateCommand(true))
        //            {
        //                command.CommandText = "USP_AddEditNodalData";
        //                command.CommandType = CommandType.StoredProcedure;

        //                foreach (var nodal in entity)
        //                {
        //                    command.Parameters.Clear();

        //                    command.Parameters.AddWithValue("@NodalId", nodal.NodalId == 0 ? (object)DBNull.Value : nodal.NodalId);
        //                    command.Parameters.AddWithValue("@SSOID", nodal.SSOID);
        //                    command.Parameters.AddWithValue("@Name", nodal.Name);
        //                    command.Parameters.AddWithValue("@MobileNo", nodal.MobileNo);
        //                    command.Parameters.AddWithValue("@Email", nodal.Email);
        //                    command.Parameters.AddWithValue("@IsEdit_Stu", nodal.IsEdit_Stu);
        //                    command.Parameters.AddWithValue("@IsAdd_CollageFees", nodal.IsAdd_CollageFees);
        //                    command.Parameters.AddWithValue("@Marked", nodal.Marked);

        //                    var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
        //                    {
        //                        Direction = ParameterDirection.Output
        //                    };
        //                    command.Parameters.Add(rowsAffectedParam);

        //                    await command.ExecuteNonQueryAsync(); 

        //                    int rowsAffected = (int)rowsAffectedParam.Value;

        //                    totalRowsAffected += rowsAffected; 
        //                }
        //            }

        //            return totalRowsAffected; 
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


        //public async Task<int> SaveNodalData(List<NodalModel> entity)
        //{
        //    _actionName = "SaveNodalData(List<NodalModel> entity)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int totalRowsAffected = 0;

        //            using (var command = _dbContext.CreateCommand(true))
        //            {
        //                command.CommandText = "USP_AddEditNodalData";
        //                command.CommandType = CommandType.StoredProcedure;

        //                // Serialize the entity list to JSON
        //                var jsonData = JsonConvert.SerializeObject(entity);

        //                // Add the parameters to the command
        //                command.Parameters.Clear();
        //                command.Parameters.AddWithValue("@NodalData", jsonData);

        //                var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
        //                {
        //                    Direction = ParameterDirection.Output
        //                };
        //                command.Parameters.Add(rowsAffectedParam);

        //                await command.ExecuteNonQueryAsync();

        //                int rowsAffected = (int)rowsAffectedParam.Value;
        //                totalRowsAffected += rowsAffected;
        //            }

        //            return totalRowsAffected;
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

        public async Task<int> SaveNodalData(List<NodalModel> entity)
        {
            _actionName = "SaveNodalData(List<NodalModel> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int totalRowsAffected = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.Parameters.Clear();

                        // Serialize the entire entity list to JSON
                        var jsonData = JsonConvert.SerializeObject(entity);

                        // Decide which stored procedure to call based on NodalId
                        if (entity[0].NodalId == 0)
                        {
                            // Call insert procedure
                            command.CommandText = "USP_AddEditNodalData";
                        }
                        else
                        {
                            // Call update procedure
                            command.CommandText = "USP_UpdateNodalData"; // Make sure this procedure exists
                        }

                        // Set the command type as stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@NodalData", jsonData);

                        // Optionally, handle the RowsAffected output parameter
                        var rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(rowsAffectedParam);

                        // Execute the command asynchronously
                        await command.ExecuteNonQueryAsync();

                        // Get the number of rows affected
                        int rowsAffected = (int)rowsAffectedParam.Value;
                        totalRowsAffected += rowsAffected;
                    }

                    // Return the total number of rows affected
                    return totalRowsAffected;
                }
                catch (Exception ex)
                {
                    // Log error and rethrow with a detailed message
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







        public async Task<DataTable> GetById(int NodalId)
        {
            _actionName = "GetById(int NodalId)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetNodalOfficersBYID";
                        command.Parameters.AddWithValue("@NodalId", NodalId);
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

        public async Task<bool> DeleteDataByID(NodalModel request)
        {
            _actionName = "DeleteDataByID(CompanyMasterModels request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_NodalOfficersDeleteBYID";
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@NodalId", request.NodalId);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);

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
