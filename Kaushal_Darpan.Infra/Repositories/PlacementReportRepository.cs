using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.PlacementReport;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class PlacementReportRepository : IPlacementReportRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public PlacementReportRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "PlacementReportRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(PlacementReportSearch filterModel)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPlacementDashReport";
                        command.Parameters.AddWithValue("@Id", filterModel.Id);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@CollegeID", filterModel.CollegeID);
                        command.Parameters.AddWithValue("@StudentName", filterModel.StudentName ?? string.Empty);
                        command.Parameters.AddWithValue("@Gender", filterModel.Gender ?? string.Empty);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@CampusID", filterModel.CampusID);
                        command.Parameters.AddWithValue("@FromAge", filterModel.FromAge);
                        command.Parameters.AddWithValue("@ToAge", filterModel.ToAge);
                        command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter

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

        public async Task<DataTable> GetAllHistory(PlacementReportSearch filterModel)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCampusStudentHistory";
                   
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);
                        command.Parameters.AddWithValue("@CampusID", filterModel.CampusID);
  

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




        #region ITI placement

        public async Task<DataTable> GetITIAllData(ITIPlacementReportSearch filterModel)
        {
            _actionName = "GetITIAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPlacementDashReport";
                        command.Parameters.AddWithValue("@Id", filterModel.Id);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@CollegeID", filterModel.CollegeID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID ?? string.Empty);
                        command.Parameters.AddWithValue("@StudentName", filterModel.StudentName ?? string.Empty);
                        command.Parameters.AddWithValue("@Gender", filterModel.Gender ?? string.Empty);
                        command.Parameters.AddWithValue("@TradeID", filterModel.TradeID ?? string.Empty);
                        command.Parameters.AddWithValue("@CompanyID", filterModel.CompanyID??string.Empty );
                        command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter

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


        #endregion

    }
}








