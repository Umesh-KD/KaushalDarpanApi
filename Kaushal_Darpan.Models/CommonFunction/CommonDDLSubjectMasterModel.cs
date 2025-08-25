namespace Kaushal_Darpan.Models.CommonFunction
{
    public class CommonDDLSubjectMasterModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int SubjectID { get; set; }
    }
    public class CommonDDLSubjectCodeMasterModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int StudentExamID { get; set; }
        public int SubjectType { get; set; }

    }
}
