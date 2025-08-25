namespace Kaushal_Darpan.Models.LevelMaster
{
    public class LevelMasterModel
    {

        public int LevelID { get; set; }
        public string? LevelNameEnglish { get; set; }
        public string? LevelNameHindi { get; set; }
        public string? LevelNameShort { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime RTS { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
    }
}
