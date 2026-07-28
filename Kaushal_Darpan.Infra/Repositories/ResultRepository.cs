using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Results;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ResultRepository : IResultRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ResultRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ResultRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        #region Result
        public async Task<DataTable> GetStudentResult(StudentResultSearchModal model)
        {
            _actionName = "GetStudentResult(StudentResultSearchModal model)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    if (model.ResultTypeID == 1 && model.DepartmentID == 1) // main result
                    {
                        command.CommandText = "USP_Result_Generate_For_BTER";

                        command.Parameters.AddWithValue("@ResultTypeID", model.ResultTypeID);
                        // command.Parameters.AddWithValue("@ResultType", model.ResultType == "main" ? 1 : model.ResultType == "ufm" ? 3 : 2);
                    }
                    else if (model.ResultTypeID == 2 && model.DepartmentID == 1) // reval result
                    {
                        command.CommandText = "USP_Reval_Result_Generate_For_BTER";
                    }
                    else if (model.ResultType == "ufm" && model.DepartmentID == 1)
                    {
                        command.CommandText = "USP_UFM_Result_Generate_For_BTER";
                    }
                    else if (model.ResultType == "main" && model.DepartmentID == 2)
                    {
                        command.CommandText = "USP_Result_Generate_For_ITI";
                        //  command.Parameters.AddWithValue("@ResultType", model.ResultType == "main" ? 1 : model.ResultType == "ufm" ? 3 : 2);
                    }
                    else if (model.ResultType == "revaluation" && model.DepartmentID == 2)
                    {
                        command.CommandText = "USP_Reval_Result_Generate_For_ITI";
                    }


                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    //command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                    command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                    command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                    command.Parameters.AddWithValue("@ModifyBy", model.UserID);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    command.Parameters.AddWithValue("@SchemeID_f", model.SchemeID);
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
        }
        #endregion

        public async Task<DataTable> GetGeneratedResultDetails(StudentResultSearchModal model)
        {
            _actionName = "GetGeneratedResultDetails(StudentResultSearchModal model)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.CommandText = "USP_GetGeneratedResultDetails_BTER";

                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    //command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                    command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                    command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                    command.Parameters.AddWithValue("@ModifyBy", model.UserID);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
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
        }

        public async Task<DataTable> Publish_Unpublish_BTER_Result(Publish_Unpublish_BTER_ResultDataModel model)
        {
            _actionName = "Publish_Unpublish_BTER_Result(Publish_Unpublish_BTER_ResultDataModel model)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.CommandText = "USP_Publish_Unpublish_BTER_Result";

                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    //command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                    command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                    command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                    command.Parameters.AddWithValue("@ModifyBy", model.UserID);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
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
        }
    }
}