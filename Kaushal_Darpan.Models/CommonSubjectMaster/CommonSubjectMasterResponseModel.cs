using Kaushal_Darpan.Models.Report;

namespace Kaushal_Darpan.Models.CommonSubjectMaster
{
    public class CommonSubjectMasterResponseModel:ResponseBaseModel
    {
        public int CommonSubjectID { get; set; }
        public string CommonSubjectName { get; set; }
        public string SemesterName { get; set; }
        public string? SubjectNames { get; set; }

        public string? CreatedDate { get; set; }
        public string? CreatedByName { get; set; }
        public string? ModifyDate { get; set; }
        public string? ModifyByName { get; set; }
        public int? TotalRecords { get; set; }

    }
}
