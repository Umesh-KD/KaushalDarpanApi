using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.BridgeCourse;
using Kaushal_Darpan.Models.DTE_AssignApplication;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.studentve;
using Kaushal_Darpan.Models.TheoryMarks;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IAssignApplicationMasterRepository
    {
        Task<int> SaveData(AssignApplicaitonDataModel productDetails);
        Task<List<AssignApplicaitonDataModel>> GetAllData(AssignApplicationSearchModel request);
        Task<DataTable> GetTotalAssignCount(RequestBaseModel request);
        Task<List<AssignedApplicationStudentDataModel>> GetStudentsData(StudentsAssignApplicationSearch request);
        Task<AssignApplicaitonDataModel> GetApplicationsById(int ID);
        Task<AssignApplicaitonDataModel> GetStudentDataById(int ApplicationID);
        Task<bool> DeleteDataByID(AssignApplicaitonDataModel productDetails);
        Task<List<AssignedApplicationStudentDataModel>> GetVerifierData(StudentsAssignApplicationSearch request);
        Task<int> AssignChecker(List<AssignCheckerModel> model);
        Task<bool> RevertApplication(RevertApplicationDataModel request);
    }
}
