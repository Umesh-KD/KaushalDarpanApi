using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.GenerateGroupCode
{
    public class GenerateGroupCodeModel
    {
        public int TotalRecord { get; set; }
        

    }

    public class GenerateGroupCodeSearchModel
    {
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public int StudentID { get; set; }
        public int RoleId { get; set; }
        public int UserID { get; set; }
    }

}
