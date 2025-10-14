using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.DocumentDetails;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICounsellingApplicationFormRepository
    {
        Task<CounsellingApplicationFormDataModel> GetApplicationDataByID_Counselling(CounsellingApplicationSearchModel searchRequest);
        Task<int> SavePersonalDetails(CounsellingApplicationFormDataModel productDetails);
        Task<int> SavePersonalDetailsFromAdmin(CounsellingApplicationFormDataModel productDetails);
        Task<int> Counselling_SaveOption(CounsellingOptionFormDataModel request);
        //Task<DataTable> Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel model);
        Task<DataTable> Counselling_GetDropdownByAction(Counselling_DropdownDataModel model);
        Task<bool> DeleteOptionByID_Counselling(CounsellingOptionFormDataModel model);
        Task<bool> PriorityChange_Counselling(CounsellingOptionFormDataModel model);
        Task<Counselling_DocumentDataModel> GetDocumentDatabyID_Counselling(CounsellingApplicationSearchModel searchRequest);
        Task<int> SaveDocumentData_Counselling(List<Counselling_DocumentDetailsModel> request);
        Task<CounsellingApplicationPreviewDataModel> PreviewData_ByID_Counselling(CounsellingApplicationSearchModel searchRequest);
        Task<List<CounsellingOptionFormDataModel>> Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel searchRequest);
        Task<bool> DeleteChildOptionByID_Counselling(InstituteListDataModel_Coun model);
        Task<bool> ChildPriorityChange_Counselling(InstituteListDataModel_Coun model);
        Task<DataTable> GetInstituteOptionList_Counselling(InstituteListDataModel_Coun filterModel);
        Task<bool> ApplicationFinalSubmit_Counselling(CounsellingApplicationSearchModel model);
        Task<bool> DeleteDocumentById_Counselling(Counselling_DocumentDetailsModel model);
        Task<bool> UnlockApplication_Counselling(CounsellingApplicationSearchModel model);

        //Ravi Raj Singh
        Task<DataTable> MapCandidateSSO(CounsellingApplicationSearchModel filterModel);
        Task<int> UpdateStudentSsoMapping(CounsellingApplicationSearchModel request);
    }



}
