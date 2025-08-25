using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.TimeTable
{
    public class TimeTableSearchModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int InstituteID { get; set; }
        public int ShiftID { get; set; }
        public int Status { get; set; }
        public string? Action { get; set; }

    }



}
