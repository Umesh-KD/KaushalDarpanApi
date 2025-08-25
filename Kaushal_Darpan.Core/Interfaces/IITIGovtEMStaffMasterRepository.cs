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
    public interface IITIGovtEMStaffMasterRepository
    {
        Task<DataTable> GetAllData(ITIGovtEMStaffMasterSearchModel filterModel); 
        
        Task<bool> SaveData(ITIGovtEMStaffMasterModel productDetails);
        Task<bool> LockandSubmit(ITIGovtEMStaffMasterModel productDetails);
        Task<bool> UnlockStaff(ITIGovtEMStaffMasterModel productDetails);
        Task<bool> ApproveStaff(ITIGovtEMStaffMasterModel productDetails);
        Task<bool> ChangeWorkingInstitute(ITIGovtEMStaffMasterModel productDetails);
        Task<int> SaveBasicData(ITIGovtEMAddStaffBasicDetailDataModel productDetails);
        Task<ITIGovtEMStaffMasterModel> GetById(int PK_ID, int DepartmentID);

        Task<DataTable> StaffLevelType(ITIGovtEMStaffMasterSearchModel filterModel);
        Task<DataTable> StaffLevelChild(ITIGovtEMStaffMasterSearchModel filterModel);
        Task<DataTable> GetCurrentWorkingInstitute_ByID(ITIGovtEMStaffMasterSearchModel filterModel);

        Task<bool> IsDownloadCertificate(ITIGovtEMStaffMasterModel productDetails);

        Task<int> IsDeleteHostelWarden(string SSOID);

        Task<int> UpdateSSOIDByPriciple(UpdateSSOIDByPricipleModel update);
        Task<DataTable> GetPrincipleList(ITIGovtStudentSearchModel body);

        Task<DataTable> ITIGovtEM_OfficeGetAllData(ITIGovtEM_OfficeSearchModel filterModel);
        Task<int> ITIGovtEM_OfficeSaveData(ITIGovtEM_OfficeSaveDataModel productDetails);
        Task<ITIGovtEM_OfficeSearchModel> ITIGovtEM_OfficeGetByID(int PK_Id);
       
        Task<bool> ITIGovtEM_OfficeDeleteById(int PK_Id, int userId);

        Task<DataTable> GetAllITI_Govt_EM_OFFICERS(ITI_Govt_EM_OFFICERSSearchDataModel iTI_Govt_EM);
        Task<int> ZonalOfficeCreateSSOID(UpdateSSOIDByPricipleModel update);


        Task<DataTable> ITIGovtEM_PostGetAllData(ITIGovtEM_PostSearchModel filterModel);
        Task<int> ITIGovtEM_PostSaveData(ITIGovtEM_PostSaveDataModel productDetails);
        Task<ITIGovtEM_PostSearchModel> ITIGovtEM_PostGetByID(int PK_Id);
        Task<bool> ITIGovtEM_PostDeleteById(int PK_Id, int userId);

        Task<DataTable> ITIGovtEM_Govt_AdminT2Zonal_Save(List<ITI_Govt_EM_ZonalOFFICERSDataModel> model);
        Task<ITI_Govt_EM_ZonalOFFICERSDataModel> ITIGovtEM_SSOIDCheck(string SSOID);
        Task<DataTable> ITIGovtEM_Govt_AdminT2Zonal_GetAllData(ITI_Govt_EM_ZonalOFFICERSSearchDataModel body);

        Task<int> ITIGovtEM_Govt_StaffProfileQualification(List<ITIGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel> model);


        Task<int> ITIGovtEM_Govt_StaffProfileStaffPosting(List<StaffPostingData> model);


        Task<int> ITIGovtEM_Govt_StaffProfileUpdate(ITIGovtEMStaff_PersonalDetailsModel model);

        Task<bool> ITI_GOVT_EM_ApproveRejectStaff(RequestUpdateStatus productDetails);
        Task<DataTable> ITIGovtEM_Govt_PersonnelDetailsInstitutionsAccordingBudget_Save(List<ITI_Govt_EM_SanctionedPostBasedInstituteModel> model);

        Task<DataTable> ITIGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData(ITI_Govt_EM_ZonalOFFICERSSearchDataModel body);

        Task<DataTable> ITIGovtEM_Govt_RoleOfficeMapping_GetAllData(ITI_Govt_EM_RoleOfficeMappingSearchDataModel body);

        Task<PersonalDetailByUserIDModel> ITIGovtEM_ITI_Govt_Em_PersonalDetailByUserID(PersonalDetailByUserIDSearchModel body);

        //Task<bool> ITIGovtEM_DeleteByIdStaffPromotionHistory(int ID, int ModifyBy);
        Task<int> ITIGovtEM_DeleteByIdStaffPromotionHistory(ITIGovtEM_DeleteByIdStaffPromotionHistoryDelete body);
        Task<DataTable> ITIGovtEM_ITI_Govt_EM_GetUserLevel(int ID);

        //Task<bool> ITI_Govt_EM_EducationalQualificationDeleteByID(int ID, int ModifyBy);
        Task<int> ITI_Govt_EM_EducationalQualificationDeleteByID(ITIGovtEM_DeleteByIdStaffEducationDelete body);
        //Task<bool> ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(int ID, int ModifyBy);
        Task<int> ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(ITIGovtEM_DeleteByIdStaffServiceDelete body);
        Task<DataTable> GetITI_Govt_EM_GetUserProfileStatus(int ID);

        Task<bool> FinalSubmitUpdateStaffProfileStatus(RequestUpdateStatus productDetails);

        Task<ITI_Govt_EM_ZonalOFFICERSDataModel> ITIGovtEM_GetSSOID(int StaffId);

        Task<DataSet> GetJoiningLetter(JoiningLetterSearchModel model); 
        Task<DataSet> GetRelievingLetter(RelievingLetterSearchModel model);

        Task<DataTable> GetITI_Govt_CheckDistrictNodalOffice(CheckDistrictNodalOfficeSearchModel model);

        //Task<bool> ITIGovtEM_OfficeDelete(int ID, int ModifyBy);
        Task<int> ITIGovtEM_OfficeDelete(ITIGovtEM_OfficeDeleteModel body);
        Task<DataTable> GetITI_Govt_EM_UserProfileStatusHt(ITI_Govt_EM_UserRequestHistoryListSearchDataModel Model);


    }
}
