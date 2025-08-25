using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.StudentMaster;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class StudentJanaadharRepository : IStudentJanaadharRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public StudentJanaadharRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "StudentJanaadharRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(StudentJanaadhar filterModel)
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
                        command.CommandText = "USP_GetStudentsJanaadhar";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);

                        command.Parameters.AddWithValue("@StudentName", filterModel.StudentName ?? string.Empty);
                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo ?? string.Empty);
                        command.Parameters.AddWithValue("@Gender", filterModel.Gender ?? string.Empty);
                        command.Parameters.AddWithValue("@MobileNo", filterModel.MobileNo ?? string.Empty);
                        command.Parameters.AddWithValue("@JanAadharNo", filterModel.JanAadharNo ?? string.Empty);
                        command.Parameters.AddWithValue("@JanAadharStatus", filterModel.JanAadharStatus ?? string.Empty);
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
        public async Task<DataTable> GetStudentsJanAadharData(StudentJanaadhar filterModel)
        {
            _actionName = "GetStudentsJanAadharData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentsJanaadhar";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);

                        command.Parameters.AddWithValue("@StudentName", filterModel.StudentName ?? string.Empty);
                        command.Parameters.AddWithValue("@EnrollmentNo", filterModel.EnrollmentNo ?? string.Empty);
                        command.Parameters.AddWithValue("@Gender", filterModel.Gender ?? string.Empty);
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);
                        command.Parameters.AddWithValue("@JanAadharNo", filterModel.JanAadharNo ?? string.Empty);
                        command.Parameters.AddWithValue("@JanAadharStatus", filterModel.JanAadharStatus ?? string.Empty);
                        command.Parameters.AddWithValue("@action", "GetStudentsJanAadharData"); // Assuming you are using the action filter

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








