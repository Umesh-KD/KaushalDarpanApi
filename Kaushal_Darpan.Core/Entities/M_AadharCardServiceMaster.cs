namespace Kaushal_Darpan.Core.Entities
{
    public class M_AadharCardServiceMaster
    {
        public int AID { get; set; }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public string ServiceURL { get; set; }
        public string Subaua { get; set; }
        public string AadhaarLicKey { get; set; }
        public string ValidateAadharServiceURL { get; set; }
        public string GetAadhaarNoByVIDURL { get; set; }
        public string eSignOTP { get; set; }
        public string ValidateAadhaarOTP_Esign { get; set; }
        public string eSignMultipleSignAgri { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
    }
}
