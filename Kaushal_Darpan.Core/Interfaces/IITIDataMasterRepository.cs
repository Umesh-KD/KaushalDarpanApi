using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.MenuMaster;
using Kaushal_Darpan.Models.Student;
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

        Task<DataTable> GetBTERStudentDetailsList(BTERStudentDetailsMasterSearchModel filterModel);

        Task<DataSet> GetStudentDetailsBYID(BTERStudentDetailsMasterSearchModel filterModel);


        Task<DataTable> GetStudentCorrectionDataByID(StudentCorrectionMasterSearchModel filterModel);

        Task<bool> SaveStudentCorrectionData(StudentCorrectionMasterSearchModel productDetails);

        Task<DataTable> GetTraineeLogsList(UploadTrainee_LogsModel filtermodel);
        Task<List<ResultModel>> UploadStatusCheck(NCVTUploadStatusCheckDataModel model);
        Task<DataTable> GetNcvt_APIDetails();
        Task<bool> SaveUploadTraineeLog(UploadTrainee_LogsModel request);
    }
}
