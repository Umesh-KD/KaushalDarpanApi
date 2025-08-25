namespace Kaushal_Darpan.Models.HrMaster
{
    public class HrMasterSearchModel
    {
        public string Name { get; set; }
        public int PlacementCompanyID { get; set; }
        public int DepartmentID { get; set; }
        public string? Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
    }

    public class HrMaster_Action
    {
        public int HRManagerID { get; set; }
        public int ActionBy { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public string Action { get; set; }
        public string? ActionRemarks { get; set; }
    }
}
