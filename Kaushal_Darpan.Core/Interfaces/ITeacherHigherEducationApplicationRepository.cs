using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.GuestRoomManagementModel;
using Kaushal_Darpan.Models.ITI_Inspection;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.Test;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ITeacherHigherEducationApplicationRepository
    {
        Task<DataTable> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model);
        Task<DataTable> GetTHTE_ApplicationData(THTE_ApplicationSearchModel model);
        Task<int> SaveTeacherHighEduApp(TeacherHigherEducationApplicationModel model);


        Task<DataTable> GetAllAppliedCoursesDDL(THTE_DDL body);
        Task<DataTable> GetAllInstitutionalsDDL(THTE_DDL body);

        Task<DataTable> GetCategoryOfApplyCourseInstitute(THTE_DDL body);

        Task<DataTable> THTE_GetStaffPersonalDetailByUserID(BTER_EM_GetPersonalDetailByUserID body);

        Task<TeacherHigherEducationApplicationModel> GetTHTE_ApplicationByID(THTE_ApplicationSearchModel model);


        Task<bool> DeleteTHTE_ApplicationByID(THTE_ApplicationSearchModel productDetails);

        Task<DataTable> THTE_GrtApplicationStatusHistory(THTE_ApplicationSearchModel productDetails);

        Task<int> CommitteeSaveData(CommitteeDataModel productDetails);

        Task<DataTable> GetCommitteeAllData(CommitteeSearchModel body);

        Task<CommitteeDataModel> GetCommitteeById_Team(int ID);

        Task<DataTable> GetCommitteeDDL(THTE_DDL body);

        Task<DataTable> Bter_CommitteeStaffCheckSSOID(CommitteeStaffSSOIDSearchModel body);
    }
}
