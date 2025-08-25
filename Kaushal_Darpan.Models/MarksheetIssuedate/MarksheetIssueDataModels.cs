namespace Kaushal_Darpan
{
    public class MarksheetIssueDataModels
    {
        public int MarksheetIssueDataId { get; set; }
        public int SemesterID { get; set; }
        public int ResultTypeId { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public string? IssuedDate { get; set; }
        public string? IPAddress { get; set; }
    }


    public class MarksheetIssueSearchModel
    {
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int SemesterID { get; set; }
    }

}
