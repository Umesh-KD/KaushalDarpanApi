namespace Kaushal_Darpan.Models.RoleMaster
{
    public class HiringRoleMasterModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string? Name { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public string? Remark { get; set; }
    }
}
