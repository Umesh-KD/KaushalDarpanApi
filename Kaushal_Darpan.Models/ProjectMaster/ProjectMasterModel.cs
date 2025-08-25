using System.Net;
using System.Numerics;

namespace Kaushal_Darpan.Models.ProjectMaster
{
    public class ProjectMasterModel: RequestBaseModel
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Vendor { get; set; }
        public string WorkorderNo { get; set; }
        public DateTime? WorkorderDate { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }

    }

    public class ProjectMasterSearchModel : RequestBaseModel
    {
        public string ProjectName { get; set; }
        public string Vendor { get; set; }
        public string WorkorderNo { get; set; }
    }


}
