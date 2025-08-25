using Kaushal_Darpan.Models.CitizenSuggestion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICitizenSuggestionRepository
    {
        Task<DataTable> GetAllData(CitizenSuggestionSearchModel model);
        Task<DataTable> GetSRNumberData(CitizenSuggestionSearchSRModel model);
        Task<DataTable> GetSRNDataList(CitizenSuggestionSearchModel model);
        //Task<bool> SaveData(CitizenSuggestion request);
        Task<DataTable> SaveData(CitizenSuggestion request);
        Task<bool> SaveReplayData(ReplayQuery request);
        Task<bool> SaveUserRating(UserRatingDataModel request);
        Task<CitizenSuggestion> GetByID(int PK_ID);

        Task<CitizenSuggestion> GetByMobileNo(string Mobile);
    }
}
