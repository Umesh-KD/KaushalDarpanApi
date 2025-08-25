using Kaushal_Darpan.Models.BterStudentJoinStatus;
using Kaushal_Darpan.Models.StudentsJoiningStatusMarks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBterStudentJoinStatusRepository
    {
        Task<DataTable> GetAllData(BterStudentsJoinStatusMarksSearchModel filterModel);
        Task<DataTable> GetWithdrawAllotmentData(BterStudentsJoinStatusMarksSearchModel filterModel);
        Task<AllotmentReportingModel> GetAllotmentdata(BterStudentsJoinStatusMarksSearchModel filterModel);
        Task<int> SaveData(BterStudentsJoinStatusMarksMedel productDetails);
        Task<int> SaveWithdrawData(BterStudentsJoinStatusMarksMedel productDetails);
        Task<int> SaveReporting(BterAllotmentReportingModel productDetails);
        Task<DataTable> GetStudentAllotmentDetails(BterStudentsJoinStatusMarksSearchModel body);

        Task<int> NodalReporting(BterAllotmentReportingModel request);
        Task<int> SaveInstituteReporting(BterAllotmentReportingModel productDetails);
    }
}
