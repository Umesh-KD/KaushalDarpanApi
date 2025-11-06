using Kaushal_Darpan.Models.ITI_BGTHeadmaster;
using Kaushal_Darpan.Models.ITIBUDGET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITIBudgetCreateRepository
    {
        Task<DataTable> GetAllData(BudgetHeadSearchFilter body);
        Task<DataTable> GetITIBudgetDropdown(ITIBudgetDropdownDataModel model);
    }
}
