using Kaushal_Darpan.Models.RenumerationExaminer;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IRenumerationExaminerRepository
    {
        Task<List<RenumerationExaminerModel>> GetAllData(RenumerationExaminerRequestModel filterModel);
        Task<List<TrackStatusDataModel>> GetTrackStatusData(RenumerationExaminerRequestModel filterModel);
        Task<DataTable> GetDataForGeneratePdf(RenumerationExaminerRequestModel filterModel);
        Task<int> SaveDataSubmitAndForwardToJD(RenumerationExaminerPDFModel request);
    }
}
