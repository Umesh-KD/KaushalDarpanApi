using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.StudentsJoiningStatusMarks
{
    public class AllotmentReportingModel
    {
        public int ReportingId { get; set; }
        public int AllotmentId { get; set; }
        public int ShiftUnitID { get; set; }
        public int ApplicationID { get; set; }
        public int CollegeTradeId { get; set; }
        public string? ReportingStatus { get; set; }
        public string? JoiningStatus { get; set; }
        public string? ReportingRemark { get; set; }
        public string? DOB { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? TradeName { get; set; }
        public string? AllotedGender { get; set; }
        public string? AllotedCategory { get; set; }
        public string? fullAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? StreamName { get; set; }
        public string? Email { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public List<AllotmentDocumentModel> AllotmentDocumentModel { get; set; }
        public bool IsVoterIDAvailable { get; set; }
        public bool ApplyUpward { get; set; }
        public int CkeckStatus { get; set; }
        public int AllotedPriority { get; set; }
        public int StreamTypeID { get; set; }
        public string? FolderName {  get; set; }
        public string? CasteCategory {  get; set; }
        public string? IsRajasthani {  get; set; }
        public string? PreferenceCategory {  get; set; }
        public string? ApplicationNo {  get; set; }
    }
    public class AllotmentDocumentModel
    {
        public int DocumentDetailsID { get; set; }
        public int DocumentMasterID { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string? Dis_FileName { get; set; }
        public string Remark { get; set; }
        public int GroupNo { get; set; }
        public bool DocumentStatus { get; set; }

    }
}
