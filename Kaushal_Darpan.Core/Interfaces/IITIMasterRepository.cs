using Kaushal_Darpan.Models.ITIMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIMasterRepository
    {
        Task<DataTable> GetAllData(ITISearchModel filterModel);

        Task<int> SaveData(ITIMasterModel productDetails);

        Task<ITIMasterModel> GetById(int TradeId);

        Task<bool> DeleteDataByID(ITIMasterModel productDetails);

        Task<DataTable> GetAllITIStudents(SearchITIModelRequest filterModel);

        Task<DataTable> GetAllPaperUploadData(ITIPaperUploadSearchModel body);

        Task<int> SavePaperUploadData(ITIPaperUploadModel request);
        Task<DataTable> GetITIFeesPerYearList(ITIFeesPerYearSearchModel request);
        Task<DataSet> ItiFeesPerYearListDownload(ITIFeesPerYearSearchModel request);

        Task<bool> unlockfee(int id, int ModifyBy, int  FeePdf);

        Task<DataTable> GetITI_CollegeLoginInfoMaster(CollegeLoginInfoSearchModel request);
        Task<DataTable> GetCollegeLoginInfoByCode(CollegeLoginInfoSearchModel request);

        Task<int> Update_CollegeLoginInfo(CollegeLoginInfoSearchModel request);

        Task<DataTable> GetCenterDetailByPaperUploadID(int PaperUploadID , int Userid , int Roleid );

        Task<DataTable> GetCenterWisePaperDetail(CenterWisePaperDetailModal request);

        Task<DataTable> PaperDownloadValidationCheck(DownloadPaperValidationModal request);

        Task<DataTable> UpdatePaperDownloadFalg(UpdateDownloadPaperFalgModal request);

        //Task<DataTable> GetCenterIDByLoginUser(CenterWisePaperDetailModal request);

    }
}
