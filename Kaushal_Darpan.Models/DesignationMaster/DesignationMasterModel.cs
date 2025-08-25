namespace Kaushal_Darpan.Models.DesignationMaster
{
    public class DesignationMasterModel
    {
        public int DesignationID { get; set; }
        public int UserID { get; set; }
        public string? DesignationNameEnglish { get; set; }
        public string? DesignationNameHindi { get; set; }
        public string? DesignationNameShort { get; set; }
        public string? Remark { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime RTS { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
    }
}

