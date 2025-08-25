using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.SetExamAttendanceMaster
{
    public class SetExamAttendanceSearchModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public int InvigilatorAppointmentID { get; set; }
        public int UserID { get; set; }
        public int ShiftID { get; set; }
        public int InstituteID { get; set; }
        public int TimeTableID { get; set; }
        public int? InternalPracticalID { get; set; }

    }
}
