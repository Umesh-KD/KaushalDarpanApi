
namespace Kaushal_Darpan.Models.RoleMenuRights
{

    public class UserRightsModel
    {
        public int RoleMenuRightID { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int RoleID { get; set; }
        public int LevelNo { get; set; }
        public int ParentId { get; set; }
        public bool U_View { get; set; }
        public bool U_Add { get; set; }
        public bool U_Update { get; set; }
        public bool U_Delete { get; set; }
        public bool U_Print { get; set; }
        
        public int ModifyBy { get; set; }
        public string IPAddress { get; set; }


    }
}
