using Kaushal_Darpan.Models.SMSConfigurationSetting;
using Kaushal_Darpan.Models.Student;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ISMSMailRepository
    {
        Task<DataTable> GetAllUnsendSMS();
        Task<SMSConfigurationSettingModel> GetSMSConfigurationSetting();
        Task<DataTable> GetSMSTemplateByMessageType(string MessageType);
        Task<bool> UpdateUnsendSMSById(string AID, string response);


        Task<int> SendSMSForStudentEnrollmentData(List<ForSMSEnrollmentStudentMarkedModel> model);

    }
}
