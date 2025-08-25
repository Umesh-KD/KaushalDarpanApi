namespace Kaushal_Darpan.Models.CenterCreationMaster
{
    public class CenterCreationAddEditModel : RequestBaseModel
    {
        public int CenterID { get; set; }
        public int InstituteID { get; set; }
        public string InstituteCode { get; set; }
        public string? AdmissionCategory { get; set; }
        public string? MobileNumber { get; set; }
        public int DistrictID { get; set; }
        public int DivisionID { get; set; }
        public int TehsilID { get; set; }
        public string Institutename { get; set; }
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public string SSOID { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
        public int? CCCode { get; set; }
        public string? Email { get; set; }
        public int EndTerm { get; set; }
        public int Capacity { get; set; }
    }
    public class ITICenterCreationAddEditModel : RequestBaseModel
    {
        public int CenterID { get; set; }
        public int Id { get; set; }
        public string InstituteCode { get; set; }
        public string? AdmissionCategory { get; set; }
        public string? MobileNumber { get; set; }
        public int DistrictID { get; set; }
        public int DivisionID { get; set; }
        public int TehsilID { get; set; }
        public string Institutename { get; set; }
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public string SSOID { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
        public int? CCCode { get; set; }
    }

    public class GenerateCCCodeDataModel: RequestBaseModel
    {
        public int CenterID { get; set; }
        public int CCCode { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
    }
}
