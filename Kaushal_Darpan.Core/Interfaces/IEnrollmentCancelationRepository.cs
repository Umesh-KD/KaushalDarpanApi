using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Attendance;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMeritIInfoModel;

namespace Kaushal_Darpan.Core.Interfaces
{
 

    public interface IEnrollmentCancelationRepository
    {

        Task<int> ChangeEnRollNoStatus(StudentEnrolmentCancelModel model);
        Task<DataTable> GetEnrollCancelationData(StudentEnrolmentCancelModel model);
        Task<List<StudentDetailsModel>> GetAllData(StudentSearchModel filterModel);
        Task<List<StudentDetailsModel>> GetEnrollmentCancelList(StudentSearchModel filterModel);
        Task<int> CancelEnrolment(StudentEnrolmentCancelModel request);
        
    }
}
