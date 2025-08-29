
using Kaushal_Darpan.Models.ITI_InstructorModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITI_InstructorRepository
    {
        Task<int> SaveInstructorData(ITI_InstructorModel model);

        Task<DataTable> GetInstructorDataByID(int id);
        Task<int> deleteInstructorDataByID(int id);

        //Task<DataTable> GetCenterSuperitendentReportData(ITICollegeStudentMarksheetSearchModel model);

        Task<DataTable> GetInstructorData(ITI_InstructorDataSearchModel model);

        Task<DataTable> GetGridInstructorData(ITI_InstructorApplicationNoDataSearchModel model);

        Task<DataTable> GetGridBindInstructorData(ITI_InstructorBindDataSearchModel model);
        Task<DataSet> GetInstructorDataBySsoid(string SSOID);
    }
}
