namespace Kaushal_Darpan.Core.Entities
{
    public class M_CommonSubject_Details
    {
        public int CommonSubjectDetailsID { get; set; }
        public int CommonSubjectID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public string SubjectCode { get; set; } = "";

    }
}
