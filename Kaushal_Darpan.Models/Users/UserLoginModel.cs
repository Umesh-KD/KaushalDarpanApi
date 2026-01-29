namespace Kaushal_Darpan.Api.Models
{
    public class UserLoginModel
    {
        public string? Email { get; set; }

        public string Password { get; set; }
        public string UserName { get; set; }
        public int DepartmentID { get; set; } = 0;
    }
}
