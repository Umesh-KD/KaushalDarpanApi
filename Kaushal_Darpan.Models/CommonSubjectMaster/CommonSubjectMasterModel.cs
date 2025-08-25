namespace Kaushal_Darpan.Models.CommonSubjectMaster
{
    public class CommonSubjectMasterModel : RequestBaseModel
    {
        public int CommonSubjectID { get; set; }
        public string CommonSubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public int SemesterID { get; set; }
        public int ModifyBy { get; set; }
        public List<CommonSubjectDetailsMasterModel> commonSubjectDetails { get; set; }//child
    }
}
