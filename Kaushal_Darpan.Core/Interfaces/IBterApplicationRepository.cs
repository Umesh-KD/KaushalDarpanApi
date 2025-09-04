using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.ApplicationMessageModel;
using Kaushal_Darpan.Models.DateConfiguration;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.ITIApplication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBterApplicationRepository
    {

        Task<int> SaveData(ApplicationDataModel productDetails);
        Task<int> SaveQualificationDataveData(QualificationDataModel productDetails);
        Task<int> SaveHighQualificationData(QualificationDataModel productDetails);
        Task<int> SaveAddressData(BterAddressDataModel productDetails);
        Task<int> SaveOtherData(BterOtherDetailsModel productDetails);
        Task<int> SaveDocumentData(List<DocumentDetailsModel> productDetails);
        Task<int> SaveFinalData( int ApplicationID, int Status);
        Task<int> SaveOptionalData(List<BterOptionsDetailsDataModel> productDetails);
        Task<ApplicationDataModel> GetApplicationDatabyID(BterSearchModel searchRequest);
        Task<QualificationDataModel> GetQualificationDatabyID(BterSearchModel searchRequest);
        Task<BterOtherDetailsModel> GetOtherDatabyID(BterSearchModel searchRequest);
        Task<BterDocumentsDataModel> GetDocumentDatabyID(BterSearchModel searchRequest);
        Task<List<BterOptionsDetailsDataModel>> GetOptionalDatabyID(BterSearchModel searchRequest);
        Task<BterAddressDataModel> GetAddressDatabyID(BterSearchModel searchRequest);
        Task<PreviewApplicationModel> GetPreviewDatabyID(BterSearchModel searchRequest);
        Task<DataTable> GetStudentProfileDownload(BterSearchModel searchRequest);
        Task<bool> DeleteOptionByID(BterOptionsDetailsDataModel productDetails);
        Task<bool> DeleteDocumentById(DocumentDetailsModel productDetails);
        Task<bool> PriorityChange(BterOptionsDetailsDataModel productDetails);


        Task<bool> DeleteByID(HighestQualificationModel productDetails);
        Task<DataTable> GetDetailsbyID(HighestQualificationModel productDetails);

        Task<DataTable> GetDetailsbyApplicationNo(List<ApplicationDetails> ApplicationDetails);
        Task<bool> DirectAdmissionPaymentUpdate(DirectAdmissionUpdatePayment model);
        Task<bool> JailAdmissionFinalSubmit(DirectAdmissionUpdatePayment model);

    }
}
