using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BTER_EstablishManagement
{
    public class QualificationMasterDataModel
    {
        public string? Action { get; set; }
        public int? QualificationID { get; set; }
        public string? QualificationLevel { get; set; }
        public string? QualificationName { get; set; }
        public string? Remarks { get; set; }
        public string? IPAddress { get; set; }
        public int? UserID { get; set; }
        public int? DepartmentID { get; set; }
    }

    public class QualificationMasterSearchModel
    {
        public int? QualificationID { get; set; }
        public string? QualificationName { get; set; }
        public string? Action { get; set; }
        public int? UserID { get; set; }
    }
}
