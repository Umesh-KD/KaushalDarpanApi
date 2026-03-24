using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITICampusDetailsWeb
{
    public class ITICampusDetailsWebSearchModel
    {
        public int DepartmentID { get; set; }
    }


    public class ITIDynamicUploadContentListsModal
    {
        public int DepartmentID { get; set; }
        public int DynamicUploadTypeID { get; set; }
        public int DepartmentSubID { get; set; }
        public string Key { get; set; }
    }

    public class ITIAllPostSearchModel : RequestBaseModel
    {
        public string BranchId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AppointmentLocation { get; set; }
        public string CampusLocation { get; set; }
        public int DistrictID { get; set; }
    }
}
