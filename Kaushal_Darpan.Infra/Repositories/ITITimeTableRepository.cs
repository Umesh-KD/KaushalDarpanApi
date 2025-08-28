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

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITITimeTableRepository : IITITimeTableRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private readonly string _IPAddress;

        public ITITimeTableRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITITimeTableRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ITITimeTableSearchModel model)
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
                        command.CommandText = "USP_ITITimeTableMaster";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@ShiftID", model.ShiftID);
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
        public async Task<ITITimeTableModel> GetById(int ID)
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
                        command.CommandText = "USP_ITITimeTable_GetById";
                        command.Parameters.AddWithValue("@TimeTableID", ID);
                        command.Parameters.AddWithValue("@action", "_getByID");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITITimeTableModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITITimeTableModel>(dataSet.Tables[0]);
                            //if (dataSet.Tables[1].Rows.Count > 0)
                            //{
                            //    data.TradeSubjectDataModel = CommonFuncationHelper.ConvertDataTable<List<TradeSubjectDataModel>>(dataSet.Tables[1]);
                            //}
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
        public async Task<int> SaveData(ITITimeTableModel request)
        {
            _actionName = "SaveData(ITITimeTableModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITI_TimeTableMaster_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TimeTableID", request.TimeTableID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@ExamDate", request.ExamDate);
                        command.Parameters.AddWithValue("@ShiftID", request.ShiftID);

                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SubjectName", request.SubjectName);
                        command.Parameters.AddWithValue("@TradeName", request.TradeName);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);

                        //command.Parameters.AddWithValue("@TradeSubjectDetails", JsonConvert.SerializeObject(request.TradeSubjectDataModel));

                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();
                        
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
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

        public async Task<bool> DeleteDataByID(ITITimeTableModel request)
        {
            _actionName = "DeleteDataByID(ITITimeTableModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $" update M_ITI_TimeTableMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where TimeTableID={request.TimeTableID} and DepartmentID={request.DepartmentID}";

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

        public async Task<List<TradeSubjectDataModel>> GetTimeTableByID(int ID)
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
                        command.CommandText = "USP_ITITimeTable_GetById";
                        command.Parameters.AddWithValue("@TimeTableID", ID);
                        command.Parameters.AddWithValue("@action", "_getTimeTableByID");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<TradeSubjectDataModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<TradeSubjectDataModel>>(dataTable);
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

        public async Task<int> SaveInvigilator(ITI_TimeTableInvigilatorModel request)
        {
            _actionName = "SaveData(TimeTableModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_M_ITI_TimeTable_Invigilator";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@TimeTableID", request.TimeTableID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@InvigilatorID", request.InvigilatorID);

                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();

                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
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

        public async Task<ITI_TimeTableInvigilatorModel> GetInvigilatorByID(int ID, int InstituteID)
        {
            _actionName = "GetInvigilatorByID(int ID, int InstituteID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITITimeTable_GetById";
                        command.Parameters.AddWithValue("@TimeTableID", ID);
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);
                        command.Parameters.AddWithValue("@action", "_getInvigilatorByID");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ITI_TimeTableInvigilatorModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ITI_TimeTableInvigilatorModel>(dataTable);
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

        



        public async Task<DataTable> GetTimeTableTradeSubject(NewITI_TimeTableValidateModel model)
        {
            _actionName = "GetTimeTableTradeSubject()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_TimeTableGetBranchSubject";
                        command.Parameters.AddWithValue("@TimeTableID", model.TimeTableID);
                        command.Parameters.AddWithValue("@SubjectList", JsonConvert.SerializeObject(model.ITISubjectList));
                        command.Parameters.AddWithValue("@SubjectCode", model.SubjectCode);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
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

        public async Task<DataTable> GetSampleTimeTableITI(ITITimeTableSearchModel request)
        {
            _actionName = "GetSampleTimeTableITI(TimeTableSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetSampleTimeTableList";
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
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


        public async Task<DataTable> GetSampleCBTCenterITI(ITITimeTableSearchModel request)
        {
            _actionName = "GetSampleTimeTableITI(TimeTableSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetCBTSampleExcel";
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
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




        public async Task<List<ITITimeTableModel>> ImportExcelFile(List<ITITimeTableModel> model)
        {
            _actionName = "ImportExcelFile(TimeTableModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_ImportExcelList_TimeTable";
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<ITITimeTableModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<ITITimeTableModel>>(dataTable);
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

        
        public async Task<int> SaveImportExcelData(List<ITITimeTableModel> request)
        {
            _actionName = "SaveImportExcelData(TimeTableModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_TimeTable_IU_Import";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TimeTableData", JsonConvert.SerializeObject(request));

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

        public async Task<List<ITICBTCenterModel>> CBTImportExcelFile(List<ITICBTCenterModel> model)
        {
            _actionName = "ImportExcelFile(TimeTableModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_CBTImportExcelList";
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<ITICBTCenterModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<ITICBTCenterModel>>(dataTable);
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

        public async Task<int> SaveCBTImportExcelData(List<ITICBTCenterModel> request)
        {
            _actionName = "SaveCBTImportExcelData(TimeTableModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "USP_ITI_CBTCenter_IU_Import";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CBTCenterData", JsonConvert.SerializeObject(request));

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

        public async Task<DataTable> GetAllCBTData(ITICBTCenterModel model)
        {
            _actionName = "GetAllCBTData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_CBTCenter";
                        
                        command.Parameters.AddWithValue("@CBTCenterID", model.CBTCenterID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        

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
    }
}
