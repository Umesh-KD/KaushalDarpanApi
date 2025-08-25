using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CampusDetailsWeb;
using System.Data;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class CampusDetailsWebRepository : ICampusDetailsWebRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CampusDetailsWebRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CampusDetailsWebRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        public async Task<DataTable> GetAllPost( int postId, int DepartmentID,int StreamID,int FinancialYearID,int InstituteID, string CampusFromDate,string CampusToDate)
            {
            _actionName = "GetAllPost(int postId)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CampusDetailsForWebSite";
                        command.Parameters.AddWithValue("@postId", postId);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@StreamID", StreamID);
                        command.Parameters.AddWithValue("@Academicyear", FinancialYearID);
                        command.Parameters.AddWithValue("@CampusFromDate", CampusFromDate);
                        command.Parameters.AddWithValue("@CampusToDate", CampusToDate);
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);

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


        public async Task<DataTable> GetAllPlacementCompany(CampusDetailsWebSearchModel model)
        {
            _actionName = "GetAllPlacementCompany()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllPlacementCompant";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);

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


        public async Task<DataTable> GetAllPostExNonList(int postId, int DepartmentID, int StreamID, int FinancialYearID, int InstituteID, string CampusFromDate, string CampusToDate)
      
        {
            _actionName = "GetAllPostExNonList(int postId)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CampusDetailsForWebSiteEx_ExNon";

                        command.Parameters.AddWithValue("@postId", postId);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        command.Parameters.AddWithValue("@StreamID", StreamID);
                        command.Parameters.AddWithValue("@Academicyear", FinancialYearID);
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);
                        command.Parameters.AddWithValue("@CampusFromDate", CampusFromDate);
                        command.Parameters.AddWithValue("@CampusToDate", CampusToDate);


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
