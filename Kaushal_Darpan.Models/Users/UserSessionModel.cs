namespace Kaushal_Darpan.Api.Models
{
    public class UserSessionModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleIDs { get; set; } = string.Empty;
        public string RoleNames { get; set; } = string.Empty;
        public string Token { get; set; }
        public int LevelId { get; set; }

        public int EndTermID { get; set; }
    }
}
