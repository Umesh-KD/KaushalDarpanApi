using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ViewPlacedStudents;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ViewPlacedStudentsRepository : IViewPlacedStudentsRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ViewPlacedStudentsRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ViewPlacedStudentsRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ViewPlacedStudents body)
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
                        command.CommandText = "USP_ViewPlacedStudent";
                        command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter
                        command.Parameters.AddWithValue("@Id", body.key);
                        command.Parameters.AddWithValue("@CampusID", body.Pk_Id);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);


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


        public async Task<DataTable> GetAllPlacementReportData(PlacementReportSearchData body)
        {
            _actionName = "GetAllPlacementReportData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPlacementDataReport";
                        command.Parameters.AddWithValue("@FromDate", body.FromDate);
                        command.Parameters.AddWithValue("@ToDate", body.ToDate);
                        command.Parameters.AddWithValue("@DepartmentID", 0);
                        command.Parameters.AddWithValue("@CollegeName", body.CollegeName);
                        command.Parameters.AddWithValue("@Branch", body.BranchName);
                        command.Parameters.AddWithValue("@Eng_NonEng", 0);

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

        public async Task<DataTable> GetAllPlacementData()
        {
            _actionName = "GetAllPlacementData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPlacementData";
                        //command.Parameters.AddWithValue("@action", _actionName); // Assuming you are using the action filter
                        //command.Parameters.AddWithValue("@Id", body.key);
                        //command.Parameters.AddWithValue("@CampusID", body.Pk_Id);
                        //command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);

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








