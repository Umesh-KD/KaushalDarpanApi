using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.StudentsJoiningStatusMarks;
using Kaushal_Darpan.Models.studentve;
using Newtonsoft.Json;
using System;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class StudentsJoiningStatusMarks : IStudentsJoiningStatusMarksRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public StudentsJoiningStatusMarks(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "TspAreaMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
       


        public async Task<DataTable> GetAllData(StudentsJoiningStatusMarksSearchModel body)
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
                        command.CommandText = "USP_GetStudentsJoiningStatusMarks";


                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentMasterId);
                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        command.Parameters.AddWithValue("@CollegeId", body.CollegeId);
                        command.Parameters.AddWithValue("@TradeId", body.TradeId);
                        command.Parameters.AddWithValue("@TradeSchemeId", body.TradeSchemeId);
                        command.Parameters.AddWithValue("@TradeLeveld", body.TradeLevel);
                        command.Parameters.AddWithValue("@ReportingStatus", body.ReportingStatus);
                        command.Parameters.AddWithValue("@AcademicYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@TradeCode", body.TradeCode);


                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);

                        command.Parameters.AddWithValue("@Action", "GetStudentsJoiningStatusMarksList");

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

        public async Task<DataTable> GetUpgradedbyUpwardList(StudentsJoiningStatusMarksSearchModel body)
        {
            _actionName = "GetUpgradedbyUpwardList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentsJoiningStatusMarks";


                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentMasterId);
                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        command.Parameters.AddWithValue("@CollegeId", body.CollegeId);
                        command.Parameters.AddWithValue("@TradeId", body.TradeId);
                        command.Parameters.AddWithValue("@TradeSchemeId", body.TradeSchemeId);
                        command.Parameters.AddWithValue("@TradeLeveld", body.TradeLevel);
                        command.Parameters.AddWithValue("@ReportingStatus", body.ReportingStatus);
                        command.Parameters.AddWithValue("@AcademicYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@TradeCode", body.TradeCode);


                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);

                        command.Parameters.AddWithValue("@Action", "UpgradedbyUpward");

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




        public async Task<int> SaveData(StudentsJoiningStatusMarksModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveData(GroupMaster request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentsJoiningStatusMarks";
                        command.Parameters.AddWithValue("@AllotmentId", request.AllotmentId);
                        command.Parameters.AddWithValue("@ReportingRemark", request.ReportingRemark);
                        command.Parameters.AddWithValue("@ReportingStatus", request.ReportingStatus);
                        command.Parameters.AddWithValue("@ReportingStatus", request);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@AcademicYearID", 0);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        command.Parameters.AddWithValue("@Action", "Insert");

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



        public async Task<int> SaveReporting(AllotmentReportingModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveReporting(GroupMaster request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Save_ITI_Allotmentreporting";
                        command.Parameters.AddWithValue("@AllotmentId", request.AllotmentId);
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@ReportingId", request.ReportingId);
                        command.Parameters.AddWithValue("@ReportingRemark", request.ReportingRemark);
                        command.Parameters.AddWithValue("@JoiningStatus", request.JoiningStatus);
                        command.Parameters.AddWithValue("@ShiftUnitID", request.ShiftUnitID);
                        command.Parameters.AddWithValue("@IsVoterIDAvailable", request.IsVoterIDAvailable);
                        command.Parameters.AddWithValue("@AllotmentDocumentModel", JsonConvert.SerializeObject(request.AllotmentDocumentModel));
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ApplyUpward", request.ApplyUpward);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        command.Parameters.AddWithValue("@Action", "Insert");

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


        public async Task<int> SaveCorrectDocument(AllotmentReportingModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveReporting(GroupMaster request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {   
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_uploadcorrectdeocumentiti";
                        command.Parameters.AddWithValue("@AllotmentId", request.AllotmentId);
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@ReportingId", request.ReportingId);
                        command.Parameters.AddWithValue("@ReportingRemark", request.ReportingRemark);
                        command.Parameters.AddWithValue("@JoiningStatus", request.JoiningStatus);
                        command.Parameters.AddWithValue("@ShiftUnitID", request.ShiftUnitID);
                        command.Parameters.AddWithValue("@IsVoterIDAvailable", request.IsVoterIDAvailable);
                        command.Parameters.AddWithValue("@AllotmentDocumentModel", JsonConvert.SerializeObject(request.AllotmentDocumentModel));
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ApplyUpward", request.ApplyUpward);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        command.Parameters.AddWithValue("@Action", "Insert");

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

        public async Task<AllotmentReportingModel> GetAllotmentdata(StudentsJoiningStatusMarksSearchModel body)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllotmentreporting";
                  
                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
           
                        command.Parameters.AddWithValue("@Action", "GetStudentsJoiningStatusMarksList");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new AllotmentReportingModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<AllotmentReportingModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count>0)
                            {

                                data.AllotmentDocumentModel = CommonFuncationHelper.ConvertDataTable<List<AllotmentDocumentModel>>(dataSet.Tables[1]);
                            }

                         

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



     



        public async Task<DataTable> GetSeatAllotmentData(StudentsJoiningStatusMarksSearchModel body)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentsJoiningStatusMarks";


                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentMasterId);
                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        command.Parameters.AddWithValue("@CollegeId", body.CollegeId);
                        command.Parameters.AddWithValue("@TradeId", body.TradeId);
                        command.Parameters.AddWithValue("@TradeSchemeId", body.TradeSchemeId);
                        command.Parameters.AddWithValue("@TradeLeveld", body.TradeLevel);
                        command.Parameters.AddWithValue("@ReportingStatus", body.ReportingStatus);
                        command.Parameters.AddWithValue("@AcademicYearID", body.FinancialYearID);

                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);

                        command.Parameters.AddWithValue("@Action", "GetStudentsJoiningStatusMarksList");

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




        public async Task<DataTable> CheckAllot(StudentsJoiningStatusMarksSearchModel body)
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
                        command.CommandText = "USP_CheckAllot";


                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentMasterId);
                        //command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        //command.Parameters.AddWithValue("@CollegeId", body.CollegeId);
                        //command.Parameters.AddWithValue("@TradeId", body.TradeId);
                        //command.Parameters.AddWithValue("@TradeSchemeId", body.TradeSchemeId);
                        //command.Parameters.AddWithValue("@TradeLeveld", body.TradeLevel);
                        //command.Parameters.AddWithValue("@ReportingStatus", body.ReportingStatus);


                        //command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        //command.Parameters.AddWithValue("@PageSize", body.PageSize);

                        //command.Parameters.AddWithValue("@Action", "GetStudentsJoiningStatusMarksList");

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


        public async Task<DataTable> GetAllReuploadDocumentList(StudentsJoiningStatusMarksSearchModel body)
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
                        command.CommandText = "USP_GetStudentReuploadDocumentlist";


                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentMasterId);
                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        command.Parameters.AddWithValue("@CollegeId", body.CollegeId);
                        command.Parameters.AddWithValue("@TradeId", body.TradeId);
                        command.Parameters.AddWithValue("@TradeSchemeId", body.TradeSchemeId);
                        command.Parameters.AddWithValue("@TradeLeveld", body.TradeLevel);
                        command.Parameters.AddWithValue("@ReportingStatus", body.ReportingStatus);
                        command.Parameters.AddWithValue("@AcademicYearID", body.FinancialYearID);


                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);

                        command.Parameters.AddWithValue("@Action", "GetStudentsJoiningStatusMarksList");

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


        public async Task<AllotmentReportingModel> GetCorrectDocumentdata(StudentsJoiningStatusMarksSearchModel body)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       command.CommandText = "USP_GetCorrectdocumentdata";

                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);

                        command.Parameters.AddWithValue("@Action", "GetStudentsJoiningStatusMarksList");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new AllotmentReportingModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<AllotmentReportingModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count>0)
                            {

                                data.AllotmentDocumentModel = CommonFuncationHelper.ConvertDataTable<List<AllotmentDocumentModel>>(dataSet.Tables[1]);
                            }



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




        public async Task<DataTable> GetCollegeAdminData(ReportCollegeForAdminModel body)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllotmentReportCollegeforAdmin";
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@CollegeId", body.CollegeId);
                        command.Parameters.AddWithValue("@TradeLevelID", body.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", body.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", body.TradeId);
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

        public async Task<DataTable> GetCollegeData(ReportCollegeModel body)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentSeatAllotment";
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevelID", body.TradeLevelID);
                        command.Parameters.AddWithValue("@TradeTypeID", body.TradeTypeID);
                        command.Parameters.AddWithValue("@TradeId", body.TradeId);
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


    }
}
