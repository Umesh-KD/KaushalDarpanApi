using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.CompanyMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IMarksheetIssueDateRepository
    {
        Task<bool> SaveData(MarksheetIssueDataModels productDetails);
        Task<DataTable> GetAllData(MarksheetIssueSearchModel filterModel);
        Task<MarksheetIssueDataModels> GetById(int MarksheetIssueDataId);
        Task<bool> DeleteDataByID(MarksheetIssueDataModels productDetails);
    }
}
