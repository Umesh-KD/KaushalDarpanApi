using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.RoleMaster;
using Kaushal_Darpan.Models.ITIPrivateEstablish;
using Kaushal_Darpan.Models.ViewPlacedStudents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIPrivateEstablishRepository
    {
        Task<DataTable> GetAllData(ITIPrivateEstablishSearchModel filterModel); 
        
        Task<bool> SaveData(ITIPrivateEstablishModel productDetails);
        Task<bool> LockandSubmit(ITIPrivateEstablishModel productDetails);
        Task<bool> UnlockStaff(ITIPrivateEstablishModel productDetails);
        Task<bool> ApproveStaff(ITIPrivateEstablishModel productDetails);
        Task<bool> ChangeWorkingInstitute(ITIPrivateEstablishModel productDetails);
        Task<int> SaveBasicData(ITIPrivateEstablish_AddStaffBasicDetailDataModel productDetails);
        Task<ITIPrivateEstablishModel> GetById(int PK_ID, int DepartmentID);

        Task<DataTable> StaffLevelType(ITIPrivateEstablishSearchModel filterModel);
        Task<DataTable> StaffLevelChild(ITIPrivateEstablishSearchModel filterModel);
        Task<DataTable> GetCurrentWorkingInstitute_ByID(ITIPrivateEstablishSearchModel filterModel);

        Task<bool> IsDownloadCertificate(ITIPrivateEstablishModel productDetails);

        Task<int> IsDeleteHostelWarden(string SSOID);

    }
}
