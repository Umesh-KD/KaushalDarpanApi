using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BhandarFormDataModel
{
    using System.Collections.Generic;

    public class AddBhandarFormDataModel
    {
        public string? MoharID { get; set; }

        public int BhandarID { get; set; }

        public int EndtermID { get; set; }
        public int UserID { get; set; }

        public int ShiftID { get; set; }

        public int CenterID { get; set; }

        public int SemesterID { get; set; }

        public string? UserName { get; set; }

        public string? ExamDate { get; set; }

        public string? Name { get; set; }

        public string? ExamNo { get; set; }

        public string? StudentNo { get; set; }

        public string? FromDutyTime { get; set; }

        public string? ToDutyTime { get; set; }

        public string? Size { get; set; }

        public string? Remark { get; set; }

        public string? DisFileName { get; set; }

        public string? FileName { get; set; }

        public bool IsOpen { get; set; }

        public List<BhandarDetailsModel>? BhandarDetailsModel { get; set; }

        public List<BhandarStudentModel>? BhandarStudentModel { get; set; }
    }

    public class BhandarDetailsModel
    {
        public int BhandarID { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string ExamNo { get; set; } = string.Empty;

        public string StudentNo { get; set; } = string.Empty;

        public string FromDutyTime { get; set; } = string.Empty;

        public string ToDutyTime { get; set; } = string.Empty;

        public string Size { get; set; } = string.Empty;
    }

    public class BhandarStudentModel
    {
        public int BhandarID { get; set; } = 0;
        public string? RollNo { get; set; }
        public string? Time { get; set; }
        public int? Type { get; set; }
    }
}
