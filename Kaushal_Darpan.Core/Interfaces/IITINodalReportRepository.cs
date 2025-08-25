using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.ITITimeTable;
using Kaushal_Darpan.Models.NodalApperentship;
using Kaushal_Darpan.Models.ScholarshipMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITINodalReportRepository
    {
        Task<DataTable> SaveData(ITIPMNAM_MelaReportBeforeAfterModal body);
        Task<DataTable> GetAllData(ITIPMNAM_Report_SearchModal body);
        Task<DataTable> GetQuaterProgressList(ITIApprenticeshipWorkshop body);
        Task<DataTable> PMNAM_report_DeletebyID(int PKID );
        Task<DataTable> QuaterListDelete(int PKID );
        Task<DataTable> GetReportDatabyID(int PKID );
        Task<DataTable> GetQuaterReportById(int PKID );
        Task<DataTable> GetAAADetailsById(int PKID);
        Task<DataTable> SaveDataMelaReportCount(ITIPMNAMAppApprenticeshipReportEntity body);
        Task<DataTable> GetAllData(int UserID,int DistrictID);
        Task<DataTable> DeleteData_Pmnam_mela_Report(ITIPMNAMAppApprenticeshipReportEntity body);
        Task<int> Save_QuaterReport(ITIApprenticeshipWorkshop body);
        Task<int> SavePassoutReport(ITIApprenticeshipRegPassOutModel body);
        Task<int> SaveFresherReport(ITIApprenticeshipRegPassOutModel body);

        Task<DataTable> Save_ITIWorkshopProgressRPT(List<workshopProgressRPTList> body);
        Task<DataTable> Submit_Apprenticeship_data(ApprenticeshipEntryDto entry, string businessNameCsv);

        Task<DataTable> Get_WorkshopProgressRPT_AllData(WorkshopProgressRPTSearchModal body);

        Task<DataTable> WorkshopProgressRPTDelete_byID(int PKID);

        Task<DataTable> Get_ApprenticeshipRegistrationReportAllData(ApprenticeshipRegistrationSearchModal body);
        Task<DataTable> Get_PassingRegistrationReportAllData(ApprenticeshipRegistrationSearchModal body);
        Task<DataTable> Get_FresherRegistrationReportAllData(ApprenticeshipRegistrationSearchModal body);

        Task<DataTable> ApprenticeshipRegistrationRPTDelete_byID(int PKID);
        Task<int> PassoutRegistrationRPTDelete_byID(int PKID);
        Task<int> FresherRegistrationRPTDelete_byID(int PKID);

        Task<DataTable> GetSamplePassoutStudent(ITITimeTableSearchModel request);
        Task<DataTable> SampleImportExcelFileFresher(ITITimeTableSearchModel request);
        Task<DataTable> MelaSampleImportExcelFile(ITITimeTableSearchModel request);
    }
}
