
using Kaushal_Darpan.Models.ITI_InstructorModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITI_BGTHeadmasterRepository
    {
        Task<int> SaveBGTHeadmasterData(ITI_BGT_HeadMasterDataModel model);
        Task<DataTable> GetBGTHeadmasterDataByID(int id);
        Task<int> deleteInstructorDataByID(int id);
        //Task<DataTable> GetCenterSuperitendentReportData(ITICollegeStudentMarksheetSearchModel model);
        Task<DataTable> GetBGTHeadmasterData(ITI_BGT_HeadMasterSearchModel model);
        Task<int> DeleteBudgetHeadById(int HeadId, int UserID);
        Task<int> SaveUCHeadData_ITI_BGT(ITI_BGT_HeadMasterDataModel request);
        Task<DataTable> GetUCHeadData_ITI_BGT(ITI_BGT_HeadMasterSearchModel model);
        Task<DataTable> GetUCHeadDataById_ITI_BGT(int id);
        Task<int> DeleteUCHeadById_ITI_BGT(int HeadId, int UserID);
    }
}
