using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ExamMasterRepository : IExamMasterRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ExamMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ExamMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ExamMasterDataModel body)
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
                        command.CommandText = "USP_ExamMaster_GetExamMasterData"; // Assuming you are using the action filter
                                                                                  
                        command.Parameters.AddWithValue("@SessionYearID", body.SessionYearID);
                        command.Parameters.AddWithValue("@SessionMonthID", body.SessionMonthID);
                        command.Parameters.AddWithValue("@ProgramTypeID", body.ProgramTypeID);
                        //command.Parameters.AddWithValue("@ExamTypeID",body. ExamTypeID);
                        //command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        //command.Parameters.AddWithValue("@AdmissionCategoryID", body.AdmissionCategoryID);


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
        public async Task<ExamMasterDataModel> Get_ExamMasterData_ByID(int ExamMasterID)
        {
            _actionName = "Get_ExamMasterData_ByID";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "Get_ExamMasterData_ByID";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ExamMasterID", ExamMasterID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ExamMasterDataModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ExamMasterDataModel>(dataTable);
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
        public async Task<bool> SaveData(ExamMasterDataModel request)
        {
            _actionName = "SaveData(ExamMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_InsertExamMaster";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ExamMasterID", request.ExamMasterID);
                        command.Parameters.AddWithValue("@SessionYearID", request.SessionYearID);
                        command.Parameters.AddWithValue("@SessionMonthID", request.SessionMonthID);
                        command.Parameters.AddWithValue("@ProgramTypeID", request.ProgramTypeID);
                        command.Parameters.AddWithValue("@ExamTypeID", request.ExamTypeID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@AdmissionCategoryID", request.AdmissionCategoryID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                    }

                    return result > 0;
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

        public async Task<bool> DeleteDataByID(ExamMasterDataModel request)
        {
            _actionName = "DeleteDataByID(PapersMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update M_ExamsPapersMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where PaperID={request.ExamMasterID}";

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








