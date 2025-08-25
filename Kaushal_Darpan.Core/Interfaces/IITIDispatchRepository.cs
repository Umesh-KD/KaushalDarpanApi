using Kaushal_Darpan.Models.DispatchFormDataModel;
using Kaushal_Darpan.Models.DispatchMaster;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ScholarshipMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIDispatchRepository
    {
       
        Task<DataTable> GetAllData(ITIDispatchSearchModel SearchReq);
        Task<DataTable> GetAllReceivedData(ITIDispatchSearchModel SearchReq);
        Task<DataTable> GetAllGroupData(ITIDispatchSearchModel SearchReq);
        Task<DataTable> getgroupteacherData(ITIDispatchSearchModel SearchReq);
        Task<DataTable> getgroupExaminerData(ITIDispatchSearchModel SearchReq);
        Task<int> SaveData(ITIDispatchFormDataModel productDetails);
        Task<int> SaveDispatchGroup(ITIDispatchGroupModel productDetails);
        Task<bool> DeleteDataByID(ITIDispatchSearchModel productDetails);
        Task<ITIDispatchFormDataModel> GetById(int PK_ID);

        Task<DataTable> GetBundelDataAllData(ITIBundelDataModel SearchReq);
        Task<ITIInstituteGroupDetail> GetGroupDataAllData(ITIDispatchGroupSearchModel SearchReq);

        Task<int> SaveDispatchReceived(List<ITIDispatchReceivedModel> dataModels);

        Task<DataTable> GetDownloadDispatchReceived(ITIDownloadDispatchReceivedSearchModel SearchReq,string DownloadFile,string Dis_DownloadFile);
        Task<ITIDispatchGroupModel> GetGroupdetailsId(int PK_ID);

        Task<bool> DeleteGroupById(int ID, int ModifyBy);
        Task<bool> UpdateGroupfile(string File,int ID);


        Task<int> SaveDispatchPrincipalGroupCodeData(ITIDispatchPrincipalGroupCodeDataModel model);

        Task<DataTable> GetDispatchGroupcodeDetails(ITIDispatchPrincipalGroupCodeSearchModel SearchReq);
        Task<DataTable> GetDispatchGroupcodeList(ITIDispatchPrincipalGroupCodeSearchModel SearchReq);

        Task<ITIDispatchPrincipalGroupCodeDataModel> GetDispatchGroupcodeId(int PK_ID);

        Task<int> OnSTatusUpdate(List<ITIDispatchStatusUpdate> model);
        Task<int> OnSTatusUpdateByExaminer(List<ITIDispatchStatusUpdate> model);
        Task<DataSet> DownloadAckReportPri(ITIDispatchSearchModel SearchReq);

        Task<int> OnSTatusUpdateDispatchl (List<ITIUpdateStatusDispatchPrincipalGroupCodeModel> model);

        Task<bool> DeleteDispatchPrincipalGroupCodeById(int ID, int ModifyBy);
        Task<bool> UpdateDispatchPrincipalGroupCodefile(string File, int ID);


        Task<int> OnSTatusUpdateByITI(List<ITIDispatchStatusUpdate> model);
        Task<bool> DeleteDispatchMasterById(int ID, int ModifyBy);


        Task<bool> UpdateDownloadFileDispatchMaster(string File, int ID);

        Task<DataTable> GetAllDataDispatchMaster(ITIDispatchSearchModel SearchReq);

        Task<int> OnSTatusUpdateDispatchMaster(List<ITIDispatchMasterStatusUpdate> model);


        Task<bool> UpdateDownloadFileDispatchReceived(string File, int ID);

        Task<DataTable>  CheckDateDispatchSearch(ITICheckDateDispatchSearchModel check);

        Task<int> UpdateBundleHandovertoExaminerByPrincipal(List<ITIDispatchStatusUpdate> model);


        Task<int> BundelNoSendToThePrincipalByTheExaminer(List<ITIDispatchStatusUpdate> model);

        Task<DataTable> DispatchSuperintendentAllottedExamDateList(ITIDispatchSearchModel model);

        Task<int> UpdateRemarkImageHandedOverToExaminerByPrincipal(ITIUpdateFileHandovertoExaminerByPrincipalModel model);


        Task<DataTable> GetAllDataCompanyDispatch(ITICompanyDispatchMasterSearchModel SearchReq);
        Task<int> SaveDataCompanyDispatch(ITICompanyDispatchMasterModel productDetails);

        Task<bool> DeleteDataCompanyDispatchByID(int ID, int ModifyBy);
        Task<ITICompanyDispatchMasterModel> GetByIdCompanyDispatch(int ID );

        Task<DataTable> GetDispatchGroupcodeDetailsCheck(ITIDispatchPrincipalGroupCodeSearchModel SearchReq);
        Task<DataTable> GetITI_Dispatch_Showbundle(ITI_Dispatch_ShowbundleSearchModel SearchReq);
        Task<DataTable> GetITI_Dispatch_ShowbundleByAdminToExaminerData(ITI_Dispatch_ShowbundleSearchModel SearchReq);
        Task<DataTable> GetITI_Dispatch_ShowbundleByExaminerToAdminData(ITI_Dispatch_ShowbundleSearchModel SearchReq);




    }
}
