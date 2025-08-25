using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIRelievingExam
{
    public class ITICoordinatorRelievingModel
    {
        public string NCVTPracticalExam { get; set; } = string.Empty;
        public string DateOfExamination { get; set; } = string.Empty;
        public string Trade { get; set; } = string.Empty;
        public string PracticalExamCentre { get; set; } = string.Empty;
        public string PracticalSuperintendentName { get; set; } = string.Empty;
        public string PracticalSuperintendentNumber { get; set; } = string.Empty;
        public string PracticalCoOrdinatorName { get; set; } = string.Empty;
        public string PracticalCoOrdinatorDesignation { get; set; } = string.Empty;
        public string PracticalCoOrdinatorNumber { get; set; } = string.Empty;
        public int NoOfRegisteredInstitutes { get; set; } = 0;
        public int DetailsOfPresentExaminers { get; set; } = 0;
        public bool IsMarkingSheetEnvelopeSubmitted { get; set; } = false;
        public bool IsCopyEnvelopeJob2Submitted { get; set; } = false;
        public bool IsPracticalCopyEnvelopeSubmitted { get; set; } = false;
        public bool IsHonorariumEnvelopeSubmitted { get; set; } = false;
        public bool IsSealedPracticalJobsSubmitted { get; set; } = false;
        public bool? IsCenterReportAttached { get; set; } = false;
        public bool IsHonorariumPaidOrVerified { get; set; } = false;
        public decimal HonorariumAmount { get; set; } = 0;
        public string? OtherInfoText { get; set; } = string.Empty;
        public string? AdditionalExaminerRemarksSubmitted { get; set; } = string.Empty;
        public string? ModifyBy { get; set; } = string.Empty;
        public string? DepartmentID { get; set; } = string.Empty;
        public int ExamCoordinatorID { get; set; } = 0;
        public string? Remarks { get; set; } =string.Empty;


    }

}
