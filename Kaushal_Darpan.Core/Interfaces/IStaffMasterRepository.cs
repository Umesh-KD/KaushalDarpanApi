using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.RoleMaster;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.ViewPlacedStudents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStaffMasterRepository
    {
        Task<DataTable> GetAllData(StaffMasterSearchModel filterModel); 
        
        Task<bool> SaveData(StaffMasterModel productDetails);
        Task<bool> LockandSubmit(StaffMasterModel productDetails);
        Task<bool> UnlockStaff(StaffMasterModel productDetails);
        Task<bool> ApproveStaff(StaffMasterModel productDetails);
        Task<bool> ChangeWorkingInstitute(StaffMasterModel productDetails);
        Task<int> SaveBasicData( AddStaffBasicDetailDataModel productDetails);
        Task<StaffMasterModel> GetById(int PK_ID, int DepartmentID);

        Task<DataTable> StaffLevelType(StaffMasterSearchModel filterModel);
        Task<DataTable> StaffLevelChild(StaffMasterSearchModel filterModel);
        Task<DataTable> GetCurrentWorkingInstitute_ByID(StaffMasterSearchModel filterModel);

        Task<bool> IsDownloadCertificate(StaffMasterModel productDetails);

        Task<int> IsDeleteHostelWarden(string SSOID);
        Task<DataTable> SaveBranchHOD(BranchHODModel body);

        Task<bool> SaveBranchSectionData(SectionDataModel body);
        Task<DataTable> GetBranchSectionData(GetSectionDataModel body);
        Task<DataTable> SaveBranchSectionEnrollmentData(List<AllSectionBranchStudentDataModel> body);
        Task<DataTable> GetBranchStudentData(GetSectionDataModel body);
        Task<DataTable> GetBranchSectionEnrollmentData(GetSectionBranchStudentDataModel body);
        Task<DataTable> GetAllRosterDisplay(GetAllRosterDisplayModel body);
        Task<int> SaveRosterDisplay(SaveRosterDisplayModel body);
    }
}
