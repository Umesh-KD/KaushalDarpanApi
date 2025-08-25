using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.StudentDetailUpdate;
using Kaushal_Darpan.Models.StudentMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStudentDetailUpdateRepository
    {
        Task<DataTable> GetStudentDetailUpdate(StudentDetailUpdateModel model);
        Task<StudentDetailUpdateModel> GetById(int PK_ID, int DepartmentID, int Eng_NonEng);
        Task<bool> UpdateStudentData(StudentDetailUpdateModel productDetails);
    }
}
