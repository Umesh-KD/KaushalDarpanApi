using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITI_InstructorModel;
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
    public class ITI_BGTHeadmasterRepository : I_ITI_BGTHeadmasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITI_BGTHeadmasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITI_BGTHeadmasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SaveBGTHeadmasterData(ITI_BGT_HeadMasterDataModel request)
        {
            _actionName = "SaveInstructorData(ITI_InstructorModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_HeadMasters";
                        command.Parameters.AddWithValue("@Action", "SaveData");
                        command.Parameters.AddWithValue("@HeadId", request.HeadId);
                        command.Parameters.AddWithValue("@HeadName", request.HeadName);
                        command.Parameters.AddWithValue("@IsUnitWise", request.IsUnitWise);
                        command.Parameters.AddWithValue("@UnitName", request.UnitName);
                        command.Parameters.AddWithValue("@HeadCode", request.HeadCode);
                        command.Parameters.AddWithValue("@HeadDescription", request.HeadDescription);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);
                        
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


        public async Task<DataTable> GetBGTHeadmasterDataByID(int id)
        {
            _actionName = "GetBGTHeadmasterDataByID(int id)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_HeadMasters";
                        command.Parameters.AddWithValue("@Action", "GetById");
                        command.Parameters.AddWithValue("@HeadId", id);

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


        public async Task<int> deleteInstructorDataByID(int id)
        {
            _actionName = " deleteInstructorDataByID(int id)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Instructor";
                        command.Parameters.AddWithValue("@Action", "deleteInstructorDataByID");

                        command.Parameters.AddWithValue("@ID", id);


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


        //public async Task<DataTable> GetCenterSuperitendentReportData()
        //{
        //    _actionName = "GetCenterSuperitendentReportData()";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_ITICenterSuperintendentExamReport";
        //                command.Parameters.AddWithValue("@Action", "GetCenterSuperitendentReportData");
        //                //command.Parameters.AddWithValue("@DistrictId", model.DistrictID);
        //                //command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
        //                //command.Parameters.AddWithValue("@InstituteId", model.InstituteID);
        //                //command.Parameters.AddWithValue("@Code", model.CollegeCode);
        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataTable = await command.FillAsync_DataTable();
        //            }

        //            return dataTable;
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



        public async Task<DataTable> GetBGTHeadmasterData(ITI_BGT_HeadMasterSearchModel model)
        {
            _actionName = "GetInstructorData(ITI_InstructorDataSearchModel model )";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_HeadMasters";
                        command.Parameters.AddWithValue("@Action", "GetData");
                        command.Parameters.AddWithValue("@HeadName", model.Name);

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

        public async Task<int> DeleteBudgetHeadById(int HeadId, int UserID)
        {
            _actionName = " DeleteBudgetHeadById(int id)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_HeadMasters";
                        command.Parameters.AddWithValue("@Action", "deleteById");
                        command.Parameters.AddWithValue("@HeadId", HeadId);
                        command.Parameters.AddWithValue("@CreatedBy", UserID);


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

        public async Task<int> SaveUCHeadData_ITI_BGT(ITI_BGT_HeadMasterDataModel request)
        {
            _actionName = "SaveUCHeadData_ITI_BGT(ITI_BGT_HeadMasterDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_UC_HeadMaster";
                        command.Parameters.AddWithValue("@Action", "SaveData");
                        command.Parameters.AddWithValue("@HeadId", request.HeadId);
                        command.Parameters.AddWithValue("@HeadName", request.HeadName);
                        command.Parameters.AddWithValue("@HeadCode", request.HeadCode);
                        command.Parameters.AddWithValue("@HeadDescription", request.HeadDescription);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);
                        command.Parameters.AddWithValue("@FinYearID", request.FinYearID);
                        command.Parameters.AddWithValue("@BudgetTypeID", request.BudgetTypeID);
                        command.Parameters.AddWithValue("@BudgetForID", request.BudgetForID);

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

        public async Task<DataTable> GetUCHeadData_ITI_BGT(ITI_BGT_HeadMasterSearchModel model)
        {
            _actionName = "GetUCHeadData_ITI_BGT(ITI_InstructorDataSearchModel model )";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_UC_HeadMaster";
                        command.Parameters.AddWithValue("@Action", "GetData");
                        command.Parameters.AddWithValue("@HeadName", model.Name);
                        command.Parameters.AddWithValue("@FinYearID", model.FinYearID);

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

        public async Task<DataTable> GetUCHeadDataById_ITI_BGT(int id)
        {
            _actionName = "GetUCHeadDataById_ITI_BGT(int id)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_UC_HeadMaster";
                        command.Parameters.AddWithValue("@Action", "GetById");
                        command.Parameters.AddWithValue("@HeadId", id);

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

        public async Task<int> DeleteUCHeadById_ITI_BGT(int HeadId, int UserID)
        {
            _actionName = " DeleteUCHeadById_ITI_BGT(int HeadId, int UserID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_BGT_UC_HeadMaster";
                        command.Parameters.AddWithValue("@Action", "deleteById");
                        command.Parameters.AddWithValue("@HeadId", HeadId);
                        command.Parameters.AddWithValue("@CreatedBy", UserID);


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
    }



}

