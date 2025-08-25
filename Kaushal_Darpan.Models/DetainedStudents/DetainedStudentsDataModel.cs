using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DetainedStudents
{
    public class DispatchMasterModel: RequestBaseModel
    {
        public int StudentID { get; set; }
        public int UserID { get; set; }
    }
}
