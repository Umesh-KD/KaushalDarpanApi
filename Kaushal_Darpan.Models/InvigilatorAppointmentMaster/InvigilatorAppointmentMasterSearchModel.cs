using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.InvigilatorAppointmentMaster
{
    public class InvigilatorAppointmentMasterSearchModel :RequestBaseModel
    {
        public string action { get; set; }
        public int InstituteID { get; set; }
        public int ModifyBy { get; set; }
        public string? ExamDate { get; set; }
        public int? ShiftID { get; set; }
    }

    public class UnlockExamAttendanceDataModel: RequestBaseModel
    {
        public string? action { get; set; }
        public int? InstituteID { get; set; }
        public int? UserID { get; set; }
        public int? CenterSuperintendentID { get; set; }
        public int? TimeTableID { get; set; }
    }
}
