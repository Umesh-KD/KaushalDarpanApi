namespace Kaushal_Darpan.Models.CommonFunction
{
    public class CommonDDLSubjectMasterModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int SubjectID { get; set; }
        public int StaffID { get; set; }
    }
    public class CommonDDLSubjectCodeMasterModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int StudentExamID { get; set; }
        public int SubjectType { get; set; }

    }

    public class UserManualModel
    {
        public int RoleId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public string FilePath { get; set; }
    }
}
