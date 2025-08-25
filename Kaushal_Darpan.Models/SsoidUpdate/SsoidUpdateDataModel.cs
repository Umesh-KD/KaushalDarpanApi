using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.SsoidUpdate
{
    public class SsoidUpdateDataModel : RequestBaseModel
    {
        public int UserID { get; set; }
        public int InstituteID { get; set; }
        public string DistrictNameEnglish { get; set; }
        public string InstituteNameEnglish { get; set; }
        public string? InstituteNameHindi { get; set; }
        public string MobileNumber { get; set; }
        public string SSOID { get; set; }
        public string Email { get; set; }
        public string EmailOfficial { get; set; }
        public string Name { get; set; }
        public bool Marked { get; set; }
        public string? IPAddress { get; set; }
        public string? OldSSOID { get; set; }
    }

    public class SsoidUpdateSearchModel: RequestBaseModel
    {
        public string? InstituteName { get; set; }
        public int? DistrictID { get; set; }
        public int? InstituteID { get; set; }
    }
}
