using Kaushal_Darpan.Models.FlyingSquad;
using Kaushal_Darpan.Models.SetPaper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ISetPaperRepository
    {
        Task<DataTable> GetSetPaper(GetSetPaperModal model);
        Task<int> PostSetPaper(PostSetPaperModal model);

        Task<int> PostAddQuestion(PostAddQuestionModal model);
        Task<DataTable> GetByIdQuestion(GetQuestionModal model);
        Task<DataTable> GetAllQuestion(GetQuestionModal model);
        Task<int> PostAddExamPaperAssignStaff(PostAddPaperAssignStaffModal model);
        Task<DataTable> GetByIdExamPaperAssignStaff(GetPaperAssignStaffModal model);
        Task<DataTable> GetAllExamPaperAssignStaff(GetPaperAssignStaffModal model);
        Task<int> PostPaperQuestionSetByStaff(List<PostAddPaperAssignStaffModal> model);
        Task<DataTable> GetAllPaperQuestionSetByStaff(GetPaperAssignStaffModal model);
    }
}
