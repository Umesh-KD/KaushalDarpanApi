using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITITheoryMarks
{
    public class ITITheoryMarksModel
    {
        public int SubjectID { get; set; }
        public int StudentExamPaperMarksID { get; set; }
        public string? IPAddress { get; set; }
        public string IsPresentTheory { get; set; }
        public string ObtainedTheory { get; set; }
        public string StudentName { get; set; }
        public string InstituteNameEnglish { get; set; }
        public string SemesterName { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }
        public string IsPresentInternalAssisment { get; set; }
        public string IsPresentPractical { get; set; }
        public string ObtainedInternalAssisment { get; set; }
        public string ObtainedPractical { get; set; }
        public string GroupCode { get; set; }
        public string CenterCode { get; set; }
        public string SubjectCode { get; set; }
        public string? RollNo { get; set; }
        public int MaxTheory { get; set; }
        public int MaxPractical { get; set; }
        public int MaxInternalAssisment { get; set; }
        public string Marked { get; set; }
        public int ModifyBy { get; set; }
        public int InternalPracticalID { get; set; }
        public bool IsChecked { get; set; }
        public bool IsFinalSubmit { get; set; }
        public bool IsPracticalChecked { get; set; }
        public int ExaminerID { get; set; }

    }
    public class ITITheorySearchModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int MarkEnter { get; set; }
        public int InternalPracticalID { get; set; }
        public string? RollNo { get; set; }
        public int GroupCodeID { get; set; }
        public int SSOID { get; set; }
        public int InstituteID { get; set; }
        public int? AppointExaminerID { get; set; }
    }


    public class CenterStudentSearchModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int MarkEnter { get; set; }

        public int UserAdditionalID { get; set; }

        public string? RollNo { get; set; }
        public int InternalPracticalID { get; set; }
        public int GroupCodeID { get; set; }
        public int InstituteID { get; set; }
        public bool IsConfirmed { get; set; }
        public string? UFMDocument { get; set; }
        public string? Dis_UFMDocument { get; set; }
        public string? ExaminerCode { get; set; }
        public string? SSOID { get; set; }
        public string? SubjectType { get; set; }
        public string? StudentName { get; set; }
        public int CenterID { get; set; }
    }


}

