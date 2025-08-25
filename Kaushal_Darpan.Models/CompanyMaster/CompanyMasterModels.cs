namespace Kaushal_Darpan.Models.CompanyMaster
{
    public class CompanyMasterModels
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Website { get; set; }
        public string Address { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int CompanyTypeId { get; set; }
        public int DepartmentID { get; set; }
        public string CompanyPhoto { get; set; }
        public string Dis_CompanyName { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }

        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? HRName { get; set; }

    }


    public class CompanyMaster_Action
    {
        public int ID { get; set; }
        public int ActionBy { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public string Action { get; set; }
        public string? ActionRemarks { get; set; }
    }
        
}
