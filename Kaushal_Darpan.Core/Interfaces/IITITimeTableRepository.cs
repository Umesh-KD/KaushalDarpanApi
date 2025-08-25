using Kaushal_Darpan.Models.ITITimeTable;
using Kaushal_Darpan.Models.TimeTable;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITITimeTableRepository
    {
        Task<DataTable> GetAllData(ITITimeTableSearchModel model);
        Task<ITITimeTableModel> GetById(int ID);
        Task<List<TradeSubjectDataModel>> GetTimeTableByID(int ID);
        Task<ITI_TimeTableInvigilatorModel> GetInvigilatorByID(int ID, int InstituteID);
        Task<int> SaveData(ITITimeTableModel productDetails);
        Task<int> SaveInvigilator(ITI_TimeTableInvigilatorModel productDetails);
        Task<bool> DeleteDataByID(ITITimeTableModel productDetails);

        //time table branch subject
        Task<List<ITITimeTableModel>> ImportExcelFile(List<ITITimeTableModel> dataTime);
        Task<DataTable> GetSampleTimeTableITI(ITITimeTableSearchModel request);
        Task<DataTable> GetTimeTableTradeSubject(NewITI_TimeTableValidateModel model);
        Task<int> SaveImportExcelData(List<ITITimeTableModel> request);
        Task<int> SaveCBTImportExcelData(List<ITICBTCenterModel> request);
        Task<List<ITICBTCenterModel>> CBTImportExcelFile(List<ITICBTCenterModel> dataTime);
        Task<DataTable> GetAllCBTData(ITICBTCenterModel model);
    }
}
