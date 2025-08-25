using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.SMSService
{
    public class SmsNotification
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string Message { get; set; }
    }
}
