using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.WebsiteSettings
{
    public class WebsiteSettingDataModel: RequestBaseModel
    {
        public int? WS_ID { get; set; }
        public int? TypeID { get; set; }
        public string? Title { get; set; }
        public string? Start_Date { get; set; }
        public string? End_Date { get; set; }
        public string? FileName { get; set; }
        public string? Dis_FileName { get; set; }
        public int? CourseSubTypeID { get; set; }
        public bool? ActiveStatus { get; set; }
        public int? UserID { get; set; }
        public int? DepartmentSubID { get; set; }
        public int? DUTC_ID { get; set; }
        public bool? IsActive { get; set; }
        public string? IPAddress { get; set; }
    }
}
