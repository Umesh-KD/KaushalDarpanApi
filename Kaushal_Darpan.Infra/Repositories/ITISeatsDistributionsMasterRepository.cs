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
    public class ITISeatsDistributionsMasterRepository : IITISeatsDistributionsMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITISeatsDistributionsMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITISeatsDistributionsMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ITISeatsDistributionsSearchModel body)
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

        public async Task<int> SaveData(ITISeatsDistributionsDataModel request)
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
                        command.Parameters.AddWithValue("@min", request.min);
                        command.Parameters.AddWithValue("@min_f", request.min_f);
                        command.Parameters.AddWithValue("@tsp", request.tsp);
                        command.Parameters.AddWithValue("@tsp_f", request.tsp_f);
                        command.Parameters.AddWithValue("@dny", request.dny);
                        command.Parameters.AddWithValue("@dny_f", request.dny_f);
                        command.Parameters.AddWithValue("@sahriya", request.sahriya);
                        command.Parameters.AddWithValue("@sahriya_f", request.sahriya_f);
                        command.Parameters.AddWithValue("@ph", request.ph);
                        command.Parameters.AddWithValue("@ex_m", request.ex_m);
                        command.Parameters.AddWithValue("@w_d", request.w_d);
                        command.Parameters.AddWithValue("@imcsc", request.imcsc);
                        command.Parameters.AddWithValue("@imcst", request.imcst);
                        command.Parameters.AddWithValue("@imcobc", request.imcobc);
                        command.Parameters.AddWithValue("@imcgen", request.imcgen);
                        command.Parameters.AddWithValue("@imctotal", request.imctotal);
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

        public async Task<ITISeatsDistributionsDataModel> GetById(int id)
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
                    var data = new ITISeatsDistributionsDataModel();
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITISeatsDistributionsDataModel>(dataTable);
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
        
    

        public async Task<DataTable> GetByIDForFee(int id, int Collegeid, int FinancialYearID)
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
                        command.CommandText = "USP_ITI_College_Update";
                        command.Parameters.AddWithValue("@CollegeId", Collegeid);
                        command.Parameters.AddWithValue("@CollegeTradeId", id);
                        command.Parameters.AddWithValue("@FinancialYearID", FinancialYearID);
                        command.Parameters.AddWithValue("@Action", "TradeDetails");

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



        public async Task<DataTable> GetSeatMetrixData(ITISeatsDistributionsDataModel model)
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
                        command.CommandText = "USP_ITISeatMetrix";
                        command.Parameters.AddWithValue("@ManagementTypeID", model.Type);
                        command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);         
                        command.Parameters.AddWithValue("@Action", model.Action);
                        command.Parameters.AddWithValue("@TradeSchemeId", model.TradeSchemeId);
                        command.Parameters.AddWithValue("@TradeLevelId", model.TradeLevelId);
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

        public async Task<int> SaveSeatsDistributions(List<ITISeatMetrixModel> model)
        {
            _actionName = "SaveSeatsDistributions(List<StudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Seat_Ditribution_IU";
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

        public async Task<int> SaveSeatsMatrixlist(List<ITISeatMetrixSaveModel> model)
        {
            _actionName = "SaveSeatsMatrixlist(List<BTERSeatMetrixModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITISeat_MatrixSampleList_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_addStudentEligibleForEnrollmentData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@AcademicYearID", model[0].FinancialYearID);

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

        public async Task<DataTable> PublishSeatMatrix(ITICollegeTradeSearchModel request)
        {
            _actionName = "PublishSeatMatrix(ITICollegeTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIPublishSeatMetrix";

                        command.Parameters.AddWithValue("@AllotmentMasterId", request.AllotmentMasterId);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@TradeLevelId", request.TradeLevelId);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@CreateBy", request.CreateBy);
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

        public async Task<bool> SaveFeeITI(int ModifyBy, int Fee, int ImcFee, int CollegeTradeId)
        {
            _actionName = "SaveFeeITI(ITICollegeMasterModel request)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandText = "USP_ITI_College_Update";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Fee", Fee);
                    command.Parameters.AddWithValue("@UpdatedBy", ModifyBy);
                    command.Parameters.AddWithValue("@ImcFee", ImcFee);
                    command.Parameters.AddWithValue("@CollegeTradeId", CollegeTradeId);
                    //command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    command.Parameters.AddWithValue("@Action", "Updatefees");


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
                var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errorDetails, ex);
            }
        }


    }
}
