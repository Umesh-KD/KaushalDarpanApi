namespace Kaushal_Darpan.Models.GroupMaster
{
    public class GroupMasterModel
    {
        public int GroupID { get; set; }
        public int SubjectID { get; set; }
        public int ExamID { get; set; }
        public string GroupCode { get; set; }
        public string CenterCode { get; set; }
        public string SubjectCode { get; set; }
        public string ExamName { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }

    }
    public class GroupSearchModel
    {
        public string GroupCode { get; set; }
        public string SubjectCode { get; set; }
        public string CenterCode { get; set; }
        public int? ExamID { get; set; }
        public int DepartmentID { get; set; }
    }
}
