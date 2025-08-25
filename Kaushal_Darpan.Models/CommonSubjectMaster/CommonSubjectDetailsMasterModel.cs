namespace Kaushal_Darpan.Models.CommonSubjectMaster
{
    public class CommonSubjectDetailsMasterModel
    {
        public int CommonSubjectDetailsID { get; set; }
        public int CommonSubjectID { get; set; }
        public int SubjectID { get; set; }
        public string ?SubjectCode { get; set; }
        public int StreamID { get; set; }
    }
}
