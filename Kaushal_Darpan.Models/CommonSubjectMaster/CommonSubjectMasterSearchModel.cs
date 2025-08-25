namespace Kaushal_Darpan.Models.CommonSubjectMaster
{
    public class CommonSubjectMasterSearchModel : RequestBaseModel
    {
        public string? CommonSubjectName { get; set; }
        public int? SemesterID { get; set; }
        public int? EndTermID { get; set; }
        public string? SubjectID { get; set; }
    }
}
