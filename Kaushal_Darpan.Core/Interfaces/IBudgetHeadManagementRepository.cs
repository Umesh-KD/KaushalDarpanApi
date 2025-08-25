using Kaushal_Darpan.Models.ITIBUDGET;
using Kaushal_Darpan.Models.NodalApperentship;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBudgetHeadManagementRepository
    {        
        
        Task<DataTable> GetAllData(BudgetHeadSearchFilter body);
        Task<DataTable> GetAllBudgetManagementData(BudgetHeadSearchFilter body);
        Task<int> Save_CollegeBudgetAlloted(CollegeBudgetAllotedModel body);
        //budget utilization
        Task<int> Save_CollegeBudgetUtilizations(List<CollegeBudgetUtilizationModel> request);
        Task<DataTable> GetUtilizationsData(BudgetHeadSearchFilter model);
        Task<int> Save_CollegeBudgetRequest(BudgetRequestModel request);
        Task<DataTable> GetBudgetRequestData(BudgetHeadSearchFilter model);

        


    }

}
