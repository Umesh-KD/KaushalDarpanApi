using Kaushal_Darpan.Models.ITINodalOfficerExminerReport;
using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITINodalOfficerExminerReportRepository
    {
        Task<int> ITINodalOfficerExminerReportSave(ITINodalOfficerExminerReport body);
        Task<DataTable> ITINodalOfficerExminerReport_GetAllData(ITINodalOfficerExminerReportSearch body);
        Task<DataTable> GetAllData(Nodalsearchmodel body);
        Task<DataTable> ITINodalOfficerExminerReport_GetAllDataByID(ITINodalOfficerExminerReportSearch body);
        Task<DataSet> Generate_ITINodalOfficerExminerReport_ByID(int id,int InstituteID,string ExamDateTime);
        Task<ITINodalOfficerExminerReportByID> ITINodalOfficerExminerReport_GetDataByID(ITINodalOfficerExminerReportSearch body);
        Task<ITIInspectExaminationCenters> ITINodalOfficerExminerReportDetails_GetByID(int PK_Id);
        Task<int> ITINodalOfficerExminerReportDetailsUpdate(ITIInspectExaminationCenters body);
        Task<int> SaveAllData(NodalExamMapping body);
        Task<int> ITINodalOfficerExminerReportDetailsDelete(ITIInspectExaminationCenters body);
    }
}
