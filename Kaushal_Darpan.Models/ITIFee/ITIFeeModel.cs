using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIFeeModel
{
    public class ITIFeeModel
    {
 
        public int Id { get; set; }
        public string AdmissionFee { get; set; }
        public string ApplicationProcessingFee { get; set; }
        public string ApplicationFormFeeGen { get; set; }
        public string ApplicationFormFeeSc { get; set; }
        public string ApplicationFormFeeSt { get; set; }
        public string ApplicationFormFeeObc { get; set; }
        public string ApplicationFormFeeMbc { get; set; }
        public int AcademicYear { get; set; }
  
    }
}
