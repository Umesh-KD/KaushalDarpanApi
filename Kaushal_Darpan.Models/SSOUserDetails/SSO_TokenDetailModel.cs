namespace Kaushal_Darpan.Models.SSOUserDetails
{
    public class SSO_TokenDetailModel
    {
        public string sAMAccountName { get; set; }//ssoid
        public List<string> Roles { get; set; }
        public string OldSSOIDs { get; set; }
        public string UserType { get; set; }
        public string DelegateBy { get; set; }
    }
}
