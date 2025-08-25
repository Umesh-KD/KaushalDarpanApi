using Kaushal_Darpan.Models;

namespace Kaushal_Darpan.Core.Entities
{
    public class M_CommonSubject:RequestBaseModel
    {
        public int CommonSubjectID { get; set; }
        public string CommonSubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public int SemesterID { get; set; }
        public string Remark { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public List<M_CommonSubject_Details> commonSubjectDetails { get; set; }// child
    }
}
