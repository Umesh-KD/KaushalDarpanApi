using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITICenterSuperitendentExamReport
{
    public class ITICenterSuperitendentExamReportModel
    {
        public int? id { get; set; }
        public string? ConfidentialityLevel { get; set; }

        public bool ExamOnTime { get; set; } = false;

        public string ExamOnTimeRemark { get; set; } = string.Empty;

        public string ExamSchedule { get; set; } = string.Empty;

        public bool MarkingGuidance { get; set; } = false;

        public string MarkingGuidanceRemark { get; set; } = string.Empty;

        public string MarkingGuidanceDocument { get; set; } = string.Empty;

        public bool ChangeSizeOfUnits { get; set; } = false;

        public string ChangeSizeOfUnitsRemark { get; set; } = string.Empty;

        public string ChangeSizeOfUnitsDocument { get; set; } = string.Empty;

        public string LightFacilities { get; set; } = string.Empty;

        public string WaterFacilities { get; set; } = string.Empty;

        public string Discipline { get; set; } = string.Empty;

        public string ToiletFacilities { get; set; } = string.Empty;

        public bool IncidentOnExam { get; set; } = false;

        public string IncidentOnExamRemark { get; set; } = string.Empty;

        public string IncidentOnExamDocument { get; set; } = string.Empty;



        public bool examConductComment { get; set; } = false;

        public string examConductCommentRemark { get; set; } = string.Empty;

        //public string CommentsOnlightFacilities { get; set; } = string.Empty;

        //public string CommentsOnwaterFacilities { get; set; } = string.Empty;

        //public string CommentsOndiscipline { get; set; } = string.Empty;

        //public string CommentsOntoiletFacilities { get; set; } = string.Empty;

        public bool futureExamCenterComment { get; set; } = false;

        public string otherFutureExamSuggestions { get; set; } = string.Empty;

        public string ExamCenterCommentDocument { get; set; } = string.Empty;


        public string FlyingSquadDetails { get; set; } = string.Empty;
    }
}

