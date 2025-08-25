using Kaushal_Darpan.Models.GenerateAdmitCard;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.PreExamStudent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IGenerateAdmitCardRepository
    {
        Task<DataTable> GetGenerateAdmitCardData(GenerateAdmitCardSearchModel model);
        Task<int> UpdateAdmitCard(List<GenerateAdmitCardModel> model);
        Task<List<DownloadDataPagingListModel>> GetGenerateAdmitCardDataBulk(GenerateAdmitCardSearchModel model);
        Task<List<DownloadDataPagingListModel>> ITIGetGenerateAdmitCardDataBulk(GenerateAdmitCardSearchModel model);
        Task<List<DownloadDataPagingListModel>> GetGenerateAdmitCardDataBulk_InsituteWise(GenerateAdmitCardSearchModel model);

        Task<List<DownloadDataPagingListModel>> GetITIGenerateAdmitCardDataBulk_CollegeWise(GenerateAdmitCardSearchModel model);


    }
}
