using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITI_AdmissionReportsRepository: I_ITI_AdmissionReports
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private readonly string _IPAddress;

        public ITI_AdmissionReportsRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ReportRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();

        }

        public async Task<DataSet> GetITISeatOffered()
        {
            DataSet ds = new DataSet();

            using (var command = await _dbContext.CreateCommandAsync())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_Get_ITI_RPT_Admission_SeatOffered";

                ds = await command.FillAsync();
            }

            return ds;
        }
    }
}
