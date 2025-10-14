using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITIFeeModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.RevaluationDataModel;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIStudentRevaluationRepository : IITIStudentRevaluationRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIStudentRevaluationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIStudentRevaluationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetStudentRevaluationDetails(ITIStudentRevaluationDataModel body)
        {
            _actionName = "GetTeacherForExaminer()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIGetStudentRevaluationDetailsByRollNo";

                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@DOB", body.DOB);

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
