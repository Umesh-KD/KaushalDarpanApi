namespace Kaushal_Darpan.Core.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

    }

    //public class RequestSearchModel
    //{
    //    public int RequestId { get; set; } = 0;
    //    public int RequestType { get; set; } = 0;
    //    public int UserId { get; set; } = 0;
    //    public string UserName { get; set; } = "";
    //    public string SSOID { get; set; } = "";
    //    public int StatusId { get; set; } = 0;
    //    public int PageNumber { get; set; } = 0;
    //    public int PageSize { get; set; } = 0;
    //    public string SearchText { get; set; } = "";
    //    public int PostID { get; set; } = 0;
    //    public int OfficeID { get; set; } = 0;
    //    public int LevelID { get; set; } = 0;
    //    public int DepartmentID { get; set; } = 0;
    //    public int DesignationID { get; set; } = 0;
    //    public int InstituteID { get; set; } = 0;
    //    public string RequestRemarks { get; set; } = "";
    //    public string OrderNo { get; set; } = "";
    //    public string OrderDate { get; set; } = "";
    //    public string JoiningDate { get; set; } = "";
    //    public string RequestDate { get; set; } = "";
    //    public int CreatedBy { get; set; } = 0;
    //    public string IPAddress { get; set; } = "";
    //    public string Action { get; set; } = "";
    //}



}
