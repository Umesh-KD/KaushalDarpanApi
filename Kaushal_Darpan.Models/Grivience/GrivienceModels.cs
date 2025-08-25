using System.Reflection;

namespace Kaushal_Darpan.Models.CompanyMaster
{
    public class GrivienceModelsDataModel
    {

        public int GrivienceID { get; set; }
        public string ComplainNo { get; set; }
        public int CategoryID { get; set; }
        public int DepartmentID { get; set; }
        public int ModuleID { get; set; }
        public string ApplicationNo { get; set; }
        public string SubjectRelatedToComplain { get; set; }
        public string FileAttachment { get; set; }
        public string DisAttachmentFileName { get; set; }
        public string Remark { get; set; }
        public int StatusID { get; set; }
        public string? ResolvedDate { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }

        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public int RoleID { get; set; }

    }
    
    public class GrivienceReopenModelsDataModel
    {

        public int GrivienceID { get; set; }
        public string FileAttachment { get; set; }
        public string DisAttachmentFileName { get; set; }
        public string Remark { get; set; }
        

    }


    public class GrivienceSearchModel
    {
        public int GrivienceID { get; set; }
        public int CategoryID { get; set; }
        public int DepartmentID { get; set; }
        public int ModuleID { get; set; }
        public int RoleID { get; set; }

        public int StatusID { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }

    }
    public class GrivienceResponseDataModel
    {
        public int GrivienceResponseID { get; set; }
        public int GrivienceID { get; set; }
        public string Remark { get; set; }
        public string ResponseFileAttachment { get; set; }
        public string DisResponseFileName { get; set; }
        public int StatusID { get; set; }
        public string RTS { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public string ModifyDate { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string ComplainNo { get; set; }
         
    }

}
