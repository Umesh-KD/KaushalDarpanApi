using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DocumentDetails
{
    public class DocumentDetailsModel
    {
        public int? DocumentDetailsID { get; set; }  //pk
        public int? DocumentMasterID { get; set; }  //fk
        public int? TransactionID { get; set; }  //other inserted table pk
        public string? TableName { get; set; }
        public string? ColumnName { get; set; }
        public string? DisplayColumnNameEn { get; set; }
        public string? DisplayColumnNameHi { get; set; }
        public string? FolderName { get; set; }
        public string? FileName { get; set; }
        public string? Dis_FileName { get; set; }
        public int? ModifyBy { get; set; }
        public string? IPAddress { get; set; }
        public bool? IsMandatory { get; set; }
        public int? GroupNo { get; set; }
        public int? SortOrder { get; set; }
        public string? MaxFileSize { get; set; }
        public string? MinFileSize { get; set; }
        public string? FileExtention { get; set; }
        public string ? Remark { get; set; }
        public string ? SubRemark { get; set; }
        public string ? CommonRemark { get; set; }
        public string ? OldFileName { get; set; }
        public int? Status { get; set; }
        public int? AcademicYearID { get; set; }
        public int? EndTermID { get; set; }
        public int? CourseType { get; set; }
    }
}
