using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ReportFeesTransactionModel;
using Newtonsoft.Json;
using System.Data;
using static Kaushal_Darpan.Models.ReportFeesTransactionModel.ReportFeesTransaction;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIReportFeesTransactionRepository : IITIReportFeesTransactionRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        public ITIReportFeesTransactionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ReportRepository";
        }

        #region StudentFeesTransactionHistory
        public async Task<DataTable> GetITIStudentFeesTransactionHistoryRpt(ITIReportFeesTransactionSearchModel model)
        {
            _actionName = "GetITIStudentFeesTransactionHistoryRpt(ITIReportFeesTransactionModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Rpt_GetITIStudentFeesTransactionHistory";
                        command.Parameters.AddWithValue("@action", "_GetITIStudentFeesTransactionHistory");
                        //command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@TransactionType", model.TransactionType);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@TransactionId", model.TransactionId);
                        //command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync_DataTable();
                    }
                    return ds;
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







