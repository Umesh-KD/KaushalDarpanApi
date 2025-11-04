using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIFeeModel;
using Kaushal_Darpan.Models.RevaluationDataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIStudentRevaluationRepository
    {
        Task<DataTable> GetStudentRevaluationDetails(ITIStudentRevaluationDataModel filterModel);
        Task<DataTable> GetAllStudentRevaluation(StudentDetailsByRollNoModel filterModel);
        Task<DataTable> SaveRVLPaymentData(RVLStudentDetailsModel body);
        Task<DataTable> GetRVLDetailByStudentApplicationNo(RVLStudentRevalRequestModel body);

        //iti student reval request details
        Task<DataTable> GetAllRevalRequestDetails(ITIRevalRequestStudentDetailsModel filtermodel);

        Task<bool> UploadDocument(ITIRevalRequestStudentDetailsModel body);


        //Update EnrollResponse in BulkExcel
        Task<bool> ImportExcelFile(List<UpdateEnrollResponseBulkExcelModel> model);

        //dynamic Update data through BulkExcel
        Task<bool> DynamicUpdateExcelData(List<Dictionary<string,object>> model , string action);


    }
}
