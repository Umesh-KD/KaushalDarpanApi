
namespace Kaushal_Darpan.Models.UserActivityLogger
{
    public class UserActivityLoggerModel
    {
        public string UserName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string PageUrl { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
    }    
}
