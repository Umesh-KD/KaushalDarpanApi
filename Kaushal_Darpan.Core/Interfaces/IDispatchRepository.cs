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
    public interface IDispatchRepository
    {
       
        Task<DataTable> GetAllData(DispatchSearchModel SearchReq);
        Task<DataTable> GetAllReceivedData(DispatchSearchModel SearchReq);
        Task<DataTable> GetAllGroupData(DispatchSearchModel SearchReq);
        Task<DataTable> getgroupteacherData(DispatchSearchModel SearchReq);
        Task<DataTable> getgroupExaminerData(DispatchSearchModel SearchReq);
        Task<int> SaveData(DispatchFormDataModel productDetails);
        Task<int> SaveDispatchGroup(DispatchGroupModel productDetails);
        Task<bool> DeleteDataByID(DispatchSearchModel productDetails);
        Task<DispatchFormDataModel> GetById(int PK_ID);

        Task<DataTable> GetBundelDataAllData(BundelDataModel SearchReq);
        Task<InstituteGroupDetail> GetGroupDataAllData(DispatchGroupSearchModel SearchReq);

        Task<int> SaveDispatchReceived(List<DispatchReceivedModel> dataModels);

        Task<DataTable> GetDownloadDispatchReceived(DownloadDispatchReceivedSearchModel SearchReq,string DownloadFile,string Dis_DownloadFile);
        Task<DispatchGroupModel> GetGroupdetailsId(int PK_ID);

        Task<bool> DeleteGroupById(int ID, int ModifyBy);
        Task<bool> UpdateGroupfile(string File,int ID);


        Task<int> SaveDispatchPrincipalGroupCodeData(DispatchPrincipalGroupCodeDataModel model);

        Task<DataTable> GetDispatchGroupcodeDetails(DispatchPrincipalGroupCodeSearchModel SearchReq);
        Task<DataTable> GetDispatchGroupcodeList(DispatchPrincipalGroupCodeSearchModel SearchReq);

        Task<DispatchPrincipalGroupCodeDataModel> GetDispatchGroupcodeId(int PK_ID);

        Task<int> OnSTatusUpdate(List<DispatchStatusUpdate> model);
        Task<int> OnSTatusUpdateByExaminer(List<DispatchStatusUpdate> model);
        Task<DataSet> DownloadAckReportPri(DispatchSearchModel SearchReq);

        Task<int> OnSTatusUpdateDispatchl (List<UpdateStatusDispatchPrincipalGroupCodeModel> model);

        Task<bool> DeleteDispatchPrincipalGroupCodeById(int ID, int ModifyBy);
        Task<bool> UpdateDispatchPrincipalGroupCodefile(string File, int ID);


        Task<int> OnSTatusUpdateByBTER(List<DispatchStatusUpdate> model);
        Task<bool> DeleteDispatchMasterById(int ID, int ModifyBy);


        Task<bool> UpdateDownloadFileDispatchMaster(string File, int ID);

        Task<DataTable> GetAllDataDispatchMaster(DispatchSearchModel SearchReq);

        Task<int> OnSTatusUpdateDispatchMaster(List<DispatchMasterStatusUpdate> model);


        Task<bool> UpdateDownloadFileDispatchReceived(string File, int ID);

        Task<DataTable>  CheckDateDispatchSearch(CheckDateDispatchSearchModel check);

        Task<int> UpdateBundleHandovertoExaminerByPrincipal(List<DispatchStatusUpdate> model);


        Task<int> BundelNoSendToThePrincipalByTheExaminer(List<DispatchStatusUpdate> model);

        Task<DataTable> DispatchSuperintendentAllottedExamDateList(DispatchSearchModel model);

        Task<int> UpdateRemarkImageHandedOverToExaminerByPrincipal(UpdateFileHandovertoExaminerByPrincipalModel model);


        Task<DataTable> GetAllDataCompanyDispatch(CompanyDispatchMasterSearchModel SearchReq);
        Task<int> SaveDataCompanyDispatch(CompanyDispatchMasterModel productDetails);

        Task<bool> DeleteDataCompanyDispatchByID(int ID, int ModifyBy);
        Task<CompanyDispatchMasterModel> GetByIdCompanyDispatch(int ID );

        Task<DataTable> GetDispatchGroupcodeDetailsCheck(DispatchPrincipalGroupCodeSearchModel SearchReq);

       



    }
}
