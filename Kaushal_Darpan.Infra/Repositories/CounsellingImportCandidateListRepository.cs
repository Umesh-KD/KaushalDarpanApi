using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.ITITimeTable;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using static Kaushal_Darpan.Models.ITIApplication.ItiApplicationPreviewDataModel;
using Kaushal_Darpan.Models.TimeTable;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.CounsellingMaster;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CounsellingImportCandidateListRepository : ICounsellingImportCandidateListRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private readonly string _IPAddress;

        public CounsellingImportCandidateListRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CounsellingImportCandidateListRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        
        public async Task<DataTable> GetSampleExcelFile()
        {
            _actionName = "GetSampleExcelFile()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CounsellingImportCandidateList";
                        command.Parameters.AddWithValue("@Action", "GetExcelFormat");
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

        public async Task<DataTable> GetCandidateList(CounsellingAllotmentListModel body)
        {
            _actionName = "GetCounsellingAllotmentList(CollegeWiseScholarshipSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CounsellingImportCandidateList";

                        command.Parameters.AddWithValue("@CandidateID", body.CandidateID);
                        //command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        //command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        //command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        //command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@Action", body.action);
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

        public async Task<List<CounsellingImportExcelModel>> ImportExcelFile(List<CounsellingImportExcelModel> model)
        {
            _actionName = "ImportExcelFile(TimeTableModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CounsellingImportCandidateList";
                        command.Parameters.AddWithValue("@Action", "PrepareExceldata");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CounsellingImportExcelModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CounsellingImportExcelModel>>(dataTable);
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

        public async Task<int> SaveImportExcelData(List<CounsellingImportExcelModel> request)
        {
            _actionName = "SaveImportExcelData(List<CounsellingImportExcelModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandText = "USP_CounsellingImportCandidateList";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "SaveData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        //result = await command.ExecuteNonQueryAsync();

                        //retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out

                        await command.ExecuteNonQueryAsync();
                        retval = (command.Parameters["@Retval"].Value == DBNull.Value) ? 0 : Convert.ToInt32(command.Parameters["@Retval"].Value);

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

        public async Task<int> EditCandidateExcelDataById(CounsellingImportExcelModel request)
        {
            _actionName = "SaveImportExcelData(List<CounsellingImportExcelModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_EditCandidateExcelData";
                        command.Parameters.AddWithValue("@Action", "EditCandidateDetails");

                        command.Parameters.AddWithValue("@CandidateName", request.CandidateName);
                        command.Parameters.AddWithValue("@CandidateFatherName", request.CandidateFatherName);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@CandidateID", request.CandidateID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        await command.ExecuteNonQueryAsync();
                        retval = (command.Parameters["@Retval"].Value == DBNull.Value) ? 0 : Convert.ToInt32(command.Parameters["@Retval"].Value);

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


    }
}
