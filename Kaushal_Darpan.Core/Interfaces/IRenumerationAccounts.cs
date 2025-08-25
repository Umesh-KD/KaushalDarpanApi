using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.RenumerationAccounts;
using Kaushal_Darpan.Models.RenumerationJD;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IRenumerationAccounts
    {
        Task<List<RenumerationAccountsModel>> GetAllData(RenumerationAccountsRequestModel filterModel);
        Task<int> HasDblicateTvNoAndVoucharNo(RenumerationAccountsSaveModel request);
        Task<int> SaveDataApprovedFromAccounts(RenumerationAccountsSaveModel request);
        Task<int> UpdateDataApprovedFromAccounts(RenumerationAccountsSaveModel request);
    }
}
