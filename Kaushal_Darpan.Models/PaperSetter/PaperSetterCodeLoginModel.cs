using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.PaperSetter
{
    public class PaperSetterCodeLoginModel : RequestBaseModel
    {
        public int ExaminerID { get; set; }
        public string SSOID { get; set; }        
        public string ExaminerCode { get; set; }        
    }
}
