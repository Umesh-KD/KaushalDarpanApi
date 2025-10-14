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
    }
}
