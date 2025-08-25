namespace Kaushal_Darpan.Models.AssignRoleRight
{
    public class AssignRoleRightsModel
    {
        public int UserAdditionID { get; set; }//insert or update on behalf
        public int UserID { get; set; }
        //public int RoleID { get; set; }
        public int ID { get; set; }
        public bool IsMainRole { get; set; }
        public int InstituteID { get; set; }
        public string? SSOID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? IPAddress { get; set; }
        public string? Name { get; set; }
        public bool Marked { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
    }

}

