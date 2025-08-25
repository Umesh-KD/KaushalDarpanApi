namespace Kaushal_Darpan.Models.CommonFunction
{
    public class CommonSerialMasterResponseModel : RequestBaseModel
    {
        public int? SerialID { get; set; }
        public int? SemesterId { get; set; }
        public string? Type { get; set; }
        public string? StaticVal { get; set; }
        public string? StartFrom { get; set; }
        public bool? ActiveStatus { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int? PartitionSize { get; set; }
    }
    public class CommonSerialMasterRequestModel : RequestBaseModel
    {
        public int? SerialID { get; set; }
        public int? SemesterId { get; set; }
        public int? TypeID { get; set; }
    }
}
