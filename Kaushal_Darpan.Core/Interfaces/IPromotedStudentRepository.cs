using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.PrometedStudentMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPromotedStudentRepository
    {
        Task<List<PrometedStudentMasterModel>> GetPromotedStudent(PromotedStudentSearchModel model);
        Task<DataTable> GetITIPromotedStudent(PromotedStudentSearchModel model);
        Task<int> SaveEnrolledStudentExam_Back(List<StudentMarkedModel> model);
        Task<int> SaveEnrolledStudentExam_Next(List<StudentMarkedModel> model);
        Task<int> SavePromotedStudent(List<PromotedStudentMarkedModel> model);
        Task<int> SaveItiPromotedStudent(List<PromotedStudentMarkedModel> model);
    }
}
