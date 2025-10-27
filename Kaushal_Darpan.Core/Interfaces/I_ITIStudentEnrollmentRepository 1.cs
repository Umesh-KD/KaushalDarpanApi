using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using ResponseData = Kaushal_Darpan.Models.Student.ResponseData;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITIStudentEnrollmentRepository
    {
        Task<DataTable> GetStudentAdmitted(PreExamStudentModel model);
        Task<int> SaveAdmittedFinalStudentData(List<StudentMarkedModelForJoined> model);
        Task<int> updateOnResponseData(List<ResponseData> model);
        Task<int> updateLogIdOnData(NCVTChunkInfoDataModel model);



        Task<List<ITITraineeUploadModel>> GetNCVTStudentData(NCVTChunkInfoDataModel model);
        Task<DataTable> GetNcvtStudentData_Chunks(ChunksSearchModel model);

        Task<DataTable> GetNcvt_APIDetails();


        //uploadTraineeData log api 
        Task<bool> SaveUploadTraineeLogs(UploadTrainee_LogsModel request);
      
    }


}
