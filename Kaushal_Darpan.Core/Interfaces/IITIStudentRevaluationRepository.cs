using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIFeeModel;
using Kaushal_Darpan.Models.RevaluationDataModel;
using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
