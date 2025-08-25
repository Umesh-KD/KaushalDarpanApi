
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

        Task<DataTable> GetBGTHeadmasterData();
    }
}
