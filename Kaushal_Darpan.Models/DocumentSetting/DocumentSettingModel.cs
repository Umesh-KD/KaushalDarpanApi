using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DocumentSettingDataModel
{
    public class DocumentSettingDataModel
    {
        public int DocumentSettingMasterId { get; set; } 
        public string Name { get; set; }
        public int ContentType { get; set; } 
        public int DocType { get; set; } 
        public string Key { get; set; } 
        public string ValuePhoto { get; set; }
        public string? Value { get; set; }
        public string Dis_valuePhoto { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsNow { get; set; } 
    }

}
