using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.MenuMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITISeatIntakeMasterRepository
    {
        Task<int> SaveSeatIntakeData(BTERSeatIntakeDataModel productDetails);
        Task<List<BTERSeatIntakeDataModel>> GetAllData(BTERSeatIntakeSearchModel request);
        Task<DataTable> GetTradeAndColleges(ITICollegeTradeSearchModel request);
        
       
        Task<BTERSeatIntakeDataModel>GetById(int id);
        Task<bool> DeleteDataByID(BTERSeatIntakeDataModel productDetails);
        Task<DataTable> ITIManagementType();
        Task<DataTable> ITITradeScheme();
        Task<DataTable> GetITICollegesByManagementType(ITICollegeTradeSearchModel request);
        Task<DataTable> getITITrade(ITICollegeTradeSearchModel request);
        Task<DataTable> GetSampleTradeAndColleges(ITICollegeTradeSearchModel request);

        Task<DataTable> SeatMatixSecondAllotment(ITICollegeTradeSearchModel request);

        Task<DataTable> SeatMatixInternalSliding(ITICollegeTradeSearchModel request);
        Task<bool> UpdateActiveStatusByID(BTERSeatIntakeDataModel productDetails);

        Task<DataTable> GetActiveSeatIntake(BTERSeatIntakeSearchModel request);

        Task<int> SaveSanctionOrderData(SanctionOrderModel model);

        Task<DataTable> GetSanctionOrderData(SanctionOrderModel model);

        
        Task<SanctionOrderModel> GetSanctionOrderByID(int id);


    }
}
