namespace Kaushal_Darpan.Models.CenterMaster
{
    public class CenterMasterModel
    {
        public int CenterID { get; set; }
        public string CenterCode { get; set; }
        public string CenterName { get; set; }
        public string SSOID { get; set; }
        public string? MobileNumber { get; set; }
        public int DistrictID { get; set; }
        public int DivisionID { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public int TehsilID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int Capacity { get; set; }   
    }
}
