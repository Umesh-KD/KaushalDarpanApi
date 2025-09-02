using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.IDfFundDetailsModel;
using Kaushal_Darpan.Models.ITIIIPManageDataModel;
using Microsoft.Data.SqlClient;
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
    public class ITIIIPManageRepository : IITIIIPManageRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIIIPManageRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIIIPManageRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataSet> GetAllData(ITIIIPManageDataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataTable = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIPManage";
                        command.Parameters.AddWithValue("@Action", "GetAllData");
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@CollegeID", body.InstituteId);
                        command.Parameters.AddWithValue("@FinancialYearId", body.FinancialYearID);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync();
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

        public async Task<int> SaveIMCReg(ITIIIPManageDataModel request)
        {
            _actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIPManage_IU";
                        command.Parameters.AddWithValue("@RegOffice", request.RegOfficeName);
                        command.Parameters.AddWithValue("@RegisterationID", request.RegOfficeID);
                        command.Parameters.AddWithValue("@RegNumber", request.RegNumber);
                        command.Parameters.AddWithValue("@RegDate", request.RegDate);
                        command.Parameters.AddWithValue("@RegLink", request.RegLink);
                        command.Parameters.AddWithValue("@FinancialYearId", request.FinancialYearID);
                        command.Parameters.AddWithValue("@CollegeID", request.InstituteId);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@IMCMemberDetails", JsonConvert.SerializeObject(request.IMCMemberDetails));

                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<int> SaveFundDetails(IDfFundDetailsModel request)
        {
            _actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_InsertFundWithDeposits";
                        command.Parameters.AddWithValue("@FundID", request.FundID);
                        command.Parameters.AddWithValue("@FinYearQuaterID", request.FinYearQuaterID);
                        command.Parameters.AddWithValue("@FinancialYearId", request.FinancialYearID);
                        command.Parameters.AddWithValue("@PrincipalName", request.PrincipalName);
                        command.Parameters.AddWithValue("@OpeningBalance", request.OpeningBalance);
                        command.Parameters.AddWithValue("@ReceivedAmount", request.ReceivedAmount);
                        command.Parameters.AddWithValue("@Expense", request.Expense);
                        command.Parameters.AddWithValue("@ClosingBalance", request.ClosingBalance);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@InstituteID", request.InsituteID);
                        command.Parameters.AddWithValue("@OtherDepositList", JsonConvert.SerializeObject(request.OtherDepositList));
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();
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
        public async Task<DataTable> GetFundDetailsData(IDfFundSearchDetailsModel body)
        {
            _actionName = "GetFundDetailsData(IDfFundSearchDetailsModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_InsertFundWithDeposits_Get";
                        command.Parameters.AddWithValue("@Action", body.Action);
                        command.Parameters.AddWithValue("@FundID", body.FundID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteId);
                        command.Parameters.AddWithValue("@FinancialYearId", body.FinancialYearID);
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

        public async Task<int> SaveIMCFund(IIPManageFundSearchModel request)
        {
            _actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    var allTrades = new List<TradeList>();


                    allTrades.AddRange(request.SanctionedTradeList.Select(x => { x.TypeId = 1; return x; }));
                    allTrades.AddRange(request.AffilateTradeList.Select(x => { x.TypeId = 2; return x; }));
                    allTrades.AddRange(request.NotAffilateTradeList.Select(x => { x.TypeId = 3; return x; }));

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIPManageFund_IU";
                        // Required SP Parameters
                        command.Parameters.AddWithValue("@Action", "INSERT"); // or "UPDATE" based on logic
                        command.Parameters.AddWithValue("@FundID", request.FundID);
                        command.Parameters.AddWithValue("@IMCRegID", request.IMCRegID); // mapping IMCRegID
                        command.Parameters.AddWithValue("@QuarterID", request.QuarterID);
                        command.Parameters.AddWithValue("@InstalmentPaid", request.InstalmentPaid);
                        command.Parameters.AddWithValue("@InstalmentPending", request.InstalmentPending);
                        command.Parameters.AddWithValue("@SchemeSanctioned", request.SchemeSanctioned);
                        command.Parameters.AddWithValue("@TradeAffiliated", request.TradeAffiliated);
                        command.Parameters.AddWithValue("@TradeNotAffiliated", request.TradeNotAffiliated);
                        command.Parameters.AddWithValue("@InstalmentPaidAmt", request.InstalmentPaidAmt);
                        command.Parameters.AddWithValue("@InstalmentPendingAmt", request.InstalmentPendingAmt);
                        command.Parameters.AddWithValue("@ReasonNotAffiliated", request.Remarks ?? "");
                        command.Parameters.AddWithValue("@OrderDate", request.OrderDate ?? "");
                        command.Parameters.AddWithValue("@FinancialYearId", request.FinancialYearID);
                        command.Parameters.AddWithValue("@CollegeID", request.InstituteId);

                        

                        command.Parameters.AddWithValue("@IMCFundTradeDetails", JsonConvert.SerializeObject(allTrades));
                        // JSON Trade List for OPENJSON()
                        //command.Parameters.AddWithValue("@IMCFundTradeDetails", JsonConvert.SerializeObject(request.SanctionedTradeList));

                        // Extra params
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<ITIIIPManageDataModel> GetById_IMC(int ID)
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
                        command.CommandText = "USP_ITI_IIPManage";
                        command.Parameters.AddWithValue("@RegisterationID", ID);
                        command.Parameters.AddWithValue("@Action", "GetById_IMC");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITIIIPManageDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITIIIPManageDataModel>(dataSet.Tables[0]);
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.IMCMemberDetails = CommonFuncationHelper.ConvertDataTable<List<IIPManageMemberDetailsDataModel>>(dataSet.Tables[1]);
                            }
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

        public async Task<DataTable> GetIMCHistory_ById(int RegID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable data = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIPManage";
                        command.Parameters.AddWithValue("@RegisterationID", RegID);
                        command.Parameters.AddWithValue("@Action", "GetIMCHistory_ById");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        data = await command.FillAsync_DataTable();
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

        public async Task<DataSet> GetAllIMCFundData(IIPManageFundSearchModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataTable = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIPManage";
                        command.Parameters.AddWithValue("@Action", "GetAllIMCFundData");
                        
                        command.Parameters.AddWithValue("@CollegeID", body.InstituteId);
                        command.Parameters.AddWithValue("@FinancialYearId", body.FinancialYearID);
                        command.Parameters.AddWithValue("@RegisterationID", body.IMCRegID);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync();
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

        public async Task<IIPManageFundSearchModel> GetById_IMCFund(int ID)
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
                        command.CommandText = "USP_ITI_IIPManage";
                        command.Parameters.AddWithValue("@FundID", ID);
                        command.Parameters.AddWithValue("@Action", "GetById_IMCFund");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new IIPManageFundSearchModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<IIPManageFundSearchModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                              data.SanctionedTradeList = CommonFuncationHelper.ConvertDataTable<List<TradeList>>(dataSet.Tables[1].AsEnumerable().Where(r => r.Field<int>("TradeTypeID") == 1).CopyToDataTable());
                              data.AffilateTradeList = CommonFuncationHelper.ConvertDataTable<List<TradeList>>(dataSet.Tables[1].AsEnumerable().Where(r => r.Field<int>("TradeTypeID") == 2).CopyToDataTable());
                              data.NotAffilateTradeList = CommonFuncationHelper.ConvertDataTable<List<TradeList>>(dataSet.Tables[1].AsEnumerable().Where(r => r.Field<int>("TradeTypeID") == 3).CopyToDataTable());
                            }
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

        public async Task<int> SaveQuaterlyProgressData(IMCFundRevenue request)
        {
            _actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_QuaterlyFundReport";
                        // Required SP Parameters
                        if (request.ID != 0)
                        {
                            command.Parameters.AddWithValue("@Action", "UPDATE");
                            command.Parameters.AddWithValue("@ID", request.ID);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Action", "INSERT");
                        }
                       
                        command.Parameters.AddWithValue("@IMCFundID", request.IMCFundID);
                        command.Parameters.AddWithValue("@IMCRegID", request.IMCRegID);
                        command.Parameters.AddWithValue("@InterestReceivedAmt", request.InterestReceivedAmt);
                        command.Parameters.AddWithValue("@AdmissionFeesAmt", request.AdmissionFeesAmt);
                        command.Parameters.AddWithValue("@OtherRevenueAmt", request.OtherRevenueAmt);
                        command.Parameters.AddWithValue("@TotalRevenueAmt", request.TotalRevenueAmt);
                        command.Parameters.AddWithValue("@CivilAmt", request.CivilAmt);
                        command.Parameters.AddWithValue("@ToolsAmt", request.ToolsAmt);
                        command.Parameters.AddWithValue("@FurnitureAmt", request.FurnitureAmt);
                        command.Parameters.AddWithValue("@BooksAmt", request.BooksAmt);
                        command.Parameters.AddWithValue("@AdditionalAmt", request.AdditionalAmt);
                        command.Parameters.AddWithValue("@MaintenanceAmt", request.MaintenanceAmt);
                        command.Parameters.AddWithValue("@MiscellaneousAmt", request.MiscellaneousAmt);
                        command.Parameters.AddWithValue("@TotalExpenditureAmt", request.TotalExpenditureAmt);

                        command.Parameters.AddWithValue("@CivilSanctionedAmt", request.CivilSanctionedAmt);
                        command.Parameters.AddWithValue("@ToolsSanctionedAmt", request.ToolsSanctionedAmt);
                        command.Parameters.AddWithValue("@FurnitureSanctionedAmt", request.FurnitureSanctionedAmt);
                        command.Parameters.AddWithValue("@BooksSanctionedAmt", request.BooksSanctionedAmt);
                        command.Parameters.AddWithValue("@AdditionalSanctionedAmt", request.AdditionalSanctionedAmt);
                        command.Parameters.AddWithValue("@MaintenanceSanctionedAmt", request.MaintenanceSanctionedAmt);
                        command.Parameters.AddWithValue("@MiscellaneousSanctionedAmt", request.MiscellaneousSanctionedAmt);
                        command.Parameters.AddWithValue("@TotalSanctionedAmt", request.TotalSanctionedAmt);

                        command.Parameters.AddWithValue("@FundAvailableAmt", request.FundAvailableAmt);

                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        //result = await command.ExecuteNonQueryAsync();
                         result = Convert.ToInt32(await command.ExecuteScalarAsync());
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

        public async Task<DataTable> GetQuaterlyProgressData(int ID)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable data = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_QuaterlyFundReport";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@Action", "GETBYID");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        data = await command.FillAsync_DataTable();
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

        public async Task<IDfFundDetailsModel> GetById_FundDetails(int ID)
        {
            _actionName = "Task<IDfFundDetailsModel> GetById_FundDetails(int ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_InsertFundWithDeposits_Get";
                        command.Parameters.AddWithValue("@Action", "GetRecord");
                        command.Parameters.AddWithValue("@FundID", ID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new IDfFundDetailsModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<IDfFundDetailsModel>(dataSet.Tables[0]);
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.OtherDepositList = CommonFuncationHelper.ConvertDataTable<List<DepositList>>(dataSet.Tables[1]);
                            }
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

        public async Task<int> FinalSubmitUpdate(int id)
        {
            //_actionName = "SaveAllData(AdminUserDetailModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_QuaterlyFundReport";
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@Action", "FinalSubmitUpdate");


                        _sqlQuery = command.GetSqlExecutableQuery();
                        //result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(await command.ExecuteScalarAsync()); 
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

        public async Task<DataSet> GetIIPQuaterlyFundReport(int Id)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_GetQuaterlyFundReport";
                        command.Parameters.AddWithValue("@ID", Id);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    return dataSet;
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


