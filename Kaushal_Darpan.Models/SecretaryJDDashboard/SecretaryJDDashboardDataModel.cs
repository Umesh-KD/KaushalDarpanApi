using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.SecretaryJDDashboard
{
    public class SecretaryJDDashboardDataModel: RequestBaseModel
    {
        public string? SSOID { get; set; }
        public int? RoleId { get; set; }
    }
}
