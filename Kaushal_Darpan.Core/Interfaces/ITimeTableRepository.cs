using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.HostelManagement;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.TimeTable;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ITimeTableRepository
    {
        Task<DataTable> GetAllData(TimeTableSearchModel model);
        Task<DataTable> GetSampleTimeTable(TimeTableSearchModel request);
        Task<TimeTableModel> GetById(int ID);
        Task<List<BranchSubjectDataModel>> GetTimeTableByID(int ID);
        Task<List<TimeTableModel>> ImportExcelFile(List<TimeTableModel> dataTime);
        Task<int> SaveImportExcelData(List<TimeTableModel> request);
        Task<TimeTableInvigilatorModel> GetInvigilatorByID(int ID, int InstituteID);
        Task<int> SaveData(TimeTableModel productDetails);
        Task<int> SaveInvigilator(TimeTableInvigilatorModel productDetails);
        Task<bool> DeleteDataByID(TimeTableModel productDetails);
    
        //time table branch subject
        Task<DataTable> GetTimeTableBranchSubject(TimeTableValidateModel model);
        Task<int> SaveTimeTableWorkflow(List<VerifyTimeTableList> model);
        Task<DataTable> VerificationTimeTableList(TimeTableSearchModel model);
        Task<int> TimeTable_News_IU(List<VerifyTimeTableList> model);


    }
}
