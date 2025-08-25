using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_InstructorModel
{
    public class ITI_BGT_HeadMasterDataModel
    {

        // Personal Details
        public int? HeadId { get; set; }
        public string? HeadName { get; set; }
        public string? HeadCode { get; set; }
        public string? HeadDescription { get; set; }

        // Additional Fields
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string? CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
    }


    public class ITI_BGT_HeadMasterSearchModel
    {
        // Personal Details
        public string? Uid { get; set; }
        public string? Name { get; set; }
        public int? DepartmentID { get; set; }
        public int? InstituteID { get; set; }
        public int? RoleID { get; set; }
    }
}

