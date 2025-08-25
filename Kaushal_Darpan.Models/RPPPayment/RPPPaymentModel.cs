namespace Kaushal_Darpan.Api.Models
{
    public class RPPRequestDetailsModel
    {
        public decimal AMOUNT { get; set; }
        public string PURPOSE { get; set; }
        public string USERNAME { get; set; }
        public string USERMOBILE { get; set; }
        public string USEREMAIL { get; set; }
        public string CreatedBy { get; set; }
        public string SSOID { get; set; }
        public int ApplyNocApplicationID { get; set; }
        public int DepartmentID { get; set; }
        public string RemitterName { get; set; }
        public string REGTINNO { get; set; }
        public string DistrictCode { get; set; }
        public string Adrees { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string? PaymentType { get; set; }
    }
}
