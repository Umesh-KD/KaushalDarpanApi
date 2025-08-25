using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.ITIAdminDashboard;
using Kaushal_Darpan.Models.ITIApplication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;
using static Kaushal_Darpan.Models.ITIApplication.ItiApplicationPreviewDataModel;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IItiApplicationFormRepository
    {
        Task<int> SavePersonalDetailsData(PersonalDetailsDataModel productDetails);
        Task<PersonalDetailsDataModel> GetApplicationDatabyID(ItiApplicationSearchModel searchRequest);
        Task<int> SaveOptionDetailsData(List<OptionDetailsDataModel> productDetails);
        Task<int> SaveQualificationDetailsData(List<QualificationDetailsDataModel> productDetails);
        Task<int> SaveDocumentDetailsData(List<DocumentDetailsModel> productDetails);
        Task<int> SaveAddressDetailsData(AddressDetailsDataModel productDetails);
        Task<List<QualificationDetailsDataModel>> GetQualificationDatabyID(ItiApplicationSearchModel request);
        Task<AddressDetailsDataModel> GetAddressDetailsbyID(ItiApplicationSearchModel request);
        Task<List<OptionDetailsDataModel>> GetOptionDetailsbyID(ItiApplicationSearchModel request);
        Task<ItiApplicationPreviewModel> GetApplicationPreviewbyID(ItiApplicationSearchModel searchRequest);
        Task<DocumentDetailsDataModel> GetDocumentDatabyID(ItiApplicationSearchModel searchRequest);
        Task<int> FinalSubmit(int ApplicationID, int Status);

        Task<DataTable> GetITIStudentProfileDownload(ItiApplicationSearchModel searchRequest);
        Task<bool> DeleteOptionByID(OptionDetailsDataModel productDetails);
        Task<bool> PriorityChange(OptionDetailsDataModel productDetails);
        Task<DataTable> GetItiApplicationData(ItiAdminDashApplicationSearchModel searchRequest);
        Task<bool> UnlockApplication(ItiApplicationUnlockDataModel productDetails);
        Task<int> ITI_DirectAdmissionApply(ITI_DirectAdmissionApplyDataModel request);
    }
}
