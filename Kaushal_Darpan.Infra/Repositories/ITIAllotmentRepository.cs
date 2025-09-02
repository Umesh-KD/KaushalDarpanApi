using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Allotment;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ITIApplication;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class ITIAllotmentRepository : IITIAllotmentRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIAllotmentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIAllotmentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetGenerateAllotment(AllotmentdataModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 999999999;
                        //if (body.TradeLevel == 8)
                        //{
                        //    command.CommandText = "USP_ITIAllotment8th";
                        //}
                        //else if (body.TradeLevel == 10)
                        //{
                        //    command.CommandText = "USP_ITIAllotment10th";
                        //}
                        command.CommandText = "USP_ITIAllotment";


                        command.Parameters.AddWithValue("@TradeLevelId", body.TradeLevel);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentId);
                        command.Parameters.AddWithValue("@CreatedBy", body.UserID);
                        command.Parameters.AddWithValue("@IPAddress", body.IPAddress);


                        //command.ExecuteNonQuery();
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


        public async Task<DataTable> AllotmentCounter(SearchModel body)
        {
            _actionName = "AllotmentCounter()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_ITI_AllotmentCounter";
                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@DepartmentType", body.DepartmentID);
                        command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
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

        public async Task<DataTable> GetShowSeatMetrix(SearchModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_ITI_SeatMetrixData";
                        command.Parameters.AddWithValue("@CollegeId", body.InstituteID);
                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
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

        public async Task<List<OptionDetailsDataModel>> GetOptionDetailsbyID(SearchModel request)
        {
            _actionName = "GetOptionDetailsbyID(int PK_ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIAllotment_GetOptions_ByID";
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@TradeLevel", request.TradeLevel);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<OptionDetailsDataModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<OptionDetailsDataModel>>(dataTable);
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

        public async Task<DataTable> GetStudentSeatAllotment(SearchModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_ITI_StudentSeatAllotment";
                        command.Parameters.AddWithValue("@CollegeId", body.StInstituteID);
                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@TradeID", body.TradeID);
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


        public async Task<DataTable> GetAllotmentData(SearchModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_AllotmentData";
                        command.Parameters.AddWithValue("@TradeLevelId", body.TradeLevel);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentMasterId);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@CollegeID", body.CollegeID);
                        command.Parameters.AddWithValue("@TradeID", body.TradeID);
                        command.Parameters.AddWithValue("@TradeSchemeId", body.StreamTypeID);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@FilterType", body.FilterType);
                        command.Parameters.AddWithValue("@SearchText", body.SearchText);
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

        public async Task<DataTable> GetAllotmentStatusList(AllotmentStatusSearchModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_AllotmentStatus_List";
                        command.Parameters.AddWithValue("@DOB", body.DOB);
                        command.Parameters.AddWithValue("@ApplicationNo", body.ApplicationNo);
                        command.Parameters.AddWithValue("@DepartmentId", body.DepartmentId);
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


        public async Task<DataTable> GetPublishAllotment(AllotmentdataModel body)
        {
            _actionName = "GetPublishAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 999999999;
                        command.CommandText = "USP_ITIPublishAllotment";
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentId);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@TradeLevelId", body.TradeLevel);
                        command.Parameters.AddWithValue("@FinancialYearID", body.AcademicYearID);
                        //command.Parameters.AddWithValue("@EndTermId", body.EndTermId);
                        //command.ExecuteNonQuery();
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

        public async Task<DataTable> GetAllotmentReport(SearchModel body)
        {
            _actionName = "USP_BTER_AllotmentReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_AllotmentReport";
                        command.Parameters.AddWithValue("@TradeLevelId", body.TradeLevel);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentMasterId);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@CollegeId", body.InstituteID);
                        command.Parameters.AddWithValue("@TradeSchemeId", body.TradeSchemeId);                    
                        command.Parameters.AddWithValue("@CollegeCode", body.CollegeCode);
                        command.Parameters.AddWithValue("@TradeCode", body.TradeCode);                      
                        command.Parameters.AddWithValue("@ManagementTypeID", body.ManagementTypeID); 
                        command.Parameters.AddWithValue("@RepotingStatus", body.ReportingStatus);                        
                        
                        command.Parameters.AddWithValue("@Action", "");

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

        #region Direct Admission

        public async Task<DataTable> GetAllData(ITIDirectAllocationSearchModel body)
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
                        command.CommandText = "USP_GetITIDirectAllotmentData";

                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@Action", "GetAllData");

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

        public async Task<DataTable> StudentDetailsList(ITIDirectAllocationSearchModel body)
        {
            _actionName = "StudentDetailsList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIDirectAllotmentData";

                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@Action", "_StudentDetailsList");

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


        public async Task<DataTable> GetAllDataPhoneVerify(ITIDirectAllocationSearchModel body)
        {
            _actionName = "GetAllDataPhoneVerify()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIDirectAllotmentData";

                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        //command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@Action", "GetDataPhoneVerifyApplication");

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

        public async Task<DataSet> GetStudentDetails(ITIDirectAllocationSearchModel body)
        {
            _actionName = "GetAllDataPhoneVerify()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataTable = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIDirectAllotmentData";

                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        //command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@Action", "GetDataPhoneVerifyApplication");

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




        public async Task<int> UpdateAllotments(ITIDirectAllocationDataModel request)
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
                        command.CommandText = "USP_GetITIDirectAllotmentData";
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@AcademicYearID", request.AcademicYearID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@TradeID", request.TradeID);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@CollegeTradeID", request.CollegeTradeID);
                        command.Parameters.AddWithValue("@TradeLevel", request.TradeLevel);
                        command.Parameters.AddWithValue("@ShiftUnit", request.ShiftUnit);
                        command.Parameters.AddWithValue("@AllotedCategory", request.AllotedCategory);
                        command.Parameters.AddWithValue("@SeatMetrixColumn", request.SeatMetrixColumn);
                        command.Parameters.AddWithValue("@SeatMetrixId", request.SeatMetrixId);
                        command.Parameters.AddWithValue("@DirectAdmissionType", request.DirectAdmissionType);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Action", "SaveUpdateAllotments");
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

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
        public async Task<int> RevertAllotments(ITIDirectAllocationDataModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "RevertAllotments(GroupMaster request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIDirectAllotmentData";
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@AcademicYearID", request.AcademicYearID);
                        command.Parameters.AddWithValue("@CollegeTradeID", request.CollegeTradeID);
                        command.Parameters.AddWithValue("@SeatMetrixColumn", request.SeatMetrixColumn);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Action", "_RevertAllotments");
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

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

        public async Task<DataTable> GetTradeListByCollege(ITIDirectAllocationSearchModel body)
        {
            _actionName = "GetTradeListByCollege()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIDirectAllotmentData";

                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        //command.Parameters.AddWithValue("@TradeLevel", body.TradeLevel);
                        command.Parameters.AddWithValue("@Action", "GetTradeListByCollege");

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
        public async Task<DataTable> ShiftUnitList(ITIDirectAllocationSearchModel body)
        {
            _actionName = "ShiftUnitList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIDirectAllotmentData";

                        command.Parameters.AddWithValue("@CollegeTradeID", body.CollegeTradeID);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        //command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        //command.Parameters.AddWithValue("@TradeID", body.TradeID);
                        command.Parameters.AddWithValue("@Action", "ShiftUnitList");

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


        #endregion







        #region "Student Withdran Request"
        public async Task<int> StudentSeatWithdrawRequest(StudentthdranSeatModel request)
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
                        command.CommandText = "USP_StudentWithdraw_Seat";
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@CollegeID", request.CollegeID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@DoucmentName", request.DoucmentName);
                        command.Parameters.AddWithValue("@AllotmentId", request.AllotmentId);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
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
        #endregion


    }
}
