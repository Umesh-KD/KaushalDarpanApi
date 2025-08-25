namespace Kaushal_Darpan.Models.StreamMaster
{
    public class StreamMasterModel: RequestBaseModel
    {
        public int StreamID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int StreamTypeID { get; set; }
        public string Duration { get; set; }
        public string Qualifications { get; set; }

        public bool ActiveStatus { get; set; }

        public bool DeleteStatus { get; set; }

        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

    }





}
