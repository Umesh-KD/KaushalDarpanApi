using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Models.ReportFeesTransactionModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.ReportFeesTransactionModel.ReportFeesTransaction;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIReportFeesTransactionRepository
    {
        Task<DataTable> GetITIStudentFeesTransactionHistoryRpt(ITIReportFeesTransactionSearchModel model);
       


    }
}
