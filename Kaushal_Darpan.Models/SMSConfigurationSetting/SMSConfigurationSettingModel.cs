namespace Kaushal_Darpan.Models.SMSConfigurationSetting
{
    public class SMSTemplateDataModel
    {
        public int SMSTemplateID { get; set; }
        public string MessageType { get; set; }
        public string TemplateID { get; set; }
        public string MessageBody { get; set; }

    }
    public class SMSConfigurationSettingModel
    {
        public string ServiceName { get; set; }
        public string UniqueID { get; set; }
        public string SMSUrl { get; set; }
        public string SMSUserName { get; set; }
        public string SMSPassWord { get; set; }
        public string SmsClientID { get; set; }
    }


    public class UNOCSMSDataModel
    {
        public string UniqueID { get; set; }
        public string ServiceName { get; set; }
        public string Language { get; set; }
        public string Message { get; set; }
        public string MobileNo { get; set; }
        public string EntityID { get; set; }
        public string TemplateID { get; set; }
    }

    public class UNOCSmsModel
    {
        public string UniqueID { get; set; }
        public string ServiceName { get; set; }
        public string Language { get; set; }
        public string Message { get; set; }
        public List<string> MobileNo { get; set; }
        public string templateID { get; set; }
    }
    public class SmsResponseModel
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
        public string responseID { get; set; }
        public string TrustType { get; set; }

    }
}
