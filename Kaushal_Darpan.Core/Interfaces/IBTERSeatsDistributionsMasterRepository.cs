using Kaushal_Darpan.Models.BTEReatsDistributionsMaster;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITISeatsDistributionsMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBTERSeatsDistributionsMasterRepository
    {

        Task<int> SaveData(BTERSeatsDistributionsDataModel productDetails);
        Task<DataTable> GetAllData(BTERSeatsDistributionsSearchModel filterModel);

        Task<BTERSeatsDistributionsDataModel> GetById(int id);

        Task<DataTable> GetSeatMetrixData(BTERSeatsDistributionsDataModel model);
        Task<int> SaveSeatsDistributions(List<BTERSeatMetrixModel> model);
        Task<DataTable> SaveSeatsMatrixlist(List<BTERSeatMetrixSaveModel> model);

        Task<DataTable> GetDirectTradeAndColleges(BTERCollegeTradeSearchModel request);

        Task<DataTable> GetTradeAndColleges(BTERCollegeTradeSearchModel request);
        Task<DataTable> GetSampleTradeAndColleges(BTERCollegeTradeSearchModel request);
        Task<DataTable> GetSampleSeatmatrixAndColleges(BTERCollegeTradeSearchModel request);
        Task<DataTable> BTERManagementType();
        Task<DataTable> BTERTradeScheme();
        Task<DataTable> GetBTERCollegesByManagementType(BTERCollegeTradeSearchModel request);
        Task<DataTable> getBTERTrade(BTERCollegeTradeSearchModel request);

        Task<DataTable> CollegeBranches(BTERCollegeTradeSearchModel request);
        Task<DataTable> GetCollegeBrancheByID(int BranchID);
        Task<DataTable> DeleteCollegeBrancheByID(int BranchID, int UserID);
        Task<DataTable> StatusChangeByID(int BranchID, int ActiveStatus ,int UserID);
        Task<DataTable> GetBranchesStreamTypeWise(BranchStreamTypeWiseSearchModel request);
        Task<DataTable> SaveCollegeBranches(BTERCollegeTradeSearchModel request);
        Task<DataTable> PublishSeatMatrix(BTERCollegeTradeSearchModel request);

        Task<DataTable> SeatMatixSecondAllotment(BTERCollegeTradeSearchModel request);

        Task<DataTable> SeatMatixInternalSliding(BTERCollegeTradeSearchModel request);

    }
}
