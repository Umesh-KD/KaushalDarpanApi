using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIRelievingExam
{
    public class ITIExaminerRelievingModel
    {
        public string NCVTPracticalExam { get; set; } = string.Empty;
        public string DateOfExamination { get; set; } = string.Empty;
        public string Trade { get; set; } = string.Empty;
        public string PracticalExamCentre { get; set; } = string.Empty;
        public string PracticalExaminerName { get; set; } = string.Empty;
        public string PracticalExaminerDesignation { get; set; } = string.Empty;
        public string PracticalExaminerNumber { get; set; } = string.Empty;

        public int NoOfTotalTrainees { get; set; } = 0;
        public int NoOfPrsentTrainees { get; set; } = 0;
        public int NoOfAbsentTrainees { get; set; } = 0;

        public bool MarckSheetPacket { get; set; } = false;
        public bool CopyPacket { get; set; } = false;
        public bool PracticalPacket { get; set; } = false;
        public bool PracticalTeacherPacket { get; set; } = false;
        public bool SealedPacket { get; set; } = false;
        public bool BillPacket { get; set; } = false;
        public bool OtherInfo { get; set; } = false;

        public string OtherInfoText { get; set; } = string.Empty;
        public string ModifyBy { get; set; } = string.Empty;
        public string DepartmentID { get; set; } = string.Empty;
        public int ?CenterAssignedID { get; set; } = 0;
    }




    public class UndertakingExaminerFormModel
    {
        public string LetterNumber { get; set; }
        public string? AppointingDate { get; set; }

        public string? SsoId { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? Organization { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailId { get; set; }
        public string? Address { get; set; }

        public string?   ItiCollege { get; set; }
        public string? MisCode { get; set; }
        public string? DateOfExamination { get; set; }
        public string? SubjectAppointed { get; set; }
        public string? ItiCompleteAddress { get; set; }

        // Uncomment if needed
        // public bool? Declaration1 { get; set; }
        // public bool? Declaration2 { get; set; }

        public string? DeficienciesDetails { get; set; }
        public string? ModifyBy { get; set; }
        public int? DepartmentID { get; set; } 
        public int? CenterAssignedID { get; set; }
        public string? Remarks { get; set; } = string.Empty;
    }


}
