using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BterStudentJoinStatus
{
    public class BterAllotmentReportingModel
    {
        public int ReportingId { get; set; }
        public int AllotmentId { get; set; }
      
        public int ApplicationID { get; set; }
     
        public string? JoiningStatus { get; set; }
        public string? ReportingRemark { get; set; }
        public string? DOB { get; set; }
        public string? StudentName { get; set; }    
        public string? FatherName { get; set; }
        public string? StreamName { get; set; }
        public string? AllotedGender { get; set; }
        public string? AllotedCategory { get; set; }
        public string? fullAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public List<BterAllotmentDocumentModel> AllotmentDocumentModel { get; set; }
        public bool IsVoterIDAvailable { get; set; }
        public bool ApplyUpward { get; set; }
        public string FeeReciptNo { get; set; }
    }
    public class BterAllotmentDocumentModel
    {
        public int DocumentDetailsID { get; set; }
        public int DocumentMasterID { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string Remark { get; set; }
        public int GroupNo { get; set; }
        public bool DocumentStatus { get; set; }

    }
}
