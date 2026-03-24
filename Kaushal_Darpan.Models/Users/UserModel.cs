
namespace Kaushal_Darpan.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
   
    public class UserLoginExtraInfoRequestModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int DepartmentID { get; set; }

        
    }
    
    public class UserLoginExtraInfoResponseModel
    {
        public string SSOID { get; set; }
        public string UserIDs { get; set; }
        public string DepartmentIDs { get; set; } 
    }

}
