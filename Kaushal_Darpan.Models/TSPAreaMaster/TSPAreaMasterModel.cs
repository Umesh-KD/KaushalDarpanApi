namespace Kaushal_Darpan.Models.TSPAreaMaster
{
    public class TSPAreaMasterModel
    {
        public int ITITspAreasId { get; set; }
        public int DistrictId { get; set; }
        public int TehsilId { get; set; }
        public string VillageName { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }

    }
    public class TSPAreaMasterSearchModel
    {
        public int ITITspAreasId { get; set; }
        public int DistrictId { get; set; }
        public int TehsilId { get; set; }
        public string VillageName { get; set; }
        public bool ActiveStatus { get; set; }

    }

    public class TSPTehsilModel
    {
        public int DistrictID { get; set; }
    }
}
