using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITISeatsDistributionsMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITISeatsDistributionsMasterRepository
    {

        Task<int> SaveData(ITISeatsDistributionsDataModel productDetails);
        Task<DataTable> GetAllData(ITISeatsDistributionsSearchModel filterModel);

        Task<ITISeatsDistributionsDataModel> GetById(int id);
        Task<DataTable> GetByIDForFee(int id, int Collegeid, int FinancialYearID);

        Task<DataTable> GetSeatMetrixData(ITISeatsDistributionsDataModel model);
        Task<int> SaveSeatsDistributions(List<ITISeatMetrixModel> model);

        Task<int> SaveSeatsMatrixlist(List<ITISeatMetrixSaveModel> request);
 
        Task<DataTable> PublishSeatMatrix(ITICollegeTradeSearchModel request);

        Task<bool> SaveFeeITI(int ModifyBy, int Fee, int ImcFee, int CollegeTradeId);



    }
}
