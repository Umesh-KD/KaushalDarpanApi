
using Kaushal_Darpan.Models.ReservationRosterModel;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IReservationRosterMasterRepository
    {

        Task<int> SaveData(ReservationRosterMasterModel productDetails);
        Task<DataTable> GetAllData(ReservationRosterSearchModel filterModel);

        Task<bool> DeleteDataByID(ReservationRosterMasterModel productDetails);

        Task<ReservationRosterMasterModel> GetById(int Id);

    }
}
