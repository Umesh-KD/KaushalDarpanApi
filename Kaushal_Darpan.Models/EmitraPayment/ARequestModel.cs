using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.EmitraPayment
{
    public class ARequestModel
    {
        public string SSOID { get; set; } = string.Empty;
        public string SERVICEID { get; set; } = string.Empty;
        public string EMSESSIONID { get; set; } = string.Empty;
        public string KIOSKCODE { get; set; } = string.Empty;
        public string KIOSKNAME { get; set; } = string.Empty;
        public string ENTITYTYPE { get; set; } = string.Empty;
        public string DISTRICT { get; set; } = string.Empty;
        public string DISTRICTCD { get; set; } = string.Empty;
        public string TEHSIL { get; set; } = string.Empty;
        public string TEHSILCD { get; set; } = string.Empty;
        public string VILLAGE { get; set; } = string.Empty;
        public string VILLAGECD { get; set; } = string.Empty;
        public string WARD { get; set; } = string.Empty;
        public string WARDCD { get; set; } = string.Empty;
        public string PINCODE { get; set; } = string.Empty;

        public string MOBILE { get; set; } = string.Empty;
        public string EMAIL { get; set; } = string.Empty;
        public string LSPNAME { get; set; } = string.Empty;
        public string PARAMETER1 { get; set; } = string.Empty;
        public string PARAMETER2 { get; set; } = string.Empty;
        public string PARAMETER3 { get; set; } = string.Empty;
        public string PARAMETER4 { get; set; } = string.Empty;
        public string PARAMETER5 { get; set; } = string.Empty;
        public string RETURNURL { get; set; } = string.Empty;
        public string EMITRATIMESTAMP { get; set; } = string.Empty;
        public string SSOTOKEN { get; set; } = string.Empty;
        public string CHECKSUM { get; set; } = string.Empty;
        public int DepartmentID { get; set; } = 0;
    }
}
