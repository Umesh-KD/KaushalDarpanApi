using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CampusDetailsWeb
{
    public class CampusDetailsWebSearchModel
    {
        public int DepartmentID { get; set; }
    }


    public class DynamicUploadContentListsModal
    {
        public int DepartmentID { get; set; }
        public int DynamicUploadTypeID { get; set; }
        public int DepartmentSubID { get; set; }
        public string Key { get; set; }
    }
}
