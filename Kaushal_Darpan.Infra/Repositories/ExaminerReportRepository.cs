using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.PolytechnicReport;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class ExaminerReportRepository : IExaminerReportRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ExaminerReportRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ExaminerReportRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ExaminerReportDataSearchModel model)
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
                        command.CommandText = "USP_GetExaminerReport";
                        //command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        //command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        //command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        //command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                        //command.Parameters.AddWithValue("@InstituteCode", model.InstituteCode);
                        //command.Parameters.AddWithValue("@InstituteName", model.InstituteName);
                        //command.Parameters.AddWithValue("@ManagementType", model.ManagementType);
                        //command.Parameters.AddWithValue("@DistrictId", model.DistrictId);
                        //command.Parameters.AddWithValue("@Email", model.Email);
                        //command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        //command.Parameters.AddWithValue("@ActiveStatus", model.Status);
                        //command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        //command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        //command.Parameters.AddWithValue("@StreamTypeID", model.StreamTypeID);

                        //command.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                        //command.Parameters.AddWithValue("@PageSize", model.PageSize);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@Action", "GetExaminerPaperCountReport");

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
