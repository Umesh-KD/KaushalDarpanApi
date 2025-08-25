using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTEReatsDistributionsMaster;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITISeatsDistributionsMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.TSPAreaMaster;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class BTERSeatsDistributionsMasterRepository : IBTERSeatsDistributionsMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public BTERSeatsDistributionsMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BTERSeatsDistributionsMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(BTERSeatsDistributionsSearchModel body)
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
                        command.CommandText = "USP_GetITISeatsDistributions";
                        command.Parameters.AddWithValue("@max_strength", body.max_strength);
                        command.Parameters.AddWithValue("@remark", body.remark);
                        command.Parameters.AddWithValue("@total_seats", body.total_seats);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@Action", "GetITISeatsDistributions");

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

        public async Task<int> SaveData(BTERSeatsDistributionsDataModel request)
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
                        command.CommandText = "USP_ITISeatsDistributionsMasterIU";
                        command.Parameters.AddWithValue("@id", request.id);
                        command.Parameters.AddWithValue("@max_strength", request.max_strength);
                        command.Parameters.AddWithValue("@total_seats", request.total_seats);
                        command.Parameters.AddWithValue("@remark", request.remark);
                        command.Parameters.AddWithValue("@sc", request.sc);
                        command.Parameters.AddWithValue("@sc_f", request.sc_f);
                        command.Parameters.AddWithValue("@st", request.st);
                        command.Parameters.AddWithValue("@st_f", request.st_f);
                        command.Parameters.AddWithValue("@obc", request.obc);
                        command.Parameters.AddWithValue("@obc_f", request.obc_f);
                        command.Parameters.AddWithValue("@mbc", request.mbc);
                        command.Parameters.AddWithValue("@mbc_f", request.mbc_f);
                        command.Parameters.AddWithValue("@ews", request.ews);
                        command.Parameters.AddWithValue("@ews_f", request.ews_f);
                        command.Parameters.AddWithValue("@gen", request.gen);
                        command.Parameters.AddWithValue("@gen_f", request.gen_f);

                        command.Parameters.AddWithValue("@tsp", request.tsp);
                        command.Parameters.AddWithValue("@tsp_f", request.tsp_f);

                        command.Parameters.AddWithValue("@km", request.km);
                        command.Parameters.AddWithValue("@ph", request.ph);
                        command.Parameters.AddWithValue("@ex_m", request.ex_m);
                        command.Parameters.AddWithValue("@w_d", request.w_d);
                        //command.Parameters.AddWithValue("@w_d", request);

                        command.Parameters.AddWithValue("@imcsc", request.mgm);

                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        command.Parameters.AddWithValue("@Action", "SaveITISeatsDistributions");
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

        public async Task<BTERSeatsDistributionsDataModel> GetById(int id)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        _sqlQuery = $" select * from iti_seatsdistributions Where id='{id}'";
                        command.CommandText = _sqlQuery;
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new BTERSeatsDistributionsDataModel();
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<BTERSeatsDistributionsDataModel>(dataTable);
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


        public async Task<DataTable> GetSeatMetrixData(BTERSeatsDistributionsDataModel model)
        {
            _actionName = "GetSeatMetrixData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERSeatMetrix";
                        command.Parameters.AddWithValue("@ManagementTypeID", model.Type);
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Action", model.Action);
                        command.Parameters.AddWithValue("@TradeSchemeId", model.StreamTypeId);
                        command.Parameters.AddWithValue("@AllotmentMasterId", model.AllotmentMasterId);

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

        public async Task<int> SaveSeatsDistributions(List<BTERSeatMetrixModel> model)
        {
            _actionName = "SaveSeatsDistributions(List<BTERSeatMetrixModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTERSeat_Ditribution_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_addStudentEligibleForEnrollmentData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));

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


        public async Task<DataTable> SaveSeatsMatrixlist(List<BTERSeatMetrixSaveModel> model)
        {
            _actionName = "SaveSeatsMatrixlist(List<BTERSeatMetrixModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTERSeat_MatrixSampleList_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_addStudentEligibleForEnrollmentData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@AcademicYearID", model[0].FinancialYearID);                      
                        command.Parameters.AddWithValue("@CourseTypeId", model[0].StreamTypeId);

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                        return dataTable;
                    }
                  
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

        public async Task<DataTable> GetDirectTradeAndColleges(BTERCollegeTradeSearchModel request)
        {
            _actionName = "GetDirectTradeAndColleges(BTERCollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERGetCollegeTradeList";
                        command.Parameters.AddWithValue("@CollegeId", request.CollegeID);
                        command.Parameters.AddWithValue("@CollegeStreamId", request.CollegeStreamId);
                        command.Parameters.AddWithValue("@StreamTypeId", request.StreamTypeId);
                        command.Parameters.AddWithValue("@AcademicYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@TradeCode", request.TradeCode);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@ShowTotalSeatsCondition", request.TotalSeatAvailable);
                        //command.Parameters.AddWithValue("@TradeLevelId", request.TradeLevelId);
                        command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                        command.Parameters.AddWithValue("@ManagementTypeId", request.ManagementTypeId);
                        command.Parameters.AddWithValue("@FeeStatus", request.FeeStatus);
                        command.Parameters.AddWithValue("@AllotmentMasterId", request.AllotmentMasterId);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@PageNumber", request.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", request.PageSize);
                        command.Parameters.AddWithValue("@Action", request.Action);

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


        public async Task<DataTable> GetTradeAndColleges(BTERCollegeTradeSearchModel request)
        {
            _actionName = "GetTradeAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERGetCollegeTradeList";
                        command.Parameters.AddWithValue("@CollegeId", request.CollegeID);
                        command.Parameters.AddWithValue("@CollegeStreamId", request.CollegeStreamId);
                        command.Parameters.AddWithValue("@StreamTypeId", request.StreamTypeId);
                        command.Parameters.AddWithValue("@AcademicYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@TradeCode", request.TradeCode);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@ShowTotalSeatsCondition", request.TotalSeatAvailable);
                        //command.Parameters.AddWithValue("@TradeLevelId", request.TradeLevelId);
                        command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                        command.Parameters.AddWithValue("@ManagementTypeId", request.ManagementTypeId);
                        command.Parameters.AddWithValue("@FeeStatus", request.FeeStatus);
                        command.Parameters.AddWithValue("@AllotmentMasterId", request.AllotmentMasterId);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@PageNumber", request.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", request.PageSize);
                        command.Parameters.AddWithValue("@Action", request.Action);

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


        public async Task<DataTable> GetSampleTradeAndColleges(BTERCollegeTradeSearchModel request)
        {
            _actionName = "GetSampleTradeAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERGetSampleCollegeTradeList";
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@StreamTypeId", request.StreamTypeId);
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

        public async Task<DataTable> GetSampleSeatmatrixAndColleges(BTERCollegeTradeSearchModel request)
        {
            _actionName = "GetSampleSeatmatrixAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERGetSampleSeatMatrixList";
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
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

        public async Task<DataTable> BTERManagementType()
        {
            _actionName = "ITIManagementType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERGetCollegeTradeList";
                        command.Parameters.AddWithValue("@Action", "_BTERManagementType");

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

        public async Task<DataTable> GetBTERCollegesByManagementType(BTERCollegeTradeSearchModel request)
        {
            _actionName = "GetITICollegesByManagementType(GetITICollegesByManagementType request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERGetCollegeTradeList";

                        command.Parameters.AddWithValue("@ManagementTypeId", request.ManagementTypeId);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@AcademicYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@StreamTypeId", request.StreamTypeId);
                        command.Parameters.AddWithValue("@Action", request.Action);

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
        public async Task<DataTable> getBTERTrade(BTERCollegeTradeSearchModel request)
        {
            _actionName = "getITITrade(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERGetCollegeTradeList";

                        command.Parameters.AddWithValue("@CollegeID", request.CollegeID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@AcademicYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@StreamTypeId", request.StreamTypeId);
                        command.Parameters.AddWithValue("@Action", request.Action);

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


        public async Task<DataTable> BTERTradeScheme()
        {
            _actionName = "ITITradeScheme()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERGetCollegeTradeList";
                        command.Parameters.AddWithValue("@Action", "_BTERTradeScheme");

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




        public async Task<DataTable> CollegeBranches(BTERCollegeTradeSearchModel request)
        {
            _actionName = "GetTradeAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERCollegeBranches";


                        command.Parameters.AddWithValue("@CollegeStreamId", request.CollegeStreamId);
                        command.Parameters.AddWithValue("@StreamTypeID", request.StreamTypeId);
                        command.Parameters.AddWithValue("@InstituteID", request.CollegeID);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@ShiftID", request.ShiftID);
                        command.Parameters.AddWithValue("@TotalSeats", request.TotalSeats);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@AcademicYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@AdmissionSeats", request.TotalAdmissionSeats);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@PageNumber", request.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", request.PageSize);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@ManagementTypeId", request.ManagementTypeId);
                        command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                        command.Parameters.AddWithValue("@StreamCode", request.StreamCode);
                        command.Parameters.AddWithValue("@StreamFor", request.StreamFor);



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

        public async Task<DataTable> SaveCollegeBranches(BTERCollegeTradeSearchModel request)
        {
            _actionName = "GetTradeAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERCollegeBranches";

                        command.Parameters.AddWithValue("@CollegeStreamId", request.CollegeStreamId);
                        command.Parameters.AddWithValue("@StreamTypeID", request.StreamTypeId);
                        command.Parameters.AddWithValue("@InstituteID", request.CollegeID);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@ShiftID", request.ShiftID);
                        command.Parameters.AddWithValue("@TotalSeats", request.TotalSeats);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@AcademicYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@AdmissionSeats", request.TotalAdmissionSeats);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@StreamFor", request.StreamFor);
                        command.Parameters.AddWithValue("@Action", request.Action);

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

        public async Task<DataTable> GetBranchesStreamTypeWise(BranchStreamTypeWiseSearchModel request)
        {
            _actionName = "GetTradeAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERCollegeBranches";

                        command.Parameters.AddWithValue("@Action", "BRANCH_LIST");
                        command.Parameters.AddWithValue("@StreamTypeID", request.StreamTypeId);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);



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

        public async Task<DataTable> GetCollegeBrancheByID(int BranchID)
        {
            _actionName = "GetTradeAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERCollegeBranches";


                        command.Parameters.AddWithValue("@CollegeStreamId", BranchID);
                        command.Parameters.AddWithValue("@Action", "GET_COLLEGE_BY_ID");



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

        public async Task<DataTable> DeleteCollegeBrancheByID(int BranchID, int UserID)
        {
            _actionName = "GetTradeAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERCollegeBranches";


                        command.Parameters.AddWithValue("@CollegeStreamId", BranchID);
                        command.Parameters.AddWithValue("@CreatedBy", UserID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@Action", "DELETE");


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

        public async Task<DataTable> StatusChangeByID(int BranchID,int ActiveStatus, int UserID)
        {
            _actionName = "GetTradeAndColleges(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERCollegeBranches";


                        command.Parameters.AddWithValue("@CollegeStreamId", BranchID);
                        command.Parameters.AddWithValue("@CreatedBy", UserID);
                        command.Parameters.AddWithValue("@ActiveStatus", ActiveStatus);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@Action", "STATUS");


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

        public async Task<DataTable> PublishSeatMatrix(BTERCollegeTradeSearchModel request)
        {
            _actionName = "PublishSeatMatrix(BTERCollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERPublishSeatMetrix";

                        command.Parameters.AddWithValue("@AllotmentMasterId", request.AllotmentMasterId);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@StreamTypeId", request.StreamTypeId);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@CreateBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);

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

        public async Task<DataTable> SeatMatixSecondAllotment(BTERCollegeTradeSearchModel request)
        {
            _actionName = "SeatMatixSecondAllotment(BTERCollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERSeatMatixSecondAllotment";   
                        command.Parameters.AddWithValue("@StreamTypeId", request.StreamTypeId);
                        command.Parameters.AddWithValue("@AcademicYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);

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

        public async Task<DataTable> SeatMatixInternalSliding(BTERCollegeTradeSearchModel request)
        {
            _actionName = "SeatMatixInternalSliding(BTERCollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTERSeatMatrixInternalSliding";
                        command.Parameters.AddWithValue("@StreamTypeId", request.StreamTypeId);
                        command.Parameters.AddWithValue("@AcademicYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);

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
