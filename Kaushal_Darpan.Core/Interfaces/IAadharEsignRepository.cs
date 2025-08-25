using Kaushal_Darpan.Models.AadhaarEsignAuth;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IAadharEsignRepository
    {
        Task<EsignDataHistoryReponseModel> GetEsignDataHistory(EsignDataHistoryRequestModel model);
        Task<int> SaveEsignDataHistory(EsignDataHistoryRequestModel model);
    }
}
