namespace Kaushal_Darpan.Models.CompanyMaster
{
    public class IndustryInstitutePartnershipMasterResponsiveModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public string Logo { get; set; }
        public string Dis_Name { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }


        public int ModifyBy { get; set; }

        public string? IPAddress { get; set; }
    }
}
