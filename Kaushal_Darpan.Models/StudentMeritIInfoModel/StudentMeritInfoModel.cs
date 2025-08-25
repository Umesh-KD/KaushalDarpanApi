using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;

namespace Kaushal_Darpan.Models.StudentMeritIInfoModel
{

    public class StudentMeritInfoModel
    {
        public int ApplicationID { get; set; }
        public int MeritId { get; set; }
        public int AllotmentMasterId { get; set; }
        public string ApplicationNo { get; set; }
        public string? StudentName { get; set; }
        public string? Gender { get; set; }
        public string? categoryName { get; set; }
        public string? DOB { get; set; }
        public int Class { get; set; }
        public decimal TenthPer { get; set; }
        public decimal MaxMarks { get; set; }
        public decimal MarksObt { get; set; }
        public int CategoryId { get; set; }

        public int CourseTypeID { get; set; }
        public int DepartmentID { get; set; }
        public string? PrefentialCategory { get; set; }
        public int MeritNo { get; set; }
        public int CategoryMeritNo { get; set; }
        public bool IsPH { get; set; }
        public bool IsKM { get; set; }
        public bool IsWID { get; set; }
        public bool IsRajDOMICILE { get; set; }
        public bool IsSingleMotherDependent { get; set; }
        public bool IsTSP { get; set; }
        public bool IsExServicemen { get; set; }
        public bool ExServicemenId { get; set; }
        public Boolean IsEdit { get; set; }
        public bool IsApply { get; set; }
        public int Action { get; set; }
        public int MarksTypeID { get; set; }

        public decimal TenthMathsPer { get; set; }
        public decimal TenthSciencePer { get; set; }
        public decimal TwelvePer { get; set; }
        public decimal MaxPer { get; set; }
        public decimal OtherQuaPer { get; set; }
        public string? OtherQuaType { get; set; }
        public decimal EnglishQuaPer { get; set; }

        public int general_male { get; set; }
        public int general_female { get; set; }
        public int ews_male { get; set; }
        public int ews_female { get; set; }
        public int sc_male { get; set; }
        public int sc_female { get; set; }
        public int st_male { get; set; }
        public int st_female { get; set; }
        public int tsp_male { get; set; }
        public int tsp_female { get; set; }
        public int obc_male { get; set; }
        public int obc_female { get; set; }
        public int mbc_male { get; set; }
        public int mbc_female { get; set; }
        public int catB_MP_male_merit { get; set; }
        public int catB_MP_female_merit { get; set; }
        public int catC_PH_merit { get; set; }
        public int catE_SMD_merit { get; set; }



        public List<QualificationViewDetails> QualificationViewDetails { get; set; }
        public List<RecheckDocumentModel> RecheckDocumentModel { get; set; }
    }

    public class RecheckDocumentModel
    {
        public int DocumentDetailsID { get; set; } // pk
        public int MeritId { get; set; } // pk
        public int DocumentMasterID { get; set; } // fk
        public int TransactionID { get; set; } // other inserted table pk
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DisplayColumnNameEn { get; set; }
        public string DisplayColumnNameHi { get; set; }
        public string FolderName { get; set; }
        public string FileName { get; set; }
        public string Dis_FileName { get; set; }
        public int ModifyBy { get; set; }
        public bool IsMandatory { get; set; } = false;
        public string FileExtention { get; set; }
        public string MinFileSize { get; set; }
        public string MaxFileSize { get; set; }
        public int SortOrder { get; set; }
        public int GroupNo { get; set; }
        public string? Remark { get; set; }
        public int Status { get; set; }
        public bool Isselect { get; set; } = false;
    }

}
