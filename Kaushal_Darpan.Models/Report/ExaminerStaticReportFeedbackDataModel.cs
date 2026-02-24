using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Report
{
    public class ExaminerStaticReportFeedbackDataModel
    {
        public int ExaminerStaticRptFeedbackID { get; set; }
        public int ExaminerID { get; set; }
        public string? ExaminerCode { get; set; }
        public int GroupCodeID { get; set; }
        public int SubjectID { get; set; }
        public string? CommonRemarkForQueAns { get; set; }
        public string? IsMassCoping { get; set; }
        public string? Syllabus { get; set; }
        public string? InstituteLevel { get; set; }
        public string? TeachingByTeacher { get; set; }
        public string? StudyOfStudent { get; set; }
        public string? SuggestionForImprovement { get; set; }
        public string? Date { get; set; }
        public string? SignPhoto { get; set; }
        public string? Dis_SignPhoto { get; set; }
        public string? ExamName { get; set; }
        public string? ExaminerName { get; set; }
        public string? GroupCode { get; set; }
        public string? SubjectCode { get; set; }
        public string? InstituteName { get; set; }
        public string? ExaminerSignNo { get; set; }
        public string? UserID { get; set; }
        public string? CourseType { get; set; }
        public string? DepartmentID { get; set; }
        public int? CenterID { get; set; }
        public string? MassCopyDocument { get; set; }
        public string? Dis_MassCopyDocument { get; set; }
        public int? Status { get; set; }
    }
}
