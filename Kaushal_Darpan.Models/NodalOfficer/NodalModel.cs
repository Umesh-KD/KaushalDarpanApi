using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.NodalOfficer
{
    public class NodalModel
    {
        public int NodalId { get; set; }
        public string SSOID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool IsEdit_Stu { get; set; }
        public bool IsAdd_CollageFees { get; set; }
        public int DepartmentID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }
        public bool? Marked { get; set; }
        public int UserID { get; set; }
        //public List<NodalModel> NodalModellist { get; set; }
    }

    public class SearchNodalModel
    {
        public string SSOID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
    }



}
