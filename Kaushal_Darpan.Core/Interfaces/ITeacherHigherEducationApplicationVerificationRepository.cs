using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.Test;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ITeacherHigherEducationApplicationVerificationRepository
    {
        Task<DataTable> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model);
        Task<int> SaveEnrolledStudentVerify_ReturnbyExamIncharge(List<EnrolledPromotedStudentSaveModel> model);
        Task<DataTable> ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model);
        Task<DataTable> ApplicationList_ForDTE_THTE(PrincipleApplicationListSearchModel model);
        Task<int> UpdateApplicationStatus_Principle_THTE(List<UpdateApplicationStatusDataModel_Principle> model);
        Task<int> UpdateApplicationStatus_DTE_THTE(List<UpdateApplicationStatusDataModel_Principle> model);
        Task<DataTable> ApplicationList_ForCommittee_THTE(PrincipleApplicationListSearchModel model);
        Task<int> UpdateApplicationStatus_Committee_THTE(UpdateApplicationStatusDataModel_Committee model);
        Task<DataTable> GetApplication_GenrateOrder_Dte_THTE(ApplicationGenrateOrderByDteListSearchModel model);


        Task<DataTable> ApplicationList_ForCommitteeAfterPrinciple_THTE(PrincipleApplicationListSearchModel model);

        Task<int> UpdateApplicationStatus_CommitteeAfterPrinciple_THTE(List<UpdateApplicationStatusDataModel_Principle> model);
        Task<int> DTECommitteeAssign_THTE(List<UpdateApplicationStatusDataModel_Principle> model);
    }
}
