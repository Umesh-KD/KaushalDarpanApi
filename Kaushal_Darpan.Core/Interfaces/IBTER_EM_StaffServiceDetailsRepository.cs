using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBTER_EM_StaffServiceDetailsRepository
    {
        Task<int> Save_StaffTrainingDetails(StaffTrainingDetailDataModel body);
        Task<DataTable> StaffTrainingDetails_GetData(StaffTrainingDetailSearchData body);
        Task<bool> StaffTrainingDetails_DeleteById(StaffTrainingDetailSearchData request);

        Task<int> StaffTrainingStatusUpdate(StaffTrainingStatusUpdateDataModel body);

        Task<DataTable> StaffTrainingHTS_GetData(StaffTrainingDetailSearchData body);

        Task<int> StaffTrainingDocUpdate(StaffTrainingDetailDataModel body);

        //// BTER Staff Transfer System
        Task<DataTable> GetStaffPersonalDetails(BTER_GetStaffPersonalDetailsModel Model);

        Task<int> BTER_EM_TransferSystem_IU(BTER_EM_TransferSystemModule body);

        Task<DataTable> GetEM_TransferSystemData(EM_TransferSystemSearchModel Model);
        Task<DataTable> GetEM_TransferSystemEmployeeStatus(EM_TransferSystemSearchModel Model);

        Task<bool> EM_TransferSystemUpdatePocessManage(EM_TransferSystemSearchModel request);

        Task<int> EM_TransferSystemUpdateStatus(TransferSystemUpdateDataModel body);
        Task<int> TransferSystemEXTStatusUpdate(TransferSystemUpdateDataModel body);
        Task<int> TransferSystemGeneratorUpdate(TransferSystemUpdateDataModel body);


        Task<int> AddTransferSystemManualRequest(BTERStaffManualRequestModel body);
    }
}
