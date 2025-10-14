using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.MenuMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIDataMasterRepository
    {

        Task<DataTable> GetAllData(DataListSearchModel request);



        Task<DataTable> GetStudentCorrectionListData(StudentCorrectionMasterSearchModel filterModel);

        Task<DataTable> GetStudentCorrectionDataByID(StudentCorrectionMasterSearchModel filterModel);

        Task<bool> SaveStudentCorrectionData(StudentCorrectionMasterSearchModel productDetails);
    }
}
