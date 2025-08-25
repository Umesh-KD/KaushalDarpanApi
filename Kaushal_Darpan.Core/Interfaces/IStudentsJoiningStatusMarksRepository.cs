
using Kaushal_Darpan.Models.StudentsJoiningStatusMarks;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStudentsJoiningStatusMarksRepository
    {

        Task<DataTable> GetAllData(StudentsJoiningStatusMarksSearchModel filterModel);
        Task<DataTable> GetAllReuploadDocumentList(StudentsJoiningStatusMarksSearchModel filterModel);
        Task<DataTable> CheckAllot(StudentsJoiningStatusMarksSearchModel filterModel);
        Task<AllotmentReportingModel> GetAllotmentdata(StudentsJoiningStatusMarksSearchModel filterModel);
        Task<AllotmentReportingModel> GetCorrectDocumentdata(StudentsJoiningStatusMarksSearchModel filterModel);
        Task<int> SaveData(StudentsJoiningStatusMarksModel productDetails);
        Task<int> SaveReporting(AllotmentReportingModel productDetails);
        Task<int> SaveCorrectDocument(AllotmentReportingModel productDetails);
        Task<DataTable> GetSeatAllotmentData(StudentsJoiningStatusMarksSearchModel body);
        Task<DataTable> GetUpgradedbyUpwardList(StudentsJoiningStatusMarksSearchModel body);

    }
}
