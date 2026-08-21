using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BTER_EstablishManagement
{
    public class PayLevelMasterDataModel
    {
        public int PayLevelID { get; set; }
        public int UserID { get; set; }
        public string PayLevel { get; set; }
        public string Action { get; set; } = string.Empty;
    }
}
