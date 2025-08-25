using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.PreExamStudent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IGenerateEnrollRepository
    {
        Task<DataTable> GetGenerateEnrollData(GenerateEnrollSearchModel model);
        Task<int> SaveEnrolledData(List<GenerateEnrollMaster> model);
        Task<int> OnPublish(List<GenerateEnrollMaster> model);
        Task<bool> SaveApplicationWorkFlow(GenerateEnrollSearchModel model);

        Task<int> ChangeEnRollNoStatus(GenerateEnrollSearchModel model);
        Task<DataTable> GetPublishedEnRollData(GenerateEnrollSearchModel model);
        Task<DataTable> GetEligibleStudentButPendingForVerification(GenerateEnrollSearchModel model);
        Task<int> SaveEligibleStudentButPendingForVerification(List<EligibleStudentButPendingForVerification> model);
        Task<DataTable> GetEligibleStudentVerified(GenerateEnrollSearchModel model);
        Task<int> StudentEnrollment_RegistrarStatus(List<EligibleStudentButPendingForVerification> model);
        Task<int> StudentEnrollment_ReturnByRegistrar(List<EligibleStudentButPendingForVerification> model);
        Task<DataTable> GetEnRollData_RegistrarVerify(GenerateEnrollSearchModel model);
    }
}
