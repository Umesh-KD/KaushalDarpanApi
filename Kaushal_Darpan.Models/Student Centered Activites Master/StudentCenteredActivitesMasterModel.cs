namespace Kaushal_Darpan.Models.TheoryMarks

{
    public class StudentCenteredActivitesMasterModel
    {
        public int SubjectID { get; set; }
        public int StudentExamPaperMarksID { get; set; }
        public string? IPAddress { get; set; }
        public string IsPresentStudentCenteredActivity { get; set; }
        public string ObtainedStudentCenteredActivity { get; set; }
        public string GroupCode { get; set; }
        public string CenterCode { get; set; }
        public string SubjectCode { get; set; }
        public string? RollNo { get; set; }
        public int MaxStudentCenteredActivity { get; set; }
        public string Marked { get; set; }
        public int ModifyBy { get; set; }
        public bool IsSCAChecked { get; set; }
        public string UFMDocument { get; set; }
        public string Dis_UFMDocument { get; set; }
        public bool isFinalSubmit { get; set; }

    }
    public class StudentCenteredActivitesMasterSearchModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int UserID { get; set; }
        public int MarkEnter { get; set; }
        public string? RollNo { get; set; }
        public int InstituteID { get; set; }
        public int? RoleID { get; set; }
        public string? InstituteName { get; set; }
    }
}
