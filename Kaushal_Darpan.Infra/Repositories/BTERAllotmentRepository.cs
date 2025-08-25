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
using Kaushal_Darpan.Models.BterMeritMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ITIApplication;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class BTERAllotmentRepository : IBTERAllotmentRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public BTERAllotmentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BTERAllotmentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetGenerateAllotment(BTERAllotmentdataModel body)
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
                        
                        //if (body.TradeLevel == 1)
                        //{
                        //    command.CommandText = "USP_BTERAllotment_ENG";
                        //}
                        //else if (body.TradeLevel == 2)
                        //{
                        //    command.CommandText = "USP_BTERAllotment_NonENG";
                        //}
                        //else if (body.TradeLevel == 3)
                        //{
                        //    command.CommandText = "USP_BTERAllotment_Lateral";
                        //}

                        command.CommandText = "USP_BTERAllotment";
                        

                        command.Parameters.AddWithValue("@CourseType",        body.StreamTypeID);
                        command.Parameters.AddWithValue("@AcademicYearID",    body.AcademicYearID);
                        command.Parameters.AddWithValue("@EndTermId",    body.EndTermId);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentId);
                        command.Parameters.AddWithValue("@IsGenerateSecondAllotment", body.IsGenerateSecondAllotment);
                        command.Parameters.AddWithValue("@CreatedBy",         body.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress",         body.IPAddress);
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
        public async Task<DataTable> GetPublishAllotment(BTERAllotmentdataModel body)
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
                        command.CommandText = "USP_BTERPublishAllotment";
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentId);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@StreamTypeID", body.TradeLevel);
                        command.Parameters.AddWithValue("@FinancialYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@EndTermId", body.EndTermId);
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


  
        public async Task<DataTable> AllotmentCounter(BTERSearchModelCounter body)
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
                        command.CommandText = "USP_Get_AllotmentCounter";
                        command.Parameters.AddWithValue("@AllotmentId", body.AllotmentId);
                        command.Parameters.AddWithValue("@DepartmentType", body.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeId", body.StreamTypeID);
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

        public async Task<DataTable> GetShowSeatMetrix(BTERSearchModel body)
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

        public async Task<List<OptionDetailsDataModel>> GetOptionDetailsbyID(BTERSearchModel request)
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

        public async Task<DataTable> GetStudentSeatAllotment(BTERSearchModel body)
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



        public async Task<DataTable> GetAllotmentData(BTERAllotmentModel body)
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
                        command.CommandText = "USP_BTER_AllotmentData";
                        command.Parameters.AddWithValue("@StreamTypeID", body.StreamTypeID);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentMasterId);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@EndTermId", body.EndTermId);             
                        command.Parameters.AddWithValue("@PageSize",body.PageSize);
                        command.Parameters.AddWithValue("@FilterType", body.FilterType);
                        command.Parameters.AddWithValue("@SearchText", body.SearchText);
                        command.Parameters.AddWithValue("@Action", body.Action);
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

        public async Task<DataTable> GetAllotmentReport(BTERAllotmentModel body)
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
                        command.CommandText = "USP_BTER_AllotmentReport";
                        command.Parameters.AddWithValue("@StreamTypeID", body.StreamTypeID);
                        command.Parameters.AddWithValue("@AllotmentMasterId", body.AllotmentMasterId);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);     
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@FilterType", body.FilterType);
                        command.Parameters.AddWithValue("@SearchText", body.SearchText);
                        command.Parameters.AddWithValue("@CollegeId", body.CollegeId);
                        command.Parameters.AddWithValue("@ShiftId", body.ShiftId);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@CollegeCode", body.CollegeCode);
                        command.Parameters.AddWithValue("@StreamCode", body.StreamCode);
                        command.Parameters.AddWithValue("@FeePaid", body.FeePaid);
                        command.Parameters.AddWithValue("@AllotmentStatus", body.AllotmentStatus);
                        command.Parameters.AddWithValue("@Action", body.Action);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);

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

        public async Task<DataTable> UploadAllotmentData(BterUploadAllotmentDataModel model)
        {
            _actionName = "UploadAllotmentData(BterUploadAllotmentDataModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {


                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTER_ALLOTMENT_UPLOAD";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_addStudentEligibleForEnrollmentData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model.AllotmentData));
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@AllotmentMasterId", model.AllotmentMasterId);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", CommonFuncationHelper.GetIpAddress());
                        command.Parameters.AddWithValue("@Action", "UPLOAD");
                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //dataTable = await command.FillAsync_DataTable();


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                        //result = Convert.ToInt32(command.Parameters["@Retval"].Value);// out





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

        public async Task<DataTable> AllotmnetFormateData(BterUploadAllotmentDataModel model)
        {
            _actionName = "AllotmnetFormateData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_ALLOTMENT_UPLOAD";
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@AllotmentMasterId", model.AllotmentMasterId);
                        command.Parameters.AddWithValue("@Action", model.Action);

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
