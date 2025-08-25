//using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.ITICampusPostMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ItiCampusPostMasterRepository
    {
        //Task<DataTable> GetAllData(string SSOID, int DepartmentID);
        Task<DataTable> GetAllData(int CompanyID, int CollegeID, string Status, int DepartmentID);
        Task<ItiCampusPostMasterModel> GetById(int PK_ID);
        Task<List<ItiCampusPostMasterModel>> GetNameWiseData(int ID, int DepartmentID);
        Task<bool> SaveData(ItiCampusPostMasterModel productDetails);
        Task<bool> Save_CampusValidation_NodalAction(ItiCampusPostMaster_Action model);
        Task<bool> UpdateData(ItiCampusPostMasterModel productDetails);
        Task<bool> DeleteDataByID(ItiCampusPostMasterModel productDetails);
        Task<DataTable> CampusValidationList(int CompanyID, int CollegeID, string Status, int DepartmentID);
    }
}
