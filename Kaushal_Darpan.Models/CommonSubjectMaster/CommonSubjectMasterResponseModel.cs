using Kaushal_Darpan.Models.Report;

namespace Kaushal_Darpan.Models.CommonSubjectMaster
{
    public class CommonSubjectMasterResponseModel:ResponseBaseModel
    {
        public int CommonSubjectID { get; set; }
        public string CommonSubjectName { get; set; }
        public string SemesterName { get; set; }
        public string? SubjectNames { get; set; }
    }
}
