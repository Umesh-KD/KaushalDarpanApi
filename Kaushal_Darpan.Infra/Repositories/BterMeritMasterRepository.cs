using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BterMeritMaster;
using Kaushal_Darpan.Models.ItiMerit;
using Kaushal_Darpan.Models.ITINCVT;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class BterMeritMasterRepository : IBterMeritMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private readonly string _IPAddress;

        public BterMeritMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "TimeTableRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(BterMeritSearchModel model)
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
                        command.CommandTimeout = 999999999;
                        if (model.MeritMasterId == 1 || model.MeritMasterId == 2)
                        {
                            command.CommandText = "USP_BTERFirstMerit";
                        }
                        else if (model.MeritMasterId == 3 || model.MeritMasterId == 4)
                        {
                            command.CommandText = "USP_BTERSecondMerit";
                        }


                        //command.CommandText = "USP_BTERMerit";

                        command.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@Action", "SELECT");
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@MeritMasterId", model.MeritMasterId);
                        command.Parameters.AddWithValue("@SearchText", model.SearchText);
                        command.Parameters.AddWithValue("@PageSize", model.PageSize);
                        command.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@CategoryId", model.Category);
                        command.Parameters.AddWithValue("@Gender", model.Gender);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

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

        public async Task<DataTable> GenerateMerit(BterMeritSearchModel model)
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
                        command.CommandTimeout = 999999999;
                        if (model.MeritMasterId == 1 || model.MeritMasterId == 2)
                        {
                            command.CommandText = "USP_BTERFirstMerit";
                        }
                        else if (model.MeritMasterId == 3 || model.MeritMasterId == 4)
                        {
                            command.CommandText = "USP_BTERSecondMerit";
                        }
                        command.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@Action", "GENERATE");
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@MeritMasterId", model.MeritMasterId);
                        command.Parameters.AddWithValue("@SearchText", model.SearchText);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@CategoryId", model.Category);
                        command.Parameters.AddWithValue("@Gender", model.Gender);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

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

        public async Task<DataTable> PublishMerit(BterMeritSearchModel model)
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
                        command.CommandTimeout = 999999999;
                        if (model.MeritMasterId == 1 || model.MeritMasterId == 2)
                        {
                            command.CommandText = "USP_BTERFirstMerit";
                        }
                        else if (model.MeritMasterId == 3 || model.MeritMasterId == 4)
                        {
                            command.CommandText = "USP_BTERSecondMerit";
                        }
                        command.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@Action", "PUBLISH");
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@MeritMasterId", model.MeritMasterId);
                        command.Parameters.AddWithValue("@SearchText", model.SearchText);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@CategoryId", model.Category);
                        command.Parameters.AddWithValue("@Gender", model.Gender);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

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


        public async Task<DataTable> UploadMeritdata(BterUploadMeritDataModel model)
        {
            _actionName = "SaveSeatsMatrixlist(List<BTERSeatMetrixModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {


                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTER_UPLOAD_MERIT";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_addStudentEligibleForEnrollmentData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model.MeritData));
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@AllotmentMasterId", model.AllotmentMasterId);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", CommonFuncationHelper.GetIpAddress());
                        //command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        //command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //dataTable = await command.FillAsync_DataTable();


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                        // result = Convert.ToInt32(command.Parameters["@Retval"].Value);// out





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


        public async Task<DataTable> MeritFormateData(BterMeritSearchModel model)
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
                        command.CommandText = "USP_BTER_GET_MERIT_FORMAT";
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

        public async Task<DataTable> MeritReport(BterMeritSearchModel model)
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
                        command.CommandTimeout = 999999999;

                        command.CommandText = "USP_BTERMeritReport";



                        //command.CommandText = "USP_BTERMerit";

                        command.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);
                        command.Parameters.AddWithValue("@Action", "SELECT");
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@EndTermId", model.EndTermId);
                        command.Parameters.AddWithValue("@MeritMasterId", model.MeritMasterId);
                        command.Parameters.AddWithValue("@SearchText", model.SearchText);
                        command.Parameters.AddWithValue("@PageSize", model.PageSize);
                        command.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@CategoryId", model.Category);
                        command.Parameters.AddWithValue("@Gender", model.Gender);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

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
