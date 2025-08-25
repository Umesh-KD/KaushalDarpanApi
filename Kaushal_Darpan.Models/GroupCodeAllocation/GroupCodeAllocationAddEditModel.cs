
namespace Kaushal_Darpan.Models.GroupCodeAllocation
{
    public class GroupCodeAllocationAddEditModel : ResponseBaseModel
    {
        public int GroupCodeID { get; set; }//pk
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public int? GroupCode { get; set; }//new generate
        public int Total { get; set; }
        public int StartValue { get; set; }//group code start
        public int CommonSubjectID { get; set; }
        public string CommonSubjectName { get; set; } 
        public string SubjectCode { get; set; }
        public string? SubjectName { get; set; }
    }

    // group code
    public class GroupCodeAddEditModel : ResponseBaseModel
    {
        public int PageNumber { get; set; }
        public int PartitionSize { get; set; }
        public int GroupNo { get; set; }
        public int Total { get; set; }
        public int GroupCodeID { get; set; }//pk
        public int SemesterID { get; set; }
        public string SemesterName { get; set; }
        public int CommonSubjectID { get; set; }
        public string CommonSubjectName { get; set; }
        public string SubjectCode { get; set; }
        public string? SubjectName { get; set; }
        public int UpShiftPageNumber { get; set; }
        public string StudentExamPaperMarksIDs { get; set; }
        public string? StudentExamPaperRevaluationIDs { get; set; }
        public string? CenterCode { get; set; }
        public bool IsDirectPicked { get; set; }
    }
    // group code detail
    public class GroupCodeDetailAddEditModel
    {
        public int PageNumber { get; set; }
        public int GroupCodeDetailID { get; set; }//pk
        public int GroupCodeID { get; set; }//fk
        public int StudentID { get; set; }
        public int StudentExamID { get; set; }
        public int StudentExamPaperID { get; set; }
        public int StudentExamPaperMarksID { get; set; }
    }

    public class GroupCodeAllocationAddEditModel_Reval : ResponseBaseModel
    {
        public int GroupCodeID { get; set; }//pk
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public string? GroupCode { get; set; }//new generate
        public int Total { get; set; }
        public int StartValue { get; set; }//group code start
        public int CommonSubjectID { get; set; }
        public string CommonSubjectName { get; set; }
        public string SubjectCode { get; set; }
        public string? SubjectName { get; set; }
    }

}
